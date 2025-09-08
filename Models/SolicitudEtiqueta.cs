using System;
using System.ComponentModel.DataAnnotations;

namespace EtiquetasApp.Models
{
    public class SolicitudEtiqueta
    {
        public int IdSolicitud { get; set; }

        [Required(ErrorMessage = "La Orden de Fabricación es requerida")]
        [StringLength(50, ErrorMessage = "La Orden de Fabricación no puede exceder 50 caracteres")]
        public string OrdenFab { get; set; }

        [Required(ErrorMessage = "La descripción es requerida")]
        [StringLength(200, ErrorMessage = "La descripción no puede exceder 200 caracteres")]
        public string Descripcion { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "La cantidad pedida debe ser mayor a 0")]
        public int CantidadPedida { get; set; }

        public int CantidadFabricada { get; set; } = 0;

        [StringLength(50, ErrorMessage = "El color no puede exceder 50 caracteres")]
        public string Color { get; set; }

        [StringLength(500, ErrorMessage = "Las observaciones no pueden exceder 500 caracteres")]
        public string Observaciones { get; set; }

        public DateTime FechaSolicitud { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "La fecha requerida es obligatoria")]
        public DateTime FechaRequerida { get; set; }

        public DateTime? FechaFabricacion { get; set; }

        [StringLength(50, ErrorMessage = "UPC1 no puede exceder 50 caracteres")]
        public string UPC1 { get; set; }

        [StringLength(50, ErrorMessage = "UPC2 no puede exceder 50 caracteres")]
        public string UPC2 { get; set; }

        [StringLength(20, ErrorMessage = "El tipo de etiqueta no puede exceder 20 caracteres")]
        public string TipoEtiqueta { get; set; }

        public bool Activo { get; set; } = true;

        public string UsuarioSolicita { get; set; }

        public DateTime? FechaModificacion { get; set; }

        // Propiedades calculadas
        public int CantidadPendiente => CantidadPedida - CantidadFabricada;

        public bool EstaCompleta => CantidadFabricada >= CantidadPedida;

        public string EstadoSolicitud
        {
            get
            {
                if (!Activo) return "Inactiva";
                if (EstaCompleta) return "Completada";
                if (CantidadFabricada > 0) return "En Proceso";
                if (FechaRequerida < DateTime.Now.Date) return "Vencida";
                return "Pendiente";
            }
        }

        public int DiasParaVencimiento
        {
            get
            {
                var dias = (FechaRequerida.Date - DateTime.Now.Date).Days;
                return dias;
            }
        }

        public bool EsUrgente => DiasParaVencimiento <= 1 && !EstaCompleta;

        // Métodos de validación
        public bool ValidarFechas()
        {
            return FechaRequerida >= DateTime.Now.Date;
        }

        public bool ValidarCantidades()
        {
            return CantidadPedida > 0 && CantidadFabricada >= 0 && CantidadFabricada <= CantidadPedida;
        }

        // Métodos de utilidad
        public void ActualizarCantidadFabricada(int cantidad)
        {
            if (cantidad < 0) throw new ArgumentException("La cantidad fabricada no puede ser negativa");
            if (cantidad > CantidadPedida) throw new ArgumentException("La cantidad fabricada no puede exceder la cantidad pedida");

            CantidadFabricada = cantidad;
            if (cantidad > 0 && !FechaFabricacion.HasValue)
            {
                FechaFabricacion = DateTime.Now;
            }
            FechaModificacion = DateTime.Now;
        }

        public void CompletarSolicitud()
        {
            CantidadFabricada = CantidadPedida;
            FechaFabricacion = DateTime.Now;
            FechaModificacion = DateTime.Now;
        }

        public void ReactivarSolicitud()
        {
            Activo = true;
            FechaModificacion = DateTime.Now;
        }

        public void DesactivarSolicitud()
        {
            Activo = false;
            FechaModificacion = DateTime.Now;
        }

        public override string ToString()
        {
            return $"OF: {OrdenFab} - {Descripcion} ({CantidadFabricada}/{CantidadPedida})";
        }

        // Constructor
        public SolicitudEtiqueta()
        {
            FechaSolicitud = DateTime.Now;
            FechaRequerida = DateTime.Now.AddDays(1);
            Activo = true;
        }
    }
}