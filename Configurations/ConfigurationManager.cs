using Newtonsoft.Json;
using System;
using System.IO;
using EtiquetasApp.Models;
using SysConfigManager = System.Configuration.ConfigurationManager;

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
        public static void Initialize()
        {
            lock (_lockObject)
            {
                if (_isInitialized) return;

                try
                {
                    LoadAllConfigurations();
                    ValidateConfigurations();
                    _isInitialized = true;
                    LogEvent("ConfigurationManager inicializado correctamente");
                }
                catch (Exception ex)
                {
                    LogError($"Error inicializando ConfigurationManager: {ex.Message}");
                    throw;
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

        #region Métodos de Carga
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
        public static string GetAppSetting(string key, string defaultValue = "")
        {
            try
            {
                return SysConfigManager.AppSettings[key] ?? defaultValue;
            }
            catch
            {
                return defaultValue;
            }
        }

        public static string GetConnectionString(string name)
        {
            try
            {
                var connectionString = SysConfigManager.ConnectionStrings[name];
                return connectionString?.ConnectionString ?? string.Empty;
            }
            catch (Exception ex)
            {
                LogError($"Error obteniendo cadena de conexión '{name}': {ex.Message}");
                return string.Empty;
            }
        }

        public static bool GetBoolAppSetting(string key, bool defaultValue = false)
        {
            string value = GetAppSetting(key);
            return bool.TryParse(value, out bool result) ? result : defaultValue;
        }

        public static int GetIntAppSetting(string key, int defaultValue = 0)
        {
            string value = GetAppSetting(key);
            return int.TryParse(value, out int result) ? result : defaultValue;
        }
        #endregion

        #region Métodos de Configuraciones por Defecto
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
                }
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
            if (string.IsNullOrEmpty(GetConnectionString("DatabaseConnection1")))
            {
                LogError("Advertencia: Cadena de conexión 'DatabaseConnection1' no configurada");
            }

            string templatesPath = GetAppSetting("TemplatesPath", @"Templates\");
            if (!Directory.Exists(templatesPath))
            {
                Directory.CreateDirectory(templatesPath);
                LogEvent($"Directorio de plantillas creado: {templatesPath}");
            }

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
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string configPath = Path.Combine(basePath, "Configurations");

            if (!Directory.Exists(configPath))
            {
                Directory.CreateDirectory(configPath);
            }

            return configPath;
        }

        private static void LogEvent(string message)
        {
            try
            {
                Console.WriteLine($"[INFO] {DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}");
            }
            catch { }
        }

        private static void LogError(string message)
        {
            try
            {
                Console.WriteLine($"[ERROR] {DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}");
            }
            catch { }
        }
        #endregion
    }
}