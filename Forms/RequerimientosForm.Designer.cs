using System;
using System.Drawing;
using System.Windows.Forms;

namespace EtiquetasApp.Forms
{
    partial class RequerimientosForm
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        // Controles de filtros
        private GroupBox filtrosGroupBox;
        private CheckBox soloSinEtiquetasCheckBox;
        private DateTimePicker fechaDesdeDateTime;
        private Label lblFechaDesde;
        private DateTimePicker fechaHastaDateTime;
        private Label lblFechaHasta;
        private TextBox ordenFabTextBox;
        private Label lblOrdenFab;
        private TextBox descripcionTextBox;
        private Label lblDescripcion;

        // Botones de acción
        private Button btnActualizar;
        private Button btnLimpiarFiltros;
        private Button btnCrearSolicitud;
        private Button btnCrearMaestro;
        private Button btnExportar;

        // Grilla de datos
        private DataGridView requerimientosGrid;

        // Panel de estadísticas
        private GroupBox estadisticasGroupBox;
        private Label lblTotalOrdenesTxt;
        private Label lblTotalOrdenes;
        private Label lblSinCodigosTxt;
        private Label lblSinCodigos;
        private Label lblConCodigosTxt;
        private Label lblConCodigos;
        private Label lblUrgentesTxt;
        private Label lblUrgentes;
        private Label lblTotalCantidadTxt;
        private Label lblTotalCantidad;
        private Label lblPorcentajeCoberturaTxt;
        private Label lblPorcentajeCobertura;

        // Panel de detalle
        private GroupBox detalleGroupBox;
        private Label detalleLabel;

        // Status Strip
        private StatusStrip statusStrip;
        private ToolStripStatusLabel statusLabel;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();

            this.filtrosGroupBox = new GroupBox();
            this.soloSinEtiquetasCheckBox = new CheckBox();
            this.fechaDesdeDateTime = new DateTimePicker();
            this.lblFechaDesde = new Label();
            this.fechaHastaDateTime = new DateTimePicker();
            this.lblFechaHasta = new Label();
            this.ordenFabTextBox = new TextBox();
            this.lblOrdenFab = new Label();
            this.descripcionTextBox = new TextBox();
            this.lblDescripcion = new Label();
            this.btnActualizar = new Button();
            this.btnLimpiarFiltros = new Button();
            this.btnCrearSolicitud = new Button();
            this.btnCrearMaestro = new Button();
            this.btnExportar = new Button();
            this.requerimientosGrid = new DataGridView();
            this.estadisticasGroupBox = new GroupBox();
            this.lblTotalOrdenesTxt = new Label();
            this.lblTotalOrdenes = new Label();
            this.lblSinCodigosTxt = new Label();
            this.lblSinCodigos = new Label();
            this.lblConCodigosTxt = new Label();
            this.lblConCodigos = new Label();
            this.lblUrgentesTxt = new Label();
            this.lblUrgentes = new Label();
            this.lblTotalCantidadTxt = new Label();
            this.lblTotalCantidad = new Label();
            this.lblPorcentajeCoberturaTxt = new Label();
            this.lblPorcentajeCobertura = new Label();
            this.detalleGroupBox = new GroupBox();
            this.detalleLabel = new Label();
            this.statusStrip = new StatusStrip();
            this.statusLabel = new ToolStripStatusLabel();

            this.filtrosGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.requerimientosGrid)).BeginInit();
            this.estadisticasGroupBox.SuspendLayout();
            this.detalleGroupBox.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();

            // 
            // filtrosGroupBox
            // 
            this.filtrosGroupBox.Controls.Add(this.descripcionTextBox);
            this.filtrosGroupBox.Controls.Add(this.lblDescripcion);
            this.filtrosGroupBox.Controls.Add(this.ordenFabTextBox);
            this.filtrosGroupBox.Controls.Add(this.lblOrdenFab);
            this.filtrosGroupBox.Controls.Add(this.fechaHastaDateTime);
            this.filtrosGroupBox.Controls.Add(this.lblFechaHasta);
            this.filtrosGroupBox.Controls.Add(this.fechaDesdeDateTime);
            this.filtrosGroupBox.Controls.Add(this.lblFechaDesde);
            this.filtrosGroupBox.Controls.Add(this.soloSinEtiquetasCheckBox);
            this.filtrosGroupBox.Location = new Point(12, 12);
            this.filtrosGroupBox.Name = "filtrosGroupBox";
            this.filtrosGroupBox.Size = new Size(800, 100);
            this.filtrosGroupBox.TabIndex = 0;
            this.filtrosGroupBox.TabStop = false;
            this.filtrosGroupBox.Text = "Filtros de Búsqueda";

            // 
            // soloSinEtiquetasCheckBox
            // 
            this.soloSinEtiquetasCheckBox.AutoSize = true;
            this.soloSinEtiquetasCheckBox.Checked = true;
            this.soloSinEtiquetasCheckBox.CheckState = CheckState.Checked;
            this.soloSinEtiquetasCheckBox.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
            this.soloSinEtiquetasCheckBox.ForeColor = Color.DarkRed;
            this.soloSinEtiquetasCheckBox.Location = new Point(15, 25);
            this.soloSinEtiquetasCheckBox.Name = "soloSinEtiquetasCheckBox";
            this.soloSinEtiquetasCheckBox.Size = new Size(180, 17);
            this.soloSinEtiquetasCheckBox.TabIndex = 0;
            this.soloSinEtiquetasCheckBox.Text = "Solo órdenes sin códigos de etiquetas";
            this.soloSinEtiquetasCheckBox.UseVisualStyleBackColor = true;

            // 
            // lblFechaDesde
            // 
            this.lblFechaDesde.AutoSize = true;
            this.lblFechaDesde.Location = new Point(15, 55);
            this.lblFechaDesde.Name = "lblFechaDesde";
            this.lblFechaDesde.Size = new Size(70, 13);
            this.lblFechaDesde.TabIndex = 1;
            this.lblFechaDesde.Text = "Fecha desde:";

            // 
            // fechaDesdeDateTime
            // 
            this.fechaDesdeDateTime.Format = DateTimePickerFormat.Short;
            this.fechaDesdeDateTime.Location = new Point(15, 75);
            this.fechaDesdeDateTime.Name = "fechaDesdeDateTime";
            this.fechaDesdeDateTime.Size = new Size(100, 20);
            this.fechaDesdeDateTime.TabIndex = 2;

            // 
            // lblFechaHasta
            // 
            this.lblFechaHasta.AutoSize = true;
            this.lblFechaHasta.Location = new Point(130, 55);
            this.lblFechaHasta.Name = "lblFechaHasta";
            this.lblFechaHasta.Size = new Size(68, 13);
            this.lblFechaHasta.TabIndex = 3;
            this.lblFechaHasta.Text = "Fecha hasta:";

            // 
            // fechaHastaDateTime
            // 
            this.fechaHastaDateTime.Format = DateTimePickerFormat.Short;
            this.fechaHastaDateTime.Location = new Point(130, 75);
            this.fechaHastaDateTime.Name = "fechaHastaDateTime";
            this.fechaHastaDateTime.Size = new Size(100, 20);
            this.fechaHastaDateTime.TabIndex = 4;

            // 
            // lblOrdenFab
            // 
            this.lblOrdenFab.AutoSize = true;
            this.lblOrdenFab.Location = new Point(250, 55);
            this.lblOrdenFab.Name = "lblOrdenFab";
            this.lblOrdenFab.Size = new Size(75, 13);
            this.lblOrdenFab.TabIndex = 5;
            this.lblOrdenFab.Text = "Orden/Parte:";

            // 
            // ordenFabTextBox
            // 
            this.ordenFabTextBox.Location = new Point(250, 75);
            this.ordenFabTextBox.Name = "ordenFabTextBox";
            this.ordenFabTextBox.Size = new Size(120, 20);
            this.ordenFabTextBox.TabIndex = 6;
            this.ordenFabTextBox.CharacterCasing = CharacterCasing.Upper;

            // 
            // lblDescripcion
            // 
            this.lblDescripcion.AutoSize = true;
            this.lblDescripcion.Location = new Point(390, 55);
            this.lblDescripcion.Name = "lblDescripcion";
            this.lblDescripcion.Size = new Size(66, 13);
            this.lblDescripcion.TabIndex = 7;
            this.lblDescripcion.Text = "Descripción:";

            // 
            // descripcionTextBox
            // 
            this.descripcionTextBox.Location = new Point(390, 75);
            this.descripcionTextBox.Name = "descripcionTextBox";
            this.descripcionTextBox.Size = new Size(200, 20);
            this.descripcionTextBox.TabIndex = 8;

            // Botones de acción
            this.btnActualizar.BackColor = Color.LightBlue;
            this.btnActualizar.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
            this.btnActualizar.Location = new Point(830, 25);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new Size(90, 30);
            this.btnActualizar.TabIndex = 1;
            this.btnActualizar.Text = "Actualizar";
            this.btnActualizar.UseVisualStyleBackColor = false;

            this.btnLimpiarFiltros.Location = new Point(830, 65);
            this.btnLimpiarFiltros.Name = "btnLimpiarFiltros";
            this.btnLimpiarFiltros.Size = new Size(90, 30);
            this.btnLimpiarFiltros.TabIndex = 2;
            this.btnLimpiarFiltros.Text = "Limpiar Filtros";
            this.btnLimpiarFiltros.UseVisualStyleBackColor = true;

            // 
            // requerimientosGrid
            // 
            this.requerimientosGrid.AllowUserToAddRows = false;
            this.requerimientosGrid.AllowUserToDeleteRows = false;
            this.requerimientosGrid.Anchor = ((AnchorStyles)((((AnchorStyles.Top | AnchorStyles.Bottom)
            | AnchorStyles.Left)
            | AnchorStyles.Right)));
            this.requerimientosGrid.BackgroundColor = SystemColors.Window;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            this.requerimientosGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.requerimientosGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;

            // Configurar columnas
            this.requerimientosGrid.Columns.Add("ColOrdenFab", "Orden Fab.");
            this.requerimientosGrid.Columns.Add("ColPartId", "Part ID");
            this.requerimientosGrid.Columns.Add("ColDescripcion", "Descripción");
            this.requerimientosGrid.Columns.Add("ColCantidad", "Cantidad");
            this.requerimientosGrid.Columns.Add("ColFechaInicio", "Fecha Inicio");
            this.requerimientosGrid.Columns.Add("ColFechaRequerida", "Fecha Requerida");
            this.requerimientosGrid.Columns.Add("ColEstado", "Estado");
            this.requerimientosGrid.Columns.Add("ColPrioridad", "Prioridad");
            this.requerimientosGrid.Columns.Add("ColTieneCodigos", "Tiene Códigos");
            this.requerimientosGrid.Columns.Add("ColDiasEntrega", "Días Entrega");

            // Configurar anchos de columnas
            this.requerimientosGrid.Columns[0].Width = 100;  // Orden Fab
            this.requerimientosGrid.Columns[1].Width = 100;  // Part ID
            this.requerimientosGrid.Columns[2].Width = 250;  // Descripción
            this.requerimientosGrid.Columns[3].Width = 80;   // Cantidad
            this.requerimientosGrid.Columns[4].Width = 90;   // Fecha Inicio
            this.requerimientosGrid.Columns[5].Width = 90;   // Fecha Requerida
            this.requerimientosGrid.Columns[6].Width = 80;   // Estado
            this.requerimientosGrid.Columns[7].Width = 80;   // Prioridad
            this.requerimientosGrid.Columns[8].Width = 80;   // Tiene Códigos
            this.requerimientosGrid.Columns[9].Width = 80;   // Días Entrega

            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            this.requerimientosGrid.DefaultCellStyle = dataGridViewCellStyle2;
            this.requerimientosGrid.Location = new Point(12, 130);
            this.requerimientosGrid.MultiSelect = false;
            this.requerimientosGrid.Name = "requerimientosGrid";
            this.requerimientosGrid.ReadOnly = true;
            this.requerimientosGrid.RowHeadersVisible = false;
            this.requerimientosGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.requerimientosGrid.Size = new Size(1050, 350);
            this.requerimientosGrid.TabIndex = 3;

            // 
            // estadisticasGroupBox
            // 
            this.estadisticasGroupBox.Anchor = ((AnchorStyles)(((AnchorStyles.Bottom | AnchorStyles.Left)
            | AnchorStyles.Right)));
            this.estadisticasGroupBox.Controls.Add(this.lblPorcentajeCobertura);
            this.estadisticasGroupBox.Controls.Add(this.lblPorcentajeCoberturaTxt);
            this.estadisticasGroupBox.Controls.Add(this.lblTotalCantidad);
            this.estadisticasGroupBox.Controls.Add(this.lblTotalCantidadTxt);
            this.estadisticasGroupBox.Controls.Add(this.lblUrgentes);
            this.estadisticasGroupBox.Controls.Add(this.lblUrgentesTxt);
            this.estadisticasGroupBox.Controls.Add(this.lblConCodigos);
            this.estadisticasGroupBox.Controls.Add(this.lblConCodigosTxt);
            this.estadisticasGroupBox.Controls.Add(this.lblSinCodigos);
            this.estadisticasGroupBox.Controls.Add(this.lblSinCodigosTxt);
            this.estadisticasGroupBox.Controls.Add(this.lblTotalOrdenes);
            this.estadisticasGroupBox.Controls.Add(this.lblTotalOrdenesTxt);
            this.estadisticasGroupBox.Location = new Point(12, 490);
            this.estadisticasGroupBox.Name = "estadisticasGroupBox";
            this.estadisticasGroupBox.Size = new Size(650, 80);
            this.estadisticasGroupBox.TabIndex = 4;
            this.estadisticasGroupBox.TabStop = false;
            this.estadisticasGroupBox.Text = "Estadísticas";

            // Fila 1 de estadísticas
            this.lblTotalOrdenesTxt.AutoSize = true;
            this.lblTotalOrdenesTxt.Location = new Point(15, 25);
            this.lblTotalOrdenesTxt.Name = "lblTotalOrdenesTxt";
            this.lblTotalOrdenesTxt.Size = new Size(75, 13);
            this.lblTotalOrdenesTxt.TabIndex = 0;
            this.lblTotalOrdenesTxt.Text = "Total Órdenes:";

            this.lblTotalOrdenes.AutoSize = true;
            this.lblTotalOrdenes.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
            this.lblTotalOrdenes.Location = new Point(95, 25);
            this.lblTotalOrdenes.Name = "lblTotalOrdenes";
            this.lblTotalOrdenes.Size = new Size(14, 13);
            this.lblTotalOrdenes.TabIndex = 1;
            this.lblTotalOrdenes.Text = "0";

            this.lblSinCodigosTxt.AutoSize = true;
            this.lblSinCodigosTxt.Location = new Point(140, 25);
            this.lblSinCodigosTxt.Name = "lblSinCodigosTxt";
            this.lblSinCodigosTxt.Size = new Size(70, 13);
            this.lblSinCodigosTxt.TabIndex = 2;
            this.lblSinCodigosTxt.Text = "Sin Códigos:";

            this.lblSinCodigos.AutoSize = true;
            this.lblSinCodigos.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
            this.lblSinCodigos.ForeColor = Color.Red;
            this.lblSinCodigos.Location = new Point(215, 25);
            this.lblSinCodigos.Name = "lblSinCodigos";
            this.lblSinCodigos.Size = new Size(14, 13);
            this.lblSinCodigos.TabIndex = 3;
            this.lblSinCodigos.Text = "0";

            this.lblConCodigosTxt.AutoSize = true;
            this.lblConCodigosTxt.Location = new Point(260, 25);
            this.lblConCodigosTxt.Name = "lblConCodigosTxt";
            this.lblConCodigosTxt.Size = new Size(75, 13);
            this.lblConCodigosTxt.TabIndex = 4;
            this.lblConCodigosTxt.Text = "Con Códigos:";

            this.lblConCodigos.AutoSize = true;
            this.lblConCodigos.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
            this.lblConCodigos.ForeColor = Color.DarkGreen;
            this.lblConCodigos.Location = new Point(340, 25);
            this.lblConCodigos.Name = "lblConCodigos";
            this.lblConCodigos.Size = new Size(14, 13);
            this.lblConCodigos.TabIndex = 5;
            this.lblConCodigos.Text = "0";

            this.lblUrgentesTxt.AutoSize = true;
            this.lblUrgentesTxt.Location = new Point(385, 25);
            this.lblUrgentesTxt.Name = "lblUrgentesTxt";
            this.lblUrgentesTxt.Size = new Size(52, 13);
            this.lblUrgentesTxt.TabIndex = 6;
            this.lblUrgentesTxt.Text = "Urgentes:";

            this.lblUrgentes.AutoSize = true;
            this.lblUrgentes.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
            this.lblUrgentes.ForeColor = Color.Red;
            this.lblUrgentes.Location = new Point(440, 25);
            this.lblUrgentes.Name = "lblUrgentes";
            this.lblUrgentes.Size = new Size(14, 13);
            this.lblUrgentes.TabIndex = 7;
            this.lblUrgentes.Text = "0";

            // Fila 2 de estadísticas
            this.lblTotalCantidadTxt.AutoSize = true;
            this.lblTotalCantidadTxt.Location = new Point(15, 50);
            this.lblTotalCantidadTxt.Name = "lblTotalCantidadTxt";
            this.lblTotalCantidadTxt.Size = new Size(82, 13);
            this.lblTotalCantidadTxt.TabIndex = 8;
            this.lblTotalCantidadTxt.Text = "Total Cantidad:";

            this.lblTotalCantidad.AutoSize = true;
            this.lblTotalCantidad.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
            this.lblTotalCantidad.Location = new Point(100, 50);
            this.lblTotalCantidad.Name = "lblTotalCantidad";
            this.lblTotalCantidad.Size = new Size(14, 13);
            this.lblTotalCantidad.TabIndex = 9;
            this.lblTotalCantidad.Text = "0";

            this.lblPorcentajeCoberturaTxt.AutoSize = true;
            this.lblPorcentajeCoberturaTxt.Location = new Point(260, 50);
            this.lblPorcentajeCoberturaTxt.Name = "lblPorcentajeCoberturaTxt";
            this.lblPorcentajeCoberturaTxt.Size = new Size(108, 13);
            this.lblPorcentajeCoberturaTxt.TabIndex = 10;
            this.lblPorcentajeCoberturaTxt.Text = "Cobertura Códigos:";

            this.lblPorcentajeCobertura.AutoSize = true;
            this.lblPorcentajeCobertura.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
            this.lblPorcentajeCobertura.ForeColor = Color.DarkBlue;
            this.lblPorcentajeCobertura.Location = new Point(375, 50);
            this.lblPorcentajeCobertura.Name = "lblPorcentajeCobertura";
            this.lblPorcentajeCobertura.Size = new Size(27, 13);
            this.lblPorcentajeCobertura.TabIndex = 11;
            this.lblPorcentajeCobertura.Text = "0%";

            // 
            // detalleGroupBox
            // 
            this.detalleGroupBox.Anchor = ((AnchorStyles)((AnchorStyles.Bottom | AnchorStyles.Right)));
            this.detalleGroupBox.Controls.Add(this.detalleLabel);
            this.detalleGroupBox.Location = new Point(680, 490);
            this.detalleGroupBox.Name = "detalleGroupBox";
            this.detalleGroupBox.Size = new Size(250, 120);
            this.detalleGroupBox.TabIndex = 5;
            this.detalleGroupBox.TabStop = false;
            this.detalleGroupBox.Text = "Detalle de Orden Seleccionada";

            // 
            // detalleLabel
            // 
            this.detalleLabel.Location = new Point(10, 20);
            this.detalleLabel.Name = "detalleLabel";
            this.detalleLabel.Size = new Size(230, 90);
            this.detalleLabel.TabIndex = 0;
            this.detalleLabel.Text = "Seleccione una orden para ver detalles";
            this.detalleLabel.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);

            // Botones de acción principales
            this.btnCrearSolicitud.Anchor = ((AnchorStyles)((AnchorStyles.Bottom | AnchorStyles.Right)));
            this.btnCrearSolicitud.BackColor = Color.LightGreen;
            this.btnCrearSolicitud.Enabled = false;
            this.btnCrearSolicitud.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
            this.btnCrearSolicitud.Location = new Point(950, 500);
            this.btnCrearSolicitud.Name = "btnCrearSolicitud";
            this.btnCrearSolicitud.Size = new Size(100, 35);
            this.btnCrearSolicitud.TabIndex = 6;
            this.btnCrearSolicitud.Text = "Crear Solicitud";
            this.btnCrearSolicitud.UseVisualStyleBackColor = false;

            this.btnCrearMaestro.Anchor = ((AnchorStyles)((AnchorStyles.Bottom | AnchorStyles.Right)));
            this.btnCrearMaestro.BackColor = Color.LightYellow;
            this.btnCrearMaestro.Enabled = false;
            this.btnCrearMaestro.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
            this.btnCrearMaestro.Location = new Point(950, 545);
            this.btnCrearMaestro.Name = "btnCrearMaestro";
            this.btnCrearMaestro.Size = new Size(100, 35);
            this.btnCrearMaestro.TabIndex = 7;
            this.btnCrearMaestro.Text = "Crear Maestro";
            this.btnCrearMaestro.UseVisualStyleBackColor = false;

            this.btnExportar.Anchor = ((AnchorStyles)((AnchorStyles.Bottom | AnchorStyles.Right)));
            this.btnExportar.Location = new Point(950, 590);
            this.btnExportar.Name = "btnExportar";
            this.btnExportar.Size = new Size(100, 30);
            this.btnExportar.TabIndex = 8;
            this.btnExportar.Text = "Exportar";
            this.btnExportar.UseVisualStyleBackColor = true;

            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new ToolStripItem[] {
            this.statusLabel});
            this.statusStrip.Location = new Point(0, 638);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new Size(1084, 22);
            this.statusStrip.TabIndex = 9;
            this.statusStrip.Text = "statusStrip1";

            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new Size(35, 17);
            this.statusLabel.Text = "Listo";

            // 
            // RequerimientosForm
            // 
            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(1084, 660);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.btnExportar);
            this.Controls.Add(this.btnCrearMaestro);
            this.Controls.Add(this.btnCrearSolicitud);
            this.Controls.Add(this.detalleGroupBox);
            this.Controls.Add(this.estadisticasGroupBox);
            this.Controls.Add(this.requerimientosGrid);
            this.Controls.Add(this.btnLimpiarFiltros);
            this.Controls.Add(this.btnActualizar);
            this.Controls.Add(this.filtrosGroupBox);
            this.Name = "RequerimientosForm";
            this.Text = "Ver Requerimientos de Etiquetas";

            this.filtrosGroupBox.ResumeLayout(false);
            this.filtrosGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.requerimientosGrid)).EndInit();
            this.estadisticasGroupBox.ResumeLayout(false);
            this.estadisticasGroupBox.PerformLayout();
            this.detalleGroupBox.ResumeLayout(false);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}