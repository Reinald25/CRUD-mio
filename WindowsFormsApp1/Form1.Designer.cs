namespace WindowsFormsApp1
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.TextBox txtPuntaje;
        private System.Windows.Forms.TextBox txtUltimoNivel;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.Label lblPuntaje;
        private System.Windows.Forms.Label lblUltimoNivel;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.Label lblId;

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
            this.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new global::System.Drawing.Size(900, 520);
            this.Text = "RPG Rankings - CRUD";

            // Controls
            this.dataGridView1 = new global::System.Windows.Forms.DataGridView();
            this.btnLoad = new global::System.Windows.Forms.Button();
            this.btnCreate = new global::System.Windows.Forms.Button();
            this.btnUpdate = new global::System.Windows.Forms.Button();
            this.btnDelete = new global::System.Windows.Forms.Button();
            this.txtUsuario = new global::System.Windows.Forms.TextBox();
            this.txtPuntaje = new global::System.Windows.Forms.TextBox();
            this.txtUltimoNivel = new global::System.Windows.Forms.TextBox();
            this.lblUsuario = new global::System.Windows.Forms.Label();
            this.lblPuntaje = new global::System.Windows.Forms.Label();
            this.lblUltimoNivel = new global::System.Windows.Forms.Label();
            this.txtId = new global::System.Windows.Forms.TextBox();
            this.lblId = new global::System.Windows.Forms.Label();

            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();

            // dataGridView1
            this.dataGridView1.Location = new global::System.Drawing.Point(12, 12);
            this.dataGridView1.Size = new global::System.Drawing.Size(660, 480);
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.MultiSelect = false;

            // Labels and TextBoxes
            this.lblId.AutoSize = true;
            this.lblId.Location = new System.Drawing.Point(690, 20);
            this.lblId.Text = "Id:";
            this.txtId.Location = new global::System.Drawing.Point(760, 16);
            this.txtId.Width = 120;
            this.txtId.ReadOnly = true;

            this.lblUsuario.AutoSize = true;
            this.lblUsuario.Location = new System.Drawing.Point(690, 60);
            this.lblUsuario.Text = "Usuario:";
            this.txtUsuario.Location = new global::System.Drawing.Point(760, 56);
            this.txtUsuario.Width = 120;

            this.lblPuntaje.AutoSize = true;
            this.lblPuntaje.Location = new System.Drawing.Point(690, 100);
            this.lblPuntaje.Text = "Puntaje:";
            this.txtPuntaje.Location = new global::System.Drawing.Point(760, 96);
            this.txtPuntaje.Width = 120;

            this.lblUltimoNivel.AutoSize = true;
            this.lblUltimoNivel.Location = new System.Drawing.Point(690, 140);
            this.lblUltimoNivel.Text = "Ultimo nivel:";
            this.txtUltimoNivel.Location = new global::System.Drawing.Point(760, 136);
            this.txtUltimoNivel.Width = 120;

            // Buttons
            this.btnLoad.Location = new global::System.Drawing.Point(690, 190);
            this.btnLoad.Size = new global::System.Drawing.Size(190, 30);
            this.btnLoad.Text = "Cargar";

            this.btnCreate.Location = new global::System.Drawing.Point(690, 230);
            this.btnCreate.Size = new global::System.Drawing.Size(190, 30);
            this.btnCreate.Text = "Crear";

            this.btnUpdate.Location = new global::System.Drawing.Point(690, 270);
            this.btnUpdate.Size = new global::System.Drawing.Size(190, 30);
            this.btnUpdate.Text = "Actualizar";

            this.btnDelete.Location = new global::System.Drawing.Point(690, 310);
            this.btnDelete.Size = new global::System.Drawing.Size(190, 30);
            this.btnDelete.Text = "Borrar";

            // Add controls
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.lblId);
            this.Controls.Add(this.txtId);
            this.Controls.Add(this.lblUsuario);
            this.Controls.Add(this.txtUsuario);
            this.Controls.Add(this.lblPuntaje);
            this.Controls.Add(this.txtPuntaje);
            this.Controls.Add(this.lblUltimoNivel);
            this.Controls.Add(this.txtUltimoNivel);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnDelete);

            // Event wiring
            this.btnLoad.Click += new System.EventHandler(this.BtnLoad_Click);
            this.btnCreate.Click += new System.EventHandler(this.BtnCreate_Click);
            this.btnUpdate.Click += new System.EventHandler(this.BtnUpdate_Click);
            this.btnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            this.dataGridView1.CellClick += new global::System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellClick);

            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
        }

        #endregion
    }
}

