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
    [RoutePrefix("api/cuota")]
    public class CuotaController : ApiController
    {
        [HttpGet]
        [Route("cuotaID")]
        public IHttpActionResult GetId(int id)
        {
            Cuota cuota = new Cuota();

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["SIUUU"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT CUOTA_ID, TIPO_CUOTA, TASA_INTERES FROM CUOTA
                                                                WHERE CUOTA_ID = @CUOTA_ID", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@CUOTA_ID", id);

                    sqlConnection.Open();

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    while (sqlDataReader.Read())
                    {
                        cuota.CUOTA_ID = sqlDataReader.GetInt32(0);
                        cuota.TIPO_CUOTA = sqlDataReader.GetString(1);
                        cuota.TASA_INTERES = sqlDataReader.GetDecimal(2);               

                    }

                    sqlConnection.Close();
                }

            }
            catch (Exception)
            {

                throw;
            }
            return Ok(cuota);
        }

        [HttpGet]
        [Route("allCuota")]
        public IHttpActionResult GetAll()
        {
            List<Cuota> cuotas = new List<Cuota>();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["SIUUU"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT CUOTA_ID, TIPO_CUOTA, TASA_INTERES FROM CUOTA", sqlConnection);

                    sqlConnection.Open();

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    while (sqlDataReader.Read())
                    {

                        Cuota cuota = new Cuota()
                        {
                            CUOTA_ID = sqlDataReader.GetInt32(0),
                            TIPO_CUOTA = sqlDataReader.GetString(1),
                            TASA_INTERES = sqlDataReader.GetDecimal(2)

                        };

                        cuotas.Add(cuota);
                    }


                    sqlConnection.Close();
                }

            }
            catch (Exception)
            {

                throw;
            }


            return Ok(cuotas);
        }

    }
}