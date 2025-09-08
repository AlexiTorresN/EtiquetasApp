using System;
using System.Drawing;
using System.Windows.Forms;

namespace EtiquetasApp.Forms
{
    partial class BaseEtiquetaForm
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        // Controles principales
        protected DataGridView solicitudesGrid;
        protected StatusStrip statusStrip;
        protected ToolStripStatusLabel statusLabel;

        // Panel de configuración de etiquetas
        protected GroupBox etiquetaGroupBox;
        protected Label lblTipoEtiqueta;
        protected ComboBox papelComboBox;
        protected Label lblPapel;
        protected ComboBox velocidadComboBox;
        protected Label lblVelocidad;
        protected ComboBox temperaturaComboBox;
        protected Label lblTemperatura;
        protected CheckBox etiquetaDobleCheckBox;

        // Panel de posiciones
        protected GroupBox posicionesGroupBox;
        protected TextBox pos1TextBox;
        protected TextBox pos2TextBox;
        protected TextBox pos3TextBox;
        protected TextBox pos4TextBox;
        protected TextBox pos5TextBox;
        protected Label lbl1;
        protected Label lbl2;
        protected Label lbl3;
        protected Label lbl4;
        protected Label lbl5;
        protected Button btnAjustarIzq;
        protected Button btnAjustarDer;

        // Panel de información de solicitud
        protected GroupBox infoGroupBox;
        protected Label lblIdSolicitudTxt;
        protected Label lblIdSolicitud;
        protected Label lblOrdenFabTxt;
        protected Label lblOrdenFab;
        protected Label lblDescripcionTxt;
        protected Label lblDescripcion;
        protected Label lblCantidadTxt;
        protected Label lblCantidad;
        protected Label lblUPCTxt;
        protected Label lblUPC;
        protected Label lblUPC2Txt;
        protected Label lblUPC2;

        // Botones de acción
        protected Button btnImprimir;
        protected Button btnPrueba;
        protected Button btnRefrescar;

        // Panel de colores (para mostrar las etiquetas)
        protected Panel coloresPanel;
        protected Button[] colorButtons;

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

            this.solicitudesGrid = new DataGridView();
            this.statusStrip = new StatusStrip();
            this.statusLabel = new ToolStripStatusLabel();
            this.etiquetaGroupBox = new GroupBox();
            this.lblTipoEtiqueta = new Label();
            this.papelComboBox = new ComboBox();
            this.lblPapel = new Label();
            this.velocidadComboBox = new ComboBox();
            this.lblVelocidad = new Label();
            this.temperaturaComboBox = new ComboBox();
            this.lblTemperatura = new Label();
            this.etiquetaDobleCheckBox = new CheckBox();
            this.posicionesGroupBox = new GroupBox();
            this.pos1TextBox = new TextBox();
            this.pos2TextBox = new TextBox();
            this.pos3TextBox = new TextBox();
            this.pos4TextBox = new TextBox();
            this.pos5TextBox = new TextBox();
            this.lbl1 = new Label();
            this.lbl2 = new Label();
            this.lbl3 = new Label();
            this.lbl4 = new Label();
            this.lbl5 = new Label();
            this.btnAjustarIzq = new Button();
            this.btnAjustarDer = new Button();
            this.infoGroupBox = new GroupBox();
            this.lblIdSolicitudTxt = new Label();
            this.lblIdSolicitud = new Label();
            this.lblOrdenFabTxt = new Label();
            this.lblOrdenFab = new Label();
            this.lblDescripcionTxt = new Label();
            this.lblDescripcion = new Label();
            this.lblCantidadTxt = new Label();
            this.lblCantidad = new Label();
            this.lblUPCTxt = new Label();
            this.lblUPC = new Label();
            this.lblUPC2Txt = new Label();
            this.lblUPC2 = new Label();
            this.btnImprimir = new Button();
            this.btnPrueba = new Button();
            this.btnRefrescar = new Button();
            this.coloresPanel = new Panel();

            ((System.ComponentModel.ISupportInitialize)(this.solicitudesGrid)).BeginInit();
            this.statusStrip.SuspendLayout();
            this.etiquetaGroupBox.SuspendLayout();
            this.posicionesGroupBox.SuspendLayout();
            this.infoGroupBox.SuspendLayout();
            this.SuspendLayout();

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
            this.solicitudesGrid.Columns.Add("ColIdSolicitud", "N° Sol.");
            this.solicitudesGrid.Columns.Add("ColOrdenFab", "Orden Fab.");
            this.solicitudesGrid.Columns.Add("ColDescripcion", "Descripción");
            this.solicitudesGrid.Columns.Add("ColCantidad", "Cantidad");
            this.solicitudesGrid.Columns.Add("ColColor", "Color");
            this.solicitudesGrid.Columns.Add("ColObservaciones", "Observaciones");
            this.solicitudesGrid.Columns.Add("ColFecha", "Fecha Req.");
            this.solicitudesGrid.Columns[0].Width = 80;
            this.solicitudesGrid.Columns[1].Width = 120;
            this.solicitudesGrid.Columns[2].Width = 200;
            this.solicitudesGrid.Columns[3].Width = 80;
            this.solicitudesGrid.Columns[4].Width = 80;
            this.solicitudesGrid.Columns[5].Width = 150;
            this.solicitudesGrid.Columns[6].Width = 100;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            this.solicitudesGrid.DefaultCellStyle = dataGridViewCellStyle2;
            this.solicitudesGrid.Location = new Point(12, 12);
            this.solicitudesGrid.MultiSelect = false;
            this.solicitudesGrid.Name = "solicitudesGrid";
            this.solicitudesGrid.ReadOnly = true;
            this.solicitudesGrid.RowHeadersVisible = false;
            this.solicitudesGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.solicitudesGrid.Size = new Size(810, 300);
            this.solicitudesGrid.TabIndex = 0;

            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new ToolStripItem[] {
            this.statusLabel});
            this.statusStrip.Location = new Point(0, 639);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new Size(1084, 22);
            this.statusStrip.TabIndex = 1;
            this.statusStrip.Text = "statusStrip1";

            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new Size(35, 17);
            this.statusLabel.Text = "Listo";

            // 
            // etiquetaGroupBox
            // 
            this.etiquetaGroupBox.Controls.Add(this.lblTipoEtiqueta);
            this.etiquetaGroupBox.Controls.Add(this.papelComboBox);
            this.etiquetaGroupBox.Controls.Add(this.lblPapel);
            this.etiquetaGroupBox.Controls.Add(this.velocidadComboBox);
            this.etiquetaGroupBox.Controls.Add(this.lblVelocidad);
            this.etiquetaGroupBox.Controls.Add(this.temperaturaComboBox);
            this.etiquetaGroupBox.Controls.Add(this.lblTemperatura);
            this.etiquetaGroupBox.Controls.Add(this.etiquetaDobleCheckBox);
            this.etiquetaGroupBox.Location = new Point(12, 330);
            this.etiquetaGroupBox.Name = "etiquetaGroupBox";
            this.etiquetaGroupBox.Size = new Size(300, 120);
            this.etiquetaGroupBox.TabIndex = 2;
            this.etiquetaGroupBox.TabStop = false;
            this.etiquetaGroupBox.Text = "Configuración de Etiquetas";

            // 
            // lblTipoEtiqueta
            // 
            this.lblTipoEtiqueta.AutoSize = true;
            this.lblTipoEtiqueta.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point);
            this.lblTipoEtiqueta.Location = new Point(15, 20);
            this.lblTipoEtiqueta.Name = "lblTipoEtiqueta";
            this.lblTipoEtiqueta.Size = new Size(120, 15);
            this.lblTipoEtiqueta.TabIndex = 0;
            this.lblTipoEtiqueta.Text = "Etiquetas C/BCO-E";

            // 
            // papelComboBox
            // 
            this.papelComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            this.papelComboBox.FormattingEnabled = true;
            this.papelComboBox.Location = new Point(60, 45);
            this.papelComboBox.Name = "papelComboBox";
            this.papelComboBox.Size = new Size(100, 21);
            this.papelComboBox.TabIndex = 2;

            // 
            // lblPapel
            // 
            this.lblPapel.AutoSize = true;
            this.lblPapel.Location = new Point(15, 48);
            this.lblPapel.Name = "lblPapel";
            this.lblPapel.Size = new Size(34, 13);
            this.lblPapel.TabIndex = 1;
            this.lblPapel.Text = "Papel";

            // 
            // velocidadComboBox
            // 
            this.velocidadComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            this.velocidadComboBox.FormattingEnabled = true;
            this.velocidadComboBox.Items.AddRange(new object[] { "1", "2", "3", "4", "5", "6", "7", "8" });
            this.velocidadComboBox.Location = new Point(230, 45);
            this.velocidadComboBox.Name = "velocidadComboBox";
            this.velocidadComboBox.Size = new Size(50, 21);
            this.velocidadComboBox.TabIndex = 4;
            this.velocidadComboBox.SelectedIndex = 3; // Velocidad 4 por defecto

            // 
            // lblVelocidad
            // 
            this.lblVelocidad.AutoSize = true;
            this.lblVelocidad.Location = new Point(175, 48);
            this.lblVelocidad.Name = "lblVelocidad";
            this.lblVelocidad.Size = new Size(54, 13);
            this.lblVelocidad.TabIndex = 3;
            this.lblVelocidad.Text = "Velocidad";

            // 
            // temperaturaComboBox
            // 
            this.temperaturaComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            this.temperaturaComboBox.FormattingEnabled = true;
            this.temperaturaComboBox.Items.AddRange(new object[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30" });
            this.temperaturaComboBox.Location = new Point(230, 75);
            this.temperaturaComboBox.Name = "temperaturaComboBox";
            this.temperaturaComboBox.Size = new Size(50, 21);
            this.temperaturaComboBox.TabIndex = 6;
            this.temperaturaComboBox.SelectedIndex = 5; // Temperatura 6 por defecto

            // 
            // lblTemperatura
            // 
            this.lblTemperatura.AutoSize = true;
            this.lblTemperatura.Location = new Point(175, 78);
            this.lblTemperatura.Name = "lblTemperatura";
            this.lblTemperatura.Size = new Size(67, 13);
            this.lblTemperatura.TabIndex = 5;
            this.lblTemperatura.Text = "Temperatura";

            // 
            // etiquetaDobleCheckBox
            // 
            this.etiquetaDobleCheckBox.AutoSize = true;
            this.etiquetaDobleCheckBox.Location = new Point(15, 75);
            this.etiquetaDobleCheckBox.Name = "etiquetaDobleCheckBox";
            this.etiquetaDobleCheckBox.Size = new Size(98, 17);
            this.etiquetaDobleCheckBox.TabIndex = 7;
            this.etiquetaDobleCheckBox.Text = "Etiqueta Doble";
            this.etiquetaDobleCheckBox.UseVisualStyleBackColor = true;

            // 
            // posicionesGroupBox
            // 
            this.posicionesGroupBox.Controls.Add(this.pos1TextBox);
            this.posicionesGroupBox.Controls.Add(this.pos2TextBox);
            this.posicionesGroupBox.Controls.Add(this.pos3TextBox);
            this.posicionesGroupBox.Controls.Add(this.pos4TextBox);
            this.posicionesGroupBox.Controls.Add(this.pos5TextBox);
            this.posicionesGroupBox.Controls.Add(this.lbl1);
            this.posicionesGroupBox.Controls.Add(this.lbl2);
            this.posicionesGroupBox.Controls.Add(this.lbl3);
            this.posicionesGroupBox.Controls.Add(this.lbl4);
            this.posicionesGroupBox.Controls.Add(this.lbl5);
            this.posicionesGroupBox.Controls.Add(this.btnAjustarIzq);
            this.posicionesGroupBox.Controls.Add(this.btnAjustarDer);
            this.posicionesGroupBox.Location = new Point(330, 330);
            this.posicionesGroupBox.Name = "posicionesGroupBox";
            this.posicionesGroupBox.Size = new Size(320, 120);
            this.posicionesGroupBox.TabIndex = 3;
            this.posicionesGroupBox.TabStop = false;
            this.posicionesGroupBox.Text = "Posición de columnas";

            // Posiciones TextBox y Labels
            this.lbl1.AutoSize = true;
            this.lbl1.Location = new Point(15, 25);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new Size(13, 13);
            this.lbl1.TabIndex = 0;
            this.lbl1.Text = "1";

            this.pos1TextBox.Location = new Point(15, 45);
            this.pos1TextBox.Name = "pos1TextBox";
            this.pos1TextBox.Size = new Size(50, 20);
            this.pos1TextBox.TabIndex = 1;
            this.pos1TextBox.Text = "4";
            this.pos1TextBox.TextAlign = HorizontalAlignment.Center;

            this.lbl2.AutoSize = true;
            this.lbl2.Location = new Point(75, 25);
            this.lbl2.Name = "lbl2";
            this.lbl2.Size = new Size(13, 13);
            this.lbl2.TabIndex = 2;
            this.lbl2.Text = "2";

            this.pos2TextBox.Location = new Point(75, 45);
            this.pos2TextBox.Name = "pos2TextBox";
            this.pos2TextBox.Size = new Size(50, 20);
            this.pos2TextBox.TabIndex = 3;
            this.pos2TextBox.Text = "148";
            this.pos2TextBox.TextAlign = HorizontalAlignment.Center;

            this.lbl3.AutoSize = true;
            this.lbl3.Location = new Point(135, 25);
            this.lbl3.Name = "lbl3";
            this.lbl3.Size = new Size(13, 13);
            this.lbl3.TabIndex = 4;
            this.lbl3.Text = "3";

            this.pos3TextBox.Location = new Point(135, 45);
            this.pos3TextBox.Name = "pos3TextBox";
            this.pos3TextBox.Size = new Size(50, 20);
            this.pos3TextBox.TabIndex = 5;
            this.pos3TextBox.Text = "292";
            this.pos3TextBox.TextAlign = HorizontalAlignment.Center;

            this.lbl4.AutoSize = true;
            this.lbl4.Location = new Point(195, 25);
            this.lbl4.Name = "lbl4";
            this.lbl4.Size = new Size(13, 13);
            this.lbl4.TabIndex = 6;
            this.lbl4.Text = "4";

            this.pos4TextBox.Location = new Point(195, 45);
            this.pos4TextBox.Name = "pos4TextBox";
            this.pos4TextBox.Size = new Size(50, 20);
            this.pos4TextBox.TabIndex = 7;
            this.pos4TextBox.Text = "436";
            this.pos4TextBox.TextAlign = HorizontalAlignment.Center;

            this.lbl5.AutoSize = true;
            this.lbl5.Location = new Point(255, 25);
            this.lbl5.Name = "lbl5";
            this.lbl5.Size = new Size(13, 13);
            this.lbl5.TabIndex = 8;
            this.lbl5.Text = "5";

            this.pos5TextBox.Location = new Point(255, 45);
            this.pos5TextBox.Name = "pos5TextBox";
            this.pos5TextBox.Size = new Size(50, 20);
            this.pos5TextBox.TabIndex = 9;
            this.pos5TextBox.Text = "580";
            this.pos5TextBox.TextAlign = HorizontalAlignment.Center;

            // 
            // btnAjustarIzq
            // 
            this.btnAjustarIzq.Location = new Point(15, 85);
            this.btnAjustarIzq.Name = "btnAjustarIzq";
            this.btnAjustarIzq.Size = new Size(30, 23);
            this.btnAjustarIzq.TabIndex = 10;
            this.btnAjustarIzq.Text = "<<";
            this.btnAjustarIzq.UseVisualStyleBackColor = true;

            // 
            // btnAjustarDer
            // 
            this.btnAjustarDer.Location = new Point(55, 85);
            this.btnAjustarDer.Name = "btnAjustarDer";
            this.btnAjustarDer.Size = new Size(30, 23);
            this.btnAjustarDer.TabIndex = 11;
            this.btnAjustarDer.Text = ">>";
            this.btnAjustarDer.UseVisualStyleBackColor = true;

            // 
            // infoGroupBox
            // 
            this.infoGroupBox.Controls.Add(this.lblIdSolicitudTxt);
            this.infoGroupBox.Controls.Add(this.lblIdSolicitud);
            this.infoGroupBox.Controls.Add(this.lblOrdenFabTxt);
            this.infoGroupBox.Controls.Add(this.lblOrdenFab);
            this.infoGroupBox.Controls.Add(this.lblDescripcionTxt);
            this.infoGroupBox.Controls.Add(this.lblDescripcion);
            this.infoGroupBox.Controls.Add(this.lblCantidadTxt);
            this.infoGroupBox.Controls.Add(this.lblCantidad);
            this.infoGroupBox.Controls.Add(this.lblUPCTxt);
            this.infoGroupBox.Controls.Add(this.lblUPC);
            this.infoGroupBox.Controls.Add(this.lblUPC2Txt);
            this.infoGroupBox.Controls.Add(this.lblUPC2);
            this.infoGroupBox.Location = new Point(670, 330);
            this.infoGroupBox.Name = "infoGroupBox";
            this.infoGroupBox.Size = new Size(220, 180);
            this.infoGroupBox.TabIndex = 4;
            this.infoGroupBox.TabStop = false;
            this.infoGroupBox.Text = "Información";

            // Labels de información
            this.lblIdSolicitudTxt.AutoSize = true;
            this.lblIdSolicitudTxt.Location = new Point(15, 25);
            this.lblIdSolicitudTxt.Name = "lblIdSolicitudTxt";
            this.lblIdSolicitudTxt.Size = new Size(48, 13);
            this.lblIdSolicitudTxt.TabIndex = 0;
            this.lblIdSolicitudTxt.Text = "N° Sol.:";

            this.lblIdSolicitud.AutoSize = true;
            this.lblIdSolicitud.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
            this.lblIdSolicitud.Location = new Point(80, 25);
            this.lblIdSolicitud.Name = "lblIdSolicitud";
            this.lblIdSolicitud.Size = new Size(11, 13);
            this.lblIdSolicitud.TabIndex = 1;
            this.lblIdSolicitud.Text = "-";

            this.lblOrdenFabTxt.AutoSize = true;
            this.lblOrdenFabTxt.Location = new Point(15, 45);
            this.lblOrdenFabTxt.Name = "lblOrdenFabTxt";
            this.lblOrdenFabTxt.Size = new Size(64, 13);
            this.lblOrdenFabTxt.TabIndex = 2;
            this.lblOrdenFabTxt.Text = "Orden Fab.:";

            this.lblOrdenFab.AutoSize = true;
            this.lblOrdenFab.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
            this.lblOrdenFab.Location = new Point(80, 45);
            this.lblOrdenFab.Name = "lblOrdenFab";
            this.lblOrdenFab.Size = new Size(11, 13);
            this.lblOrdenFab.TabIndex = 3;
            this.lblOrdenFab.Text = "-";

            this.lblDescripcionTxt.AutoSize = true;
            this.lblDescripcionTxt.Location = new Point(15, 65);
            this.lblDescripcionTxt.Name = "lblDescripcionTxt";
            this.lblDescripcionTxt.Size = new Size(66, 13);
            this.lblDescripcionTxt.TabIndex = 4;
            this.lblDescripcionTxt.Text = "Descripción:";

            this.lblDescripcion.Location = new Point(15, 85);
            this.lblDescripcion.Name = "lblDescripcion";
            this.lblDescripcion.Size = new Size(190, 30);
            this.lblDescripcion.TabIndex = 5;
            this.lblDescripcion.Text = "-";

            this.lblCantidadTxt.AutoSize = true;
            this.lblCantidadTxt.Location = new Point(15, 120);
            this.lblCantidadTxt.Name = "lblCantidadTxt";
            this.lblCantidadTxt.Size = new Size(52, 13);
            this.lblCantidadTxt.TabIndex = 6;
            this.lblCantidadTxt.Text = "Cantidad:";

            this.lblCantidad.AutoSize = true;
            this.lblCantidad.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
            this.lblCantidad.Location = new Point(80, 120);
            this.lblCantidad.Name = "lblCantidad";
            this.lblCantidad.Size = new Size(11, 13);
            this.lblCantidad.TabIndex = 7;
            this.lblCantidad.Text = "-";

            this.lblUPCTxt.AutoSize = true;
            this.lblUPCTxt.Location = new Point(15, 140);
            this.lblUPCTxt.Name = "lblUPCTxt";
            this.lblUPCTxt.Size = new Size(33, 13);
            this.lblUPCTxt.TabIndex = 8;
            this.lblUPCTxt.Text = "UPC:";

            this.lblUPC.AutoSize = true;
            this.lblUPC.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
            this.lblUPC.Location = new Point(80, 140);
            this.lblUPC.Name = "lblUPC";
            this.lblUPC.Size = new Size(11, 13);
            this.lblUPC.TabIndex = 9;
            this.lblUPC.Text = "-";

            this.lblUPC2Txt.AutoSize = true;
            this.lblUPC2Txt.Location = new Point(15, 160);
            this.lblUPC2Txt.Name = "lblUPC2Txt";
            this.lblUPC2Txt.Size = new Size(39, 13);
            this.lblUPC2Txt.TabIndex = 10;
            this.lblUPC2Txt.Text = "UPC2:";

            this.lblUPC2.AutoSize = true;
            this.lblUPC2.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
            this.lblUPC2.Location = new Point(80, 160);
            this.lblUPC2.Name = "lblUPC2";
            this.lblUPC2.Size = new Size(11, 13);
            this.lblUPC2.TabIndex = 11;
            this.lblUPC2.Text = "-";

            // 
            // btnImprimir
            // 
            this.btnImprimir.Anchor = ((AnchorStyles)((AnchorStyles.Bottom | AnchorStyles.Right)));
            this.btnImprimir.BackColor = Color.LightGreen;
            this.btnImprimir.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point);
            this.btnImprimir.Location = new Point(910, 400);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new Size(80, 40);
            this.btnImprimir.TabIndex = 5;
            this.btnImprimir.Text = "Imprimir";
            this.btnImprimir.UseVisualStyleBackColor = false;

            // 
            // btnPrueba
            // 
            this.btnPrueba.Anchor = ((AnchorStyles)((AnchorStyles.Bottom | AnchorStyles.Right)));
            this.btnPrueba.Location = new Point(910, 450);
            this.btnPrueba.Name = "btnPrueba";
            this.btnPrueba.Size = new Size(80, 30);
            this.btnPrueba.TabIndex = 6;
            this.btnPrueba.Text = "Prueba";
            this.btnPrueba.UseVisualStyleBackColor = true;

            // 
            // btnRefrescar
            // 
            this.btnRefrescar.Anchor = ((AnchorStyles)((AnchorStyles.Bottom | AnchorStyles.Right)));
            this.btnRefrescar.Location = new Point(910, 490);
            this.btnRefrescar.Name = "btnRefrescar";
            this.btnRefrescar.Size = new Size(80, 30);
            this.btnRefrescar.TabIndex = 7;
            this.btnRefrescar.Text = "Refrescar";
            this.btnRefrescar.UseVisualStyleBackColor = true;
            this.btnRefrescar.Click += new EventHandler((s, e) => RefrescarDatos());

            // 
            // coloresPanel
            // 
            this.coloresPanel.Anchor = ((AnchorStyles)((AnchorStyles.Bottom | AnchorStyles.Left)));
            this.coloresPanel.BorderStyle = BorderStyle.FixedSingle;
            this.coloresPanel.Location = new Point(12, 470);
            this.coloresPanel.Name = "coloresPanel";
            this.coloresPanel.Size = new Size(638, 150);
            this.coloresPanel.TabIndex = 8;

            // Inicializar botones de colores
            InitializeColorButtons();

            // 
            // BaseEtiquetaForm
            // 
            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(1084, 661);
            this.Controls.Add(this.coloresPanel);
            this.Controls.Add(this.btnRefrescar);
            this.Controls.Add(this.btnPrueba);
            this.Controls.Add(this.btnImprimir);
            this.Controls.Add(this.infoGroupBox);
            this.Controls.Add(this.posicionesGroupBox);
            this.Controls.Add(this.etiquetaGroupBox);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.solicitudesGrid);
            this.Name = "BaseEtiquetaForm";
            this.Text = "Sistema de Etiquetas";

            ((System.ComponentModel.ISupportInitialize)(this.solicitudesGrid)).EndInit();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.etiquetaGroupBox.ResumeLayout(false);
            this.etiquetaGroupBox.PerformLayout();
            this.posicionesGroupBox.ResumeLayout(false);
            this.posicionesGroupBox.PerformLayout();
            this.infoGroupBox.ResumeLayout(false);
            this.infoGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void InitializeColorButtons()
        {
            colorButtons = new Button[5];

            for (int i = 0; i < 5; i++)
            {
                colorButtons[i] = new Button();
                colorButtons[i].Size = new Size(120, 140);
                colorButtons[i].Location = new Point(10 + (i * 125), 10);
                colorButtons[i].BackColor = Color.LightGray;
                colorButtons[i].Text = "0";
                colorButtons[i].Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold);
                colorButtons[i].FlatStyle = FlatStyle.Flat;
                colorButtons[i].FlatAppearance.BorderSize = 2;
                colorButtons[i].UseVisualStyleBackColor = false;

                coloresPanel.Controls.Add(colorButtons[i]);
            }
        }

        #endregion
    }
}