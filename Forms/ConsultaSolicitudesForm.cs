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
            tipoEtiquetaComboBox.SelectedIndexChanged += FiltroChanged;
            estadoComboBox.SelectedIndexChanged += FiltroChanged;
            fechaDesdeDateTime.ValueChanged += FiltroChanged;
            fechaHastaDateTime.ValueChanged += FiltroChanged;
            ordenFabTextBox.TextChanged += FiltroChanged;
            descripcionTextBox.TextChanged += FiltroChanged;

            btnBuscar.Click += BtnBuscar_Click;
            btnLimpiar.Click += BtnLimpiar_Click;
            btnExportar.Click += BtnExportar_Click;
            btnImprimir.Click += BtnImprimir_Click;

            solicitudesGrid.SelectionChanged += SolicitudesGrid_SelectionChanged;
            solicitudesGrid.CellDoubleClick += SolicitudesGrid_CellDoubleClick;
            solicitudesGrid.CellFormatting += SolicitudesGrid_CellFormatting;
        }

        private void ConfigurarFiltros()
        {
            tipoEtiquetaComboBox.Items.Add("Todos");
            foreach (TipoEtiqueta tipo in Enum.GetValues(typeof(TipoEtiqueta)))
            {
                tipoEtiquetaComboBox.Items.Add(tipo.GetDisplayName());
            }
            tipoEtiquetaComboBox.SelectedIndex = 0;

            estadoComboBox.Items.Add("Todos");
            estadoComboBox.Items.Add("Pendiente");
            estadoComboBox.Items.Add("Completada");
            estadoComboBox.Items.Add("Vencida");
            estadoComboBox.SelectedIndex = 0;

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
            return solicitud.EstadoDescripcion == estadoSeleccionado;
        }

        private bool FiltrarPorFechas(SolicitudEtiqueta solicitud)
        {
            return solicitud.FechaRequerida.Date >= fechaDesdeDateTime.Value.Date &&
                   solicitud.FechaRequerida.Date <= fechaHastaDateTime.Value.Date;
        }

        private bool FiltrarPorOrdenFab(SolicitudEtiqueta solicitud)
        {
            if (string.IsNullOrEmpty(ordenFabTextBox.Text)) return true;

            return solicitud.OrdenFab.ToString().Contains(ordenFabTextBox.Text);
        }

        private bool FiltrarPorDescripcion(SolicitudEtiqueta solicitud)
        {
            if (string.IsNullOrEmpty(descripcionTextBox.Text)) return true;

            return solicitud.Descripcion?.ToUpper().Contains(descripcionTextBox.Text.ToUpper()) ?? false;
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
                    row.Cells[8].Value = solicitud.EstadoDescripcion;
                    row.Cells[9].Value = solicitud.FechaSolicitud.ToString("dd/MM/yyyy");
                    row.Cells[10].Value = solicitud.FechaRequerida.ToString("dd/MM/yyyy");
                    row.Cells[11].Value = solicitud.FechaFabricacion?.ToString("dd/MM/yyyy") ?? "";
                    row.Cells[12].Value = solicitud.Usuario ?? "";
                    row.Cells[13].Value = solicitud.Observaciones;

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
            if (solicitud.EstaCompletada)
            {
                row.DefaultCellStyle.BackColor = Color.LightGreen;
            }
            else if (solicitud.EsUrgente)
            {
                row.DefaultCellStyle.BackColor = Color.LightCoral;
            }
            else if (solicitud.DiasVencimiento <= 3 && solicitud.DiasVencimiento >= 0)
            {
                row.DefaultCellStyle.BackColor = Color.LightYellow;
            }
            else if (solicitud.EstaVencida)
            {
                row.DefaultCellStyle.BackColor = Color.LightGray;
            }
        }

        private void ActualizarEstadisticas()
        {
            try
            {
                var total = solicitudesFiltradas.Count;
                var completadas = solicitudesFiltradas.Count(s => s.EstaCompletada);
                var pendientes = solicitudesFiltradas.Count(s => !s.EstaCompletada);
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
                    saveDialog.Filter = "Archivos CSV|*.csv";
                    saveDialog.Title = "Exportar Solicitudes";
                    saveDialog.FileName = $"Solicitudes_Etiquetas_{DateTime.Now:yyyyMMdd_HHmmss}";

                    if (saveDialog.ShowDialog() == DialogResult.OK)
                    {
                        ExportarCSV(saveDialog.FileName);
                        MostrarMensaje("Datos exportados correctamente", false);
                    }
                }
            }
            catch (Exception ex)
            {
                MostrarError($"Error exportando datos: {ex.Message}");
            }
        }

        private void ExportarCSV(string archivo)
        {
            using (var writer = new System.IO.StreamWriter(archivo, false, System.Text.Encoding.UTF8))
            {
                writer.WriteLine("ID,Orden Fab,Descripción,Tipo,Cant. Pedida,Cant. Fabricada,Cant. Pendiente,Color,Estado,Fecha Solicitud,Fecha Requerida,Fecha Fabricación,Usuario,Observaciones");

                foreach (var solicitud in solicitudesFiltradas)
                {
                    writer.WriteLine($"{solicitud.IdSolicitud},{solicitud.OrdenFab},\"{solicitud.Descripcion}\",{solicitud.TipoEtiqueta},{solicitud.CantidadPedida},{solicitud.CantidadFabricada},{solicitud.CantidadPendiente},{solicitud.Color},{solicitud.EstadoDescripcion},{solicitud.FechaSolicitud:dd/MM/yyyy},{solicitud.FechaRequerida:dd/MM/yyyy},{solicitud.FechaFabricacion?.ToString("dd/MM/yyyy") ?? ""},{solicitud.Usuario},\"{solicitud.Observaciones}\"");
                }
            }
        }

        private void BtnImprimir_Click(object sender, EventArgs e)
        {
            var solicitudSeleccionada = ObtenerSolicitudSeleccionada();
            if (solicitudSeleccionada == null)
            {
                MostrarError("Debe seleccionar una solicitud para imprimir");
                return;
            }

            MostrarMensaje("Funcionalidad de impresión pendiente de implementación", true);
        }

        private void SolicitudesGrid_SelectionChanged(object sender, EventArgs e)
        {
            var solicitud = ObtenerSolicitudSeleccionada();
            btnImprimir.Enabled = solicitud != null;
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
            if (e.ColumnIndex == 8 && e.Value != null) // Columna Estado
            {
                var estado = e.Value.ToString();
                switch (estado)
                {
                    case "Completada":
                    case "Fabricada":
                        e.CellStyle.ForeColor = Color.DarkGreen;
                        break;
                    case "Vencida":
                        e.CellStyle.ForeColor = Color.Red;
                        break;
                    case "En Proceso":
                    case "Parcial":
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