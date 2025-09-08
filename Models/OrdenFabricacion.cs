using System;
using System.ComponentModel.DataAnnotations;

namespace EtiquetasApp.Models
{
    public class OrdenFabricacion
    {
        [Required(ErrorMessage = "El ID de la orden de fabricación es requerido")]
        [StringLength(50, ErrorMessage = "El ID de la orden no puede exceder 50 caracteres")]
        public string OrdenFab { get; set; }

        [Required(ErrorMessage = "El ID de la parte es requerido")]
        [StringLength(50, ErrorMessage = "El ID de la parte no puede exceder 50 caracteres")]
        public string PartId { get; set; }

        [Required(ErrorMessage = "La descripción es requerida")]
        [StringLength(200, ErrorMessage = "La descripción no puede exceder 200 caracteres")]
        public string Descripcion { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor a 0")]
        public int Cantidad { get; set; }

        public DateTime FechaInicio { get; set; }

        public DateTime FechaRequerida { get; set; }

        public DateTime? FechaCompletada { get; set; }

        [StringLength(20, ErrorMessage = "El estado no puede exceder 20 caracteres")]
        public string Estado { get; set; } = "PROGRAMADA";

        [StringLength(50, ErrorMessage = "El recurso no puede exceder 50 caracteres")]
        public string Recurso { get; set; } = "ETIQ";

        [StringLength(20, ErrorMessage = "El schedule ID no puede exceder 20 caracteres")]
        public string ScheduleId { get; set; } = "STANDARD";

        public int Prioridad { get; set; } = 5;

        public bool RequiereEtiquetas { get; set; } = true;

        public bool TieneCodigoEtiqueta { get; set; }

        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        public DateTime? FechaModificacion { get; set; }

        // Propiedades calculadas
        public int DiasParaInicio
        {
            get
            {
                return (FechaInicio.Date - DateTime.Now.Date).Days;
            }
        }

        public int DiasParaEntrega
        {
            get
            {
                return (FechaRequerida.Date - DateTime.Now.Date).Days;
            }
        }

        public bool EstaVencida => FechaRequerida < DateTime.Now && !EstaCompletada;

        public bool EstaCompletada => FechaCompletada.HasValue;

        public bool EsUrgente => DiasParaEntrega <= 2 && !EstaCompletada;

        public string EstadoDescripcion
        {
            get
            {
                if (EstaCompletada) return "Completada";
                if (EstaVencida) return "Vencida";
                if (DiasParaInicio <= 0) return "En Proceso";
                if (DiasParaInicio <= 1) return "Por Iniciar";
                return "Programada";
            }
        }

        public string PrioridadDescripcion
        {
            get
            {
                return Prioridad switch
                {
                    1 => "Muy Alta",
                    2 => "Alta",
                    3 => "Media-Alta",
                    4 => "Media",
                    5 => "Normal",
                    6 => "Media-Baja",
                    7 => "Baja",
                    8 => "Muy Baja",
                    _ => "Normal"
                };
            }
        }

        // Métodos de validación
        public bool ValidarFechas()
        {
            if (FechaInicio > FechaRequerida)
                return false;

            if (FechaCompletada.HasValue && FechaCompletada < FechaInicio)
                return false;

            return true;
        }

        public bool PuedeGenerarSolicitudEtiquetas()
        {
            return RequiereEtiquetas && !TieneCodigoEtiqueta && DiasParaEntrega > 0;
        }

        // Métodos de utilidad
        public void CompletarOrden()
        {
            Estado = "COMPLETADA";
            FechaCompletada = DateTime.Now;
            FechaModificacion = DateTime.Now;
        }

        public void IniciarOrden()
        {
            Estado = "EN_PROCESO";
            FechaModificacion = DateTime.Now;
        }

        public void CancelarOrden()
        {
            Estado = "CANCELADA";
            FechaModificacion = DateTime.Now;
        }

        public void PausarOrden()
        {
            Estado = "PAUSADA";
            FechaModificacion = DateTime.Now;
        }

        public void ReanudarOrden()
        {
            Estado = "EN_PROCESO";
            FechaModificacion = DateTime.Now;
        }

        public void ActualizarPrioridad(int nuevaPrioridad)
        {
            if (nuevaPrioridad < 1 || nuevaPrioridad > 10)
                throw new ArgumentException("La prioridad debe estar entre 1 y 10");

            Prioridad = nuevaPrioridad;
            FechaModificacion = DateTime.Now;
        }

        public void MarcarComoConEtiquetas()
        {
            TieneCodigoEtiqueta = true;
            FechaModificacion = DateTime.Now;
        }

        public SolicitudEtiqueta GenerarSolicitudEtiqueta(string tipoEtiqueta = "", string observaciones = "")
        {
            if (!PuedeGenerarSolicitudEtiquetas())
                throw new InvalidOperationException("No se puede generar solicitud de etiquetas para esta orden");

            return new SolicitudEtiqueta
            {
                OrdenFab = this.OrdenFab,
                Descripcion = this.Descripcion,
                CantidadPedida = this.Cantidad,
                FechaRequerida = this.FechaRequerida.AddDays(-1), // Un día antes de la fecha requerida
                TipoEtiqueta = tipoEtiqueta,
                Observaciones = observaciones,
                Color = "Blancas" // Valor por defecto
            };
        }

        public override string ToString()
        {
            return $"{OrdenFab} - {Descripcion} ({Cantidad} pcs) - {EstadoDescripcion}";
        }

        // Constructor
        public OrdenFabricacion()
        {
            FechaCreacion = DateTime.Now;
            Estado = "PROGRAMADA";
            Recurso = "ETIQ";
            ScheduleId = "STANDARD";
            Prioridad = 5;
            RequiereEtiquetas = true;
        }
    }
}