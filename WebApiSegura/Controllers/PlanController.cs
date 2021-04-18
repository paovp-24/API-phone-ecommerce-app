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
    [RoutePrefix("api/plan")]
    public class PlanController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetId(int id)
        {
            Plan plan = new Plan();

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["SIUUU"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT PLAN_ID, TIPO_PLAN, TASA_INTERES FROM PLAN
                                                                WHERE PLAN_ID = @PLAN_ID", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@PLAN_ID", id);

                    sqlConnection.Open();

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    while (sqlDataReader.Read())
                    {
                        plan.PLAN_ID = sqlDataReader.GetInt32(0);
                        plan.TIPO_PLAN = sqlDataReader.GetString(1);
                        plan.TASA_INTERES = sqlDataReader.GetDouble(2);               

                    }

                    sqlConnection.Close();
                }

            }
            catch (Exception)
            {

                throw;
            }
            return Ok(plan);
        }

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            List<Plan> planes = new List<Plan>();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["SIUUU"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT PLAN_ID, TIPO_PLAN, TASA_INTERES FROM PLAN", sqlConnection);

                    sqlConnection.Open();

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    while (sqlDataReader.Read())
                    {

                        Plan plan = new Plan()
                        {
                            PLAN_ID = sqlDataReader.GetInt32(0),
                            TIPO_PLAN = sqlDataReader.GetString(1),
                            TASA_INTERES = sqlDataReader.GetDouble(2)

                        };

                        planes.Add(plan);
                    }


                    sqlConnection.Close();
                }

            }
            catch (Exception)
            {

                throw;
            }


            return Ok(planes);
        }

        [HttpPost]
        public IHttpActionResult Ingresar(Plan plan)
        {
            if (plan == null)
                return BadRequest();

            if (RegistrarPlan(plan))
                return Ok(plan);
            else
                return InternalServerError();
        }

        private bool RegistrarPlan(Plan plan)
        {
            bool resultado = false;

            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["SIUUU"].ConnectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(@"INSERT INTO PLAN (TIPO_PLAN, TASA_INTERES) 
                                                            VALUES (@TIPO_PLAN, @TASA_INTERES)", sqlConnection);

                sqlCommand.Parameters.AddWithValue("@TIPO_PLAN", plan.TIPO_PLAN);
                sqlCommand.Parameters.AddWithValue("@TASA_INTERES", plan.TASA_INTERES);
 
                sqlConnection.Open();

                int filasAfectadas = sqlCommand.ExecuteNonQuery();
                if (filasAfectadas > 0)
                    resultado = true;

                sqlConnection.Close();
            }

            return resultado;

        }

        [HttpPut]
        public IHttpActionResult Actualizar(Plan plan)
        {
            if (plan == null)
                return BadRequest();

            if (ActualizarPlan(plan))
                return Ok(plan);
            else
                return InternalServerError();
        }

        private bool ActualizarPlan(Plan plan)
        {
            bool resultado = false;

            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["SIUUU"].ConnectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(@"UPDATE PRODUCTO SET TIPO_PLAN = @TIPO_PLAN,
                                                                             TASA_INTERES = @TASA_INTERES                                                                               
                                                                             WHERE PLAN_ID = @PLAN_ID", sqlConnection);

                sqlCommand.Parameters.AddWithValue("@PLAN_ID", plan.PLAN_ID);
                sqlCommand.Parameters.AddWithValue("@TIPO_PLAN", plan.TIPO_PLAN);
                sqlCommand.Parameters.AddWithValue("@TASA_INTERES", plan.TASA_INTERES);

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

            if (EliminarPlan(id))
                return Ok(id);
            else
                return InternalServerError();
        }

        private bool EliminarPlan(int id)
        {
            bool resultado = false;

            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["SIUUU"].ConnectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(@"DELETE PLAN
                                                         WHERE PLAN_ID = @PLAN_ID", sqlConnection);

                sqlCommand.Parameters.AddWithValue("@PLAN_ID", id);

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