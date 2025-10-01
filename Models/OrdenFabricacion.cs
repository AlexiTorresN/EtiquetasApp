using System;
using System.ComponentModel.DataAnnotations;

namespace EtiquetasApp.Models
{
    public class OrdenFabricacion
    {
        [Required(ErrorMessage = "El Base ID es requerido")]
        [StringLength(50, ErrorMessage = "El Base ID no puede exceder 50 caracteres")]
        public string BaseId { get; set; }

        [Required(ErrorMessage = "El Part ID es requerido")]
        [StringLength(50, ErrorMessage = "El Part ID no puede exceder 50 caracteres")]
        public string PartId { get; set; }

        [StringLength(200, ErrorMessage = "La descripción no puede exceder 200 caracteres")]
        public string Descripcion { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor a 0")]
        public int Cantidad { get; set; }

        [StringLength(20, ErrorMessage = "El estado no puede exceder 20 caracteres")]
        public string Estado { get; set; } = "ACTIVE";

        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        public DateTime? FechaInicio { get; set; }

        public DateTime? FechaFin { get; set; }

        public DateTime? FechaProgramada { get; set; }

        [StringLength(50, ErrorMessage = "El usuario no puede exceder 50 caracteres")]
        public string UsuarioCreacion { get; set; }

        [StringLength(500, ErrorMessage = "Las observaciones no pueden exceder 500 caracteres")]
        public string Observaciones { get; set; }

        public int Prioridad { get; set; } = 5; // 1 = Alta, 5 = Normal, 10 = Baja

        // Propiedades relacionadas con etiquetas
        public bool RequiereEtiquetas { get; set; } = true;

        public bool TieneCodigoEtiqueta { get; set; } = false;

        public int CantidadEtiquetasSolicitadas { get; set; } = 0;

        public int CantidadEtiquetasFabricadas { get; set; } = 0;

        // Propiedades calculadas
        public bool EstaActiva => Estado == "ACTIVE";

        public bool EstaCompletada => Estado == "COMPLETED";

        public bool EstaCancelada => Estado == "CANCELLED";

        public bool EstaEnProceso => Estado == "IN_PROCESS";

        public string EstadoDescripcion
        {
            get
            {
                return Estado switch
                {
                    "ACTIVE" => "Activa",
                    "IN_PROCESS" => "En Proceso",
                    "COMPLETED" => "Completada",
                    "CANCELLED" => "Cancelada",
                    "PAUSED" => "Pausada",
                    "PENDING" => "Pendiente",
                    _ => Estado
                };
            }
        }

        public int CantidadEtiquetasPendientes => Math.Max(0, CantidadEtiquetasSolicitadas - CantidadEtiquetasFabricadas);

        public bool RequiereGeneracionEtiquetas => RequiereEtiquetas && !TieneCodigoEtiqueta;

        public bool RequiereImpresionEtiquetas => RequiereEtiquetas && TieneCodigoEtiqueta && CantidadEtiquetasPendientes > 0;

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

        public int DiasDesdeCreacion => (DateTime.Now - FechaCreacion).Days;

        public int? DiasParaProgramada => FechaProgramada.HasValue ? (FechaProgramada.Value - DateTime.Now).Days : null;

        public bool EsUrgente => Prioridad <= 2 || (DiasParaProgramada.HasValue && DiasParaProgramada <= 1);

        // Validaciones de negocio
        public bool ValidarDatos()
        {
            return !string.IsNullOrEmpty(BaseId) &&
                   !string.IsNullOrEmpty(PartId) &&
                   Cantidad > 0 &&
                   !string.IsNullOrEmpty(Estado);
        }

        public bool ValidarFechas()
        {
            if (FechaInicio.HasValue && FechaFin.HasValue)
                return FechaFin >= FechaInicio;

            if (FechaProgramada.HasValue)
                return FechaProgramada >= FechaCreacion.Date;

            return true;
        }

        public bool ValidarEtiquetas()
        {
            if (!RequiereEtiquetas)
                return true;

            return CantidadEtiquetasSolicitadas >= 0 &&
                   CantidadEtiquetasFabricadas >= 0 &&
                   CantidadEtiquetasFabricadas <= CantidadEtiquetasSolicitadas;
        }

        // Métodos de utilidad
        public void IniciarProceso(string usuario = "")
        {
            if (!EstaActiva)
                throw new InvalidOperationException("Solo se pueden iniciar órdenes activas");

            Estado = "IN_PROCESS";
            FechaInicio = DateTime.Now;

            if (!string.IsNullOrEmpty(usuario))
                UsuarioCreacion = usuario;
        }

        public void CompletarOrden(string usuario = "")
        {
            if (!EstaEnProceso)
                throw new InvalidOperationException("Solo se pueden completar órdenes en proceso");

            Estado = "COMPLETED";
            FechaFin = DateTime.Now;

            if (!string.IsNullOrEmpty(usuario))
                UsuarioCreacion = usuario;
        }

        public void CancelarOrden(string razon, string usuario = "")
        {
            if (EstaCompletada)
                throw new InvalidOperationException("No se puede cancelar una orden completada");

            Estado = "CANCELLED";
            Observaciones = $"{Observaciones}\n[{DateTime.Now:dd/MM/yyyy}] Cancelada: {razon}".Trim();

            if (!string.IsNullOrEmpty(usuario))
                UsuarioCreacion = usuario;
        }

        public void SolicitarEtiquetas(int cantidad, string usuario = "")
        {
            if (cantidad <= 0)
                throw new ArgumentException("La cantidad debe ser mayor a 0");

            if (cantidad > 10000)
                throw new ArgumentException("No se pueden solicitar más de 10,000 etiquetas por orden");

            CantidadEtiquetasSolicitadas = cantidad;
            RequiereEtiquetas = true;

            if (!string.IsNullOrEmpty(usuario))
                UsuarioCreacion = usuario;
        }

        public void MarcarEtiquetasFabricadas(int cantidad, string usuario = "")
        {
            if (cantidad < 0)
                throw new ArgumentException("La cantidad no puede ser negativa");

            if (cantidad > CantidadEtiquetasPendientes)
                throw new ArgumentException("No se pueden fabricar más etiquetas de las solicitadas");

            CantidadEtiquetasFabricadas += cantidad;

            if (!string.IsNullOrEmpty(usuario))
                UsuarioCreacion = usuario;
        }

        public void EstablecerCodigoEtiqueta(bool tieneCodigoEtiqueta, string usuario = "")
        {
            TieneCodigoEtiqueta = tieneCodigoEtiqueta;

            if (!string.IsNullOrEmpty(usuario))
                UsuarioCreacion = usuario;
        }

        public override string ToString()
        {
            return $"{BaseId} - {PartId} ({Cantidad} unidades)";
        }

        public override bool Equals(object obj)
        {
            if (obj is OrdenFabricacion other)
                return BaseId == other.BaseId;
            return false;
        }

        public override int GetHashCode()
        {
            return BaseId?.GetHashCode() ?? 0;
        }
    }

    // Extensiones para EstadoOrden - utiliza el enum de TipoEtiqueta.cs
    public static class EstadoOrdenExtensions
    {
        public static string GetDisplayName(this EstadoOrden estado)
        {
            return estado switch
            {
                EstadoOrden.Programada => "Programada",
                EstadoOrden.PorIniciar => "Por Iniciar",
                EstadoOrden.EnProceso => "En Proceso",
                EstadoOrden.Pausada => "Pausada",
                EstadoOrden.Completada => "Completada",
                EstadoOrden.Cancelada => "Cancelada",
                EstadoOrden.Vencida => "Vencida",
                _ => estado.ToString()
            };
        }

        public static string GetDescription(this EstadoOrden estado)
        {
            return estado switch
            {
                EstadoOrden.Programada => "Orden programada para producción",
                EstadoOrden.PorIniciar => "Orden próxima a iniciar",
                EstadoOrden.EnProceso => "Orden en proceso de fabricación",
                EstadoOrden.Completada => "Orden completada exitosamente",
                EstadoOrden.Cancelada => "Orden cancelada",
                EstadoOrden.Pausada => "Orden pausada temporalmente",
                EstadoOrden.Vencida => "Fecha de entrega vencida",
                _ => "Estado desconocido"
            };
        }
    }
}