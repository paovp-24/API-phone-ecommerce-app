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
        [Authorize]
        [RoutePrefix("api/producto")]
        public class ProductoController : ApiController
        {
            [HttpGet]
            public IHttpActionResult GetId(int id)
            {
                Producto producto = new Producto();

                try
                {
                    using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["SIUUU"].ConnectionString))
                    {
                        SqlCommand sqlCommand = new SqlCommand(@"SELECT PRODUCTO_ID, NOMBRE, DETALLES, IMAGEN, 
                                                                GARANTIA, PRECIO, STOCK FROM FACTURA
                                                                WHERE PRODUCTO_ID = @PRODUCTO_ID", sqlConnection);

                        sqlCommand.Parameters.AddWithValue("@PRODUCTO_ID", id);

                        sqlConnection.Open();

                        SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                        while (sqlDataReader.Read())
                        {
                        producto.PRODUCTO_ID = sqlDataReader.GetInt32(0);
                        producto.NOMBRE = sqlDataReader.GetString(1);
                        producto.DETALLES = sqlDataReader.GetString(2);
                        producto.IMAGEN = sqlDataReader.GetString(3);
                        producto.GARANTIA = sqlDataReader.GetString(4);
                        producto.PRECIO = sqlDataReader.GetDouble(5);
                        producto.STOCK = sqlDataReader.GetInt32(6);

                    }

                        sqlConnection.Close();
                    }

                }
                catch (Exception)
                {

                    throw;
                }
                return Ok(producto);
            }

            [HttpGet]
            public IHttpActionResult GetAll()
            {
                List<Producto> productos = new List<Producto>();
                try
                {
                    using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["SIUUU"].ConnectionString))
                    {
                        SqlCommand sqlCommand = new SqlCommand(@"SELECT PRODUCTO_ID, NOMBRE, DETALLES,
                                                                 IMAGEN, GARANTIA, PRECIO, STOCK FROM PRODUCTO", sqlConnection);

                        sqlConnection.Open();

                        SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                        while (sqlDataReader.Read())
                        {

                        Producto producto = new Producto()
                        {
                            PRODUCTO_ID = sqlDataReader.GetInt32(0),
                            NOMBRE = sqlDataReader.GetString(1),
                            DETALLES = sqlDataReader.GetString(2),
                            IMAGEN = sqlDataReader.GetString(3),
                            GARANTIA = sqlDataReader.GetString(4),
                            PRECIO = sqlDataReader.GetDouble(5),
                            STOCK = sqlDataReader.GetInt32(6),

                        };

                        productos.Add(producto);
                        }


                        sqlConnection.Close();
                    }

                }
                catch (Exception)
                {

                    throw;
                }


                return Ok(productos);
            }

            [HttpPost]
            public IHttpActionResult Ingresar(Producto producto)
            {
                if (producto == null)
                    return BadRequest();

                if (RegistrarProducto(producto))
                    return Ok(producto);
                else
                    return InternalServerError();
            }

            private bool RegistrarProducto(Producto producto)
            {
                bool resultado = false;

                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["SIUUU"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"INSERT INTO PRODUCTO (NOMBRE, IMAGEN, GARANTIA, PRECIO, STOCK) 
                                                            VALUES (@NOMBRE, @DETALLES, @IMAGEN, @GARANTIA, @PRECIO, @STOCK)", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@NOMBRE", producto.NOMBRE);
                    sqlCommand.Parameters.AddWithValue("@DETALLES", producto.DETALLES);
                    sqlCommand.Parameters.AddWithValue("@IMAGEN", producto.IMAGEN);
                    sqlCommand.Parameters.AddWithValue("@GARANTIA", producto.GARANTIA);
                    sqlCommand.Parameters.AddWithValue("@PRECIO", producto.PRECIO);
                    sqlCommand.Parameters.AddWithValue("@STOCK", producto.STOCK);


                sqlConnection.Open();

                    int filasAfectadas = sqlCommand.ExecuteNonQuery();
                    if (filasAfectadas > 0)
                        resultado = true;

                    sqlConnection.Close();
                }

                return resultado;

            }

            [HttpPut]
            public IHttpActionResult Actualizar(Producto producto)
            {
                if (producto == null)
                    return BadRequest();

                if (ActualizarProducto(producto))
                    return Ok(producto);
                else
                    return InternalServerError();
            }

            private bool ActualizarProducto(Producto producto)
            {
                bool resultado = false;

                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["SIUUU"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"UPDATE PRODUCTO SET NOMBRE = @NOMBRE,
                                                                                DETALLES = @DETALLES 
                                                                                IMAGEN = @IMAGEN
                                                                                GARANTIA = @GARANTIA
                                                                                PRECIO = @PRECIO
                                                                                STOCK = @STOCK
                                                                                WHERE PRODUCTO_ID = @PRODUCTO_ID", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@PRODUCTO_ID", producto.PRODUCTO_ID);
                    sqlCommand.Parameters.AddWithValue("@NOMBRE", producto.NOMBRE);
                    sqlCommand.Parameters.AddWithValue("@DETALLES", producto.DETALLES);
                    sqlCommand.Parameters.AddWithValue("@IMAGEN", producto.IMAGEN);
                    sqlCommand.Parameters.AddWithValue("@GARANTIA", producto.GARANTIA);
                    sqlCommand.Parameters.AddWithValue("@PRECIO", producto.PRECIO);
                    sqlCommand.Parameters.AddWithValue("@STOCK", producto.STOCK);

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

                if (EliminarProducto(id))
                    return Ok(id);
                else
                    return InternalServerError();
            }

            private bool EliminarProducto(int id)
            {
                bool resultado = false;

                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["SIUUU"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"DELETE PRODUCTO
                                                            WHERE PRODUCTO_ID = @PRODUCTO_ID", sqlConnection);

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
