using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace EtiquetasApp.Services
{
    public static class PrinterService
    {
        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool OpenPrinter(string szPrinter, out IntPtr hPrinter, IntPtr pd);

        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool ClosePrinter(IntPtr hPrinter);

        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool StartDocPrinter(IntPtr hPrinter, int level, [In, MarshalAs(UnmanagedType.LPStruct)] DOCINFOA di);

        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool EndDocPrinter(IntPtr hPrinter);

        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool StartPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool EndPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool WritePrinter(IntPtr hPrinter, IntPtr pBytes, int dwCount, out int dwWritten);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public class DOCINFOA
        {
            [MarshalAs(UnmanagedType.LPStr)]
            public string pDocName;
            [MarshalAs(UnmanagedType.LPStr)]
            public string pOutputFile;
            [MarshalAs(UnmanagedType.LPStr)]
            public string pDataType;
        }

        public static List<string> GetInstalledPrinters()
        {
            var printers = new List<string>();

            try
            {
                foreach (string printerName in PrinterSettings.InstalledPrinters)
                {
                    printers.Add(printerName);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error obteniendo impresoras instaladas: {ex.Message}", ex);
            }

            return printers;
        }

        public static List<string> GetZebraPrinters()
        {
            var zebraPrinters = new List<string>();
            var installedPrinters = GetInstalledPrinters();

            foreach (var printer in installedPrinters)
            {
                if (printer.ToUpper().Contains("ZEBRA") ||
                    printer.ToUpper().Contains("ZPL") ||
                    IsZebraPrinter(printer))
                {
                    zebraPrinters.Add(printer);
                }
            }

            return zebraPrinters;
        }

        private static bool IsZebraPrinter(string printerName)
        {
            try
            {
                var printerSettings = new PrinterSettings();
                printerSettings.PrinterName = printerName;

                // Verificar si es válida y obtener información
                return printerSettings.IsValid;
            }
            catch
            {
                return false;
            }
        }

        public static bool PrinterExists(string printerName)
        {
            var installedPrinters = GetInstalledPrinters();
            return installedPrinters.Any(p => p.Equals(printerName, StringComparison.OrdinalIgnoreCase));
        }

        public static bool SendRawDataToPrinter(string printerName, string data)
        {
            IntPtr hPrinter = IntPtr.Zero;
            var di = new DOCINFOA();
            bool success = false;

            try
            {
                // Abrir la impresora
                if (!OpenPrinter(printerName.Normalize(), out hPrinter, IntPtr.Zero))
                {
                    throw new Exception($"No se pudo abrir la impresora: {printerName}");
                }

                // Configurar el documento
                di.pDocName = "Etiqueta ZPL";
                di.pDataType = "RAW";

                // Iniciar documento
                if (!StartDocPrinter(hPrinter, 1, di))
                {
                    throw new Exception("No se pudo iniciar el documento de impresión");
                }

                // Iniciar página
                if (!StartPagePrinter(hPrinter))
                {
                    throw new Exception("No se pudo iniciar la página de impresión");
                }

                // Convertir datos a bytes
                byte[] bytes = Encoding.UTF8.GetBytes(data);
                IntPtr pBytes = Marshal.AllocHGlobal(bytes.Length);
                Marshal.Copy(bytes, 0, pBytes, bytes.Length);

                // Enviar datos a la impresora
                int bytesWritten;
                if (!WritePrinter(hPrinter, pBytes, bytes.Length, out bytesWritten))
                {
                    throw new Exception("Error al escribir datos en la impresora");
                }

                // Finalizar página
                EndPagePrinter(hPrinter);

                // Finalizar documento
                EndDocPrinter(hPrinter);

                success = true;
                Marshal.FreeHGlobal(pBytes);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error imprimiendo: {ex.Message}", ex);
            }
            finally
            {
                // Cerrar impresora
                if (hPrinter != IntPtr.Zero)
                {
                    ClosePrinter(hPrinter);
                }
            }

            return success;
        }

        public static bool SendZPLToPrinter(string printerName, string zplCode)
        {
            try
            {
                // Verificar que la impresora existe
                if (!PrinterExists(printerName))
                {
                    throw new Exception($"Impresora '{printerName}' no encontrada");
                }

                // Limpiar el código ZPL si es necesario
                var cleanZpl = CleanZPLCode(zplCode);

                // Enviar a la impresora
                return SendRawDataToPrinter(printerName, cleanZpl);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error enviando ZPL a impresora: {ex.Message}", ex);
            }
        }

        private static string CleanZPLCode(string zplCode)
        {
            if (string.IsNullOrEmpty(zplCode))
                return "";

            // Remover caracteres especiales que pueden causar problemas
            var cleaned = zplCode.Replace("${", "").Replace("}$", "");

            // Asegurar que comience con ^XA y termine con ^XZ
            if (!cleaned.StartsWith("^XA"))
                cleaned = "^XA" + cleaned;

            if (!cleaned.EndsWith("^XZ"))
                cleaned = cleaned + "^XZ";

            return cleaned;
        }

        public static void SaveZPLToFile(string zplCode, string fileName)
        {
            try
            {
                var filePath = Path.Combine(ConfigurationService.TemplatesPath, fileName);
                File.WriteAllText(filePath, zplCode, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error guardando archivo ZPL: {ex.Message}", ex);
            }
        }

        public static string LoadZPLFromFile(string fileName)
        {
            try
            {
                var filePath = Path.Combine(ConfigurationService.TemplatesPath, fileName);
                if (!File.Exists(filePath))
                    throw new FileNotFoundException($"Archivo ZPL no encontrado: {fileName}");

                return File.ReadAllText(filePath, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error cargando archivo ZPL: {ex.Message}", ex);
            }
        }

        public static bool TestPrinter(string printerName)
        {
            try
            {
                var testZpl = @"
                ^XA
                ^LH0,0
                ^FO50,50^A0N,50,50^FDTest de Impresion^FS
                ^FO50,120^A0N,30,30^FDSistema de Etiquetas^FS
                ^FO50,170^A0N,25,25^FD" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + @"^FS
                ^XZ";

                return SendZPLToPrinter(printerName, testZpl);
            }
            catch
            {
                return false;
            }
        }

        public static string GetDefaultPrinter()
        {
            try
            {
                var defaultPrinter = new PrinterSettings().PrinterName;
                return defaultPrinter;
            }
            catch
            {
                return "";
            }
        }

        public static PrinterInfo GetPrinterInfo(string printerName)
        {
            try
            {
                var printerSettings = new PrinterSettings();
                printerSettings.PrinterName = printerName;

                if (!printerSettings.IsValid)
                    return null;

                return new PrinterInfo
                {
                    Name = printerName,
                    IsDefault = printerName.Equals(GetDefaultPrinter(), StringComparison.OrdinalIgnoreCase),
                    IsOnline = true, // Simplificado - en una implementación real se podría verificar el estado
                    SupportsZPL = printerName.ToUpper().Contains("ZEBRA") ||
                                 printerName.ToUpper().Contains("ZPL"),
                    MaxCopies = printerSettings.MaximumCopies
                };
            }
            catch
            {
                return null;
            }
        }
    }

    public class PrinterInfo
    {
        public string Name { get; set; }
        public bool IsDefault { get; set; }
        public bool IsOnline { get; set; }
        public bool SupportsZPL { get; set; }
        public int MaxCopies { get; set; }
        public string Status { get; set; } = "Listo";
    }
}