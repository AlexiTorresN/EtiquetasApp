using System;
using System.Linq;
using EtiquetasApp.Models;
using EtiquetasApp.Services;

namespace EtiquetasApp.Forms
{
    public partial class EtiquetasCBCOEForm : BaseEtiquetaForm
    {
        public EtiquetasCBCOEForm()
        {
            InitializeComponent();
            ConfigurarFormularioEspecifico();
        }

        protected override void InitializeBaseForm()
        {
            tipoEtiqueta = TipoEtiqueta.CBCOE;
            base.InitializeBaseForm();
        }

        private void ConfigurarFormularioEspecifico()
        {
            // Configuración específica para C/BCO-E
            Text = "Etiquetas C/BCO-E";
            lblTipoEtiqueta.Text = "Etiquetas C/BCO-E";

            // Configurar valores específicos
            ConfigurarControlesEspecificos();
        }

        private void ConfigurarControlesEspecificos()
        {
            // Configurar papel específico para C/BCO-E
            if (papelComboBox.Items.Contains("EMPACK"))
            {
                papelComboBox.SelectedItem = "EMPACK";
            }

            // Configurar velocidad y temperatura recomendadas
            velocidadComboBox.SelectedItem = TipoEtiqueta.CBCOE.GetVelocidadRecomendada().ToString().Replace("Normal", "4");
            temperaturaComboBox.SelectedItem = TipoEtiqueta.CBCOE.GetTemperaturaRecomendada().ToString().Replace("Baja", "6");

            // Deshabilitar etiqueta doble para este tipo
            etiquetaDobleCheckBox.Visible = false;
        }

        protected override void CargarSolicitudes()
        {
            try
            {
                // Cargar solicitudes específicas para C/BCO-E
                solicitudes = DatabaseService.GetSolicitudesEtiquetas()
                    .Where(s => string.IsNullOrEmpty(s.TipoEtiqueta) ||
                               s.TipoEtiqueta == "CBCOE" ||
                               s.TipoEtiqueta == "C/BCO-E")
                    .Where(s => s.CantidadPendiente > 0)
                    .OrderBy(s => s.FechaRequerida)
                    .ThenBy(s => s.EsUrgente ? 0 : 1)
                    .ToList();

                CargarGrillaSolicitudes();
            }
            catch (Exception ex)
            {
                MostrarError($"Error cargando solicitudes C/BCO-E: {ex.Message}");
            }
        }

        protected override void CargarPosiciones()
        {
            // Cargar posiciones específicas para C/BCO-E
            pos1TextBox.Text = ConfigurationService.GetEtiquetaPosition("CBCOE", "Posicion1");
            pos2TextBox.Text = ConfigurationService.GetEtiquetaPosition("CBCOE", "Posicion2");
            pos3TextBox.Text = ConfigurationService.GetEtiquetaPosition("CBCOE", "Posicion3");
            pos4TextBox.Text = ConfigurationService.GetEtiquetaPosition("CBCOE", "Posicion4");
            pos5TextBox.Text = ConfigurationService.GetEtiquetaPosition("CBCOE", "Posicion5");

            // Si no hay posiciones configuradas, usar valores por defecto de C/BCO-E
            if (string.IsNullOrEmpty(pos1TextBox.Text)) pos1TextBox.Text = "4";
            if (string.IsNullOrEmpty(pos2TextBox.Text)) pos2TextBox.Text = "148";
            if (string.IsNullOrEmpty(pos3TextBox.Text)) pos3TextBox.Text = "292";
            if (string.IsNullOrEmpty(pos4TextBox.Text)) pos4TextBox.Text = "436";
            if (string.IsNullOrEmpty(pos5TextBox.Text)) pos5TextBox.Text = "580";
        }

        protected override void GuardarPosiciones()
        {
            // Guardar posiciones específicas para C/BCO-E
            ConfigurationService.SetEtiquetaPosition("CBCOE", "Posicion1", pos1TextBox.Text);
            ConfigurationService.SetEtiquetaPosition("CBCOE", "Posicion2", pos2TextBox.Text);
            ConfigurationService.SetEtiquetaPosition("CBCOE", "Posicion3", pos3TextBox.Text);
            ConfigurationService.SetEtiquetaPosition("CBCOE", "Posicion4", pos4TextBox.Text);
            ConfigurationService.SetEtiquetaPosition("CBCOE", "Posicion5", pos5TextBox.Text);
        }

        protected override string GenerarCodigoZPL(EtiquetaData etiquetaData)
        {
            return ZPLService.GenerateEtiquetaCBCOE(etiquetaData);
        }

        protected override EtiquetaData CrearEtiquetaData(SolicitudEtiqueta solicitud)
        {
            var etiquetaData = base.CrearEtiquetaData(solicitud);

            // Configuraciones específicas para C/BCO-E
            etiquetaData.TipoEtiqueta = "CBCOE";

            // Verificar si requiere logo
            var maestroEtiqueta = ObtenerMaestroEtiqueta(solicitud.OrdenFab);
            if (maestroEtiqueta != null && maestroEtiqueta.RequiereLogo)
            {
                etiquetaData.Logo = maestroEtiqueta.NombreLogo ?? "PINO";
            }

            return etiquetaData;
        }

        private MaestroCodigoEtiqueta ObtenerMaestroEtiqueta(string ordenFab)
        {
            try
            {
                var maestros = DatabaseService.MaestroCodigosEtiquetas;
                return maestros.FirstOrDefault(m => m.PartId == ordenFab);
            }
            catch (Exception ex)
            {
                MostrarError($"Error obteniendo maestro de etiquetas: {ex.Message}");
                return null;
            }
        }

        protected override void ActualizarContadores()
        {
            base.ActualizarContadores();

            // Información adicional específica para C/BCO-E
            var totalEtiquetas = solicitudes.Sum(s => s.CantidadPendiente);
            var hojas = totalEtiquetas / 5; // C/BCO-E tiene 5 etiquetas por hoja

            statusLabel.Text += $" | Hojas requeridas: {hojas:N0}";
        }

        protected override bool ValidarDatosImpresion()
        {
            if (!base.ValidarDatosImpresion())
                return false;

            var solicitud = ObtenerSolicitudSeleccionada();
            if (solicitud == null)
                return false;

            // Validaciones específicas para C/BCO-E
            if (string.IsNullOrEmpty(solicitud.UPC1))
            {
                MostrarError("La solicitud seleccionada no tiene código UPC configurado");
                return false;
            }

            if (string.IsNullOrEmpty(solicitud.Descripcion))
            {
                MostrarError("La solicitud seleccionada no tiene descripción");
                return false;
            }

            // Validar posiciones
            if (!ValidarPosiciones())
            {
                MostrarError("Las posiciones de columnas deben ser números válidos");
                return false;
            }

            return true;
        }

        private bool ValidarPosiciones()
        {
            return int.TryParse(pos1TextBox.Text, out _) &&
                   int.TryParse(pos2TextBox.Text, out _) &&
                   int.TryParse(pos3TextBox.Text, out _) &&
                   int.TryParse(pos4TextBox.Text, out _) &&
                   int.TryParse(pos5TextBox.Text, out _);
        }

        protected override void CargarDatosSolicitud(SolicitudEtiqueta solicitud)
        {
            base.CargarDatosSolicitud(solicitud);

            // Cargar datos específicos para C/BCO-E
            ActualizarVisualizacionEtiqueta(solicitud);
        }

        private void ActualizarVisualizacionEtiqueta(SolicitudEtiqueta solicitud)
        {
            try
            {
                // Actualizar los botones de colores para mostrar la previsualización
                var cantidad = solicitud.CantidadPendiente;
                var etiquetasPorHoja = 5;
                var hojas = (cantidad + etiquetasPorHoja - 1) / etiquetasPorHoja; // Redondear hacia arriba

                for (int i = 0; i < colorButtons.Length; i++)
                {
                    if (i < etiquetasPorHoja)
                    {
                        colorButtons[i].BackColor = System.Drawing.Color.White;
                        colorButtons[i].Text = solicitud.OrdenFab ?? "";
                        colorButtons[i].Enabled = true;
                    }
                    else
                    {
                        colorButtons[i].BackColor = System.Drawing.Color.LightGray;
                        colorButtons[i].Text = "0";
                        colorButtons[i].Enabled = false;
                    }
                }

                // Actualizar información en el status
                statusLabel.Text = $"Orden: {solicitud.OrdenFab} | Cantidad: {cantidad:N0} | Hojas: {hojas:N0} | Tipo: C/BCO-E";
            }
            catch (Exception ex)
            {
                MostrarError($"Error actualizando visualización: {ex.Message}");
            }
        }

        // Evento específico para C/BCO-E
        private void PapelComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var papelSeleccionado = papelComboBox.SelectedItem?.ToString();
                if (!string.IsNullOrEmpty(papelSeleccionado))
                {
                    // Ajustar configuraciones según el tipo de papel
                    switch (papelSeleccionado)
                    {
                        case "EMPACK":
                            temperaturaComboBox.SelectedItem = "6";
                            break;
                        case "SOLUCORP":
                            temperaturaComboBox.SelectedItem = "8";
                            break;
                        case "Térmico":
                            temperaturaComboBox.SelectedItem = "4";
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MostrarError($"Error cambiando tipo de papel: {ex.Message}");
            }
        }

        // Override para personalizar mensajes
        protected override void MostrarMensaje(string mensaje, bool esAdvertencia)
        {
            if (mensaje.Contains("etiquetas") && !esAdvertencia)
            {
                mensaje = $"C/BCO-E: {mensaje}";
            }
            base.MostrarMensaje(mensaje, esAdvertencia);
        }
    }
}