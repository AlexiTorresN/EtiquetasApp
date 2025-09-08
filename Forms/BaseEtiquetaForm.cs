using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using EtiquetasApp.Models;
using EtiquetasApp.Services;

namespace EtiquetasApp.Forms
{
    public partial class BaseEtiquetaForm : Form
    {
        protected List<SolicitudEtiqueta> solicitudes;
        protected List<string> impresoras;
        protected string impresoraSeleccionada;
        protected TipoEtiqueta tipoEtiqueta;

        public BaseEtiquetaForm()
        {
            InitializeComponent();
            InitializeBaseForm();
        }

        protected virtual void InitializeBaseForm()
        {
            solicitudes = new List<SolicitudEtiqueta>();
            impresoras = new List<string>();
            CargarImpresoras();
            CargarSolicitudes();
            ConfigurarEventos();
        }

        protected virtual void ConfigurarEventos()
        {
            // Eventos para controles de posiciones
            pos1TextBox.TextChanged += PosicionTextBox_TextChanged;
            pos2TextBox.TextChanged += PosicionTextBox_TextChanged;
            pos3TextBox.TextChanged += PosicionTextBox_TextChanged;
            pos4TextBox.TextChanged += PosicionTextBox_TextChanged;
            pos5TextBox.TextChanged += PosicionTextBox_TextChanged;

            // Eventos para botones de ajuste de posiciones
            btnAjustarIzq.Click += BtnAjustarIzq_Click;
            btnAjustarDer.Click += BtnAjustarDer_Click;

            // Eventos de grilla
            solicitudesGrid.SelectionChanged += SolicitudesGrid_SelectionChanged;
            solicitudesGrid.CellDoubleClick += SolicitudesGrid_CellDoubleClick;

            // Eventos de impresión
            btnImprimir.Click += BtnImprimir_Click;
            btnPrueba.Click += BtnPrueba_Click;
        }

        protected virtual void CargarImpresoras()
        {
            try
            {
                impresoras = PrinterService.GetInstalledPrinters();
                papelComboBox.Items.Clear();
                papelComboBox.Items.AddRange(new[] { "EMPACK", "SOLUCORP", "Térmico", "Sintético" });
                papelComboBox.SelectedIndex = 0;

                // Cargar impresora por defecto
                var defaultPrinter = ConfigurationService.DefaultPrinter;
                if (!string.IsNullOrEmpty(defaultPrinter) && impresoras.Contains(defaultPrinter))
                {
                    impresoraSeleccionada = defaultPrinter;
                }
            }
            catch (Exception ex)
            {
                MostrarError($"Error cargando impresoras: {ex.Message}");
            }
        }

        protected virtual void CargarSolicitudes()
        {
            try
            {
                solicitudes = DatabaseService.GetSolicitudesEtiquetas()
                    .Where(s => string.IsNullOrEmpty(s.TipoEtiqueta) || s.TipoEtiqueta == tipoEtiqueta.ToCodeString())
                    .Where(s => s.CantidadPendiente > 0)
                    .OrderBy(s => s.FechaRequerida)
                    .ToList();

                CargarGrillaSolicitudes();
            }
            catch (Exception ex)
            {
                MostrarError($"Error cargando solicitudes: {ex.Message}");
            }
        }

        protected virtual void CargarGrillaSolicitudes()
        {
            try
            {
                solicitudesGrid.Rows.Clear();

                foreach (var solicitud in solicitudes)
                {
                    var row = new DataGridViewRow();
                    row.CreateCells(solicitudesGrid);

                    row.Cells[0].Value = solicitud.IdSolicitud;
                    row.Cells[1].Value = solicitud.OrdenFab;
                    row.Cells[2].Value = solicitud.Descripcion;
                    row.Cells[3].Value = solicitud.CantidadPedida;
                    row.Cells[4].Value = solicitud.Color;
                    row.Cells[5].Value = solicitud.Observaciones;
                    row.Cells[6].Value = solicitud.FechaRequerida.ToString("dd/MM/yyyy");

                    // Colorear filas según urgencia
                    if (solicitud.EsUrgente)
                        row.DefaultCellStyle.BackColor = Color.LightCoral;
                    else if (solicitud.DiasParaVencimiento <= 3)
                        row.DefaultCellStyle.BackColor = Color.LightYellow;

                    row.Tag = solicitud;
                    solicitudesGrid.Rows.Add(row);
                }

                ActualizarContadores();
            }
            catch (Exception ex)
            {
                MostrarError($"Error cargando grilla: {ex.Message}");
            }
        }

        protected virtual void ActualizarContadores()
        {
            var total = solicitudes.Count;
            var urgentes = solicitudes.Count(s => s.EsUrgente);
            var pendientes = solicitudes.Sum(s => s.CantidadPendiente);

            statusLabel.Text = $"Total: {total} solicitudes | Urgentes: {urgentes} | Pendientes: {pendientes:N0} etiquetas";
        }

        protected virtual void SolicitudesGrid_SelectionChanged(object sender, EventArgs e)
        {
            if (solicitudesGrid.SelectedRows.Count > 0)
            {
                var solicitud = solicitudesGrid.SelectedRows[0].Tag as SolicitudEtiqueta;
                if (solicitud != null)
                {
                    CargarDatosSolicitud(solicitud);
                }
            }
        }

        protected virtual void CargarDatosSolicitud(SolicitudEtiqueta solicitud)
        {
            try
            {
                lblIdSolicitud.Text = solicitud.IdSolicitud.ToString();
                lblOrdenFab.Text = solicitud.OrdenFab;
                lblDescripcion.Text = solicitud.Descripcion;
                lblCantidad.Text = solicitud.CantidadPendiente.ToString();
                lblUPC.Text = solicitud.UPC1 ?? "";
                lblUPC2.Text = solicitud.UPC2 ?? "";

                // Cargar configuración específica del tipo
                CargarConfiguracionTipo();
            }
            catch (Exception ex)
            {
                MostrarError($"Error cargando datos: {ex.Message}");
            }
        }

        protected virtual void CargarConfiguracionTipo()
        {
            // Cargar velocidad y temperatura por defecto
            velocidadComboBox.SelectedItem = ConfigurationService.DefaultVelocidad;
            temperaturaComboBox.SelectedItem = ConfigurationService.DefaultTemperatura;

            // Cargar posiciones específicas del tipo
            CargarPosiciones();
        }

        protected virtual void CargarPosiciones()
        {
            try
            {
                var tipoString = tipoEtiqueta.ToCodeString();
                pos1TextBox.Text = ConfigurationService.GetEtiquetaPosition(tipoString, "Posicion1");
                pos2TextBox.Text = ConfigurationService.GetEtiquetaPosition(tipoString, "Posicion2");
                pos3TextBox.Text = ConfigurationService.GetEtiquetaPosition(tipoString, "Posicion3");
                pos4TextBox.Text = ConfigurationService.GetEtiquetaPosition(tipoString, "Posicion4");
                pos5TextBox.Text = ConfigurationService.GetEtiquetaPosition(tipoString, "Posicion5");
            }
            catch (Exception ex)
            {
                MostrarError($"Error cargando posiciones: {ex.Message}");
            }
        }

        protected virtual void GuardarPosiciones()
        {
            try
            {
                var tipoString = tipoEtiqueta.ToCodeString();
                ConfigurationService.SetEtiquetaPosition(tipoString, "Posicion1", pos1TextBox.Text);
                ConfigurationService.SetEtiquetaPosition(tipoString, "Posicion2", pos2TextBox.Text);
                ConfigurationService.SetEtiquetaPosition(tipoString, "Posicion3", pos3TextBox.Text);
                ConfigurationService.SetEtiquetaPosition(tipoString, "Posicion4", pos4TextBox.Text);
                ConfigurationService.SetEtiquetaPosition(tipoString, "Posicion5", pos5TextBox.Text);
            }
            catch (Exception ex)
            {
                MostrarError($"Error guardando posiciones: {ex.Message}");
            }
        }

        protected virtual void PosicionTextBox_TextChanged(object sender, EventArgs e)
        {
            // Validar que solo contenga números
            var textBox = sender as TextBox;
            if (textBox != null)
            {
                if (!int.TryParse(textBox.Text, out _) && !string.IsNullOrEmpty(textBox.Text))
                {
                    textBox.BackColor = Color.LightCoral;
                }
                else
                {
                    textBox.BackColor = SystemColors.Window;
                }
            }
        }

        protected virtual void BtnAjustarIzq_Click(object sender, EventArgs e)
        {
            AjustarPosiciones(-1);
        }

        protected virtual void BtnAjustarDer_Click(object sender, EventArgs e)
        {
            AjustarPosiciones(1);
        }

        protected virtual void AjustarPosiciones(int ajuste)
        {
            try
            {
                AjustarPosicion(pos1TextBox, ajuste);
                AjustarPosicion(pos2TextBox, ajuste);
                AjustarPosicion(pos3TextBox, ajuste);
                AjustarPosicion(pos4TextBox, ajuste);
                AjustarPosicion(pos5TextBox, ajuste);
            }
            catch (Exception ex)
            {
                MostrarError($"Error ajustando posiciones: {ex.Message}");
            }
        }

        protected virtual void AjustarPosicion(TextBox textBox, int ajuste)
        {
            if (int.TryParse(textBox.Text, out int valor))
            {
                textBox.Text = (valor + ajuste).ToString();
            }
        }

        protected virtual void SolicitudesGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                btnImprimir.PerformClick();
            }
        }

        protected virtual void BtnImprimir_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidarDatosImpresion())
                {
                    var solicitud = ObtenerSolicitudSeleccionada();
                    if (solicitud != null)
                    {
                        EjecutarImpresion(solicitud);
                    }
                }
            }
            catch (Exception ex)
            {
                MostrarError($"Error en impresión: {ex.Message}");
            }
        }

        protected virtual void BtnPrueba_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(impresoraSeleccionada))
                {
                    SeleccionarImpresora();
                }

                if (!string.IsNullOrEmpty(impresoraSeleccionada))
                {
                    if (PrinterService.TestPrinter(impresoraSeleccionada))
                    {
                        MostrarMensaje("Prueba de impresión enviada correctamente", false);
                    }
                    else
                    {
                        MostrarError("Error en la prueba de impresión");
                    }
                }
            }
            catch (Exception ex)
            {
                MostrarError($"Error en prueba: {ex.Message}");
            }
        }

        protected virtual bool ValidarDatosImpresion()
        {
            if (ObtenerSolicitudSeleccionada() == null)
            {
                MostrarError("Debe seleccionar una solicitud de etiquetas");
                return false;
            }

            if (string.IsNullOrEmpty(impresoraSeleccionada))
            {
                SeleccionarImpresora();
                return !string.IsNullOrEmpty(impresoraSeleccionada);
            }

            return true;
        }

        protected virtual SolicitudEtiqueta ObtenerSolicitudSeleccionada()
        {
            if (solicitudesGrid.SelectedRows.Count > 0)
            {
                return solicitudesGrid.SelectedRows[0].Tag as SolicitudEtiqueta;
            }
            return null;
        }

        protected virtual void EjecutarImpresion(SolicitudEtiqueta solicitud)
        {
            try
            {
                MostrarMensaje("Coloque las etiquetas y presione Aceptar", true);

                var etiquetaData = CrearEtiquetaData(solicitud);
                var zplCode = GenerarCodigoZPL(etiquetaData);

                if (PrinterService.SendZPLToPrinter(impresoraSeleccionada, zplCode))
                {
                    // Actualizar cantidad fabricada
                    DatabaseService.UpdateSolicitudEtiquetaFabricada(solicitud.IdSolicitud, solicitud.CantidadPedida);

                    // Recargar datos
                    CargarSolicitudes();

                    MostrarMensaje("Etiquetas impresas correctamente", false);
                }
                else
                {
                    MostrarError("Error enviando datos a la impresora");
                }
            }
            catch (Exception ex)
            {
                MostrarError($"Error en impresión: {ex.Message}");
            }
        }

        protected virtual EtiquetaData CrearEtiquetaData(SolicitudEtiqueta solicitud)
        {
            var etiquetaData = new EtiquetaData
            {
                TipoEtiqueta = tipoEtiqueta.ToCodeString(),
                UPC = solicitud.UPC1,
                UPC2 = solicitud.UPC2,
                Descripcion = solicitud.Descripcion,
                OrdenFab = solicitud.OrdenFab,
                Cantidad = solicitud.CantidadPendiente,
                Velocidad = (int)velocidadComboBox.SelectedItem,
                Temperatura = (int)temperaturaComboBox.SelectedItem,
                Color = solicitud.Color
            };

            // Cargar posiciones
            etiquetaData.Posiciones["Posicion1"] = pos1TextBox.Text;
            etiquetaData.Posiciones["Posicion2"] = pos2TextBox.Text;
            etiquetaData.Posiciones["Posicion3"] = pos3TextBox.Text;
            etiquetaData.Posiciones["Posicion4"] = pos4TextBox.Text;
            etiquetaData.Posiciones["Posicion5"] = pos5TextBox.Text;

            return etiquetaData;
        }

        protected virtual string GenerarCodigoZPL(EtiquetaData etiquetaData)
        {
            // Método base - debe ser sobrescrito en clases derivadas
            return ZPLService.GenerateEtiquetaCBCOE(etiquetaData);
        }

        protected virtual void SeleccionarImpresora()
        {
            using (var form = new SeleccionImpresoraForm(impresoras))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    impresoraSeleccionada = form.ImpresoraSeleccionada;
                }
            }
        }

        protected virtual void RefrescarDatos()
        {
            CargarSolicitudes();
        }

        protected virtual void MostrarError(string mensaje)
        {
            MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        protected virtual void MostrarMensaje(string mensaje, bool esAdvertencia)
        {
            var icon = esAdvertencia ? MessageBoxIcon.Warning : MessageBoxIcon.Information;
            MessageBox.Show(mensaje, "Información", MessageBoxButtons.OK, icon);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            try
            {
                GuardarPosiciones();
            }
            catch (Exception ex)
            {
                MostrarError($"Error guardando configuración: {ex.Message}");
            }
            base.OnFormClosing(e);
        }
    }
}