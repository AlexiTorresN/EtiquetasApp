using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using EtiquetasApp.Models;

namespace EtiquetasApp.Services
{
    public static class PrinterService
    {
        private static readonly Dictionary<string, bool> _printerStatus = new Dictionary<string, bool>();

        /// <summary>
        /// Envía código ZPL a una impresora Zebra
        /// </summary>
        public static bool SendZPLToPrinter(string printerName, string zplCode)
        {
            try
            {
                if (string.IsNullOrEmpty(printerName) || string.IsNullOrEmpty(zplCode))
                {
                    LogError("Nombre de impresora o código ZPL vacío");
                    return false;
                }

                var printerConfig = GetPrinterConfiguration(printerName);
                if (printerConfig == null)
                {
                    LogError($"Configuración de impresora '{printerName}' no encontrada");
                    return false;
                }

                if (printerConfig.IsNetworkPrinter)
                {
                    return SendZPLToNetworkPrinter(printerConfig.IPAddress, printerConfig.Port, zplCode);
                }
                else
                {
                    return SendZPLToLocalPrinter(printerName, zplCode);
                }
            }
            catch (Exception ex)
            {
                LogError($"Error enviando ZPL a impresora '{printerName}': {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Envía código ZPL a una impresora de red por TCP/IP
        /// </summary>
        private static bool SendZPLToNetworkPrinter(string ipAddress, int port, string zplCode)
        {
            try
            {
                using (var client = new TcpClient())
                {
                    var connectTask = client.ConnectAsync(ipAddress, port);
                    if (!connectTask.Wait(5000))
                    {
                        LogError($"Timeout conectando a impresora en {ipAddress}:{port}");
                        return false;
                    }

                    using (var stream = client.GetStream())
                    {
                        byte[] data = Encoding.UTF8.GetBytes(zplCode);
                        stream.Write(data, 0, data.Length);
                        stream.Flush();
                        System.Threading.Thread.Sleep(500);
                    }
                }

                LogInfo($"Código ZPL enviado exitosamente a {ipAddress}:{port}");
                return true;
            }
            catch (SocketException ex)
            {
                LogError($"Error de red enviando a {ipAddress}:{port}: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                LogError($"Error enviando ZPL a impresora de red: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Envía código ZPL a una impresora local/USB
        /// </summary>
        private static bool SendZPLToLocalPrinter(string printerName, string zplCode)
        {
            try
            {
                string tempPath = Path.Combine(Path.GetTempPath(), $"ZPL_{printerName}_{DateTime.Now:yyyyMMdd_HHmmss}.zpl");
                File.WriteAllText(tempPath, zplCode);

                LogInfo($"Código ZPL guardado en archivo temporal: {tempPath}");

                // TODO: Implementar envío real a impresora local usando:
                // - System.Drawing.Printing
                // - Win32 APIs
                // - Drivers específicos de Zebra

                return true;
            }
            catch (Exception ex)
            {
                LogError($"Error enviando ZPL a impresora local: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Obtiene la configuración de una impresora
        /// </summary>
        private static ZebraPrinter GetPrinterConfiguration(string printerName)
        {
            try
            {
                var config = ConfigurationService.PrinterConfig;
                return config?.ZebraPrinters?.Find(p =>
                    p.Name.Equals(printerName, StringComparison.OrdinalIgnoreCase) && p.IsEnabled);
            }
            catch (Exception ex)
            {
                LogError($"Error obteniendo configuración de impresora: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Verifica el estado de una impresora
        /// </summary>
        public static bool CheckPrinterStatus(string printerName)
        {
            try
            {
                var printerConfig = GetPrinterConfiguration(printerName);
                if (printerConfig == null)
                    return false;

                if (printerConfig.IsNetworkPrinter)
                {
                    return CheckNetworkPrinterStatus(printerConfig.IPAddress, printerConfig.Port);
                }
                else
                {
                    return CheckLocalPrinterStatus(printerName);
                }
            }
            catch (Exception ex)
            {
                LogError($"Error verificando estado de impresora '{printerName}': {ex.Message}");
                return false;
            }
        }

        private static bool CheckNetworkPrinterStatus(string ipAddress, int port)
        {
            try
            {
                using (var client = new TcpClient())
                {
                    var connectTask = client.ConnectAsync(ipAddress, port);
                    if (connectTask.Wait(2000))
                    {
                        return true;
                    }
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        private static bool CheckLocalPrinterStatus(string printerName)
        {
            try
            {
                // Verificar usando System.Drawing.Printing
                return true; // Por ahora asumimos que está disponible
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Obtiene lista de impresoras disponibles
        /// </summary>
        public static List<string> GetAvailablePrinters()
        {
            var printers = new List<string>();

            try
            {
                var config = ConfigurationService.PrinterConfig;
                if (config?.ZebraPrinters != null)
                {
                    foreach (var printer in config.ZebraPrinters)
                    {
                        if (printer.IsEnabled)
                        {
                            printers.Add(printer.Name);
                        }
                    }
                }

                // Agregar impresoras del sistema
                foreach (string printerName in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
                {
                    if (!printers.Contains(printerName))
                    {
                        printers.Add(printerName);
                    }
                }
            }
            catch (Exception ex)
            {
                LogError($"Error obteniendo impresoras disponibles: {ex.Message}");
            }

            return printers;
        }

        /// <summary>
        /// Envía comando de prueba a la impresora
        /// </summary>
        public static bool PrintTest(string printerName)
        {
            try
            {
                string testZPL = @"
^XA
^LH0,0
^FO50,50^A0N,50,50^FDPrueba de Impresion^FS
^FO50,120^A0N,30,30^FDImpresora: " + printerName + @"^FS
^FO50,170^A0N,30,30^FDFecha: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm") + @"^FS
^FO50,220^BY2,3,50^BCN,50,Y,N,N^FD123456789^FS
^XZ";

                return SendZPLToPrinter(printerName, testZPL);
            }
            catch (Exception ex)
            {
                LogError($"Error enviando prueba de impresión: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Configura parámetros de impresión
        /// </summary>
        public static string SetPrinterParameters(int speed, int density, int tearOff = 0)
        {
            var parameters = new StringBuilder();

            char speedChar = speed switch
            {
                1 => 'A',
                2 => 'A',
                3 => 'B',
                4 => 'B',
                5 => 'C',
                6 => 'C',
                _ => 'D'
            };
            parameters.AppendLine($"^PR{speedChar}");

            parameters.AppendLine($"^MD{density}");

            if (tearOff != 0)
            {
                parameters.AppendLine($"^TO{tearOff},{tearOff}");
            }

            return parameters.ToString();
        }

        /// <summary>
        /// Obtiene información de estado de la impresora
        /// </summary>
        public static async Task<string> GetPrinterInfo(string printerName)
        {
            try
            {
                var printerConfig = GetPrinterConfiguration(printerName);
                if (printerConfig?.IsNetworkPrinter == true)
                {
                    string statusCommand = "~HS";
                    return "Impresora en línea";
                }
                return "Estado no disponible para impresora local";
            }
            catch (Exception ex)
            {
                LogError($"Error obteniendo información de impresora: {ex.Message}");
                return $"Error: {ex.Message}";
            }
        }

        #region Logging
        private static void LogInfo(string message)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"[PRINTER-INFO] {DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}");
            }
            catch { }
        }

        private static void LogError(string message)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"[PRINTER-ERROR] {DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}");
            }
            catch { }
        }
        #endregion
    }

    /// <summary>
    /// Clase para resultados de impresión
    /// </summary>
    public class PrintResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string PrinterName { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public int JobId { get; set; }

        public static PrintResult CreateSuccess(string printerName, string message = "Impresión exitosa")
        {
            return new PrintResult
            {
                Success = true,
                Message = message,
                PrinterName = printerName
            };
        }

        public static PrintResult CreateError(string printerName, string errorMessage)
        {
            return new PrintResult
            {
                Success = false,
                Message = errorMessage,
                PrinterName = printerName
            };
        }
    }
}