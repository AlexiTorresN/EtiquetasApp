namespace EtiquetasApp.Forms
{
    partial class CodificaEtiquetasForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            // Controles de filtro superiores
            this.lblFiltros = new System.Windows.Forms.Label();
            this.partIdTextBox = new System.Windows.Forms.TextBox();
            this.lblPartId = new System.Windows.Forms.Label();
            this.descripcionFiltroTextBox = new System.Windows.Forms.TextBox();
            this.lblDescripcionFiltro = new System.Windows.Forms.Label();
            this.tipoEtiquetaFiltroComboBox = new System.Windows.Forms.ComboBox();
            this.lblTipoEtiquetaFiltro = new System.Windows.Forms.Label();
            this.soloActivosCheckBox = new System.Windows.Forms.CheckBox();

            // DataGridView principal
            this.maestrosGrid = new System.Windows.Forms.DataGridView();
            this.colPartId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUPC1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDescripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTipo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colColor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEstado = new System.Windows.Forms.DataGridViewTextBoxColumn();

            // Panel de estadísticas
            this.panelEstadisticas = new System.Windows.Forms.Panel();
            this.lblEstadisticasTitulo = new System.Windows.Forms.Label();
            this.lblTotalMaestrosTxt = new System.Windows.Forms.Label();
            this.lblTotalMaestros = new System.Windows.Forms.Label();
            this.lblActivosTxt = new System.Windows.Forms.Label();
            this.lblActivos = new System.Windows.Forms.Label();
            this.lblInactivosTxt = new System.Windows.Forms.Label();
            this.lblInactivos = new System.Windows.Forms.Label();
            this.lblValidosImpresionTxt = new System.Windows.Forms.Label();
            this.lblValidosImpresion = new System.Windows.Forms.Label();

            // Panel de datos/edición
            this.panelDatos = new System.Windows.Forms.Panel();
            this.lblDatos = new System.Windows.Forms.Label();

            // Controles de datos principales
            this.lblPartIdDato = new System.Windows.Forms.Label();
            this.partIdDatoTextBox = new System.Windows.Forms.TextBox();
            this.lblUPC1 = new System.Windows.Forms.Label();
            this.upc1TextBox = new System.Windows.Forms.TextBox();
            this.lblUPC2 = new System.Windows.Forms.Label();
            this.upc2TextBox = new System.Windows.Forms.TextBox();
            this.lblDescripcion = new System.Windows.Forms.Label();
            this.descripcionTextBox = new System.Windows.Forms.TextBox();

            // Controles de configuración
            this.lblTipoEtiqueta = new System.Windows.Forms.Label();
            this.tipoEtiquetaComboBox = new System.Windows.Forms.ComboBox();
            this.lblColor = new System.Windows.Forms.Label();
            this.colorComboBox = new System.Windows.Forms.ComboBox();
            this.lblVelocidad = new System.Windows.Forms.Label();
            this.velocidadNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.lblTemperatura = new System.Windows.Forms.Label();
            this.temperaturaNumericUpDown = new System.Windows.Forms.NumericUpDown();

            // Controles de logo
            this.requireLogoCheckBox = new System.Windows.Forms.CheckBox();
            this.lblLogo = new System.Windows.Forms.Label();
            this.logoTextBox = new System.Windows.Forms.TextBox();

            // Observaciones
            this.lblObservaciones = new System.Windows.Forms.Label();
            this.observacionesTextBox = new System.Windows.Forms.TextBox();

            // Botones de UPC
            this.btnValidarUPC = new System.Windows.Forms.Button();
            this.btnGenerarUPC = new System.Windows.Forms.Button();

            // Labels de información
            this.lblFechaCreacionTxt = new System.Windows.Forms.Label();
            this.lblFechaCreacion = new System.Windows.Forms.Label();
            this.lblUsuarioCreacionTxt = new System.Windows.Forms.Label();
            this.lblUsuarioCreacion = new System.Windows.Forms.Label();
            this.lblFechaModificacionTxt = new System.Windows.Forms.Label();
            this.lblFechaModificacion = new System.Windows.Forms.Label();
            this.lblEstadoTxt = new System.Windows.Forms.Label();
            this.lblEstado = new System.Windows.Forms.Label();
            this.lblValidacionesTxt = new System.Windows.Forms.Label();
            this.lblValidaciones = new System.Windows.Forms.Label();
            this.lblEjemploUPCTxt = new System.Windows.Forms.Label();
            this.lblEjemploUPC = new System.Windows.Forms.Label();

            // Botones principales
            this.btnNuevo = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnActivar = new System.Windows.Forms.Button();
            this.btnClonar = new System.Windows.Forms.Button();

            // Status bar
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.contadorLabel = new System.Windows.Forms.ToolStripStatusLabel();

            ((System.ComponentModel.ISupportInitialize)(this.maestrosGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.velocidadNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.temperaturaNumericUpDown)).BeginInit();
            this.panelEstadisticas.SuspendLayout();
            this.panelDatos.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();

            // 
            // lblFiltros
            // 
            this.lblFiltros.AutoSize = true;
            this.lblFiltros.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.lblFiltros.Location = new System.Drawing.Point(12, 15);
            this.lblFiltros.Name = "lblFiltros";
            this.lblFiltros.Size = new System.Drawing.Size(49, 15);
            this.lblFiltros.TabIndex = 0;
            this.lblFiltros.Text = "Filtros:";

            // 
            // lblPartId
            // 
            this.lblPartId.AutoSize = true;
            this.lblPartId.Location = new System.Drawing.Point(12, 45);
            this.lblPartId.Name = "lblPartId";
            this.lblPartId.Size = new System.Drawing.Size(43, 13);
            this.lblPartId.TabIndex = 1;
            this.lblPartId.Text = "Part ID:";

            // 
            // partIdTextBox
            // 
            this.partIdTextBox.Location = new System.Drawing.Point(12, 65);
            this.partIdTextBox.Name = "partIdTextBox";
            this.partIdTextBox.Size = new System.Drawing.Size(120, 20);
            this.partIdTextBox.TabIndex = 2;
            this.partIdTextBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;

            // 
            // lblDescripcionFiltro
            // 
            this.lblDescripcionFiltro.AutoSize = true;
            this.lblDescripcionFiltro.Location = new System.Drawing.Point(150, 45);
            this.lblDescripcionFiltro.Name = "lblDescripcionFiltro";
            this.lblDescripcionFiltro.Size = new System.Drawing.Size(66, 13);
            this.lblDescripcionFiltro.TabIndex = 3;
            this.lblDescripcionFiltro.Text = "Descripción:";

            // 
            // descripcionFiltroTextBox
            // 
            this.descripcionFiltroTextBox.Location = new System.Drawing.Point(150, 65);
            this.descripcionFiltroTextBox.Name = "descripcionFiltroTextBox";
            this.descripcionFiltroTextBox.Size = new System.Drawing.Size(200, 20);
            this.descripcionFiltroTextBox.TabIndex = 4;

            // 
            // lblTipoEtiquetaFiltro
            // 
            this.lblTipoEtiquetaFiltro.AutoSize = true;
            this.lblTipoEtiquetaFiltro.Location = new System.Drawing.Point(370, 45);
            this.lblTipoEtiquetaFiltro.Name = "lblTipoEtiquetaFiltro";
            this.lblTipoEtiquetaFiltro.Size = new System.Drawing.Size(31, 13);
            this.lblTipoEtiquetaFiltro.TabIndex = 5;
            this.lblTipoEtiquetaFiltro.Text = "Tipo:";

            // 
            // tipoEtiquetaFiltroComboBox
            // 
            this.tipoEtiquetaFiltroComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tipoEtiquetaFiltroComboBox.Location = new System.Drawing.Point(370, 65);
            this.tipoEtiquetaFiltroComboBox.Name = "tipoEtiquetaFiltroComboBox";
            this.tipoEtiquetaFiltroComboBox.Size = new System.Drawing.Size(120, 21);
            this.tipoEtiquetaFiltroComboBox.TabIndex = 6;

            // 
            // soloActivosCheckBox
            // 
            this.soloActivosCheckBox.AutoSize = true;
            this.soloActivosCheckBox.Checked = true;
            this.soloActivosCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.soloActivosCheckBox.Location = new System.Drawing.Point(510, 67);
            this.soloActivosCheckBox.Name = "soloActivosCheckBox";
            this.soloActivosCheckBox.Size = new System.Drawing.Size(86, 17);
            this.soloActivosCheckBox.TabIndex = 7;
            this.soloActivosCheckBox.Text = "Solo Activos";
            this.soloActivosCheckBox.UseVisualStyleBackColor = true;

            // 
            // maestrosGrid
            // 
            this.maestrosGrid.AllowUserToAddRows = false;
            this.maestrosGrid.AllowUserToDeleteRows = false;
            this.maestrosGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.maestrosGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.maestrosGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.maestrosGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colPartId,
            this.colUPC1,
            this.colDescripcion,
            this.colTipo,
            this.colColor,
            this.colEstado});
            this.maestrosGrid.Location = new System.Drawing.Point(12, 100);
            this.maestrosGrid.MultiSelect = false;
            this.maestrosGrid.Name = "maestrosGrid";
            this.maestrosGrid.ReadOnly = true;
            this.maestrosGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.maestrosGrid.Size = new System.Drawing.Size(940, 280);
            this.maestrosGrid.TabIndex = 8;

            // 
            // colPartId
            // 
            this.colPartId.HeaderText = "Part ID";
            this.colPartId.Name = "colPartId";
            this.colPartId.ReadOnly = true;

            // 
            // colUPC1
            // 
            this.colUPC1.HeaderText = "UPC";
            this.colUPC1.Name = "colUPC1";
            this.colUPC1.ReadOnly = true;

            // 
            // colDescripcion
            // 
            this.colDescripcion.HeaderText = "Descripción";
            this.colDescripcion.Name = "colDescripcion";
            this.colDescripcion.ReadOnly = true;

            // 
            // colTipo
            // 
            this.colTipo.HeaderText = "Tipo";
            this.colTipo.Name = "colTipo";
            this.colTipo.ReadOnly = true;

            // 
            // colColor
            // 
            this.colColor.HeaderText = "Color";
            this.colColor.Name = "colColor";
            this.colColor.ReadOnly = true;

            // 
            // colEstado
            // 
            this.colEstado.HeaderText = "Estado";
            this.colEstado.Name = "colEstado";
            this.colEstado.ReadOnly = true;

            // 
            // panelEstadisticas
            // 
            this.panelEstadisticas.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panelEstadisticas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelEstadisticas.Controls.Add(this.lblEstadisticasTitulo);
            this.panelEstadisticas.Controls.Add(this.lblTotalMaestrosTxt);
            this.panelEstadisticas.Controls.Add(this.lblTotalMaestros);
            this.panelEstadisticas.Controls.Add(this.lblActivosTxt);
            this.panelEstadisticas.Controls.Add(this.lblActivos);
            this.panelEstadisticas.Controls.Add(this.lblInactivosTxt);
            this.panelEstadisticas.Controls.Add(this.lblInactivos);
            this.panelEstadisticas.Controls.Add(this.lblValidosImpresionTxt);
            this.panelEstadisticas.Controls.Add(this.lblValidosImpresion);
            this.panelEstadisticas.Location = new System.Drawing.Point(970, 100);
            this.panelEstadisticas.Name = "panelEstadisticas";
            this.panelEstadisticas.Size = new System.Drawing.Size(220, 120);
            this.panelEstadisticas.TabIndex = 9;

            // 
            // lblEstadisticasTitulo
            // 
            this.lblEstadisticasTitulo.AutoSize = true;
            this.lblEstadisticasTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblEstadisticasTitulo.Location = new System.Drawing.Point(10, 10);
            this.lblEstadisticasTitulo.Name = "lblEstadisticasTitulo";
            this.lblEstadisticasTitulo.Size = new System.Drawing.Size(75, 13);
            this.lblEstadisticasTitulo.TabIndex = 0;
            this.lblEstadisticasTitulo.Text = "Estadísticas:";

            // 
            // lblTotalMaestrosTxt
            // 
            this.lblTotalMaestrosTxt.AutoSize = true;
            this.lblTotalMaestrosTxt.Location = new System.Drawing.Point(10, 30);
            this.lblTotalMaestrosTxt.Name = "lblTotalMaestrosTxt";
            this.lblTotalMaestrosTxt.Size = new System.Drawing.Size(34, 13);
            this.lblTotalMaestrosTxt.TabIndex = 1;
            this.lblTotalMaestrosTxt.Text = "Total:";

            // 
            // lblTotalMaestros
            // 
            this.lblTotalMaestros.AutoSize = true;
            this.lblTotalMaestros.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblTotalMaestros.Location = new System.Drawing.Point(120, 30);
            this.lblTotalMaestros.Name = "lblTotalMaestros";
            this.lblTotalMaestros.Size = new System.Drawing.Size(14, 13);
            this.lblTotalMaestros.TabIndex = 2;
            this.lblTotalMaestros.Text = "0";

            // 
            // lblActivosTxt
            // 
            this.lblActivosTxt.AutoSize = true;
            this.lblActivosTxt.Location = new System.Drawing.Point(10, 50);
            this.lblActivosTxt.Name = "lblActivosTxt";
            this.lblActivosTxt.Size = new System.Drawing.Size(43, 13);
            this.lblActivosTxt.TabIndex = 3;
            this.lblActivosTxt.Text = "Activos:";

            // 
            // lblActivos
            // 
            this.lblActivos.AutoSize = true;
            this.lblActivos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblActivos.ForeColor = System.Drawing.Color.Green;
            this.lblActivos.Location = new System.Drawing.Point(120, 50);
            this.lblActivos.Name = "lblActivos";
            this.lblActivos.Size = new System.Drawing.Size(14, 13);
            this.lblActivos.TabIndex = 4;
            this.lblActivos.Text = "0";

            // 
            // lblInactivosTxt
            // 
            this.lblInactivosTxt.AutoSize = true;
            this.lblInactivosTxt.Location = new System.Drawing.Point(10, 70);
            this.lblInactivosTxt.Name = "lblInactivosTxt";
            this.lblInactivosTxt.Size = new System.Drawing.Size(52, 13);
            this.lblInactivosTxt.TabIndex = 5;
            this.lblInactivosTxt.Text = "Inactivos:";

            // 
            // lblInactivos
            // 
            this.lblInactivos.AutoSize = true;
            this.lblInactivos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblInactivos.ForeColor = System.Drawing.Color.Red;
            this.lblInactivos.Location = new System.Drawing.Point(120, 70);
            this.lblInactivos.Name = "lblInactivos";
            this.lblInactivos.Size = new System.Drawing.Size(14, 13);
            this.lblInactivos.TabIndex = 6;
            this.lblInactivos.Text = "0";

            // 
            // lblValidosImpresionTxt
            // 
            this.lblValidosImpresionTxt.AutoSize = true;
            this.lblValidosImpresionTxt.Location = new System.Drawing.Point(10, 90);
            this.lblValidosImpresionTxt.Name = "lblValidosImpresionTxt";
            this.lblValidosImpresionTxt.Size = new System.Drawing.Size(102, 13);
            this.lblValidosImpresionTxt.TabIndex = 7;
            this.lblValidosImpresionTxt.Text = "Válidos impresión:";

            // 
            // lblValidosImpresion
            // 
            this.lblValidosImpresion.AutoSize = true;
            this.lblValidosImpresion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblValidosImpresion.ForeColor = System.Drawing.Color.Blue;
            this.lblValidosImpresion.Location = new System.Drawing.Point(120, 90);
            this.lblValidosImpresion.Name = "lblValidosImpresion";
            this.lblValidosImpresion.Size = new System.Drawing.Size(14, 13);
            this.lblValidosImpresion.TabIndex = 8;
            this.lblValidosImpresion.Text = "0";

            // 
            // panelDatos
            // 
            this.panelDatos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelDatos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelDatos.Controls.Add(this.lblDatos);
            this.panelDatos.Controls.Add(this.lblPartIdDato);
            this.panelDatos.Controls.Add(this.partIdDatoTextBox);
            this.panelDatos.Controls.Add(this.lblUPC1);
            this.panelDatos.Controls.Add(this.upc1TextBox);
            this.panelDatos.Controls.Add(this.lblUPC2);
            this.panelDatos.Controls.Add(this.upc2TextBox);
            this.panelDatos.Controls.Add(this.lblDescripcion);
            this.panelDatos.Controls.Add(this.descripcionTextBox);
            this.panelDatos.Controls.Add(this.lblTipoEtiqueta);
            this.panelDatos.Controls.Add(this.tipoEtiquetaComboBox);
            this.panelDatos.Controls.Add(this.lblColor);
            this.panelDatos.Controls.Add(this.colorComboBox);
            this.panelDatos.Controls.Add(this.lblVelocidad);
            this.panelDatos.Controls.Add(this.velocidadNumericUpDown);
            this.panelDatos.Controls.Add(this.lblTemperatura);
            this.panelDatos.Controls.Add(this.temperaturaNumericUpDown);
            this.panelDatos.Controls.Add(this.requireLogoCheckBox);
            this.panelDatos.Controls.Add(this.lblLogo);
            this.panelDatos.Controls.Add(this.logoTextBox);
            this.panelDatos.Controls.Add(this.lblObservaciones);
            this.panelDatos.Controls.Add(this.observacionesTextBox);
            this.panelDatos.Controls.Add(this.btnValidarUPC);
            this.panelDatos.Controls.Add(this.btnGenerarUPC);
            this.panelDatos.Controls.Add(this.lblFechaCreacionTxt);
            this.panelDatos.Controls.Add(this.lblFechaCreacion);
            this.panelDatos.Controls.Add(this.lblUsuarioCreacionTxt);
            this.panelDatos.Controls.Add(this.lblUsuarioCreacion);
            this.panelDatos.Controls.Add(this.lblFechaModificacionTxt);
            this.panelDatos.Controls.Add(this.lblFechaModificacion);
            this.panelDatos.Controls.Add(this.lblEstadoTxt);
            this.panelDatos.Controls.Add(this.lblEstado);
            this.panelDatos.Controls.Add(this.lblValidacionesTxt);
            this.panelDatos.Controls.Add(this.lblValidaciones);
            this.panelDatos.Controls.Add(this.lblEjemploUPCTxt);
            this.panelDatos.Controls.Add(this.lblEjemploUPC);
            this.panelDatos.Location = new System.Drawing.Point(12, 390);
            this.panelDatos.Name = "panelDatos";
            this.panelDatos.Size = new System.Drawing.Size(1050, 220);
            this.panelDatos.TabIndex = 10;

            // 
            // lblDatos
            // 
            this.lblDatos.AutoSize = true;
            this.lblDatos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.lblDatos.Location = new System.Drawing.Point(10, 10);
            this.lblDatos.Name = "lblDatos";
            this.lblDatos.Size = new System.Drawing.Size(121, 15);
            this.lblDatos.TabIndex = 0;
            this.lblDatos.Text = "Datos de Etiqueta:";

            // Primera fila de controles
            // 
            // lblPartIdDato
            // 
            this.lblPartIdDato.AutoSize = true;
            this.lblPartIdDato.Location = new System.Drawing.Point(10, 35);
            this.lblPartIdDato.Name = "lblPartIdDato";
            this.lblPartIdDato.Size = new System.Drawing.Size(43, 13);
            this.lblPartIdDato.TabIndex = 1;
            this.lblPartIdDato.Text = "Part ID:";

            // 
            // partIdDatoTextBox
            // 
            this.partIdDatoTextBox.Location = new System.Drawing.Point(10, 55);
            this.partIdDatoTextBox.Name = "partIdDatoTextBox";
            this.partIdDatoTextBox.Size = new System.Drawing.Size(120, 20);
            this.partIdDatoTextBox.TabIndex = 2;
            this.partIdDatoTextBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;

            // 
            // lblUPC1
            // 
            this.lblUPC1.AutoSize = true;
            this.lblUPC1.Location = new System.Drawing.Point(140, 35);
            this.lblUPC1.Name = "lblUPC1";
            this.lblUPC1.Size = new System.Drawing.Size(38, 13);
            this.lblUPC1.TabIndex = 3;
            this.lblUPC1.Text = "UPC1:";

            // 
            // upc1TextBox
            // 
            this.upc1TextBox.Location = new System.Drawing.Point(140, 55);
            this.upc1TextBox.Name = "upc1TextBox";
            this.upc1TextBox.Size = new System.Drawing.Size(120, 20);
            this.upc1TextBox.TabIndex = 4;

            // 
            // lblUPC2
            // 
            this.lblUPC2.AutoSize = true;
            this.lblUPC2.Location = new System.Drawing.Point(270, 35);
            this.lblUPC2.Name = "lblUPC2";
            this.lblUPC2.Size = new System.Drawing.Size(38, 13);
            this.lblUPC2.TabIndex = 5;
            this.lblUPC2.Text = "UPC2:";

            // 
            // upc2TextBox
            // 
            this.upc2TextBox.Location = new System.Drawing.Point(270, 55);
            this.upc2TextBox.Name = "upc2TextBox";
            this.upc2TextBox.Size = new System.Drawing.Size(120, 20);
            this.upc2TextBox.TabIndex = 6;

            // 
            // lblDescripcion
            // 
            this.lblDescripcion.AutoSize = true;
            this.lblDescripcion.Location = new System.Drawing.Point(400, 35);
            this.lblDescripcion.Name = "lblDescripcion";
            this.lblDescripcion.Size = new System.Drawing.Size(66, 13);
            this.lblDescripcion.TabIndex = 7;
            this.lblDescripcion.Text = "Descripción:";

            // 
            // descripcionTextBox
            // 
            this.descripcionTextBox.Location = new System.Drawing.Point(400, 55);
            this.descripcionTextBox.Name = "descripcionTextBox";
            this.descripcionTextBox.Size = new System.Drawing.Size(250, 20);
            this.descripcionTextBox.TabIndex = 8;

            // Botones UPC
            // 
            // btnValidarUPC
            // 
            this.btnValidarUPC.Location = new System.Drawing.Point(670, 35);
            this.btnValidarUPC.Name = "btnValidarUPC";
            this.btnValidarUPC.Size = new System.Drawing.Size(80, 23);
            this.btnValidarUPC.TabIndex = 9;
            this.btnValidarUPC.Text = "Validar UPC";
            this.btnValidarUPC.UseVisualStyleBackColor = true;

            // 
            // btnGenerarUPC
            // 
            this.btnGenerarUPC.Location = new System.Drawing.Point(670, 65);
            this.btnGenerarUPC.Name = "btnGenerarUPC";
            this.btnGenerarUPC.Size = new System.Drawing.Size(80, 23);
            this.btnGenerarUPC.TabIndex = 10;
            this.btnGenerarUPC.Text = "Generar UPC";
            this.btnGenerarUPC.UseVisualStyleBackColor = true;

            // Segunda fila de controles
            // 
            // lblTipoEtiqueta
            // 
            this.lblTipoEtiqueta.AutoSize = true;
            this.lblTipoEtiqueta.Location = new System.Drawing.Point(10, 85);
            this.lblTipoEtiqueta.Name = "lblTipoEtiqueta";
            this.lblTipoEtiqueta.Size = new System.Drawing.Size(31, 13);
            this.lblTipoEtiqueta.TabIndex = 11;
            this.lblTipoEtiqueta.Text = "Tipo:";

            // 
            // tipoEtiquetaComboBox
            // 
            this.tipoEtiquetaComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tipoEtiquetaComboBox.Location = new System.Drawing.Point(10, 105);
            this.tipoEtiquetaComboBox.Name = "tipoEtiquetaComboBox";
            this.tipoEtiquetaComboBox.Size = new System.Drawing.Size(120, 21);
            this.tipoEtiquetaComboBox.TabIndex = 12;

            // 
            // lblColor
            // 
            this.lblColor.AutoSize = true;
            this.lblColor.Location = new System.Drawing.Point(140, 85);
            this.lblColor.Name = "lblColor";
            this.lblColor.Size = new System.Drawing.Size(34, 13);
            this.lblColor.TabIndex = 13;
            this.lblColor.Text = "Color:";

            // 
            // colorComboBox
            // 
            this.colorComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.colorComboBox.Location = new System.Drawing.Point(140, 105);
            this.colorComboBox.Name = "colorComboBox";
            this.colorComboBox.Size = new System.Drawing.Size(120, 21);
            this.colorComboBox.TabIndex = 14;

            // 
            // lblVelocidad
            // 
            this.lblVelocidad.AutoSize = true;
            this.lblVelocidad.Location = new System.Drawing.Point(270, 85);
            this.lblVelocidad.Name = "lblVelocidad";
            this.lblVelocidad.Size = new System.Drawing.Size(57, 13);
            this.lblVelocidad.TabIndex = 15;
            this.lblVelocidad.Text = "Velocidad:";

            // 
            // velocidadNumericUpDown
            // 
            this.velocidadNumericUpDown.Location = new System.Drawing.Point(270, 105);
            this.velocidadNumericUpDown.Maximum = new decimal(new int[] { 12, 0, 0, 0 });
            this.velocidadNumericUpDown.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.velocidadNumericUpDown.Name = "velocidadNumericUpDown";
            this.velocidadNumericUpDown.Size = new System.Drawing.Size(60, 20);
            this.velocidadNumericUpDown.TabIndex = 16;
            this.velocidadNumericUpDown.Value = new decimal(new int[] { 4, 0, 0, 0 });

            // 
            // lblTemperatura
            // 
            this.lblTemperatura.AutoSize = true;
            this.lblTemperatura.Location = new System.Drawing.Point(340, 85);
            this.lblTemperatura.Name = "lblTemperatura";
            this.lblTemperatura.Size = new System.Drawing.Size(70, 13);
            this.lblTemperatura.TabIndex = 17;
            this.lblTemperatura.Text = "Temperatura:";

            // 
            // temperaturaNumericUpDown
            // 
            this.temperaturaNumericUpDown.Location = new System.Drawing.Point(340, 105);
            this.temperaturaNumericUpDown.Maximum = new decimal(new int[] { 30, 0, 0, 0 });
            this.temperaturaNumericUpDown.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.temperaturaNumericUpDown.Name = "temperaturaNumericUpDown";
            this.temperaturaNumericUpDown.Size = new System.Drawing.Size(60, 20);
            this.temperaturaNumericUpDown.TabIndex = 18;
            this.temperaturaNumericUpDown.Value = new decimal(new int[] { 15, 0, 0, 0 });

            // 
            // requireLogoCheckBox
            // 
            this.requireLogoCheckBox.AutoSize = true;
            this.requireLogoCheckBox.Location = new System.Drawing.Point(420, 107);
            this.requireLogoCheckBox.Name = "requireLogoCheckBox";
            this.requireLogoCheckBox.Size = new System.Drawing.Size(96, 17);
            this.requireLogoCheckBox.TabIndex = 19;
            this.requireLogoCheckBox.Text = "Requiere Logo";
            this.requireLogoCheckBox.UseVisualStyleBackColor = true;

            // 
            // lblLogo
            // 
            this.lblLogo.AutoSize = true;
            this.lblLogo.Location = new System.Drawing.Point(530, 85);
            this.lblLogo.Name = "lblLogo";
            this.lblLogo.Size = new System.Drawing.Size(34, 13);
            this.lblLogo.TabIndex = 20;
            this.lblLogo.Text = "Logo:";

            // 
            // logoTextBox
            // 
            this.logoTextBox.Location = new System.Drawing.Point(530, 105);
            this.logoTextBox.Name = "logoTextBox";
            this.logoTextBox.Size = new System.Drawing.Size(120, 20);
            this.logoTextBox.TabIndex = 21;

            // Tercera fila - Observaciones
            // 
            // lblObservaciones
            // 
            this.lblObservaciones.AutoSize = true;
            this.lblObservaciones.Location = new System.Drawing.Point(10, 135);
            this.lblObservaciones.Name = "lblObservaciones";
            this.lblObservaciones.Size = new System.Drawing.Size(81, 13);
            this.lblObservaciones.TabIndex = 22;
            this.lblObservaciones.Text = "Observaciones:";

            // 
            // observacionesTextBox
            // 
            this.observacionesTextBox.Location = new System.Drawing.Point(10, 155);
            this.observacionesTextBox.Multiline = true;
            this.observacionesTextBox.Name = "observacionesTextBox";
            this.observacionesTextBox.Size = new System.Drawing.Size(500, 40);
            this.observacionesTextBox.TabIndex = 23;

            // Información del registro
            // 
            // lblFechaCreacionTxt
            // 
            this.lblFechaCreacionTxt.AutoSize = true;
            this.lblFechaCreacionTxt.Location = new System.Drawing.Point(530, 135);
            this.lblFechaCreacionTxt.Name = "lblFechaCreacionTxt";
            this.lblFechaCreacionTxt.Size = new System.Drawing.Size(74, 13);
            this.lblFechaCreacionTxt.TabIndex = 24;
            this.lblFechaCreacionTxt.Text = "F. Creación:";

            // 
            // lblFechaCreacion
            // 
            this.lblFechaCreacion.AutoSize = true;
            this.lblFechaCreacion.Location = new System.Drawing.Point(610, 135);
            this.lblFechaCreacion.Name = "lblFechaCreacion";
            this.lblFechaCreacion.Size = new System.Drawing.Size(0, 13);
            this.lblFechaCreacion.TabIndex = 25;

            // 
            // lblUsuarioCreacionTxt
            // 
            this.lblUsuarioCreacionTxt.AutoSize = true;
            this.lblUsuarioCreacionTxt.Location = new System.Drawing.Point(530, 155);
            this.lblUsuarioCreacionTxt.Name = "lblUsuarioCreacionTxt";
            this.lblUsuarioCreacionTxt.Size = new System.Drawing.Size(46, 13);
            this.lblUsuarioCreacionTxt.TabIndex = 26;
            this.lblUsuarioCreacionTxt.Text = "Usuario:";

            // 
            // lblUsuarioCreacion
            // 
            this.lblUsuarioCreacion.AutoSize = true;
            this.lblUsuarioCreacion.Location = new System.Drawing.Point(610, 155);
            this.lblUsuarioCreacion.Name = "lblUsuarioCreacion";
            this.lblUsuarioCreacion.Size = new System.Drawing.Size(0, 13);
            this.lblUsuarioCreacion.TabIndex = 27;

            // 
            // lblFechaModificacionTxt
            // 
            this.lblFechaModificacionTxt.AutoSize = true;
            this.lblFechaModificacionTxt.Location = new System.Drawing.Point(530, 175);
            this.lblFechaModificacionTxt.Name = "lblFechaModificacionTxt";
            this.lblFechaModificacionTxt.Size = new System.Drawing.Size(87, 13);
            this.lblFechaModificacionTxt.TabIndex = 28;
            this.lblFechaModificacionTxt.Text = "F. Modificación:";

            // 
            // lblFechaModificacion
            // 
            this.lblFechaModificacion.AutoSize = true;
            this.lblFechaModificacion.Location = new System.Drawing.Point(620, 175);
            this.lblFechaModificacion.Name = "lblFechaModificacion";
            this.lblFechaModificacion.Size = new System.Drawing.Size(0, 13);
            this.lblFechaModificacion.TabIndex = 29;

            // 
            // lblEstadoTxt
            // 
            this.lblEstadoTxt.AutoSize = true;
            this.lblEstadoTxt.Location = new System.Drawing.Point(760, 135);
            this.lblEstadoTxt.Name = "lblEstadoTxt";
            this.lblEstadoTxt.Size = new System.Drawing.Size(43, 13);
            this.lblEstadoTxt.TabIndex = 30;
            this.lblEstadoTxt.Text = "Estado:";

            // 
            // lblEstado
            // 
            this.lblEstado.AutoSize = true;
            this.lblEstado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblEstado.Location = new System.Drawing.Point(810, 135);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(0, 13);
            this.lblEstado.TabIndex = 31;

            // 
            // lblValidacionesTxt
            // 
            this.lblValidacionesTxt.AutoSize = true;
            this.lblValidacionesTxt.Location = new System.Drawing.Point(760, 155);
            this.lblValidacionesTxt.Name = "lblValidacionesTxt";
            this.lblValidacionesTxt.Size = new System.Drawing.Size(72, 13);
            this.lblValidacionesTxt.TabIndex = 32;
            this.lblValidacionesTxt.Text = "Validaciones:";

            // 
            // lblValidaciones
            // 
            this.lblValidaciones.AutoSize = true;
            this.lblValidaciones.Location = new System.Drawing.Point(760, 175);
            this.lblValidaciones.Name = "lblValidaciones";
            this.lblValidaciones.Size = new System.Drawing.Size(0, 13);
            this.lblValidaciones.TabIndex = 33;

            // 
            // lblEjemploUPCTxt
            // 
            this.lblEjemploUPCTxt.AutoSize = true;
            this.lblEjemploUPCTxt.Location = new System.Drawing.Point(760, 195);
            this.lblEjemploUPCTxt.Name = "lblEjemploUPCTxt";
            this.lblEjemploUPCTxt.Size = new System.Drawing.Size(50, 13);
            this.lblEjemploUPCTxt.TabIndex = 34;
            this.lblEjemploUPCTxt.Text = "Ejemplo:";

            // 
            // lblEjemploUPC
            // 
            this.lblEjemploUPC.AutoSize = true;
            this.lblEjemploUPC.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblEjemploUPC.Location = new System.Drawing.Point(820, 195);
            this.lblEjemploUPC.Name = "lblEjemploUPC";
            this.lblEjemploUPC.Size = new System.Drawing.Size(0, 13);
            this.lblEjemploUPC.TabIndex = 35;

            // Botones principales
            // 
            // btnNuevo
            // 
            this.btnNuevo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNuevo.BackColor = System.Drawing.Color.LightGreen;
            this.btnNuevo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnNuevo.Location = new System.Drawing.Point(970, 230);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(75, 30);
            this.btnNuevo.TabIndex = 11;
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.UseVisualStyleBackColor = false;

            // 
            // btnGuardar
            // 
            this.btnGuardar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGuardar.BackColor = System.Drawing.Color.LightBlue;
            this.btnGuardar.Enabled = false;
            this.btnGuardar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnGuardar.Location = new System.Drawing.Point(1055, 230);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(75, 30);
            this.btnGuardar.TabIndex = 12;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = false;

            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelar.Enabled = false;
            this.btnCancelar.Location = new System.Drawing.Point(1140, 230);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 30);
            this.btnCancelar.TabIndex = 13;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;

            // 
            // btnEliminar
            // 
            this.btnEliminar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEliminar.BackColor = System.Drawing.Color.LightCoral;
            this.btnEliminar.Enabled = false;
            this.btnEliminar.Location = new System.Drawing.Point(970, 270);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(75, 30);
            this.btnEliminar.TabIndex = 14;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = false;

            // 
            // btnActivar
            // 
            this.btnActivar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnActivar.BackColor = System.Drawing.Color.LightYellow;
            this.btnActivar.Enabled = false;
            this.btnActivar.Location = new System.Drawing.Point(1055, 270);
            this.btnActivar.Name = "btnActivar";
            this.btnActivar.Size = new System.Drawing.Size(75, 30);
            this.btnActivar.TabIndex = 15;
            this.btnActivar.Text = "Activar";
            this.btnActivar.UseVisualStyleBackColor = false;

            // 
            // btnClonar
            // 
            this.btnClonar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClonar.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btnClonar.Enabled = false;
            this.btnClonar.Location = new System.Drawing.Point(1140, 270);
            this.btnClonar.Name = "btnClonar";
            this.btnClonar.Size = new System.Drawing.Size(75, 30);
            this.btnClonar.TabIndex = 16;
            this.btnClonar.Text = "Clonar";
            this.btnClonar.UseVisualStyleBackColor = false;

            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel,
            this.contadorLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 620);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1230, 22);
            this.statusStrip.TabIndex = 17;

            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(42, 17);
            this.statusLabel.Text = "Listo...";

            // 
            // contadorLabel
            // 
            this.contadorLabel.Name = "contadorLabel";
            this.contadorLabel.Size = new System.Drawing.Size(51, 17);
            this.contadorLabel.Text = "0 registros";
            this.contadorLabel.Spring = true;
            this.contadorLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            // 
            // CodificaEtiquetasForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1230, 642);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.btnClonar);
            this.Controls.Add(this.btnActivar);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.btnNuevo);
            this.Controls.Add(this.panelDatos);
            this.Controls.Add(this.panelEstadisticas);
            this.Controls.Add(this.maestrosGrid);
            this.Controls.Add(this.soloActivosCheckBox);
            this.Controls.Add(this.tipoEtiquetaFiltroComboBox);
            this.Controls.Add(this.lblTipoEtiquetaFiltro);
            this.Controls.Add(this.descripcionFiltroTextBox);
            this.Controls.Add(this.lblDescripcionFiltro);
            this.Controls.Add(this.partIdTextBox);
            this.Controls.Add(this.lblPartId);
            this.Controls.Add(this.lblFiltros);
            this.MinimumSize = new System.Drawing.Size(1246, 680);
            this.Name = "CodificaEtiquetasForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Codifica Etiquetas - Sistema de Etiquetas Molduras";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.maestrosGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.velocidadNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.temperaturaNumericUpDown)).EndInit();
            this.panelEstadisticas.ResumeLayout(false);
            this.panelEstadisticas.PerformLayout();
            this.panelDatos.ResumeLayout(false);
            this.panelDatos.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        // Controles de filtro superiores
        private System.Windows.Forms.Label lblFiltros;
        private System.Windows.Forms.Label lblPartId;
        private System.Windows.Forms.TextBox partIdTextBox;
        private System.Windows.Forms.Label lblDescripcionFiltro;
        private System.Windows.Forms.TextBox descripcionFiltroTextBox;
        private System.Windows.Forms.Label lblTipoEtiquetaFiltro;
        private System.Windows.Forms.ComboBox tipoEtiquetaFiltroComboBox;
        private System.Windows.Forms.CheckBox soloActivosCheckBox;

        // DataGridView principal y columnas
        private System.Windows.Forms.DataGridView maestrosGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPartId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUPC1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDescripcion;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTipo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colColor;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEstado;

        // Panel de estadísticas
        private System.Windows.Forms.Panel panelEstadisticas;
        private System.Windows.Forms.Label lblEstadisticasTitulo;
        private System.Windows.Forms.Label lblTotalMaestrosTxt;
        private System.Windows.Forms.Label lblTotalMaestros;
        private System.Windows.Forms.Label lblActivosTxt;
        private System.Windows.Forms.Label lblActivos;
        private System.Windows.Forms.Label lblInactivosTxt;
        private System.Windows.Forms.Label lblInactivos;
        private System.Windows.Forms.Label lblValidosImpresionTxt;
        private System.Windows.Forms.Label lblValidosImpresion;

        // Panel de datos/edición
        private System.Windows.Forms.Panel panelDatos;
        private System.Windows.Forms.Label lblDatos;

        // Controles de datos principales
        private System.Windows.Forms.Label lblPartIdDato;
        private System.Windows.Forms.TextBox partIdDatoTextBox;
        private System.Windows.Forms.Label lblUPC1;
        private System.Windows.Forms.TextBox upc1TextBox;
        private System.Windows.Forms.Label lblUPC2;
        private System.Windows.Forms.TextBox upc2TextBox;
        private System.Windows.Forms.Label lblDescripcion;
        private System.Windows.Forms.TextBox descripcionTextBox;

        // Controles de configuración
        private System.Windows.Forms.Label lblTipoEtiqueta;
        private System.Windows.Forms.ComboBox tipoEtiquetaComboBox;
        private System.Windows.Forms.Label lblColor;
        private System.Windows.Forms.ComboBox colorComboBox;
        private System.Windows.Forms.Label lblVelocidad;
        private System.Windows.Forms.NumericUpDown velocidadNumericUpDown;
        private System.Windows.Forms.Label lblTemperatura;
        private System.Windows.Forms.NumericUpDown temperaturaNumericUpDown;

        // Controles de logo
        private System.Windows.Forms.CheckBox requireLogoCheckBox;
        private System.Windows.Forms.Label lblLogo;
        private System.Windows.Forms.TextBox logoTextBox;

        // Observaciones
        private System.Windows.Forms.Label lblObservaciones;
        private System.Windows.Forms.TextBox observacionesTextBox;

        // Botones de UPC
        private System.Windows.Forms.Button btnValidarUPC;
        private System.Windows.Forms.Button btnGenerarUPC;

        // Labels de información
        private System.Windows.Forms.Label lblFechaCreacionTxt;
        private System.Windows.Forms.Label lblFechaCreacion;
        private System.Windows.Forms.Label lblUsuarioCreacionTxt;
        private System.Windows.Forms.Label lblUsuarioCreacion;
        private System.Windows.Forms.Label lblFechaModificacionTxt;
        private System.Windows.Forms.Label lblFechaModificacion;
        private System.Windows.Forms.Label lblEstadoTxt;
        private System.Windows.Forms.Label lblEstado;
        private System.Windows.Forms.Label lblValidacionesTxt;
        private System.Windows.Forms.Label lblValidaciones;
        private System.Windows.Forms.Label lblEjemploUPCTxt;
        private System.Windows.Forms.Label lblEjemploUPC;

        // Botones principales
        private System.Windows.Forms.Button btnNuevo;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnActivar;
        private System.Windows.Forms.Button btnClonar;

        // Status bar
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.ToolStripStatusLabel contadorLabel;
    }
}