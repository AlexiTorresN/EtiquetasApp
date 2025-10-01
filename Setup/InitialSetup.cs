using System;
using System.IO;
using EtiquetasApp.Services;

namespace EtiquetasApp.Setup
{
    public static class InitialSetup
    {
        public static void Initialize()
        {
            try
            {
                // Inicializar servicios de configuración
                ConfigurationService.Initialize();

                // Crear directorios necesarios
                CreateRequiredDirectories();

                // Validar y crear configuración por defecto si no existe
                if (!ConfigurationService.ValidateConfiguration())
                {
                    ConfigurationService.CreateDefaultConfiguration();
                }

                // Verificar templates
                EnsureDefaultTemplates();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error en setup inicial: {ex.Message}", ex);
            }
        }

        private static void CreateRequiredDirectories()
        {
            // Templates
            var templatesPath = ConfigurationService.TemplatesPath;
            if (!Directory.Exists(templatesPath))
                Directory.CreateDirectory(templatesPath);

            // Resources
            var resourcesPath = ConfigurationService.ResourcesPath;
            if (!Directory.Exists(resourcesPath))
                Directory.CreateDirectory(resourcesPath);

            // Images
            var imagesPath = ConfigurationService.ImagesPath;
            if (!Directory.Exists(imagesPath))
                Directory.CreateDirectory(imagesPath);

            // Logs
            var logsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
            if (!Directory.Exists(logsPath))
                Directory.CreateDirectory(logsPath);
        }

        private static void EnsureDefaultTemplates()
        {
            var templatesPath = ConfigurationService.TemplatesPath;

            // Crear template de Garden State si no existe
            var gardenStatePath = Path.Combine(templatesPath, "garden_state.zpl");
            if (!File.Exists(gardenStatePath))
            {
                CreateDefaultGardenStateTemplate(gardenStatePath);
            }
        }

        private static void CreateDefaultGardenStateTemplate(string path)
        {
            var template = @"^XA
^PR{VELOCIDAD}
^LH0,0
^LL1918
^PW831
^FT277,261^A0B,51,50^FH\^FD{CAMPO1}^FS
^FT276,613^A0B,51,50^FH\^FDPcs/Bundle:^FS
^FT468,1451^A0B,54,52^FH\^FDARAUCO^FS
^FT467,1779^A0B,54,52^FH\^FDSource: ^FS
^FT473,563^A0B,51,50^FH\^FD{CAMPO2}^FS
^FT471,930^A0B,51,50^FH\^FDLength:^FS
^FT349,870^A0B,51,50^FH\^FD{CAMPO3}^FS
^FT278,1006^A0B,51,50^FH\^FDBundle Quantity:^FS
^FT285,1773^A0B,51,50^FH\^FDItem:^FS
^FO41,288^BY7,3,160^FT710,1660^BCB,,Y,N^FR^FD{UPC}^FS
^PQ{CANTIDAD},0,1,Y
^XZ";

            File.WriteAllText(path, template);
        }
    }
}