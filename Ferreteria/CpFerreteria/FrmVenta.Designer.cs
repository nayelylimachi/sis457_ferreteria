namespace CpFerreteria
{
    partial class FrmVenta
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmVenta));
            this.dtpFecha = new System.Windows.Forms.DateTimePicker();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.dgvDetalleVenta = new System.Windows.Forms.DataGridView();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblCiNit = new System.Windows.Forms.Label();
            this.txtCiNit = new System.Windows.Forms.TextBox();
            this.btnBuscarCliente = new System.Windows.Forms.Button();
            this.lblNombreCliente = new System.Windows.Forms.Label();
            this.txtbNombreCliente = new System.Windows.Forms.TextBox();
            this.lblFecha = new System.Windows.Forms.Label();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.ptbUsuario = new System.Windows.Forms.PictureBox();
            this.txbUsuario = new System.Windows.Forms.TextBox();
            this.lblProducto = new System.Windows.Forms.Label();
            this.cmbProducto = new System.Windows.Forms.ComboBox();
            this.lblCantidad = new System.Windows.Forms.Label();
            this.txbCantidad = new System.Windows.Forms.TextBox();
            this.txbPrecio = new System.Windows.Forms.TextBox();
            this.lblPrecio = new System.Windows.Forms.Label();
            this.txbTotal = new System.Windows.Forms.TextBox();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnRegistrarVenta = new System.Windows.Forms.Button();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.gbxCliente = new System.Windows.Forms.GroupBox();
            this.gbxProducto = new System.Windows.Forms.GroupBox();
            this.gbxListaProductos = new System.Windows.Forms.GroupBox();
            this.pnlAcciones = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalleVenta)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptbUsuario)).BeginInit();
            this.gbxCliente.SuspendLayout();
            this.gbxProducto.SuspendLayout();
            this.gbxListaProductos.SuspendLayout();
            this.pnlAcciones.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtpFecha
            // 
            this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFecha.Location = new System.Drawing.Point(190, 53);
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.Size = new System.Drawing.Size(126, 22);
            this.dtpFecha.TabIndex = 0;
            // 
            // btnAgregar
            // 
            this.btnAgregar.AutoSize = true;
            this.btnAgregar.Image = ((System.Drawing.Image)(resources.GetObject("btnAgregar.Image")));
            this.btnAgregar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAgregar.Location = new System.Drawing.Point(385, 85);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(140, 54);
            this.btnAgregar.TabIndex = 3;
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.UseVisualStyleBackColor = true;
            // 
            // dgvDetalleVenta
            // 
            this.dgvDetalleVenta.AllowUserToAddRows = false;
            this.dgvDetalleVenta.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetalleVenta.Location = new System.Drawing.Point(55, 21);
            this.dgvDetalleVenta.Name = "dgvDetalleVenta";
            this.dgvDetalleVenta.ReadOnly = true;
            this.dgvDetalleVenta.RowHeadersWidth = 51;
            this.dgvDetalleVenta.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDetalleVenta.Size = new System.Drawing.Size(623, 136);
            this.dgvDetalleVenta.TabIndex = 4;
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.Location = new System.Drawing.Point(529, 166);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(43, 16);
            this.lblTotal.TabIndex = 5;
            this.lblTotal.Text = "Total";
            // 
            // lblCiNit
            // 
            this.lblCiNit.AutoSize = true;
            this.lblCiNit.Location = new System.Drawing.Point(124, 28);
            this.lblCiNit.Name = "lblCiNit";
            this.lblCiNit.Size = new System.Drawing.Size(48, 16);
            this.lblCiNit.TabIndex = 10;
            this.lblCiNit.Text = "CI/NIT:";
            // 
            // txtCiNit
            // 
            this.txtCiNit.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCiNit.Location = new System.Drawing.Point(199, 25);
            this.txtCiNit.MaxLength = 15;
            this.txtCiNit.Name = "txtCiNit";
            this.txtCiNit.Size = new System.Drawing.Size(170, 22);
            this.txtCiNit.TabIndex = 11;
            // 
            // btnBuscarCliente
            // 
            this.btnBuscarCliente.AutoSize = true;
            this.btnBuscarCliente.Image = ((System.Drawing.Image)(resources.GetObject("btnBuscarCliente.Image")));
            this.btnBuscarCliente.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBuscarCliente.Location = new System.Drawing.Point(410, 15);
            this.btnBuscarCliente.Name = "btnBuscarCliente";
            this.btnBuscarCliente.Size = new System.Drawing.Size(115, 44);
            this.btnBuscarCliente.TabIndex = 12;
            this.btnBuscarCliente.Text = "Buscar";
            this.btnBuscarCliente.UseVisualStyleBackColor = true;
            // 
            // lblNombreCliente
            // 
            this.lblNombreCliente.AutoSize = true;
            this.lblNombreCliente.Location = new System.Drawing.Point(69, 67);
            this.lblNombreCliente.Name = "lblNombreCliente";
            this.lblNombreCliente.Size = new System.Drawing.Size(103, 16);
            this.lblNombreCliente.TabIndex = 13;
            this.lblNombreCliente.Text = "Nombre Cliente:";
            // 
            // txtbNombreCliente
            // 
            this.txtbNombreCliente.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txtbNombreCliente.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtbNombreCliente.Location = new System.Drawing.Point(199, 64);
            this.txtbNombreCliente.MaxLength = 15;
            this.txtbNombreCliente.Name = "txtbNombreCliente";
            this.txtbNombreCliente.ReadOnly = true;
            this.txtbNombreCliente.Size = new System.Drawing.Size(326, 22);
            this.txtbNombreCliente.TabIndex = 14;
            // 
            // lblFecha
            // 
            this.lblFecha.AutoSize = true;
            this.lblFecha.Location = new System.Drawing.Point(124, 58);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(48, 16);
            this.lblFecha.TabIndex = 15;
            this.lblFecha.Text = "Fecha:";
            // 
            // lblUsuario
            // 
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.Location = new System.Drawing.Point(971, 233);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(54, 16);
            this.lblUsuario.TabIndex = 16;
            this.lblUsuario.Text = "Usuario";
            // 
            // ptbUsuario
            // 
            this.ptbUsuario.Image = ((System.Drawing.Image)(resources.GetObject("ptbUsuario.Image")));
            this.ptbUsuario.Location = new System.Drawing.Point(918, 65);
            this.ptbUsuario.Name = "ptbUsuario";
            this.ptbUsuario.Size = new System.Drawing.Size(148, 125);
            this.ptbUsuario.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ptbUsuario.TabIndex = 17;
            this.ptbUsuario.TabStop = false;
            // 
            // txbUsuario
            // 
            this.txbUsuario.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txbUsuario.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txbUsuario.Location = new System.Drawing.Point(941, 197);
            this.txbUsuario.MaxLength = 15;
            this.txbUsuario.Name = "txbUsuario";
            this.txbUsuario.ReadOnly = true;
            this.txbUsuario.Size = new System.Drawing.Size(116, 22);
            this.txbUsuario.TabIndex = 18;
            // 
            // lblProducto
            // 
            this.lblProducto.AutoSize = true;
            this.lblProducto.Location = new System.Drawing.Point(107, 26);
            this.lblProducto.Name = "lblProducto";
            this.lblProducto.Size = new System.Drawing.Size(64, 16);
            this.lblProducto.TabIndex = 19;
            this.lblProducto.Text = "Producto:";
            // 
            // cmbProducto
            // 
            this.cmbProducto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProducto.FormattingEnabled = true;
            this.cmbProducto.Location = new System.Drawing.Point(190, 23);
            this.cmbProducto.Name = "cmbProducto";
            this.cmbProducto.Size = new System.Drawing.Size(334, 24);
            this.cmbProducto.TabIndex = 20;
            // 
            // lblCantidad
            // 
            this.lblCantidad.AutoSize = true;
            this.lblCantidad.Location = new System.Drawing.Point(108, 84);
            this.lblCantidad.Name = "lblCantidad";
            this.lblCantidad.Size = new System.Drawing.Size(64, 16);
            this.lblCantidad.TabIndex = 21;
            this.lblCantidad.Text = "Cantidad:";
            // 
            // txbCantidad
            // 
            this.txbCantidad.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txbCantidad.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txbCantidad.Location = new System.Drawing.Point(190, 81);
            this.txbCantidad.MaxLength = 15;
            this.txbCantidad.Name = "txbCantidad";
            this.txbCantidad.ReadOnly = true;
            this.txbCantidad.Size = new System.Drawing.Size(126, 22);
            this.txbCantidad.TabIndex = 22;
            this.txbCantidad.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txbPrecio
            // 
            this.txbPrecio.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txbPrecio.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txbPrecio.Location = new System.Drawing.Point(190, 113);
            this.txbPrecio.MaxLength = 15;
            this.txbPrecio.Name = "txbPrecio";
            this.txbPrecio.ReadOnly = true;
            this.txbPrecio.Size = new System.Drawing.Size(126, 22);
            this.txbPrecio.TabIndex = 24;
            this.txbPrecio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblPrecio
            // 
            this.lblPrecio.AutoSize = true;
            this.lblPrecio.Location = new System.Drawing.Point(74, 116);
            this.lblPrecio.Name = "lblPrecio";
            this.lblPrecio.Size = new System.Drawing.Size(98, 16);
            this.lblPrecio.TabIndex = 23;
            this.lblPrecio.Text = "Precio Unitario:";
            // 
            // txbTotal
            // 
            this.txbTotal.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txbTotal.Location = new System.Drawing.Point(578, 163);
            this.txbTotal.Name = "txbTotal";
            this.txbTotal.ReadOnly = true;
            this.txbTotal.Size = new System.Drawing.Size(100, 22);
            this.txbTotal.TabIndex = 25;
            this.txbTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnCancelar
            // 
            this.btnCancelar.AutoSize = true;
            this.btnCancelar.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelar.Image")));
            this.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancelar.Location = new System.Drawing.Point(385, 13);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(130, 44);
            this.btnCancelar.TabIndex = 26;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancelar.UseVisualStyleBackColor = true;
            // 
            // btnRegistrarVenta
            // 
            this.btnRegistrarVenta.AutoSize = true;
            this.btnRegistrarVenta.Image = ((System.Drawing.Image)(resources.GetObject("btnRegistrarVenta.Image")));
            this.btnRegistrarVenta.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRegistrarVenta.Location = new System.Drawing.Point(169, 13);
            this.btnRegistrarVenta.Name = "btnRegistrarVenta";
            this.btnRegistrarVenta.Size = new System.Drawing.Size(147, 44);
            this.btnRegistrarVenta.TabIndex = 27;
            this.btnRegistrarVenta.Text = "Registrar Venta";
            this.btnRegistrarVenta.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRegistrarVenta.UseVisualStyleBackColor = true;
            // 
            // lblTitulo
            // 
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.Location = new System.Drawing.Point(0, 9);
            this.lblTitulo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(1110, 38);
            this.lblTitulo.TabIndex = 28;
            this.lblTitulo.Text = "Venta";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gbxCliente
            // 
            this.gbxCliente.Controls.Add(this.txtbNombreCliente);
            this.gbxCliente.Controls.Add(this.lblNombreCliente);
            this.gbxCliente.Controls.Add(this.btnBuscarCliente);
            this.gbxCliente.Controls.Add(this.txtCiNit);
            this.gbxCliente.Controls.Add(this.lblCiNit);
            this.gbxCliente.Location = new System.Drawing.Point(0, 50);
            this.gbxCliente.Name = "gbxCliente";
            this.gbxCliente.Size = new System.Drawing.Size(706, 99);
            this.gbxCliente.TabIndex = 29;
            this.gbxCliente.TabStop = false;
            this.gbxCliente.Text = "Cliente";
            // 
            // gbxProducto
            // 
            this.gbxProducto.Controls.Add(this.txbPrecio);
            this.gbxProducto.Controls.Add(this.lblPrecio);
            this.gbxProducto.Controls.Add(this.txbCantidad);
            this.gbxProducto.Controls.Add(this.lblCantidad);
            this.gbxProducto.Controls.Add(this.cmbProducto);
            this.gbxProducto.Controls.Add(this.lblProducto);
            this.gbxProducto.Controls.Add(this.lblFecha);
            this.gbxProducto.Controls.Add(this.btnAgregar);
            this.gbxProducto.Controls.Add(this.dtpFecha);
            this.gbxProducto.Location = new System.Drawing.Point(0, 148);
            this.gbxProducto.Name = "gbxProducto";
            this.gbxProducto.Size = new System.Drawing.Size(706, 156);
            this.gbxProducto.TabIndex = 30;
            this.gbxProducto.TabStop = false;
            this.gbxProducto.Text = "Productos";
            // 
            // gbxListaProductos
            // 
            this.gbxListaProductos.Controls.Add(this.txbTotal);
            this.gbxListaProductos.Controls.Add(this.lblTotal);
            this.gbxListaProductos.Controls.Add(this.dgvDetalleVenta);
            this.gbxListaProductos.Location = new System.Drawing.Point(0, 303);
            this.gbxListaProductos.Name = "gbxListaProductos";
            this.gbxListaProductos.Size = new System.Drawing.Size(706, 192);
            this.gbxListaProductos.TabIndex = 31;
            this.gbxListaProductos.TabStop = false;
            this.gbxListaProductos.Text = "Lista de Productos";
            // 
            // pnlAcciones
            // 
            this.pnlAcciones.Controls.Add(this.btnRegistrarVenta);
            this.pnlAcciones.Controls.Add(this.btnCancelar);
            this.pnlAcciones.Location = new System.Drawing.Point(0, 494);
            this.pnlAcciones.Name = "pnlAcciones";
            this.pnlAcciones.Size = new System.Drawing.Size(699, 72);
            this.pnlAcciones.TabIndex = 32;
            // 
            // FrmVenta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1108, 567);
            this.Controls.Add(this.pnlAcciones);
            this.Controls.Add(this.gbxListaProductos);
            this.Controls.Add(this.gbxProducto);
            this.Controls.Add(this.gbxCliente);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.txbUsuario);
            this.Controls.Add(this.ptbUsuario);
            this.Controls.Add(this.lblUsuario);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmVenta";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "::: Ferreteria - Ventas :::";
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalleVenta)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptbUsuario)).EndInit();
            this.gbxCliente.ResumeLayout(false);
            this.gbxCliente.PerformLayout();
            this.gbxProducto.ResumeLayout(false);
            this.gbxProducto.PerformLayout();
            this.gbxListaProductos.ResumeLayout(false);
            this.gbxListaProductos.PerformLayout();
            this.pnlAcciones.ResumeLayout(false);
            this.pnlAcciones.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpFecha;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.DataGridView dgvDetalleVenta;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblCiNit;
        private System.Windows.Forms.TextBox txtCiNit;
        private System.Windows.Forms.Button btnBuscarCliente;
        private System.Windows.Forms.Label lblNombreCliente;
        private System.Windows.Forms.TextBox txtbNombreCliente;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.PictureBox ptbUsuario;
        private System.Windows.Forms.TextBox txbUsuario;
        private System.Windows.Forms.Label lblProducto;
        private System.Windows.Forms.ComboBox cmbProducto;
        private System.Windows.Forms.Label lblCantidad;
        private System.Windows.Forms.TextBox txbCantidad;
        private System.Windows.Forms.TextBox txbPrecio;
        private System.Windows.Forms.Label lblPrecio;
        private System.Windows.Forms.TextBox txbTotal;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnRegistrarVenta;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.GroupBox gbxCliente;
        private System.Windows.Forms.GroupBox gbxProducto;
        private System.Windows.Forms.GroupBox gbxListaProductos;
        private System.Windows.Forms.Panel pnlAcciones;
    }
}