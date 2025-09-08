using System;

namespace EtiquetasApp.Forms
{
    partial class EtiquetasCBCOEForm
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

            // Configuraciones específicas para C/BCO-E
            this.Text = "Etiquetas C/BCO-E";
            this.lblTipoEtiqueta.Text = "Etiquetas C/BCO-E";
            this.lblTipoEtiqueta.ForeColor = System.Drawing.Color.DarkBlue;

            // Configurar eventos específicos
            this.papelComboBox.SelectedIndexChanged += new EventHandler(this.PapelComboBox_SelectedIndexChanged);

            // Ajustar título de columnas en la grilla para C/BCO-E
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

            // Configurar GroupBox específico para C/BCO-E
            this.etiquetaGroupBox.Text = "Configuración C/BCO-E";

            // Configurar posiciones por defecto para C/BCO-E
            this.posicionesGroupBox.Text = "Posición de columnas (C/BCO-E)";

            // Ocultar controles no necesarios para C/BCO-E
            this.etiquetaDobleCheckBox.Visible = false;

            // Configurar panel de colores específico para C/BCO-E
            ConfigurarPanelColoresCBCOE();

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void ConfigurarPanelColoresCBCOE()
        {
            // Configurar el panel de colores específicamente para C/BCO-E
            this.coloresPanel.BackColor = System.Drawing.Color.WhiteSmoke;

            // Configurar los 5 botones de colores para C/BCO-E
            if (this.colorButtons != null)
            {
                for (int i = 0; i < this.colorButtons.Length; i++)
                {
                    this.colorButtons[i].BackColor = System.Drawing.Color.White;
                    this.colorButtons[i].FlatAppearance.BorderColor = System.Drawing.Color.DarkBlue;
                    this.colorButtons[i].FlatAppearance.BorderSize = 2;
                    this.colorButtons[i].Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
                    this.colorButtons[i].Text = "0";
                    this.colorButtons[i].UseVisualStyleBackColor = false;

                    // Agregar tooltip con información
                    var tooltip = new System.Windows.Forms.ToolTip();
                    tooltip.SetToolTip(this.colorButtons[i], $"Posición {i + 1} - C/BCO-E");
                }
            }
        }

        #endregion
    }
}