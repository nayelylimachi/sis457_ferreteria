﻿using CadFerreteria;
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
        private List<FrmBusquedaProductos.ProductoSeleccionado> productosSeleccionados = new List<FrmBusquedaProductos.ProductoSeleccionado>();

        public FrmVenta()
        {
            InitializeComponent();
            ConfigurarDataGridView();

            txbEfectivo.KeyPress += txtEfectivo_KeyPress;
            txbEfectivo.TextChanged += txtEfectivo_TextChanged;

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
            if (string.IsNullOrWhiteSpace(txbEfectivo.Text))
            {
                MessageBox.Show("Debe agregar efectivo.", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Intentar parsear el Total y el Efectivo
            string totalTexto = txtTotal.Text.Replace("Bs. ", "").Trim();
            decimal total;
            decimal efectivo;

            // Primero, validar si el campo total se puede convertir a número
            if (!decimal.TryParse(totalTexto, out total))
            {
                MessageBox.Show("Error al leer el total de la venta. Verifique el formato.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Segundo, validar si el campo efectivo es un número válido y mayor que cero
            if (!decimal.TryParse(txbEfectivo.Text.Replace(',', '.'), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out efectivo) || efectivo <= 0)
            {
                MessageBox.Show("El monto de efectivo debe ser un número válido y mayor que cero.", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // --- ¡NUEVA VALIDACIÓN CLAVE PARA PAGO INSUFICIENTE! ---
            if (efectivo < total)
            {
                MessageBox.Show("El efectivo ingresado es insuficiente para cubrir el total de la venta.", "Pago Insuficiente",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Detiene el proceso de registro de la venta
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
                idUsuario = Util.usuario.id, // Asegúrate que Util.usuario.id es válido y existe en la tabla Usuario
                estado = 1,
                usuarioRegistro = Util.usuario.usuario1,
                transaccion = new Random().Next(1000, 999999)
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
                    total = producto.Cantidad * producto.Precio,
                    usuarioRegistro = Util.usuario.usuario1,
                    fechaRegistro = DateTime.Now,            
                    estado = 1                               

                };
                BusquedaProductoCln.insertar(detalle);

                Producto productoEnDb = ProductoCln.obtener(producto.IdProducto);

                if (productoEnDb != null)
                {
                    // Verifica que haya suficiente saldo antes de restar (aunque ya se hizo al agregar al carrito, es buena práctica)
                    if (productoEnDb.saldo >= producto.Cantidad)
                    {
                        productoEnDb.saldo -= producto.Cantidad;
                        // También actualiza los campos de auditoría si tu método de actualizar lo requiere
                        productoEnDb.usuarioRegistro = Util.usuario.usuario1; // Usuario que realizó la modificación
                                                                              // productoEnDb.fechaRegistro = DateTime.Now; // Esto es fecha de registro, no de modificación

                        ProductoCln.actualizarSaldo(productoEnDb); // Necesitas crear este método
                    }
                    else
                    {
                        MessageBox.Show($"Advertencia: Saldo insuficiente para el producto {productoEnDb.descripcion}. No se actualizó el inventario.", "Advertencia de Inventario", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show($"Advertencia: Producto con ID {producto.IdProducto} no encontrado en la base de datos. No se actualizó el inventario.", "Advertencia de Inventario", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
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
            var frmDetalle = new FrmBusquedaProductos();
            if (frmDetalle.ShowDialog() == DialogResult.OK && frmDetalle.ProductoElegido != null)
            {
                productosSeleccionados.Add(frmDetalle.ProductoElegido);
                ActualizarListaProductos();
            }
        }

        private void ActualizarListaProductos()
        {
            dgvLista.Rows.Clear();

            foreach (var p in productosSeleccionados)
            {
                dgvLista.Rows.Add(p.IdProducto, p.Nombre, p.Precio, p.Cantidad, p.Precio * p.Cantidad);
            }

            // Total de la venta
            decimal total = productosSeleccionados.Sum(p => p.Precio * p.Cantidad);
            txtTotal.Text = $"Bs. {total:#,##0.00}";
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
        private void txtEfectivo_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox tb = sender as TextBox;

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != ',')
            {
                e.Handled = true;
            }

            // Permitir solo un separador decimal (punto o coma)
            if ((e.KeyChar == '.' || e.KeyChar == ',') && (tb.Text.Contains(".") || tb.Text.Contains(",")))
            {
                e.Handled = true;
            }
        }

        private void txtEfectivo_TextChanged(object sender, EventArgs e)
        {
            CalcularVuelto();
        }

        private void CalcularVuelto()
        {
            // Quitar "Bs. " del texto para parsear sólo el número
            string totalTexto = txtTotal.Text.Replace("Bs. ", "").Trim();

            if (decimal.TryParse(totalTexto, out decimal total) &&
                decimal.TryParse(txbEfectivo.Text.Replace(',', '.'), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out decimal efectivo))
            {
                decimal vuelto = efectivo - total;
                if (vuelto < 0)
                {
                    txbCambio.Text = "Pago insuficiente";
                }
                else
                {
                    txbCambio.Text = $"Bs. {vuelto:#,##0.00}";
                }
            }
            else
            {
                txbCambio.Text = string.Empty;
            }
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