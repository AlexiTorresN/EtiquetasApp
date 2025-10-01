using System;
using System.ComponentModel.DataAnnotations;

namespace EtiquetasApp.Models
{
    public class SolicitudEtiqueta
    {
        public int IdSolicitud { get; set; }

        [Required(ErrorMessage = "El Part ID es requerido")]
        [StringLength(50, ErrorMessage = "El Part ID no puede exceder 50 caracteres")]
        public string PartId { get; set; }

        public int OrdenFab { get; set; }

        [Required(ErrorMessage = "La fecha de solicitud es requerida")]
        public DateTime FechaSolicitud { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "La fecha requerida es requerida")]
        public DateTime FechaRequerida { get; set; }

        [Required(ErrorMessage = "La cantidad pedida es requerida")]
        [Range(1, 10000, ErrorMessage = "La cantidad debe estar entre 1 y 10,000")]
        public int CantidadPedida { get; set; }

        [StringLength(20, ErrorMessage = "El color no puede exceder 20 caracteres")]
        public string Color { get; set; } = "Blancas";

        [StringLength(500, ErrorMessage = "Las observaciones no pueden exceder 500 caracteres")]
        public string Observaciones { get; set; }

        [Required(ErrorMessage = "El tipo de etiqueta es requerido")]
        [StringLength(20, ErrorMessage = "El tipo de etiqueta no puede exceder 20 caracteres")]
        public string TipoEtiqueta { get; set; }

        [StringLength(50, ErrorMessage = "El usuario no puede exceder 50 caracteres")]
        public string Usuario { get; set; }

        public int CantidadFabricada { get; set; } = 0;

        public DateTime? FechaFabricacion { get; set; }

        [StringLength(100, ErrorMessage = "El nombre de quien retira no puede exceder 100 caracteres")]
        public string NombreRetira { get; set; }

        public DateTime? FechaEntrega { get; set; }

        // Propiedades adicionales (desde JOIN con MAESTRO_COD_ETIQ)
        [StringLength(200, ErrorMessage = "La descripción no puede exceder 200 caracteres")]
        public string Descripcion { get; set; }

        [StringLength(50, ErrorMessage = "UPC1 no puede exceder 50 caracteres")]
        public string UPC1 { get; set; }

        [StringLength(50, ErrorMessage = "UPC2 no puede exceder 50 caracteres")]
        public string UPC2 { get; set; }

        // Propiedades calculadas
        public int CantidadPendiente => Math.Max(0, CantidadPedida - CantidadFabricada);

        public bool EstaCompletada => CantidadFabricada >= CantidadPedida;

        public bool EstaEntregada => FechaEntrega.HasValue;

        public string EstadoDescripcion
        {
            get
            {
                if (EstaEntregada)
                    return "Entregada";
                if (EstaCompletada)
                    return "Fabricada";
                if (CantidadFabricada > 0)
                    return "Parcial";
                return "Pendiente";
            }
        }

        public bool RequiereImpresion => CantidadPendiente > 0 && !EstaEntregada;

        public int DiasVencimiento
        {
            get
            {
                var dias = (FechaRequerida - DateTime.Now).Days;
                return dias;
            }
        }

        public bool EstaVencida => DiasVencimiento < 0;

        public bool EsUrgente => DiasVencimiento <= 1 && DiasVencimiento >= 0;

        // Validaciones de negocio
        public bool ValidarCantidades()
        {
            return CantidadPedida > 0 && CantidadPedida <= 10000 && CantidadFabricada >= 0;
        }

        public bool ValidarFechas()
        {
            return FechaRequerida >= FechaSolicitud.Date;
        }

        public bool ValidarConfiguracion()
        {
            return !string.IsNullOrEmpty(PartId) &&
                   !string.IsNullOrEmpty(TipoEtiqueta) &&
                   ValidarCantidades() &&
                   ValidarFechas();
        }

        // Métodos de utilidad
        public void MarcarComoFabricada(int cantidadFabricada, string usuario = "")
        {
            if (cantidadFabricada < 0 || cantidadFabricada > CantidadPendiente)
                throw new ArgumentException("Cantidad fabricada inválida");

            CantidadFabricada += cantidadFabricada;
            FechaFabricacion = DateTime.Now;

            if (!string.IsNullOrEmpty(usuario))
                Usuario = usuario;
        }

        public void MarcarComoEntregada(string nombreRetira, string usuario = "")
        {
            if (string.IsNullOrEmpty(nombreRetira))
                throw new ArgumentException("Debe especificar quién retira");

            if (!EstaCompletada)
                throw new InvalidOperationException("No se puede entregar una solicitud incompleta");

            NombreRetira = nombreRetira;
            FechaEntrega = DateTime.Now;

            if (!string.IsNullOrEmpty(usuario))
                Usuario = usuario;
        }

        public void ReactivarSolicitud(string usuario = "")
        {
            CantidadFabricada = 0;
            FechaFabricacion = null;
            NombreRetira = null;
            FechaEntrega = null;

            if (!string.IsNullOrEmpty(usuario))
                Usuario = usuario;
        }

        public override string ToString()
        {
            return $"{PartId} - {TipoEtiqueta} ({CantidadPendiente}/{CantidadPedida})";
        }

        public override bool Equals(object obj)
        {
            if (obj is SolicitudEtiqueta other)
                return IdSolicitud == other.IdSolicitud;
            return false;
        }

        public override int GetHashCode()
        {
            return IdSolicitud.GetHashCode();
        }
    }

    // Enumeración para tipos de etiquetas
    public enum TipoEtiqueta
    {
        MOLDURAS,
        EAN13,
        I2DE5,
        CBCOE,
        DUAL
    }

    // Enumeración para colores de etiquetas
    public enum ColorEtiqueta
    {
        Blancas,
        Rojas,
        Bicolor,
        NA
    }

    // Extensiones para las enumeraciones
    public static class TipoEtiquetaExtensions
    {
        public static string GetDisplayName(this TipoEtiqueta tipo)
        {
            return tipo switch
            {
                TipoEtiqueta.MOLDURAS => "Molduras",
                TipoEtiqueta.EAN13 => "EAN-13",
                TipoEtiqueta.I2DE5 => "Interleaved 2 of 5",
                TipoEtiqueta.CBCOE => "C/BCO-E",
                TipoEtiqueta.DUAL => "Dual",
                _ => tipo.ToString()
            };
        }

        public static string GetDescription(this TipoEtiqueta tipo)
        {
            return tipo switch
            {
                TipoEtiqueta.MOLDURAS => "Etiquetas para molduras con colores",
                TipoEtiqueta.EAN13 => "Códigos de barras EAN-13 estándar",
                TipoEtiqueta.I2DE5 => "Códigos Interleaved 2 of 5",
                TipoEtiqueta.CBCOE => "Etiquetas C/BCO-E - 5 por hoja",
                TipoEtiqueta.DUAL => "Etiquetas duales - 5 por hoja",
                _ => "Tipo de etiqueta desconocido"
            };
        }
    }

    public static class ColorEtiquetaExtensions
    {
        public static string GetDisplayName(this ColorEtiqueta color)
        {
            return color switch
            {
                ColorEtiqueta.Blancas => "Blancas",
                ColorEtiqueta.Rojas => "Rojas",
                ColorEtiqueta.Bicolor => "Bicolor",
                ColorEtiqueta.NA => "N/A",
                _ => color.ToString()
            };
        }
    }
}