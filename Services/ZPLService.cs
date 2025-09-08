using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using EtiquetasApp.Models;

namespace EtiquetasApp.Services
{
    public static class ZPLService
    {
        public static string GenerateEtiquetaCBCOE(EtiquetaData data)
        {
            var zpl = new StringBuilder();

            // Obtener posiciones desde configuración
            var pos1 = ConfigurationService.GetEtiquetaPosition("CBCOE", "Posicion1");
            var pos2 = ConfigurationService.GetEtiquetaPosition("CBCOE", "Posicion2");
            var pos3 = ConfigurationService.GetEtiquetaPosition("CBCOE", "Posicion3");
            var pos4 = ConfigurationService.GetEtiquetaPosition("CBCOE", "Posicion4");
            var pos5 = ConfigurationService.GetEtiquetaPosition("CBCOE", "Posicion5");

            zpl.AppendLine("^XA");
            zpl.AppendLine($"^PR{data.Velocidad}");
            zpl.AppendLine("^LH0,0^FS");
            zpl.AppendLine("^LL719");
            zpl.AppendLine("^MD0");
            zpl.AppendLine("^MNY");
            zpl.AppendLine("^LH0,0^FS");

            // Descripción en cada posición
            zpl.AppendLine($"^FO{int.Parse(pos1) - 5},280^A0R,28,23^CI13^FR^FD{FormatText(data.Descripcion)}^FS");
            zpl.AppendLine($"^FO{int.Parse(pos2) - 5},280^A0R,28,23^CI13^FR^FD{FormatText(data.Descripcion)}^FS");
            zpl.AppendLine($"^FO{int.Parse(pos3) - 5},280^A0R,28,23^CI13^FR^FD{FormatText(data.Descripcion)}^FS");
            zpl.AppendLine($"^FO{int.Parse(pos4) - 5},280^A0R,28,23^CI13^FR^FD{FormatText(data.Descripcion)}^FS");
            zpl.AppendLine($"^FO{int.Parse(pos5) - 5},280^A0R,28,23^CI13^FR^FD{FormatText(data.Descripcion)}^FS");

            // Códigos de barras
            zpl.AppendLine($"^BY2,3.0^FO{pos1},80^BER,37,Y,N^FR^FD{data.UPC}^FS");
            zpl.AppendLine($"^BY2,3.0^FO{pos2},80^BER,37,Y,N^FR^FD{data.UPC}^FS");
            zpl.AppendLine($"^BY2,3.0^FO{pos3},80^BER,37,Y,N^FR^FD{data.UPC}^FS");
            zpl.AppendLine($"^BY2,3.0^FO{pos4},80^BER,37,Y,N^FR^FD{data.UPC}^FS");
            zpl.AppendLine($"^BY2,3.0^FO{pos5},80^BER,37,Y,N^FR^FD{data.UPC}^FS");

            // Logos (si están configurados)
            if (!string.IsNullOrEmpty(data.Logo))
            {
                zpl.AppendLine($"^FO{int.Parse(pos1) - 15},20^FR^XG{data.Logo},1,1^FS");
                zpl.AppendLine($"^FO{int.Parse(pos2) - 15},20^FR^XG{data.Logo},1,1^FS");
                zpl.AppendLine($"^FO{int.Parse(pos3) - 15},20^FR^XG{data.Logo},1,1^FS");
                zpl.AppendLine($"^FO{int.Parse(pos4) - 15},20^FR^XG{data.Logo},1,1^FS");
                zpl.AppendLine($"^FO{int.Parse(pos5) - 15},20^FR^XG{data.Logo},1,1^FS");
            }

            zpl.AppendLine($"^PQ{data.Cantidad},0,1,Y");
            zpl.AppendLine("^XZ");

            return zpl.ToString();
        }

        public static string GenerateEtiquetaDual(EtiquetaData data)
        {
            var zpl = new StringBuilder();

            var pos1 = ConfigurationService.GetEtiquetaPosition("DUAL", "Posicion1");
            var pos2 = ConfigurationService.GetEtiquetaPosition("DUAL", "Posicion2");
            var pos3 = ConfigurationService.GetEtiquetaPosition("DUAL", "Posicion3");
            var pos4 = ConfigurationService.GetEtiquetaPosition("DUAL", "Posicion4");
            var pos5 = ConfigurationService.GetEtiquetaPosition("DUAL", "Posicion5");

            zpl.AppendLine("^XA");
            zpl.AppendLine($"^PR{data.Velocidad}");
            zpl.AppendLine("^LH0,0");
            zpl.AppendLine("^LL400");
            zpl.AppendLine("^JMA");

            // Códigos duales con separación
            var offset = 65;
            zpl.AppendLine($"^BY2,3.0^FO{pos1},43^BUR,25,Y,N,Y^FR^FD{data.UPC}^FS");
            zpl.AppendLine($"^BY2,3.0^FO{int.Parse(pos1) + offset},43^BUR,25,Y,N,Y^FR^FD{data.UPC2}^FS");

            zpl.AppendLine($"^BY2,3.0^FO{pos2},43^BUR,25,Y,N,Y^FR^FD{data.UPC}^FS");
            zpl.AppendLine($"^BY2,3.0^FO{int.Parse(pos2) + offset},43^BUR,25,Y,N,Y^FR^FD{data.UPC2}^FS");

            zpl.AppendLine($"^BY2,3.0^FO{pos3},43^BUR,25,Y,N,Y^FR^FD{data.UPC}^FS");
            zpl.AppendLine($"^BY2,3.0^FO{int.Parse(pos3) + offset},43^BUR,25,Y,N,Y^FR^FD{data.UPC2}^FS");

            zpl.AppendLine($"^BY2,3.0^FO{pos4},43^BUR,25,Y,N,Y^FR^FD{data.UPC}^FS");
            zpl.AppendLine($"^BY2,3.0^FO{int.Parse(pos4) + offset},43^BUR,25,Y,N,Y^FR^FD{data.UPC2}^FS");

            zpl.AppendLine($"^BY2,3.0^FO{pos5},43^BUR,25,Y,N,Y^FR^FD{data.UPC}^FS");
            zpl.AppendLine($"^BY2,3.0^FO{int.Parse(pos5) + offset},43^BUR,25,Y,N,Y^FR^FD{data.UPC2}^FS");

            zpl.AppendLine($"^PQ{data.Cantidad},0,1,Y");
            zpl.AppendLine("^XZ");

            return zpl.ToString();
        }

        public static string GenerateEtiquetaEAN13(EtiquetaData data)
        {
            var zpl = new StringBuilder();

            var pos1 = ConfigurationService.GetEtiquetaPosition("EAN13", "Posicion1");
            var pos2 = ConfigurationService.GetEtiquetaPosition("EAN13", "Posicion2");
            var pos3 = ConfigurationService.GetEtiquetaPosition("EAN13", "Posicion3");
            var pos4 = ConfigurationService.GetEtiquetaPosition("EAN13", "Posicion4");
            var pos5 = ConfigurationService.GetEtiquetaPosition("EAN13", "Posicion5");

            zpl.AppendLine("^XA");
            zpl.AppendLine($"^PR{data.Velocidad}");
            zpl.AppendLine("^LH0,0");
            zpl.AppendLine("^LL400");
            zpl.AppendLine("^JMA");

            // Información de orden y cantidad
            zpl.AppendLine($"^FO20,30^CI0^ASR,28,15^FR^FDOF : {data.OrdenFab}^FS");
            zpl.AppendLine($"^FO60,30^CI0^ASR,28,15^FR^FDCant. : {data.Cantidad}^FS");

            // Repetir para las 5 posiciones
            for (int i = 1; i <= 5; i++)
            {
                var posX = i * 150 - 130; // Calculado aproximadamente
                zpl.AppendLine($"^FO{posX},30^CI0^ASR,28,15^FR^FDOF : {data.OrdenFab}^FS");
                zpl.AppendLine($"^FO{posX + 40},30^CI0^ASR,28,15^FR^FDCant. : {data.Cantidad}^FS");
            }

            // Códigos EAN13
            zpl.AppendLine($"^BY2,3.0^FO{pos1},100^BER,65,Y,N^FR^FD{data.UPC}^FS");
            zpl.AppendLine($"^BY2,3.0^FO{pos2},100^BER,65,Y,N^FR^FD{data.UPC}^FS");
            zpl.AppendLine($"^BY2,3.0^FO{pos3},100^BER,65,Y,N^FR^FD{data.UPC}^FS");
            zpl.AppendLine($"^BY2,3.0^FO{pos4},100^BER,65,Y,N^FR^FD{data.UPC}^FS");
            zpl.AppendLine($"^BY2,3.0^FO{pos5},100^BER,65,Y,N^FR^FD{data.UPC}^FS");

            zpl.AppendLine($"^PQ1,0,1,Y"); // Solo 1 copia para EAN13
            zpl.AppendLine("^XZ");

            return zpl.ToString();
        }

        public static string GenerateEtiquetaBicolor(EtiquetaData data)
        {
            var zpl = new StringBuilder();

            zpl.AppendLine("^XA");
            zpl.AppendLine($"^PR{data.Velocidad}");
            zpl.AppendLine($"^MT{data.Temperatura}");
            zpl.AppendLine("^LH0,0");
            zpl.AppendLine("^LL400");

            // Para BICOLOR generalmente es más simple
            zpl.AppendLine($"^FO50,50^A0N,50,50^FD{data.Descripcion}^FS");
            zpl.AppendLine($"^FO50,120^BY3^BCN,100,Y,N,N^FD{data.UPC}^FS");

            if (!string.IsNullOrEmpty(data.OrdenFab))
            {
                zpl.AppendLine($"^FO50,250^A0N,30,30^FDOF: {data.OrdenFab}^FS");
            }

            zpl.AppendLine($"^PQ{data.Cantidad},0,1,Y");
            zpl.AppendLine("^XZ");

            return zpl.ToString();
        }

        public static string GenerateEtiquetaGardenState(EtiquetaData data)
        {
            var template = LoadTemplate("garden_state.zpl");
            if (!string.IsNullOrEmpty(template))
            {
                return ReplaceTemplatePlaceholders(template, data);
            }

            // Template por defecto si no existe archivo
            var zpl = new StringBuilder();
            zpl.AppendLine("^XA");
            zpl.AppendLine($"^PR{data.Velocidad}");
            zpl.AppendLine("^LH0,0");
            zpl.AppendLine("^LL1918");
            zpl.AppendLine("^PW831");

            // Garden State es más complejo, incluye muchos campos
            zpl.AppendLine($"^FT277,261^A0B,51,50^FH\\^FD{data.Campo1}^FS");
            zpl.AppendLine($"^FT276,613^A0B,51,50^FH\\^FDPcs/Bundle:^FS");
            zpl.AppendLine($"^FT468,1451^A0B,54,52^FH\\^FDARAUCO^FS");
            zpl.AppendLine($"^FT467,1779^A0B,54,52^FH\\^FDSource: ^FS");
            zpl.AppendLine($"^FT473,563^A0B,51,50^FH\\^FD{data.Campo2}^FS");
            zpl.AppendLine($"^FT471,930^A0B,51,50^FH\\^FDLength:^FS");
            zpl.AppendLine($"^FT349,870^A0B,51,50^FH\\^FD{data.Campo3}^FS");
            zpl.AppendLine($"^FT278,1006^A0B,51,50^FH\\^FDBundle Quantity:^FS");
            zpl.AppendLine($"^FT285,1773^A0B,51,50^FH\\^FDItem:^FS");

            // Código de barras principal
            zpl.AppendLine($"^FO41,288^BY7,3,160^FT710,1660^BCB,,Y,N^FR^FD{data.UPC}^FS");

            zpl.AppendLine($"^PQ{data.Cantidad},0,1,Y");
            zpl.AppendLine("^XZ");

            return zpl.ToString();
        }

        public static string GenerateEtiquetaMolduras(EtiquetaData data)
        {
            var zpl = new StringBuilder();

            zpl.AppendLine("^XA");
            zpl.AppendLine($"^PR{data.Velocidad}");
            zpl.AppendLine("^LH0,0");
            zpl.AppendLine("^LL400");

            // Específico para molduras
            zpl.AppendLine($"^FO50,50^A0N,40,40^FD{data.Descripcion}^FS");
            zpl.AppendLine($"^FO50,100^A0N,25,25^FDSKU: {data.SKU}^FS");

            if (!string.IsNullOrEmpty(data.UPC))
            {
                zpl.AppendLine($"^FO50,150^BY2^BCN,80,Y,N,N^FD{data.UPC}^FS");
            }

            zpl.AppendLine($"^FO50,250^A0N,20,20^FDPiezas por bolsa^FS");
            zpl.AppendLine($"^FO50,280^A0N,30,30^FDVelocidad: {data.Velocidad}^FS");

            zpl.AppendLine($"^PQ{data.Cantidad},0,1,Y");
            zpl.AppendLine("^XZ");

            return zpl.ToString();
        }

        public static string LoadTemplate(string templateName)
        {
            try
            {
                var templatePath = Path.Combine(ConfigurationService.TemplatesPath, templateName);
                if (File.Exists(templatePath))
                {
                    return File.ReadAllText(templatePath, Encoding.UTF8);
                }
            }
            catch (Exception ex)
            {
                // Log error but continue with default template
                System.Diagnostics.Debug.WriteLine($"Error cargando template {templateName}: {ex.Message}");
            }
            return null;
        }

        public static void SaveTemplate(string templateName, string zplContent)
        {
            try
            {
                var templatePath = Path.Combine(ConfigurationService.TemplatesPath, templateName);
                File.WriteAllText(templatePath, zplContent, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error guardando template {templateName}: {ex.Message}", ex);
            }
        }

        private static string ReplaceTemplatePlaceholders(string template, EtiquetaData data)
        {
            var result = template;

            // Reemplazar placeholders comunes
            result = result.Replace("{VELOCIDAD}", data.Velocidad.ToString());
            result = result.Replace("{TEMPERATURA}", data.Temperatura.ToString());
            result = result.Replace("{CANTIDAD}", data.Cantidad.ToString());
            result = result.Replace("{UPC}", data.UPC ?? "");
            result = result.Replace("{UPC2}", data.UPC2 ?? "");
            result = result.Replace("{DESCRIPCION}", FormatText(data.Descripcion ?? ""));
            result = result.Replace("{ORDEN_FAB}", data.OrdenFab ?? "");
            result = result.Replace("{SKU}", data.SKU ?? "");
            result = result.Replace("{CAMPO1}", data.Campo1 ?? "");
            result = result.Replace("{CAMPO2}", data.Campo2 ?? "");
            result = result.Replace("{CAMPO3}", data.Campo3 ?? "");
            result = result.Replace("{FECHA}", DateTime.Now.ToString("dd/MM/yyyy"));
            result = result.Replace("{HORA}", DateTime.Now.ToString("HH:mm:ss"));

            // Reemplazar posiciones
            result = result.Replace("{POS1}", ConfigurationService.GetEtiquetaPosition(data.TipoEtiqueta, "Posicion1"));
            result = result.Replace("{POS2}", ConfigurationService.GetEtiquetaPosition(data.TipoEtiqueta, "Posicion2"));
            result = result.Replace("{POS3}", ConfigurationService.GetEtiquetaPosition(data.TipoEtiqueta, "Posicion3"));
            result = result.Replace("{POS4}", ConfigurationService.GetEtiquetaPosition(data.TipoEtiqueta, "Posicion4"));
            result = result.Replace("{POS5}", ConfigurationService.GetEtiquetaPosition(data.TipoEtiqueta, "Posicion5"));

            return result;
        }

        private static string FormatText(string text)
        {
            if (string.IsNullOrEmpty(text))
                return "";

            // Limpiar caracteres especiales para ZPL
            var formatted = text
                .Replace("ñ", "n")
                .Replace("Ñ", "N")
                .Replace("á", "a").Replace("é", "e").Replace("í", "i").Replace("ó", "o").Replace("ú", "u")
                .Replace("Á", "A").Replace("É", "E").Replace("Í", "I").Replace("Ó", "O").Replace("Ú", "U")
                .Replace("ü", "u").Replace("Ü", "U")
                .Replace("^", "").Replace("~", ""); // Caracteres especiales ZPL

            // Truncar si es muy largo
            if (formatted.Length > 50)
                formatted = formatted.Substring(0, 50);

            return formatted;
        }

        public static List<string> GetAvailableTemplates()
        {
            var templates = new List<string>();

            try
            {
                var templatesPath = ConfigurationService.TemplatesPath;
                if (Directory.Exists(templatesPath))
                {
                    var files = Directory.GetFiles(templatesPath, "*.zpl");
                    foreach (var file in files)
                    {
                        templates.Add(Path.GetFileName(file));
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error obteniendo templates: {ex.Message}");
            }

            return templates;
        }

        public static bool ValidateZPL(string zplCode)
        {
            if (string.IsNullOrEmpty(zplCode))
                return false;

            // Validaciones básicas
            if (!zplCode.Contains("^XA"))
                return false;

            if (!zplCode.Contains("^XZ"))
                return false;

            // Verificar que no tenga caracteres inválidos
            if (zplCode.Contains("${") || zplCode.Contains("}$"))
                return false;

            return true;
        }
    }
}