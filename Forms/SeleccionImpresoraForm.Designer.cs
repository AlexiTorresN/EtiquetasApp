using System;
using System.Drawing;
using System.Windows.Forms;

namespace EtiquetasApp.Forms
{
    partial class SeleccionImpresoraForm
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        // Controles del formulario
        private Label lblInstrucciones;
        private ListBox impresorasListBox;
        private Label lblInfoImpresora;
        private CheckBox soloZebraCheckBox;
        private CheckBox establecerPorDefectoCheckBox;
        private Button btnSeleccionar;
        private Button btnCancelar;
        private Button btnActualizar;
        private Button btnPrueba;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel statusLabel;
        private GroupBox infoGroupBox;

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

            this.lblInstrucciones = new Label();
            this.impresorasListBox = new ListBox();
            this.infoGroupBox = new GroupBox();
            this.lblInfoImpresora = new Label();
            this.soloZebraCheckBox = new CheckBox();
            this.establecerPorDefectoCheckBox = new CheckBox();
            this.btnSeleccionar = new Button();
            this.btnCancelar = new Button();
            this.btnActualizar = new Button();
            this.btnPrueba = new Button();
            this.statusStrip = new StatusStrip();
            this.statusLabel = new ToolStripStatusLabel();

            this.infoGroupBox.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();

            // 
            // lblInstrucciones
            // 
            this.lblInstrucciones.AutoSize = true;
            this.lblInstrucciones.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point);
            this.lblInstrucciones.Location = new Point(15, 15);
            this.lblInstrucciones.Name = "lblInstrucciones";
            this.lblInstrucciones.Size = new Size(200, 15);
            this.lblInstrucciones.TabIndex = 0;
            this.lblInstrucciones.Text = "Seleccione una impresora de la lista:";

            // 
            // impresorasListBox
            // 
            this.impresorasListBox.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point);
            this.impresorasListBox.FormattingEnabled = true;
            this.impresorasListBox.ItemHeight = 15;
            this.impresorasListBox.Location = new Point(15, 40);
            this.impresorasListBox.Name = "impresorasListBox";
            this.impresorasListBox.Size = new Size(400, 154);
            this.impresorasListBox.TabIndex = 1;

            // 
            // infoGroupBox
            // 
            this.infoGroupBox.Controls.Add(this.lblInfoImpresora);
            this.infoGroupBox.Location = new Point(430, 40);
            this.infoGroupBox.Name = "infoGroupBox";
            this.infoGroupBox.Size = new Size(250, 154);
            this.infoGroupBox.TabIndex = 2;
            this.infoGroupBox.TabStop = false;
            this.infoGroupBox.Text = "Información de la Impresora";

            // 
            // lblInfoImpresora
            // 
            this.lblInfoImpresora.Location = new Point(10, 20);
            this.lblInfoImpresora.Name = "lblInfoImpresora";
            this.lblInfoImpresora.Size = new Size(230, 120);
            this.lblInfoImpresora.TabIndex = 0;
            this.lblInfoImpresora.Text = "Seleccione una impresora para ver su información";
            this.lblInfoImpresora.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);

            // 
            // soloZebraCheckBox
            // 
            this.soloZebraCheckBox.AutoSize = true;
            this.soloZebraCheckBox.Location = new Point(15, 210);
            this.soloZebraCheckBox.Name = "soloZebraCheckBox";
            this.soloZebraCheckBox.Size = new Size(150, 17);
            this.soloZebraCheckBox.TabIndex = 3;
            this.soloZebraCheckBox.Text = "Mostrar solo impresoras Zebra";
            this.soloZebraCheckBox.UseVisualStyleBackColor = true;

            // 
            // establecerPorDefectoCheckBox
            // 
            this.establecerPorDefectoCheckBox.AutoSize = true;
            this.establecerPorDefectoCheckBox.Location = new Point(15, 235);
            this.establecerPorDefectoCheckBox.Name = "establecerPorDefectoCheckBox";
            this.establecerPorDefectoCheckBox.Size = new Size(180, 17);
            this.establecerPorDefectoCheckBox.TabIndex = 4;
            this.establecerPorDefectoCheckBox.Text = "Establecer como predeterminada";
            this.establecerPorDefectoCheckBox.UseVisualStyleBackColor = true;

            // 
            // btnActualizar
            // 
            this.btnActualizar.Image = SystemIcons.Asterisk.ToBitmap(); // Icono simple
            this.btnActualizar.Location = new Point(430, 210);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new Size(90, 30);
            this.btnActualizar.TabIndex = 5;
            this.btnActualizar.Text = "Actualizar";
            this.btnActualizar.TextImageRelation = TextImageRelation.ImageBeforeText;
            this.btnActualizar.UseVisualStyleBackColor = true;

            // 
            // btnPrueba
            // 
            this.btnPrueba.Enabled = false;
            this.btnPrueba.Location = new Point(530, 210);
            this.btnPrueba.Name = "btnPrueba";
            this.btnPrueba.Size = new Size(90, 30);
            this.btnPrueba.TabIndex = 6;
            this.btnPrueba.Text = "Prueba";
            this.btnPrueba.UseVisualStyleBackColor = true;

            // 
            // btnCancelar
            // 
            this.btnCancelar.DialogResult = DialogResult.Cancel;
            this.btnCancelar.Location = new Point(530, 250);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new Size(90, 35);
            this.btnCancelar.TabIndex = 8;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;

            // 
            // btnSeleccionar
            // 
            this.btnSeleccionar.BackColor = Color.LightGreen;
            this.btnSeleccionar.Enabled = false;
            this.btnSeleccionar.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point);
            this.btnSeleccionar.Location = new Point(430, 250);
            this.btnSeleccionar.Name = "btnSeleccionar";
            this.btnSeleccionar.Size = new Size(90, 35);
            this.btnSeleccionar.TabIndex = 7;
            this.btnSeleccionar.Text = "Seleccionar";
            this.btnSeleccionar.UseVisualStyleBackColor = false;

            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new ToolStripItem[] {
            this.statusLabel});
            this.statusStrip.Location = new Point(0, 306);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new Size(694, 22);
            this.statusStrip.TabIndex = 9;
            this.statusStrip.Text = "statusStrip1";

            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new Size(85, 17);
            this.statusLabel.Text = "Cargando...";

            // 
            // SeleccionImpresoraForm
            // 
            this.AcceptButton = this.btnSeleccionar;
            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.CancelButton = this.btnCancelar;
            this.ClientSize = new Size(694, 328);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.btnSeleccionar);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnPrueba);
            this.Controls.Add(this.btnActualizar);
            this.Controls.Add(this.establecerPorDefectoCheckBox);
            this.Controls.Add(this.soloZebraCheckBox);
            this.Controls.Add(this.infoGroupBox);
            this.Controls.Add(this.impresorasListBox);
            this.Controls.Add(this.lblInstrucciones);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SeleccionImpresoraForm";
            this.ShowInTaskbar = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Seleccionar Impresora";

            this.infoGroupBox.ResumeLayout(false);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}