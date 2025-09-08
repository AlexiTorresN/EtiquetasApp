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
                        OrdenFab = row["Orden_Fab"].ToString(),
                        Descripcion = row["Descripcion"].ToString(),
                        CantidadPedida = Convert.ToInt32(row["Cantidad_Pedida"]),
                        CantidadFabricada = row["Cantidad_Fabricada"] != DBNull.Value ? Convert.ToInt32(row["Cantidad_Fabricada"]) : 0,
                        Color = row["Color"].ToString(),
                        Observaciones = row["Observaciones"].ToString(),
                        FechaSolicitud = Convert.ToDateTime(row["Fecha_Solicitud"]),
                        FechaRequerida = Convert.ToDateTime(row["Fecha_Requerida"]),
                        FechaFabricacion = row["Fecha_Fabricacion"] != DBNull.Value ? Convert.ToDateTime(row["Fecha_Fabricacion"]) : DateTime.MinValue
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
                    WO.BASE_ID as OrdenFab,
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
                var dataTable = ExecuteQuery(sql, "DatabaseConnection2"); // Asumiendo que las órdenes están en la segunda BD
                foreach (DataRow row in dataTable.Rows)
                {
                    ordenes.Add(new OrdenFabricacion
                    {
                        OrdenFab = row["OrdenFab"].ToString(),
                        Cantidad = Convert.ToInt32(row["Cantidad"]),
                        Descripcion = row["Descripcion"].ToString(),
                        PartId = row["PartId"].ToString(),
                        FechaRequerida = Convert.ToDateTime(row["FechaRequerida"]),
                        FechaInicio = Convert.ToDateTime(row["FechaInicio"])
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
                new SqlParameter("@Descripcion", solicitud.Descripcion),
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

        public static List<MaestroCodigoEtiqueta> GetMaestroCodigosEtiquetas()
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
    }
}