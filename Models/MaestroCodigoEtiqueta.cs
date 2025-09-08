using System;
using System.ComponentModel.DataAnnotations;

namespace EtiquetasApp.Models
{
    public class MaestroCodigoEtiqueta
    {
        [Required(ErrorMessage = "El ID de la parte es requerido")]
        [StringLength(50, ErrorMessage = "El ID de la parte no puede exceder 50 caracteres")]
        public string PartId { get; set; }

        [Required(ErrorMessage = "El UPC1 es requerido")]
        [StringLength(50, ErrorMessage = "UPC1 no puede exceder 50 caracteres")]
        public string UPC1 { get; set; }

        [StringLength(50, ErrorMessage = "UPC2 no puede exceder 50 caracteres")]
        public string UPC2 { get; set; }

        [Required(ErrorMessage = "La descripción es requerida")]
        [StringLength(200, ErrorMessage = "La descripción no puede exceder 200 caracteres")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "El tipo de etiqueta es requerido")]
        [StringLength(20, ErrorMessage = "El tipo de etiqueta no puede exceder 20 caracteres")]
        public string TipoEtiqueta { get; set; }

        public bool Activo { get; set; } = true;

        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        public DateTime? FechaModificacion { get; set; }

        [StringLength(50, ErrorMessage = "El usuario no puede exceder 50 caracteres")]
        public string UsuarioCreacion { get; set; }

        [StringLength(50, ErrorMessage = "El usuario no puede exceder 50 caracteres")]
        public string UsuarioModificacion { get; set; }

        [StringLength(500, ErrorMessage = "Las observaciones no pueden exceder 500 caracteres")]
        public string Observaciones { get; set; }

        // Campos adicionales para configuración de etiquetas
        [StringLength(20, ErrorMessage = "El color no puede exceder 20 caracteres")]
        public string ColorEtiqueta { get; set; } = "Blancas";

        public int VelocidadImpresion { get; set; } = 4;

        public int TemperaturaImpresion { get; set; } = 6;

        public bool RequiereLogo { get; set; } = false;

        [StringLength(50, ErrorMessage = "El nombre del logo no puede exceder 50 caracteres")]
        public string NombreLogo { get; set; }

        // Propiedades para posiciones específicas por tipo de etiqueta
        public string PosicionesPersonalizadas { get; set; } // JSON con posiciones específicas

        // Propiedades calculadas
        public string EstadoDescripcion => Activo ? "Activo" : "Inactivo";

        public bool TieneDualUPC => !string.IsNullOrEmpty(UPC2);

        public string TipoEtiquetaDescripcion
        {
            get
            {
                return TipoEtiqueta switch
                {
                    "CBCOE" => "C/BCO-E",
                    "DUAL" => "Dual",
                    "EAN13" => "EAN 13",
                    "BICOLOR" => "Bicolor",
                    "GARDEN" => "Garden State",
                    "MOLDURAS" => "Molduras",
                    "LAQUEADO" => "Laqueado",
                    "I2DE5" => "I 2 de 5",
                    _ => TipoEtiqueta
                };
            }
        }

        public bool EsValidoParaImpresion
        {
            get
            {
                return Activo &&
                       !string.IsNullOrEmpty(UPC1) &&
                       !string.IsNullOrEmpty(TipoEtiqueta) &&
                       VelocidadImpresion > 0 &&
                       TemperaturaImpresion > 0;
            }
        }

        // Métodos de validación
        public bool ValidarUPC()
        {
            // Validar formato de UPC según el tipo de etiqueta
            return TipoEtiqueta switch
            {
                "EAN13" => ValidarEAN13(UPC1),
                "CBCOE" => ValidarUPCGeneral(UPC1),
                "DUAL" => ValidarUPCGeneral(UPC1) && (!TieneDualUPC || ValidarUPCGeneral(UPC2)),
                _ => ValidarUPCGeneral(UPC1)
            };
        }

        private bool ValidarEAN13(string codigo)
        {
            if (string.IsNullOrEmpty(codigo) || codigo.Length != 13)
                return false;

            return codigo.All(char.IsDigit);
        }

        private bool ValidarUPCGeneral(string codigo)
        {
            if (string.IsNullOrEmpty(codigo))
                return false;

            // Validar que contenga solo caracteres válidos para códigos de barras
            return codigo.All(c => char.IsLetterOrDigit(c) || c == '-' || c == '.');
        }

        public bool ValidarConfiguracion()
        {
            if (VelocidadImpresion < 1 || VelocidadImpresion > 10)
                return false;

            if (TemperaturaImpresion < 1 || TemperaturaImpresion > 30)
                return false;

            if (RequiereLogo && string.IsNullOrEmpty(NombreLogo))
                return false;

            return true;
        }

        // Métodos de utilidad
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

        public void ConfigurarLogo(bool requiere, string nombreLogo = "", string usuario = "")
        {
            RequiereLogo = requiere;
            NombreLogo = requiere ? nombreLogo : "";
            FechaModificacion = DateTime.Now;
            if (!string.IsNullOrEmpty(usuario))
                UsuarioModificacion = usuario;
        }

        public EtiquetaData ConvertirAEtiquetaData(int cantidad = 1)
        {
            return new EtiquetaData
            {
                PartId = this.PartId,
                UPC = this.UPC1,
                UPC2 = this.UPC2,
                Descripcion = this.Descripcion,
                TipoEtiqueta = this.TipoEtiqueta,
                Cantidad = cantidad,
                Velocidad = this.VelocidadImpresion,
                Temperatura = this.TemperaturaImpresion,
                Logo = this.RequiereLogo ? this.NombreLogo : "",
                Color = this.ColorEtiqueta
            };
        }

        public MaestroCodigoEtiqueta ClonarPara(string nuevoPartId, string usuario = "")
        {
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
                PosicionesPersonalizadas = this.PosicionesPersonalizadas,
                UsuarioCreacion = usuario,
                Activo = true
            };
        }

        public override string ToString()
        {
            return $"{PartId} - {Descripcion} ({TipoEtiquetaDescripcion}) - {EstadoDescripcion}";
        }

        // Constructor
        public MaestroCodigoEtiqueta()
        {
            FechaCreacion = DateTime.Now;
            Activo = true;
            ColorEtiqueta = "Blancas";
            VelocidadImpresion = 4;
            TemperaturaImpresion = 6;
        }
    }
}