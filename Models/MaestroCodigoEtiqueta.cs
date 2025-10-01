using System;
using System.ComponentModel.DataAnnotations;

namespace EtiquetasApp.Models
{
    // Extensión del modelo existente MaestroCodigoEtiqueta
    public partial class MaestroCodigoEtiqueta
    {
        // Propiedad faltante
        public bool Activo { get; set; } // Se agregó esta propiedad

        public DateTime FechaModificacion { get; set; } // Se agregó esta propiedad

        public string UsuarioModificacion { get; set; } // Se agregó esta propiedad

        public string Observaciones { get; set; } // Se agregó esta propiedad

        // Métodos de activación/desactivación
        public void Activar(string usuario = "")
        {
            Activo = true;
            FechaModificacion = DateTime.Now;
            if (!string.IsNullOrEmpty(usuario))
                UsuarioModificacion = usuario;
        }

        public void Desactivar(string usuario = "", string razon = "")
        {
            Activo = false;
            FechaModificacion = DateTime.Now;
            if (!string.IsNullOrEmpty(usuario))
                UsuarioModificacion = usuario;
            if (!string.IsNullOrEmpty(razon))
                Observaciones = $"{Observaciones}\n[{DateTime.Now:dd/MM/yyyy}] Desactivado: {razon}".Trim();
        }

        // Validaciones adicionales
        public override bool ValidarConfiguracion()
        {
            // Validaciones base del modelo padre
            if (!base.ValidarConfiguracion())
                return false;

            // Validaciones específicas para impresión
            if (VelocidadImpresion < 1 || VelocidadImpresion > 10)
                return false;

            if (TemperaturaImpresion < 1 || TemperaturaImpresion > 30)
                return false;

            if (RequiereLogo && string.IsNullOrEmpty(NombreLogo))
                return false;

            // Validación específica por tipo de etiqueta
            if (!ValidarConfiguracionPorTipo())
                return false;

            return true;
        }

        private bool ValidarConfiguracionPorTipo()
        {
            switch (TipoEtiqueta?.ToUpper())
            {
                case "MOLDURAS":
                    return ValidarConfiguracionMolduras();
                case "EAN13":
                    return ValidarConfiguracionEAN13();
                case "I2DE5":
                    return ValidarConfiguracionI2DE5();
                case "CBCOE":
                    return ValidarConfiguracionCBCOE();
                case "DUAL":
                    return ValidarConfiguracionDual();
                default:
                    return true; // Tipos desconocidos se consideran válidos por defecto
            }
        }

        private bool ValidarConfiguracionMolduras()
        {
            // Las molduras requieren color específico
            if (string.IsNullOrEmpty(ColorEtiqueta) || ColorEtiqueta == "N/A")
                return false;

            // UPC debe tener longitud apropiada
            if (string.IsNullOrEmpty(UPC1) || UPC1.Length < 8)
                return false;

            return true;
        }

        private bool ValidarConfiguracionEAN13()
        {
            // EAN13 requiere código de exactamente 13 dígitos
            if (string.IsNullOrEmpty(UPC1) || UPC1.Length != 13)
                return false;

            // Debe ser solo números
            if (!long.TryParse(UPC1, out _))
                return false;

            // Color debe ser N/A para EAN13
            return ColorEtiqueta == "N/A" || string.IsNullOrEmpty(ColorEtiqueta);
        }

        private bool ValidarConfiguracionI2DE5()
        {
            // I2DE5 requiere código de longitud par
            if (string.IsNullOrEmpty(UPC1) || UPC1.Length % 2 != 0)
                return false;

            // Debe ser solo números
            if (!long.TryParse(UPC1, out _))
                return false;

            return true;
        }

        private bool ValidarConfiguracionCBCOE()
        {
            // C/BCO-E generalmente usa etiquetas blancas
            if (ColorEtiqueta != "Blancas" && !string.IsNullOrEmpty(ColorEtiqueta))
                return false;

            // Velocidad recomendada para C/BCO-E
            if (VelocidadImpresion > 6)
                return false;

            return true;
        }

        private bool ValidarConfiguracionDual()
        {
            // Similar a C/BCO-E
            return ValidarConfiguracionCBCOE();
        }

        // Propiedades calculadas adicionales
        public bool EsValidoParaImpresion => Activo && ValidarConfiguracion();

        public string TipoEtiquetaDescripcion
        {
            get
            {
                return TipoEtiqueta?.ToUpper() switch
                {
                    "MOLDURAS" => "Molduras con Color",
                    "EAN13" => "Código EAN-13",
                    "I2DE5" => "Interleaved 2 of 5",
                    "CBCOE" => "C/BCO-E (5 por hoja)",
                    "DUAL" => "Dual (5 por hoja)",
                    _ => TipoEtiqueta ?? "Desconocido"
                };
            }
        }

        public string ConfiguracionImpresion => $"Vel: {VelocidadImpresion}, Temp: {TemperaturaImpresion}°C";

        public int EtiquetasPorHoja
        {
            get
            {
                return TipoEtiqueta?.ToUpper() switch
                {
                    "CBCOE" => 5,
                    "DUAL" => 5,
                    "MOLDURAS" => 3,
                    "EAN13" => 1,
                    "I2DE5" => 1,
                    _ => 1
                };
            }
        }

        // Métodos de utilidad para códigos
        public string GenerarCodigoVerificacion()
        {
            if (string.IsNullOrEmpty(UPC1) || UPC1.Length < 8)
                return UPC1;

            // Generar dígito verificador para códigos EAN
            if (TipoEtiqueta?.ToUpper() == "EAN13")
            {
                return GenerarDigitoVerificadorEAN13();
            }

            return UPC1;
        }

        private string GenerarDigitoVerificadorEAN13()
        {
            if (UPC1.Length != 12 && UPC1.Length != 13)
                return UPC1;

            var codigo = UPC1.Length == 13 ? UPC1.Substring(0, 12) : UPC1;
            var suma = 0;

            for (int i = 0; i < codigo.Length; i++)
            {
                if (int.TryParse(codigo[i].ToString(), out int digito))
                {
                    suma += (i % 2 == 0) ? digito : digito * 3;
                }
            }

            var digitoVerificacion = (10 - (suma % 10)) % 10;
            return codigo + digitoVerificacion;
        }

        // Método para clonar maestro
        public MaestroCodigoEtiqueta Clonar(string nuevoPartId, string usuario = "")
        {
            if (string.IsNullOrEmpty(nuevoPartId))
                throw new ArgumentException("El nuevo Part ID es requerido");

            return new MaestroCodigoEtiqueta
            {
                PartId = nuevoPartId,
                UPC1 = this.UPC1,
                UPC2 = this.UPC2,
                Descripcion = this.Descripcion,
                TipoEtiqueta = this.TipoEtiqueta,
                ColorEtiqueta = this.ColorEtiqueta,
                VelocidadImpresion = this.VelocidadImpresion,
                TemperaturaImpresion = this.TemperaturaImpresion,
                RequiereLogo = this.RequiereLogo,
                NombreLogo = this.NombreLogo,
                Observaciones = $"Clonado de {this.PartId}",
                UsuarioCreacion = usuario,
                FechaCreacion = DateTime.Now,
                Activo = true
            };
        }

        // Conversión a datos de etiqueta para impresión
        public EtiquetaData ConvertirAEtiquetaData(int cantidad = 1)
        {
            return new EtiquetaData
            {
                PartId = this.PartId,
                UPC = this.UPC1,
                UPC2 = this.UPC2,
                Descripcion = this.Descripcion,
                TipoEtiqueta = this.TipoEtiqueta,
                Color = this.ColorEtiqueta,
                Cantidad = cantidad,
                Velocidad = this.VelocidadImpresion,
                Temperatura = this.TemperaturaImpresion,
                Logo = this.RequiereLogo ? this.NombreLogo : null,
                Factor = "", // Se puede agregar si es necesario
                FechaImpresion = DateTime.Now
            };
        }

        // Actualización de configuración de impresión
        public void ActualizarConfiguracionImpresion(int velocidad, int temperatura, string usuario = "")
        {
            if (velocidad < 1 || velocidad > 10)
                throw new ArgumentException("La velocidad debe estar entre 1 y 10");
            if (temperatura < 1 || temperatura > 30)
                throw new ArgumentException("La temperatura debe estar entre 1 y 30");

            VelocidadImpresion = velocidad;
            TemperaturaImpresion = temperatura;
            FechaModificacion = DateTime.Now;
            if (!string.IsNullOrEmpty(usuario))
                UsuarioModificacion = usuario;
        }

        // Actualización de códigos UPC
        public void ActualizarUPC(string upc1, string upc2 = "", string usuario = "")
        {
            if (string.IsNullOrEmpty(upc1))
                throw new ArgumentException("UPC1 es requerido");

            UPC1 = upc1;
            UPC2 = upc2;
            FechaModificacion = DateTime.Now;
            if (!string.IsNullOrEmpty(usuario))
                UsuarioModificacion = usuario;
        }

        // Configuración de logo
        public void ConfigurarLogo(bool requiere, string nombreLogo = "", string usuario = "")
        {
            RequiereLogo = requiere;
            NombreLogo = requiere ? nombreLogo : "";
            FechaModificacion = DateTime.Now;
            if (!string.IsNullOrEmpty(usuario))
                UsuarioModificacion = usuario;
        }

        // Override del ToString para mejor visualización
        public override string ToString()
        {
            return $"{PartId} - {TipoEtiquetaDescripcion} ({EstadoDescripcion})";
        }
    }

    // Clase para datos de etiqueta en impresión
    public class EtiquetaData
    {
        public string PartId { get; set; }
        public string UPC { get; set; }
        public string UPC2 { get; set; }
        public string Descripcion { get; set; }
        public string TipoEtiqueta { get; set; }
        public string Color { get; set; }
        public int Cantidad { get; set; }
        public int Velocidad { get; set; }
        public int Temperatura { get; set; }
        public string Logo { get; set; }
        public string Factor { get; set; }
        public DateTime FechaImpresion { get; set; } = DateTime.Now;

        // Posiciones para impresión (se cargan desde configuración)
        public int Posicion1 { get; set; }
        public int Posicion2 { get; set; }
        public int Posicion3 { get; set; }
        public int Posicion4 { get; set; }
        public int Posicion5 { get; set; }

        public bool ValidarDatos()
        {
            return !string.IsNullOrEmpty(PartId) &&
                   !string.IsNullOrEmpty(UPC) &&
                   !string.IsNullOrEmpty(Descripcion) &&
                   !string.IsNullOrEmpty(TipoEtiqueta) &&
                   Cantidad > 0 &&
                   Velocidad >= 1 && Velocidad <= 10 &&
                   Temperatura >= 1 && Temperatura <= 30;
        }
    }
}