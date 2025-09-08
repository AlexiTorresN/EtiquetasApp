using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using EtiquetasApp.Models;
using EtiquetasApp.Services;

namespace EtiquetasApp.Forms
{
    public partial class ConsultaSolicitudesForm : Form
    {
        private List<SolicitudEtiqueta> solicitudes;
        private List<SolicitudEtiqueta> solicitudesFiltradas;

        public ConsultaSolicitudesForm()
        {
            InitializeComponent();
            InitializeForm();
        }

        private void InitializeForm()
        {
            solicitudes = new List<SolicitudEtiqueta>();
            solicitudesFiltradas = new List<SolicitudEtiqueta>();

            ConfigurarEventos();
            CargarDatos();
            ConfigurarFiltros();
        }

        private void ConfigurarEventos()
        {
            // Eventos de filtros
            tipoEtiquetaComboBox.SelectedIndexChanged += FiltroChanged;
            estadoComboBox.SelectedIndexChanged += FiltroChanged;
            fechaDesdeDateTime.ValueChanged += FiltroChanged;
            fechaHastaDateTime.ValueChanged += FiltroChanged;
            ordenFabTextBox.TextChanged += FiltroChanged;
            descripcionTextBox.TextChanged += FiltroChanged;

            // Eventos de botones
            btnBuscar.Click += BtnBuscar_Click;
            btnLimpiar.Click += BtnLimpiar_Click;
            btnExportar.Click += BtnExportar_Click;
            btnImprimir.Click += BtnImprimir_Click;
            btnEliminar.Click += BtnEliminar_Click;
            btnReactivar.Click += BtnReactivar_Click;

            // Eventos de grilla
            solicitudesGrid.SelectionChanged += SolicitudesGrid_SelectionChanged;
            solicitudesGrid.CellDoubleClick += SolicitudesGrid_CellDoubleClick;
            solicitudesGrid.CellFormatting += SolicitudesGrid_CellFormatting;
        }

        private void ConfigurarFiltros()
        {
            // Configurar ComboBox de tipo de etiqueta
            tipoEtiquetaComboBox.Items.Add("Todos");
            foreach (TipoEtiqueta tipo in Enum.GetValues(typeof(TipoEtiqueta)))
            {
                tipoEtiquetaComboBox.Items.Add(tipo.GetDisplayName());
            }
            tipoEtiquetaComboBox.SelectedIndex = 0;

            // Configurar ComboBox de estado
            estadoComboBox.Items.Add("Todos");
            foreach (EstadoSolicitud estado in Enum.GetValues(typeof(EstadoSolicitud)))
            {
                estadoComboBox.Items.Add(estado.GetDisplayName());
            }
            estadoComboBox.SelectedIndex = 0;

            // Configurar fechas
            fechaDesdeDateTime.Value = DateTime.Now.AddMonths(-1);
            fechaHastaDateTime.Value = DateTime.Now.AddDays(7);
        }

        private void CargarDatos()
        {
            try
            {
                solicitudes = DatabaseService.GetSolicitudesEtiquetas();
                AplicarFiltros();
                ActualizarEstadisticas();
            }
            catch (Exception ex)
            {
                MostrarError($"Error cargando solicitudes: {ex.Message}");
            }
        }

        private void AplicarFiltros()
        {
            try
            {
                solicitudesFiltradas = solicitudes.AsQueryable()
                    .Where(s => FiltrarPorTipo(s))
                    .Where(s => FiltrarPorEstado(s))
                    .Where(s => FiltrarPorFechas(s))
                    .Where(s => FiltrarPorOrdenFab(s))
                    .Where(s => FiltrarPorDescripcion(s))
                    .OrderByDescending(s => s.FechaRequerida)
                    .ToList();

                CargarGrilla();
                ActualizarEstadisticas();
            }
            catch (Exception ex)
            {
                MostrarError($"Error aplicando filtros: {ex.Message}");
            }
        }

        private bool FiltrarPorTipo(SolicitudEtiqueta solicitud)
        {
            if (tipoEtiquetaComboBox.SelectedIndex <= 0) return true;

            var tipoSeleccionado = tipoEtiquetaComboBox.SelectedItem.ToString();
            return string.IsNullOrEmpty(solicitud.TipoEtiqueta) ||
                   solicitud.TipoEtiqueta.Contains(tipoSeleccionado) ||
                   tipoSeleccionado == "Todos";
        }

        private bool FiltrarPorEstado(SolicitudEtiqueta solicitud)
        {
            if (estadoComboBox.SelectedIndex <= 0) return true;

            var estadoSeleccionado = estadoComboBox.SelectedItem.ToString();
            return solicitud.EstadoSolicitud == estadoSeleccionado;
        }

        private bool FiltrarPorFechas(SolicitudEtiqueta solicitud)
        {
            return solicitud.FechaRequerida.Date >= fechaDesdeDateTime.Value.Date &&
                   solicitud.FechaRequerida.Date <= fechaHastaDateTime.Value.Date;
        }

        private bool FiltrarPorOrdenFab(SolicitudEtiqueta solicitud)
        {
            if (string.IsNullOrEmpty(ordenFabTextBox.Text)) return true;

            return solicitud.OrdenFab.ToUpper().Contains(ordenFabTextBox.Text.ToUpper());
        }

        private bool FiltrarPorDescripcion(SolicitudEtiqueta solicitud)
        {
            if (string.IsNullOrEmpty(descripcionTextBox.Text)) return true;

            return solicitud.Descripcion.ToUpper().Contains(descripcionTextBox.Text.ToUpper());
        }

        private void CargarGrilla()
        {
            try
            {
                solicitudesGrid.Rows.Clear();

                foreach (var solicitud in solicitudesFiltradas)
                {
                    var row = new DataGridViewRow();
                    row.CreateCells(solicitudesGrid);

                    row.Cells[0].Value = solicitud.IdSolicitud;
                    row.Cells[1].Value = solicitud.OrdenFab;
                    row.Cells[2].Value = solicitud.Descripcion;
                    row.Cells[3].Value = solicitud.TipoEtiqueta;
                    row.Cells[4].Value = solicitud.CantidadPedida;
                    row.Cells[5].Value = solicitud.CantidadFabricada;
                    row.Cells[6].Value = solicitud.CantidadPendiente;
                    row.Cells[7].Value = solicitud.Color;
                    row.Cells[8].Value = solicitud.EstadoSolicitud;
                    row.Cells[9].Value = solicitud.FechaSolicitud.ToString("dd/MM/yyyy");
                    row.Cells[10].Value = solicitud.FechaRequerida.ToString("dd/MM/yyyy");
                    row.Cells[11].Value = solicitud.FechaFabricacion?.ToString("dd/MM/yyyy") ?? "";
                    row.Cells[12].Value = solicitud.UsuarioSolicita ?? "";
                    row.Cells[13].Value = solicitud.Observaciones;

                    // Colorear filas según estado y urgencia
                    ConfigurarColorFila(row, solicitud);

                    row.Tag = solicitud;
                    solicitudesGrid.Rows.Add(row);
                }
            }
            catch (Exception ex)
            {
                MostrarError($"Error cargando grilla: {ex.Message}");
            }
        }

        private void ConfigurarColorFila(DataGridViewRow row, SolicitudEtiqueta solicitud)
        {
            if (!solicitud.Activo)
            {
                row.DefaultCellStyle.BackColor = Color.LightGray;
                row.DefaultCellStyle.ForeColor = Color.DarkGray;
            }
            else if (solicitud.EstaCompleta)
            {
                row.DefaultCellStyle.BackColor = Color.LightGreen;
            }
            else if (solicitud.EsUrgente)
            {
                row.DefaultCellStyle.BackColor = Color.LightCoral;
            }
            else if (solicitud.DiasParaVencimiento <= 3)
            {
                row.DefaultCellStyle.BackColor = Color.LightYellow;
            }
        }

        private void ActualizarEstadisticas()
        {
            try
            {
                var total = solicitudesFiltradas.Count;
                var completadas = solicitudesFiltradas.Count(s => s.EstaCompleta);
                var pendientes = solicitudesFiltradas.Count(s => !s.EstaCompleta && s.Activo);
                var urgentes = solicitudesFiltradas.Count(s => s.EsUrgente);
                var totalEtiquetas = solicitudesFiltradas.Sum(s => s.CantidadPedida);
                var etiquetasFabricadas = solicitudesFiltradas.Sum(s => s.CantidadFabricada);
                var etiquetasPendientes = solicitudesFiltradas.Sum(s => s.CantidadPendiente);

                lblTotal.Text = total.ToString("N0");
                lblCompletadas.Text = completadas.ToString("N0");
                lblPendientes.Text = pendientes.ToString("N0");
                lblUrgentes.Text = urgentes.ToString("N0");
                lblTotalEtiquetas.Text = totalEtiquetas.ToString("N0");
                lblEtiquetasFabricadas.Text = etiquetasFabricadas.ToString("N0");
                lblEtiquetasPendientes.Text = etiquetasPendientes.ToString("N0");

                // Calcular porcentaje de avance
                var porcentajeAvance = totalEtiquetas > 0 ? (etiquetasFabricadas * 100.0 / totalEtiquetas) : 0;
                lblPorcentajeAvance.Text = $"{porcentajeAvance:F1}%";

                statusLabel.Text = $"Mostrando {total:N0} solicitudes | {pendientes:N0} pendientes | {urgentes:N0} urgentes";
            }
            catch (Exception ex)
            {
                MostrarError($"Error actualizando estadísticas: {ex.Message}");
            }
        }

        private void FiltroChanged(object sender, EventArgs e)
        {
            // Aplicar filtros automáticamente al cambiar cualquier filtro
            if (autoFiltrarCheckBox.Checked)
            {
                AplicarFiltros();
            }
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            AplicarFiltros();
        }

        private void BtnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarFiltros();
        }

        private void LimpiarFiltros()
        {
            tipoEtiquetaComboBox.SelectedIndex = 0;
            estadoComboBox.SelectedIndex = 0;
            fechaDesdeDateTime.Value = DateTime.Now.AddMonths(-1);
            fechaHastaDateTime.Value = DateTime.Now.AddDays(7);
            ordenFabTextBox.Clear();
            descripcionTextBox.Clear();

            AplicarFiltros();
        }

        private void BtnExportar_Click(object sender, EventArgs e)
        {
            try
            {
                using (var saveDialog = new SaveFileDialog())
                {
                    saveDialog.Filter = "Archivos CSV|*.csv|Archivos Excel|*.xlsx";
                    saveDialog.Title = "Exportar Solicitudes";
                    saveDialog.FileName = $"Solicitudes_Etiquetas_{DateTime.Now:yyyyMMdd_HHmmss}";

                    if (saveDialog.ShowDialog() == DialogResult.OK)
                    {
                        ExportarDatos(saveDialog.FileName);
                        MostrarMensaje("Datos exportados correctamente", false);
                    }
                }
            }
            catch (Exception ex)
            {
                MostrarError($"Error exportando datos: {ex.Message}");
            }
        }

        private void ExportarDatos(string archivo)
        {
            // Implementar exportación según extensión del archivo
            var extension = System.IO.Path.GetExtension(archivo).ToLower();

            if (extension == ".csv")
            {
                ExportarCSV(archivo);
            }
            else if (extension == ".xlsx")
            {
                ExportarExcel(archivo);
            }
        }

        private void ExportarCSV(string archivo)
        {
            using (var writer = new System.IO.StreamWriter(archivo, false, System.Text.Encoding.UTF8))
            {
                // Escribir encabezados
                writer.WriteLine("ID,Orden Fab,Descripción,Tipo,Cant. Pedida,Cant. Fabricada,Cant. Pendiente,Color,Estado,Fecha Solicitud,Fecha Requerida,Fecha Fabricación,Usuario,Observaciones");

                // Escribir datos
                foreach (var solicitud in solicitudesFiltradas)
                {
                    writer.WriteLine($"{solicitud.IdSolicitud},{solicitud.OrdenFab},\"{solicitud.Descripcion}\",{solicitud.TipoEtiqueta},{solicitud.CantidadPedida},{solicitud.CantidadFabricada},{solicitud.CantidadPendiente},{solicitud.Color},{solicitud.EstadoSolicitud},{solicitud.FechaSolicitud:dd/MM/yyyy},{solicitud.FechaRequerida:dd/MM/yyyy},{solicitud.FechaFabricacion?.ToString("dd/MM/yyyy") ?? ""},{solicitud.UsuarioSolicita},\"{solicitud.Observaciones}\"");
                }
            }
        }

        private void ExportarExcel(string archivo)
        {
            // Implementación básica de exportación a Excel
            // En una implementación real, usarías una librería como EPPlus o ClosedXML
            MostrarMensaje("Exportación a Excel no implementada. Use formato CSV.", true);
        }

        private void BtnImprimir_Click(object sender, EventArgs e)
        {
            var solicitudSeleccionada = ObtenerSolicitudSeleccionada();
            if (solicitudSeleccionada == null)
            {
                MostrarError("Debe seleccionar una solicitud para imprimir");
                return;
            }

            try
            {
                // Abrir formulario de impresión según el tipo de etiqueta
                AbrirFormularioImpresion(solicitudSeleccionada);
            }
            catch (Exception ex)
            {
                MostrarError($"Error abriendo formulario de impresión: {ex.Message}");
            }
        }

        private void AbrirFormularioImpresion(SolicitudEtiqueta solicitud)
        {
            Form formularioImpresion = null;

            switch (solicitud.TipoEtiqueta?.ToUpper())
            {
                case "CBCOE":
                case "C/BCO-E":
                    formularioImpresion = new EtiquetasCBCOEForm();
                    break;
                case "DUAL":
                    formularioImpresion = new EtiquetasDualForm();
                    break;
                case "EAN13":
                    formularioImpresion = new EtiquetasEAN13Form();
                    break;
                default:
                    formularioImpresion = new EtiquetasCBCOEForm(); // Por defecto
                    break;
            }

            if (formularioImpresion != null)
            {
                formularioImpresion.MdiParent = this.MdiParent;
                formularioImpresion.WindowState = FormWindowState.Maximized;
                formularioImpresion.Show();
            }
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            var solicitudSeleccionada = ObtenerSolicitudSeleccionada();
            if (solicitudSeleccionada == null)
            {
                MostrarError("Debe seleccionar una solicitud para eliminar");
                return;
            }

            if (MessageBox.Show($"¿Está seguro que desea eliminar la solicitud {solicitudSeleccionada.IdSolicitud}?",
                               "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    solicitudSeleccionada.DesactivarSolicitud();
                    // Aquí iría la actualización en base de datos
                    // DatabaseService.UpdateSolicitudEtiqueta(solicitudSeleccionada);

                    CargarDatos();
                    MostrarMensaje("Solicitud eliminada correctamente", false);
                }
                catch (Exception ex)
                {
                    MostrarError($"Error eliminando solicitud: {ex.Message}");
                }
            }
        }

        private void BtnReactivar_Click(object sender, EventArgs e)
        {
            var solicitudSeleccionada = ObtenerSolicitudSeleccionada();
            if (solicitudSeleccionada == null)
            {
                MostrarError("Debe seleccionar una solicitud para reactivar");
                return;
            }

            try
            {
                solicitudSeleccionada.ReactivarSolicitud();
                // Aquí iría la actualización en base de datos
                // DatabaseService.UpdateSolicitudEtiqueta(solicitudSeleccionada);

                CargarDatos();
                MostrarMensaje("Solicitud reactivada correctamente", false);
            }
            catch (Exception ex)
            {
                MostrarError($"Error reactivando solicitud: {ex.Message}");
            }
        }

        private void SolicitudesGrid_SelectionChanged(object sender, EventArgs e)
        {
            var solicitud = ObtenerSolicitudSeleccionada();
            btnImprimir.Enabled = solicitud != null;
            btnEliminar.Enabled = solicitud != null && solicitud.Activo;
            btnReactivar.Enabled = solicitud != null && !solicitud.Activo;
        }

        private void SolicitudesGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                btnImprimir.PerformClick();
            }
        }

        private void SolicitudesGrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Formatear celdas específicas
            if (e.ColumnIndex == 8 && e.Value != null) // Columna Estado
            {
                var estado = e.Value.ToString();
                switch (estado)
                {
                    case "Completada":
                        e.CellStyle.ForeColor = Color.DarkGreen;
                        break;
                    case "Vencida":
                        e.CellStyle.ForeColor = Color.Red;
                        break;
                    case "En Proceso":
                        e.CellStyle.ForeColor = Color.DarkBlue;
                        break;
                }
            }
        }

        private SolicitudEtiqueta ObtenerSolicitudSeleccionada()
        {
            if (solicitudesGrid.SelectedRows.Count > 0)
            {
                return solicitudesGrid.SelectedRows[0].Tag as SolicitudEtiqueta;
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
    }
}