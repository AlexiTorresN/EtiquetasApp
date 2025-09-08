using System;
using System.Drawing;
using System.Windows.Forms;

namespace EtiquetasApp.Forms
{
    partial class SolicitudesEtiquetasForm
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        // Controles principales
        private GroupBox datosOrdenGroupBox;
        private Label lblOrdenFab;
        private TextBox ordenFabTextBox;
        private Button btnBuscarOrden;
        private Label lblDescripcion;
        private TextBox descripcionTextBox;
        private Label maestroEncontradoLabel;
        private Button btnVerMaestro;
        private Button btnCrearMaestro;

        // Información de la orden
        private GroupBox infoOrdenGroupBox;
        private Label infoOrdenLabel;

        // Datos de la solicitud
        private GroupBox datosSolicitudGroupBox;
        private Label lblTipoEtiqueta;
        private ComboBox tipoEtiquetaComboBox;
        private Label lblUPC1;
        private TextBox upc1TextBox;
        private Label lblUPC2;
        private TextBox upc2TextBox;
        private Label upcEjemploLabel;
        private Label lblCantidad;
        private NumericUpDown cantidadNumericUpDown;
        private Label lblFechaRequerida;
        private DateTimePicker fechaRequieridaDateTime;
        private Label fechaAdvertenciaLabel;
        private Label lblColor;
        private ComboBox colorComboBox;
        private Label lblObservaciones;
        private TextBox observacionesTextBox;

        // Botones de acción
        private Button btnGuardar;
        private Button btnLimpiar;
        private Button btnCancelar;

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
            datosOrdenGroupBox = new GroupBox();
            btnCrearMaestro = new Button();
            btnVerMaestro = new Button();
            maestroEncontradoLabel = new Label();
            descripcionTextBox = new TextBox();
            lblDescripcion = new Label();
            btnBuscarOrden = new Button();
            ordenFabTextBox = new TextBox();
            lblOrdenFab = new Label();
            infoOrdenGroupBox = new GroupBox();
            infoOrdenLabel = new Label();
            datosSolicitudGroupBox = new GroupBox();
            observacionesTextBox = new TextBox();
            lblObservaciones = new Label();
            colorComboBox = new ComboBox();
            lblColor = new Label();
            fechaAdvertenciaLabel = new Label();
            fechaRequieridaDateTime = new DateTimePicker();
            lblFechaRequerida = new Label();
            cantidadNumericUpDown = new NumericUpDown();
            lblCantidad = new Label();
            upcEjemploLabel = new Label();
            upc2TextBox = new TextBox();
            lblUPC2 = new Label();
            upc1TextBox = new TextBox();
            lblUPC1 = new Label();
            tipoEtiquetaComboBox = new ComboBox();
            lblTipoEtiqueta = new Label();
            btnGuardar = new Button();
            btnLimpiar = new Button();
            btnCancelar = new Button();
            statusStrip = new StatusStrip();
            statusLabel = new ToolStripStatusLabel();
            datosOrdenGroupBox.SuspendLayout();
            infoOrdenGroupBox.SuspendLayout();
            datosSolicitudGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)cantidadNumericUpDown).BeginInit();
            statusStrip.SuspendLayout();
            SuspendLayout();
            // 
            // datosOrdenGroupBox
            // 
            datosOrdenGroupBox.Controls.Add(btnCrearMaestro);
            datosOrdenGroupBox.Controls.Add(btnVerMaestro);
            datosOrdenGroupBox.Controls.Add(maestroEncontradoLabel);
            datosOrdenGroupBox.Controls.Add(descripcionTextBox);
            datosOrdenGroupBox.Controls.Add(lblDescripcion);
            datosOrdenGroupBox.Controls.Add(btnBuscarOrden);
            datosOrdenGroupBox.Controls.Add(ordenFabTextBox);
            datosOrdenGroupBox.Controls.Add(lblOrdenFab);
            datosOrdenGroupBox.Location = new Point(18, 17);
            datosOrdenGroupBox.Margin = new Padding(4, 3, 4, 3);
            datosOrdenGroupBox.Name = "datosOrdenGroupBox";
            datosOrdenGroupBox.Padding = new Padding(4, 3, 4, 3);
            datosOrdenGroupBox.Size = new Size(525, 173);
            datosOrdenGroupBox.TabIndex = 0;
            datosOrdenGroupBox.TabStop = false;
            datosOrdenGroupBox.Text = "Datos de la Orden de Fabricación";
            // 
            // btnCrearMaestro
            // 
            btnCrearMaestro.BackColor = Color.LightYellow;
            btnCrearMaestro.Enabled = false;
            btnCrearMaestro.Location = new Point(373, 144);
            btnCrearMaestro.Margin = new Padding(4, 3, 4, 3);
            btnCrearMaestro.Name = "btnCrearMaestro";
            btnCrearMaestro.Size = new Size(93, 27);
            btnCrearMaestro.TabIndex = 7;
            btnCrearMaestro.Text = "Crear Maestro";
            btnCrearMaestro.UseVisualStyleBackColor = false;
            // 
            // btnVerMaestro
            // 
            btnVerMaestro.Enabled = false;
            btnVerMaestro.Location = new Point(373, 115);
            btnVerMaestro.Margin = new Padding(4, 3, 4, 3);
            btnVerMaestro.Name = "btnVerMaestro";
            btnVerMaestro.Size = new Size(93, 27);
            btnVerMaestro.TabIndex = 6;
            btnVerMaestro.Text = "Ver Maestro";
            btnVerMaestro.UseVisualStyleBackColor = true;
            // 
            // maestroEncontradoLabel
            // 
            maestroEncontradoLabel.AutoSize = true;
            maestroEncontradoLabel.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold);
            maestroEncontradoLabel.Location = new Point(373, 58);
            maestroEncontradoLabel.Margin = new Padding(4, 0, 4, 0);
            maestroEncontradoLabel.Name = "maestroEncontradoLabel";
            maestroEncontradoLabel.Size = new Size(0, 13);
            maestroEncontradoLabel.TabIndex = 5;
            // 
            // descripcionTextBox
            // 
            descripcionTextBox.BackColor = SystemColors.Control;
            descripcionTextBox.Location = new Point(18, 115);
            descripcionTextBox.Margin = new Padding(4, 3, 4, 3);
            descripcionTextBox.Name = "descripcionTextBox";
            descripcionTextBox.ReadOnly = true;
            descripcionTextBox.Size = new Size(338, 23);
            descripcionTextBox.TabIndex = 4;
            // 
            // lblDescripcion
            // 
            lblDescripcion.AutoSize = true;
            lblDescripcion.Location = new Point(18, 92);
            lblDescripcion.Margin = new Padding(4, 0, 4, 0);
            lblDescripcion.Name = "lblDescripcion";
            lblDescripcion.Size = new Size(72, 15);
            lblDescripcion.TabIndex = 3;
            lblDescripcion.Text = "Descripción:";
            // 
            // btnBuscarOrden
            // 
            btnBuscarOrden.BackColor = Color.LightBlue;
            btnBuscarOrden.Enabled = false;
            btnBuscarOrden.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold);
            btnBuscarOrden.Location = new Point(262, 52);
            btnBuscarOrden.Margin = new Padding(4, 3, 4, 3);
            btnBuscarOrden.Name = "btnBuscarOrden";
            btnBuscarOrden.Size = new Size(93, 27);
            btnBuscarOrden.TabIndex = 2;
            btnBuscarOrden.Text = "Buscar";
            btnBuscarOrden.UseVisualStyleBackColor = false;
            // 
            // ordenFabTextBox
            // 
            ordenFabTextBox.CharacterCasing = CharacterCasing.Upper;
            ordenFabTextBox.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            ordenFabTextBox.Location = new Point(18, 52);
            ordenFabTextBox.Margin = new Padding(4, 3, 4, 3);
            ordenFabTextBox.Name = "ordenFabTextBox";
            ordenFabTextBox.Size = new Size(233, 23);
            ordenFabTextBox.TabIndex = 1;
            // 
            // lblOrdenFab
            // 
            lblOrdenFab.AutoSize = true;
            lblOrdenFab.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);
            lblOrdenFab.Location = new Point(18, 29);
            lblOrdenFab.Margin = new Padding(4, 0, 4, 0);
            lblOrdenFab.Name = "lblOrdenFab";
            lblOrdenFab.Size = new Size(129, 15);
            lblOrdenFab.TabIndex = 0;
            lblOrdenFab.Text = "Orden Fabricación:";
            // 
            // infoOrdenGroupBox
            // 
            infoOrdenGroupBox.Controls.Add(infoOrdenLabel);
            infoOrdenGroupBox.Location = new Point(560, 17);
            infoOrdenGroupBox.Margin = new Padding(4, 3, 4, 3);
            infoOrdenGroupBox.Name = "infoOrdenGroupBox";
            infoOrdenGroupBox.Padding = new Padding(4, 3, 4, 3);
            infoOrdenGroupBox.Size = new Size(327, 173);
            infoOrdenGroupBox.TabIndex = 1;
            infoOrdenGroupBox.TabStop = false;
            infoOrdenGroupBox.Text = "Información de la Orden";
            // 
            // infoOrdenLabel
            // 
            infoOrdenLabel.Font = new Font("Microsoft Sans Serif", 8.25F);
            infoOrdenLabel.Location = new Point(18, 29);
            infoOrdenLabel.Margin = new Padding(4, 0, 4, 0);
            infoOrdenLabel.Name = "infoOrdenLabel";
            infoOrdenLabel.Size = new Size(292, 133);
            infoOrdenLabel.TabIndex = 0;
            infoOrdenLabel.Text = "Busque una orden de fabricación para ver su información";
            // 
            // datosSolicitudGroupBox
            // 
            datosSolicitudGroupBox.Controls.Add(observacionesTextBox);
            datosSolicitudGroupBox.Controls.Add(lblObservaciones);
            datosSolicitudGroupBox.Controls.Add(colorComboBox);
            datosSolicitudGroupBox.Controls.Add(lblColor);
            datosSolicitudGroupBox.Controls.Add(fechaAdvertenciaLabel);
            datosSolicitudGroupBox.Controls.Add(fechaRequieridaDateTime);
            datosSolicitudGroupBox.Controls.Add(lblFechaRequerida);
            datosSolicitudGroupBox.Controls.Add(cantidadNumericUpDown);
            datosSolicitudGroupBox.Controls.Add(lblCantidad);
            datosSolicitudGroupBox.Controls.Add(upcEjemploLabel);
            datosSolicitudGroupBox.Controls.Add(upc2TextBox);
            datosSolicitudGroupBox.Controls.Add(lblUPC2);
            datosSolicitudGroupBox.Controls.Add(upc1TextBox);
            datosSolicitudGroupBox.Controls.Add(lblUPC1);
            datosSolicitudGroupBox.Controls.Add(tipoEtiquetaComboBox);
            datosSolicitudGroupBox.Controls.Add(lblTipoEtiqueta);
            datosSolicitudGroupBox.Location = new Point(18, 208);
            datosSolicitudGroupBox.Margin = new Padding(4, 3, 4, 3);
            datosSolicitudGroupBox.Name = "datosSolicitudGroupBox";
            datosSolicitudGroupBox.Padding = new Padding(4, 3, 4, 3);
            datosSolicitudGroupBox.Size = new Size(869, 254);
            datosSolicitudGroupBox.TabIndex = 2;
            datosSolicitudGroupBox.TabStop = false;
            datosSolicitudGroupBox.Text = "Datos de la Solicitud de Etiquetas";
            // 
            // observacionesTextBox
            // 
            observacionesTextBox.Location = new Point(18, 196);
            observacionesTextBox.Margin = new Padding(4, 3, 4, 3);
            observacionesTextBox.Multiline = true;
            observacionesTextBox.Name = "observacionesTextBox";
            observacionesTextBox.ScrollBars = ScrollBars.Vertical;
            observacionesTextBox.Size = new Size(705, 46);
            observacionesTextBox.TabIndex = 15;
            // 
            // lblObservaciones
            // 
            lblObservaciones.AutoSize = true;
            lblObservaciones.Location = new Point(18, 173);
            lblObservaciones.Margin = new Padding(4, 0, 4, 0);
            lblObservaciones.Name = "lblObservaciones";
            lblObservaciones.Size = new Size(87, 15);
            lblObservaciones.TabIndex = 14;
            lblObservaciones.Text = "Observaciones:";
            // 
            // colorComboBox
            // 
            colorComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            colorComboBox.FormattingEnabled = true;
            colorComboBox.Location = new Point(327, 133);
            colorComboBox.Margin = new Padding(4, 3, 4, 3);
            colorComboBox.Name = "colorComboBox";
            colorComboBox.Size = new Size(139, 23);
            colorComboBox.TabIndex = 13;
            // 
            // lblColor
            // 
            lblColor.AutoSize = true;
            lblColor.Location = new Point(327, 110);
            lblColor.Margin = new Padding(4, 0, 4, 0);
            lblColor.Name = "lblColor";
            lblColor.Size = new Size(39, 15);
            lblColor.TabIndex = 12;
            lblColor.Text = "Color:";
            // 
            // fechaAdvertenciaLabel
            // 
            fechaAdvertenciaLabel.AutoSize = true;
            fechaAdvertenciaLabel.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold);
            fechaAdvertenciaLabel.Location = new Point(169, 136);
            fechaAdvertenciaLabel.Margin = new Padding(4, 0, 4, 0);
            fechaAdvertenciaLabel.Name = "fechaAdvertenciaLabel";
            fechaAdvertenciaLabel.Size = new Size(0, 13);
            fechaAdvertenciaLabel.TabIndex = 11;
            // 
            // fechaRequieridaDateTime
            // 
            fechaRequieridaDateTime.Format = DateTimePickerFormat.Short;
            fechaRequieridaDateTime.Location = new Point(18, 133);
            fechaRequieridaDateTime.Margin = new Padding(4, 3, 4, 3);
            fechaRequieridaDateTime.Name = "fechaRequieridaDateTime";
            fechaRequieridaDateTime.Size = new Size(139, 23);
            fechaRequieridaDateTime.TabIndex = 10;
            // 
            // lblFechaRequerida
            // 
            lblFechaRequerida.AutoSize = true;
            lblFechaRequerida.Location = new Point(18, 110);
            lblFechaRequerida.Margin = new Padding(4, 0, 4, 0);
            lblFechaRequerida.Name = "lblFechaRequerida";
            lblFechaRequerida.Size = new Size(97, 15);
            lblFechaRequerida.TabIndex = 9;
            lblFechaRequerida.Text = "Fecha Requerida:";
            // 
            // cantidadNumericUpDown
            // 
            cantidadNumericUpDown.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);
            cantidadNumericUpDown.Location = new Point(607, 52);
            cantidadNumericUpDown.Margin = new Padding(4, 3, 4, 3);
            cantidadNumericUpDown.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            cantidadNumericUpDown.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            cantidadNumericUpDown.Name = "cantidadNumericUpDown";
            cantidadNumericUpDown.Size = new Size(117, 21);
            cantidadNumericUpDown.TabIndex = 8;
            cantidadNumericUpDown.ThousandsSeparator = true;
            cantidadNumericUpDown.Value = new decimal(new int[] { 100, 0, 0, 0 });
            // 
            // lblCantidad
            // 
            lblCantidad.AutoSize = true;
            lblCantidad.Location = new Point(607, 29);
            lblCantidad.Margin = new Padding(4, 0, 4, 0);
            lblCantidad.Name = "lblCantidad";
            lblCantidad.Size = new Size(58, 15);
            lblCantidad.TabIndex = 7;
            lblCantidad.Text = "Cantidad:";
            // 
            // upcEjemploLabel
            // 
            upcEjemploLabel.AutoSize = true;
            upcEjemploLabel.Font = new Font("Microsoft Sans Serif", 7.5F, FontStyle.Italic);
            upcEjemploLabel.ForeColor = Color.Gray;
            upcEjemploLabel.Location = new Point(216, 81);
            upcEjemploLabel.Margin = new Padding(4, 0, 4, 0);
            upcEjemploLabel.Name = "upcEjemploLabel";
            upcEjemploLabel.Size = new Size(137, 13);
            upcEjemploLabel.TabIndex = 6;
            upcEjemploLabel.Text = "Ejemplo UPC1: ABC123456";
            // 
            // upc2TextBox
            // 
            upc2TextBox.BackColor = SystemColors.Control;
            upc2TextBox.CharacterCasing = CharacterCasing.Upper;
            upc2TextBox.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);
            upc2TextBox.Location = new Point(408, 52);
            upc2TextBox.Margin = new Padding(4, 3, 4, 3);
            upc2TextBox.Name = "upc2TextBox";
            upc2TextBox.ReadOnly = true;
            upc2TextBox.Size = new Size(174, 21);
            upc2TextBox.TabIndex = 5;
            // 
            // lblUPC2
            // 
            lblUPC2.AutoSize = true;
            lblUPC2.Location = new Point(408, 29);
            lblUPC2.Margin = new Padding(4, 0, 4, 0);
            lblUPC2.Name = "lblUPC2";
            lblUPC2.Size = new Size(39, 15);
            lblUPC2.TabIndex = 4;
            lblUPC2.Text = "UPC2:";
            // 
            // upc1TextBox
            // 
            upc1TextBox.BackColor = SystemColors.Control;
            upc1TextBox.CharacterCasing = CharacterCasing.Upper;
            upc1TextBox.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);
            upc1TextBox.Location = new Point(216, 52);
            upc1TextBox.Margin = new Padding(4, 3, 4, 3);
            upc1TextBox.Name = "upc1TextBox";
            upc1TextBox.ReadOnly = true;
            upc1TextBox.Size = new Size(174, 21);
            upc1TextBox.TabIndex = 3;
            // 
            // lblUPC1
            // 
            lblUPC1.AutoSize = true;
            lblUPC1.Location = new Point(216, 29);
            lblUPC1.Margin = new Padding(4, 0, 4, 0);
            lblUPC1.Name = "lblUPC1";
            lblUPC1.Size = new Size(39, 15);
            lblUPC1.TabIndex = 2;
            lblUPC1.Text = "UPC1:";
            // 
            // tipoEtiquetaComboBox
            // 
            tipoEtiquetaComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            tipoEtiquetaComboBox.FormattingEnabled = true;
            tipoEtiquetaComboBox.Location = new Point(18, 52);
            tipoEtiquetaComboBox.Margin = new Padding(4, 3, 4, 3);
            tipoEtiquetaComboBox.Name = "tipoEtiquetaComboBox";
            tipoEtiquetaComboBox.Size = new Size(174, 23);
            tipoEtiquetaComboBox.TabIndex = 1;
            // 
            // lblTipoEtiqueta
            // 
            lblTipoEtiqueta.AutoSize = true;
            lblTipoEtiqueta.Location = new Point(18, 29);
            lblTipoEtiqueta.Margin = new Padding(4, 0, 4, 0);
            lblTipoEtiqueta.Name = "lblTipoEtiqueta";
            lblTipoEtiqueta.Size = new Size(80, 15);
            lblTipoEtiqueta.TabIndex = 0;
            lblTipoEtiqueta.Text = "Tipo Etiqueta:";
            // 
            // btnGuardar
            // 
            btnGuardar.BackColor = Color.LightGreen;
            btnGuardar.Enabled = false;
            btnGuardar.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            btnGuardar.Location = new Point(560, 485);
            btnGuardar.Margin = new Padding(4, 3, 4, 3);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(117, 46);
            btnGuardar.TabIndex = 3;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = false;
            // 
            // btnLimpiar
            // 
            btnLimpiar.Location = new Point(688, 485);
            btnLimpiar.Margin = new Padding(4, 3, 4, 3);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(93, 46);
            btnLimpiar.TabIndex = 4;
            btnLimpiar.Text = "Limpiar";
            btnLimpiar.UseVisualStyleBackColor = true;
            // 
            // btnCancelar
            // 
            btnCancelar.DialogResult = DialogResult.Cancel;
            btnCancelar.Location = new Point(793, 485);
            btnCancelar.Margin = new Padding(4, 3, 4, 3);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(93, 46);
            btnCancelar.TabIndex = 5;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            // 
            // statusStrip
            // 
            statusStrip.Items.AddRange(new ToolStripItem[] { statusLabel });
            statusStrip.Location = new Point(0, 555);
            statusStrip.Name = "statusStrip";
            statusStrip.Padding = new Padding(1, 0, 16, 0);
            statusStrip.Size = new Size(915, 22);
            statusStrip.TabIndex = 6;
            statusStrip.Text = "statusStrip1";
            // 
            // statusLabel
            // 
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(32, 17);
            statusLabel.Text = "Listo";
            // 
            // SolicitudesEtiquetasForm
            // 
            AcceptButton = btnGuardar;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancelar;
            ClientSize = new Size(915, 577);
            Controls.Add(statusStrip);
            Controls.Add(btnCancelar);
            Controls.Add(btnLimpiar);
            Controls.Add(btnGuardar);
            Controls.Add(datosSolicitudGroupBox);
            Controls.Add(infoOrdenGroupBox);
            Controls.Add(datosOrdenGroupBox);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            Name = "SolicitudesEtiquetasForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Nueva Solicitud de Etiquetas";
            datosOrdenGroupBox.ResumeLayout(false);
            datosOrdenGroupBox.PerformLayout();
            infoOrdenGroupBox.ResumeLayout(false);
            datosSolicitudGroupBox.ResumeLayout(false);
            datosSolicitudGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)cantidadNumericUpDown).EndInit();
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}