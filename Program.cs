using System;
using System.Windows.Forms;
using EtiquetasApp.Services;

namespace EtiquetasApp
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                // Inicializar servicios
                ConfigurationService.Initialize();

                // Verificar conexión a base de datos
                if (!DatabaseService.TestConnection())
                {
                    MessageBox.Show("Error: No se pudo conectar a la base de datos. Verifique la configuración.",
                                  "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Ejecutar aplicación principal
                Application.Run(new MainForm());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al inicializar la aplicación: {ex.Message}",
                              "Error Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}