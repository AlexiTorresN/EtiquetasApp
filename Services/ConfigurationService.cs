using System;
using System.Configuration;
using System.IO;

namespace EtiquetasApp.Services
{
    public static class ConfigurationService
    {
        private static Configuration _config;

        public static void Initialize()
        {
            try
            {
                _config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error al cargar configuración: {ex.Message}", ex);
            }
        }

        // Conexiones de Base de Datos
        public static string GetConnectionString(string name)
        {
            var connectionString = ConfigurationManager.ConnectionStrings[name];
            if (connectionString == null)
                throw new ConfigurationErrorsException($"Cadena de conexión '{name}' no encontrada en app.config");

            return connectionString.ConnectionString;
        }

        public static string DatabaseConnection1 => GetConnectionString("DatabaseConnection1");
        public static string DatabaseConnection2 => GetConnectionString("DatabaseConnection2");

        // Configuración de Impresión
        public static string DefaultPrinter
        {
            get => GetAppSetting("DefaultPrinter", "");
            set => SetAppSetting("DefaultPrinter", value);
        }

        public static int DefaultVelocidad
        {
            get => int.Parse(GetAppSetting("DefaultVelocidad", "4"));
            set => SetAppSetting("DefaultVelocidad", value.ToString());
        }

        public static int DefaultTemperatura
        {
            get => int.Parse(GetAppSetting("DefaultTemperatura", "6"));
            set => SetAppSetting("DefaultTemperatura", value.ToString());
        }

        // Configuración de Templates
        public static string TemplatesPath
        {
            get
            {
                var path = GetAppSetting("TemplatesPath", Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates"));
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                return path;
            }
            set => SetAppSetting("TemplatesPath", value);
        }

        // Configuración de Recursos
        public static string ResourcesPath
        {
            get
            {
                var path = GetAppSetting("ResourcesPath", Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources"));
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                return path;
            }
            set => SetAppSetting("ResourcesPath", value);
        }

        public static string ImagesPath
        {
            get
            {
                var path = Path.Combine(ResourcesPath, "Images");
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                return path;
            }
        }

        // Configuración de posiciones para etiquetas
        public static string GetEtiquetaPosition(string tipoEtiqueta, string posicion)
        {
            return GetAppSetting($"{tipoEtiqueta}_{posicion}", "0");
        }

        public static void SetEtiquetaPosition(string tipoEtiqueta, string posicion, string valor)
        {
            SetAppSetting($"{tipoEtiqueta}_{posicion}", valor);
        }

        // Métodos auxiliares
        private static string GetAppSetting(string key, string defaultValue = "")
        {
            try
            {
                var value = ConfigurationManager.AppSettings[key];
                return !string.IsNullOrEmpty(value) ? value : defaultValue;
            }
            catch
            {
                return defaultValue;
            }
        }

        private static void SetAppSetting(string key, string value)
        {
            try
            {
                if (_config.AppSettings.Settings[key] != null)
                    _config.AppSettings.Settings[key].Value = value;
                else
                    _config.AppSettings.Settings.Add(key, value);

                _config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error al guardar configuración '{key}': {ex.Message}", ex);
            }
        }

        // Métodos para validar configuración
        public static bool ValidateConfiguration()
        {
            try
            {
                // Verificar conexiones de BD
                if (string.IsNullOrEmpty(GetConnectionString("DatabaseConnection1")))
                    return false;

                if (string.IsNullOrEmpty(GetConnectionString("DatabaseConnection2")))
                    return false;

                // Verificar directorios
                if (!Directory.Exists(TemplatesPath))
                    Directory.CreateDirectory(TemplatesPath);

                if (!Directory.Exists(ResourcesPath))
                    Directory.CreateDirectory(ResourcesPath);

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
                // Configuración por defecto
                DefaultVelocidad = 4;
                DefaultTemperatura = 6;

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
    }
}