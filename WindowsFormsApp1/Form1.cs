using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.IO;
using MySql.Data.MySqlClient;
// ... existing usings

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        // Controles declarados manualmente (el diseñador los crea en InitializeComponent)
        private System.Windows.Forms.DataGridView dgvPersonas;
        private System.Windows.Forms.Label lblNombres;
        private System.Windows.Forms.TextBox txtNombres;
        private System.Windows.Forms.Label lblApellido;
        private System.Windows.Forms.TextBox txtApellido;
        private System.Windows.Forms.Label lblEdad;
        private System.Windows.Forms.TextBox txtEdad;
        private System.Windows.Forms.Label lblEstado;
        private System.Windows.Forms.TextBox txtEstado;
        private System.Windows.Forms.Button btnListar;
        private System.Windows.Forms.Button btnCrear;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnLimpiar;

        private int selectedId = -1;
        private string _connectionString;

        public Form1()
        {
            InitializeComponent();
            _connectionString = GetConnectionStringFromConfig();
            if (string.IsNullOrWhiteSpace(_connectionString))
            {
                MessageBox.Show("No se encontró la cadena de conexión 'DefaultConnection' en el archivo de configuración.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            LoadData();
        }

        private string GetConnectionStringFromConfig()
        {
            try
            {
                var configPath = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
                if (!File.Exists(configPath)) return null;
                var doc = XDocument.Load(configPath);
                var add = doc.Descendants("connectionStrings").Descendants("add").FirstOrDefault(x => (string)x.Attribute("name") == "DefaultConnection");
                return add?.Attribute("connectionString")?.Value;
            }
            catch
            {
                return null;
            }
        }

        private void LoadData()
        {
            if (string.IsNullOrWhiteSpace(_connectionString)) return;

            var dt = new DataTable();
            try
            {
                using (var conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    using (var cmd = new MySqlCommand("SELECT id_persona AS Id, nombres AS Nombres, apellido AS Apellido, edad AS Edad, estado AS Estado FROM persona", conn))
                    using (var adapter = new MySqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }
                dgvPersonas.DataSource = dt;
                ClearFields(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearFields(bool clearSelection = true)
        {
            txtNombres.Text = string.Empty;
            txtApellido.Text = string.Empty;
            txtEdad.Text = string.Empty;
            txtEstado.Text = string.Empty;
            selectedId = -1;
            if (clearSelection && dgvPersonas.Rows.Count > 0)
            {
                dgvPersonas.ClearSelection();
            }
        }

        private void btnListar_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_connectionString)) return;
            if (string.IsNullOrWhiteSpace(txtNombres.Text)) { MessageBox.Show("Nombres requeridos"); return; }
            try
            {
                using (var conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    using (var cmd = new MySqlCommand("INSERT INTO persona (nombres, apellido, edad, estado) VALUES (@nombres,@apellido,@edad,@estado)", conn))
                    {
                        cmd.Parameters.AddWithValue("@nombres", txtNombres.Text);
                        cmd.Parameters.AddWithValue("@apellido", txtApellido.Text);
                        cmd.Parameters.AddWithValue("@edad", int.TryParse(txtEdad.Text, out var edadVal) ? edadVal : 0);
                        cmd.Parameters.AddWithValue("@estado", txtEstado.Text);
                        cmd.ExecuteNonQuery();
                    }
                }
                LoadData();
                MessageBox.Show("Registro creado correctamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al crear: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (selectedId <= 0) { MessageBox.Show("Seleccione un registro para editar."); return; }
            try
            {
                using (var conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    using (var cmd = new MySqlCommand("UPDATE persona SET nombres=@nombres, apellido=@apellido, edad=@edad, estado=@estado WHERE id_persona=@id", conn))
                    {
                        cmd.Parameters.AddWithValue("@nombres", txtNombres.Text);
                        cmd.Parameters.AddWithValue("@apellido", txtApellido.Text);
                        cmd.Parameters.AddWithValue("@edad", int.TryParse(txtEdad.Text, out var edadVal) ? edadVal : 0);
                        cmd.Parameters.AddWithValue("@estado", txtEstado.Text);
                        cmd.Parameters.AddWithValue("@id", selectedId);
                        cmd.ExecuteNonQuery();
                    }
                }
                LoadData();
                MessageBox.Show("Registro actualizado.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (selectedId <= 0) { MessageBox.Show("Seleccione un registro para eliminar."); return; }
            var conf = MessageBox.Show("¿Desea eliminar el registro seleccionado?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (conf != DialogResult.Yes) return;
            try
            {
                using (var conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    using (var cmd = new MySqlCommand("DELETE FROM persona WHERE id_persona=@id", conn))
                    {
                        cmd.Parameters.AddWithValue("@id", selectedId);
                        cmd.ExecuteNonQuery();
                    }
                }
                LoadData();
                MessageBox.Show("Registro eliminado.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvPersonas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = dgvPersonas.Rows[e.RowIndex];
            if (row.Cells[0].Value == null) return;
            selectedId = Convert.ToInt32(row.Cells[0].Value);
            txtNombres.Text = row.Cells[1].Value?.ToString();
            txtApellido.Text = row.Cells[2].Value?.ToString();
            txtEdad.Text = row.Cells[3].Value?.ToString();
            txtEstado.Text = row.Cells[4].Value?.ToString();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            ClearFields();
        }
    }
}
