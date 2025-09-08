using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace EtiquetasApp.Models
{
    public class EtiquetaData
    {
        // Datos básicos de la etiqueta
        [StringLength(50, ErrorMessage = "El PartId no puede exceder 50 caracteres")]
        public string PartId { get; set; }

        [StringLength(50, ErrorMessage = "UPC no puede exceder 50 caracteres")]
        public string UPC { get; set; }

        [StringLength(50, ErrorMessage = "UPC2 no puede exceder 50 caracteres")]
        public string UPC2 { get; set; }

        [StringLength(200, ErrorMessage = "La descripción no puede exceder 200 caracteres")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "El tipo de etiqueta es requerido")]
        [StringLength(20, ErrorMessage = "El tipo de etiqueta no puede exceder 20 caracteres")]
        public string TipoEtiqueta { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor a 0")]
        public int Cantidad { get; set; } = 1;

        // Configuración de impresión
        [Range(1, 10, ErrorMessage = "La velocidad debe estar entre 1 y 10")]
        public int Velocidad { get; set; } = 4;

        [Range(1, 30, ErrorMessage = "La temperatura debe estar entre 1 y 30")]
        public int Temperatura { get; set; } = 6;

        // Datos adicionales
        [StringLength(50, ErrorMessage = "La orden de fabricación no puede exceder 50 caracteres")]
        public string OrdenFab { get; set; }

        [StringLength(50, ErrorMessage = "El SKU no puede exceder 50 caracteres")]
        public string SKU { get; set; }

        [StringLength(20, ErrorMessage = "El color no puede exceder 20 caracteres")]
        public string Color { get; set; } = "Blancas";

        [StringLength(50, ErrorMessage = "El logo no puede exceder 50 caracteres")]
        public string Logo { get; set; }

        // Campos específicos para diferentes tipos de etiquetas
        [StringLength(100, ErrorMessage = "Campo1 no puede exceder 100 caracteres")]
        public string Campo1 { get; set; }

        [StringLength(100, ErrorMessage = "Campo2 no puede exceder 100 caracteres")]
        public string Campo2 { get; set; }

        [StringLength(100, ErrorMessage = "Campo3 no puede exceder 100 caracteres")]
        public string Campo3 { get; set; }

        [StringLength(100, ErrorMessage = "Campo4 no puede exceder 100 caracteres")]
        public string Campo4 { get; set; }

        [StringLength(100, ErrorMessage = "Campo5 no puede exceder 100 caracteres")]
        public string Campo5 { get; set; }

        // Posiciones específicas para el tipo de etiqueta
        public Dictionary<string, string> Posiciones { get; set; } = new Dictionary<string, string>();

        // Datos para Garden State y etiquetas especiales
        [StringLength(50, ErrorMessage = "Bundle Quantity no puede exceder 50 caracteres")]
        public string BundleQuantity { get; set; }

        [StringLength(50, ErrorMessage = "Length no puede exceder 50 caracteres")]
        public string Length { get; set; }

        [StringLength(50, ErrorMessage = "Source no puede exceder 50 caracteres")]
        public string Source { get; set; }

        [StringLength(50, ErrorMessage = "Item no puede exceder 50 caracteres")]
        public string Item { get; set; }

        // Datos para molduras
        [StringLength(100, ErrorMessage = "Piezas por bolsa no puede exceder 100 caracteres")]
        public string PiezasPorBolsa { get; set; }

        // Configuración adicional
        public bool EtiquetaDoble { get; set; } = false;
        public bool IncluirFecha { get; set; } = false;
        public bool IncluirHora { get; set; } = false;
        public bool RotarTexto { get; set; } = false;

        // Metadatos
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public string UsuarioCreacion { get; set; }

        // Propiedades calculadas
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

        public bool EsValidaParaImpresion
        {
            get
            {
                return !string.IsNullOrEmpty(TipoEtiqueta) &&
                       Cantidad > 0 &&
                       Velocidad > 0 &&
                       Temperatura > 0 &&
                       ValidarCamposObligatoriosPorTipo();
            }
        }

        public string CantidadFormateada
        {
            get
            {
                // Para ciertos tipos de etiquetas, la cantidad se divide por 5
                var cantidadReal = TipoEtiqueta switch
                {
                    "CBCOE" => Cantidad,
                    "DUAL" => Cantidad,
                    "EAN13" => Cantidad / 5, // Se imprimen 5 por hoja
                    "MOLDURAS" => Cantidad,
                    _ => Cantidad
                };
                return cantidadReal.ToString();
            }
        }

        // Métodos de validación
        private bool ValidarCamposObligatoriosPorTipo()
        {
            return TipoEtiqueta switch
            {
                "CBCOE" => !string.IsNullOrEmpty(UPC) && !string.IsNullOrEmpty(Descripcion),
                "DUAL" => !string.IsNullOrEmpty(UPC) && !string.IsNullOrEmpty(UPC2),
                "EAN13" => !string.IsNullOrEmpty(UPC) && !string.IsNullOrEmpty(OrdenFab),
                "BICOLOR" => !string.IsNullOrEmpty(UPC),
                "GARDEN" => !string.IsNullOrEmpty(UPC) && !string.IsNullOrEmpty(Campo1),
                "MOLDURAS" => !string.IsNullOrEmpty(SKU),
                "LAQUEADO" => !string.IsNullOrEmpty(UPC),
                "I2DE5" => !string.IsNullOrEmpty(UPC),
                _ => !string.IsNullOrEmpty(UPC)
            };
        }

        public bool ValidarUPC()
        {
            if (string.IsNullOrEmpty(UPC))
                return false;

            // Validaciones específicas por tipo de etiqueta
            return TipoEtiqueta switch
            {
                "EAN13" => ValidarEAN13(UPC),
                "DUAL" => ValidarUPCGeneral(UPC) && (!string.IsNullOrEmpty(UPC2) && ValidarUPCGeneral(UPC2)),
                _ => ValidarUPCGeneral(UPC)
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

            return codigo.All(c => char.IsLetterOrDigit(c) || c == '-' || c == '.');
        }

        // Métodos de utilidad
        public void CargarPosicionesPorTipo()
        {
            Posiciones.Clear();

            switch (TipoEtiqueta)
            {
                case "CBCOE":
                    CargarPosicionesCBCOE();
                    break;
                case "DUAL":
                    CargarPosicionesDual();
                    break;
                case "EAN13":
                    CargarPosicionesEAN13();
                    break;
                default:
                    CargarPosicionesGenericas();
                    break;
            }
        }

        private void CargarPosicionesCBCOE()
        {
            Posiciones["Posicion1"] = "4";
            Posiciones["Posicion2"] = "148";
            Posiciones["Posicion3"] = "292";
            Posiciones["Posicion4"] = "436";
            Posiciones["Posicion5"] = "580";
        }

        private void CargarPosicionesDual()
        {
            Posiciones["Posicion1"] = "17";
            Posiciones["Posicion2"] = "185";
            Posiciones["Posicion3"] = "354";
            Posiciones["Posicion4"] = "522";
            Posiciones["Posicion5"] = "689";
        }

        private void CargarPosicionesEAN13()
        {
            Posiciones["Posicion1"] = "835";
            Posiciones["Posicion2"] = "695";
            Posiciones["Posicion3"] = "550";
            Posiciones["Posicion4"] = "406";
            Posiciones["Posicion5"] = "262";
        }

        private void CargarPosicionesGenericas()
        {
            Posiciones["Posicion1"] = "100";
            Posiciones["Posicion2"] = "250";
            Posiciones["Posicion3"] = "400";
            Posiciones["Posicion4"] = "550";
            Posiciones["Posicion5"] = "700";
        }

        public void ActualizarPosicion(string posicion, string valor)
        {
            if (Posiciones.ContainsKey(posicion))
                Posiciones[posicion] = valor;
            else
                Posiciones.Add(posicion, valor);
        }

        public string ObtenerPosicion(string posicion)
        {
            return Posiciones.ContainsKey(posicion) ? Posiciones[posicion] : "0";
        }

        public void ConfigurarParaGardenState(string bundleQty, string length, string source, string item)
        {
            BundleQuantity = bundleQty;
            Length = length;
            Source = source;
            Item = item;
        }

        public void ConfigurarParaMolduras(string sku, string piezasPorBolsa)
        {
            SKU = sku;
            PiezasPorBolsa = piezasPorBolsa;
        }

        public EtiquetaData Clonar()
        {
            return new EtiquetaData
            {
                PartId = this.PartId,
                UPC = this.UPC,
                UPC2 = this.UPC2,
                Descripcion = this.Descripcion,
                TipoEtiqueta = this.TipoEtiqueta,
                Cantidad = this.Cantidad,
                Velocidad = this.Velocidad,
                Temperatura = this.Temperatura,
                OrdenFab = this.OrdenFab,
                SKU = this.SKU,
                Color = this.Color,
                Logo = this.Logo,
                Campo1 = this.Campo1,
                Campo2 = this.Campo2,
                Campo3 = this.Campo3,
                Campo4 = this.Campo4,
                Campo5 = this.Campo5,
                BundleQuantity = this.BundleQuantity,
                Length = this.Length,
                Source = this.Source,
                Item = this.Item,
                PiezasPorBolsa = this.PiezasPorBolsa,
                EtiquetaDoble = this.EtiquetaDoble,
                IncluirFecha = this.IncluirFecha,
                IncluirHora = this.IncluirHora,
                RotarTexto = this.RotarTexto,
                Posiciones = new Dictionary<string, string>(this.Posiciones)
            };
        }

        public override string ToString()
        {
            var desc = !string.IsNullOrEmpty(Descripcion) ? Descripcion : PartId;
            return $"{TipoEtiquetaDescripcion} - {desc} ({Cantidad} pcs)";
        }

        // Constructor
        public EtiquetaData()
        {
            FechaCreacion = DateTime.Now;
            Cantidad = 1;
            Velocidad = 4;
            Temperatura = 6;
            Color = "Blancas";
            Posiciones = new Dictionary<string, string>();
        }
    }
}