using ClnFerreteria;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CpFerreteria
{
    public partial class FrmBusquedaProductos : Form
    {
        public ProductoSeleccionado ProductoElegido { get; private set; }

        public class ProductoSeleccionado
        {
            public int IdProducto { get; set; }
            public string Nombre { get; set; }
            public decimal Precio { get; set; }
            public int Cantidad { get; set; }
        }
        public FrmBusquedaProductos()
        {
            InitializeComponent();
        }

        private void CargarProductos()
        {
            var lista = ProductoCln.listarPa(txtParametro.Text.Trim());
            dgvLista.DataSource = lista;
            dgvLista.Columns["id"].Visible = false;
            dgvLista.Columns["estado"].Visible = false;
            dgvLista.Columns["idCategoria"].Visible = false;
            dgvLista.Columns["idProveedor"].Visible = false;
            dgvLista.Columns["codigo"].HeaderText = "Código";
            dgvLista.Columns["descripcion"].HeaderText = "Descripción";
            dgvLista.Columns["precioVenta"].HeaderText = "Precio de Venta";
            dgvLista.Columns["saldo"].HeaderText = "Saldo";
            dgvLista.Columns["unidadMedida"].HeaderText = "Unidad de Medida";
            dgvLista.Columns["usuarioRegistro"].Visible = false;
            dgvLista.Columns["fechaRegistro"].Visible = false;
            if (lista.Count > 0) dgvLista.CurrentCell = dgvLista.Rows[0].Cells["codigo"];
        }

        private void FrmBusquedaProductos_Load(object sender, EventArgs e)
        {
            CargarProductos();
        }

        private void btnSeleccionarProducto_Click_1(object sender, EventArgs e)
        {
            if (dgvLista.CurrentRow != null)
            {
                int index = dgvLista.CurrentCell.RowIndex;
                int id = Convert.ToInt32(dgvLista.Rows[index].Cells["id"].Value);
                var descripcion = dgvLista.CurrentRow.Cells["descripcion"].Value.ToString();
                var precioUnitario = Convert.ToDecimal(dgvLista.CurrentRow.Cells["precioVenta"].Value);
                var cantidad = (int)nudCantidad.Value;

                if (cantidad <= 0)
                {
                    MessageBox.Show("La cantidad debe ser mayor a 0", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                ProductoElegido = new ProductoSeleccionado
                {
                    IdProducto = id,
                    Nombre = descripcion,
                    Precio = precioUnitario,
                    Cantidad = cantidad
                };

                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("Seleccione un producto.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarProductos();
        }

        private void txtParametro_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter) CargarProductos();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
