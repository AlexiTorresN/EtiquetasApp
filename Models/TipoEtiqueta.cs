using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EtiquetasApp.Models
{
    public enum TipoEtiqueta
    {
        [Display(Name = "C/BCO-E", Description = "Etiquetas C/BCO-E estándar")]
        CBCOE,

        [Display(Name = "DUAL", Description = "Etiquetas duales con dos códigos")]
        DUAL,

        [Display(Name = "EAN 13", Description = "Etiquetas con código EAN 13")]
        EAN13,

        [Display(Name = "BICOLOR2", Description = "Etiquetas bicolor")]
        BICOLOR,

        [Display(Name = "Garden State", Description = "Etiquetas especiales Garden State")]
        GARDEN,

        [Display(Name = "Molduras", Description = "Etiquetas para molduras")]
        MOLDURAS,

        [Display(Name = "Laqueado", Description = "Etiquetas para productos laqueados")]
        LAQUEADO,

        [Display(Name = "I 2 de 5", Description = "Etiquetas con código I 2 de 5")]
        I2DE5
    }

    public enum EstadoSolicitud
    {
        [Display(Name = "Pendiente", Description = "Solicitud creada, esperando fabricación")]
        Pendiente,

        [Display(Name = "En Proceso", Description = "Fabricación iniciada")]
        EnProceso,

        [Display(Name = "Completada", Description = "Solicitud completamente fabricada")]
        Completada,

        [Display(Name = "Vencida", Description = "Fecha requerida vencida sin completar")]
        Vencida,

        [Display(Name = "Cancelada", Description = "Solicitud cancelada")]
        Cancelada,

        [Display(Name = "Inactiva", Description = "Solicitud desactivada")]
        Inactiva
    }

    public enum EstadoOrden
    {
        [Display(Name = "Programada", Description = "Orden programada para producción")]
        Programada,

        [Display(Name = "Por Iniciar", Description = "Orden próxima a iniciar")]
        PorIniciar,

        [Display(Name = "En Proceso", Description = "Orden en producción")]
        EnProceso,

        [Display(Name = "Pausada", Description = "Orden pausada temporalmente")]
        Pausada,

        [Display(Name = "Completada", Description = "Orden completada")]
        Completada,

        [Display(Name = "Cancelada", Description = "Orden cancelada")]
        Cancelada,

        [Display(Name = "Vencida", Description = "Fecha de entrega vencida")]
        Vencida
    }

    public enum Prioridad
    {
        [Display(Name = "Muy Alta", Description = "Prioridad crítica - 1")]
        MuyAlta = 1,

        [Display(Name = "Alta", Description = "Prioridad alta - 2")]
        Alta = 2,

        [Display(Name = "Media-Alta", Description = "Prioridad media-alta - 3")]
        MediaAlta = 3,

        [Display(Name = "Media", Description = "Prioridad media - 4")]
        Media = 4,

        [Display(Name = "Normal", Description = "Prioridad normal - 5")]
        Normal = 5,

        [Display(Name = "Media-Baja", Description = "Prioridad media-baja - 6")]
        MediaBaja = 6,

        [Display(Name = "Baja", Description = "Prioridad baja - 7")]
        Baja = 7,

        [Display(Name = "Muy Baja", Description = "Prioridad muy baja - 8")]
        MuyBaja = 8
    }

    public enum ColorEtiqueta
    {
        [Display(Name = "Blancas", Description = "Etiquetas blancas estándar")]
        Blancas,

        [Display(Name = "Amarillas", Description = "Etiquetas amarillas")]
        Amarillas,

        [Display(Name = "Azules", Description = "Etiquetas azules")]
        Azules,

        [Display(Name = "Verdes", Description = "Etiquetas verdes")]
        Verdes,

        [Display(Name = "Rojas", Description = "Etiquetas rojas")]
        Rojas,

        [Display(Name = "Transparentes", Description = "Etiquetas transparentes")]
        Transparentes
    }

    public enum TipoPapel
    {
        [Display(Name = "EMPACK", Description = "Papel EMPACK estándar")]
        EMPACK,

        [Display(Name = "SOLUCORP", Description = "Papel SOLUCORP")]
        SOLUCORP,

        [Display(Name = "Térmico", Description = "Papel térmico")]
        Termico,

        [Display(Name = "Sintético", Description = "Material sintético")]
        Sintetico,

        [Display(Name = "Adhesivo", Description = "Papel adhesivo especial")]
        Adhesivo
    }

    public enum VelocidadImpresion
    {
        [Display(Name = "Muy Lenta (1)", Description = "Velocidad 1 - Para alta calidad")]
        MuyLenta = 1,

        [Display(Name = "Lenta (2)", Description = "Velocidad 2")]
        Lenta = 2,

        [Display(Name = "Media-Lenta (3)", Description = "Velocidad 3")]
        MediaLenta = 3,

        [Display(Name = "Normal (4)", Description = "Velocidad 4 - Estándar")]
        Normal = 4,

        [Display(Name = "Media-Rápida (5)", Description = "Velocidad 5")]
        MediaRapida = 5,

        [Display(Name = "Rápida (6)", Description = "Velocidad 6")]
        Rapida = 6,

        [Display(Name = "Muy Rápida (7)", Description = "Velocidad 7")]
        MuyRapida = 7,

        [Display(Name = "Máxima (8)", Description = "Velocidad 8 - Máxima velocidad")]
        Maxima = 8
    }

    public enum TemperaturaImpresion
    {
        [Display(Name = "Muy Baja (1-5)", Description = "Temperatura muy baja")]
        MuyBaja = 3,

        [Display(Name = "Baja (6-10)", Description = "Temperatura baja")]
        Baja = 6,

        [Display(Name = "Normal (11-15)", Description = "Temperatura normal")]
        Normal = 10,

        [Display(Name = "Media (16-20)", Description = "Temperatura media")]
        Media = 18,

        [Display(Name = "Alta (21-25)", Description = "Temperatura alta")]
        Alta = 23,

        [Display(Name = "Muy Alta (26-30)", Description = "Temperatura muy alta")]
        MuyAlta = 28
    }

    // Clase de extensiones para los enums
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum enumValue)
        {
            var displayAttribute = enumValue.GetType()
                .GetField(enumValue.ToString())
                ?.GetCustomAttributes(typeof(DisplayAttribute), false)
                .FirstOrDefault() as DisplayAttribute;

            return displayAttribute?.Name ?? enumValue.ToString();
        }

        public static string GetDescription(this Enum enumValue)
        {
            var displayAttribute = enumValue.GetType()
                .GetField(enumValue.ToString())
                ?.GetCustomAttributes(typeof(DisplayAttribute), false)
                .FirstOrDefault() as DisplayAttribute;

            return displayAttribute?.Description ?? enumValue.ToString();
        }

        public static TipoEtiqueta ParseTipoEtiqueta(string tipoString)
        {
            return tipoString?.ToUpper() switch
            {
                "CBCOE" or "C/BCO-E" => TipoEtiqueta.CBCOE,
                "DUAL" => TipoEtiqueta.DUAL,
                "EAN13" or "EAN 13" => TipoEtiqueta.EAN13,
                "BICOLOR" or "BICOLOR2" => TipoEtiqueta.BICOLOR,
                "GARDEN" or "GARDEN STATE" => TipoEtiqueta.GARDEN,
                "MOLDURAS" => TipoEtiqueta.MOLDURAS,
                "LAQUEADO" => TipoEtiqueta.LAQUEADO,
                "I2DE5" or "I 2 DE 5" => TipoEtiqueta.I2DE5,
                _ => TipoEtiqueta.CBCOE // Valor por defecto
            };
        }

        public static string ToCodeString(this TipoEtiqueta tipo)
        {
            return tipo switch
            {
                TipoEtiqueta.CBCOE => "CBCOE",
                TipoEtiqueta.DUAL => "DUAL",
                TipoEtiqueta.EAN13 => "EAN13",
                TipoEtiqueta.BICOLOR => "BICOLOR",
                TipoEtiqueta.GARDEN => "GARDEN",
                TipoEtiqueta.MOLDURAS => "MOLDURAS",
                TipoEtiqueta.LAQUEADO => "LAQUEADO",
                TipoEtiqueta.I2DE5 => "I2DE5",
                _ => "CBCOE"
            };
        }

        public static bool RequiereDualUPC(this TipoEtiqueta tipo)
        {
            return tipo == TipoEtiqueta.DUAL;
        }

        public static bool RequiereOrdenFab(this TipoEtiqueta tipo)
        {
            return tipo == TipoEtiqueta.EAN13 || tipo == TipoEtiqueta.MOLDURAS;
        }

        public static bool RequiereSKU(this TipoEtiqueta tipo)
        {
            return tipo == TipoEtiqueta.MOLDURAS || tipo == TipoEtiqueta.EAN13;
        }

        public static bool SoportaLogo(this TipoEtiqueta tipo)
        {
            return tipo == TipoEtiqueta.CBCOE || tipo == TipoEtiqueta.GARDEN;
        }

        public static int GetCantidadPorHoja(this TipoEtiqueta tipo)
        {
            return tipo switch
            {
                TipoEtiqueta.CBCOE => 5,
                TipoEtiqueta.DUAL => 5,
                TipoEtiqueta.EAN13 => 5,
                TipoEtiqueta.BICOLOR => 1,
                TipoEtiqueta.GARDEN => 1,
                TipoEtiqueta.MOLDURAS => 1,
                TipoEtiqueta.LAQUEADO => 1,
                TipoEtiqueta.I2DE5 => 5,
                _ => 1
            };
        }

        public static VelocidadImpresion GetVelocidadRecomendada(this TipoEtiqueta tipo)
        {
            return tipo switch
            {
                TipoEtiqueta.GARDEN => VelocidadImpresion.Lenta, // Más detallada
                TipoEtiqueta.EAN13 => VelocidadImpresion.MediaLenta, // Precisión en códigos
                _ => VelocidadImpresion.Normal
            };
        }

        public static TemperaturaImpresion GetTemperaturaRecomendada(this TipoEtiqueta tipo)
        {
            return tipo switch
            {
                TipoEtiqueta.GARDEN => TemperaturaImpresion.Media, // Más calor para mejor adherencia
                TipoEtiqueta.BICOLOR => TemperaturaImpresion.Alta, // Para colores especiales
                _ => TemperaturaImpresion.Baja
            };
        }
    }
}