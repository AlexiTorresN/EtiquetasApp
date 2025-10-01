using EtiquetasApp.Models;
using ConfigMgr = EtiquetasApp.Configurations.ConfigurationManager;

namespace EtiquetasApp.Services
{
    /// <summary>
    /// Servicio de configuración simplificado que delega a ConfigurationManager
    /// </summary>
    public static class ConfigurationService
    {
        public static void Initialize()
        {
            try
            {
                ConfigMgr.Initialize();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error al inicializar configuración: {ex.Message}", ex);
            }
        }

        // Acceso a configuraciones
        public static PrinterConfiguration PrinterConfig => ConfigMgr.PrinterConfig;
        public static TemplatesConfiguration TemplatesConfig => ConfigMgr.TemplatesConfig;
        public static DatabaseConfiguration DatabaseConfig => ConfigMgr.DatabaseConfig;
        public static SecurityConfiguration SecurityConfig => ConfigMgr.SecurityConfig;

        // Conexiones de Base de Datos
        public static string GetConnectionString(string name) => ConfigMgr.GetConnectionString(name);
        public static string DatabaseConnection1 => GetConnectionString("DatabaseConnection1");
        public static string DatabaseConnection2 => GetConnectionString("DatabaseConnection2");

        // Configuración de Impresión
        public static string DefaultPrinter
        {
            get => ConfigMgr.GetAppSetting("DefaultPrinter", "");
            set
            {
                PrinterConfig.PrinterSettings.DefaultPrinter = value;
                ConfigMgr.SavePrinterConfiguration();
            }
        }

        public static int DefaultVelocidad
        {
            get => ConfigMgr.GetIntAppSetting("DefaultVelocidad", 4);
            set
            {
                PrinterConfig.PrintParameters.DefaultSpeed = value;
                ConfigMgr.SavePrinterConfiguration();
            }
        }

        public static int DefaultTemperatura
        {
            get => ConfigMgr.GetIntAppSetting("DefaultTemperatura", 6);
            set
            {
                PrinterConfig.PrintParameters.DefaultDensity = value;
                ConfigMgr.SavePrinterConfiguration();
            }
        }

        // Configuración de Templates
        public static string TemplatesPath
        {
            get
            {
                var path = ConfigMgr.GetAppSetting("TemplatesPath",
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates"));
                EnsureDirectoryExists(path);
                return path;
            }
        }

        // Configuración de Recursos
        public static string ResourcesPath
        {
            get
            {
                var path = ConfigMgr.GetAppSetting("ResourcesPath",
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources"));
                EnsureDirectoryExists(path);
                return path;
            }
        }

        public static string ImagesPath
        {
            get
            {
                var path = Path.Combine(ResourcesPath, "Images");
                EnsureDirectoryExists(path);
                return path;
            }
        }

        // Configuración de posiciones para etiquetas
        public static string GetEtiquetaPosition(string tipoEtiqueta, string posicion)
        {
            return ConfigMgr.GetAppSetting($"{tipoEtiqueta}_{posicion}", "0");
        }

        public static void SetEtiquetaPosition(string tipoEtiqueta, string posicion, string valor)
        {
            // Nota: Para persistir esto necesitarías actualizar app.config directamente
            // Por ahora solo lo mantenemos en memoria
            Console.WriteLine($"Posición {tipoEtiqueta}_{posicion} = {valor}");
        }

        // Métodos de validación
        public static bool ValidateConfiguration()
        {
            try
            {
                if (string.IsNullOrEmpty(GetConnectionString("DatabaseConnection1")))
                    return false;

                if (string.IsNullOrEmpty(GetConnectionString("DatabaseConnection2")))
                    return false;

                EnsureDirectoryExists(TemplatesPath);
                EnsureDirectoryExists(ResourcesPath);

                return true;
            }
            catch
            {
                return false;
            }
        }

        // Crear configuración por defecto
        public static void CreateDefaultConfiguration()
        {
            try
            {
                // Las configuraciones por defecto ya se crean en ConfigurationManager
                ConfigMgr.Initialize();

                // Posiciones por defecto para C/BCO-E
                SetEtiquetaPosition("CBCOE", "Posicion1", "4");
                SetEtiquetaPosition("CBCOE", "Posicion2", "148");
                SetEtiquetaPosition("CBCOE", "Posicion3", "292");
                SetEtiquetaPosition("CBCOE", "Posicion4", "436");
                SetEtiquetaPosition("CBCOE", "Posicion5", "580");

                // Posiciones por defecto para DUAL
                SetEtiquetaPosition("DUAL", "Posicion1", "17");
                SetEtiquetaPosition("DUAL", "Posicion2", "185");
                SetEtiquetaPosition("DUAL", "Posicion3", "354");
                SetEtiquetaPosition("DUAL", "Posicion4", "522");
                SetEtiquetaPosition("DUAL", "Posicion5", "689");

                // Posiciones por defecto para EAN13
                SetEtiquetaPosition("EAN13", "Posicion1", "835");
                SetEtiquetaPosition("EAN13", "Posicion2", "695");
                SetEtiquetaPosition("EAN13", "Posicion3", "550");
                SetEtiquetaPosition("EAN13", "Posicion4", "406");
                SetEtiquetaPosition("EAN13", "Posicion5", "262");
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error al crear configuración por defecto: {ex.Message}", ex);
            }
        }

        private static void EnsureDirectoryExists(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }
    }
}