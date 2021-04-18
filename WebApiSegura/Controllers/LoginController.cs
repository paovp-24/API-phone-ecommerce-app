using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Net;
using System.Threading;
using System.Web.Http;
using WebApiSegura.Models;

namespace WebApiSegura.Controllers
{
    /// <summary>
    /// login controller class for authenticate users
    /// </summary>
    [AllowAnonymous]
    [RoutePrefix("api/login")]
    public class LoginController : ApiController
    {
        [HttpGet]
        [Route("echoping")]
        public IHttpActionResult EchoPing()
        {
            return Ok(true);
        }

        [HttpGet]
        [Route("echouser")]
        public IHttpActionResult EchoUser()
        {
            var identity = Thread.CurrentPrincipal.Identity;
            return Ok($" IPrincipal-user: {identity.Name} - IsAuthenticated: {identity.IsAuthenticated}");
        }

        [HttpPost]
        [Route("authenticate")]
        public IHttpActionResult Authenticate(LoginRequest login)
        {
            if (login == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            Usuario usuarioValidado = ValidarUsuario(login);
            if (!string.IsNullOrEmpty(usuarioValidado.IDENTIFICACION))
            {
                var token = TokenGenerator.GenerateTokenJwt(login.Username);
                usuarioValidado.CadenaToken = token;
                return Ok(usuarioValidado);
            }
            else
            {
                return Unauthorized();
            }
        }

        private Usuario ValidarUsuario(LoginRequest loginRequest)
        {
            Usuario usuario = new Usuario();

            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["SIUUU"].ConnectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(@"SELECT USUARIO_ID, NOMBRE, APELLIDOS, 
                                                        IDENTIFICACION, TELEFONO, FEC_NAC, DIRECCION, EMAIL, PASSWORD, ROL
                                                        FROM USUARIO
                                                        WHERE IDENTIFICACION = @IDENTIFICACION", sqlConnection);
                sqlCommand.Parameters.AddWithValue("@IDENTIFICACION", loginRequest.Username);

                sqlConnection.Open();

                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                while (sqlDataReader.Read())
                {
                    if (loginRequest.Password.Equals(sqlDataReader.GetString(8)))
                    {
                        usuario.USUARIO_ID = sqlDataReader.GetInt32(0);
                        usuario.NOMBRE = sqlDataReader.GetString(1);
                        usuario.APELLIDOS = sqlDataReader.GetString(2);
                        usuario.IDENTIFICACION = sqlDataReader.GetString(3);
                        usuario.TELEFONO = sqlDataReader.GetInt32(4);
                        usuario.FEC_NAC = sqlDataReader.GetDateTime(5);
                        usuario.DIRECCION = sqlDataReader.GetString(6);
                        usuario.EMAIL = sqlDataReader.GetString(7);
                        usuario.PASSWORD = sqlDataReader.GetString(8);
                        usuario.ROL = sqlDataReader.GetInt32(9);

                    }
                }

                sqlConnection.Close();
            }
            return usuario;
        }

        [HttpPost]
        [Route("register")]
        public IHttpActionResult Register(Usuario usuario)
        {

            if (usuario == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["SIUUU"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"INSERT INTO USUARIO (NOMBRE, APELLIDOS, 
                                                        IDENTIFICACION, TELEFONO, FEC_NAC, DIRECCION, EMAIL, PASSWORD,ROL)
                                                        VALUES(@NOMBRE, @APELLIDOS, 
                                                       @IDENTIFICACION, @TELEFONO, @FEC_NAC, @DIRECCION, @EMAIL, @PASSWORD,@ROL)", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@NOMBRE", usuario.NOMBRE);
                    sqlCommand.Parameters.AddWithValue("@APELLIDOS", usuario.APELLIDOS);
                    sqlCommand.Parameters.AddWithValue("@IDENTIFICACION", usuario.IDENTIFICACION);
                    sqlCommand.Parameters.AddWithValue("@TELEFONO", usuario.TELEFONO);
                    sqlCommand.Parameters.AddWithValue("@FEC_NAC", usuario.FEC_NAC);
                    sqlCommand.Parameters.AddWithValue("@DIRECCION", usuario.DIRECCION);
                    sqlCommand.Parameters.AddWithValue("@EMAIL", usuario.EMAIL);
                    sqlCommand.Parameters.AddWithValue("@PASSWORD", usuario.PASSWORD);
                    sqlCommand.Parameters.AddWithValue("@ROL", usuario.ROL);                   

                    sqlConnection.Open();

                    int filasAfectadas = sqlCommand.ExecuteNonQuery();
                    if (filasAfectadas < 0)
                        return InternalServerError();

                    sqlConnection.Close();
                }

            }
            catch (Exception)
            {

                throw;
            }

            return Ok(usuario);
        }






    }
}