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
        public FrmProducto()
        {
            InitializeComponent();
        }

        private void FrmProducto_Load(object sender, EventArgs e)
        {
            Size = new Size(1124, 450);
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Size = new Size(1124, 606);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Size = new Size(1124, 450);
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
