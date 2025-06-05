using CadFerreteria;
using ClnFerreteria;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CpFerreteria
{
    public partial class FrmProducto: Form
    {
        private bool esNuevo = false;
        public FrmProducto()
        {
            InitializeComponent();
        }

        private void listar()
        {
            var lista = ProductoCln.listarPa(txtParametro.Text.Trim());
            dgvLista.DataSource = lista;
            dgvLista.Columns["id"].Visible = false;
            dgvLista.Columns["estado"].Visible = false;
            dgvLista.Columns["idCategoria"].HeaderText = "Categoría";
            dgvLista.Columns["idProveedor"].HeaderText = "Proveedor";
            dgvLista.Columns["codigo"].HeaderText = "Código";
            dgvLista.Columns["descripcion"].HeaderText = "Descripción";
            dgvLista.Columns["precioVenta"].HeaderText = "Precio de Venta";
            dgvLista.Columns["saldo"].HeaderText = "Saldo";
            dgvLista.Columns["unidadMedida"].HeaderText = "Unidad de Medida";
            dgvLista.Columns["usuarioRegistro"].HeaderText = "Usuario Registro";
            dgvLista.Columns["fechaRegistro"].HeaderText = "Fecha de Registro";
            dgvLista.Columns["fechaRegistro"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";
            if (lista.Count>0) dgvLista.CurrentCell=dgvLista.Rows[0].Cells["codigo"];
            btnEditar.Enabled= lista.Count > 0;
            btnEliminar.Enabled = lista.Count > 0;
        }

        private void FrmProducto_Load(object sender, EventArgs e)
        {
            Size = new Size(1124, 445);
            listar();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            esNuevo = true;
            Size = new Size(1124, 610);
            txtCodigo.Focus();
        }
        private void btnEditar_Click(object sender, EventArgs e)
        {
            esNuevo = false;
            Size = new Size(1124, 606);

            int index = dgvLista.CurrentCell.RowIndex;
            int id = Convert.ToInt32(dgvLista.Rows[index].Cells["id"].Value);
            var producto = ProductoCln.obtenerUno(id);
            txtCodigo.Text = producto.codigo;
            txtDescripcion.Text = producto.descripcion;
            nudPrecioVenta.Value = producto.precioVenta;
            nudSaldo.Value = producto.saldo;
            cbxUnidadMedida.SelectedItem = producto.unidadMedida;
            cbxCategoria.SelectedItem = producto.idCategoria.ToString();
            cbxProveedor.SelectedItem = producto.idProveedor.ToString();
            txtCodigo.Focus();
        }

        private void limpiar()
        {
            txtCodigo.Clear();
            txtDescripcion.Clear();
            nudPrecioVenta.Value = 0;
            nudSaldo.Value = 0;
            cbxUnidadMedida.SelectedIndex = -1;
            cbxCategoria.SelectedIndex = -1;
            cbxProveedor.SelectedIndex = -1;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Size = new Size(1124, 450);
            limpiar();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            listar();
        }

        private void txtParametro_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter) listar();
        }

        private bool validar()
        {
            bool esValido = true;
            erpCodigo.SetError(txtCodigo, "");
            erpDescripcion.SetError(txtDescripcion, "");
            erpPrecioVenta.SetError(nudPrecioVenta, "");
            erpSaldo.SetError(nudSaldo, "");
            erpUnidadMedida.SetError(cbxUnidadMedida, "");

            if (string.IsNullOrWhiteSpace(txtCodigo.Text.Trim()))
            {
                erpCodigo.SetError(txtCodigo, "El campo código es obligatorio");
                esValido = false;
            }
            if (string.IsNullOrWhiteSpace(txtDescripcion.Text.Trim()))
            {
                erpDescripcion.SetError(txtDescripcion, "El campo descripción es obligatorio");
                esValido = false;
            }
            if (cbxUnidadMedida.SelectedIndex < 0)
            {
                erpUnidadMedida.SetError(cbxUnidadMedida, "El campo unidad de Medida es obligatio");
                esValido = false;
            }
            if (nudPrecioVenta.Value < 0)
            {
                erpPrecioVenta.SetError(nudPrecioVenta, "El campo precio de venta debe no debe ser menor a cero");
                esValido = false;
            }
            if (nudSaldo.Value < 0)
            {
                erpSaldo.SetError(nudSaldo, "El campo saldo no puede ser menor a cero");
                esValido = false;
            }
            if (cbxCategoria.SelectedIndex < 0)
            {
                erpCategoria.SetError(cbxCategoria, "El campo categoría es obligatorio");
                esValido = false;
            }
            if (cbxProveedor.SelectedIndex < 0)
            {
                erpProveedor.SetError(cbxProveedor, "El campo proveedor es obligatorio");
                esValido = false;
            }


                return esValido;
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (validar())
            {
                var producto = new Producto();
                producto.codigo = txtCodigo.Text.Trim();
                producto.descripcion = txtDescripcion.Text.Trim();
                producto.precioVenta = nudPrecioVenta.Value;
                producto.saldo = nudSaldo.Value;
                producto.unidadMedida = cbxUnidadMedida.SelectedItem.ToString();
                producto.idCategoria = Convert.ToInt32(cbxCategoria.SelectedItem.ToString());
                producto.idProveedor = Convert.ToInt32(cbxProveedor.SelectedItem.ToString());
                producto.usuarioRegistro = "admin"; // Cambiar por el usuario actual

                if(esNuevo)
                {
                    producto.fechaRegistro = DateTime.Now;
                    producto.estado = 1; // Activo
                    ProductoCln.insertar(producto);
                }
                else
                {
                    int index = dgvLista.CurrentCell.RowIndex;
                    producto.id = Convert.ToInt32(dgvLista.Rows[index].Cells["id"].Value);
                    ProductoCln.actualizar(producto);
                }
                listar();
                btnCancelar.PerformClick();
                MessageBox.Show("Producto guardado correctamente.", "::: Ferreteria - Mensaje",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int index = dgvLista.CurrentCell.RowIndex;
            int id = Convert.ToInt32(dgvLista.Rows[index].Cells["id"].Value);
            string codigo = dgvLista.Rows[index].Cells["codigo"].Value.ToString();
            DialogResult dialog = MessageBox.Show($"¿Está seguro de eliminar el producto {codigo}?", "::: Ferreteria - Mensaje :::",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                ProductoCln.eliminar(id, "admin");
                listar();
                MessageBox.Show("Producto eliminado correctamente.", "::: Ferreteria - Mensaje :::",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
