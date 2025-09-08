using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace EtiquetasApp.Models
{
    public class ConfiguracionImpresora
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre de la impresora es requerido")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder 100 caracteres")]
        public string Nombre { get; set; }

        [StringLength(200, ErrorMessage = "La descripción no puede exceder 200 caracteres")]
        public string Descripcion { get; set; }

        public bool EsDefault { get; set; } = false;

        public bool Activa { get; set; } = true;

        public bool EsZebra { get; set; } = false;

        public bool SoportaZPL { get; set; } = false;

        public bool SoportaColor { get; set; } = false;

        // Configuración por defecto
        [Range(1, 10, ErrorMessage = "La velocidad debe estar entre 1 y 10")]
        public int VelocidadDefault { get; set; } = 4;

        [Range(1, 30, ErrorMessage = "La temperatura debe estar entre 1 y 30")]
        public int TemperaturaDefault { get; set; } = 6;

        // Configuración de papel
        [StringLength(20, ErrorMessage = "El tipo de papel no puede exceder 20 caracteres")]
        public string TipoPapelDefault { get; set; } = "EMPACK";

        [StringLength(20, ErrorMessage = "El color por defecto no puede exceder 20 caracteres")]
        public string ColorDefault { get; set; } = "Blancas";

        // Configuración física
        public int AnchoMaximo { get; set; } = 831; // píxeles
        public int AltoMaximo { get; set; } = 1918; // píxeles
        public int DPI { get; set; } = 203; // dots per inch

        // Configuración de red (si aplica)
        [StringLength(50, ErrorMessage = "La dirección IP no puede exceder 50 caracteres")]
        public string DireccionIP { get; set; }

        public int Puerto { get; set; } = 9100;

        [StringLength(50, ErrorMessage = "La dirección MAC no puede exceder 50 caracteres")]
        public string DireccionMAC { get; set; }

        // Configuración de tipos de etiqueta soportados
        public string TiposEtiquetaSoportados { get; set; } = "CBCOE,DUAL,EAN13,BICOLOR,GARDEN,MOLDURAS,LAQUEADO,I2DE5";

        // Configuración específica por tipo
        public string ConfiguracionesPorTipo { get; set; } // JSON con configuraciones específicas

        // Estado y estadísticas
        public bool EstadoOnline { get; set; } = true;
        public DateTime? UltimaVerificacion { get; set; }
        public int EtiquetasImpresas { get; set; } = 0;
        public DateTime? UltimaImpresion { get; set; }

        // Configuración de mantenimiento
        public int AlertaMantenimiento { get; set; } = 10000; // Cada 10000 etiquetas
        public DateTime? UltimoMantenimiento { get; set; }
        public DateTime? ProximoMantenimiento { get; set; }

        // Metadatos
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public DateTime? FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }

        [StringLength(500, ErrorMessage = "Las observaciones no pueden exceder 500 caracteres")]
        public string Observaciones { get; set; }

        // Propiedades calculadas
        public string EstadoDescripcion
        {
            get
            {
                if (!Activa) return "Inactiva";
                if (!EstadoOnline) return "Desconectada";
                if (RequiereMantenimiento) return "Requiere Mantenimiento";
                return "Operativa";
            }
        }

        public bool RequiereMantenimiento
        {
            get
            {
                if (!UltimoMantenimiento.HasValue) return EtiquetasImpresas > AlertaMantenimiento;

                var etiquetasDesdeMant = EtiquetasImpresas;
                return etiquetasDesdeMant >= AlertaMantenimiento;
            }
        }

        public int EtiquetasParaMantenimiento
        {
            get
            {
                var pendientes = AlertaMantenimiento - EtiquetasImpresas;
                return pendientes > 0 ? pendientes : 0;
            }
        }

        public List<TipoEtiqueta> TiposEtiquetaList
        {
            get
            {
                var tipos = new List<TipoEtiqueta>();
                if (string.IsNullOrEmpty(TiposEtiquetaSoportados))
                    return tipos;

                var tiposString = TiposEtiquetaSoportados.Split(',');
                foreach (var tipo in tiposString)
                {
                    if (Enum.TryParse<TipoEtiqueta>(tipo.Trim(), out var tipoEnum))
                    {
                        tipos.Add(tipoEnum);
                    }
                }
                return tipos;
            }
        }

        public bool EsDisponible => Activa && EstadoOnline && !RequiereMantenimiento;

        public TimeSpan TiempoSinUso
        {
            get
            {
                if (!UltimaImpresion.HasValue)
                    return TimeSpan.MaxValue;

                return DateTime.Now - UltimaImpresion.Value;
            }
        }

        // Métodos de validación
        public bool ValidarConfiguracion()
        {
            if (string.IsNullOrEmpty(Nombre))
                return false;

            if (VelocidadDefault < 1 || VelocidadDefault > 10)
                return false;

            if (TemperaturaDefault < 1 || TemperaturaDefault > 30)
                return false;

            if (!string.IsNullOrEmpty(DireccionIP) && !ValidarIP(DireccionIP))
                return false;

            return true;
        }

        private bool ValidarIP(string ip)
        {
            if (System.Net.IPAddress.TryParse(ip, out _))
                return true;
            return false;
        }

        public bool SoportaTipoEtiqueta(TipoEtiqueta tipo)
        {
            return TiposEtiquetaList.Contains(tipo);
        }

        public bool SoportaTipoEtiqueta(string tipoString)
        {
            if (string.IsNullOrEmpty(TiposEtiquetaSoportados))
                return false;

            return TiposEtiquetaSoportados.Contains(tipoString, StringComparison.OrdinalIgnoreCase);
        }

        // Métodos de utilidad
        public void MarcarComoDefault()
        {
            EsDefault = true;
            FechaModificacion = DateTime.Now;
        }

        public void QuitarDefault()
        {
            EsDefault = false;
            FechaModificacion = DateTime.Now;
        }

        public void Activar(string usuario = "")
        {
            Activa = true;
            FechaModificacion = DateTime.Now;
            if (!string.IsNullOrEmpty(usuario))
                UsuarioModificacion = usuario;
        }

        public void Desactivar(string usuario = "", string razon = "")
        {
            Activa = false;
            FechaModificacion = DateTime.Now;
            if (!string.IsNullOrEmpty(usuario))
                UsuarioModificacion = usuario;
            if (!string.IsNullOrEmpty(razon))
                Observaciones = $"{Observaciones}\n[{DateTime.Now:dd/MM/yyyy}] Desactivada: {razon}".Trim();
        }

        public void ActualizarEstado(bool online)
        {
            EstadoOnline = online;
            UltimaVerificacion = DateTime.Now;
        }

        public void RegistrarImpresion(int cantidad = 1)
        {
            EtiquetasImpresas += cantidad;
            UltimaImpresion = DateTime.Now;
            FechaModificacion = DateTime.Now;
        }

        public void RealizarMantenimiento(string usuario = "", string descripcion = "")
        {
            UltimoMantenimiento = DateTime.Now;
            ProximoMantenimiento = DateTime.Now.AddDays(30); // 30 días después
            EtiquetasImpresas = 0; // Reset contador

            if (!string.IsNullOrEmpty(descripcion))
                Observaciones = $"{Observaciones}\n[{DateTime.Now:dd/MM/yyyy}] Mantenimiento: {descripcion}".Trim();

            FechaModificacion = DateTime.Now;
            if (!string.IsNullOrEmpty(usuario))
                UsuarioModificacion = usuario;
        }

        public void ActualizarConfiguracionDefault(int velocidad, int temperatura, string tipoPapel = "", string color = "")
        {
            if (velocidad >= 1 && velocidad <= 10)
                VelocidadDefault = velocidad;

            if (temperatura >= 1 && temperatura <= 30)
                TemperaturaDefault = temperatura;

            if (!string.IsNullOrEmpty(tipoPapel))
                TipoPapelDefault = tipoPapel;

            if (!string.IsNullOrEmpty(color))
                ColorDefault = color;

            FechaModificacion = DateTime.Now;
        }

        public void ConfigurarRed(string ip, int puerto = 9100, string mac = "")
        {
            if (ValidarIP(ip))
            {
                DireccionIP = ip;
                Puerto = puerto;
                if (!string.IsNullOrEmpty(mac))
                    DireccionMAC = mac;
                FechaModificacion = DateTime.Now;
            }
        }

        public void AgregarTipoEtiqueta(TipoEtiqueta tipo)
        {
            var tipos = TiposEtiquetaList;
            if (!tipos.Contains(tipo))
            {
                tipos.Add(tipo);
                TiposEtiquetaSoportados = string.Join(",", tipos.Select(t => t.ToString()));
                FechaModificacion = DateTime.Now;
            }
        }

        public void RemoverTipoEtiqueta(TipoEtiqueta tipo)
        {
            var tipos = TiposEtiquetaList;
            if (tipos.Contains(tipo))
            {
                tipos.Remove(tipo);
                TiposEtiquetaSoportados = string.Join(",", tipos.Select(t => t.ToString()));
                FechaModificacion = DateTime.Now;
            }
        }

        public bool PuedeImprimir(EtiquetaData etiqueta)
        {
            if (!EsDisponible)
                return false;

            if (!SoportaTipoEtiqueta(etiqueta.TipoEtiqueta))
                return false;

            if (!SoportaZPL && etiqueta.TipoEtiqueta != "SIMPLE")
                return false;

            return true;
        }

        public ConfiguracionImpresora Clonar()
        {
            return new ConfiguracionImpresora
            {
                Nombre = $"{this.Nombre} - Copia",
                Descripcion = this.Descripcion,
                EsZebra = this.EsZebra,
                SoportaZPL = this.SoportaZPL,
                SoportaColor = this.SoportaColor,
                VelocidadDefault = this.VelocidadDefault,
                TemperaturaDefault = this.TemperaturaDefault,
                TipoPapelDefault = this.TipoPapelDefault,
                ColorDefault = this.ColorDefault,
                AnchoMaximo = this.AnchoMaximo,
                AltoMaximo = this.AltoMaximo,
                DPI = this.DPI,
                TiposEtiquetaSoportados = this.TiposEtiquetaSoportados,
                ConfiguracionesPorTipo = this.ConfiguracionesPorTipo,
                AlertaMantenimiento = this.AlertaMantenimiento,
                Activa = false // Las copias inician inactivas
            };
        }

        public override string ToString()
        {
            return $"{Nombre} - {EstadoDescripcion}" + (EsDefault ? " (Default)" : "");
        }

        // Constructor
        public ConfiguracionImpresora()
        {
            FechaCreacion = DateTime.Now;
            Activa = true;
            VelocidadDefault = 4;
            TemperaturaDefault = 6;
            TipoPapelDefault = "EMPACK";
            ColorDefault = "Blancas";
            TiposEtiquetaSoportados = "CBCOE,DUAL,EAN13,BICOLOR,GARDEN,MOLDURAS,LAQUEADO,I2DE5";
            AlertaMantenimiento = 10000;
            Puerto = 9100;
            AnchoMaximo = 831;
            AltoMaximo = 1918;
            DPI = 203;
        }
    }
}