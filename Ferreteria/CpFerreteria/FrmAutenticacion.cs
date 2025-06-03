using ClnFerreteria;
using CpMinerva;
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
    public partial class FrmAutenticacion : Form
    {

        public FrmAutenticacion()
        {
            InitializeComponent();
            //txtUsuario.Text = Util.Encrypt("123456");
        }
        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private bool validar()
        {
            bool esValido = true;
            erpUsuario.SetError(txtUsuario, "");
            erpContraseña.SetError(txtContraseña, "");

            if (string.IsNullOrEmpty(txtUsuario.Text))
            {
                erpUsuario.SetError(txtUsuario, "El Usuario es obligatorio");
                esValido = false;
            }
            if (string.IsNullOrEmpty(txtContraseña.Text))
            {
                erpContraseña.SetError(txtContraseña, "La Contraseña es obligatoria");
                esValido = false;
            }

            return esValido;
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            if (validar())
            {
                var usuario = UsuarioCln.validar(txtUsuario.Text, Util.Encrypt(txtContraseña.Text));
                if (usuario != null)
                {
                    Util.usuario = usuario;
                    txtContraseña.Clear();
                    txtUsuario.Focus();
                    txtUsuario.SelectAll();
                    Hide(); // Ocultar el formulario de autenticación
                    new FrmPrincipal(this).ShowDialog();
                }
                else
                    {
                    MessageBox.Show("Usuario y/o contraseña incorrectos", "::: Ferreteria - Mensaje :::",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtUsuario.Focus();
                }
            }
        }

        private void txtContraseña_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter) btnIngresar.PerformClick(); // Simular el clic en el botón Ingresar
        }
    }
}
