using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Xml;

namespace EtiquetasApp.Configurations
{
    /// <summary>
    /// Gestor centralizado de configuraciones para EtiquetasApp
    /// </summary>
    public static class ConfigurationManager
    {
        #region Variables Privadas
        private static PrinterConfiguration _printerConfig;
        private static TemplatesConfiguration _templatesConfig;
        private static DatabaseConfiguration _databaseConfig;
        private static SecurityConfiguration _securityConfig;
        private static bool _isInitialized = false;
        private static readonly object _lockObject = new object();
        #endregion

        #region Propiedades Públicas
        public static PrinterConfiguration PrinterConfig
        {
            get
            {
                if (_printerConfig == null)
                    LoadPrinterConfiguration();
                return _printerConfig;
            }
        }

        public static TemplatesConfiguration TemplatesConfig
        {
            get
            {
                if (_templatesConfig == null)
                    LoadTemplatesConfiguration();
                return _templatesConfig;
            }
        }

        public static DatabaseConfiguration DatabaseConfig
        {
            get
            {
                if (_databaseConfig == null)
                    LoadDatabaseConfiguration();
                return _databaseConfig;
            }
        }

        public static SecurityConfiguration SecurityConfig
        {
            get
            {
                if (_securityConfig == null)
                    LoadSecurityConfiguration();
                return _securityConfig;
            }
        }
        #endregion

        #region Métodos de Inicialización
        /// <summary>
        /// Inicializa todas las configuraciones
        /// </summary>
        public static void Initialize()
        {
            lock (_lockObject)
            {
                if (_isInitialized)
                    return;

                try
                {
                    // Verificar que existe el directorio de configuraciones
                    string configPath = GetConfigPath();
                    if (!Directory.Exists(configPath))
                    {
                        Directory.CreateDirectory(configPath);
                    }

                    // Cargar todas las configuraciones
                    LoadAllConfigurations();

                    // Validar configuraciones críticas
                    ValidateConfigurations();

                    _isInitialized = true;
                    LogEvent($"Configuraciones inicializadas correctamente desde: {configPath}");
                }
                catch (Exception ex)
                {
                    LogError($"Error al inicializar configuraciones: {ex.Message}");
                    throw new ConfigurationErrorsException("Error al cargar configuraciones del sistema", ex);
                }
            }
        }

        private static void LoadAllConfigurations()
        {
            LoadPrinterConfiguration();
            LoadTemplatesConfiguration();
            LoadDatabaseConfiguration();
            LoadSecurityConfiguration();
        }
        #endregion

        #region Métodos de Carga de Configuraciones
        private static void LoadPrinterConfiguration()
        {
            try
            {
                string filePath = Path.Combine(GetConfigPath(), "printer-config.json");
                if (File.Exists(filePath))
                {
                    string json = File.ReadAllText(filePath);
                    _printerConfig = JsonConvert.DeserializeObject<PrinterConfiguration>(json);
                }
                else
                {
                    _printerConfig = CreateDefaultPrinterConfiguration();
                    SavePrinterConfiguration();
                }
            }
            catch (Exception ex)
            {
                LogError($"Error cargando configuración de impresoras: {ex.Message}");
                _printerConfig = CreateDefaultPrinterConfiguration();
            }
        }

        private static void LoadTemplatesConfiguration()
        {
            try
            {
                string filePath = Path.Combine(GetConfigPath(), "templates-config.json");
                if (File.Exists(filePath))
                {
                    string json = File.ReadAllText(filePath);
                    _templatesConfig = JsonConvert.DeserializeObject<TemplatesConfiguration>(json);
                }
                else
                {
                    _templatesConfig = CreateDefaultTemplatesConfiguration();
                    SaveTemplatesConfiguration();
                }
            }
            catch (Exception ex)
            {
                LogError($"Error cargando configuración de plantillas: {ex.Message}");
                _templatesConfig = CreateDefaultTemplatesConfiguration();
            }
        }

        private static void LoadDatabaseConfiguration()
        {
            try
            {
                string filePath = Path.Combine(GetConfigPath(), "database-config.json");
                if (File.Exists(filePath))
                {
                    string json = File.ReadAllText(filePath);
                    _databaseConfig = JsonConvert.DeserializeObject<DatabaseConfiguration>(json);
                }
                else
                {
                    _databaseConfig = CreateDefaultDatabaseConfiguration();
                    SaveDatabaseConfiguration();
                }
            }
            catch (Exception ex)
            {
                LogError($"Error cargando configuración de base de datos: {ex.Message}");
                _databaseConfig = CreateDefaultDatabaseConfiguration();
            }
        }

        private static void LoadSecurityConfiguration()
        {
            try
            {
                string filePath = Path.Combine(GetConfigPath(), "security-config.json");
                if (File.Exists(filePath))
                {
                    string json = File.ReadAllText(filePath);
                    _securityConfig = JsonConvert.DeserializeObject<SecurityConfiguration>(json);
                }
                else
                {
                    _securityConfig = CreateDefaultSecurityConfiguration();
                    SaveSecurityConfiguration();
                }
            }
            catch (Exception ex)
            {
                LogError($"Error cargando configuración de seguridad: {ex.Message}");
                _securityConfig = CreateDefaultSecurityConfiguration();
            }
        }
        #endregion

        #region Métodos de Guardado
        public static void SavePrinterConfiguration()
        {
            try
            {
                string filePath = Path.Combine(GetConfigPath(), "printer-config.json");
                string json = JsonConvert.SerializeObject(_printerConfig, Formatting.Indented);
                File.WriteAllText(filePath, json);
                LogEvent("Configuración de impresoras guardada");
            }
            catch (Exception ex)
            {
                LogError($"Error guardando configuración de impresoras: {ex.Message}");
            }
        }

        public static void SaveTemplatesConfiguration()
        {
            try
            {
                string filePath = Path.Combine(GetConfigPath(), "templates-config.json");
                string json = JsonConvert.SerializeObject(_templatesConfig, Formatting.Indented);
                File.WriteAllText(filePath, json);
                LogEvent("Configuración de plantillas guardada");
            }
            catch (Exception ex)
            {
                LogError($"Error guardando configuración de plantillas: {ex.Message}");
            }
        }

        public static void SaveDatabaseConfiguration()
        {
            try
            {
                string filePath = Path.Combine(GetConfigPath(), "database-config.json");
                string json = JsonConvert.SerializeObject(_databaseConfig, Formatting.Indented);
                File.WriteAllText(filePath, json);
                LogEvent("Configuración de base de datos guardada");
            }
            catch (Exception ex)
            {
                LogError($"Error guardando configuración de base de datos: {ex.Message}");
            }
        }

        public static void SaveSecurityConfiguration()
        {
            try
            {
                string filePath = Path.Combine(GetConfigPath(), "security-config.json");
                string json = JsonConvert.SerializeObject(_securityConfig, Formatting.Indented);
                File.WriteAllText(filePath, json);
                LogEvent("Configuración de seguridad guardada");
            }
            catch (Exception ex)
            {
                LogError($"Error guardando configuración de seguridad: {ex.Message}");
            }
        }
        #endregion

        #region Métodos Utilitarios App.config
        /// <summary>
        /// Obtiene un valor de App.config
        /// </summary>
        public static string GetAppSetting(string key, string defaultValue = "")
        {
            try
            {
                return System.Configuration.ConfigurationManager.AppSettings[key] ?? defaultValue;
            }
            catch
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Obtiene una cadena de conexión
        /// </summary>
        public static string GetConnectionString(string name)
        {
            try
            {
                var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings[name];
                return connectionString?.ConnectionString ?? string.Empty;
            }
            catch (Exception ex)
            {
                LogError($"Error obteniendo cadena de conexión '{name}': {ex.Message}");
                return string.Empty;
            }
        }

        /// <summary>
        /// Obtiene un valor booleano de App.config
        /// </summary>
        public static bool GetBoolAppSetting(string key, bool defaultValue = false)
        {
            string value = GetAppSetting(key);
            return bool.TryParse(value, out bool result) ? result : defaultValue;
        }

        /// <summary>
        /// Obtiene un valor entero de App.config
        /// </summary>
        public static int GetIntAppSetting(string key, int defaultValue = 0)
        {
            string value = GetAppSetting(key);
            return int.TryParse(value, out int result) ? result : defaultValue;
        }
        #endregion

        #region Métodos Privados de Configuraciones por Defecto
        private static PrinterConfiguration CreateDefaultPrinterConfiguration()
        {
            return new PrinterConfiguration
            {
                PrinterSettings = new PrinterSettings
                {
                    DefaultPrinter = "ZEBRA01",
                    AutoDetectPrinters = true,
                    ConnectionTimeout = 30,
                    PrintTimeout = 60,
                    MaxRetries = 3,
                    RetryDelay = 2000
                },
                ZebraPrinters = new List<ZebraPrinter>(),
                PrintParameters = new PrintParameters
                {
                    DefaultSpeed = 4,
                    DefaultDensity = 15,
                    DefaultTearOff = 0,
                    SpeedOptions = new[] { 2, 3, 4, 5, 6, 8, 10, 12 },
                    DensityOptions = new[] { 5, 10, 15, 20, 25, 30 }
                }
            };
        }

        private static TemplatesConfiguration CreateDefaultTemplatesConfiguration()
        {
            return new TemplatesConfiguration
            {
                TemplateSettings = new TemplateSettings
                {
                    DefaultTemplatesPath = @"Templates\ZPL\",
                    BackupTemplatesPath = @"Backups\Templates\",
                    EnableTemplateValidation = true,
                    AutoSaveChanges = true,
                    TemplateEncoding = "UTF-8"
                },
                LabelTypes = new List<LabelType>(),
                Variables = new Variables()
            };
        }

        private static DatabaseConfiguration CreateDefaultDatabaseConfiguration()
        {
            return new DatabaseConfiguration
            {
                DatabaseSettings = new DatabaseSettings
                {
                    Provider = "SqlServer",
                    ConnectionTimeout = 30,
                    CommandTimeout = 60,
                    EnableConnectionPooling = true,
                    MaxPoolSize = 100,
                    MinPoolSize = 5
                }
            };
        }

        private static SecurityConfiguration CreateDefaultSecurityConfiguration()
        {
            return new SecurityConfiguration
            {
                SecuritySettings = new SecuritySettings
                {
                    AuthenticationMode = "ComputerName",
                    EnableAuditLog = true,
                    SessionTimeoutMinutes = 60,
                    MaxConcurrentSessions = 1
                },
                ApplicationSecurity = new ApplicationSecurity
                {
                    ApplicationName = "ETIQUETAS MOLDURAS",
                    EnableComputerValidation = true,
                    DefaultPermissionLevel = "ReadOnly"
                }
            };
        }
        #endregion

        #region Métodos de Validación
        private static void ValidateConfigurations()
        {
            // Validar configuración de base de datos
            if (string.IsNullOrEmpty(GetConnectionString("EtiquetasDB")))
            {
                throw new ConfigurationErrorsException("Cadena de conexión 'EtiquetasDB' no configurada");
            }

            // Validar directorio de plantillas
            string templatesPath = GetAppSetting("ZPLTemplatesPath", @"Templates\ZPL\");
            if (!Directory.Exists(templatesPath))
            {
                Directory.CreateDirectory(templatesPath);
                LogEvent($"Directorio de plantillas creado: {templatesPath}");
            }

            // Validar directorio de logs
            string logsPath = GetAppSetting("LogPath", @"Logs\");
            if (!Directory.Exists(logsPath))
            {
                Directory.CreateDirectory(logsPath);
                LogEvent($"Directorio de logs creado: {logsPath}");
            }
        }
        #endregion

        #region Métodos Utilitarios
        private static string GetConfigPath()
        {
            return GetAppSetting("ConfigurationsPath", @"Configurations\");
        }

        /// <summary>
        /// Obtiene la ruta completa de un archivo en el directorio de configuraciones
        /// </summary>
        public static string GetConfigFilePath(string fileName)
        {
            return Path.Combine(GetConfigPath(), fileName);
        }

        /// <summary>
        /// Verifica si un archivo de configuración existe
        /// </summary>
        public static bool ConfigFileExists(string fileName)
        {
            return File.Exists(GetConfigFilePath(fileName));
        }

        /// <summary>
        /// Recargar todas las configuraciones
        /// </summary>
        public static void RefreshConfigurations()
        {
            lock (_lockObject)
            {
                _printerConfig = null;
                _templatesConfig = null;
                _databaseConfig = null;
                _securityConfig = null;
                _isInitialized = false;

                Initialize();
                LogEvent("Configuraciones recargadas");
            }
        }
        #endregion

        #region Métodos de Logging
        private static void LogEvent(string message)
        {
            try
            {
                // Usar el sistema de logging de la aplicación
                Console.WriteLine($"[CONFIG] {DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}");
                System.Diagnostics.Trace.TraceInformation($"[CONFIG] {message}");
            }
            catch
            {
                // Ignorar errores de logging para evitar bucles infinitos
            }
        }

        private static void LogError(string message)
        {
            try
            {
                Console.WriteLine($"[CONFIG ERROR] {DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}");
                System.Diagnostics.Trace.TraceError($"[CONFIG] {message}");
            }
            catch
            {
                // Ignorar errores de logging para evitar bucles infinitos
            }
        }
        #endregion
    }

    #region Clases de Configuración
    public class PrinterConfiguration
    {
        public PrinterSettings PrinterSettings { get; set; }
        public List<ZebraPrinter> ZebraPrinters { get; set; }
        public PrintParameters PrintParameters { get; set; }
    }

    public class PrinterSettings
    {
        public string DefaultPrinter { get; set; }
        public bool AutoDetectPrinters { get; set; }
        public int ConnectionTimeout { get; set; }
        public int PrintTimeout { get; set; }
        public int MaxRetries { get; set; }
        public int RetryDelay { get; set; }
    }

    public class ZebraPrinter
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string IPAddress { get; set; }
        public int Port { get; set; }
        public string Model { get; set; }
        public int Resolution { get; set; }
        public int MaxWidth { get; set; }
        public int MaxHeight { get; set; }
        public bool IsNetworkPrinter { get; set; }
        public bool IsEnabled { get; set; }
        public string Description { get; set; }
    }

    public class PrintParameters
    {
        public int DefaultSpeed { get; set; }
        public int DefaultDensity { get; set; }
        public int DefaultTearOff { get; set; }
        public int[] SpeedOptions { get; set; }
        public int[] DensityOptions { get; set; }
    }

    public class TemplatesConfiguration
    {
        public TemplateSettings TemplateSettings { get; set; }
        public List<LabelType> LabelTypes { get; set; }
        public Variables Variables { get; set; }
    }

    public class TemplateSettings
    {
        public string DefaultTemplatesPath { get; set; }
        public string BackupTemplatesPath { get; set; }
        public bool EnableTemplateValidation { get; set; }
        public bool AutoSaveChanges { get; set; }
        public string TemplateEncoding { get; set; }
    }

    public class LabelType
    {
        public string TipoCodigo { get; set; }
        public string Descripcion { get; set; }
        public string TemplateFile { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public bool IsActive { get; set; }
        public string[] RequiredFields { get; set; }
        public string[] OptionalFields { get; set; }
    }

    public class Variables
    {
        public List<Variable> GlobalVariables { get; set; }
        public List<Variable> CommonVariables { get; set; }
    }

    public class Variable
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Format { get; set; }
        public string Description { get; set; }
        public int MaxLength { get; set; }
        public bool Required { get; set; }
    }

    public class DatabaseConfiguration
    {
        public DatabaseSettings DatabaseSettings { get; set; }
    }

    public class DatabaseSettings
    {
        public string Provider { get; set; }
        public int ConnectionTimeout { get; set; }
        public int CommandTimeout { get; set; }
        public bool EnableConnectionPooling { get; set; }
        public int MaxPoolSize { get; set; }
        public int MinPoolSize { get; set; }
    }

    public class SecurityConfiguration
    {
        public SecuritySettings SecuritySettings { get; set; }
        public ApplicationSecurity ApplicationSecurity { get; set; }
    }

    public class SecuritySettings
    {
        public string AuthenticationMode { get; set; }
        public bool EnableAuditLog { get; set; }
        public int SessionTimeoutMinutes { get; set; }
        public int MaxConcurrentSessions { get; set; }
    }

    public class ApplicationSecurity
    {
        public string ApplicationName { get; set; }
        public bool EnableComputerValidation { get; set; }
        public string DefaultPermissionLevel { get; set; }
    }
    #endregion
}