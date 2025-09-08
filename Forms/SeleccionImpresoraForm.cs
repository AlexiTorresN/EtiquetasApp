using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using EtiquetasApp.Services;

namespace EtiquetasApp.Forms
{
    public partial class SeleccionImpresoraForm : Form
    {
        private List<string> impresoras;
        public string ImpresoraSeleccionada { get; private set; }

        public SeleccionImpresoraForm(List<string> listaImpresoras)
        {
            InitializeComponent();
            this.impresoras = listaImpresoras ?? new List<string>();
            InitializeForm();
        }

        private void InitializeForm()
        {
            CargarImpresoras();
            ConfigurarEventos();
            ConfigurarFormulario();
        }

        private void ConfigurarFormulario()
        {
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ShowInTaskbar = false;
        }

        private void ConfigurarEventos()
        {
            impresorasListBox.SelectedIndexChanged += ImpresorasListBox_SelectedIndexChanged;
            impresorasListBox.DoubleClick += ImpresorasListBox_DoubleClick;
            btnSeleccionar.Click += BtnSeleccionar_Click;
            btnCancelar.Click += BtnCancelar_Click;
            btnActualizar.Click += BtnActualizar_Click;
            btnPrueba.Click += BtnPrueba_Click;
            soloZebraCheckBox.CheckedChanged += SoloZebraCheckBox_CheckedChanged;
        }

        private void CargarImpresoras()
        {
            try
            {
                var impresoras = this.impresoras;

                // Filtrar solo Zebra si está marcado
                if (soloZebraCheckBox.Checked)
                {
                    impresoras = PrinterService.GetZebraPrinters();
                }

                impresorasListBox.Items.Clear();
                foreach (var impresora in impresoras)
                {
                    var info = PrinterService.GetPrinterInfo(impresora);
                    var displayText = impresora;

                    if (info != null)
                    {
                        if (info.IsDefault)
                            displayText += " (Predeterminada)";

                        if (info.SupportsZPL)
                            displayText += " [ZPL]";

                        if (!info.IsOnline)
                            displayText += " - Desconectada";
                    }

                    impresorasListBox.Items.Add(displayText);
                }

                ActualizarEstado();
            }
            catch (Exception ex)
            {
                MostrarError($"Error cargando impresoras: {ex.Message}");
            }
        }

        private void ActualizarEstado()
        {
            var totalImpresoras = impresorasListBox.Items.Count;
            var zebraCount = PrinterService.GetZebraPrinters().Count;

            statusLabel.Text = $"Total: {totalImpresoras} impresoras | Zebra/ZPL: {zebraCount}";

            btnSeleccionar.Enabled = impresorasListBox.SelectedIndex >= 0;
            btnPrueba.Enabled = impresorasListBox.SelectedIndex >= 0;

            if (totalImpresoras == 0)
            {
                lblInstrucciones.Text = "No se encontraron impresoras instaladas";
                lblInstrucciones.ForeColor = Color.Red;
            }
            else
            {
                lblInstrucciones.Text = "Seleccione una impresora de la lista:";
                lblInstrucciones.ForeColor = SystemColors.ControlText;
            }
        }

        private void ImpresorasListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActualizarEstado();
            MostrarInfoImpresora();
        }

        private void MostrarInfoImpresora()
        {
            if (impresorasListBox.SelectedIndex >= 0)
            {
                var nombreImpresora = ObtenerNombreImpresoraSeleccionada();
                var info = PrinterService.GetPrinterInfo(nombreImpresora);

                if (info != null)
                {
                    var infoText = $"Impresora: {info.Name}\n";
                    infoText += $"Estado: {info.Status}\n";
                    infoText += $"Tipo: {(info.SupportsZPL ? "Compatible ZPL" : "Estándar")}\n";
                    infoText += $"Conexión: {(info.IsOnline ? "Conectada" : "Desconectada")}\n";

                    if (info.IsDefault)
                        infoText += "Es la impresora predeterminada del sistema\n";

                    lblInfoImpresora.Text = infoText;
                }
                else
                {
                    lblInfoImpresora.Text = "Información no disponible";
                }
            }
            else
            {
                lblInfoImpresora.Text = "";
            }
        }

        private string ObtenerNombreImpresoraSeleccionada()
        {
            if (impresorasListBox.SelectedIndex >= 0)
            {
                var textoCompleto = impresorasListBox.SelectedItem.ToString();
                // Extraer solo el nombre de la impresora (antes de cualquier texto adicional)
                var partes = textoCompleto.Split(new[] { " (", " [", " -" }, StringSplitOptions.None);
                return partes[0];
            }
            return null;
        }

        private void ImpresorasListBox_DoubleClick(object sender, EventArgs e)
        {
            if (impresorasListBox.SelectedIndex >= 0)
            {
                BtnSeleccionar_Click(sender, e);
            }
        }

        private void BtnSeleccionar_Click(object sender, EventArgs e)
        {
            if (impresorasListBox.SelectedIndex >= 0)
            {
                ImpresoraSeleccionada = ObtenerNombreImpresoraSeleccionada();

                // Guardar como impresora por defecto si está marcado
                if (establecerPorDefectoCheckBox.Checked)
                {
                    try
                    {
                        ConfigurationService.DefaultPrinter = ImpresoraSeleccionada;
                    }
                    catch (Exception ex)
                    {
                        MostrarError($"Error guardando impresora por defecto: {ex.Message}");
                    }
                }

                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MostrarError("Debe seleccionar una impresora");
            }
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void BtnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                // Recargar lista de impresoras del sistema
                this.impresoras = PrinterService.GetInstalledPrinters();
                CargarImpresoras();
                MostrarMensaje("Lista de impresoras actualizada", false);
            }
            catch (Exception ex)
            {
                MostrarError($"Error actualizando lista: {ex.Message}");
            }
        }

        private void BtnPrueba_Click(object sender, EventArgs e)
        {
            if (impresorasListBox.SelectedIndex >= 0)
            {
                var nombreImpresora = ObtenerNombreImpresoraSeleccionada();

                try
                {
                    btnPrueba.Enabled = false;
                    btnPrueba.Text = "Enviando...";
                    Application.DoEvents();

                    if (PrinterService.TestPrinter(nombreImpresora))
                    {
                        MostrarMensaje("Prueba de impresión enviada correctamente", false);
                    }
                    else
                    {
                        MostrarError("Error en la prueba de impresión");
                    }
                }
                catch (Exception ex)
                {
                    MostrarError($"Error en prueba: {ex.Message}");
                }
                finally
                {
                    btnPrueba.Enabled = true;
                    btnPrueba.Text = "Prueba";
                }
            }
        }

        private void SoloZebraCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CargarImpresoras();
        }

        private void MostrarError(string mensaje)
        {
            MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void MostrarMensaje(string mensaje, bool esAdvertencia)
        {
            var icon = esAdvertencia ? MessageBoxIcon.Warning : MessageBoxIcon.Information;
            MessageBox.Show(mensaje, "Información", MessageBoxButtons.OK, icon);
        }

        // Método estático para uso rápido
        public static string SeleccionarImpresora(IWin32Window owner = null)
        {
            try
            {
                var impresoras = PrinterService.GetInstalledPrinters();
                using (var form = new SeleccionImpresoraForm(impresoras))
                {
                    var result = owner != null ? form.ShowDialog(owner) : form.ShowDialog();
                    return result == DialogResult.OK ? form.ImpresoraSeleccionada : null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error seleccionando impresora: {ex.Message}",
                               "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        // Método para preseleccionar una impresora
        public void PreseleccionarImpresora(string nombreImpresora)
        {
            if (string.IsNullOrEmpty(nombreImpresora)) return;

            for (int i = 0; i < impresorasListBox.Items.Count; i++)
            {
                var item = impresorasListBox.Items[i].ToString();
                if (item.StartsWith(nombreImpresora, StringComparison.OrdinalIgnoreCase))
                {
                    impresorasListBox.SelectedIndex = i;
                    break;
                }
            }
        }

        // Propiedades para configuración externa
        public bool MostrarSoloZebra
        {
            get => soloZebraCheckBox.Checked;
            set => soloZebraCheckBox.Checked = value;
        }

        public bool EstablecerPorDefecto
        {
            get => establecerPorDefectoCheckBox.Checked;
            set => establecerPorDefectoCheckBox.Checked = value;
        }
    }
}