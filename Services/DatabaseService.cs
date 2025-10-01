using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using EtiquetasApp.Models;

namespace EtiquetasApp.Services
{
    public static class DatabaseService
    {
        private static string _connection1;
        private static string _connection2;

        static DatabaseService()
        {
            _connection1 = ConfigurationService.DatabaseConnection1;
            _connection2 = ConfigurationService.DatabaseConnection2;
        }

        public static bool TestConnection(string connectionName = "DatabaseConnection1")
        {
            try
            {
                var connectionString = connectionName == "DatabaseConnection1" ? _connection1 : _connection2;
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public static DataTable ExecuteQuery(string sql, string connectionName = "DatabaseConnection1", SqlParameter[] parameters = null)
        {
            var connectionString = connectionName == "DatabaseConnection1" ? _connection1 : _connection2;
            var dataTable = new DataTable();

            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    using (var command = new SqlCommand(sql, connection))
                    {
                        if (parameters != null)
                            command.Parameters.AddRange(parameters);

                        using (var adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dataTable);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error ejecutando consulta: {ex.Message}", ex);
            }

            return dataTable;
        }

        public static int ExecuteNonQuery(string sql, string connectionName = "DatabaseConnection1", SqlParameter[] parameters = null)
        {
            var connectionString = connectionName == "DatabaseConnection1" ? _connection1 : _connection2;

            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    using (var command = new SqlCommand(sql, connection))
                    {
                        if (parameters != null)
                            command.Parameters.AddRange(parameters);

                        connection.Open();
                        return command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error ejecutando comando: {ex.Message}", ex);
            }
        }

        public static object ExecuteScalar(string sql, string connectionName = "DatabaseConnection1", SqlParameter[] parameters = null)
        {
            var connectionString = connectionName == "DatabaseConnection1" ? _connection1 : _connection2;

            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    using (var command = new SqlCommand(sql, connection))
                    {
                        if (parameters != null)
                            command.Parameters.AddRange(parameters);

                        connection.Open();
                        return command.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error ejecutando escalar: {ex.Message}", ex);
            }
        }

        // Métodos específicos para el sistema de etiquetas

        public static List<SolicitudEtiqueta> GetSolicitudesEtiquetas()
        {
            var solicitudes = new List<SolicitudEtiqueta>();
            var sql = @"
                SELECT Id_Solicitud, Orden_Fab, Descripcion, Cantidad_Pedida, 
                       Cantidad_Fabricada, Color, Observaciones, Fecha_Solicitud, 
                       Fecha_Requerida, Fecha_Fabricacion 
                FROM SOLICITUD_ETIQUETA 
                WHERE Cantidad_Fabricada < Cantidad_Pedida 
                ORDER BY Fecha_Requerida";

            try
            {
                var dataTable = ExecuteQuery(sql);
                foreach (DataRow row in dataTable.Rows)
                {
                    solicitudes.Add(new SolicitudEtiqueta
                    {
                        IdSolicitud = Convert.ToInt32(row["Id_Solicitud"]),
                        OrdenFab = Convert.ToInt32(row["Orden_Fab"]),
                        Descripcion = row["Descripcion"].ToString(),
                        CantidadPedida = Convert.ToInt32(row["Cantidad_Pedida"]),
                        CantidadFabricada = row["Cantidad_Fabricada"] != DBNull.Value ? Convert.ToInt32(row["Cantidad_Fabricada"]) : 0,
                        Color = row["Color"].ToString(),
                        Observaciones = row["Observaciones"].ToString(),
                        FechaSolicitud = Convert.ToDateTime(row["Fecha_Solicitud"]),
                        FechaRequerida = Convert.ToDateTime(row["Fecha_Requerida"]),
                        FechaFabricacion = row["Fecha_Fabricacion"] != DBNull.Value ? Convert.ToDateTime(row["Fecha_Fabricacion"]) : (DateTime?)null
                    });
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error obteniendo solicitudes de etiquetas: {ex.Message}", ex);
            }

            return solicitudes;
        }

        public static List<OrdenFabricacion> GetOrdenesRequerimientos()
        {
            var ordenes = new List<OrdenFabricacion>();
            var sql = @"
                SELECT DISTINCT 
                    WO.BASE_ID,
                    WO.PART_QTY as Cantidad,
                    P.DESCRIPTION as Descripcion,
                    P.ID as PartId,
                    WO.NEED_DATE as FechaRequerida,
                    WO.START_DATE as FechaInicio
                FROM WORK_ORDER WO
                INNER JOIN OPERATION_SCHED OS ON WO.BASE_ID = OS.WORKORDER_BASE_ID
                INNER JOIN PART P ON WO.PART_ID = P.ID
                WHERE OS.SCHEDULE_ID = 'STANDARD'
                AND OS.RESOURCE_ID = 'ETIQ'
                AND P.ID NOT IN (SELECT DISTINCT PART_ID FROM MAESTRO_COD_ETIQ)
                ORDER BY WO.NEED_DATE";

            try
            {
                var dataTable = ExecuteQuery(sql, "DatabaseConnection2");
                foreach (DataRow row in dataTable.Rows)
                {
                    ordenes.Add(new OrdenFabricacion
                    {
                        BaseId = row["BASE_ID"].ToString(),
                        Cantidad = Convert.ToInt32(row["Cantidad"]),
                        Descripcion = row["Descripcion"].ToString(),
                        PartId = row["PartId"].ToString(),
                        FechaProgramada = row["FechaRequerida"] != DBNull.Value ? Convert.ToDateTime(row["FechaRequerida"]) : (DateTime?)null,
                        FechaInicio = row["FechaInicio"] != DBNull.Value ? Convert.ToDateTime(row["FechaInicio"]) : (DateTime?)null
                    });
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error obteniendo órdenes de fabricación: {ex.Message}", ex);
            }

            return ordenes;
        }

        public static bool InsertSolicitudEtiqueta(SolicitudEtiqueta solicitud)
        {
            var sql = @"
                INSERT INTO SOLICITUD_ETIQUETA 
                (Orden_Fab, Descripcion, Cantidad_Pedida, Color, Observaciones, 
                 Fecha_Solicitud, Fecha_Requerida, UPC1, UPC2, Tipo_Etiqueta)
                VALUES 
                (@OrdenFab, @Descripcion, @CantidadPedida, @Color, @Observaciones, 
                 @FechaSolicitud, @FechaRequerida, @UPC1, @UPC2, @TipoEtiqueta)";

            var parameters = new SqlParameter[]
            {
                new SqlParameter("@OrdenFab", solicitud.OrdenFab),
                new SqlParameter("@Descripcion", solicitud.Descripcion ?? ""),
                new SqlParameter("@CantidadPedida", solicitud.CantidadPedida),
                new SqlParameter("@Color", solicitud.Color ?? ""),
                new SqlParameter("@Observaciones", solicitud.Observaciones ?? ""),
                new SqlParameter("@FechaSolicitud", solicitud.FechaSolicitud),
                new SqlParameter("@FechaRequerida", solicitud.FechaRequerida),
                new SqlParameter("@UPC1", solicitud.UPC1 ?? ""),
                new SqlParameter("@UPC2", solicitud.UPC2 ?? ""),
                new SqlParameter("@TipoEtiqueta", solicitud.TipoEtiqueta ?? "")
            };

            try
            {
                return ExecuteNonQuery(sql, "DatabaseConnection1", parameters) > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error insertando solicitud de etiqueta: {ex.Message}", ex);
            }
        }

        public static bool UpdateSolicitudEtiquetaFabricada(int idSolicitud, int cantidadFabricada)
        {
            var sql = @"
                UPDATE SOLICITUD_ETIQUETA 
                SET Cantidad_fabricada = @CantidadFabricada, 
                    Fecha_Fabricacion = GETDATE()
                WHERE Id_Solicitud = @IdSolicitud";

            var parameters = new SqlParameter[]
            {
                new SqlParameter("@IdSolicitud", idSolicitud),
                new SqlParameter("@CantidadFabricada", cantidadFabricada)
            };

            try
            {
                return ExecuteNonQuery(sql, "DatabaseConnection1", parameters) > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error actualizando solicitud fabricada: {ex.Message}", ex);
            }
        }

        public static List<MaestroCodigoEtiqueta> MaestroCodigosEtiquetas
        {
            get
            {
                var codigos = new List<MaestroCodigoEtiqueta>();
                var sql = @"
                SELECT PART_ID, UPC1, UPC2, DESCRIPCION, TIPO_ETIQUETA, ACTIVO
                FROM MAESTRO_COD_ETIQ
                WHERE ACTIVO = 1
                ORDER BY PART_ID";

                try
                {
                    var dataTable = ExecuteQuery(sql);
                    foreach (DataRow row in dataTable.Rows)
                    {
                        codigos.Add(new MaestroCodigoEtiqueta
                        {
                            PartId = row["PART_ID"].ToString(),
                            UPC1 = row["UPC1"].ToString(),
                            UPC2 = row["UPC2"].ToString(),
                            Descripcion = row["DESCRIPCION"].ToString(),
                            TipoEtiqueta = row["TIPO_ETIQUETA"].ToString(),
                            Activo = Convert.ToBoolean(row["ACTIVO"])
                        });
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error obteniendo maestro de códigos: {ex.Message}", ex);
                }

                return codigos;
            }
        }

        public static string GetFechaServidor()
        {
            try
            {
                var fecha = ExecuteScalar("SELECT CONVERT(CHAR(10), GETDATE(), 105)");
                return fecha?.ToString() ?? DateTime.Now.ToString("dd-MM-yyyy");
            }
            catch
            {
                return DateTime.Now.ToString("dd-MM-yyyy");
            }
        }

        public static DateTime GetFechaHoraServidor()
        {
            try
            {
                var fechaHora = ExecuteScalar("SELECT GETDATE()");
                return fechaHora != null ? Convert.ToDateTime(fechaHora) : DateTime.Now;
            }
            catch
            {
                return DateTime.Now;
            }
        }

        // Métodos para MaestroCodigoEtiqueta
        public static List<MaestroCodigoEtiqueta> GetMaestroCodigosEtiquetas()
        {
            var maestros = new List<MaestroCodigoEtiqueta>();

            try
            {
                string sql = @"
                    SELECT PART_ID, ID_ETIQUETA, NOMBRE, DESCRIPCION, FACTOR, COLOR, 
                           USER_1, SKU, TIPO_CODIGO, ACTIVO, FECHA_CREACION, USUARIO_CREACION,
                           FECHA_MODIFICACION, USUARIO_MODIFICACION, OBSERVACIONES,
                           VELOCIDAD_IMPRESION, TEMPERATURA_IMPRESION, REQUIERE_LOGO, NOMBRE_LOGO
                    FROM MAESTRO_COD_ETIQ 
                    ORDER BY PART_ID";

                var dataTable = ExecuteQuery(sql, "DatabaseConnection1");

                foreach (DataRow row in dataTable.Rows)
                {
                    var maestro = new MaestroCodigoEtiqueta
                    {
                        PartId = row["PART_ID"].ToString(),
                        UPC1 = row["ID_ETIQUETA"].ToString(),
                        UPC2 = row["NOMBRE"].ToString(),
                        Descripcion = row["DESCRIPCION"].ToString(),
                        TipoEtiqueta = row["TIPO_CODIGO"].ToString(),
                        ColorEtiqueta = row["COLOR"].ToString(),
                        Activo = row["ACTIVO"] != DBNull.Value ? Convert.ToBoolean(row["ACTIVO"]) : true,
                        FechaCreacion = row["FECHA_CREACION"] != DBNull.Value ? Convert.ToDateTime(row["FECHA_CREACION"]) : DateTime.Now,
                        UsuarioCreacion = row["USUARIO_CREACION"].ToString(),
                        FechaModificacion = row["FECHA_MODIFICACION"] as DateTime?,
                        UsuarioModificacion = row["USUARIO_MODIFICACION"].ToString(),
                        Observaciones = row["OBSERVACIONES"].ToString(),
                        VelocidadImpresionConfig = row["VELOCIDAD_IMPRESION"] != DBNull.Value ? Convert.ToInt32(row["VELOCIDAD_IMPRESION"]) : 4,
                        TemperaturaImpresion = row["TEMPERATURA_IMPRESION"] != DBNull.Value ? Convert.ToInt32(row["TEMPERATURA_IMPRESION"]) : 6,
                        RequiereLogo = row["REQUIERE_LOGO"] != DBNull.Value ? Convert.ToBoolean(row["REQUIERE_LOGO"]) : false,
                        NombreLogo = row["NOMBRE_LOGO"].ToString()
                    };

                    maestros.Add(maestro);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error obteniendo maestros de códigos: {ex.Message}", ex);
            }

            return maestros;
        }

        public static void InsertMaestroCodigoEtiqueta(MaestroCodigoEtiqueta maestro)
        {
            try
            {
                string sql = @"
                    INSERT INTO MAESTRO_COD_ETIQ 
                    (PART_ID, ID_ETIQUETA, NOMBRE, DESCRIPCION, FACTOR, COLOR, USER_1, SKU, TIPO_CODIGO,
                     ACTIVO, FECHA_CREACION, USUARIO_CREACION, VELOCIDAD_IMPRESION, TEMPERATURA_IMPRESION,
                     REQUIERE_LOGO, NOMBRE_LOGO, OBSERVACIONES)
                    VALUES 
                    (@PartId, @UPC1, @UPC2, @Descripcion, @Factor, @Color, @User1, @SKU, @TipoEtiqueta,
                     @Activo, @FechaCreacion, @UsuarioCreacion, @VelocidadImpresion, @TemperaturaImpresion,
                     @RequiereLogo, @NombreLogo, @Observaciones)";

                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@PartId", maestro.PartId),
                    new SqlParameter("@UPC1", maestro.UPC1 ?? ""),
                    new SqlParameter("@UPC2", maestro.UPC2 ?? ""),
                    new SqlParameter("@Descripcion", maestro.Descripcion ?? ""),
                    new SqlParameter("@Factor", ""),
                    new SqlParameter("@Color", maestro.ColorEtiqueta ?? ""),
                    new SqlParameter("@User1", ""),
                    new SqlParameter("@SKU", ""),
                    new SqlParameter("@TipoEtiqueta", maestro.TipoEtiqueta ?? ""),
                    new SqlParameter("@Activo", maestro.Activo),
                    new SqlParameter("@FechaCreacion", maestro.FechaCreacion),
                    new SqlParameter("@UsuarioCreacion", maestro.UsuarioCreacion ?? ""),
                    new SqlParameter("@VelocidadImpresion", maestro.VelocidadImpresionConfig),
                    new SqlParameter("@TemperaturaImpresion", maestro.TemperaturaImpresion),
                    new SqlParameter("@RequiereLogo", maestro.RequiereLogo),
                    new SqlParameter("@NombreLogo", maestro.NombreLogo ?? ""),
                    new SqlParameter("@Observaciones", maestro.Observaciones ?? "")
                };

                ExecuteNonQuery(sql, "DatabaseConnection1", parameters);

                // Log de la operación
                LogEtiquetaOperation("INSERT", $"Maestro creado: {maestro.PartId}", maestro.UsuarioCreacion);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error insertando maestro de código: {ex.Message}", ex);
            }
        }

        public static void UpdateMaestroCodigoEtiqueta(MaestroCodigoEtiqueta maestro)
        {
            try
            {
                string sql = @"
                    UPDATE MAESTRO_COD_ETIQ SET
                        ID_ETIQUETA = @UPC1,
                        NOMBRE = @UPC2,
                        DESCRIPCION = @Descripcion,
                        COLOR = @Color,
                        TIPO_CODIGO = @TipoEtiqueta,
                        ACTIVO = @Activo,
                        FECHA_MODIFICACION = @FechaModificacion,
                        USUARIO_MODIFICACION = @UsuarioModificacion,
                        VELOCIDAD_IMPRESION = @VelocidadImpresion,
                        TEMPERATURA_IMPRESION = @TemperaturaImpresion,
                        REQUIERE_LOGO = @RequiereLogo,
                        NOMBRE_LOGO = @NombreLogo,
                        OBSERVACIONES = @Observaciones
                    WHERE PART_ID = @PartId";

                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@PartId", maestro.PartId),
                    new SqlParameter("@UPC1", maestro.UPC1 ?? ""),
                    new SqlParameter("@UPC2", maestro.UPC2 ?? ""),
                    new SqlParameter("@Descripcion", maestro.Descripcion ?? ""),
                    new SqlParameter("@Color", maestro.ColorEtiqueta ?? ""),
                    new SqlParameter("@TipoEtiqueta", maestro.TipoEtiqueta ?? ""),
                    new SqlParameter("@Activo", maestro.Activo),
                    new SqlParameter("@FechaModificacion", maestro.FechaModificacion ?? DateTime.Now),
                    new SqlParameter("@UsuarioModificacion", maestro.UsuarioModificacion ?? ""),
                    new SqlParameter("@VelocidadImpresion", maestro.VelocidadImpresionConfig),
                    new SqlParameter("@TemperaturaImpresion", maestro.TemperaturaImpresion),
                    new SqlParameter("@RequiereLogo", maestro.RequiereLogo),
                    new SqlParameter("@NombreLogo", maestro.NombreLogo ?? ""),
                    new SqlParameter("@Observaciones", maestro.Observaciones ?? "")
                };

                ExecuteNonQuery(sql, "DatabaseConnection1", parameters);

                // Log de la operación
                LogEtiquetaOperation("UPDATE", $"Maestro actualizado: {maestro.PartId}", maestro.UsuarioModificacion);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error actualizando maestro de código: {ex.Message}", ex);
            }
        }

        private static void LogEtiquetaOperation(string operacion, string descripcion, string usuario)
        {
            try
            {
                string sql = @"
                    INSERT INTO ETIQUETAS_LOG (DESCRIPCION, ANTES, DESPUES, USUARIO, FECHA)
                    VALUES (@Descripcion, @Antes, @Despues, @Usuario, @Fecha)";

                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@Descripcion", operacion),
                    new SqlParameter("@Antes", ""),
                    new SqlParameter("@Despues", descripcion),
                    new SqlParameter("@Usuario", usuario ?? Environment.UserName),
                    new SqlParameter("@Fecha", DateTime.Now)
                };

                ExecuteNonQuery(sql, "DatabaseConnection1", parameters);
            }
            catch
            {
                // No fallar si no se puede escribir el log
            }
        }

        // Métodos para Solicitudes de Etiquetas
        public static List<SolicitudEtiqueta> GetSolicitudesEtiquetas(string tipoEtiqueta = null)
        {
            var solicitudes = new List<SolicitudEtiqueta>();

            try
            {
                string sql = @"
                    SELECT s.ID_ETIQUETA, s.PART_ID, s.ORDEN_FAB, s.FECHA_SOLICITUD, s.FECHA_REQUERIDA,
                           s.CANTIDAD_PEDIDA, s.COLOR, s.OBS, s.TIPO_CODIGO, s.USUARIO,
                           s.CANTIDAD_FABRICADA, s.FECHA_FABRICACION, s.NOMBRE_RETIRA, s.FECHA_ENTREGA,
                           m.DESCRIPCION, m.ID_ETIQUETA as UPC1, m.NOMBRE as UPC2
                    FROM SOLICITUD_ETIQUETAS s
                    LEFT JOIN MAESTRO_COD_ETIQ m ON s.PART_ID = m.PART_ID AND s.TIPO_CODIGO = m.TIPO_CODIGO
                    WHERE (@TipoEtiqueta IS NULL OR s.TIPO_CODIGO = @TipoEtiqueta)
                    ORDER BY s.FECHA_SOLICITUD DESC";

                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@TipoEtiqueta", (object)tipoEtiqueta ?? DBNull.Value)
                };

                var dataTable = ExecuteQuery(sql, "DatabaseConnection1", parameters);

                foreach (DataRow row in dataTable.Rows)
                {
                    var solicitud = new SolicitudEtiqueta
                    {
                        IdSolicitud = Convert.ToInt32(row["ID_ETIQUETA"]),
                        PartId = row["PART_ID"].ToString(),
                        OrdenFab = row["ORDEN_FAB"] != DBNull.Value ? Convert.ToInt32(row["ORDEN_FAB"]) : 0,
                        FechaSolicitud = Convert.ToDateTime(row["FECHA_SOLICITUD"]),
                        FechaRequerida = Convert.ToDateTime(row["FECHA_REQUERIDA"]),
                        CantidadPedida = Convert.ToInt32(row["CANTIDAD_PEDIDA"]),
                        Color = row["COLOR"].ToString(),
                        Observaciones = row["OBS"].ToString(),
                        TipoEtiqueta = row["TIPO_CODIGO"].ToString(),
                        Usuario = row["USUARIO"].ToString(),
                        CantidadFabricada = row["CANTIDAD_FABRICADA"] != DBNull.Value ? Convert.ToInt32(row["CANTIDAD_FABRICADA"]) : 0,
                        FechaFabricacion = row["FECHA_FABRICACION"] as DateTime?,
                        NombreRetira = row["NOMBRE_RETIRA"].ToString(),
                        FechaEntrega = row["FECHA_ENTREGA"] as DateTime?,
                        Descripcion = row["DESCRIPCION"].ToString(),
                        UPC1 = row["UPC1"].ToString(),
                        UPC2 = row["UPC2"].ToString()
                    };

                    solicitudes.Add(solicitud);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error obteniendo solicitudes de etiquetas: {ex.Message}", ex);
            }

            return solicitudes;
        }

        // Métodos para Órdenes de Fabricación  
        public static List<OrdenFabricacion> GetOrdenesFabricacion()
        {
            var ordenes = new List<OrdenFabricacion>();

            try
            {
                string sql = @"
                    SELECT w.BASE_ID, w.PART_ID, p.DESCRIPTION, w.QUANTITY, w.STATUS
                    FROM WORK_ORDER w
                    LEFT JOIN PART p ON w.PART_ID = p.ID
                    WHERE w.STATUS = 'ACTIVE'
                    ORDER BY w.BASE_ID";

                var dataTable = ExecuteQuery(sql, "DatabaseConnection2");

                foreach (DataRow row in dataTable.Rows)
                {
                    var orden = new OrdenFabricacion
                    {
                        BaseId = row["BASE_ID"].ToString(),
                        PartId = row["PART_ID"].ToString(),
                        Descripcion = row["DESCRIPTION"].ToString(),
                        Cantidad = row["QUANTITY"] != DBNull.Value ? Convert.ToInt32(row["QUANTITY"]) : 0,
                        Estado = row["STATUS"].ToString()
                    };

                    ordenes.Add(orden);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error obteniendo órdenes de fabricación: {ex.Message}", ex);
            }

            return ordenes;
        }
    }
}