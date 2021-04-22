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
    [RoutePrefix("api/compra_producto")]
    public class Compra_productoController : ApiController
    {
        
            [HttpGet]
            public IHttpActionResult GetId(int id)
            {
                Compra_producto compra_Producto = new Compra_producto();

                try
                {
                    using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["SIUUU"].ConnectionString))
                    {
                        SqlCommand sqlCommand = new SqlCommand(@"SELECT FACTURA_ID, PRODUCTO_ID FROM COMPRA_PRODUCTO
                                                            WHERE FACTURA_ID = @FACTURA_ID", sqlConnection);

                        sqlCommand.Parameters.AddWithValue("@FACTURA_ID", id);

                        sqlConnection.Open();

                        SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                        while (sqlDataReader.Read())
                        {
                            compra_Producto.FACTURA_ID = sqlDataReader.GetInt32(0);
                            compra_Producto.PRODUCTO_ID = sqlDataReader.GetInt32(1);

                        }

                        sqlConnection.Close();
                    }

                }
                catch (Exception)
                {

                    throw;
                }
                return Ok(compra_Producto);
            }

            [HttpGet]
            public IHttpActionResult GetAll()
            {
                List<Compra_producto> compra_productos = new List<Compra_producto>();
                try
                {
                    using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["SIUUU"].ConnectionString))
                    {
                        SqlCommand sqlCommand = new SqlCommand(@"SELECT FACTURA_ID, PRODUCTO_ID FROM COMPRA_PRODUCTO", sqlConnection);

                        sqlConnection.Open();

                        SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                        while (sqlDataReader.Read())
                        {
                            Compra_producto compra_producto = new Compra_producto()
                            {
                                FACTURA_ID = sqlDataReader.GetInt32(0),
                                PRODUCTO_ID = sqlDataReader.GetInt32(1)
                            };

                            compra_productos.Add(compra_producto);
                        }


                        sqlConnection.Close();
                    }

                }
                catch (Exception)
                {

                    throw;
                }


                return Ok(compra_productos);
            }

            [HttpPost]
            public IHttpActionResult Ingresar(Compra_producto compra_producto)
            {
                if (compra_producto == null)
                    return BadRequest();

                if (RegistrarCompraProducto(compra_producto))
                    return Ok(compra_producto);
                else
                    return InternalServerError();
            }

            private bool RegistrarCompraProducto(Compra_producto compra_producto)
            {
                bool resultado = false;

                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["SIUUU"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"INSERT INTO COMPRA_PRODUCTO (FACTURA_ID, PRODUCTO_ID) VALUES (@FACTURA_ID, @PRODUCTO_ID)", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@FACTURA_ID", compra_producto.FACTURA_ID);
                    sqlCommand.Parameters.AddWithValue("@PRODUCTO_ID", compra_producto.PRODUCTO_ID);


                    sqlConnection.Open();

                    int filasAfectadas = sqlCommand.ExecuteNonQuery();
                    if (filasAfectadas > 0)
                        resultado = true;

                    sqlConnection.Close();
                }

                return resultado;

            }

            [HttpPut]
            public IHttpActionResult Actualizar(Compra_producto compra_producto)
            {
                if (compra_producto == null)
                    return BadRequest();

                if (ActualizarCompra_producto(compra_producto))
                    return Ok(compra_producto);
                else
                    return InternalServerError();
            }

            private bool ActualizarCompra_producto(Compra_producto compra_producto)
            {
                bool resultado = false;

                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["SIUUU"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"UPDATE COMPRA_PRODUCTO SET PRODUCTO_ID = @PRODUCTO_ID,                                                                           
                                                                      WHERE FACTURA_ID = @FACTURA_ID AND PRODUCTO_ID = @PRODUCTO_ID ", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@FACTURA_ID", compra_producto.FACTURA_ID);
                    sqlCommand.Parameters.AddWithValue("@PRODUCTO_ID", compra_producto.PRODUCTO_ID);

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

                if (EliminarCompra_producto(id))
                    return Ok(id);
                else
                    return InternalServerError();
            }

            private bool EliminarCompra_producto(int id)
            {
                bool resultado = false;

                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["SIUUU"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"DELETE COMPRA_PRODUCTO
                                                         WHERE FACTURA_ID = @FACTURA_ID AND PRODUCTO_ID = @PRODUCTO_ID", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@FACTURA_ID", id);
                    sqlCommand.Parameters.AddWithValue("@PRODUCTO_ID", id);

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