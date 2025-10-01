using EtiquetasApp.Forms;
using EtiquetasApp.Services;
using EtiquetasApp.Setup;
using System;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;


namespace EtiquetasApp
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            try
            {
                InitialSetup.Initialize();
                Console.WriteLine("Configuración inicial completada");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error en configuración inicial: {ex.Message}");
            }
            InitializeComponent();
            InitializeCustomComponents();
        }

        private void InitializeCustomComponents()
        {
            // Configuración adicional después del diseñador
            Text = "Sistema de Etiquetas [ V 6.0 | 05.01.2025] - [Impresión de Etiquetas]";

            // Actualizar fecha en tiempo real
            var timer = new Timer { Interval = 1000 };
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            fechaLabel.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }

        // Event Handlers para el menú
        private void VerRequerimientos_Click(object sender, EventArgs e)
        {
            ShowMdiForm<Forms.RequerimientosForm>();
        }

        private void SolicitudesEtiquetas_Click(object sender, EventArgs e)
        {
            ShowMdiForm<Forms.SolicitudesEtiquetasForm>();
        }

        private void ConsultaSolicitudes_Click(object sender, EventArgs e)
        {
            ShowMdiForm<Forms.ConsultaSolicitudesForm>();
        }

        //private void ReactivaSolicitud_Click(object sender, EventArgs e)
        //{
        //    ShowMdiForm<Forms.ReactivaSolicitudForm>();
        //}

        //private void EliminaSolicitud_Click(object sender, EventArgs e)
        //{
        //    ShowMdiForm<Forms.EliminaSolicitudForm>();
        //}

        private void CodificaEtiquetas_Click(object sender, EventArgs e)
        {
            ShowMdiForm<Forms.CodificaEtiquetasForm>();
        }

        private void EtiquetasCBCOE_Click(object sender, EventArgs e)
        {
            ShowMdiForm<Forms.EtiquetasCBCOEForm>();
        }

        private void EtiquetasDual_Click(object sender, EventArgs e)
        {
            ShowMdiForm<Forms.EtiquetasDualForm>();
        }

        //private void EtiquetasLaqueado_Click(object sender, EventArgs e)
        //{
        //    ShowMdiForm<Forms.EtiquetasLaqueadoForm>();
        //}

        //private void EtiquetasGardenState_Click(object sender, EventArgs e)
        //{
        //    ShowMdiForm<Forms.EtiquetasGardenStateForm>();
        //}

        //private void EtiquetasBicolor_Click(object sender, EventArgs e)
        //{
        //    ShowMdiForm<Forms.EtiquetasBicolorForm>();
        //}

        //private void EtiquetasMolduras_Click(object sender, EventArgs e)
        //{
        //    ShowMdiForm<Forms.EtiquetasMoldurasForm>();
        //}

        //private void EtiquetasEAN13_Click(object sender, EventArgs e)
        //{
        //    ShowMdiForm<Forms.EtiquetasEAN13Form>();
        //}

        //private void EtiquetasI2de5_Click(object sender, EventArgs e)
        //{
        //    ShowMdiForm<Forms.EtiquetasI2de5Form>();
        //}

        //private void ConfigurarImpresoras_Click(object sender, EventArgs e)
        //{
        //    ShowMdiForm<Forms.ConfiguracionImpresorasForm>();
        //}

        //private void ConfigurarConexion_Click(object sender, EventArgs e)
        //{
        //    ShowMdiForm<Forms.ConfiguracionConexionForm>();
        //}

        private void Salir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro que desea salir de la aplicación?",
                               "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void ShowMdiForm<T>() where T : Form, new()
        {
            // Verificar si el formulario ya está abierto
            foreach (Form form in MdiChildren)
            {
                if (form is T)
                {
                    form.Activate();
                    return;
                }
            }

            // Crear nueva instancia del formulario
            try
            {
                var newForm = new T
                {
                    MdiParent = this,
                    WindowState = FormWindowState.Maximized
                };
                newForm.Show();
                statusLabel.Text = $"Abriendo {typeof(T).Name}...";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir formulario: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                statusLabel.Text = "Error al abrir formulario";
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                if (MessageBox.Show("¿Está seguro que desea cerrar la aplicación?",
                                   "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
            base.OnFormClosing(e);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            statusLabel.Text = "Sistema de Etiquetas iniciado correctamente";

            // Verificar configuración al cargar
            if (!ConfigurationService.ValidateConfiguration())
            {
                statusLabel.Text = "Advertencia: Revisar configuración del sistema";
            }
        }
    }
}