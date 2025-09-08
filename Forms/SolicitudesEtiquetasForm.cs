using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using EtiquetasApp.Models;
using EtiquetasApp.Services;

namespace EtiquetasApp.Forms
{
    public partial class SolicitudesEtiquetasForm : Form
    {
        private List<OrdenFabricacion> ordenesDisponibles;
        private List<MaestroCodigoEtiqueta> maestrosCodigos;
        private SolicitudEtiqueta solicitudActual;

        public SolicitudesEtiquetasForm()
        {
            InitializeComponent();
            InitializeForm();
        }

        private void InitializeForm()
        {
            ordenesDisponibles = new List<OrdenFabricacion>();
            maestrosCodigos = new List<MaestroCodigoEtiqueta>();
            solicitudActual = new SolicitudEtiqueta();

            ConfigurarEventos();
            CargarDatosIniciales();
            ConfigurarControles();
            LimpiarFormulario();
        }

        private void ConfigurarEventos()
        {
            // Eventos de búsqueda
            ordenFabTextBox.TextChanged += OrdenFabTextBox_TextChanged;
            ordenFabTextBox.KeyPress += OrdenFabTextBox_KeyPress;
            btnBuscarOrden.Click += BtnBuscarOrden_Click;

            // Eventos de combos
            tipoEtiquetaComboBox.SelectedIndexChanged += TipoEtiquetaComboBox_SelectedIndexChanged;
            colorComboBox.SelectedIndexChanged += ColorComboBox_SelectedIndexChanged;

            // Eventos de fechas
            fechaRequieridaDateTime.ValueChanged += FechaRequieridaDateTime_ValueChanged;

            // Eventos de cantidad
            cantidadNumericUpDown.ValueChanged += CantidadNumericUpDown_ValueChanged;

            // Eventos de botones
            btnGuardar.Click += BtnGuardar_Click;
            btnLimpiar.Click += BtnLimpiar_Click;
            btnCancelar.Click += BtnCancelar_Click;
            btnVerMaestro.Click += BtnVerMaestro_Click;
            btnCrearMaestro.Click += BtnCrearMaestro_Click;

            // Eventos del formulario
            Load += SolicitudesEtiquetasForm_Load;
        }

        private void ConfigurarControles()
        {
            // Configurar ComboBox de tipo de etiqueta
            tipoEtiquetaComboBox.Items.Clear();
            foreach (TipoEtiqueta tipo in Enum.GetValues(typeof(TipoEtiqueta)))
            {
                tipoEtiquetaComboBox.Items.Add(tipo.GetDisplayName());
            }
            tipoEtiquetaComboBox.SelectedIndex = 0; // CBCOE por defecto

            // Configurar ComboBox de colores
            colorComboBox.Items.Clear();
            foreach (ColorEtiqueta color in Enum.GetValues(typeof(ColorEtiqueta)))
            {
                colorComboBox.Items.Add(color.GetDisplayName());
            }
            colorComboBox.SelectedIndex = 0; // Blancas por defecto

            // Configurar fechas
            fechaRequieridaDateTime.Value = DateTime.Now.AddDays(2); // 2 días por defecto
            fechaRequieridaDateTime.MinDate = DateTime.Now.Date;

            // Configurar cantidad
            cantidadNumericUpDown.Minimum = 1;
            cantidadNumericUpDown.Maximum = 100000;
            cantidadNumericUpDown.Value = 100;

            // Configurar campos de solo lectura inicialmente
            descripcionTextBox.ReadOnly = true;
            upc1TextBox.ReadOnly = true;
            upc2TextBox.ReadOnly = true;
        }

        private void CargarDatosIniciales()
        {
            try
            {
                maestrosCodigos = DatabaseService.GetMaestroCodigosEtiquetas();
                statusLabel.Text = "Listo para crear nueva solicitud";
            }
            catch (Exception ex)
            {
                MostrarError($"Error cargando datos iniciales: {ex.Message}");
            }
        }

        private void SolicitudesEtiquetasForm_Load(object sender, EventArgs e)
        {
            ordenFabTextBox.Focus();
        }

        private void OrdenFabTextBox_TextChanged(object sender, EventArgs e)
        {
            // Limpiar datos relacionados cuando cambia la orden
            LimpiarDatosOrden();

            // Habilitar búsqueda solo si hay texto
            btnBuscarOrden.Enabled = !string.IsNullOrEmpty(ordenFabTextBox.Text.Trim());
        }

        private void OrdenFabTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                BtnBuscarOrden_Click(sender, e);
            }
        }

        private void BtnBuscarOrden_Click(object sender, EventArgs e)
        {
            var ordenFab = ordenFabTextBox.Text.Trim();
            if (string.IsNullOrEmpty(ordenFab))
            {
                MostrarError("Ingrese un número de orden de fabricación");
                return;
            }

            try
            {
                BuscarOrdenFabricacion(ordenFab);
            }
            catch (Exception ex)
            {
                MostrarError($"Error buscando orden: {ex.Message}");
            }
        }

        private void BuscarOrdenFabricacion(string ordenFab)
        {
            try
            {
                // Buscar en órdenes de requerimientos
                var ordenes = DatabaseService.GetOrdenesRequerimientos();
                var orden = ordenes.FirstOrDefault(o => o.OrdenFab.Equals(ordenFab, StringComparison.OrdinalIgnoreCase));

                if (orden != null)
                {
                    CargarDatosOrden(orden);
                }
                else
                {
                    // Si no se encuentra en requerimientos, permitir crear solicitud manual
                    var result = MessageBox.Show($"La orden '{ordenFab}' no se encontró en los requerimientos.\n¿Desea crear una solicitud manual?",
                                               "Orden no encontrada", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        CrearSolicitudManual(ordenFab);
                    }
                }
            }
            catch (Exception ex)
            {
                MostrarError($"Error buscando orden de fabricación: {ex.Message}");
            }
        }

        private void CargarDatosOrden(OrdenFabricacion orden)
        {
            try
            {
                // Cargar datos de la orden
                descripcionTextBox.Text = orden.Descripcion;
                cantidadNumericUpDown.Value = orden.Cantidad;
                fechaRequieridaDateTime.Value = orden.FechaRequerida.AddDays(-1); // Un día antes

                // Buscar códigos en maestro
                var maestro = maestrosCodigos.FirstOrDefault(m => m.PartId.Equals(orden.PartId, StringComparison.OrdinalIgnoreCase));

                if (maestro != null)
                {
                    CargarDatosMaestro(maestro);
                    maestroEncontradoLabel.Text = "✓ Maestro encontrado";
                    maestroEncontradoLabel.ForeColor = Color.DarkGreen;
                    btnVerMaestro.Enabled = true;
                }
                else
                {
                    maestroEncontradoLabel.Text = "⚠ Maestro no encontrado";
                    maestroEncontradoLabel.ForeColor = Color.Red;
                    btnCrearMaestro.Enabled = true;
                    HabilitarEdicionCodigos();
                }

                // Actualizar información de la orden
                ActualizarInfoOrden(orden);

                statusLabel.Text = $"Orden cargada: {orden.OrdenFab}";
            }
            catch (Exception ex)
            {
                MostrarError($"Error cargando datos de orden: {ex.Message}");
            }
        }

        private void CargarDatosMaestro(MaestroCodigoEtiqueta maestro)
        {
            upc1TextBox.Text = maestro.UPC1;
            upc2TextBox.Text = maestro.UPC2;

            // Seleccionar tipo de etiqueta
            var tipoIndex = tipoEtiquetaComboBox.Items.Cast<string>()
                .ToList().FindIndex(item => item.Contains(maestro.TipoEtiqueta));
            if (tipoIndex >= 0)
                tipoEtiquetaComboBox.SelectedIndex = tipoIndex;

            // Seleccionar color
            var colorIndex = colorComboBox.Items.Cast<string>()
                .ToList().FindIndex(item => item.Equals(maestro.ColorEtiqueta, StringComparison.OrdinalIgnoreCase));
            if (colorIndex >= 0)
                colorComboBox.SelectedIndex = colorIndex;

            observacionesTextBox.Text = maestro.Observaciones;
        }

        private void CrearSolicitudManual(string ordenFab)
        {
            descripcionTextBox.Text = "";
            cantidadNumericUpDown.Value = 100;
            fechaRequieridaDateTime.Value = DateTime.Now.AddDays(2);

            maestroEncontradoLabel.Text = "⚠ Solicitud manual";
            maestroEncontradoLabel.ForeColor = Color.Blue;

            HabilitarEdicionCodigos();
            HabilitarEdicionDescripcion();

            statusLabel.Text = "Solicitud manual - Complete los datos necesarios";
        }

        private void HabilitarEdicionCodigos()
        {
            upc1TextBox.ReadOnly = false;
            upc2TextBox.ReadOnly = false;
            upc1TextBox.BackColor = SystemColors.Window;
            upc2TextBox.BackColor = SystemColors.Window;
        }

        private void HabilitarEdicionDescripcion()
        {
            descripcionTextBox.ReadOnly = false;
            descripcionTextBox.BackColor = SystemColors.Window;
        }

        private void ActualizarInfoOrden(OrdenFabricacion orden)
        {
            var info = $"Parte: {orden.PartId}\n";
            info += $"Cantidad: {orden.Cantidad:N0}\n";
            info += $"Fecha Inicio: {orden.FechaInicio:dd/MM/yyyy}\n";
            info += $"Fecha Requerida: {orden.FechaRequerida:dd/MM/yyyy}\n";
            info += $"Estado: {orden.EstadoDescripcion}\n";
            info += $"Prioridad: {orden.PrioridadDescripcion}";

            if (orden.EsUrgente)
            {
                info += "\n⚠ URGENTE";
                infoOrdenLabel.ForeColor = Color.Red;
            }
            else
            {
                infoOrdenLabel.ForeColor = SystemColors.ControlText;
            }

            infoOrdenLabel.Text = info;
        }

        private void LimpiarDatosOrden()
        {
            descripcionTextBox.Clear();
            upc1TextBox.Clear();
            upc2TextBox.Clear();
            infoOrdenLabel.Text = "";
            maestroEncontradoLabel.Text = "";

            btnVerMaestro.Enabled = false;
            btnCrearMaestro.Enabled = false;

            descripcionTextBox.ReadOnly = true;
            upc1TextBox.ReadOnly = true;
            upc2TextBox.ReadOnly = true;
            descripcionTextBox.BackColor = SystemColors.Control;
            upc1TextBox.BackColor = SystemColors.Control;
            upc2TextBox.BackColor = SystemColors.Control;
        }

        private void TipoEtiquetaComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ValidarFormulario();
            ActualizarEjemplosUPC();
        }

        private void ColorComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ValidarFormulario();
        }

        private void FechaRequieridaDateTime_ValueChanged(object sender, EventArgs e)
        {
            ValidarFormulario();
            ValidarFechaRequerida();
        }

        private void CantidadNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            ValidarFormulario();
        }

        private void ValidarFechaRequerida()
        {
            var fechaSeleccionada = fechaRequieridaDateTime.Value.Date;
            var hoy = DateTime.Now.Date;
            var diferenciaDias = (fechaSeleccionada - hoy).Days;

            if (diferenciaDias < 0)
            {
                fechaRequieridaDateTime.BackColor = Color.LightCoral;
                fechaAdvertenciaLabel.Text = "⚠ Fecha en el pasado";
                fechaAdvertenciaLabel.ForeColor = Color.Red;
            }
            else if (diferenciaDias <= 1)
            {
                fechaRequieridaDateTime.BackColor = Color.LightYellow;
                fechaAdvertenciaLabel.Text = "⚠ Fecha muy próxima";
                fechaAdvertenciaLabel.ForeColor = Color.Orange;
            }
            else
            {
                fechaRequieridaDateTime.BackColor = SystemColors.Window;
                fechaAdvertenciaLabel.Text = $"✓ {diferenciaDias} días disponibles";
                fechaAdvertenciaLabel.ForeColor = Color.DarkGreen;
            }
        }

        private void ActualizarEjemplosUPC()
        {
            var tipoSeleccionado = ObtenerTipoEtiquetaSeleccionado();

            switch (tipoSeleccionado)
            {
                case TipoEtiqueta.EAN13:
                    upcEjemploLabel.Text = "Ejemplo UPC1: 1234567890123 (13 dígitos)";
                    break;
                case TipoEtiqueta.DUAL:
                    upcEjemploLabel.Text = "Ejemplo UPC1: ABC123, UPC2: DEF456";
                    break;
                default:
                    upcEjemploLabel.Text = "Ejemplo UPC1: ABC123456";
                    break;
            }
        }

        private TipoEtiqueta ObtenerTipoEtiquetaSeleccionado()
        {
            if (tipoEtiquetaComboBox.SelectedIndex >= 0)
            {
                var tipoText = tipoEtiquetaComboBox.SelectedItem.ToString();
                return EnumExtensions.ParseTipoEtiqueta(tipoText);
            }
            return TipoEtiqueta.CBCOE;
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidarDatosCompletos())
                {
                    GuardarSolicitud();
                }
            }
            catch (Exception ex)
            {
                MostrarError($"Error guardando solicitud: {ex.Message}");
            }
        }

        private bool ValidarDatosCompletos()
        {
            var errores = new List<string>();

            if (string.IsNullOrEmpty(ordenFabTextBox.Text.Trim()))
                errores.Add("La orden de fabricación es requerida");

            if (string.IsNullOrEmpty(descripcionTextBox.Text.Trim()))
                errores.Add("La descripción es requerida");

            if (string.IsNullOrEmpty(upc1TextBox.Text.Trim()))
                errores.Add("El código UPC1 es requerido");

            if (cantidadNumericUpDown.Value <= 0)
                errores.Add("La cantidad debe ser mayor a cero");

            if (fechaRequieridaDateTime.Value.Date < DateTime.Now.Date)
                errores.Add("La fecha requerida no puede ser en el pasado");

            // Validaciones específicas por tipo
            var tipo = ObtenerTipoEtiquetaSeleccionado();
            if (tipo.RequiereDualUPC() && string.IsNullOrEmpty(upc2TextBox.Text.Trim()))
                errores.Add("El tipo DUAL requiere UPC2");

            if (tipo == TipoEtiqueta.EAN13 && !ValidarEAN13(upc1TextBox.Text.Trim()))
                errores.Add("El código EAN13 debe tener 13 dígitos numéricos");

            if (errores.Any())
            {
                var mensaje = "Errores de validación:\n" + string.Join("\n", errores);
                MostrarError(mensaje);
                return false;
            }

            return true;
        }

        private bool ValidarEAN13(string codigo)
        {
            return codigo.Length == 13 && codigo.All(char.IsDigit);
        }

        private void GuardarSolicitud()
        {
            try
            {
                var solicitud = new SolicitudEtiqueta
                {
                    OrdenFab = ordenFabTextBox.Text.Trim(),
                    Descripcion = descripcionTextBox.Text.Trim(),
                    CantidadPedida = (int)cantidadNumericUpDown.Value,
                    FechaRequerida = fechaRequieridaDateTime.Value,
                    UPC1 = upc1TextBox.Text.Trim(),
                    UPC2 = upc2TextBox.Text.Trim(),
                    TipoEtiqueta = ObtenerTipoEtiquetaSeleccionado().ToCodeString(),
                    Color = colorComboBox.SelectedItem?.ToString() ?? "Blancas",
                    Observaciones = observacionesTextBox.Text.Trim(),
                    UsuarioSolicita = Environment.UserName
                };

                if (DatabaseService.InsertSolicitudEtiqueta(solicitud))
                {
                    MostrarMensaje("Solicitud guardada correctamente", false);
                    LimpiarFormulario();
                    statusLabel.Text = "Solicitud guardada - Lista para nueva solicitud";
                }
                else
                {
                    MostrarError("Error guardando la solicitud en la base de datos");
                }
            }
            catch (Exception ex)
            {
                MostrarError($"Error guardando solicitud: {ex.Message}");
            }
        }

        private void BtnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void LimpiarFormulario()
        {
            ordenFabTextBox.Clear();
            descripcionTextBox.Clear();
            upc1TextBox.Clear();
            upc2TextBox.Clear();
            observacionesTextBox.Clear();
            infoOrdenLabel.Text = "";
            maestroEncontradoLabel.Text = "";
            fechaAdvertenciaLabel.Text = "";
            upcEjemploLabel.Text = "";

            cantidadNumericUpDown.Value = 100;
            fechaRequieridaDateTime.Value = DateTime.Now.AddDays(2);
            tipoEtiquetaComboBox.SelectedIndex = 0;
            colorComboBox.SelectedIndex = 0;

            btnVerMaestro.Enabled = false;
            btnCrearMaestro.Enabled = false;
            btnGuardar.Enabled = false;

            LimpiarDatosOrden();
            ordenFabTextBox.Focus();

            statusLabel.Text = "Formulario limpiado - Listo para nueva solicitud";
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            if (FormularioTieneDatos())
            {
                var result = MessageBox.Show("¿Está seguro que desea cancelar? Se perderán los datos ingresados.",
                                           "Confirmar Cancelación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.No)
                    return;
            }

            this.Close();
        }

        private bool FormularioTieneDatos()
        {
            return !string.IsNullOrEmpty(ordenFabTextBox.Text) ||
                   !string.IsNullOrEmpty(descripcionTextBox.Text) ||
                   !string.IsNullOrEmpty(upc1TextBox.Text) ||
                   !string.IsNullOrEmpty(observacionesTextBox.Text);
        }

        private void BtnVerMaestro_Click(object sender, EventArgs e)
        {
            // Implementar visualización del maestro de códigos
            MostrarMensaje("Funcionalidad pendiente: Ver maestro de códigos", true);
        }

        private void BtnCrearMaestro_Click(object sender, EventArgs e)
        {
            // Implementar creación de maestro de códigos
            MostrarMensaje("Funcionalidad pendiente: Crear maestro de códigos", true);
        }

        private void ValidarFormulario()
        {
            var esValido = !string.IsNullOrEmpty(ordenFabTextBox.Text.Trim()) &&
                          !string.IsNullOrEmpty(descripcionTextBox.Text.Trim()) &&
                          !string.IsNullOrEmpty(upc1TextBox.Text.Trim()) &&
                          cantidadNumericUpDown.Value > 0;

            btnGuardar.Enabled = esValido;
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

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (FormularioTieneDatos())
            {
                var result = MessageBox.Show("¿Está seguro que desea cerrar? Se perderán los datos no guardados.",
                                           "Confirmar Cierre", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.No)
                {
                    e.Cancel = true;
                    return;
                }
            }

            base.OnFormClosing(e);
        }
    }
}