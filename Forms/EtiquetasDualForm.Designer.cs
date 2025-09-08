using System;

namespace EtiquetasApp.Forms
{
    partial class EtiquetasDualForm
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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

            // Llamar al InitializeComponent de la clase base
            base.InitializeComponent();

            this.SuspendLayout();

            // Configuraciones específicas para DUAL
            this.Text = "Etiquetas DUAL";
            this.lblTipoEtiqueta.Text = "Etiquetas DUAL";
            this.lblTipoEtiqueta.ForeColor = System.Drawing.Color.DarkBlue;

            // Configurar eventos específicos
            this.papelComboBox.SelectedIndexChanged += new System.EventHandler(this.PapelComboBox_SelectedIndexChanged);

            // Ajustar título de columnas en la grilla para DUAL
            if (this.solicitudesGrid.Columns.Count > 0)
            {
                this.solicitudesGrid.Columns[0].HeaderText = "N° Sol.";
                this.solicitudesGrid.Columns[1].HeaderText = "Orden Fab.";
                this.solicitudesGrid.Columns[2].HeaderText = "Descripción";
                this.solicitudesGrid.Columns[3].HeaderText = "Cantidad";
                this.solicitudesGrid.Columns[4].HeaderText = "Color";
                this.solicitudesGrid.Columns[5].HeaderText = "Observaciones";
                this.solicitudesGrid.Columns[6].HeaderText = "Fecha Req.";
            }

            // Configurar GroupBox específico para DUAL
            this.etiquetaGroupBox.Text = "Configuración DUAL";

            // Configurar posiciones por defecto para DUAL
            this.posicionesGroupBox.Text = "Posición de columnas (DUAL)";

            // Mostrar y configurar control de etiqueta doble
            this.etiquetaDobleCheckBox.Visible = true;
            this.etiquetaDobleCheckBox.Checked = true;
            this.etiquetaDobleCheckBox.Enabled = false;
            this.etiquetaDobleCheckBox.Text = "Etiqueta Doble (Requerido)";

            // Configurar panel de colores específico para DUAL
            ConfigurarPanelColoresDual();

            // Agregar información específica de DUAL
            AgregarInformacionDual();

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void ConfigurarPanelColoresDual()
        {
            // Configurar el panel de colores específicamente para DUAL
            this.coloresPanel.BackColor = System.Drawing.Color.AliceBlue;

            // Configurar los 5 botones de colores para DUAL
            if (this.colorButtons != null)
            {
                for (int i = 0; i < this.colorButtons.Length; i++)
                {
                    this.colorButtons[i].BackColor = System.Drawing.Color.LightBlue;
                    this.colorButtons[i].FlatAppearance.BorderColor = System.Drawing.Color.DarkBlue;
                    this.colorButtons[i].FlatAppearance.BorderSize = 2;
                    this.colorButtons[i].Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold);
                    this.colorButtons[i].Text = "UPC1\nUPC2";
                    this.colorButtons[i].UseVisualStyleBackColor = false;

                    // Agregar tooltip con información dual
                    var tooltip = new System.Windows.Forms.ToolTip();
                    tooltip.SetToolTip(this.colorButtons[i], $"Posición {i + 1} - DUAL (Códigos duales)");
                }
            }
        }

        private void AgregarInformacionDual()
        {
            // Agregar label informativo sobre etiquetas DUAL
            var lblInfoDual = new System.Windows.Forms.Label();
            lblInfoDual.AutoSize = true;
            lblInfoDual.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            lblInfoDual.ForeColor = System.Drawing.Color.DarkBlue;
            lblInfoDual.Location = new System.Drawing.Point(15, 580);
            lblInfoDual.Name = "lblInfoDual";
            lblInfoDual.Size = new System.Drawing.Size(300, 13);
            lblInfoDual.TabIndex = 999;
            lblInfoDual.Text = "Las etiquetas DUAL requieren dos códigos UPC (UPC1 y UPC2)";
            this.Controls.Add(lblInfoDual);

            // Hacer más visible el campo UPC2 en el panel de información
            if (this.lblUPC2Txt != null)
            {
                this.lblUPC2Txt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                this.lblUPC2Txt.ForeColor = System.Drawing.Color.DarkBlue;
                this.lblUPC2Txt.Text = "UPC2 (Req.):";
            }

            if (this.lblUPC2 != null)
            {
                this.lblUPC2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                this.lblUPC2.ForeColor = System.Drawing.Color.DarkBlue;
            }
        }

        #endregion
    }
}