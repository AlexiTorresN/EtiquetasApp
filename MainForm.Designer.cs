using System;
using System.Drawing;
using System.Windows.Forms;

namespace EtiquetasApp
{
    partial class MainForm
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        // Controles del formulario
        private MenuStrip mainMenu;
        private ToolStripMenuItem solicitudesMenu;
        private ToolStripMenuItem maestrosMenu;
        private ToolStripMenuItem operacionesMenu;
        private ToolStripMenuItem configuracionMenu;
        private ToolStripMenuItem salirMenu;

        private StatusStrip statusStrip;
        private ToolStripStatusLabel statusLabel;
        private ToolStripStatusLabel fechaLabel;

        // Elementos del menú Solicitudes
        private ToolStripMenuItem verRequerimientosItem;
        private ToolStripSeparator separador1;
        private ToolStripMenuItem solicitudesEtiquetasItem;
        private ToolStripMenuItem consultaSolicitudesItem;
        private ToolStripMenuItem reactivaSolicitudItem;
        private ToolStripMenuItem eliminaSolicitudItem;

        // Elementos del menú Maestros
        private ToolStripMenuItem codificaEtiquetasItem;

        // Elementos del menú Operaciones
        private ToolStripMenuItem etiquetasCBCOEItem;
        private ToolStripMenuItem etiquetasDualItem;
        private ToolStripMenuItem etiquetasLaqueadoItem;
        private ToolStripMenuItem etiquetasGardenStateItem;
        private ToolStripMenuItem etiquetasBicolorItem;
        private ToolStripMenuItem etiquetasMoldurasItem;
        private ToolStripMenuItem etiquetasEAN13Item;
        private ToolStripMenuItem etiquetasI2de5Item;

        // Elementos del menú Configuración
        private ToolStripMenuItem configurarImpresorasItem;
        private ToolStripMenuItem configurarConexionItem;

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
            mainMenu = new MenuStrip();
            solicitudesMenu = new ToolStripMenuItem();
            verRequerimientosItem = new ToolStripMenuItem();
            separador1 = new ToolStripSeparator();
            solicitudesEtiquetasItem = new ToolStripMenuItem();
            consultaSolicitudesItem = new ToolStripMenuItem();
            reactivaSolicitudItem = new ToolStripMenuItem();
            eliminaSolicitudItem = new ToolStripMenuItem();
            maestrosMenu = new ToolStripMenuItem();
            codificaEtiquetasItem = new ToolStripMenuItem();
            operacionesMenu = new ToolStripMenuItem();
            etiquetasCBCOEItem = new ToolStripMenuItem();
            etiquetasDualItem = new ToolStripMenuItem();
            etiquetasLaqueadoItem = new ToolStripMenuItem();
            etiquetasGardenStateItem = new ToolStripMenuItem();
            etiquetasBicolorItem = new ToolStripMenuItem();
            etiquetasMoldurasItem = new ToolStripMenuItem();
            etiquetasEAN13Item = new ToolStripMenuItem();
            etiquetasI2de5Item = new ToolStripMenuItem();
            configuracionMenu = new ToolStripMenuItem();
            configurarImpresorasItem = new ToolStripMenuItem();
            configurarConexionItem = new ToolStripMenuItem();
            salirMenu = new ToolStripMenuItem();
            statusStrip = new StatusStrip();
            statusLabel = new ToolStripStatusLabel();
            fechaLabel = new ToolStripStatusLabel();
            mainMenu.SuspendLayout();
            statusStrip.SuspendLayout();
            SuspendLayout();
            // 
            // mainMenu
            // 
            mainMenu.Items.AddRange(new ToolStripItem[] { solicitudesMenu, maestrosMenu, operacionesMenu, configuracionMenu, salirMenu });
            mainMenu.Location = new Point(0, 0);
            mainMenu.Name = "mainMenu";
            mainMenu.Padding = new Padding(7, 2, 0, 2);
            mainMenu.Size = new Size(1475, 24);
            mainMenu.TabIndex = 1;
            mainMenu.Text = "menuStrip1";
            // 
            // solicitudesMenu
            // 
            solicitudesMenu.DropDownItems.AddRange(new ToolStripItem[] { verRequerimientosItem, separador1, solicitudesEtiquetasItem, consultaSolicitudesItem, reactivaSolicitudItem, eliminaSolicitudItem });
            solicitudesMenu.Name = "solicitudesMenu";
            solicitudesMenu.Size = new Size(76, 20);
            solicitudesMenu.Text = "Solicitudes";
            // 
            // verRequerimientosItem
            // 
            verRequerimientosItem.Name = "verRequerimientosItem";
            verRequerimientosItem.Size = new Size(248, 22);
            verRequerimientosItem.Text = "Ver Requerimientos";
            verRequerimientosItem.Click += VerRequerimientos_Click;
            // 
            // separador1
            // 
            separador1.Name = "separador1";
            separador1.Size = new Size(245, 6);
            // 
            // solicitudesEtiquetasItem
            // 
            solicitudesEtiquetasItem.Name = "solicitudesEtiquetasItem";
            solicitudesEtiquetasItem.Size = new Size(248, 22);
            solicitudesEtiquetasItem.Text = "Solicitudes de etiquetas";
            solicitudesEtiquetasItem.Click += SolicitudesEtiquetas_Click;
            // 
            // consultaSolicitudesItem
            // 
            consultaSolicitudesItem.Name = "consultaSolicitudesItem";
            consultaSolicitudesItem.Size = new Size(248, 22);
            consultaSolicitudesItem.Text = "Consulta Solicitudes de etiquetas";
            consultaSolicitudesItem.Click += ConsultaSolicitudes_Click;
            // 
            // reactivaSolicitudItem
            // 
            reactivaSolicitudItem.Name = "reactivaSolicitudItem";
            reactivaSolicitudItem.Size = new Size(248, 22);
            reactivaSolicitudItem.Text = "Reactiva solicitud";
            reactivaSolicitudItem.Click += ReactivaSolicitud_Click;
            // 
            // eliminaSolicitudItem
            // 
            eliminaSolicitudItem.Name = "eliminaSolicitudItem";
            eliminaSolicitudItem.Size = new Size(248, 22);
            eliminaSolicitudItem.Text = "Elimina Solicitud";
            eliminaSolicitudItem.Click += EliminaSolicitud_Click;
            // 
            // maestrosMenu
            // 
            maestrosMenu.DropDownItems.AddRange(new ToolStripItem[] { codificaEtiquetasItem });
            maestrosMenu.Name = "maestrosMenu";
            maestrosMenu.Size = new Size(67, 20);
            maestrosMenu.Text = "Maestros";
            // 
            // codificaEtiquetasItem
            // 
            codificaEtiquetasItem.Name = "codificaEtiquetasItem";
            codificaEtiquetasItem.Size = new Size(180, 22);
            codificaEtiquetasItem.Text = "Codifica etiquetas";
            codificaEtiquetasItem.Click += CodificaEtiquetas_Click;
            // 
            // operacionesMenu
            // 
            operacionesMenu.DropDownItems.AddRange(new ToolStripItem[] { etiquetasCBCOEItem, etiquetasDualItem, etiquetasLaqueadoItem, etiquetasGardenStateItem, etiquetasBicolorItem, etiquetasMoldurasItem, etiquetasEAN13Item, etiquetasI2de5Item });
            operacionesMenu.Name = "operacionesMenu";
            operacionesMenu.Size = new Size(85, 20);
            operacionesMenu.Text = "Operaciones";
            // 
            // etiquetasCBCOEItem
            // 
            etiquetasCBCOEItem.Name = "etiquetasCBCOEItem";
            etiquetasCBCOEItem.Size = new Size(206, 22);
            etiquetasCBCOEItem.Text = "Etiquetas C/BCO-E";
            etiquetasCBCOEItem.Click += EtiquetasCBCOE_Click;
            // 
            // etiquetasDualItem
            // 
            etiquetasDualItem.Name = "etiquetasDualItem";
            etiquetasDualItem.Size = new Size(206, 22);
            etiquetasDualItem.Text = "Etiquetas DUAL";
            etiquetasDualItem.Click += EtiquetasDual_Click;
            // 
            // etiquetasLaqueadoItem
            // 
            etiquetasLaqueadoItem.Name = "etiquetasLaqueadoItem";
            etiquetasLaqueadoItem.Size = new Size(206, 22);
            etiquetasLaqueadoItem.Text = "Etiquetas Laqueado";
            etiquetasLaqueadoItem.Click += EtiquetasLaqueado_Click;
            // 
            // etiquetasGardenStateItem
            // 
            etiquetasGardenStateItem.Name = "etiquetasGardenStateItem";
            etiquetasGardenStateItem.Size = new Size(206, 22);
            etiquetasGardenStateItem.Text = "Etiquetas GARDEN STATE";
            etiquetasGardenStateItem.Click += EtiquetasGardenState_Click;
            // 
            // etiquetasBicolorItem
            // 
            etiquetasBicolorItem.Name = "etiquetasBicolorItem";
            etiquetasBicolorItem.Size = new Size(206, 22);
            etiquetasBicolorItem.Text = "Etiquetas BICOLOR2";
            etiquetasBicolorItem.Click += EtiquetasBicolor_Click;
            // 
            // etiquetasMoldurasItem
            // 
            etiquetasMoldurasItem.Name = "etiquetasMoldurasItem";
            etiquetasMoldurasItem.Size = new Size(206, 22);
            etiquetasMoldurasItem.Text = "Etiquetas para Molduras";
            etiquetasMoldurasItem.Click += EtiquetasMolduras_Click;
            // 
            // etiquetasEAN13Item
            // 
            etiquetasEAN13Item.Name = "etiquetasEAN13Item";
            etiquetasEAN13Item.Size = new Size(206, 22);
            etiquetasEAN13Item.Text = "Etiquetas EAN 13";
            etiquetasEAN13Item.Click += EtiquetasEAN13_Click;
            // 
            // etiquetasI2de5Item
            // 
            etiquetasI2de5Item.Name = "etiquetasI2de5Item";
            etiquetasI2de5Item.Size = new Size(206, 22);
            etiquetasI2de5Item.Text = "Etiquetas I 2 de 5";
            etiquetasI2de5Item.Click += EtiquetasI2de5_Click;
            // 
            // configuracionMenu
            // 
            configuracionMenu.DropDownItems.AddRange(new ToolStripItem[] { configurarImpresorasItem, configurarConexionItem });
            configuracionMenu.Name = "configuracionMenu";
            configuracionMenu.Size = new Size(95, 20);
            configuracionMenu.Text = "Configuración";
            // 
            // configurarImpresorasItem
            // 
            configurarImpresorasItem.Name = "configurarImpresorasItem";
            configurarImpresorasItem.Size = new Size(202, 22);
            configurarImpresorasItem.Text = "Configurar Impresoras";
            configurarImpresorasItem.Click += ConfigurarImpresoras_Click;
            // 
            // configurarConexionItem
            // 
            configurarConexionItem.Name = "configurarConexionItem";
            configurarConexionItem.Size = new Size(202, 22);
            configurarConexionItem.Text = "Configurar Conexión DB";
            configurarConexionItem.Click += ConfigurarConexion_Click;
            // 
            // salirMenu
            // 
            salirMenu.Name = "salirMenu";
            salirMenu.Size = new Size(41, 20);
            salirMenu.Text = "Salir";
            salirMenu.Click += Salir_Click;
            // 
            // statusStrip
            // 
            statusStrip.Items.AddRange(new ToolStripItem[] { statusLabel, fechaLabel });
            statusStrip.Location = new Point(0, 716);
            statusStrip.Name = "statusStrip";
            statusStrip.Padding = new Padding(1, 0, 16, 0);
            statusStrip.Size = new Size(1475, 24);
            statusStrip.TabIndex = 2;
            statusStrip.Text = "statusStrip1";
            // 
            // statusLabel
            // 
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(1344, 19);
            statusLabel.Spring = true;
            statusLabel.Text = "Listo";
            statusLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fechaLabel
            // 
            fechaLabel.BorderSides = ToolStripStatusLabelBorderSides.Left;
            fechaLabel.Name = "fechaLabel";
            fechaLabel.Size = new Size(114, 19);
            fechaLabel.Text = "08-09-2025 11:08:06";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.LightGray;
            ClientSize = new Size(1475, 740);
            Controls.Add(statusStrip);
            Controls.Add(mainMenu);
            IsMdiContainer = true;
            MainMenuStrip = mainMenu;
            Margin = new Padding(4, 3, 4, 3);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            WindowState = FormWindowState.Maximized;
            Load += MainForm_Load;
            mainMenu.ResumeLayout(false);
            mainMenu.PerformLayout();
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}