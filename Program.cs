using System;
using System.Windows.Forms;
using EtiquetasApp.Services;

namespace EtiquetasApp
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicaci�n.
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

                // Verificar conexi�n a base de datos
                if (!DatabaseService.TestConnection())
                {
                    MessageBox.Show("Error: No se pudo conectar a la base de datos. Verifique la configuraci�n.",
                                  "Error de Conexi�n", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Ejecutar aplicaci�n principal
                Application.Run(new MainForm());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al inicializar la aplicaci�n: {ex.Message}",
                              "Error Cr�tico", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}