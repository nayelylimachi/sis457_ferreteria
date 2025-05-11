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
    public partial class FrmVenta: Form
    {
        public FrmVenta()
        {
            InitializeComponent();

            // Desactivar la generación automática de columnas
            dgvDetalleVenta.AutoGenerateColumns = false;

            // Llamar método para configurar columnas si quieres hacerlo todo por código
            ConfigurarColumnasDetalle();
        }

        private void ConfigurarColumnasDetalle()
        {
            dgvDetalleVenta.Columns.Clear();

            dgvDetalleVenta.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colProducto",
                HeaderText = "Producto",
                ReadOnly = true,
                Width = 200
            });

            dgvDetalleVenta.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colCantidad",
                HeaderText = "Cantidad",
                ReadOnly = true,
                Width = 70,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight }
            });

            dgvDetalleVenta.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colPrecioUnitario",
                HeaderText = "Precio Unitario",
                ReadOnly = true,
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight }
            });

            dgvDetalleVenta.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colTotal",
                HeaderText = "Total",
                ReadOnly = true,
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight }
            });

            dgvDetalleVenta.Columns.Add(new DataGridViewButtonColumn
            {
                Name = "colEliminar",
                HeaderText = "Eliminar",
                Text = "🗑️",
                UseColumnTextForButtonValue = true,
                Width = 70
            });
        }

    }
}
