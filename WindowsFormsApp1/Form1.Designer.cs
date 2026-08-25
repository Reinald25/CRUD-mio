namespace WindowsFormsApp1
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 520);
            this.Text = "CRUD Alumnos";

            // Controles
            this.dgvPersonas = new System.Windows.Forms.DataGridView();
            this.lblNombres = new System.Windows.Forms.Label();
            this.txtNombres = new System.Windows.Forms.TextBox();
            this.lblApellido = new System.Windows.Forms.Label();
            this.txtApellido = new System.Windows.Forms.TextBox();
            this.lblEdad = new System.Windows.Forms.Label();
            this.txtEdad = new System.Windows.Forms.TextBox();
            this.lblEstado = new System.Windows.Forms.Label();
            this.txtEstado = new System.Windows.Forms.TextBox();
            this.btnListar = new System.Windows.Forms.Button();
            this.btnCrear = new System.Windows.Forms.Button();
            this.btnEditar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnLimpiar = new System.Windows.Forms.Button();

            // DataGridView
            this.dgvPersonas.Location = new System.Drawing.Point(12, 12);
            this.dgvPersonas.Size = new System.Drawing.Size(660, 480);
            this.dgvPersonas.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dgvPersonas.ReadOnly = true;
            this.dgvPersonas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPersonas.MultiSelect = false;
            this.dgvPersonas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPersonas.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPersonas_CellClick);

            // Labels y TextBoxes
            int xForm = 690;
            int lblWidth = 60;
            int txtWidth = 180;
            int top = 20;

            this.lblNombres.Location = new System.Drawing.Point(xForm, top);
            this.lblNombres.Size = new System.Drawing.Size(lblWidth, 23);
            this.lblNombres.Text = "Nombres:";
            this.txtNombres.Location = new System.Drawing.Point(xForm + lblWidth, top);
            this.txtNombres.Size = new System.Drawing.Size(txtWidth, 23);

            top += 40;
            this.lblApellido.Location = new System.Drawing.Point(xForm, top);
            this.lblApellido.Size = new System.Drawing.Size(lblWidth, 23);
            this.lblApellido.Text = "Apellido:";
            this.txtApellido.Location = new System.Drawing.Point(xForm + lblWidth, top);
            this.txtApellido.Size = new System.Drawing.Size(txtWidth, 23);

            top += 40;
            this.lblEdad.Location = new System.Drawing.Point(xForm, top);
            this.lblEdad.Size = new System.Drawing.Size(lblWidth, 23);
            this.lblEdad.Text = "Edad:";
            this.txtEdad.Location = new System.Drawing.Point(xForm + lblWidth, top);
            this.txtEdad.Size = new System.Drawing.Size(80, 23);

            top += 40;
            this.lblEstado.Location = new System.Drawing.Point(xForm, top);
            this.lblEstado.Size = new System.Drawing.Size(lblWidth, 23);
            this.lblEstado.Text = "Estado:";
            this.txtEstado.Location = new System.Drawing.Point(xForm + lblWidth, top);
            this.txtEstado.Size = new System.Drawing.Size(txtWidth, 23);

            // Botones
            top += 60;
            int btnWidth = 120;
            this.btnListar.Location = new System.Drawing.Point(xForm, top);
            this.btnListar.Size = new System.Drawing.Size(btnWidth, 35);
            this.btnListar.Text = "Listar";
            this.btnListar.Click += new System.EventHandler(this.btnListar_Click);

            this.btnCrear.Location = new System.Drawing.Point(xForm + btnWidth + 10, top);
            this.btnCrear.Size = new System.Drawing.Size(btnWidth, 35);
            this.btnCrear.Text = "Crear";
            this.btnCrear.Click += new System.EventHandler(this.btnCrear_Click);

            top += 50;
            this.btnEditar.Location = new System.Drawing.Point(xForm, top);
            this.btnEditar.Size = new System.Drawing.Size(btnWidth, 35);
            this.btnEditar.Text = "Editar";
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);

            this.btnEliminar.Location = new System.Drawing.Point(xForm + btnWidth + 10, top);
            this.btnEliminar.Size = new System.Drawing.Size(btnWidth, 35);
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);

            top += 50;
            this.btnLimpiar.Location = new System.Drawing.Point(xForm, top);
            this.btnLimpiar.Size = new System.Drawing.Size(btnWidth, 35);
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);

            // Añadir controles al formulario
            this.Controls.Add(this.dgvPersonas);
            this.Controls.Add(this.lblNombres);
            this.Controls.Add(this.txtNombres);
            this.Controls.Add(this.lblApellido);
            this.Controls.Add(this.txtApellido);
            this.Controls.Add(this.lblEdad);
            this.Controls.Add(this.txtEdad);
            this.Controls.Add(this.lblEstado);
            this.Controls.Add(this.txtEstado);
            this.Controls.Add(this.btnListar);
            this.Controls.Add(this.btnCrear);
            this.Controls.Add(this.btnEditar);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.btnLimpiar);
        }

        #endregion
    }
}

