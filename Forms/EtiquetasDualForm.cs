using System;
using System.Linq;
using EtiquetasApp.Models;
using EtiquetasApp.Services;

namespace EtiquetasApp.Forms
{
    public partial class EtiquetasDualForm : BaseEtiquetaForm
    {
        public EtiquetasDualForm()
        {
            InitializeComponent();
            ConfigurarFormularioEspecifico();
        }

        protected override void InitializeBaseForm()
        {
            tipoEtiqueta = TipoEtiqueta.DUAL;
            base.InitializeBaseForm();
        }

        private void ConfigurarFormularioEspecifico()
        {
            // Configuración específica para DUAL
            Text = "Etiquetas DUAL";
            lblTipoEtiqueta.Text = "Etiquetas DUAL";

            // Configurar valores específicos
            ConfigurarControlesEspecificos();
        }

        private void ConfigurarControlesEspecificos()
        {
            // Configurar papel específico para DUAL
            if (papelComboBox.Items.Contains("EMPACK"))
            {
                papelComboBox.SelectedItem = "EMPACK";
            }

            // Configurar velocidad y temperatura recomendadas
            velocidadComboBox.SelectedItem = TipoEtiqueta.DUAL.GetVelocidadRecomendada().ToString().Replace("Normal", "4");
            temperaturaComboBox.SelectedItem = TipoEtiqueta.DUAL.GetTemperaturaRecomendada().ToString().Replace("Baja", "6");

            // Habilitar etiqueta doble para este tipo
            etiquetaDobleCheckBox.Visible = true;
            etiquetaDobleCheckBox.Checked = true;
            etiquetaDobleCheckBox.Enabled = false; // Siempre doble para DUAL

            // Hacer más visible el campo UPC2
            if (lblUPC2 != null)
            {
                lblUPC2.Font = new System.Drawing.Font(lblUPC2.Font, System.Drawing.FontStyle.Bold);
                lblUPC2.ForeColor = System.Drawing.Color.DarkBlue;
            }
        }

        protected override void CargarSolicitudes()
        {
            try
            {
                // Cargar solicitudes específicas para DUAL
                solicitudes = DatabaseService.GetSolicitudesEtiquetas()
                    .Where(s => s.TipoEtiqueta == "DUAL")
                    .Where(s => s.CantidadPendiente > 0)
                    .Where(s => !string.IsNullOrEmpty(s.UPC1) && !string.IsNullOrEmpty(s.UPC2)) // DUAL requiere ambos UPC
                    .OrderBy(s => s.FechaRequerida)
                    .ThenBy(s => s.EsUrgente ? 0 : 1)
                    .ToList();

                CargarGrillaSolicitudes();
            }
            catch (Exception ex)
            {
                MostrarError($"Error cargando solicitudes DUAL: {ex.Message}");
            }
        }

        protected override void CargarPosiciones()
        {
            // Cargar posiciones específicas para DUAL
            pos1TextBox.Text = ConfigurationService.GetEtiquetaPosition("DUAL", "Posicion1");
            pos2TextBox.Text = ConfigurationService.GetEtiquetaPosition("DUAL", "Posicion2");
            pos3TextBox.Text = ConfigurationService.GetEtiquetaPosition("DUAL", "Posicion3");
            pos4TextBox.Text = ConfigurationService.GetEtiquetaPosition("DUAL", "Posicion4");
            pos5TextBox.Text = ConfigurationService.GetEtiquetaPosition("DUAL", "Posicion5");

            // Si no hay posiciones configuradas, usar valores por defecto de DUAL
            if (string.IsNullOrEmpty(pos1TextBox.Text)) pos1TextBox.Text = "17";
            if (string.IsNullOrEmpty(pos2TextBox.Text)) pos2TextBox.Text = "185";
            if (string.IsNullOrEmpty(pos3TextBox.Text)) pos3TextBox.Text = "354";
            if (string.IsNullOrEmpty(pos4TextBox.Text)) pos4TextBox.Text = "522";
            if (string.IsNullOrEmpty(pos5TextBox.Text)) pos5TextBox.Text = "689";
        }

        protected override void GuardarPosiciones()
        {
            // Guardar posiciones específicas para DUAL
            ConfigurationService.SetEtiquetaPosition("DUAL", "Posicion1", pos1TextBox.Text);
            ConfigurationService.SetEtiquetaPosition("DUAL", "Posicion2", pos2TextBox.Text);
            ConfigurationService.SetEtiquetaPosition("DUAL", "Posicion3", pos3TextBox.Text);
            ConfigurationService.SetEtiquetaPosition("DUAL", "Posicion4", pos4TextBox.Text);
            ConfigurationService.SetEtiquetaPosition("DUAL", "Posicion5", pos5TextBox.Text);
        }

        protected override string GenerarCodigoZPL(EtiquetaData etiquetaData)
        {
            return ZPLService.GenerateEtiquetaDual(etiquetaData);
        }

        protected override EtiquetaData CrearEtiquetaData(SolicitudEtiqueta solicitud)
        {
            var etiquetaData = base.CrearEtiquetaData(solicitud);

            // Configuraciones específicas para DUAL
            etiquetaData.TipoEtiqueta = "DUAL";
            etiquetaData.EtiquetaDoble = true;

            // Verificar que ambos UPC estén presentes
            if (string.IsNullOrEmpty(etiquetaData.UPC2))
            {
                throw new InvalidOperationException("Las etiquetas DUAL requieren ambos códigos UPC1 y UPC2");
            }

            return etiquetaData;
        }

        protected override void ActualizarContadores()
        {
            base.ActualizarContadores();

            // Información adicional específica para DUAL
            var totalEtiquetas = solicitudes.Sum(s => s.CantidadPendiente);
            var hojas = totalEtiquetas / 5; // DUAL también tiene 5 etiquetas por hoja (pero duales)

            statusLabel.Text += $" | Hojas requeridas: {hojas:N0} | Códigos duales: {totalEtiquetas * 2:N0}";
        }

        protected override bool ValidarDatosImpresion()
        {
            if (!base.ValidarDatosImpresion())
                return false;

            var solicitud = ObtenerSolicitudSeleccionada();
            if (solicitud == null)
                return false;

            // Validaciones específicas para DUAL
            if (string.IsNullOrEmpty(solicitud.UPC1))
            {
                MostrarError("La solicitud seleccionada no tiene código UPC1 configurado");
                return false;
            }

            if (string.IsNullOrEmpty(solicitud.UPC2))
            {
                MostrarError("Las etiquetas DUAL requieren código UPC2 configurado");
                return false;
            }

            if (string.IsNullOrEmpty(solicitud.Descripcion))
            {
                MostrarError("La solicitud seleccionada no tiene descripción");
                return false;
            }

            // Validar posiciones
            if (!ValidarPosicionesDual())
            {
                MostrarError("Las posiciones de columnas deben ser números válidos y apropiadas para etiquetas duales");
                return false;
            }

            return true;
        }

        private bool ValidarPosicionesDual()
        {
            // Validar que las posiciones sean números válidos
            if (!int.TryParse(pos1TextBox.Text, out int pos1) ||
                !int.TryParse(pos2TextBox.Text, out int pos2) ||
                !int.TryParse(pos3TextBox.Text, out int pos3) ||
                !int.TryParse(pos4TextBox.Text, out int pos4) ||
                !int.TryParse(pos5TextBox.Text, out int pos5))
            {
                return false;
            }

            // Validar que haya suficiente espacio entre posiciones para códigos duales
            var posiciones = new[] { pos1, pos2, pos3, pos4, pos5 };
            for (int i = 0; i < posiciones.Length - 1; i++)
            {
                if (Math.Abs(posiciones[i + 1] - posiciones[i]) < 130) // Mínimo 130 píxeles para códigos duales
                {
                    MostrarError($"Las posiciones {i + 1} y {i + 2} están muy cerca para etiquetas duales (mínimo 130 píxeles)");
                    return false;
                }
            }

            return true;
        }

        protected override void CargarDatosSolicitud(SolicitudEtiqueta solicitud)
        {
            base.CargarDatosSolicitud(solicitud);

            // Cargar datos específicos para DUAL
            ActualizarVisualizacionEtiquetaDual(solicitud);
            ValidarCodigosDuales(solicitud);
        }

        private void ValidarCodigosDuales(SolicitudEtiqueta solicitud)
        {
            // Validar que ambos códigos UPC estén presentes
            if (string.IsNullOrEmpty(solicitud.UPC1) || string.IsNullOrEmpty(solicitud.UPC2))
            {
                statusLabel.Text = "⚠ ADVERTENCIA: Faltan códigos UPC para etiqueta DUAL";
                statusLabel.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                statusLabel.Text = $"✓ Códigos duales válidos: {solicitud.UPC1} / {solicitud.UPC2}";
                statusLabel.ForeColor = System.Drawing.Color.DarkGreen;
            }
        }

        private void ActualizarVisualizacionEtiquetaDual(SolicitudEtiqueta solicitud)
        {
            try
            {
                // Actualizar los botones de colores para mostrar la previsualización dual
                var cantidad = solicitud.CantidadPendiente;
                var etiquetasPorHoja = 5;
                var hojas = (cantidad + etiquetasPorHoja - 1) / etiquetasPorHoja;

                for (int i = 0; i < colorButtons.Length; i++)
                {
                    if (i < etiquetasPorHoja)
                    {
                        colorButtons[i].BackColor = System.Drawing.Color.LightBlue;
                        // Mostrar ambos códigos en el botón
                        var texto = $"{solicitud.UPC1}\n{solicitud.UPC2}";
                        colorButtons[i].Text = texto;
                        colorButtons[i].Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold);
                        colorButtons[i].Enabled = true;
                    }
                    else
                    {
                        colorButtons[i].BackColor = System.Drawing.Color.LightGray;
                        colorButtons[i].Text = "0";
                        colorButtons[i].Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
                        colorButtons[i].Enabled = false;
                    }
                }

                // Actualizar información en el status
                statusLabel.Text = $"Orden: {solicitud.OrdenFab} | Cantidad: {cantidad:N0} | Hojas: {hojas:N0} | Tipo: DUAL";
            }
            catch (Exception ex)
            {
                MostrarError($"Error actualizando visualización dual: {ex.Message}");
            }
        }

        // Método específico para ajustar posiciones considerando el espacio dual
        protected override void AjustarPosiciones(int ajuste)
        {
            try
            {
                // Para etiquetas duales, ajustar en incrementos mayores
                var ajusteReal = ajuste * 2; // Doble ajuste para mantener proporciones

                AjustarPosicion(pos1TextBox, ajusteReal);
                AjustarPosicion(pos2TextBox, ajusteReal);
                AjustarPosicion(pos3TextBox, ajusteReal);
                AjustarPosicion(pos4TextBox, ajusteReal);
                AjustarPosicion(pos5TextBox, ajusteReal);
            }
            catch (Exception ex)
            {
                MostrarError($"Error ajustando posiciones duales: {ex.Message}");
            }
        }

        // Evento específico para cambio de papel en DUAL
        private void PapelComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var papelSeleccionado = papelComboBox.SelectedItem?.ToString();
                if (!string.IsNullOrEmpty(papelSeleccionado))
                {
                    // Ajustar configuraciones según el tipo de papel para DUAL
                    switch (papelSeleccionado)
                    {
                        case "EMPACK":
                            temperaturaComboBox.SelectedItem = "6";
                            velocidadComboBox.SelectedItem = "4";
                            break;
                        case "SOLUCORP":
                            temperaturaComboBox.SelectedItem = "8";
                            velocidadComboBox.SelectedItem = "3"; // Más lento para mejor calidad en dual
                            break;
                        case "Térmico":
                            temperaturaComboBox.SelectedItem = "4";
                            velocidadComboBox.SelectedItem = "5";
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MostrarError($"Error cambiando tipo de papel para DUAL: {ex.Message}");
            }
        }

        // Override para personalizar mensajes específicos de DUAL
        protected override void MostrarMensaje(string mensaje, bool esAdvertencia)
        {
            if (mensaje.Contains("etiquetas") && !esAdvertencia)
            {
                mensaje = $"DUAL: {mensaje} (Códigos duales)";
            }
            base.MostrarMensaje(mensaje, esAdvertencia);
        }

        // Método para verificar compatibilidad de códigos
        private bool VerificarCompatibilidadCodigos(string upc1, string upc2)
        {
            // Verificar que los códigos no sean iguales
            if (upc1.Equals(upc2, StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            // Verificar longitudes similares para mejor distribución visual
            var diferencia = Math.Abs(upc1.Length - upc2.Length);
            return diferencia <= 3; // Máximo 3 caracteres de diferencia
        }

        protected override void EjecutarImpresion(SolicitudEtiqueta solicitud)
        {
            try
            {
                // Verificar compatibilidad de códigos antes de imprimir
                if (!VerificarCompatibilidadCodigos(solicitud.UPC1, solicitud.UPC2))
                {
                    var result = MessageBox.Show(
                        $"Los códigos UPC tienen longitudes muy diferentes:\nUPC1: {solicitud.UPC1}\nUPC2: {solicitud.UPC2}\n\n¿Continuar con la impresión?",
                        "Advertencia - Códigos Duales",
                        System.Windows.Forms.MessageBoxButtons.YesNo,
                        System.Windows.Forms.MessageBoxIcon.Warning);

                    if (result == System.Windows.Forms.DialogResult.No)
                        return;
                }

                MostrarMensaje("Coloque las etiquetas DUAL y presione Aceptar", true);

                base.EjecutarImpresion(solicitud);
            }
            catch (Exception ex)
            {
                MostrarError($"Error en impresión DUAL: {ex.Message}");
            }
        }
    }
}