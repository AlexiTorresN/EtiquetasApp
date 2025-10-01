using System.Collections.Generic;

namespace EtiquetasApp.Models
{
    // CONFIGURACIÓN DE IMPRESORAS
    public class PrinterConfiguration
    {
        public PrinterSettings PrinterSettings { get; set; }
        public List<ZebraPrinter> ZebraPrinters { get; set; }
        public PrintParameters PrintParameters { get; set; }

        public PrinterConfiguration()
        {
            PrinterSettings = new PrinterSettings();
            ZebraPrinters = new List<ZebraPrinter>();
            PrintParameters = new PrintParameters();
        }
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
        public int Port { get; set; } = 9100;
        public string Model { get; set; }
        public int Resolution { get; set; } = 203;
        public int MaxWidth { get; set; }
        public int MaxHeight { get; set; }
        public bool IsNetworkPrinter { get; set; } = true;
        public bool IsEnabled { get; set; } = true;
        public string Description { get; set; }

        // Propiedades de compatibilidad
        public int DPI => Resolution;
    }

    public class PrintParameters
    {
        public int DefaultSpeed { get; set; }
        public int DefaultDensity { get; set; }
        public int DefaultTearOff { get; set; }
        public int[] SpeedOptions { get; set; }
        public int[] DensityOptions { get; set; }
    }

    // CONFIGURACIÓN DE TEMPLATES
    public class TemplatesConfiguration
    {
        public TemplateSettings TemplateSettings { get; set; }
        public List<LabelType> LabelTypes { get; set; }
        public TemplateVariables Variables { get; set; }

        public TemplatesConfiguration()
        {
            TemplateSettings = new TemplateSettings();
            LabelTypes = new List<LabelType>();
            Variables = new TemplateVariables();
        }
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

    public class TemplateVariables
    {
        public List<TemplateVariable> GlobalVariables { get; set; }
        public List<TemplateVariable> CommonVariables { get; set; }

        public TemplateVariables()
        {
            GlobalVariables = new List<TemplateVariable>();
            CommonVariables = new List<TemplateVariable>();
        }
    }

    public class TemplateVariable
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Format { get; set; }
        public string Description { get; set; }
        public int MaxLength { get; set; }
        public bool Required { get; set; }
    }

    // CONFIGURACIÓN DE BASE DE DATOS
    public class DatabaseConfiguration
    {
        public DatabaseSettings DatabaseSettings { get; set; }

        public DatabaseConfiguration()
        {
            DatabaseSettings = new DatabaseSettings();
        }
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

    // CONFIGURACIÓN DE SEGURIDAD
    public class SecurityConfiguration
    {
        public SecuritySettings SecuritySettings { get; set; }
        public ApplicationSecurity ApplicationSecurity { get; set; }

        public SecurityConfiguration()
        {
            SecuritySettings = new SecuritySettings();
            ApplicationSecurity = new ApplicationSecurity();
        }
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
}