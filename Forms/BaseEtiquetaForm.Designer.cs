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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            solicitudesGrid = new DataGridView();
            statusStrip = new StatusStrip();
            statusLabel = new ToolStripStatusLabel();
            etiquetaGroupBox = new GroupBox();
            lblTipoEtiqueta = new Label();
            papelComboBox = new ComboBox();
            lblPapel = new Label();
            velocidadComboBox = new ComboBox();
            lblVelocidad = new Label();
            temperaturaComboBox = new ComboBox();
            lblTemperatura = new Label();
            etiquetaDobleCheckBox = new CheckBox();
            posicionesGroupBox = new GroupBox();
            pos1TextBox = new TextBox();
            pos2TextBox = new TextBox();
            pos3TextBox = new TextBox();
            pos4TextBox = new TextBox();
            pos5TextBox = new TextBox();
            lbl1 = new Label();
            lbl2 = new Label();
            lbl3 = new Label();
            lbl4 = new Label();
            lbl5 = new Label();
            btnAjustarIzq = new Button();
            btnAjustarDer = new Button();
            infoGroupBox = new GroupBox();
            lblIdSolicitudTxt = new Label();
            lblIdSolicitud = new Label();
            lblOrdenFabTxt = new Label();
            lblOrdenFab = new Label();
            lblDescripcionTxt = new Label();
            lblDescripcion = new Label();
            lblCantidadTxt = new Label();
            lblCantidad = new Label();
            lblUPCTxt = new Label();
            lblUPC = new Label();
            lblUPC2Txt = new Label();
            lblUPC2 = new Label();
            btnImprimir = new Button();
            btnPrueba = new Button();
            btnRefrescar = new Button();
            coloresPanel = new Panel();
            dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn4 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn5 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn6 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn7 = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)solicitudesGrid).BeginInit();
            statusStrip.SuspendLayout();
            etiquetaGroupBox.SuspendLayout();
            posicionesGroupBox.SuspendLayout();
            infoGroupBox.SuspendLayout();
            SuspendLayout();
            // 
            // solicitudesGrid
            // 
            solicitudesGrid.AllowUserToAddRows = false;
            solicitudesGrid.AllowUserToDeleteRows = false;
            solicitudesGrid.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            solicitudesGrid.BackgroundColor = SystemColors.Window;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            solicitudesGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            solicitudesGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            solicitudesGrid.Columns.AddRange(new DataGridViewColumn[] { dataGridViewTextBoxColumn1, dataGridViewTextBoxColumn2, dataGridViewTextBoxColumn3, dataGridViewTextBoxColumn4, dataGridViewTextBoxColumn5, dataGridViewTextBoxColumn6, dataGridViewTextBoxColumn7 });
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            solicitudesGrid.DefaultCellStyle = dataGridViewCellStyle2;
            solicitudesGrid.Location = new Point(14, 14);
            solicitudesGrid.Margin = new Padding(4, 3, 4, 3);
            solicitudesGrid.MultiSelect = false;
            solicitudesGrid.Name = "solicitudesGrid";
            solicitudesGrid.ReadOnly = true;
            solicitudesGrid.RowHeadersVisible = false;
            solicitudesGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            solicitudesGrid.Size = new Size(945, 346);
            solicitudesGrid.TabIndex = 0;
            // 
            // statusStrip
            // 
            statusStrip.Items.AddRange(new ToolStripItem[] { statusLabel });
            statusStrip.Location = new Point(0, 741);
            statusStrip.Name = "statusStrip";
            statusStrip.Padding = new Padding(1, 0, 16, 0);
            statusStrip.Size = new Size(1265, 22);
            statusStrip.TabIndex = 1;
            statusStrip.Text = "statusStrip1";
            // 
            // statusLabel
            // 
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(32, 17);
            statusLabel.Text = "Listo";
            // 
            // etiquetaGroupBox
            // 
            etiquetaGroupBox.Controls.Add(lblTipoEtiqueta);
            etiquetaGroupBox.Controls.Add(papelComboBox);
            etiquetaGroupBox.Controls.Add(lblPapel);
            etiquetaGroupBox.Controls.Add(velocidadComboBox);
            etiquetaGroupBox.Controls.Add(lblVelocidad);
            etiquetaGroupBox.Controls.Add(temperaturaComboBox);
            etiquetaGroupBox.Controls.Add(lblTemperatura);
            etiquetaGroupBox.Controls.Add(etiquetaDobleCheckBox);
            etiquetaGroupBox.Location = new Point(14, 381);
            etiquetaGroupBox.Margin = new Padding(4, 3, 4, 3);
            etiquetaGroupBox.Name = "etiquetaGroupBox";
            etiquetaGroupBox.Padding = new Padding(4, 3, 4, 3);
            etiquetaGroupBox.Size = new Size(350, 138);
            etiquetaGroupBox.TabIndex = 2;
            etiquetaGroupBox.TabStop = false;
            etiquetaGroupBox.Text = "Configuración de Etiquetas";
            // 
            // lblTipoEtiqueta
            // 
            lblTipoEtiqueta.AutoSize = true;
            lblTipoEtiqueta.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);
            lblTipoEtiqueta.Location = new Point(18, 23);
            lblTipoEtiqueta.Margin = new Padding(4, 0, 4, 0);
            lblTipoEtiqueta.Name = "lblTipoEtiqueta";
            lblTipoEtiqueta.Size = new Size(126, 15);
            lblTipoEtiqueta.TabIndex = 0;
            lblTipoEtiqueta.Text = "Etiquetas C/BCO-E";
            // 
            // papelComboBox
            // 
            papelComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            papelComboBox.FormattingEnabled = true;
            papelComboBox.Location = new Point(70, 52);
            papelComboBox.Margin = new Padding(4, 3, 4, 3);
            papelComboBox.Name = "papelComboBox";
            papelComboBox.Size = new Size(116, 23);
            papelComboBox.TabIndex = 2;
            // 
            // lblPapel
            // 
            lblPapel.AutoSize = true;
            lblPapel.Location = new Point(18, 55);
            lblPapel.Margin = new Padding(4, 0, 4, 0);
            lblPapel.Name = "lblPapel";
            lblPapel.Size = new Size(36, 15);
            lblPapel.TabIndex = 1;
            lblPapel.Text = "Papel";
            // 
            // velocidadComboBox
            // 
            velocidadComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            velocidadComboBox.FormattingEnabled = true;
            velocidadComboBox.Items.AddRange(new object[] { "1", "2", "3", "4", "5", "6", "7", "8" });
            velocidadComboBox.Location = new Point(268, 52);
            velocidadComboBox.Margin = new Padding(4, 3, 4, 3);
            velocidadComboBox.Name = "velocidadComboBox";
            velocidadComboBox.Size = new Size(58, 23);
            velocidadComboBox.TabIndex = 4;
            // 
            // lblVelocidad
            // 
            lblVelocidad.AutoSize = true;
            lblVelocidad.Location = new Point(204, 55);
            lblVelocidad.Margin = new Padding(4, 0, 4, 0);
            lblVelocidad.Name = "lblVelocidad";
            lblVelocidad.Size = new Size(58, 15);
            lblVelocidad.TabIndex = 3;
            lblVelocidad.Text = "Velocidad";
            // 
            // temperaturaComboBox
            // 
            temperaturaComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            temperaturaComboBox.FormattingEnabled = true;
            temperaturaComboBox.Items.AddRange(new object[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30" });
            temperaturaComboBox.Location = new Point(268, 87);
            temperaturaComboBox.Margin = new Padding(4, 3, 4, 3);
            temperaturaComboBox.Name = "temperaturaComboBox";
            temperaturaComboBox.Size = new Size(58, 23);
            temperaturaComboBox.TabIndex = 6;
            // 
            // lblTemperatura
            // 
            lblTemperatura.AutoSize = true;
            lblTemperatura.Location = new Point(204, 90);
            lblTemperatura.Margin = new Padding(4, 0, 4, 0);
            lblTemperatura.Name = "lblTemperatura";
            lblTemperatura.Size = new Size(74, 15);
            lblTemperatura.TabIndex = 5;
            lblTemperatura.Text = "Temperatura";
            // 
            // etiquetaDobleCheckBox
            // 
            etiquetaDobleCheckBox.AutoSize = true;
            etiquetaDobleCheckBox.Location = new Point(18, 87);
            etiquetaDobleCheckBox.Margin = new Padding(4, 3, 4, 3);
            etiquetaDobleCheckBox.Name = "etiquetaDobleCheckBox";
            etiquetaDobleCheckBox.Size = new Size(103, 19);
            etiquetaDobleCheckBox.TabIndex = 7;
            etiquetaDobleCheckBox.Text = "Etiqueta Doble";
            etiquetaDobleCheckBox.UseVisualStyleBackColor = true;
            // 
            // posicionesGroupBox
            // 
            posicionesGroupBox.Controls.Add(pos1TextBox);
            posicionesGroupBox.Controls.Add(pos2TextBox);
            posicionesGroupBox.Controls.Add(pos3TextBox);
            posicionesGroupBox.Controls.Add(pos4TextBox);
            posicionesGroupBox.Controls.Add(pos5TextBox);
            posicionesGroupBox.Controls.Add(lbl1);
            posicionesGroupBox.Controls.Add(lbl2);
            posicionesGroupBox.Controls.Add(lbl3);
            posicionesGroupBox.Controls.Add(lbl4);
            posicionesGroupBox.Controls.Add(lbl5);
            posicionesGroupBox.Controls.Add(btnAjustarIzq);
            posicionesGroupBox.Controls.Add(btnAjustarDer);
            posicionesGroupBox.Location = new Point(385, 381);
            posicionesGroupBox.Margin = new Padding(4, 3, 4, 3);
            posicionesGroupBox.Name = "posicionesGroupBox";
            posicionesGroupBox.Padding = new Padding(4, 3, 4, 3);
            posicionesGroupBox.Size = new Size(373, 138);
            posicionesGroupBox.TabIndex = 3;
            posicionesGroupBox.TabStop = false;
            posicionesGroupBox.Text = "Posición de columnas";
            // 
            // pos1TextBox
            // 
            pos1TextBox.Location = new Point(18, 52);
            pos1TextBox.Margin = new Padding(4, 3, 4, 3);
            pos1TextBox.Name = "pos1TextBox";
            pos1TextBox.Size = new Size(58, 23);
            pos1TextBox.TabIndex = 1;
            pos1TextBox.Text = "4";
            pos1TextBox.TextAlign = HorizontalAlignment.Center;
            // 
            // pos2TextBox
            // 
            pos2TextBox.Location = new Point(88, 52);
            pos2TextBox.Margin = new Padding(4, 3, 4, 3);
            pos2TextBox.Name = "pos2TextBox";
            pos2TextBox.Size = new Size(58, 23);
            pos2TextBox.TabIndex = 3;
            pos2TextBox.Text = "148";
            pos2TextBox.TextAlign = HorizontalAlignment.Center;
            // 
            // pos3TextBox
            // 
            pos3TextBox.Location = new Point(158, 52);
            pos3TextBox.Margin = new Padding(4, 3, 4, 3);
            pos3TextBox.Name = "pos3TextBox";
            pos3TextBox.Size = new Size(58, 23);
            pos3TextBox.TabIndex = 5;
            pos3TextBox.Text = "292";
            pos3TextBox.TextAlign = HorizontalAlignment.Center;
            // 
            // pos4TextBox
            // 
            pos4TextBox.Location = new Point(227, 52);
            pos4TextBox.Margin = new Padding(4, 3, 4, 3);
            pos4TextBox.Name = "pos4TextBox";
            pos4TextBox.Size = new Size(58, 23);
            pos4TextBox.TabIndex = 7;
            pos4TextBox.Text = "436";
            pos4TextBox.TextAlign = HorizontalAlignment.Center;
            // 
            // pos5TextBox
            // 
            pos5TextBox.Location = new Point(298, 52);
            pos5TextBox.Margin = new Padding(4, 3, 4, 3);
            pos5TextBox.Name = "pos5TextBox";
            pos5TextBox.Size = new Size(58, 23);
            pos5TextBox.TabIndex = 9;
            pos5TextBox.Text = "580";
            pos5TextBox.TextAlign = HorizontalAlignment.Center;
            // 
            // lbl1
            // 
            lbl1.AutoSize = true;
            lbl1.Location = new Point(18, 29);
            lbl1.Margin = new Padding(4, 0, 4, 0);
            lbl1.Name = "lbl1";
            lbl1.Size = new Size(13, 15);
            lbl1.TabIndex = 0;
            lbl1.Text = "1";
            // 
            // lbl2
            // 
            lbl2.AutoSize = true;
            lbl2.Location = new Point(88, 29);
            lbl2.Margin = new Padding(4, 0, 4, 0);
            lbl2.Name = "lbl2";
            lbl2.Size = new Size(13, 15);
            lbl2.TabIndex = 2;
            lbl2.Text = "2";
            // 
            // lbl3
            // 
            lbl3.AutoSize = true;
            lbl3.Location = new Point(158, 29);
            lbl3.Margin = new Padding(4, 0, 4, 0);
            lbl3.Name = "lbl3";
            lbl3.Size = new Size(13, 15);
            lbl3.TabIndex = 4;
            lbl3.Text = "3";
            // 
            // lbl4
            // 
            lbl4.AutoSize = true;
            lbl4.Location = new Point(227, 29);
            lbl4.Margin = new Padding(4, 0, 4, 0);
            lbl4.Name = "lbl4";
            lbl4.Size = new Size(13, 15);
            lbl4.TabIndex = 6;
            lbl4.Text = "4";
            // 
            // lbl5
            // 
            lbl5.AutoSize = true;
            lbl5.Location = new Point(298, 29);
            lbl5.Margin = new Padding(4, 0, 4, 0);
            lbl5.Name = "lbl5";
            lbl5.Size = new Size(13, 15);
            lbl5.TabIndex = 8;
            lbl5.Text = "5";
            // 
            // btnAjustarIzq
            // 
            btnAjustarIzq.Location = new Point(18, 98);
            btnAjustarIzq.Margin = new Padding(4, 3, 4, 3);
            btnAjustarIzq.Name = "btnAjustarIzq";
            btnAjustarIzq.Size = new Size(35, 27);
            btnAjustarIzq.TabIndex = 10;
            btnAjustarIzq.Text = "<<";
            btnAjustarIzq.UseVisualStyleBackColor = true;
            // 
            // btnAjustarDer
            // 
            btnAjustarDer.Location = new Point(64, 98);
            btnAjustarDer.Margin = new Padding(4, 3, 4, 3);
            btnAjustarDer.Name = "btnAjustarDer";
            btnAjustarDer.Size = new Size(35, 27);
            btnAjustarDer.TabIndex = 11;
            btnAjustarDer.Text = ">>";
            btnAjustarDer.UseVisualStyleBackColor = true;
            // 
            // infoGroupBox
            // 
            infoGroupBox.Controls.Add(lblIdSolicitudTxt);
            infoGroupBox.Controls.Add(lblIdSolicitud);
            infoGroupBox.Controls.Add(lblOrdenFabTxt);
            infoGroupBox.Controls.Add(lblOrdenFab);
            infoGroupBox.Controls.Add(lblDescripcionTxt);
            infoGroupBox.Controls.Add(lblDescripcion);
            infoGroupBox.Controls.Add(lblCantidadTxt);
            infoGroupBox.Controls.Add(lblCantidad);
            infoGroupBox.Controls.Add(lblUPCTxt);
            infoGroupBox.Controls.Add(lblUPC);
            infoGroupBox.Controls.Add(lblUPC2Txt);
            infoGroupBox.Controls.Add(lblUPC2);
            infoGroupBox.Location = new Point(782, 381);
            infoGroupBox.Margin = new Padding(4, 3, 4, 3);
            infoGroupBox.Name = "infoGroupBox";
            infoGroupBox.Padding = new Padding(4, 3, 4, 3);
            infoGroupBox.Size = new Size(257, 208);
            infoGroupBox.TabIndex = 4;
            infoGroupBox.TabStop = false;
            infoGroupBox.Text = "Información";
            // 
            // lblIdSolicitudTxt
            // 
            lblIdSolicitudTxt.AutoSize = true;
            lblIdSolicitudTxt.Location = new Point(18, 29);
            lblIdSolicitudTxt.Margin = new Padding(4, 0, 4, 0);
            lblIdSolicitudTxt.Name = "lblIdSolicitudTxt";
            lblIdSolicitudTxt.Size = new Size(46, 15);
            lblIdSolicitudTxt.TabIndex = 0;
            lblIdSolicitudTxt.Text = "N° Sol.:";
            // 
            // lblIdSolicitud
            // 
            lblIdSolicitud.AutoSize = true;
            lblIdSolicitud.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold);
            lblIdSolicitud.Location = new Point(93, 29);
            lblIdSolicitud.Margin = new Padding(4, 0, 4, 0);
            lblIdSolicitud.Name = "lblIdSolicitud";
            lblIdSolicitud.Size = new Size(11, 13);
            lblIdSolicitud.TabIndex = 1;
            lblIdSolicitud.Text = "-";
            // 
            // lblOrdenFabTxt
            // 
            lblOrdenFabTxt.AutoSize = true;
            lblOrdenFabTxt.Location = new Point(18, 52);
            lblOrdenFabTxt.Margin = new Padding(4, 0, 4, 0);
            lblOrdenFabTxt.Name = "lblOrdenFabTxt";
            lblOrdenFabTxt.Size = new Size(68, 15);
            lblOrdenFabTxt.TabIndex = 2;
            lblOrdenFabTxt.Text = "Orden Fab.:";
            // 
            // lblOrdenFab
            // 
            lblOrdenFab.AutoSize = true;
            lblOrdenFab.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold);
            lblOrdenFab.Location = new Point(93, 52);
            lblOrdenFab.Margin = new Padding(4, 0, 4, 0);
            lblOrdenFab.Name = "lblOrdenFab";
            lblOrdenFab.Size = new Size(11, 13);
            lblOrdenFab.TabIndex = 3;
            lblOrdenFab.Text = "-";
            // 
            // lblDescripcionTxt
            // 
            lblDescripcionTxt.AutoSize = true;
            lblDescripcionTxt.Location = new Point(18, 75);
            lblDescripcionTxt.Margin = new Padding(4, 0, 4, 0);
            lblDescripcionTxt.Name = "lblDescripcionTxt";
            lblDescripcionTxt.Size = new Size(72, 15);
            lblDescripcionTxt.TabIndex = 4;
            lblDescripcionTxt.Text = "Descripción:";
            // 
            // lblDescripcion
            // 
            lblDescripcion.Location = new Point(18, 98);
            lblDescripcion.Margin = new Padding(4, 0, 4, 0);
            lblDescripcion.Name = "lblDescripcion";
            lblDescripcion.Size = new Size(222, 35);
            lblDescripcion.TabIndex = 5;
            lblDescripcion.Text = "-";
            // 
            // lblCantidadTxt
            // 
            lblCantidadTxt.AutoSize = true;
            lblCantidadTxt.Location = new Point(18, 138);
            lblCantidadTxt.Margin = new Padding(4, 0, 4, 0);
            lblCantidadTxt.Name = "lblCantidadTxt";
            lblCantidadTxt.Size = new Size(58, 15);
            lblCantidadTxt.TabIndex = 6;
            lblCantidadTxt.Text = "Cantidad:";
            // 
            // lblCantidad
            // 
            lblCantidad.AutoSize = true;
            lblCantidad.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold);
            lblCantidad.Location = new Point(93, 138);
            lblCantidad.Margin = new Padding(4, 0, 4, 0);
            lblCantidad.Name = "lblCantidad";
            lblCantidad.Size = new Size(11, 13);
            lblCantidad.TabIndex = 7;
            lblCantidad.Text = "-";
            // 
            // lblUPCTxt
            // 
            lblUPCTxt.AutoSize = true;
            lblUPCTxt.Location = new Point(18, 162);
            lblUPCTxt.Margin = new Padding(4, 0, 4, 0);
            lblUPCTxt.Name = "lblUPCTxt";
            lblUPCTxt.Size = new Size(33, 15);
            lblUPCTxt.TabIndex = 8;
            lblUPCTxt.Text = "UPC:";
            // 
            // lblUPC
            // 
            lblUPC.AutoSize = true;
            lblUPC.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold);
            lblUPC.Location = new Point(93, 162);
            lblUPC.Margin = new Padding(4, 0, 4, 0);
            lblUPC.Name = "lblUPC";
            lblUPC.Size = new Size(11, 13);
            lblUPC.TabIndex = 9;
            lblUPC.Text = "-";
            // 
            // lblUPC2Txt
            // 
            lblUPC2Txt.AutoSize = true;
            lblUPC2Txt.Location = new Point(18, 185);
            lblUPC2Txt.Margin = new Padding(4, 0, 4, 0);
            lblUPC2Txt.Name = "lblUPC2Txt";
            lblUPC2Txt.Size = new Size(39, 15);
            lblUPC2Txt.TabIndex = 10;
            lblUPC2Txt.Text = "UPC2:";
            // 
            // lblUPC2
            // 
            lblUPC2.AutoSize = true;
            lblUPC2.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold);
            lblUPC2.Location = new Point(93, 185);
            lblUPC2.Margin = new Padding(4, 0, 4, 0);
            lblUPC2.Name = "lblUPC2";
            lblUPC2.Size = new Size(11, 13);
            lblUPC2.TabIndex = 11;
            lblUPC2.Text = "-";
            // 
            // btnImprimir
            // 
            btnImprimir.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnImprimir.BackColor = Color.LightGreen;
            btnImprimir.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);
            btnImprimir.Location = new Point(1062, 462);
            btnImprimir.Margin = new Padding(4, 3, 4, 3);
            btnImprimir.Name = "btnImprimir";
            btnImprimir.Size = new Size(93, 46);
            btnImprimir.TabIndex = 5;
            btnImprimir.Text = "Imprimir";
            btnImprimir.UseVisualStyleBackColor = false;
            // 
            // btnPrueba
            // 
            btnPrueba.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnPrueba.Location = new Point(1062, 519);
            btnPrueba.Margin = new Padding(4, 3, 4, 3);
            btnPrueba.Name = "btnPrueba";
            btnPrueba.Size = new Size(93, 35);
            btnPrueba.TabIndex = 6;
            btnPrueba.Text = "Prueba";
            btnPrueba.UseVisualStyleBackColor = true;
            // 
            // btnRefrescar
            // 
            btnRefrescar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnRefrescar.Location = new Point(1062, 565);
            btnRefrescar.Margin = new Padding(4, 3, 4, 3);
            btnRefrescar.Name = "btnRefrescar";
            btnRefrescar.Size = new Size(93, 35);
            btnRefrescar.TabIndex = 7;
            btnRefrescar.Text = "Refrescar";
            btnRefrescar.UseVisualStyleBackColor = true;
            // 
            // coloresPanel
            // 
            coloresPanel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            coloresPanel.BorderStyle = BorderStyle.FixedSingle;
            coloresPanel.Location = new Point(14, 542);
            coloresPanel.Margin = new Padding(4, 3, 4, 3);
            coloresPanel.Name = "coloresPanel";
            coloresPanel.Size = new Size(744, 173);
            coloresPanel.TabIndex = 8;
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewTextBoxColumn1.HeaderText = "N° Sol.";
            dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewTextBoxColumn2.HeaderText = "Orden Fab.";
            dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            dataGridViewTextBoxColumn3.HeaderText = "Descripción";
            dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            dataGridViewTextBoxColumn4.HeaderText = "Cantidad";
            dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn5
            // 
            dataGridViewTextBoxColumn5.HeaderText = "Color";
            dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            dataGridViewTextBoxColumn5.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn6
            // 
            dataGridViewTextBoxColumn6.HeaderText = "Observaciones";
            dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            dataGridViewTextBoxColumn6.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn7
            // 
            dataGridViewTextBoxColumn7.HeaderText = "Fecha Req.";
            dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            dataGridViewTextBoxColumn7.ReadOnly = true;
            // 
            // BaseEtiquetaForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1265, 763);
            Controls.Add(coloresPanel);
            Controls.Add(btnRefrescar);
            Controls.Add(btnPrueba);
            Controls.Add(btnImprimir);
            Controls.Add(infoGroupBox);
            Controls.Add(posicionesGroupBox);
            Controls.Add(etiquetaGroupBox);
            Controls.Add(statusStrip);
            Controls.Add(solicitudesGrid);
            Margin = new Padding(4, 3, 4, 3);
            Name = "BaseEtiquetaForm";
            Text = "Sistema de Etiquetas";
            ((System.ComponentModel.ISupportInitialize)solicitudesGrid).EndInit();
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            etiquetaGroupBox.ResumeLayout(false);
            etiquetaGroupBox.PerformLayout();
            posicionesGroupBox.ResumeLayout(false);
            posicionesGroupBox.PerformLayout();
            infoGroupBox.ResumeLayout(false);
            infoGroupBox.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
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

        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
    }
}