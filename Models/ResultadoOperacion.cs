using System;
using System.Collections.Generic;
using System.Linq;

namespace EtiquetasApp.Models
{
    /// <summary>
    /// Clase genérica para manejar resultados de operaciones del sistema
    /// </summary>
    /// <typeparam name="T">Tipo de datos que retorna la operación</typeparam>
    public class ResultadoOperacion<T>
    {
        public bool Exito { get; set; }
        public string Mensaje { get; set; }
        public T Datos { get; set; }
        public List<string> Errores { get; set; } = new List<string>();
        public List<string> Advertencias { get; set; } = new List<string>();
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public string OperacionId { get; set; } = Guid.NewGuid().ToString();

        // Propiedades adicionales para debugging
        public string Usuario { get; set; }
        public string Origen { get; set; }
        public TimeSpan? TiempoEjecucion { get; set; }

        // Propiedades calculadas
        public bool TieneErrores => Errores?.Any() == true;
        public bool TieneAdvertencias => Advertencias?.Any() == true;
        public int CantidadErrores => Errores?.Count ?? 0;
        public int CantidadAdvertencias => Advertencias?.Count ?? 0;

        public string MensajeCompleto
        {
            get
            {
                var mensajes = new List<string>();

                if (!string.IsNullOrEmpty(Mensaje))
                    mensajes.Add(Mensaje);

                if (TieneErrores)
                    mensajes.AddRange(Errores.Select(e => $"Error: {e}"));

                if (TieneAdvertencias)
                    mensajes.AddRange(Advertencias.Select(a => $"Advertencia: {a}"));

                return string.Join(Environment.NewLine, mensajes);
            }
        }

        // Constructores
        public ResultadoOperacion()
        {
            Errores = new List<string>();
            Advertencias = new List<string>();
        }

        public ResultadoOperacion(bool exito, string mensaje = "", T datos = default(T))
        {
            Exito = exito;
            Mensaje = mensaje;
            Datos = datos;
            Errores = new List<string>();
            Advertencias = new List<string>();
        }

        // Métodos estáticos para crear resultados comunes
        public static ResultadoOperacion<T> ExitosoConDatos(T datos, string mensaje = "Operación exitosa")
        {
            return new ResultadoOperacion<T>
            {
                Exito = true,
                Mensaje = mensaje,
                Datos = datos
            };
        }

        public static ResultadoOperacion<T> Exitoso(string mensaje = "Operación exitosa")
        {
            return new ResultadoOperacion<T>
            {
                Exito = true,
                Mensaje = mensaje
            };
        }

        public static ResultadoOperacion<T> Fallido(string mensaje, params string[] errores)
        {
            var resultado = new ResultadoOperacion<T>
            {
                Exito = false,
                Mensaje = mensaje
            };

            if (errores != null)
                resultado.Errores.AddRange(errores);

            return resultado;
        }

        public static ResultadoOperacion<T> FallidoConExcepcion(Exception ex, string mensajePersonalizado = "")
        {
            var mensaje = string.IsNullOrEmpty(mensajePersonalizado)
                ? "Ocurrió un error durante la operación"
                : mensajePersonalizado;

            return new ResultadoOperacion<T>
            {
                Exito = false,
                Mensaje = mensaje,
                Errores = new List<string> { ex.Message }
            };
        }

        // Métodos para agregar información
        public ResultadoOperacion<T> AgregarError(string error)
        {
            if (!string.IsNullOrEmpty(error))
                Errores.Add(error);
            Exito = false; // Si hay errores, la operación no es exitosa
            return this;
        }

        public ResultadoOperacion<T> AgregarErrores(params string[] errores)
        {
            if (errores != null)
            {
                foreach (var error in errores.Where(e => !string.IsNullOrEmpty(e)))
                {
                    Errores.Add(error);
                }
                if (errores.Any())
                    Exito = false;
            }
            return this;
        }

        public ResultadoOperacion<T> AgregarAdvertencia(string advertencia)
        {
            if (!string.IsNullOrEmpty(advertencia))
                Advertencias.Add(advertencia);
            return this;
        }

        public ResultadoOperacion<T> AgregarAdvertencias(params string[] advertencias)
        {
            if (advertencias != null)
            {
                foreach (var advertencia in advertencias.Where(a => !string.IsNullOrEmpty(a)))
                {
                    Advertencias.Add(advertencia);
                }
            }
            return this;
        }

        public ResultadoOperacion<T> ConDatos(T datos)
        {
            Datos = datos;
            return this;
        }

        public ResultadoOperacion<T> ConMensaje(string mensaje)
        {
            Mensaje = mensaje;
            return this;
        }

        public ResultadoOperacion<T> ConUsuario(string usuario)
        {
            Usuario = usuario;
            return this;
        }

        public ResultadoOperacion<T> ConOrigen(string origen)
        {
            Origen = origen;
            return this;
        }

        public ResultadoOperacion<T> ConTiempoEjecucion(TimeSpan tiempo)
        {
            TiempoEjecucion = tiempo;
            return this;
        }

        // Métodos de validación
        public bool EsValidoParaMostrar()
        {
            return !string.IsNullOrEmpty(Mensaje) || TieneErrores || TieneAdvertencias;
        }

        public bool RequiereAtencion()
        {
            return !Exito || TieneErrores;
        }

        // Métodos de conversión
        public ResultadoOperacion<TNew> ConvertirA<TNew>(TNew nuevosDatos)
        {
            return new ResultadoOperacion<TNew>
            {
                Exito = this.Exito,
                Mensaje = this.Mensaje,
                Datos = nuevosDatos,
                Errores = new List<string>(this.Errores),
                Advertencias = new List<string>(this.Advertencias),
                Timestamp = this.Timestamp,
                OperacionId = this.OperacionId,
                Usuario = this.Usuario,
                Origen = this.Origen,
                TiempoEjecucion = this.TiempoEjecucion
            };
        }

        public ResultadoOperacion<TNew> ConvertirA<TNew>()
        {
            return ConvertirA<TNew>(default(TNew));
        }

        // Métodos de combinación
        public ResultadoOperacion<T> CombinarCon(ResultadoOperacion<T> otro)
        {
            if (otro == null) return this;

            // Si cualquiera de los dos falló, el resultado combinado falla
            Exito = Exito && otro.Exito;

            // Combinar mensajes
            if (!string.IsNullOrEmpty(otro.Mensaje))
            {
                Mensaje = string.IsNullOrEmpty(Mensaje)
                    ? otro.Mensaje
                    : $"{Mensaje}; {otro.Mensaje}";
            }

            // Combinar errores y advertencias
            if (otro.TieneErrores)
                Errores.AddRange(otro.Errores);

            if (otro.TieneAdvertencias)
                Advertencias.AddRange(otro.Advertencias);

            return this;
        }

        // Override ToString para debugging
        public override string ToString()
        {
            var estado = Exito ? "EXITOSO" : "FALLIDO";
            var info = $"[{estado}] {Mensaje}";

            if (TieneErrores)
                info += $" | Errores: {CantidadErrores}";

            if (TieneAdvertencias)
                info += $" | Advertencias: {CantidadAdvertencias}";

            if (TiempoEjecucion.HasValue)
                info += $" | Tiempo: {TiempoEjecucion.Value.TotalMilliseconds}ms";

            return info;
        }
    }

    /// <summary>
    /// Versión no genérica para operaciones que no retornan datos
    /// </summary>
    public class ResultadoOperacion : ResultadoOperacion<object>
    {
        public ResultadoOperacion() : base() { }

        public ResultadoOperacion(bool exito, string mensaje = "") : base(exito, mensaje, null) { }

        // Métodos que retornan la versión no genérica
        public new ResultadoOperacion AgregarError(string error)
        {
            base.AgregarError(error);
            return this;
        }

        public new ResultadoOperacion AgregarErrores(params string[] errores)
        {
            base.AgregarErrores(errores);
            return this;
        }

        public new ResultadoOperacion AgregarAdvertencia(string advertencia)
        {
            base.AgregarAdvertencia(advertencia);
            return this;
        }

        public new ResultadoOperacion AgregarAdvertencias(params string[] advertencias)
        {
            base.AgregarAdvertencias(advertencias);
            return this;
        }

        public new ResultadoOperacion ConMensaje(string mensaje)
        {
            base.ConMensaje(mensaje);
            return this;
        }

        public new ResultadoOperacion ConUsuario(string usuario)
        {
            base.ConUsuario(usuario);
            return this;
        }

        public new ResultadoOperacion ConOrigen(string origen)
        {
            base.ConOrigen(origen);
            return this;
        }

        public new ResultadoOperacion ConTiempoEjecucion(TimeSpan tiempo)
        {
            base.ConTiempoEjecucion(tiempo);
            return this;
        }

        // Métodos estáticos específicos para la versión no genérica
        public static new ResultadoOperacion Exitoso(string mensaje = "Operación exitosa")
        {
            return new ResultadoOperacion(true, mensaje);
        }

        public static new ResultadoOperacion Fallido(string mensaje, params string[] errores)
        {
            var resultado = new ResultadoOperacion(false, mensaje);
            if (errores != null)
                resultado.Errores.AddRange(errores);
            return resultado;
        }

        public static new ResultadoOperacion FallidoConExcepcion(Exception ex, string mensajePersonalizado = "")
        {
            var mensaje = string.IsNullOrEmpty(mensajePersonalizado)
                ? "Ocurrió un error durante la operación"
                : mensajePersonalizado;

            var resultado = new ResultadoOperacion(false, mensaje);
            resultado.AgregarError(ex.Message);
            return resultado;
        }
    }

    /// <summary>
    /// Extensiones útiles para trabajar con ResultadoOperacion
    /// </summary>
    public static class ResultadoOperacionExtensions
    {
        public static void MostrarMensaje(this ResultadoOperacion resultado, Action<string, bool> mostrarCallback)
        {
            if (resultado.EsValidoParaMostrar())
            {
                mostrarCallback(resultado.MensajeCompleto, resultado.Exito);
            }
        }

        public static T ObtenerDatosODefault<T>(this ResultadoOperacion<T> resultado, T valorDefault = default(T))
        {
            return resultado.Exito ? resultado.Datos : valorDefault;
        }

        public static bool EjecutarSiExitoso<T>(this ResultadoOperacion<T> resultado, Action<T> accion)
        {
            if (resultado.Exito && accion != null)
            {
                accion(resultado.Datos);
                return true;
            }
            return false;
        }

        public static bool EjecutarSiFallo<T>(this ResultadoOperacion<T> resultado, Action<List<string>> accion)
        {
            if (!resultado.Exito && accion != null)
            {
                accion(resultado.Errores);
                return true;
            }
            return false;
        }
    }
}