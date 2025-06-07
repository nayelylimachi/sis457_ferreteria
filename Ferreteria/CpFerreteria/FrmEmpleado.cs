using CadFerreteria;
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
    public partial class FrmEmpleado : Form
    {
        private bool esNuevo = false;
        public FrmEmpleado()
        {
            InitializeComponent();
        }
        private void listar()
        {
            var lista = EmpleadoCln.listarPa(txtParametro.Text.Trim());
            dgvLista.DataSource = lista;
            dgvLista.Columns["id"].Visible = false;
            dgvLista.Columns["estado"].Visible = false;
            dgvLista.Columns["usuario"].HeaderText = "Usuario";
            dgvLista.Columns["cedulaIdentidad"].HeaderText = "Cédula de Identidad";
            dgvLista.Columns["nombre"].HeaderText = "Nombre";
            dgvLista.Columns["primerApellido"].HeaderText = "Primer Apellido";
            dgvLista.Columns["segundoApellido"].HeaderText = "Segundo Apellido";
            dgvLista.Columns["direccion"].HeaderText = "Dirección";
            dgvLista.Columns["celular"].HeaderText = "Celular";
            dgvLista.Columns["cargo"].HeaderText = "Cargo";
            dgvLista.Columns["usuarioRegistro"].HeaderText = "Usuario Registro";
            dgvLista.Columns["fechaRegistro"].HeaderText = "Fecha Registro";
            if (lista.Count > 0) dgvLista.CurrentCell = dgvLista.Rows[0].Cells["cedulaIdentidad"];
            btnEditar.Enabled = lista.Count > 0;
            btnEliminar.Enabled = lista.Count > 0;
        }
        private void FrmEmpleado_Load(object sender, EventArgs e)
        {
            Size = new Size(650, 362);
            listar();
            txtCelular.KeyPress += Util.onlyNumbers;
            txtNombres.KeyPress += Util.onlyLetters;
            txtPrimerApellido.KeyPress += Util.onlyLetters;
            txtSegundoApellido.KeyPress += Util.onlyLetters;

        }
        private void limpiar()
        {
            txtCedulaIdentidad.Clear();
            txtNombres.Clear();
            txtPrimerApellido.Clear();
            txtSegundoApellido.Clear();
            txtDireccion.Clear();
            txtCelular.Clear();
            txtUsuario.Clear();
            cbxCargo.SelectedIndex = -1;
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Size = new Size(650, 362);
            limpiar();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            esNuevo = true;
            Size = new Size(650, 487);
            txtCedulaIdentidad.Focus();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvLista.CurrentRow == null)
            {
                MessageBox.Show("Debe seleccionar un empleado para editar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Detiene la ejecución del método
            }
            esNuevo = false;
            Size = new Size(650, 487);

            int index = dgvLista.CurrentCell.RowIndex;
            int id = Convert.ToInt32(dgvLista.Rows[index].Cells["id"].Value);
            var empleado = EmpleadoCln.obtenerUno(id);
            var usuario = empleado.Usuario.Count > 0 ? empleado.Usuario.First().usuario1 : "";
            txtCedulaIdentidad.Text = empleado.cedulaIdentidad;
            txtNombres.Text = empleado.nombre;
            txtPrimerApellido.Text = empleado.primerApellido;
            txtSegundoApellido.Text = empleado.segundoApellido;
            txtDireccion.Text = empleado.direccion;
            txtCelular.Text = empleado.celular.ToString();
            txtUsuario.Text = usuario;
            cbxCargo.Text = empleado.cargo;
            txtCedulaIdentidad.Focus();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtParametro_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter) listar();
        }
        
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            var empleado = new Empleado();
            empleado.cedulaIdentidad = txtCedulaIdentidad.Text.Trim();
            empleado.nombre = txtNombres.Text.Trim();
            empleado.primerApellido = txtPrimerApellido.Text.Trim();
            empleado.segundoApellido = txtSegundoApellido.Text.Trim();
            empleado.direccion = txtDireccion.Text.Trim();
            empleado.celular = Convert.ToInt64(txtCelular.Text);
            empleado.cargo = cbxCargo.Text;
            empleado.usuarioRegistro = Util.usuario.usuario1;

            Usuario usuario = null;
            if (!string.IsNullOrEmpty(txtUsuario.Text))
            {
                usuario = new Usuario();
                usuario.usuario1 = txtUsuario.Text.Trim();
                usuario.contraseña = Util.Encrypt("hola123");
            }

            if (esNuevo)
            {
                empleado.fechaRegistro = DateTime.Now;
                empleado.estado = 1;
                EmpleadoCln.insertar(empleado, usuario);
            }
            else
            {
                int index = dgvLista.CurrentCell.RowIndex;
                empleado.id = Convert.ToInt32(dgvLista.Rows[index].Cells["id"].Value);
                EmpleadoCln.actualizar(empleado, txtUsuario.Text.Trim(), Util.Encrypt("hola123"));
            }
            listar();
            btnCancelar.PerformClick();
            MessageBox.Show("Registro guardado correctamente", "::: Minerva - Mensaje :::",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int index = dgvLista.CurrentCell.RowIndex;
            int id = Convert.ToInt32(dgvLista.Rows[index].Cells["id"].Value);
            string ci = dgvLista.Rows[index].Cells["cedulaIdentidad"].Value.ToString();
            DialogResult dialog = MessageBox.Show($"¿Está seguro de eliminar el empleado {ci}?",
                "::: Minerva - Mensaje :::", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                EmpleadoCln.eliminar(id, Util.usuario.usuario1);
                listar();
                MessageBox.Show("Producto dado de baja correctamente", "::: Minerva - Mensaje :::",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            listar();
        }
    }
}
