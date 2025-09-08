using System;
using System.Drawing;
using System.Windows.Forms;

namespace EtiquetasApp.Forms
{
    partial class ConsultaSolicitudesForm
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        // Controles de filtros
        private GroupBox filtrosGroupBox;
        private ComboBox tipoEtiquetaComboBox;
        private Label lblTipoEtiqueta;
        private ComboBox estadoComboBox;
        private Label lblEstado;
        private DateTimePicker fechaDesdeDateTime;
        private Label lblFechaDesde;
        private DateTimePicker fechaHastaDateTime;
        private Label lblFechaHasta;
        private TextBox ordenFabTextBox;
        private Label lblOrdenFab;
        private TextBox descripcionTextBox;
        private Label lblDescripcion;
        private CheckBox autoFiltrarCheckBox;

        // Botones de acción
        private Button btnBuscar;
        private Button btnLimpiar;
        private Button btnExportar;
        private Button btnImprimir;
        private Button btnEliminar;
        private Button btnReactivar;

        // Grilla de datos
        private DataGridView solicitudesGrid;

        // Panel de estadísticas
        private GroupBox estadisticasGroupBox;
        private Label lblTotalTxt;
        private Label lblTotal;
        private Label lblCompletadasTxt;
        private Label lblCompletadas;
        private Label lblPendientesTxt;
        private Label lblPendientes;
        private Label lblUrgentesTxt;
        private Label lblUrgentes;
        private Label lblTotalEtiquetasTxt;
        private Label lblTotalEtiquetas;
        private Label lblEtiquetasFabricadasTxt;
        private Label lblEtiquetasFabricadas;
        private Label lblEtiquetasPendientesTxt;
        private Label lblEtiquetasPendientes;
        private Label lblPorcentajeAvanceTxt;
        private Label lblPorcentajeAvance;

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
            this.tipoEtiquetaComboBox = new ComboBox();
            this.lblTipoEtiqueta = new Label();
            this.estadoComboBox = new ComboBox();
            this.lblEstado = new Label();
            this.fechaDesdeDateTime = new DateTimePicker();
            this.lblFechaDesde = new Label();
            this.fechaHastaDateTime = new DateTimePicker();
            this.lblFechaHasta = new Label();
            this.ordenFabTextBox = new TextBox();
            this.lblOrdenFab = new Label();
            this.descripcionTextBox = new TextBox();
            this.lblDescripcion = new Label();
            this.autoFiltrarCheckBox = new CheckBox();
            this.btnBuscar = new Button();
            this.btnLimpiar = new Button();
            this.btnExportar = new Button();
            this.btnImprimir = new Button();
            this.btnEliminar = new Button();
            this.btnReactivar = new Button();
            this.solicitudesGrid = new DataGridView();
            this.estadisticasGroupBox = new GroupBox();
            this.lblTotalTxt = new Label();
            this.lblTotal = new Label();
            this.lblCompletadasTxt = new Label();
            this.lblCompletadas = new Label();
            this.lblPendientesTxt = new Label();
            this.lblPendientes = new Label();
            this.lblUrgentesTxt = new Label();
            this.lblUrgentes = new Label();
            this.lblTotalEtiquetasTxt = new Label();
            this.lblTotalEtiquetas = new Label();
            this.lblEtiquetasFabricadasTxt = new Label();
            this.lblEtiquetasFabricadas = new Label();
            this.lblEtiquetasPendientesTxt = new Label();
            this.lblEtiquetasPendientes = new Label();
            this.lblPorcentajeAvanceTxt = new Label();
            this.lblPorcentajeAvance = new Label();
            this.statusStrip = new StatusStrip();
            this.statusLabel = new ToolStripStatusLabel();

            this.filtrosGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.solicitudesGrid)).BeginInit();
            this.estadisticasGroupBox.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();

            // 
            // filtrosGroupBox
            // 
            this.filtrosGroupBox.Controls.Add(this.autoFiltrarCheckBox);
            this.filtrosGroupBox.Controls.Add(this.descripcionTextBox);
            this.filtrosGroupBox.Controls.Add(this.lblDescripcion);
            this.filtrosGroupBox.Controls.Add(this.ordenFabTextBox);
            this.filtrosGroupBox.Controls.Add(this.lblOrdenFab);
            this.filtrosGroupBox.Controls.Add(this.fechaHastaDateTime);
            this.filtrosGroupBox.Controls.Add(this.lblFechaHasta);
            this.filtrosGroupBox.Controls.Add(this.fechaDesdeDateTime);
            this.filtrosGroupBox.Controls.Add(this.lblFechaDesde);
            this.filtrosGroupBox.Controls.Add(this.estadoComboBox);
            this.filtrosGroupBox.Controls.Add(this.lblEstado);
            this.filtrosGroupBox.Controls.Add(this.tipoEtiquetaComboBox);
            this.filtrosGroupBox.Controls.Add(this.lblTipoEtiqueta);
            this.filtrosGroupBox.Location = new Point(12, 12);
            this.filtrosGroupBox.Name = "filtrosGroupBox";
            this.filtrosGroupBox.Size = new Size(950, 100);
            this.filtrosGroupBox.TabIndex = 0;
            this.filtrosGroupBox.TabStop = false;
            this.filtrosGroupBox.Text = "Filtros de Búsqueda";

            // 
            // lblTipoEtiqueta
            // 
            this.lblTipoEtiqueta.AutoSize = true;
            this.lblTipoEtiqueta.Location = new Point(15, 25);
            this.lblTipoEtiqueta.Name = "lblTipoEtiqueta";
            this.lblTipoEtiqueta.Size = new Size(76, 13);
            this.lblTipoEtiqueta.TabIndex = 0;
            this.lblTipoEtiqueta.Text = "Tipo Etiqueta:";

            // 
            // tipoEtiquetaComboBox
            // 
            this.tipoEtiquetaComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            this.tipoEtiquetaComboBox.FormattingEnabled = true;
            this.tipoEtiquetaComboBox.Location = new Point(15, 45);
            this.tipoEtiquetaComboBox.Name = "tipoEtiquetaComboBox";
            this.tipoEtiquetaComboBox.Size = new Size(120, 21);
            this.tipoEtiquetaComboBox.TabIndex = 1;

            // 
            // lblEstado
            // 
            this.lblEstado.AutoSize = true;
            this.lblEstado.Location = new Point(150, 25);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new Size(43, 13);
            this.lblEstado.TabIndex = 2;
            this.lblEstado.Text = "Estado:";

            // 
            // estadoComboBox
            // 
            this.estadoComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            this.estadoComboBox.FormattingEnabled = true;
            this.estadoComboBox.Location = new Point(150, 45);
            this.estadoComboBox.Name = "estadoComboBox";
            this.estadoComboBox.Size = new Size(120, 21);
            this.estadoComboBox.TabIndex = 3;

            // 
            // lblFechaDesde
            // 
            this.lblFechaDesde.AutoSize = true;
            this.lblFechaDesde.Location = new Point(285, 25);
            this.lblFechaDesde.Name = "lblFechaDesde";
            this.lblFechaDesde.Size = new Size(70, 13);
            this.lblFechaDesde.TabIndex = 4;
            this.lblFechaDesde.Text = "Fecha desde:";

            // 
            // fechaDesdeDateTime
            // 
            this.fechaDesdeDateTime.Format = DateTimePickerFormat.Short;
            this.fechaDesdeDateTime.Location = new Point(285, 45);
            this.fechaDesdeDateTime.Name = "fechaDesdeDateTime";
            this.fechaDesdeDateTime.Size = new Size(100, 20);
            this.fechaDesdeDateTime.TabIndex = 5;

            // 
            // lblFechaHasta
            // 
            this.lblFechaHasta.AutoSize = true;
            this.lblFechaHasta.Location = new Point(400, 25);
            this.lblFechaHasta.Name = "lblFechaHasta";
            this.lblFechaHasta.Size = new Size(68, 13);
            this.lblFechaHasta.TabIndex = 6;
            this.lblFechaHasta.Text = "Fecha hasta:";

            // 
            // fechaHastaDateTime
            // 
            this.fechaHastaDateTime.Format = DateTimePickerFormat.Short;
            this.fechaHastaDateTime.Location = new Point(400, 45);
            this.fechaHastaDateTime.Name = "fechaHastaDateTime";
            this.fechaHastaDateTime.Size = new Size(100, 20);
            this.fechaHastaDateTime.TabIndex = 7;

            // 
            // lblOrdenFab
            // 
            this.lblOrdenFab.AutoSize = true;
            this.lblOrdenFab.Location = new Point(520, 25);
            this.lblOrdenFab.Name = "lblOrdenFab";
            this.lblOrdenFab.Size = new Size(64, 13);
            this.lblOrdenFab.TabIndex = 8;
            this.lblOrdenFab.Text = "Orden Fab.:";

            // 
            // ordenFabTextBox
            // 
            this.ordenFabTextBox.Location = new Point(520, 45);
            this.ordenFabTextBox.Name = "ordenFabTextBox";
            this.ordenFabTextBox.Size = new Size(120, 20);
            this.ordenFabTextBox.TabIndex = 9;

            // 
            // lblDescripcion
            // 
            this.lblDescripcion.AutoSize = true;
            this.lblDescripcion.Location = new Point(655, 25);
            this.lblDescripcion.Name = "lblDescripcion";
            this.lblDescripcion.Size = new Size(66, 13);
            this.lblDescripcion.TabIndex = 10;
            this.lblDescripcion.Text = "Descripción:";

            // 
            // descripcionTextBox
            // 
            this.descripcionTextBox.Location = new Point(655, 45);
            this.descripcionTextBox.Name = "descripcionTextBox";
            this.descripcionTextBox.Size = new Size(150, 20);
            this.descripcionTextBox.TabIndex = 11;

            // 
            // autoFiltrarCheckBox
            // 
            this.autoFiltrarCheckBox.AutoSize = true;
            this.autoFiltrarCheckBox.Checked = true;
            this.autoFiltrarCheckBox.CheckState = CheckState.Checked;
            this.autoFiltrarCheckBox.Location = new Point(820, 47);
            this.autoFiltrarCheckBox.Name = "autoFiltrarCheckBox";
            this.autoFiltrarCheckBox.Size = new Size(75, 17);
            this.autoFiltrarCheckBox.TabIndex = 12;
            this.autoFiltrarCheckBox.Text = "Auto filtrar";
            this.autoFiltrarCheckBox.UseVisualStyleBackColor = true;

            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new Point(980, 30);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new Size(80, 30);
            this.btnBuscar.TabIndex = 1;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = true;

            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Location = new Point(980, 70);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new Size(80, 30);
            this.btnLimpiar.TabIndex = 2;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = true;

            // 
            // solicitudesGrid
            // 
            this.solicitudesGrid.AllowUserToAddRows = false;
            this.solicitudesGrid.AllowUserToDeleteRows = false;
            this.solicitudesGrid.Anchor = ((AnchorStyles)((((AnchorStyles.Top | AnchorStyles.Bottom)
            | AnchorStyles.Left)
            | AnchorStyles.Right)));
            this.solicitudesGrid.BackgroundColor = SystemColors.Window;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            this.solicitudesGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.solicitudesGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;

            // Configurar columnas
            this.solicitudesGrid.Columns.Add("ColId", "ID");
            this.solicitudesGrid.Columns.Add("ColOrdenFab", "Orden Fab.");
            this.solicitudesGrid.Columns.Add("ColDescripcion", "Descripción");
            this.solicitudesGrid.Columns.Add("ColTipo", "Tipo");
            this.solicitudesGrid.Columns.Add("ColCantPedida", "Cant. Pedida");
            this.solicitudesGrid.Columns.Add("ColCantFabricada", "Cant. Fabricada");
            this.solicitudesGrid.Columns.Add("ColCantPendiente", "Cant. Pendiente");
            this.solicitudesGrid.Columns.Add("ColColor", "Color");
            this.solicitudesGrid.Columns.Add("ColEstado", "Estado");
            this.solicitudesGrid.Columns.Add("ColFechaSolicitud", "Fecha Solicitud");
            this.solicitudesGrid.Columns.Add("ColFechaRequerida", "Fecha Requerida");
            this.solicitudesGrid.Columns.Add("ColFechaFabricacion", "Fecha Fabricación");
            this.solicitudesGrid.Columns.Add("ColUsuario", "Usuario");
            this.solicitudesGrid.Columns.Add("ColObservaciones", "Observaciones");

            // Configurar anchos de columnas
            this.solicitudesGrid.Columns[0].Width = 60;   // ID
            this.solicitudesGrid.Columns[1].Width = 100;  // Orden Fab
            this.solicitudesGrid.Columns[2].Width = 200;  // Descripción
            this.solicitudesGrid.Columns[3].Width = 80;   // Tipo
            this.solicitudesGrid.Columns[4].Width = 80;   // Cant. Pedida
            this.solicitudesGrid.Columns[5].Width = 80;   // Cant. Fabricada
            this.solicitudesGrid.Columns[6].Width = 80;   // Cant. Pendiente
            this.solicitudesGrid.Columns[7].Width = 70;   // Color
            this.solicitudesGrid.Columns[8].Width = 90;   // Estado
            this.solicitudesGrid.Columns[9].Width = 90;   // Fecha Solicitud
            this.solicitudesGrid.Columns[10].Width = 90;  // Fecha Requerida
            this.solicitudesGrid.Columns[11].Width = 90;  // Fecha Fabricación
            this.solicitudesGrid.Columns[12].Width = 80;  // Usuario
            this.solicitudesGrid.Columns[13].Width = 150; // Observaciones

            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            this.solicitudesGrid.DefaultCellStyle = dataGridViewCellStyle2;
            this.solicitudesGrid.Location = new Point(12, 130);
            this.solicitudesGrid.MultiSelect = false;
            this.solicitudesGrid.Name = "solicitudesGrid";
            this.solicitudesGrid.ReadOnly = true;
            this.solicitudesGrid.RowHeadersVisible = false;
            this.solicitudesGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.solicitudesGrid.Size = new Size(1050, 350);
            this.solicitudesGrid.TabIndex = 3;

            // 
            // estadisticasGroupBox
            // 
            this.estadisticasGroupBox.Anchor = ((AnchorStyles)(((AnchorStyles.Bottom | AnchorStyles.Left)
            | AnchorStyles.Right)));
            this.estadisticasGroupBox.Controls.Add(this.lblPorcentajeAvance);
            this.estadisticasGroupBox.Controls.Add(this.lblPorcentajeAvanceTxt);
            this.estadisticasGroupBox.Controls.Add(this.lblEtiquetasPendientes);
            this.estadisticasGroupBox.Controls.Add(this.lblEtiquetasPendientesTxt);
            this.estadisticasGroupBox.Controls.Add(this.lblEtiquetasFabricadas);
            this.estadisticasGroupBox.Controls.Add(this.lblEtiquetasFabricadasTxt);
            this.estadisticasGroupBox.Controls.Add(this.lblTotalEtiquetas);
            this.estadisticasGroupBox.Controls.Add(this.lblTotalEtiquetasTxt);
            this.estadisticasGroupBox.Controls.Add(this.lblUrgentes);
            this.estadisticasGroupBox.Controls.Add(this.lblUrgentesTxt);
            this.estadisticasGroupBox.Controls.Add(this.lblPendientes);
            this.estadisticasGroupBox.Controls.Add(this.lblPendientesTxt);
            this.estadisticasGroupBox.Controls.Add(this.lblCompletadas);
            this.estadisticasGroupBox.Controls.Add(this.lblCompletadasTxt);
            this.estadisticasGroupBox.Controls.Add(this.lblTotal);
            this.estadisticasGroupBox.Controls.Add(this.lblTotalTxt);
            this.estadisticasGroupBox.Location = new Point(12, 490);
            this.estadisticasGroupBox.Name = "estadisticasGroupBox";
            this.estadisticasGroupBox.Size = new Size(800, 80);
            this.estadisticasGroupBox.TabIndex = 4;
            this.estadisticasGroupBox.TabStop = false;
            this.estadisticasGroupBox.Text = "Estadísticas";

            // Fila 1 de estadísticas
            this.lblTotalTxt.AutoSize = true;
            this.lblTotalTxt.Location = new Point(15, 25);
            this.lblTotalTxt.Name = "lblTotalTxt";
            this.lblTotalTxt.Size = new Size(34, 13);
            this.lblTotalTxt.TabIndex = 0;
            this.lblTotalTxt.Text = "Total:";

            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
            this.lblTotal.Location = new Point(55, 25);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new Size(14, 13);
            this.lblTotal.TabIndex = 1;
            this.lblTotal.Text = "0";

            this.lblCompletadasTxt.AutoSize = true;
            this.lblCompletadasTxt.Location = new Point(120, 25);
            this.lblCompletadasTxt.Name = "lblCompletadasTxt";
            this.lblCompletadasTxt.Size = new Size(70, 13);
            this.lblCompletadasTxt.TabIndex = 2;
            this.lblCompletadasTxt.Text = "Completadas:";

            this.lblCompletadas.AutoSize = true;
            this.lblCompletadas.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
            this.lblCompletadas.ForeColor = Color.DarkGreen;
            this.lblCompletadas.Location = new Point(195, 25);
            this.lblCompletadas.Name = "lblCompletadas";
            this.lblCompletadas.Size = new Size(14, 13);
            this.lblCompletadas.TabIndex = 3;
            this.lblCompletadas.Text = "0";

            this.lblPendientesTxt.AutoSize = true;
            this.lblPendientesTxt.Location = new Point(260, 25);
            this.lblPendientesTxt.Name = "lblPendientesTxt";
            this.lblPendientesTxt.Size = new Size(63, 13);
            this.lblPendientesTxt.TabIndex = 4;
            this.lblPendientesTxt.Text = "Pendientes:";

            this.lblPendientes.AutoSize = true;
            this.lblPendientes.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
            this.lblPendientes.ForeColor = Color.DarkBlue;
            this.lblPendientes.Location = new Point(330, 25);
            this.lblPendientes.Name = "lblPendientes";
            this.lblPendientes.Size = new Size(14, 13);
            this.lblPendientes.TabIndex = 5;
            this.lblPendientes.Text = "0";

            this.lblUrgentesTxt.AutoSize = true;
            this.lblUrgentesTxt.Location = new Point(395, 25);
            this.lblUrgentesTxt.Name = "lblUrgentesTxt";
            this.lblUrgentesTxt.Size = new Size(52, 13);
            this.lblUrgentesTxt.TabIndex = 6;
            this.lblUrgentesTxt.Text = "Urgentes:";

            this.lblUrgentes.AutoSize = true;
            this.lblUrgentes.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
            this.lblUrgentes.ForeColor = Color.Red;
            this.lblUrgentes.Location = new Point(450, 25);
            this.lblUrgentes.Name = "lblUrgentes";
            this.lblUrgentes.Size = new Size(14, 13);
            this.lblUrgentes.TabIndex = 7;
            this.lblUrgentes.Text = "0";

            // Fila 2 de estadísticas
            this.lblTotalEtiquetasTxt.AutoSize = true;
            this.lblTotalEtiquetasTxt.Location = new Point(15, 50);
            this.lblTotalEtiquetasTxt.Name = "lblTotalEtiquetasTxt";
            this.lblTotalEtiquetasTxt.Size = new Size(82, 13);
            this.lblTotalEtiquetasTxt.TabIndex = 8;
            this.lblTotalEtiquetasTxt.Text = "Total Etiquetas:";

            this.lblTotalEtiquetas.AutoSize = true;
            this.lblTotalEtiquetas.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
            this.lblTotalEtiquetas.Location = new Point(100, 50);
            this.lblTotalEtiquetas.Name = "lblTotalEtiquetas";
            this.lblTotalEtiquetas.Size = new Size(14, 13);
            this.lblTotalEtiquetas.TabIndex = 9;
            this.lblTotalEtiquetas.Text = "0";

            this.lblEtiquetasFabricadasTxt.AutoSize = true;
            this.lblEtiquetasFabricadasTxt.Location = new Point(180, 50);
            this.lblEtiquetasFabricadasTxt.Name = "lblEtiquetasFabricadasTxt";
            this.lblEtiquetasFabricadasTxt.Size = new Size(66, 13);
            this.lblEtiquetasFabricadasTxt.TabIndex = 10;
            this.lblEtiquetasFabricadasTxt.Text = "Fabricadas:";

            this.lblEtiquetasFabricadas.AutoSize = true;
            this.lblEtiquetasFabricadas.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
            this.lblEtiquetasFabricadas.ForeColor = Color.DarkGreen;
            this.lblEtiquetasFabricadas.Location = new Point(250, 50);
            this.lblEtiquetasFabricadas.Name = "lblEtiquetasFabricadas";
            this.lblEtiquetasFabricadas.Size = new Size(14, 13);
            this.lblEtiquetasFabricadas.TabIndex = 11;
            this.lblEtiquetasFabricadas.Text = "0";

            this.lblEtiquetasPendientesTxt.AutoSize = true;
            this.lblEtiquetasPendientesTxt.Location = new Point(320, 50);
            this.lblEtiquetasPendientesTxt.Name = "lblEtiquetasPendientesTxt";
            this.lblEtiquetasPendientesTxt.Size = new Size(63, 13);
            this.lblEtiquetasPendientesTxt.TabIndex = 12;
            this.lblEtiquetasPendientesTxt.Text = "Pendientes:";

            this.lblEtiquetasPendientes.AutoSize = true;
            this.lblEtiquetasPendientes.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
            this.lblEtiquetasPendientes.ForeColor = Color.DarkBlue;
            this.lblEtiquetasPendientes.Location = new Point(390, 50);
            this.lblEtiquetasPendientes.Name = "lblEtiquetasPendientes";
            this.lblEtiquetasPendientes.Size = new Size(14, 13);
            this.lblEtiquetasPendientes.TabIndex = 13;
            this.lblEtiquetasPendientes.Text = "0";

            this.lblPorcentajeAvanceTxt.AutoSize = true;
            this.lblPorcentajeAvanceTxt.Location = new Point(460, 50);
            this.lblPorcentajeAvanceTxt.Name = "lblPorcentajeAvanceTxt";
            this.lblPorcentajeAvanceTxt.Size = new Size(50, 13);
            this.lblPorcentajeAvanceTxt.TabIndex = 14;
            this.lblPorcentajeAvanceTxt.Text = "Avance:";

            this.lblPorcentajeAvance.AutoSize = true;
            this.lblPorcentajeAvance.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
            this.lblPorcentajeAvance.ForeColor = Color.DarkGreen;
            this.lblPorcentajeAvance.Location = new Point(515, 50);
            this.lblPorcentajeAvance.Name = "lblPorcentajeAvance";
            this.lblPorcentajeAvance.Size = new Size(27, 13);
            this.lblPorcentajeAvance.TabIndex = 15;
            this.lblPorcentajeAvance.Text = "0%";

            // Botones de acción
            this.btnExportar.Anchor = ((AnchorStyles)((AnchorStyles.Bottom | AnchorStyles.Right)));
            this.btnExportar.Location = new Point(830, 500);
            this.btnExportar.Name = "btnExportar";
            this.btnExportar.Size = new Size(80, 30);
            this.btnExportar.TabIndex = 5;
            this.btnExportar.Text = "Exportar";
            this.btnExportar.UseVisualStyleBackColor = true;

            this.btnImprimir.Anchor = ((AnchorStyles)((AnchorStyles.Bottom | AnchorStyles.Right)));
            this.btnImprimir.BackColor = Color.LightGreen;
            this.btnImprimir.Enabled = false;
            this.btnImprimir.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
            this.btnImprimir.Location = new Point(920, 500);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new Size(80, 30);
            this.btnImprimir.TabIndex = 6;
            this.btnImprimir.Text = "Imprimir";
            this.btnImprimir.UseVisualStyleBackColor = false;

            this.btnEliminar.Anchor = ((AnchorStyles)((AnchorStyles.Bottom | AnchorStyles.Right)));
            this.btnEliminar.BackColor = Color.LightCoral;
            this.btnEliminar.Enabled = false;
            this.btnEliminar.Location = new Point(830, 540);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new Size(80, 30);
            this.btnEliminar.TabIndex = 7;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = false;

            this.btnReactivar.Anchor = ((AnchorStyles)((AnchorStyles.Bottom | AnchorStyles.Right)));
            this.btnReactivar.BackColor = Color.LightBlue;
            this.btnReactivar.Enabled = false;
            this.btnReactivar.Location = new Point(920, 540);
            this.btnReactivar.Name = "btnReactivar";
            this.btnReactivar.Size = new Size(80, 30);
            this.btnReactivar.TabIndex = 8;
            this.btnReactivar.Text = "Reactivar";
            this.btnReactivar.UseVisualStyleBackColor = false;

            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new ToolStripItem[] {
            this.statusLabel});
            this.statusStrip.Location = new Point(0, 588);
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
            // ConsultaSolicitudesForm
            // 
            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(1084, 610);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.btnReactivar);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.btnImprimir);
            this.Controls.Add(this.btnExportar);
            this.Controls.Add(this.estadisticasGroupBox);
            this.Controls.Add(this.solicitudesGrid);
            this.Controls.Add(this.btnLimpiar);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.filtrosGroupBox);
            this.Name = "ConsultaSolicitudesForm";
            this.Text = "Consulta de Solicitudes de Etiquetas";

            this.filtrosGroupBox.ResumeLayout(false);
            this.filtrosGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.solicitudesGrid)).EndInit();
            this.estadisticasGroupBox.ResumeLayout(false);
            this.estadisticasGroupBox.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}