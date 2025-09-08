using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using EtiquetasApp.Models;
using EtiquetasApp.Services;

namespace EtiquetasApp.Forms
{
    public partial class CodificaEtiquetasForm : Form
    {
        private List<MaestroCodigoEtiqueta> maestrosCodigos;
        private List<MaestroCodigoEtiqueta> maestrosFiltrados;
        private MaestroCodigoEtiqueta maestroActual;
        private bool esNuevoRegistro;

        public CodificaEtiquetasForm()
        {
            InitializeComponent();
            InitializeForm();
        }

        private void InitializeForm()
        {
            maestrosCodigos = new List<MaestroCodigoEtiqueta>();
            maestrosFiltrados = new List<MaestroCodigoEtiqueta>();
            maestroActual = new MaestroCodigoEtiqueta();
            esNuevoRegistro = false;

            ConfigurarEventos();
            ConfigurarControles();
            CargarDatos();
            LimpiarFormulario();
        }

        private void ConfigurarEventos()
        {
            // Eventos de búsqueda
            partIdTextBox.TextChanged += FiltroChanged;
            descripcionFiltroTextBox.TextChanged += FiltroChanged;
            tipoEtiquetaFiltroComboBox.SelectedIndexChanged += FiltroChanged;
            soloActivosCheckBox.CheckedChanged += FiltroChanged;

            // Eventos de la grilla
            maestrosGrid.SelectionChanged += MaestrosGrid_SelectionChanged;
            maestrosGrid.CellDoubleClick += MaestrosGrid_CellDoubleClick;

            // Eventos de botones principales
            btnNuevo.Click += BtnNuevo_Click;
            btnGuardar.Click += BtnGuardar_Click;
            btnCancelar.Click += BtnCancelar_Click;
            btnEliminar.Click += BtnEliminar_Click;
            btnActivar.Click += BtnActivar_Click;
            btnClonar.Click += BtnClonar_Click;

            // Eventos de validación
            partIdDatoTextBox.TextChanged += ValidarFormulario;
            upc1TextBox.TextChanged += ValidarFormulario;
            descripcionTextBox.TextChanged += ValidarFormulario;
            tipoEtiquetaComboBox.SelectedIndexChanged += ValidarFormulario;

            // Eventos específicos
            tipoEtiquetaComboBox.SelectedIndexChanged += TipoEtiquetaComboBox_SelectedIndexChanged;
            btnValidarUPC.Click += BtnValidarUPC_Click;
            btnGenerarUPC.Click += BtnGenerarUPC_Click;
            requireLogoCheckBox.CheckedChanged += RequireLogoCheckBox_CheckedChanged;
        }

        private void ConfigurarControles()
        {
            // Configurar ComboBox de tipo de etiqueta (filtro)
            tipoEtiquetaFiltroComboBox.Items.Add("Todos");
            foreach (TipoEtiqueta tipo in Enum.GetValues(typeof(TipoEtiqueta)))
            {
                tipoEtiquetaFiltroComboBox.Items.Add(tipo.GetDisplayName());
            }
            tipoEtiquetaFiltroComboBox.SelectedIndex = 0;

            // Configurar ComboBox de tipo de etiqueta (datos)
            tipoEtiquetaComboBox.Items.Clear();
            foreach (TipoEtiqueta tipo in Enum.GetValues(typeof(TipoEtiqueta)))
            {
                tipoEtiquetaComboBox.Items.Add(tipo.GetDisplayName());
            }
            tipoEtiquetaComboBox.SelectedIndex = 0;

            // Configurar ComboBox de colores
            colorComboBox.Items.Clear();
            foreach (ColorEtiqueta color in Enum.GetValues(typeof(ColorEtiqueta)))
            {
                colorComboBox.Items.Add(color.GetDisplayName());
            }
            colorComboBox.SelectedIndex = 0;

            // Configurar controles numéricos
            velocidadNumericUpDown.Minimum = 1;
            velocidadNumericUpDown.Maximum = 10;
            velocidadNumericUpDown.Value = 4;

            temperaturaNumericUpDown.Minimum = 1;
            temperaturaNumericUpDown.Maximum = 30;
            temperaturaNumericUpDown.Value = 6;

            // Configurar estados iniciales
            soloActivosCheckBox.Checked = true;
            HabilitarEdicion(false);
        }

        private void CargarDatos()
        {
            try
            {
                maestrosCodigos = DatabaseService.GetMaestroCodigosEtiquetas();
                AplicarFiltros();
                ActualizarEstadisticas();
            }
            catch (Exception ex)
            {
                MostrarError($"Error cargando maestros de códigos: {ex.Message}");
            }
        }

        private void AplicarFiltros()
        {
            try
            {
                maestrosFiltrados = maestrosCodigos.AsQueryable()
                    .Where(m => FiltrarPorPartId(m))
                    .Where(m => FiltrarPorDescripcion(m))
                    .Where(m => FiltrarPorTipo(m))
                    .Where(m => FiltrarPorActivo(m))
                    .OrderBy(m => m.PartId)
                    .ToList();

                CargarGrilla();
                ActualizarEstadisticas();
            }
            catch (Exception ex)
            {
                MostrarError($"Error aplicando filtros: {ex.Message}");
            }
        }

        private bool FiltrarPorPartId(MaestroCodigoEtiqueta maestro)
        {
            if (string.IsNullOrEmpty(partIdTextBox.Text)) return true;
            return maestro.PartId.ToUpper().Contains(partIdTextBox.Text.ToUpper());
        }

        private bool FiltrarPorDescripcion(MaestroCodigoEtiqueta maestro)
        {
            if (string.IsNullOrEmpty(descripcionFiltroTextBox.Text)) return true;
            return maestro.Descripcion.ToUpper().Contains(descripcionFiltroTextBox.Text.ToUpper());
        }

        private bool FiltrarPorTipo(MaestroCodigoEtiqueta maestro)
        {
            if (tipoEtiquetaFiltroComboBox.SelectedIndex <= 0) return true;
            var tipoSeleccionado = tipoEtiquetaFiltroComboBox.SelectedItem.ToString();
            return maestro.TipoEtiquetaDescripcion.Contains(tipoSeleccionado);
        }

        private bool FiltrarPorActivo(MaestroCodigoEtiqueta maestro)
        {
            if (!soloActivosCheckBox.Checked) return true;
            return maestro.Activo;
        }

        private void CargarGrilla()
        {
            try
            {
                maestrosGrid.Rows.Clear();

                foreach (var maestro in maestrosFiltrados)
                {
                    var row = new DataGridViewRow();
                    row.CreateCells(maestrosGrid);

                    row.Cells[0].Value = maestro.PartId;
                    row.Cells[1].Value = maestro.Descripcion;
                    row.Cells[2].Value = maestro.TipoEtiquetaDescripcion;
                    row.Cells[3].Value = maestro.UPC1;
                    row.Cells[4].Value = maestro.UPC2;
                    row.Cells[5].Value = maestro.ColorEtiqueta;
                    row.Cells[6].Value = maestro.EstadoDescripcion;
                    row.Cells[7].Value = maestro.FechaCreacion.ToString("dd/MM/yyyy");
                    row.Cells[8].Value = maestro.UsuarioCreacion ?? "";

                    // Colorear filas según estado
                    if (!maestro.Activo)
                    {
                        row.DefaultCellStyle.BackColor = Color.LightGray;
                        row.DefaultCellStyle.ForeColor = Color.DarkGray;
                    }
                    else if (!maestro.EsValidoParaImpresion)
                    {
                        row.DefaultCellStyle.BackColor = Color.LightYellow;
                    }

                    row.Tag = maestro;
                    maestrosGrid.Rows.Add(row);
                }
            }
            catch (Exception ex)
            {
                MostrarError($"Error cargando grilla: {ex.Message}");
            }
        }

        private void ActualizarEstadisticas()
        {
            try
            {
                var total = maestrosFiltrados.Count;
                var activos = maestrosFiltrados.Count(m => m.Activo);
                var inactivos = maestrosFiltrados.Count(m => !m.Activo);
                var validosImpresion = maestrosFiltrados.Count(m => m.EsValidoParaImpresion);

                lblTotalMaestros.Text = total.ToString("N0");
                lblActivos.Text = activos.ToString("N0");
                lblInactivos.Text = inactivos.ToString("N0");
                lblValidosImpresion.Text = validosImpresion.ToString("N0");

                statusLabel.Text = $"Mostrando {total:N0} maestros | {activos:N0} activos | {validosImpresion:N0} válidos para impresión";
            }
            catch (Exception ex)
            {
                MostrarError($"Error actualizando estadísticas: {ex.Message}");
            }
        }

        private void FiltroChanged(object sender, EventArgs e)
        {
            AplicarFiltros();
        }

        private void MaestrosGrid_SelectionChanged(object sender, EventArgs e)
        {
            var maestro = ObtenerMaestroSeleccionado();
            ActualizarBotones(maestro);

            if (maestro != null && !esNuevoRegistro)
            {
                CargarDatosMaestro(maestro);
            }
        }

        private void ActualizarBotones(MaestroCodigoEtiqueta maestro)
        {
            bool haySeleccion = maestro != null;

            btnEliminar.Enabled = haySeleccion && maestro.Activo && !esNuevoRegistro;
            btnActivar.Enabled = haySeleccion && !maestro.Activo && !esNuevoRegistro;
            btnClonar.Enabled = haySeleccion && !esNuevoRegistro;

            btnActivar.Text = (maestro != null && maestro.Activo) ? "Desactivar" : "Activar";
        }

        private void CargarDatosMaestro(MaestroCodigoEtiqueta maestro)
        {
            try
            {
                maestroActual = maestro;

                partIdDatoTextBox.Text = maestro.PartId;
                descripcionTextBox.Text = maestro.Descripcion;
                upc1TextBox.Text = maestro.UPC1;
                upc2TextBox.Text = maestro.UPC2;
                observacionesTextBox.Text = maestro.Observaciones;

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

                velocidadNumericUpDown.Value = maestro.VelocidadImpresion;
                temperaturaNumericUpDown.Value = maestro.TemperaturaImpresion;
                requireLogoCheckBox.Checked = maestro.RequiereLogo;
                logoTextBox.Text = maestro.NombreLogo ?? "";

                // Mostrar información adicional
                lblFechaCreacion.Text = maestro.FechaCreacion.ToString("dd/MM/yyyy HH:mm");
                lblUsuarioCreacion.Text = maestro.UsuarioCreacion ?? "";
                lblFechaModificacion.Text = maestro.FechaModificacion?.ToString("dd/MM/yyyy HH:mm") ?? "";
                lblEstado.Text = maestro.EstadoDescripcion;
                lblEstado.ForeColor = maestro.Activo ? Color.DarkGreen : Color.Red;

                ActualizarInformacionValidacion(maestro);
            }
            catch (Exception ex)
            {
                MostrarError($"Error cargando datos del maestro: {ex.Message}");
            }
        }

        private void ActualizarInformacionValidacion(MaestroCodigoEtiqueta maestro)
        {
            var validaciones = new List<string>();

            if (maestro.ValidarUPC())
                validaciones.Add("✓ Códigos UPC válidos");
            else
                validaciones.Add("⚠ Códigos UPC inválidos");

            if (maestro.ValidarConfiguracion())
                validaciones.Add("✓ Configuración válida");
            else
                validaciones.Add("⚠ Configuración inválida");

            if (maestro.EsValidoParaImpresion)
                validaciones.Add("✓ Listo para impresión");
            else
                validaciones.Add("⚠ No listo para impresión");

            lblValidaciones.Text = string.Join("\n", validaciones);
        }

        private void BtnNuevo_Click(object sender, EventArgs e)
        {
            try
            {
                esNuevoRegistro = true;
                maestroActual = new MaestroCodigoEtiqueta();
                LimpiarFormulario();
                HabilitarEdicion(true);
                partIdDatoTextBox.Focus();
                statusLabel.Text = "Creando nuevo maestro de códigos";
            }
            catch (Exception ex)
            {
                MostrarError($"Error iniciando nuevo registro: {ex.Message}");
            }
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidarDatosCompletos())
                {
                    GuardarMaestro();
                }
            }
            catch (Exception ex)
            {
                MostrarError($"Error guardando maestro: {ex.Message}");
            }
        }

        private bool ValidarDatosCompletos()
        {
            var errores = new List<string>();

            if (string.IsNullOrEmpty(partIdDatoTextBox.Text.Trim()))
                errores.Add("El Part ID es requerido");

            if (string.IsNullOrEmpty(descripcionTextBox.Text.Trim()))
                errores.Add("La descripción es requerida");

            if (string.IsNullOrEmpty(upc1TextBox.Text.Trim()))
                errores.Add("El código UPC1 es requerido");

            // Validar si ya existe el Part ID (solo para nuevos)
            if (esNuevoRegistro)
            {
                var existe = maestrosCodigos.Any(m => m.PartId.Equals(partIdDatoTextBox.Text.Trim(), StringComparison.OrdinalIgnoreCase));
                if (existe)
                    errores.Add("Ya existe un maestro con este Part ID");
            }

            // Validaciones específicas por tipo
            var tipo = ObtenerTipoEtiquetaSeleccionado();
            if (tipo.RequiereDualUPC() && string.IsNullOrEmpty(upc2TextBox.Text.Trim()))
                errores.Add("El tipo DUAL requiere UPC2");

            if (tipo == TipoEtiqueta.EAN13 && !ValidarEAN13(upc1TextBox.Text.Trim()))
                errores.Add("El código EAN13 debe tener 13 dígitos numéricos");

            if (requireLogoCheckBox.Checked && string.IsNullOrEmpty(logoTextBox.Text.Trim()))
                errores.Add("Debe especificar el nombre del logo");

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

        private TipoEtiqueta ObtenerTipoEtiquetaSeleccionado()
        {
            if (tipoEtiquetaComboBox.SelectedIndex >= 0)
            {
                var tipoText = tipoEtiquetaComboBox.SelectedItem.ToString();
                return EnumExtensions.ParseTipoEtiqueta(tipoText);
            }
            return TipoEtiqueta.CBCOE;
        }

        private void GuardarMaestro()
        {
            try
            {
                // Actualizar datos del maestro actual
                maestroActual.PartId = partIdDatoTextBox.Text.Trim();
                maestroActual.Descripcion = descripcionTextBox.Text.Trim();
                maestroActual.UPC1 = upc1TextBox.Text.Trim();
                maestroActual.UPC2 = upc2TextBox.Text.Trim();
                maestroActual.TipoEtiqueta = ObtenerTipoEtiquetaSeleccionado().ToCodeString();
                maestroActual.ColorEtiqueta = colorComboBox.SelectedItem?.ToString() ?? "Blancas";
                maestroActual.VelocidadImpresion = (int)velocidadNumericUpDown.Value;
                maestroActual.TemperaturaImpresion = (int)temperaturaNumericUpDown.Value;
                maestroActual.RequiereLogo = requireLogoCheckBox.Checked;
                maestroActual.NombreLogo = requireLogoCheckBox.Checked ? logoTextBox.Text.Trim() : "";
                maestroActual.Observaciones = observacionesTextBox.Text.Trim();

                if (esNuevoRegistro)
                {
                    maestroActual.UsuarioCreacion = Environment.UserName;
                    maestroActual.FechaCreacion = DateTime.Now;
                }
                else
                {
                    maestroActual.UsuarioModificacion = Environment.UserName;
                    maestroActual.FechaModificacion = DateTime.Now;
                }

                // Aquí se guardaría en la base de datos
                // var resultado = DatabaseService.SaveMaestroCodigoEtiqueta(maestroActual);

                // Simular guardado exitoso
                if (esNuevoRegistro)
                {
                    maestrosCodigos.Add(maestroActual);
                }

                esNuevoRegistro = false;
                HabilitarEdicion(false);
                CargarDatos();

                MostrarMensaje("Maestro guardado correctamente", false);
                statusLabel.Text = "Maestro guardado correctamente";
            }
            catch (Exception ex)
            {
                MostrarError($"Error guardando maestro: {ex.Message}");
            }
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            if (esNuevoRegistro || FormularioModificado())
            {
                var result = MessageBox.Show("¿Está seguro que desea cancelar? Se perderán los cambios no guardados.",
                                           "Confirmar Cancelación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.No)
                    return;
            }

            CancelarEdicion();
        }

        private void CancelarEdicion()
        {
            esNuevoRegistro = false;
            HabilitarEdicion(false);

            var maestroSeleccionado = ObtenerMaestroSeleccionado();
            if (maestroSeleccionado != null)
            {
                CargarDatosMaestro(maestroSeleccionado);
            }
            else
            {
                LimpiarFormulario();
            }

            statusLabel.Text = "Edición cancelada";
        }

        private bool FormularioModificado()
        {
            if (maestroActual == null) return false;

            return maestroActual.PartId != partIdDatoTextBox.Text.Trim() ||
                   maestroActual.Descripcion != descripcionTextBox.Text.Trim() ||
                   maestroActual.UPC1 != upc1TextBox.Text.Trim() ||
                   maestroActual.UPC2 != upc2TextBox.Text.Trim() ||
                   maestroActual.Observaciones != observacionesTextBox.Text.Trim();
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            var maestro = ObtenerMaestroSeleccionado();
            if (maestro == null) return;

            var result = MessageBox.Show($"¿Está seguro que desea desactivar el maestro '{maestro.PartId}'?",
                                       "Confirmar Desactivación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    maestro.Desactivar(Environment.UserName, "Desactivado desde formulario");
                    CargarDatos();
                    MostrarMensaje("Maestro desactivado correctamente", false);
                }
                catch (Exception ex)
                {
                    MostrarError($"Error desactivando maestro: {ex.Message}");
                }
            }
        }

        private void BtnActivar_Click(object sender, EventArgs e)
        {
            var maestro = ObtenerMaestroSeleccionado();
            if (maestro == null) return;

            try
            {
                if (maestro.Activo)
                {
                    maestro.Desactivar(Environment.UserName);
                    MostrarMensaje("Maestro desactivado correctamente", false);
                }
                else
                {
                    maestro.Activar(Environment.UserName);
                    MostrarMensaje("Maestro activado correctamente", false);
                }

                CargarDatos();
            }
            catch (Exception ex)
            {
                MostrarError($"Error cambiando estado del maestro: {ex.Message}");
            }
        }

        private void BtnClonar_Click(object sender, EventArgs e)
        {
            var maestro = ObtenerMaestroSeleccionado();
            if (maestro == null) return;

            try
            {
                var nuevoPartId = Microsoft.VisualBasic.Interaction.InputBox(
                    "Ingrese el Part ID para el nuevo maestro:",
                    "Clonar Maestro",
                    maestro.PartId + "_COPIA");

                if (!string.IsNullOrEmpty(nuevoPartId))
                {
                    var existe = maestrosCodigos.Any(m => m.PartId.Equals(nuevoPartId, StringComparison.OrdinalIgnoreCase));
                    if (existe)
                    {
                        MostrarError("Ya existe un maestro con ese Part ID");
                        return;
                    }

                    var clon = maestro.ClonarPara(nuevoPartId, Environment.UserName);
                    maestrosCodigos.Add(clon);
                    CargarDatos();

                    // Seleccionar el nuevo registro
                    SeleccionarMaestro(nuevoPartId);

                    MostrarMensaje("Maestro clonado correctamente", false);
                }
            }
            catch (Exception ex)
            {
                MostrarError($"Error clonando maestro: {ex.Message}");
            }
        }

        private void SeleccionarMaestro(string partId)
        {
            foreach (DataGridViewRow row in maestrosGrid.Rows)
            {
                var maestro = row.Tag as MaestroCodigoEtiqueta;
                if (maestro != null && maestro.PartId.Equals(partId, StringComparison.OrdinalIgnoreCase))
                {
                    row.Selected = true;
                    maestrosGrid.CurrentCell = row.Cells[0];
                    break;
                }
            }
        }

        private void TipoEtiquetaComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var tipo = ObtenerTipoEtiquetaSeleccionado();

            // Habilitar/deshabilitar UPC2 según el tipo
            upc2TextBox.Enabled = tipo.RequiereDualUPC();
            lblUPC2.Enabled = tipo.RequiereDualUPC();

            if (tipo.RequiereDualUPC())
            {
                lblUPC2.Font = new Font(lblUPC2.Font, FontStyle.Bold);
                lblUPC2.ForeColor = Color.DarkBlue;
            }
            else
            {
                lblUPC2.Font = new Font(lblUPC2.Font, FontStyle.Regular);
                lblUPC2.ForeColor = SystemColors.ControlText;
                upc2TextBox.Clear();
            }

            // Actualizar ejemplo de UPC
            ActualizarEjemploUPC(tipo);
            ValidarFormulario(sender, e);
        }

        private void ActualizarEjemploUPC(TipoEtiqueta tipo)
        {
            switch (tipo)
            {
                case TipoEtiqueta.EAN13:
                    lblEjemploUPC.Text = "Ejemplo: 1234567890123 (13 dígitos)";
                    break;
                case TipoEtiqueta.DUAL:
                    lblEjemploUPC.Text = "Ejemplo UPC1: ABC123, UPC2: DEF456";
                    break;
                default:
                    lblEjemploUPC.Text = "Ejemplo: ABC123456";
                    break;
            }
        }

        private void BtnValidarUPC_Click(object sender, EventArgs e)
        {
            try
            {
                var tipo = ObtenerTipoEtiquetaSeleccionado();
                var upc1 = upc1TextBox.Text.Trim();
                var upc2 = upc2TextBox.Text.Trim();

                var validaciones = new List<string>();

                if (string.IsNullOrEmpty(upc1))
                {
                    validaciones.Add("UPC1 es requerido");
                }
                else
                {
                    switch (tipo)
                    {
                        case TipoEtiqueta.EAN13:
                            if (ValidarEAN13(upc1))
                                validaciones.Add("✓ EAN13 válido");
                            else
                                validaciones.Add("⚠ EAN13 inválido (debe tener 13 dígitos)");
                            break;
                        default:
                            validaciones.Add("✓ UPC1 formato aceptable");
                            break;
                    }
                }

                if (tipo.RequiereDualUPC())
                {
                    if (string.IsNullOrEmpty(upc2))
                        validaciones.Add("⚠ UPC2 es requerido para tipo DUAL");
                    else
                        validaciones.Add("✓ UPC2 presente");
                }

                var mensaje = string.Join("\n", validaciones);
                MostrarMensaje(mensaje, validaciones.Any(v => v.Contains("⚠")));
            }
            catch (Exception ex)
            {
                MostrarError($"Error validando UPC: {ex.Message}");
            }
        }

        private void BtnGenerarUPC_Click(object sender, EventArgs e)
        {
            try
            {
                var tipo = ObtenerTipoEtiquetaSeleccionado();
                var partId = partIdDatoTextBox.Text.Trim();

                if (string.IsNullOrEmpty(partId))
                {
                    MostrarError("Ingrese primero el Part ID");
                    return;
                }

                switch (tipo)
                {
                    case TipoEtiqueta.EAN13:
                        upc1TextBox.Text = GenerarEAN13(partId);
                        break;
                    case TipoEtiqueta.DUAL:
                        upc1TextBox.Text = $"{partId}_A";
                        upc2TextBox.Text = $"{partId}_B";
                        break;
                    default:
                        upc1TextBox.Text = partId.Length > 10 ? partId.Substring(0, 10) : partId;
                        break;
                }

                MostrarMensaje("Códigos UPC generados automáticamente. Revise y modifique si es necesario.", true);
            }
            catch (Exception ex)
            {
                MostrarError($"Error generando UPC: {ex.Message}");
            }
        }

        private string GenerarEAN13(string partId)
        {
            // Generar un EAN13 básico basado en el PartId
            var codigo = "123" + partId.PadRight(9, '0').Substring(0, 9);
            if (codigo.Length > 12)
                codigo = codigo.Substring(0, 12);
            else
                codigo = codigo.PadRight(12, '0');

            // Calcular dígito de verificación (simplificado)
            var suma = 0;
            for (int i = 0; i < 12; i++)
            {
                var digito = int.Parse(codigo[i].ToString());
                suma += (i % 2 == 0) ? digito : digito * 3;
            }
            var digitoVerificacion = (10 - (suma % 10)) % 10;

            return codigo + digitoVerificacion;
        }

        private void RequireLogoCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            logoTextBox.Enabled = requireLogoCheckBox.Checked;
            lblLogo.Enabled = requireLogoCheckBox.Checked;

            if (!requireLogoCheckBox.Checked)
            {
                logoTextBox.Clear();
            }
            else if (string.IsNullOrEmpty(logoTextBox.Text))
            {
                logoTextBox.Text = "PINO"; // Logo por defecto
            }
        }

        private void ValidarFormulario(object sender, EventArgs e)
        {
            if (!esNuevoRegistro) return;

            var esValido = !string.IsNullOrEmpty(partIdDatoTextBox.Text.Trim()) &&
                          !string.IsNullOrEmpty(descripcionTextBox.Text.Trim()) &&
                          !string.IsNullOrEmpty(upc1TextBox.Text.Trim());

            btnGuardar.Enabled = esValido;
        }

        private void MaestrosGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && !esNuevoRegistro)
            {
                HabilitarEdicion(true);
                statusLabel.Text = "Editando maestro de códigos";
            }
        }

        private void HabilitarEdicion(bool habilitar)
        {
            // Habilitar/deshabilitar controles de edición
            partIdDatoTextBox.ReadOnly = !habilitar || !esNuevoRegistro; // Part ID solo editable en nuevos
            descripcionTextBox.ReadOnly = !habilitar;
            upc1TextBox.ReadOnly = !habilitar;
            upc2TextBox.ReadOnly = !habilitar;
            observacionesTextBox.ReadOnly = !habilitar;

            tipoEtiquetaComboBox.Enabled = habilitar;
            colorComboBox.Enabled = habilitar;
            velocidadNumericUpDown.Enabled = habilitar;
            temperaturaNumericUpDown.Enabled = habilitar;
            requireLogoCheckBox.Enabled = habilitar;
            logoTextBox.Enabled = habilitar && requireLogoCheckBox.Checked;

            btnValidarUPC.Enabled = habilitar;
            btnGenerarUPC.Enabled = habilitar;

            // Cambiar colores de fondo
            var colorFondo = habilitar ? SystemColors.Window : SystemColors.Control;
            partIdDatoTextBox.BackColor = colorFondo;
            descripcionTextBox.BackColor = colorFondo;
            upc1TextBox.BackColor = colorFondo;
            upc2TextBox.BackColor = colorFondo;
            observacionesTextBox.BackColor = colorFondo;

            // Habilitar/deshabilitar botones
            btnGuardar.Enabled = habilitar;
            btnCancelar.Enabled = habilitar;
            btnNuevo.Enabled = !habilitar;

            // Habilitar/deshabilitar grilla
            maestrosGrid.Enabled = !habilitar;
        }

        private void LimpiarFormulario()
        {
            partIdDatoTextBox.Clear();
            descripcionTextBox.Clear();
            upc1TextBox.Clear();
            upc2TextBox.Clear();
            observacionesTextBox.Clear();
            logoTextBox.Clear();

            tipoEtiquetaComboBox.SelectedIndex = 0;
            colorComboBox.SelectedIndex = 0;
            velocidadNumericUpDown.Value = 4;
            temperaturaNumericUpDown.Value = 6;
            requireLogoCheckBox.Checked = false;

            lblFechaCreacion.Text = "";
            lblUsuarioCreacion.Text = "";
            lblFechaModificacion.Text = "";
            lblEstado.Text = "";
            lblValidaciones.Text = "";
            lblEjemploUPC.Text = "";
        }

        private MaestroCodigoEtiqueta ObtenerMaestroSeleccionado()
        {
            if (maestrosGrid.SelectedRows.Count > 0)
            {
                return maestrosGrid.SelectedRows[0].Tag as MaestroCodigoEtiqueta;
            }
            return null;
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

        // Método público para pre-cargar un Part ID
        public void CargarPartId(string partId)
        {
            if (!string.IsNullOrEmpty(partId))
            {
                partIdTextBox.Text = partId;
                AplicarFiltros();
                SeleccionarMaestro(partId);
            }
        }
    }
}