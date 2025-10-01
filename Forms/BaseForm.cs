using System;
using System.Windows.Forms;

namespace EtiquetasApp.Forms
{
    /// <summary>
    /// Clase base para todos los formularios del sistema
    /// </summary>
    public class BaseForm : Form
    {
        protected StatusStrip statusStrip;
        protected ToolStripStatusLabel statusLabel;

#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de agregar el modificador "required" o declararlo como un valor que acepta valores NULL.
        public BaseForm()
#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de agregar el modificador "required" o declararlo como un valor que acepta valores NULL.
        {
            InitializeBaseComponents();
        }

        private void InitializeBaseComponents()
        {
            // StatusStrip común
            statusStrip = new StatusStrip();
            statusLabel = new ToolStripStatusLabel
            {
                Name = "statusLabel",
                Spring = true,
                Text = "Listo",
                TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            };

            statusStrip.Items.Add(statusLabel);
            Controls.Add(statusStrip);

            // Configuración básica
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.Sizable;
        }

        protected void ShowStatus(string message)
        {
            if (statusLabel != null)
                statusLabel.Text = message;
        }

        protected void ShowError(string message, string title = "Error")
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            ShowStatus($"Error: {message}");
        }

        protected void ShowSuccess(string message, string title = "Éxito")
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
            ShowStatus(message);
        }

        protected bool ConfirmAction(string message, string title = "Confirmación")
        {
            return MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            ShowStatus($"{Text} cargado");
        }
    }
}