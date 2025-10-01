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
            tipoEtiquetaFiltroComboBox.Items.Add("MOLDURAS");
            tipoEtiquetaFiltroComboBox.Items.Add("EAN13");
            tipoEtiquetaFiltroComboBox.Items.Add("I2DE5");
            tipoEtiquetaFiltroComboBox.Items.Add("CBCOE");
            tipoEtiquetaFiltroComboBox.Items.Add("DUAL");
            tipoEtiquetaFiltroComboBox.SelectedIndex = 0;

            // Configurar ComboBox de tipo de etiqueta (datos)
            tipoEtiquetaComboBox.Items.Clear();
            tipoEtiquetaComboBox.Items.Add("MOLDURAS");
            tipoEtiquetaComboBox.Items.Add("EAN13");
            tipoEtiquetaComboBox.Items.Add("I2DE5");
            tipoEtiquetaComboBox.Items.Add("CBCOE");
            tipoEtiquetaComboBox.Items.Add("DUAL");
            tipoEtiquetaComboBox.SelectedIndex = 0;

            // Configurar ComboBox de colores
            colorComboBox.Items.Clear();
            colorComboBox.Items.Add("Blancas");
            colorComboBox.Items.Add("Rojas");
            colorComboBox.Items.Add("Bicolor");
            colorComboBox.Items.Add("N/A");
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
                maestrosCodigos = DatabaseService.MaestroCodigosEtiquetas;
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
            if (tipoEtiquetaFiltroComboBox.SelectedIndex == 0) return true; // "Todos"
            var tipoSeleccionado = tipoEtiquetaFiltroComboBox.SelectedItem.ToString();
            return maestro.TipoEtiqueta == tipoSeleccionado;
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
                    row.Cells[1].Value = maestro.UPC1;
                    row.Cells[2].Value = maestro.Descripcion;
                    row.Cells[3].Value = maestro.TipoEtiqueta;
                    row.Cells[4].Value = maestro.ColorEtiqueta;
                    row.Cells[5].Value = maestro.Activo ? "Activo" : "Inactivo";

                    // Colorear filas según estado
                    if (!maestro.Activo)
                    {
                        row.DefaultCellStyle.BackColor = Color.LightGray;
                        row.DefaultCellStyle.ForeColor = Color.DarkGray;
                    }
                    else if (!maestro.ValidarConfiguracion())
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
                var validosImpresion = maestrosFiltrados.Count(m => m.ValidarConfiguracion());

                lblTotalMaestros.Text = total.ToString("N0");
                lblActivos.Text = activos.ToString("N0");
                lblInactivos.Text = inactivos.ToString("N0");
                lblValidosImpresion.Text = validosImpresion.ToString("N0");

                contadorLabel.Text = $"{total:N0} registros";
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
            btnActivar.Enabled = haySeleccion && !esNuevoRegistro;
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
                lblEstado.Text = maestro.Activo ? "Activo" : "Inactivo";
                lblEstado.ForeColor = maestro.Activo ? Color.Green : Color.Red;

                // Mostrar validaciones
                ActualizarValidaciones(maestro);
            }
            catch (Exception ex)
            {
                MostrarError($"Error cargando datos del maestro: {ex.Message}");
            }
        }

        private void ActualizarValidaciones(MaestroCodigoEtiqueta maestro)
        {
            var validaciones = new List<string>();

            if (string.IsNullOrEmpty(maestro.UPC1))
                validaciones.Add("⚠ UPC1 requerido");

            if (string.IsNullOrEmpty(maestro.Descripcion))
                validaciones.Add("⚠ Descripción requerida");

            if (maestro.RequiereLogo && string.IsNullOrEmpty(maestro.NombreLogo))
                validaciones.Add("⚠ Nombre de logo requerido");

            lblValidaciones.Text = validaciones.Count > 0 ? string.Join(", ", validaciones) : "✓ Configuración válida";
            lblValidaciones.ForeColor = validaciones.Count > 0 ? Color.Red : Color.Green;

            // Mostrar ejemplo de UPC
            lblEjemploUPC.Text = $"Ejemplo: {maestro.UPC1}";
        }

        private void BtnNuevo_Click(object sender, EventArgs e)
        {
            esNuevoRegistro = true;
            maestroActual = new MaestroCodigoEtiqueta();
            LimpiarFormulario();
            HabilitarEdicion(true);
            partIdDatoTextBox.Focus();
            statusLabel.Text = "Creando nuevo maestro de códigos";
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidarDatos()) return;

                if (esNuevoRegistro)
                {
                    // Verificar que no exista ya
                    if (maestrosCodigos.Any(m => m.PartId == partIdDatoTextBox.Text.Trim()))
                    {
                        MostrarError("Ya existe un maestro con ese Part ID");
                        return;
                    }

                    maestroActual = new MaestroCodigoEtiqueta();
                    maestroActual.PartId = partIdDatoTextBox.Text.Trim();
                    maestroActual.UsuarioCreacion = Environment.UserName;
                    maestroActual.FechaCreacion = DateTime.Now;
                }

                // Actualizar datos
                maestroActual.Descripcion = descripcionTextBox.Text.Trim();
                maestroActual.UPC1 = upc1TextBox.Text.Trim();
                maestroActual.UPC2 = upc2TextBox.Text.Trim();
                maestroActual.TipoEtiqueta = tipoEtiquetaComboBox.SelectedItem.ToString();
                maestroActual.ColorEtiqueta = colorComboBox.SelectedItem.ToString();
                maestroActual.VelocidadImpresion = (int)velocidadNumericUpDown.Value;
                maestroActual.TemperaturaImpresion = (int)temperaturaNumericUpDown.Value;
                maestroActual.RequiereLogo = requireLogoCheckBox.Checked;
                maestroActual.NombreLogo = logoTextBox.Text.Trim();
                maestroActual.Observaciones = observacionesTextBox.Text.Trim();
                maestroActual.UsuarioModificacion = Environment.UserName;
                maestroActual.FechaModificacion = DateTime.Now;

                // Guardar en base de datos
                if (esNuevoRegistro)
                {
                    DatabaseService.InsertMaestroCodigoEtiqueta(maestroActual);
                    MostrarMensaje("Maestro creado correctamente", false);
                }
                else
                {
                    DatabaseService.UpdateMaestroCodigoEtiqueta(maestroActual);
                    MostrarMensaje("Maestro actualizado correctamente", false);
                }

                esNuevoRegistro = false;
                HabilitarEdicion(false);
                CargarDatos();
            }
            catch (Exception ex)
            {
                MostrarError($"Error guardando maestro: {ex.Message}");
            }
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            esNuevoRegistro = false;
            HabilitarEdicion(false);
            var maestro = ObtenerMaestroSeleccionado();
            if (maestro != null)
            {
                CargarDatosMaestro(maestro);
            }
            else
            {
                LimpiarFormulario();
            }
            statusLabel.Text = "Operación cancelada";
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            var maestro = ObtenerMaestroSeleccionado();
            if (maestro == null) return;

            var result = MessageBox.Show($"¿Está seguro de desactivar el maestro '{maestro.PartId}'?\n\n" +
                                       "Esta acción no elimina el registro, solo lo desactiva.",
                                       "Confirmar Desactivación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    maestro.Desactivar(Environment.UserName, "Desactivado desde formulario");
                    DatabaseService.UpdateMaestroCodigoEtiqueta(maestro);
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

                DatabaseService.UpdateMaestroCodigoEtiqueta(maestro);
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
                var inputForm = new InputDialog("Ingrese el Part ID para el nuevo maestro:", "Clonar Maestro", maestro.PartId + "_COPIA");
                if (inputForm.ShowDialog() == DialogResult.OK)
                {
                    var nuevoPartId = inputForm.InputText.Trim();

                    if (string.IsNullOrEmpty(nuevoPartId))
                    {
                        MostrarError("Debe ingresar un Part ID válido");
                        return;
                    }

                    if (maestrosCodigos.Any(m => m.PartId == nuevoPartId))
                    {
                        MostrarError("Ya existe un maestro con ese Part ID");
                        return;
                    }

                    // Crear clon
                    var clon = new MaestroCodigoEtiqueta
                    {
                        PartId = nuevoPartId,
                        Descripcion = maestro.Descripcion,
                        UPC1 = maestro.UPC1,
                        UPC2 = maestro.UPC2,
                        TipoEtiqueta = maestro.TipoEtiqueta,
                        ColorEtiqueta = maestro.ColorEtiqueta,
                        VelocidadImpresion = maestro.VelocidadImpresion,
                        TemperaturaImpresion = maestro.TemperaturaImpresion,
                        RequiereLogo = maestro.RequiereLogo,
                        NombreLogo = maestro.NombreLogo,
                        Observaciones = $"Clonado de {maestro.PartId}",
                        UsuarioCreacion = Environment.UserName,
                        FechaCreacion = DateTime.Now,
                        Activo = true
                    };

                    DatabaseService.InsertMaestroCodigoEtiqueta(clon);
                    CargarDatos();
                    MostrarMensaje("Maestro clonado correctamente", false);

                    // Seleccionar el nuevo registro
                    SeleccionarMaestro(nuevoPartId);
                }
            }
            catch (Exception ex)
            {
                MostrarError($"Error clonando maestro: {ex.Message}");
            }
        }

        private void BtnValidarUPC_Click(object sender, EventArgs e)
        {
            var upc = upc1TextBox.Text.Trim();
            if (string.IsNullOrEmpty(upc))
            {
                MostrarError("Ingrese un código UPC para validar");
                return;
            }

            // Validación básica
            if (upc.Length < 8 || upc.Length > 14)
            {
                MostrarError("El código UPC debe tener entre 8 y 14 dígitos");
                return;
            }

            if (!upc.All(char.IsDigit))
            {
                MostrarError("El código UPC solo debe contener números");
                return;
            }

            MostrarMensaje("Código UPC válido", false);
        }

        private void BtnGenerarUPC_Click(object sender, EventArgs e)
        {
            try
            {
                var partId = partIdDatoTextBox.Text.Trim();
                if (string.IsNullOrEmpty(partId))
                {
                    MostrarError("Ingrese un Part ID primero");
                    return;
                }

                // Generar UPC basado en Part ID
                var baseCode = partId.Length > 8 ? partId.Substring(0, 8) : partId.PadRight(8, '0');
                var numerosPart = new string(baseCode.Where(char.IsDigit).ToArray());

                if (numerosPart.Length < 6)
                    numerosPart = numerosPart.PadRight(6, '0');

                var codigo = "77" + numerosPart.Substring(0, 6).PadLeft(6, '0');
                var upcGenerado = GenerarDigitoVerificacion(codigo);

                upc1TextBox.Text = upcGenerado;
                lblEjemploUPC.Text = $"Generado: {upcGenerado}";

                MostrarMensaje("Código UPC generado automáticamente", false);
            }
            catch (Exception ex)
            {
                MostrarError($"Error generando UPC: {ex.Message}");
            }
        }

        private void TipoEtiquetaComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Ajustar configuraciones según el tipo de etiqueta
            var tipo = tipoEtiquetaComboBox.SelectedItem?.ToString();

            switch (tipo)
            {
                case "MOLDURAS":
                    colorComboBox.Enabled = true;
                    break;
                case "EAN13":
                case "I2DE5":
                    colorComboBox.SelectedIndex = 3; // N/A
                    colorComboBox.Enabled = false;
                    break;
                case "CBCOE":
                case "DUAL":
                    colorComboBox.SelectedIndex = 0; // Blancas
                    colorComboBox.Enabled = false;
                    break;
            }
        }

        private string GenerarDigitoVerificacion(string codigo)
        {
            var suma = 0;
            for (int i = 0; i < codigo.Length; i++)
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

        private bool ValidarDatos()
        {
            if (string.IsNullOrEmpty(partIdDatoTextBox.Text.Trim()))
            {
                MostrarError("El Part ID es requerido");
                partIdDatoTextBox.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(descripcionTextBox.Text.Trim()))
            {
                MostrarError("La descripción es requerida");
                descripcionTextBox.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(upc1TextBox.Text.Trim()))
            {
                MostrarError("El código UPC1 es requerido");
                upc1TextBox.Focus();
                return false;
            }

            if (requireLogoCheckBox.Checked && string.IsNullOrEmpty(logoTextBox.Text.Trim()))
            {
                MostrarError("El nombre del logo es requerido cuando está marcado");
                logoTextBox.Focus();
                return false;
            }

            return true;
        }

        private MaestroCodigoEtiqueta ObtenerMaestroSeleccionado()
        {
            if (maestrosGrid.SelectedRows.Count > 0)
            {
                return maestrosGrid.SelectedRows[0].Tag as MaestroCodigoEtiqueta;
            }
            return null;
        }

        private void SeleccionarMaestro(string partId)
        {
            for (int i = 0; i < maestrosGrid.Rows.Count; i++)
            {
                var maestro = maestrosGrid.Rows[i].Tag as MaestroCodigoEtiqueta;
                if (maestro?.PartId == partId)
                {
                    maestrosGrid.ClearSelection();
                    maestrosGrid.Rows[i].Selected = true;
                    maestrosGrid.FirstDisplayedScrollingRowIndex = i;
                    break;
                }
            }
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

    // Clase auxiliar para input dialogs
    public partial class InputDialog : Form
    {
        public string InputText { get; private set; }

        private TextBox textBox;
        private Button btnOK;
        private Button btnCancel;

        public InputDialog(string prompt, string title, string defaultValue = "")
        {
            InitializeComponent(prompt, title, defaultValue);
        }

        private void InitializeComponent(string prompt, string title, string defaultValue)
        {
            this.Size = new Size(400, 150);
            this.Text = title;
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            var lblPrompt = new Label
            {
                Text = prompt,
                Location = new Point(12, 15),
                Size = new Size(360, 20)
            };

            textBox = new TextBox
            {
                Location = new Point(12, 40),
                Size = new Size(360, 20),
                Text = defaultValue
            };

            btnOK = new Button
            {
                Text = "Aceptar",
                Location = new Point(215, 70),
                Size = new Size(75, 23),
                DialogResult = DialogResult.OK
            };

            btnCancel = new Button
            {
                Text = "Cancelar",
                Location = new Point(297, 70),
                Size = new Size(75, 23),
                DialogResult = DialogResult.Cancel
            };

            btnOK.Click += (s, e) => { InputText = textBox.Text; };

            this.Controls.AddRange(new Control[] { lblPrompt, textBox, btnOK, btnCancel });
            this.AcceptButton = btnOK;
            this.CancelButton = btnCancel;
        }
    }
}