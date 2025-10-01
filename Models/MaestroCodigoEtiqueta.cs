using System;
using System.ComponentModel.DataAnnotations;

namespace EtiquetasApp.Models
{
    // Clase principal con todas las propiedades necesarias
    public class MaestroCodigoEtiqueta
    {
        // Propiedades base (las que estaban faltando)
        [Key]
        [Required(ErrorMessage = "El Part ID es requerido")]
        [StringLength(50, ErrorMessage = "El Part ID no puede exceder 50 caracteres")]
        public string PartId { get; set; }

        [StringLength(50, ErrorMessage = "UPC1 no puede exceder 50 caracteres")]
        public string UPC1 { get; set; }

        [StringLength(50, ErrorMessage = "UPC2 no puede exceder 50 caracteres")]
        public string UPC2 { get; set; }

        [StringLength(200, ErrorMessage = "La descripción no puede exceder 200 caracteres")]
        public string Descripcion { get; set; }

        [StringLength(20, ErrorMessage = "El tipo de etiqueta no puede exceder 20 caracteres")]
        public string TipoEtiqueta { get; set; }

        [StringLength(20, ErrorMessage = "El color no puede exceder 20 caracteres")]
        public string ColorEtiqueta { get; set; } = "Blancas";

        public bool RequiereLogo { get; set; } = false;

        [StringLength(50, ErrorMessage = "El nombre del logo no puede exceder 50 caracteres")]
        public string NombreLogo { get; set; }

        [StringLength(50, ErrorMessage = "El usuario no puede exceder 50 caracteres")]
        public string UsuarioCreacion { get; set; }

        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        // Propiedades adicionales
        public bool Activo { get; set; } = true;

        public DateTime? FechaModificacion { get; set; }

        [StringLength(50, ErrorMessage = "El usuario no puede exceder 50 caracteres")]
        public string UsuarioModificacion { get; set; }

        [StringLength(500, ErrorMessage = "Las observaciones no pueden exceder 500 caracteres")]
        public string Observaciones { get; set; }

        // Renombrado para evitar conflicto con el enum
        [Range(1, 10, ErrorMessage = "La velocidad debe estar entre 1 y 10")]
        public int VelocidadImpresionConfig { get; set; } = 4;

        // Propiedad de temperatura como int
        [Range(1, 30, ErrorMessage = "La temperatura debe estar entre 1 y 30")]
        public int TemperaturaImpresion { get; set; } = 6;

        // Propiedad calculada
        public string EstadoDescripcion
        {
            get
            {
                if (!Activo) return "Inactivo";
                if (ValidarConfiguracion()) return "Válido";
                return "Requiere Corrección";
            }
        }

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

        public bool ValidarConfiguracion()
        {
            // Validaciones específicas para impresión
            if (VelocidadImpresionConfig < 1 || VelocidadImpresionConfig > 10)
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
            if (VelocidadImpresionConfig > 6)
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

        public string ConfiguracionImpresion => $"Vel: {VelocidadImpresionConfig}, Temp: {TemperaturaImpresion}°C";

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
                VelocidadImpresionConfig = this.VelocidadImpresionConfig,
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
                Velocidad = this.VelocidadImpresionConfig,
                Temperatura = this.TemperaturaImpresion,
                Logo = this.RequiereLogo ? this.NombreLogo : null
            };
        }

        // Actualización de configuración de impresión
        public void ActualizarConfiguracionImpresion(int velocidad, int temperatura, string usuario = "")
        {
            if (velocidad < 1 || velocidad > 10)
                throw new ArgumentException("La velocidad debe estar entre 1 y 10");
            if (temperatura < 1 || temperatura > 30)
                throw new ArgumentException("La temperatura debe estar entre 1 y 30");

            VelocidadImpresionConfig = velocidad;
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
}