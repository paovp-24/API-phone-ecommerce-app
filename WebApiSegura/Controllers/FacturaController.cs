using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiSegura.Models;

namespace WebApiSegura.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("api/factura")]
    public class FacturaController : ApiController
    {
            [HttpGet]
            public IHttpActionResult GetId(int id)
            {
                Factura factura = new Factura();

                try
                {
                    using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["SIUUU"].ConnectionString))
                    {
                        SqlCommand sqlCommand = new SqlCommand(@"SELECT FACTURA_ID, USUARIO_ID, PLAN_ID, MONTO_FACTURA, 
                                                                CANT_PRODUCTOS, ESTADO, PAGO_MENSUAL FROM FACTURA
                                                                WHERE FACTURA_ID = @FACTURA_ID", sqlConnection);

                        sqlCommand.Parameters.AddWithValue("@FACTURA_ID", id);

                        sqlConnection.Open();

                        SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                        while (sqlDataReader.Read())
                        {
                            factura.FACTURA_ID = sqlDataReader.GetInt32(0);
                            factura.USUARIO_ID = sqlDataReader.GetInt32(1);
                            factura.PLAN_ID = sqlDataReader.GetInt32(2);
                            factura.MONTO_FACTURA = sqlDataReader.GetDouble(3);
                            factura.CANT_PRODUCTOS = sqlDataReader.GetInt32(4);
                            factura.ESTADO = sqlDataReader.GetString(5);
                            factura.PAGO_MENSUAL = sqlDataReader.GetDouble(6);

                    }

                        sqlConnection.Close();
                    }

                }
                catch (Exception)
                {

                    throw;
                }
                return Ok(factura);
            }

        [HttpGet]
        [Route("getLastID")]
        public IHttpActionResult getLatestId()
        {
            Factura factura = new Factura();

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["SIUUU"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT FACTURA_ID FROM FACTURA WHERE FACTURA_ID = (SELECT IDENT_CURRENT('FACTURA'))", sqlConnection);

                    sqlConnection.Open();

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    while (sqlDataReader.Read())
                    {
                        factura.FACTURA_ID = sqlDataReader.GetInt32(0);

                    }

                    sqlConnection.Close();
                }

            }
            catch (Exception)
            {

                throw;
            }
            return Ok(factura);
        }


        [HttpGet]
            public IHttpActionResult GetAll()
            {
                List<Factura> facturas = new List<Factura>();
                try
                {
                    using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["SIUUU"].ConnectionString))
                    {
                        SqlCommand sqlCommand = new SqlCommand(@"SELECT FACTURA_ID, USUARIO_ID, PLAN_ID,
                                                                 MONTO_FACTURA, CANT_PRODUCTOS, PAGO_MENSUAL FROM FACTURA
                                                                 USUARIO_ID = @USUARIO_ID   ", sqlConnection);

                        sqlConnection.Open();

                        SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                        while (sqlDataReader.Read())
                        {
                            Factura factura = new Factura()
                            {
                                FACTURA_ID = sqlDataReader.GetInt32(0),
                                USUARIO_ID = sqlDataReader.GetInt32(1),
                                PLAN_ID = sqlDataReader.GetInt32(2),
                                MONTO_FACTURA = sqlDataReader.GetDouble(3),
                                CANT_PRODUCTOS = sqlDataReader.GetInt32(4),
                                ESTADO = sqlDataReader.GetString(5),
                                PAGO_MENSUAL = sqlDataReader.GetDouble(6)
                    };

                            facturas.Add(factura);
                        }


                        sqlConnection.Close();
                    }

                }
                catch (Exception)
                {

                    throw;
                }


                return Ok(facturas);
            }

            
            
            

            [HttpPost]
            public IHttpActionResult Ingresar(Factura factura)
            {
                if (factura == null)
                    return BadRequest();

                if (RegistrarFactura(factura))
                    return Ok(factura);
                else
                    return InternalServerError();
            }

            private bool RegistrarFactura(Factura factura)
            {
                bool resultado = false;

                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["SIUUU"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"INSERT INTO FACTURA (USUARIO_ID, PLAN_ID, MONTO_FACTURA, CANT_PRODUCTOS, ESTADO, PAGO_MENSUAL) 
                                                            VALUES (@USUARIO_ID, @PLAN_ID, @MONTO_FACTURA, @CANT_PRODUCTOS, @ESTADO, @PAGO_MENSUAL)", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@USUARIO_ID", factura.USUARIO_ID);
                    sqlCommand.Parameters.AddWithValue("@PLAN_ID", factura.PLAN_ID);
                    sqlCommand.Parameters.AddWithValue("@MONTO_FACTURA", factura.MONTO_FACTURA);
                    sqlCommand.Parameters.AddWithValue("@CANT_PRODUCTOS", factura.CANT_PRODUCTOS);
                    sqlCommand.Parameters.AddWithValue("@ESTADO", factura.ESTADO);
                    sqlCommand.Parameters.AddWithValue("@PAGO_MENSUAL", factura.PAGO_MENSUAL);


                sqlConnection.Open();

                    int filasAfectadas = sqlCommand.ExecuteNonQuery();
                    if (filasAfectadas > 0)
                        resultado = true;

                    sqlConnection.Close();
                }

                return resultado;

            }

            [HttpPut]
            public IHttpActionResult Actualizar(Factura factura)
            {
                if (factura == null)
                    return BadRequest();

                if (ActualizarFactura(factura))
                    return Ok(factura);
                else
                    return InternalServerError();
            }

            private bool ActualizarFactura(Factura factura)
            {
                bool resultado = false;

                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["SIUUU"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"UPDATE FACTURA SET USUARIO_ID = @USUARIO_ID,
                                                                                PLAN_ID = @PLAN_ID 
                                                                                MONTO_FACTURA = @MONTO_FACTURA
                                                                                CANT_PRODUCTOS = @CANT_PRODUCTOS
                                                                                ESTADO = @ESTADO
                                                                                PAGO_MENSUAL = @PAGO_MENSUAL
                                                                                WHERE FACTURA_ID = @FACTURA_ID", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@FACTURA_ID", factura.FACTURA_ID);
                    sqlCommand.Parameters.AddWithValue("@USUARIO_ID", factura.USUARIO_ID);
                    sqlCommand.Parameters.AddWithValue("@PLAN_ID", factura.PLAN_ID);
                    sqlCommand.Parameters.AddWithValue("@MONTO_FACTURA", factura.MONTO_FACTURA);
                    sqlCommand.Parameters.AddWithValue("@CANT_PRODUCTOS", factura.CANT_PRODUCTOS);
                    sqlCommand.Parameters.AddWithValue("@ESTADO", factura.ESTADO);
                    sqlCommand.Parameters.AddWithValue("@PAGO_MENSUAL", factura.PAGO_MENSUAL);

                sqlConnection.Open();

                    int filasAfectadas = sqlCommand.ExecuteNonQuery();
                    if (filasAfectadas > 0)
                        resultado = true;

                    sqlConnection.Close();
                }

                return resultado;

            }

            [HttpDelete]
            public IHttpActionResult Eliminar(int id)
            {
                if (id < 1)
                    return BadRequest();

                if (EliminarFactura(id))
                    return Ok(id);
                else
                    return InternalServerError();
            }

            private bool EliminarFactura(int id)
            {
                bool resultado = false;

                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["SIUUU"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"DELETE FACTURA
                                                            WHERE FACTURA_ID = @FACTURA_ID", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@FACTURA_ID", id);

                    sqlConnection.Open();

                    int filasAfectadas = sqlCommand.ExecuteNonQuery();
                    if (filasAfectadas > 0)
                        resultado = true;

                    sqlConnection.Close();
                }

                return resultado;

            }
        }

        }