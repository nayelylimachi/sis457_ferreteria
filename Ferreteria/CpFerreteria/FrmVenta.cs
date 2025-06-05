using CadFerreteria;
using ClnFerreteria;
using CpMinerva;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CpFerreteria
{
    public partial class FrmVenta : Form
    {
        private Cliente clienteActual = null;
        private List<FrmVentaDetalle.ProductoSeleccionado> productosSeleccionados = new List<FrmVentaDetalle.ProductoSeleccionado>();

        public FrmVenta()
        {
            InitializeComponent();
            ConfigurarDataGridView();
        }
        private void ConfigurarDataGridView()
        {
            // Configurar el DataGridView
            dgvLista.AutoGenerateColumns = false;

            // Limpiar columnas existentes
            dgvLista.Columns.Clear();

            // Agregar columnas manualmente
            dgvLista.Columns.Add("IdProducto", "Código");
            dgvLista.Columns.Add("Nombre", "Producto");
            dgvLista.Columns.Add("Precio", "Precio Unitario");
            dgvLista.Columns["Precio"].DefaultCellStyle.Format = "C2";
            dgvLista.Columns.Add("Cantidad", "Cantidad");
            dgvLista.Columns.Add("Total", "Total");
            dgvLista.Columns["Total"].DefaultCellStyle.Format = "C2";
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnRegistrarVenta_Click_1(object sender, EventArgs e)
        {
            string nit = txtCiNit.Text.Trim();
            string nombre = txtNombreCliente.Text.Trim();

            if (string.IsNullOrEmpty(nit) || string.IsNullOrEmpty(nombre))
            {
                MessageBox.Show("Debe ingresar el NIT y el nombre del cliente.", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (productosSeleccionados.Count == 0)
            {
                MessageBox.Show("Debe agregar al menos un producto a la venta.", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Si el cliente no existe, se crea uno nuevo
            if (clienteActual == null)
            {
                clienteActual = new Cliente
                {
                    nit = nit,
                    nombre = nombre,
                    estado = 1,
                    usuarioRegistro = "admin",
                    fechaRegistro = DateTime.Now
                    // Usuario actual
                };
                clienteActual.id = ClienteCln.insertar(clienteActual);
            }

            // Registrar la venta
            Venta venta = new Venta
            {
                fecha = DateTime.Now,
                idCliente = clienteActual.id,
                estado = 1,
                usuarioRegistro = Util.usuario.usuario1,
                transaccion = new Random().Next(1000, 999999) // o algún sistema de generación de transacción
            };

            int idVenta = VentaCln.insertar(venta);

            // Registrar los detalles de venta
            foreach (var producto in productosSeleccionados)
            {
                VentaDetalle detalle = new VentaDetalle
                {
                    idVenta = idVenta,
                    idProducto = producto.IdProducto,
                    cantidad = producto.Cantidad,
                    precioUnitario = producto.Precio,
                    total = producto.Cantidad * producto.Precio
                };
                VentaDetalleCln.insertar(detalle);
            }

            // Limpiar después de registrar
            productosSeleccionados.Clear();
            ActualizarListaProductos();
            txtCiNit.Text = "";
            txtNombreCliente.Text = "";
            txtNombreCliente.Enabled = true;
            clienteActual = null;

            MessageBox.Show($"Venta registrada correctamente. N° {idVenta}", "Éxito",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            LimpiarFormulario();
        }

        private void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            string nit = txtCiNit.Text.Trim();
            if (string.IsNullOrEmpty(nit))
            {
                MessageBox.Show("Debe ingresar un NIT para buscar.", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            clienteActual = ClienteCln.bucar(nit);
            if (clienteActual != null)
            {
                txtNombreCliente.Text = clienteActual.nombre;
                txtNombreCliente.Enabled = false;
            }
            else
            {
                txtNombreCliente.Text = string.Empty;
                txtNombreCliente.Enabled = true;
                MessageBox.Show("Cliente no encontrado. Se registrará uno nuevo al guardar la venta.",
                    "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            var frmDetalle = new FrmVentaDetalle();
            if (frmDetalle.ShowDialog() == DialogResult.OK && frmDetalle.ProductoElegido != null)
            {
                productosSeleccionados.Add(frmDetalle.ProductoElegido);
                ActualizarListaProductos();
            }
        }

        private void ActualizarListaProductos()
        {
            // Actualizar DataGridView
            dgvLista.DataSource = null;
            dgvLista.DataSource = productosSeleccionados.Select(p => new
            {
                idProducto = p.IdProducto,
                nombre = p.Nombre,
                precio = $"Bs. {p.Precio}",
                cantidad = p.Cantidad,
                Total = $"Bs. {(p.Precio * p.Cantidad)}"
            }).ToList();

            // Calcular y mostrar el total en bolivianos
            decimal total = productosSeleccionados.Sum(p => p.Precio * p.Cantidad);
            txtTotal.Text = $"Bs. {total.ToString("#,##0.00")}";  // Formato: Bs. 1,250.50
        }

        private void LimpiarFormulario()
        {
            txtCiNit.Text = "";
            txtNombreCliente.Text = "";
            txtNombreCliente.Enabled = true;
            clienteActual = null;
            productosSeleccionados.Clear();
            dgvLista.DataSource = null;
            txtTotal.Text = "Total: 0.00";
        }

        private void txtCiNit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnBuscarCliente.PerformClick();
            }
        }
    }
}