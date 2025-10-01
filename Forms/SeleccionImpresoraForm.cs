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

                if (soloZebraCheckBox.Checked)
                {
                    impresoras = impresoras.Where(p =>
                        p.ToUpper().Contains("ZEBRA") ||
                        p.ToUpper().Contains("ZT") ||
                        p.ToUpper().Contains("ZD")).ToList();
                }

                impresorasListBox.Items.Clear();

                var defaultPrinter = ConfigurationService.DefaultPrinter;

                foreach (var impresora in impresoras)
                {
                    var displayText = impresora;

                    if (!string.IsNullOrEmpty(defaultPrinter) &&
                        impresora.Equals(defaultPrinter, StringComparison.OrdinalIgnoreCase))
                    {
                        displayText += " (Predeterminada)";
                    }

                    if (impresora.ToUpper().Contains("ZEBRA"))
                    {
                        displayText += " [ZPL]";
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
            var zebraCount = impresoras.Count(p => p.ToUpper().Contains("ZEBRA"));

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

        private async void MostrarInfoImpresora()
        {
            if (impresorasListBox.SelectedIndex >= 0)
            {
                var nombreImpresora = ObtenerNombreImpresoraSeleccionada();

                try
                {
                    var estado = PrinterService.CheckPrinterStatus(nombreImpresora);
                    var esZebra = nombreImpresora.ToUpper().Contains("ZEBRA");
                    var esDefault = nombreImpresora.Equals(ConfigurationService.DefaultPrinter,
                        StringComparison.OrdinalIgnoreCase);

                    var infoText = $"Impresora: {nombreImpresora}\n";
                    infoText += $"Estado: {(estado ? "Conectada" : "Desconectada")}\n";
                    infoText += $"Tipo: {(esZebra ? "Compatible ZPL" : "Estándar")}\n";

                    if (esDefault)
                        infoText += "\nImpresora predeterminada del sistema";

                    lblInfoImpresora.Text = infoText;
                }
                catch
                {
                    lblInfoImpresora.Text = $"Impresora: {nombreImpresora}\nInformación no disponible";
                }
            }
            else
            {
                lblInfoImpresora.Text = "Seleccione una impresora\npara ver información";
            }
        }

        private string ObtenerNombreImpresoraSeleccionada()
        {
            if (impresorasListBox.SelectedIndex >= 0)
            {
                var textoCompleto = impresorasListBox.SelectedItem.ToString();
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
                this.impresoras = PrinterService.GetAvailablePrinters();
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

                    if (PrinterService.PrintTest(nombreImpresora))
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

        public static string SeleccionarImpresora(IWin32Window owner = null)
        {
            try
            {
                var impresoras = PrinterService.GetAvailablePrinters();
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