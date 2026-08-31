using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// omit direct using for Windows Forms and IO; use global:: qualifiers to avoid namespace resolution issues
using System.Xml;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Globalization;

namespace WindowsFormsApp1
{
    public partial class Form1 : global::System.Windows.Forms.Form
    {
        private readonly HttpClient _http = new HttpClient();
        private string _apiBaseUrl;

        public Form1()
        {
            InitializeComponent();
            // Leer URL base de la API desde App.config (clave ApiBaseUrl) o usar el valor por defecto
            _apiBaseUrl = ReadSettingFromConfig("ApiBaseUrl") ?? "http://localhost:5227/api/rpg";
            _http.BaseAddress = new Uri(_apiBaseUrl.Replace("/api/rpg", ""));
        }

        private string ReadSettingFromConfig(string key)
        {
            try
            {
                var configPath = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
                if (!global::System.IO.File.Exists(configPath)) return null;
                var doc = new XmlDocument();
                doc.Load(configPath);
                var node = doc.SelectSingleNode($"/configuration/appSettings/add[@key='{key}']");
                return node?.Attributes?["value"]?.Value;
            }
            catch
            {
                return null;
            }
        }

        private class RankingView
        {
            public int IdRanking { get; set; }
            public string Usuario { get; set; }
            public decimal Puntaje { get; set; }
            public int Ultimonivel { get; set; }
        }

        private List<RankingView> ParseRankingListFromJson(string json)
        {
            var list = new List<RankingView>();
            if (string.IsNullOrWhiteSpace(json)) return list;
            try
            {
                // match objects inside array
                var objMatches = Regex.Matches(json, "\\{(.*?)\\}", RegexOptions.Singleline);
                foreach (Match m in objMatches)
                {
                    var content = m.Groups[1].Value;
                    var idMatch = Regex.Match(content, "\"(?:IdRanking|id_ranking|id)\"\\s*:\\s*(\\d+)", RegexOptions.IgnoreCase);
                    var usuarioMatch = Regex.Match(content, "\"(?:Usuario|usuario)\"\\s*:\\s*\"(.*?)\"", RegexOptions.Singleline);
                    var puntajeMatch = Regex.Match(content, "\"(?:Puntaje|puntaje)\"\\s*:\\s*([0-9.+\\-eE]+)", RegexOptions.IgnoreCase);
                    var nivelMatch = Regex.Match(content, "\"(?:Ultimonivel|ultimonivel|ultimoNivel)\"\\s*:\\s*(\\d+)", RegexOptions.IgnoreCase);
                    var item = new RankingView();
                    if (idMatch.Success && int.TryParse(idMatch.Groups[1].Value, out var idv)) item.IdRanking = idv;
                    if (usuarioMatch.Success) item.Usuario = usuarioMatch.Groups[1].Value.Replace("\\\"", "\"");
                    if (puntajeMatch.Success && decimal.TryParse(puntajeMatch.Groups[1].Value, out var pv)) item.Puntaje = pv;
                    if (nivelMatch.Success && int.TryParse(nivelMatch.Groups[1].Value, out var nv)) item.Ultimonivel = nv;
                    list.Add(item);
                }
            }
            catch
            {
                // ignore parse errors
            }
            return list;
        }

        private async void BtnLoad_Click(object sender, EventArgs e)
        {
            await LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            try
            {
                var resp = await _http.GetAsync("/api/rpg");
                if (!resp.IsSuccessStatusCode)
                {
                    global::System.Windows.Forms.MessageBox.Show($"Error al obtener datos: {resp.StatusCode}", "Error", global::System.Windows.Forms.MessageBoxButtons.OK, global::System.Windows.Forms.MessageBoxIcon.Error);
                    return;
                }
                var json = await resp.Content.ReadAsStringAsync();
                var list = ParseRankingListFromJson(json);
                dataGridView1.DataSource = list;
                // Formatear columna Puntaje para mostrar '.' como separador decimal
                try
                {
                    foreach (global::System.Windows.Forms.DataGridViewColumn c in dataGridView1.Columns)
                    {
                        if (string.Equals(c.DataPropertyName, "Puntaje", StringComparison.OrdinalIgnoreCase) || string.Equals(c.Name, "Puntaje", StringComparison.OrdinalIgnoreCase) || string.Equals(c.HeaderText, "Puntaje", StringComparison.OrdinalIgnoreCase))
                        {
                            c.DefaultCellStyle.Format = "F2";
                            c.DefaultCellStyle.FormatProvider = CultureInfo.InvariantCulture;
                        }
                    }
                }
                catch
                {
                    // ignore styling errors
                }
            }
            catch (Exception ex)
            {
                    global::System.Windows.Forms.MessageBox.Show($"Error al cargar datos: {ex.Message}", "Error", global::System.Windows.Forms.MessageBoxButtons.OK, global::System.Windows.Forms.MessageBoxIcon.Error);
            }
        }

        private async void BtnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                decimal pVal;
                int nVal;
                if (!decimal.TryParse(txtPuntaje.Text, NumberStyles.Number, CultureInfo.InvariantCulture, out pVal)) pVal = 0m;
                if (!int.TryParse(txtUltimoNivel.Text, out var n)) nVal = 0; else nVal = n;
                var usuarioEsc = (txtUsuario.Text ?? string.Empty).Replace("\\", "\\\\").Replace("\"", "\\\"");
                var json = "{" + $"\"Usuario\":\"{usuarioEsc}\",\"Puntaje\":{pVal.ToString(CultureInfo.InvariantCulture)},\"Ultimonivel\":{nVal}" + "}";
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var resp = await _http.PostAsync("/api/rpg", content);
                if (resp.IsSuccessStatusCode)
                {
                    var createdJson = await resp.Content.ReadAsStringAsync();
                    var createdList = ParseRankingListFromJson("[" + createdJson + "]");
                    var created = createdList?.FirstOrDefault();
                    if (created != null) txtId.Text = created.IdRanking.ToString();
                    await LoadDataAsync();
                }
                else
                {
                    global::System.Windows.Forms.MessageBox.Show($"Error al crear: {resp.StatusCode}", "Error", global::System.Windows.Forms.MessageBoxButtons.OK, global::System.Windows.Forms.MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                global::System.Windows.Forms.MessageBox.Show($"Error al crear: {ex.Message}", "Error", global::System.Windows.Forms.MessageBoxButtons.OK, global::System.Windows.Forms.MessageBoxIcon.Error);
            }
        }

        private async void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtId.Text)) { global::System.Windows.Forms.MessageBox.Show("Seleccione un registro para actualizar.", "Atención", global::System.Windows.Forms.MessageBoxButtons.OK, global::System.Windows.Forms.MessageBoxIcon.Information); return; }
            try
            {
                var id = int.Parse(txtId.Text);
                decimal pVal;
                int nVal;
                if (!decimal.TryParse(txtPuntaje.Text, NumberStyles.Number, CultureInfo.InvariantCulture, out pVal)) pVal = 0m;
                if (!int.TryParse(txtUltimoNivel.Text, out var n)) nVal = 0; else nVal = n;
                var usuarioEsc = (txtUsuario.Text ?? string.Empty).Replace("\\", "\\\\").Replace("\"", "\\\"");
                var json = "{" + $"\"Usuario\":\"{usuarioEsc}\",\"Puntaje\":{pVal.ToString(CultureInfo.InvariantCulture)},\"Ultimonivel\":{nVal}" + "}";
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var resp = await _http.PutAsync($"/api/rpg/{id}", content);
                if (resp.IsSuccessStatusCode)
                {
                    await LoadDataAsync();
                }
                else if (resp.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    global::System.Windows.Forms.MessageBox.Show("Registro no encontrado.", "Info", global::System.Windows.Forms.MessageBoxButtons.OK, global::System.Windows.Forms.MessageBoxIcon.Information);
                }
                else
                {
                    global::System.Windows.Forms.MessageBox.Show($"Error al actualizar: {resp.StatusCode}", "Error", global::System.Windows.Forms.MessageBoxButtons.OK, global::System.Windows.Forms.MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                global::System.Windows.Forms.MessageBox.Show($"Error al actualizar: {ex.Message}", "Error", global::System.Windows.Forms.MessageBoxButtons.OK, global::System.Windows.Forms.MessageBoxIcon.Error);
            }
        }

        private async void BtnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtId.Text)) { global::System.Windows.Forms.MessageBox.Show("Seleccione un registro para borrar.", "Atención", global::System.Windows.Forms.MessageBoxButtons.OK, global::System.Windows.Forms.MessageBoxIcon.Information); return; }
            var confirm = global::System.Windows.Forms.MessageBox.Show("¿Seguro que desea borrar el registro seleccionado?", "Confirmar", global::System.Windows.Forms.MessageBoxButtons.YesNo, global::System.Windows.Forms.MessageBoxIcon.Question);
            if (confirm != global::System.Windows.Forms.DialogResult.Yes) return;
            try
            {
                var id = int.Parse(txtId.Text);
                var resp = await _http.DeleteAsync($"/api/rpg/{id}");
                if (resp.IsSuccessStatusCode)
                {
                    await LoadDataAsync();
                    ClearForm();
                }
                else if (resp.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    global::System.Windows.Forms.MessageBox.Show("Registro no encontrado.", "Info", global::System.Windows.Forms.MessageBoxButtons.OK, global::System.Windows.Forms.MessageBoxIcon.Information);
                }
                else
                {
                    global::System.Windows.Forms.MessageBox.Show($"Error al borrar: {resp.StatusCode}", "Error", global::System.Windows.Forms.MessageBoxButtons.OK, global::System.Windows.Forms.MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                global::System.Windows.Forms.MessageBox.Show($"Error al borrar: {ex.Message}", "Error", global::System.Windows.Forms.MessageBoxButtons.OK, global::System.Windows.Forms.MessageBoxIcon.Error);
            }
        }

        private void DataGridView1_CellClick(object sender, global::System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var item = dataGridView1.Rows[e.RowIndex].DataBoundItem as RankingView;
            if (item == null) return;
            txtId.Text = item.IdRanking.ToString();
            txtUsuario.Text = item.Usuario;
            // Mostrar puntaje con '.' como separador decimal
            txtPuntaje.Text = item.Puntaje.ToString(CultureInfo.InvariantCulture);
            txtUltimoNivel.Text = item.Ultimonivel.ToString();
        }

        private void ClearForm()
        {
            txtId.Text = "";
            txtUsuario.Text = "";
            txtPuntaje.Text = "";
            txtUltimoNivel.Text = "";
        }
    }
}
