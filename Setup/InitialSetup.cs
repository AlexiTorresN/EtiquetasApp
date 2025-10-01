using System;
using System.IO;
using System.Windows.Forms;
using EtiquetasApp.Configurations;

namespace EtiquetasApp.Setup
{
    /// <summary>
    /// Clase para la configuración inicial del sistema
    /// </summary>
    public static class InitialSetup
    {
        /// <summary>
        /// Inicializa la aplicación creando directorios y archivos necesarios
        /// </summary>
        public static void Initialize()
        {
            try
            {
                CreateDirectories();
                CreateDefaultTemplates();
                InitializeConfigurations();
                CreateLogDirectories();

                LogMessage("Configuración inicial completada exitosamente.");
            }
            catch (Exception ex)
            {
                LogMessage($"Error en configuración inicial: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Crea los directorios necesarios para la aplicación
        /// </summary>
        private static void CreateDirectories()
        {
            var directories = new[]
            {
                "Configurations",
                "Templates",
                "Templates\\ZPL",
                "Resources",
                "Resources\\Images",
                "Logs",
                "Logs\\Audit",
                "Backups",
                "Backups\\Templates",
                "Scripts",
                "Utils"
            };

            foreach (var directory in directories)
            {
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                    LogMessage($"Directorio creado: {directory}");
                }
            }
        }

        /// <summary>
        /// Crea plantillas ZPL por defecto
        /// </summary>
        private static void CreateDefaultTemplates()
        {
            CreateCBCOETemplate();
            CreateDualTemplate();
            CreateMoldurasTemplate();
            CreateEAN13Template();
            CreateI2DE5Template();
        }

        /// <summary>
        /// Inicializa las configuraciones JSON
        /// </summary>
        private static void InitializeConfigurations()
        {
            try
            {
                // Esto activará la creación automática de archivos por defecto
                var printerConfig = ConfigurationManager.PrinterConfig;
                var templatesConfig = ConfigurationManager.TemplatesConfig;
                var databaseConfig = ConfigurationManager.DatabaseConfig;
                var securityConfig = ConfigurationManager.SecurityConfig;

                LogMessage("Configuraciones JSON inicializadas correctamente.");
            }
            catch (Exception ex)
            {
                LogMessage($"Error inicializando configuraciones: {ex.Message}");
            }
        }

        /// <summary>
        /// Crea directorios de logs con archivos iniciales
        /// </summary>
        private static void CreateLogDirectories()
        {
            var logPath = "Logs";
            var auditPath = "Logs\\Audit";

            // Crear archivo de log principal si no existe
            var mainLogFile = Path.Combine(logPath, "EtiquetasApp.log");
            if (!File.Exists(mainLogFile))
            {
                File.WriteAllText(mainLogFile, $"# Log de EtiquetasApp - Iniciado: {DateTime.Now:dd/MM/yyyy HH:mm:ss}\n");
            }

            // Crear archivo de auditoría si no existe
            var auditLogFile = Path.Combine(auditPath, $"Audit_{DateTime.Now:yyyyMMdd}.log");
            if (!File.Exists(auditLogFile))
            {
                File.WriteAllText(auditLogFile, $"# Log de Auditoría - Iniciado: {DateTime.Now:dd/MM/yyyy HH:mm:ss}\n");
            }
        }

        #region Plantillas ZPL

        private static void CreateCBCOETemplate()
        {
            var template = @"^XA
^PR{VELOCIDAD}
^LH0,0^FS
^LL719
^MD0
^MNY
^LH0,0^FS

; Descripción en cada posición
^FO{POS1_X},280^A0R,28,23^CI13^FR^FD{DESCRIPCION}^FS
^FO{POS2_X},280^A0R,28,23^CI13^FR^FD{DESCRIPCION}^FS
^FO{POS3_X},280^A0R,28,23^CI13^FR^FD{DESCRIPCION}^FS
^FO{POS4_X},280^A0R,28,23^CI13^FR^FD{DESCRIPCION}^FS
^FO{POS5_X},280^A0R,28,23^CI13^FR^FD{DESCRIPCION}^FS

; Códigos de barras
^BY2,3.0^FO{POS1_X},80^BER,37,Y,N^FR^FD{UPC}^FS
^BY2,3.0^FO{POS2_X},80^BER,37,Y,N^FR^FD{UPC}^FS
^BY2,3.0^FO{POS3_X},80^BER,37,Y,N^FR^FD{UPC}^FS
^BY2,3.0^FO{POS4_X},80^BER,37,Y,N^FR^FD{UPC}^FS
^BY2,3.0^FO{POS5_X},80^BER,37,Y,N^FR^FD{UPC}^FS

^XZ";

            WriteTemplate("cbcoe_template.zpl", template);
        }

        private static void CreateDualTemplate()
        {
            var template = @"^XA
^PR{VELOCIDAD}
^LH0,0^FS
^LL719
^MD0
^MNY

; Etiquetas duales con configuración específica
^FO{POS1_X},280^A0R,28,23^CI13^FR^FD{DESCRIPCION}^FS
^FO{POS2_X},280^A0R,28,23^CI13^FR^FD{DESCRIPCION}^FS
^FO{POS3_X},280^A0R,28,23^CI13^FR^FD{DESCRIPCION}^FS
^FO{POS4_X},280^A0R,28,23^CI13^FR^FD{DESCRIPCION}^FS
^FO{POS5_X},280^A0R,28,23^CI13^FR^FD{DESCRIPCION}^FS

^BY2,3.0^FO{POS1_X},80^BER,37,Y,N^FR^FD{UPC}^FS
^BY2,3.0^FO{POS2_X},80^BER,37,Y,N^FR^FD{UPC}^FS
^BY2,3.0^FO{POS3_X},80^BER,37,Y,N^FR^FD{UPC}^FS
^BY2,3.0^FO{POS4_X},80^BER,37,Y,N^FR^FD{UPC}^FS
^BY2,3.0^FO{POS5_X},80^BER,37,Y,N^FR^FD{UPC}^FS

^XZ";

            WriteTemplate("dual_template.zpl", template);
        }

        private static void CreateMoldurasTemplate()
        {
            var template = @"^XA
^PR{VELOCIDAD}
^LH0,0^FS
^LL719
^MD0
^MNY

; Plantilla para molduras con color
^FO{POS1_X},280^A0R,28,23^CI13^FR^FD{DESCRIPCION}^FS
^FO{POS2_X},280^A0R,28,23^CI13^FR^FD{DESCRIPCION}^FS
^FO{POS3_X},280^A0R,28,23^CI13^FR^FD{DESCRIPCION}^FS

; Color de moldura
^FO{POS1_X},320^A0R,20,18^CI13^FR^FD{COLOR}^FS
^FO{POS2_X},320^A0R,20,18^CI13^FR^FD{COLOR}^FS
^FO{POS3_X},320^A0R,20,18^CI13^FR^FD{COLOR}^FS

^BY2,3.0^FO{POS1_X},80^BER,37,Y,N^FR^FD{UPC}^FS
^BY2,3.0^FO{POS2_X},80^BER,37,Y,N^FR^FD{UPC}^FS
^BY2,3.0^FO{POS3_X},80^BER,37,Y,N^FR^FD{UPC}^FS

^XZ";

            WriteTemplate("molduras_template.zpl", template);
        }

        private static void CreateEAN13Template()
        {
            var template = @"^XA
^PR{VELOCIDAD}
^LH0,0^FS
^LL719
^MD0
^MNY

; Plantilla EAN-13
^FO50,280^A0R,28,23^CI13^FR^FD{DESCRIPCION}^FS
^BY2,3.0^FO50,80^BER,37,Y,N^FR^FD{UPC}^FS

^XZ";

            WriteTemplate("ean13_template.zpl", template);
        }

        private static void CreateI2DE5Template()
        {
            var template = @"^XA
^PR{VELOCIDAD}
^LH0,0^FS
^LL719
^MD0
^MNY

; Plantilla Interleaved 2 of 5
^FO50,280^A0R,28,23^CI13^FR^FD{DESCRIPCION}^FS
^BY2,3.0^FO50,80^BI2,37,Y,N^FR^FD{UPC}^FS

^XZ";

            WriteTemplate("i2de5_template.zpl", template);
        }

        private static void WriteTemplate(string fileName, string content)
        {
            var filePath = Path.Combine("Templates", "ZPL", fileName);

            try
            {
                File.WriteAllText(filePath, content);
                LogMessage($"Plantilla creada: {fileName}");
            }
            catch (Exception ex)
            {
                LogMessage($"Error creando plantilla {fileName}: {ex.Message}");
            }
        }

        #endregion

        /// <summary>
        /// Verifica si la configuración inicial está completa
        /// </summary>
        public static bool IsSetupComplete()
        {
            var requiredFiles = new[]
            {
                "Configurations\\printer-config.json",
                "Configurations\\templates-config.json",
                "Configurations\\database-config.json",
                "Configurations\\security-config.json"
            };

            var requiredDirectories = new[]
            {
                "Templates\\ZPL",
                "Resources\\Images",
                "Logs"
            };

            // Verificar archivos
            foreach (var file in requiredFiles)
            {
                if (!File.Exists(file))
                {
                    LogMessage($"Archivo requerido no encontrado: {file}");
                    return false;
                }
            }

            // Verificar directorios
            foreach (var directory in requiredDirectories)
            {
                if (!Directory.Exists(directory))
                {
                    LogMessage($"Directorio requerido no encontrado: {directory}");
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Muestra información del estado de la configuración
        /// </summary>
        public static void ShowSetupStatus()
        {
            string message = "=== Estado de Configuración EtiquetasApp ===\n";
            message += $"Configuración completa: {(IsSetupComplete() ? "SÍ" : "NO")}\n";
            message += $"Fecha verificación: {DateTime.Now:dd/MM/yyyy HH:mm:ss}\n";
            message += "============================================";

            MessageBox.Show(message, "Estado de Configuración", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Método de logging para Windows Forms (sin Console)
        /// </summary>
        private static void LogMessage(string message)
        {
            try
            {
                // Para debugging en Visual Studio
                System.Diagnostics.Debug.WriteLine($"[SETUP] {DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}");

                // Opcional: escribir a archivo de log
                var logFile = Path.Combine("Logs", "Setup.log");
                if (Directory.Exists("Logs"))
                {
                    File.AppendAllText(logFile, $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}\n");
                }
            }
            catch
            {
                // Evitar errores de logging
            }
        }
    }
}