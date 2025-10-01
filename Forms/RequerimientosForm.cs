using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using EtiquetasApp.Models;
using EtiquetasApp.Services;

namespace EtiquetasApp.Forms
{
    public partial class RequerimientosForm : Form
    {
        private List<OrdenFabricacion> ordenesRequerimientos;
        private List<OrdenFabricacion> ordenesFiltradas;
        private List<MaestroCodigoEtiqueta> maestrosCodigos;

        public RequerimientosForm()
        {
            InitializeComponent();
            InitializeForm();
        }

        private void InitializeForm()
        {
            ordenesRequerimientos = new List<OrdenFabricacion>();
            ordenesFiltradas = new List<OrdenFabricacion>();
            maestrosCodigos = new List<MaestroCodigoEtiqueta>();

            ConfigurarEventos();
            CargarDatos();
            ConfigurarFiltros();
        }

        private void ConfigurarEventos()
        {
            soloSinEtiquetasCheckBox.CheckedChanged += FiltroChanged;
            fechaDesdeDateTime.ValueChanged += FiltroChanged;
            fechaHastaDateTime.ValueChanged += FiltroChanged;
            ordenFabTextBox.TextChanged += FiltroChanged;
            descripcionTextBox.TextChanged += FiltroChanged;

            btnActualizar.Click += BtnActualizar_Click;
            btnLimpiarFiltros.Click += BtnLimpiarFiltros_Click;
            btnCrearSolicitud.Click += BtnCrearSolicitud_Click;
            btnCrearMaestro.Click += BtnCrearMaestro_Click;
            btnExportar.Click += BtnExportar_Click;

            requerimientosGrid.SelectionChanged += RequerimientosGrid_SelectionChanged;
            requerimientosGrid.CellDoubleClick += RequerimientosGrid_CellDoubleClick;
            requerimientosGrid.CellFormatting += RequerimientosGrid_CellFormatting;
        }

        private void ConfigurarFiltros()
        {
            fechaDesdeDateTime.Value = DateTime.Now.Date;
            fechaHastaDateTime.Value = DateTime.Now.AddDays(30);
            soloSinEtiquetasCheckBox.Checked = true;
        }

        private void CargarDatos()
        {
            try
            {
                ordenesRequerimientos = DatabaseService.GetOrdenesRequerimientos();
                maestrosCodigos = DatabaseService.GetMaestroCodigosEtiquetas();

                MarcarOrdenesConCodigos();
                AplicarFiltros();
                ActualizarEstadisticas();
            }
            catch (Exception ex)
            {
                MostrarError($"Error cargando requerimientos: {ex.Message}");
            }
        }

        private void MarcarOrdenesConCodigos()
        {
            foreach (var orden in ordenesRequerimientos)
            {
                var tieneMaestro = maestrosCodigos.Any(m => m.PartId.Equals(orden.PartId, StringComparison.OrdinalIgnoreCase));
                orden.TieneCodigoEtiqueta = tieneMaestro;
            }
        }

        private void AplicarFiltros()
        {
            try
            {
                ordenesFiltradas = ordenesRequerimientos.AsQueryable()
                    .Where(o => FiltrarPorFechas(o))
                    .Where(o => FiltrarPorOrdenFab(o))
                    .Where(o => FiltrarPorDescripcion(o))
                    .Where(o => FiltrarPorEtiquetas(o))
                    .OrderBy(o => o.FechaProgramada)
                    .ThenByDescending(o => o.Prioridad)
                    .ToList();

                CargarGrilla();
                ActualizarEstadisticas();
            }
            catch (Exception ex)
            {
                MostrarError($"Error aplicando filtros: {ex.Message}");
            }
        }

        private bool FiltrarPorFechas(OrdenFabricacion orden)
        {
            if (!orden.FechaProgramada.HasValue) return false;

            return orden.FechaProgramada.Value.Date >= fechaDesdeDateTime.Value.Date &&
                   orden.FechaProgramada.Value.Date <= fechaHastaDateTime.Value.Date;
        }

        private bool FiltrarPorOrdenFab(OrdenFabricacion orden)
        {
            if (string.IsNullOrEmpty(ordenFabTextBox.Text)) return true;

            return orden.BaseId.ToUpper().Contains(ordenFabTextBox.Text.ToUpper()) ||
                   orden.PartId.ToUpper().Contains(ordenFabTextBox.Text.ToUpper());
        }

        private bool FiltrarPorDescripcion(OrdenFabricacion orden)
        {
            if (string.IsNullOrEmpty(descripcionTextBox.Text)) return true;

            return orden.Descripcion?.ToUpper().Contains(descripcionTextBox.Text.ToUpper()) ?? false;
        }

        private bool FiltrarPorEtiquetas(OrdenFabricacion orden)
        {
            if (!soloSinEtiquetasCheckBox.Checked) return true;

            return !orden.TieneCodigoEtiqueta;
        }

        private void CargarGrilla()
        {
            try
            {
                requerimientosGrid.Rows.Clear();

                foreach (var orden in ordenesFiltradas)
                {
                    var row = new DataGridViewRow();
                    row.CreateCells(requerimientosGrid);

                    row.Cells[0].Value = orden.BaseId;
                    row.Cells[1].Value = orden.PartId;
                    row.Cells[2].Value = orden.Descripcion;
                    row.Cells[3].Value = orden.Cantidad;
                    row.Cells[4].Value = orden.FechaInicio?.ToString("dd/MM/yyyy") ?? "";
                    row.Cells[5].Value = orden.FechaProgramada?.ToString("dd/MM/yyyy") ?? "";
                    row.Cells[6].Value = orden.EstadoDescripcion;
                    row.Cells[7].Value = orden.PrioridadDescripcion;
                    row.Cells[8].Value = orden.TieneCodigoEtiqueta ? "Sí" : "No";
                    row.Cells[9].Value = orden.DiasParaProgramada?.ToString() ?? "";

                    ConfigurarColorFila(row, orden);

                    row.Tag = orden;
                    requerimientosGrid.Rows.Add(row);
                }
            }
            catch (Exception ex)
            {
                MostrarError($"Error cargando grilla: {ex.Message}");
            }
        }

        private void ConfigurarColorFila(DataGridViewRow row, OrdenFabricacion orden)
        {
            if (orden.TieneCodigoEtiqueta)
            {
                row.DefaultCellStyle.BackColor = Color.LightGreen;
            }
            else if (orden.EsUrgente)
            {
                row.DefaultCellStyle.BackColor = Color.LightCoral;
            }
            else if (orden.DiasParaProgramada.HasValue && orden.DiasParaProgramada <= 7)
            {
                row.DefaultCellStyle.BackColor = Color.LightYellow;
            }
            else if (orden.Prioridad <= 2)
            {
                row.DefaultCellStyle.BackColor = Color.LightSalmon;
            }
        }

        private void ActualizarEstadisticas()
        {
            try
            {
                var total = ordenesFiltradas.Count;
                var sinCodigos = ordenesFiltradas.Count(o => !o.TieneCodigoEtiqueta);
                var conCodigos = ordenesFiltradas.Count(o => o.TieneCodigoEtiqueta);
                var urgentes = ordenesFiltradas.Count(o => o.EsUrgente);
                var totalCantidad = ordenesFiltradas.Sum(o => o.Cantidad);

                lblTotalOrdenes.Text = total.ToString("N0");
                lblSinCodigos.Text = sinCodigos.ToString("N0");
                lblConCodigos.Text = conCodigos.ToString("N0");
                lblUrgentes.Text = urgentes.ToString("N0");
                lblTotalCantidad.Text = totalCantidad.ToString("N0");

                var porcentajeCobertura = total > 0 ? (conCodigos * 100.0 / total) : 0;
                lblPorcentajeCobertura.Text = $"{porcentajeCobertura:F1}%";

                statusLabel.Text = $"Mostrando {total:N0} órdenes | {sinCodigos:N0} sin códigos | {urgentes:N0} urgentes";
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

        private void BtnActualizar_Click(object sender, EventArgs e)
        {
            CargarDatos();
            MostrarMensaje("Datos actualizados correctamente", false);
        }

        private void BtnLimpiarFiltros_Click(object sender, EventArgs e)
        {
            LimpiarFiltros();
        }

        private void LimpiarFiltros()
        {
            fechaDesdeDateTime.Value = DateTime.Now.Date;
            fechaHastaDateTime.Value = DateTime.Now.AddDays(30);
            ordenFabTextBox.Clear();
            descripcionTextBox.Clear();
            soloSinEtiquetasCheckBox.Checked = true;

            AplicarFiltros();
        }

        private void BtnCrearSolicitud_Click(object sender, EventArgs e)
        {
            var ordenSeleccionada = ObtenerOrdenSeleccionada();
            if (ordenSeleccionada == null)
            {
                MostrarError("Debe seleccionar una orden para crear solicitud");
                return;
            }

            try
            {
                var solicitudForm = new SolicitudesEtiquetasForm();
                solicitudForm.MdiParent = this.MdiParent;
                solicitudForm.WindowState = FormWindowState.Maximized;
                solicitudForm.Show();
            }
            catch (Exception ex)
            {
                MostrarError($"Error abriendo formulario de solicitudes: {ex.Message}");
            }
        }

        private void BtnCrearMaestro_Click(object sender, EventArgs e)
        {
            var ordenSeleccionada = ObtenerOrdenSeleccionada();
            if (ordenSeleccionada == null)
            {
                MostrarError("Debe seleccionar una orden para crear maestro");
                return;
            }

            if (ordenSeleccionada.TieneCodigoEtiqueta)
            {
                MostrarError("Esta orden ya tiene códigos de etiqueta configurados");
                return;
            }

            try
            {
                var maestroForm = new CodificaEtiquetasForm();
                maestroForm.CargarPartId(ordenSeleccionada.PartId);
                maestroForm.MdiParent = this.MdiParent;
                maestroForm.WindowState = FormWindowState.Maximized;
                maestroForm.Show();
            }
            catch (Exception ex)
            {
                MostrarError($"Error abriendo formulario de maestros: {ex.Message}");
            }
        }

        private void BtnExportar_Click(object sender, EventArgs e)
        {
            try
            {
                using (var saveDialog = new SaveFileDialog())
                {
                    saveDialog.Filter = "Archivos CSV|*.csv";
                    saveDialog.Title = "Exportar Requerimientos";
                    saveDialog.FileName = $"Requerimientos_Etiquetas_{DateTime.Now:yyyyMMdd_HHmmss}.csv";

                    if (saveDialog.ShowDialog() == DialogResult.OK)
                    {
                        ExportarCSV(saveDialog.FileName);
                        MostrarMensaje("Requerimientos exportados correctamente", false);
                    }
                }
            }
            catch (Exception ex)
            {
                MostrarError($"Error exportando requerimientos: {ex.Message}");
            }
        }

        private void ExportarCSV(string archivo)
        {
            using (var writer = new System.IO.StreamWriter(archivo, false, System.Text.Encoding.UTF8))
            {
                writer.WriteLine("Orden Fab,Part ID,Descripción,Cantidad,Fecha Inicio,Fecha Requerida,Estado,Prioridad,Tiene Códigos,Días para Entrega");

                foreach (var orden in ordenesFiltradas)
                {
                    writer.WriteLine($"{orden.BaseId},{orden.PartId},\"{orden.Descripcion}\",{orden.Cantidad},{orden.FechaInicio?.ToString("dd/MM/yyyy") ?? ""},{orden.FechaProgramada?.ToString("dd/MM/yyyy") ?? ""},{orden.EstadoDescripcion},{orden.PrioridadDescripcion},{(orden.TieneCodigoEtiqueta ? "Sí" : "No")},{orden.DiasParaProgramada?.ToString() ?? ""}");
                }
            }
        }

        private void RequerimientosGrid_SelectionChanged(object sender, EventArgs e)
        {
            var orden = ObtenerOrdenSeleccionada();
            btnCrearSolicitud.Enabled = orden != null;
            btnCrearMaestro.Enabled = orden != null && !orden.TieneCodigoEtiqueta;

            MostrarDetalleOrden(orden);
        }

        private void MostrarDetalleOrden(OrdenFabricacion orden)
        {
            if (orden != null)
            {
                var detalle = $"Orden: {orden.BaseId}\n";
                detalle += $"Parte: {orden.PartId}\n";
                detalle += $"Descripción: {orden.Descripcion}\n";
                detalle += $"Cantidad: {orden.Cantidad:N0}\n";
                detalle += $"Estado: {orden.EstadoDescripcion}\n";
                detalle += $"Prioridad: {orden.PrioridadDescripcion}\n";
                detalle += $"Fecha Inicio: {orden.FechaInicio?.ToString("dd/MM/yyyy") ?? "N/A"}\n";
                detalle += $"Fecha Programada: {orden.FechaProgramada?.ToString("dd/MM/yyyy") ?? "N/A"}\n";
                detalle += $"Días para Entrega: {orden.DiasParaProgramada?.ToString() ?? "N/A"}\n";
                detalle += $"Tiene Códigos: {(orden.TieneCodigoEtiqueta ? "Sí" : "No")}";

                if (orden.EsUrgente)
                {
                    detalle += "\n⚠ URGENTE";
                }

                detalleLabel.Text = detalle;
            }
            else
            {
                detalleLabel.Text = "Seleccione una orden para ver detalles";
            }
        }

        private void RequerimientosGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var orden = ObtenerOrdenSeleccionada();
                if (orden != null)
                {
                    if (orden.TieneCodigoEtiqueta)
                    {
                        BtnCrearSolicitud_Click(sender, e);
                    }
                    else
                    {
                        BtnCrearMaestro_Click(sender, e);
                    }
                }
            }
        }

        private void RequerimientosGrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 8 && e.Value != null) // Columna "Tiene Códigos"
            {
                var valor = e.Value.ToString();
                if (valor == "Sí")
                {
                    e.CellStyle.ForeColor = Color.DarkGreen;
                    e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                }
                else
                {
                    e.CellStyle.ForeColor = Color.Red;
                    e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                }
            }

            if (e.ColumnIndex == 9 && e.Value != null) // Columna "Días para Entrega"
            {
                var valorStr = e.Value.ToString();
                if (!string.IsNullOrEmpty(valorStr) && int.TryParse(valorStr, out int dias))
                {
                    if (dias < 0)
                    {
                        e.CellStyle.ForeColor = Color.Red;
                        e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                    }
                    else if (dias <= 3)
                    {
                        e.CellStyle.ForeColor = Color.Orange;
                        e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                    }
                }
            }
        }

        private OrdenFabricacion ObtenerOrdenSeleccionada()
        {
            if (requerimientosGrid.SelectedRows.Count > 0)
            {
                return requerimientosGrid.SelectedRows[0].Tag as OrdenFabricacion;
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

        public void ActualizarDatos()
        {
            CargarDatos();
        }

        public void SeleccionarOrden(string baseId)
        {
            if (!string.IsNullOrEmpty(baseId))
            {
                foreach (DataGridViewRow row in requerimientosGrid.Rows)
                {
                    var orden = row.Tag as OrdenFabricacion;
                    if (orden != null && orden.BaseId.Equals(baseId, StringComparison.OrdinalIgnoreCase))
                    {
                        row.Selected = true;
                        requerimientosGrid.CurrentCell = row.Cells[0];
                        break;
                    }
                }
            }
        }
    }
}