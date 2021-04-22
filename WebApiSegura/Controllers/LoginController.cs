using System;
using System.Collections.Generic;
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
        //====================================================
        //GET
        //====================================================

        //Consigue el usuarioID por correo y clave iguales
        [HttpGet]
        [Route("getID")]
        public IHttpActionResult GetId(string correo, string clave)
        {
            UsuarioId usuario = new UsuarioId();

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["SIUUU"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT USUARIO_ID FROM USUARIO WHERE EMAIL = @EMAIL AND PASSWORD = @PASSWORD ", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@EMAIL", correo);
                    sqlCommand.Parameters.AddWithValue("@PASSWORD", clave);



                    sqlConnection.Open();

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    while (sqlDataReader.Read())
                    {
                        usuario.usuario_Id = sqlDataReader.GetInt32(0);

                    }

                    sqlConnection.Close();
                }

            }
            catch (Exception)
            {

                throw;
            }
            return Ok(usuario);
        }


        //Consigue todos los usuarios
        [HttpGet]
        [Route("allUser")]
        public IHttpActionResult GetAll()
        {
            List<Usuario> usuarios = new List<Usuario>();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["SIUUU"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT USUARIO_ID, NOMBRE, APELLIDOS, IDENTIFICACION, TELEFONO, DIRECCION, EMAIL, PASSWORD, ROL FROM USUARIO", sqlConnection);

                    sqlConnection.Open();

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    while (sqlDataReader.Read())
                    {

                        Usuario usuario = new Usuario()
                        {
                        USUARIO_ID = sqlDataReader.GetInt32(0),
                        NOMBRE = sqlDataReader.GetString(1),
                        APELLIDOS = sqlDataReader.GetString(2),
                        IDENTIFICACION = sqlDataReader.GetString(3),
                        TELEFONO = sqlDataReader.GetInt32(4),
                        DIRECCION = sqlDataReader.GetString(5),
                        EMAIL = sqlDataReader.GetString(6),
                        PASSWORD = sqlDataReader.GetString(7),
                        ROL = sqlDataReader.GetInt32(8)

                    };
                        usuarios.Add(usuario);
                    }

                    sqlConnection.Close();
                }

            }
            catch (Exception)
            {

                throw;
            }
            return Ok(usuarios);
        }

        //====================================================
        //POST
        //====================================================
        [HttpPost]
        [Route("authenticate")]
        public IHttpActionResult Authenticate(LoginRequest login)
        {
            if (login == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            Usuario usuarioValidado = ValidarUsuario(login);
            if (!string.IsNullOrEmpty(usuarioValidado.IDENTIFICACION))
            {
                return Ok(usuarioValidado);
            }
            else
            {
                return Unauthorized();
            }
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
                                                        IDENTIFICACION, TELEFONO, DIRECCION, EMAIL, PASSWORD,ROL)
                                                        VALUES(@NOMBRE, @APELLIDOS, 
                                                       @IDENTIFICACION, @TELEFONO, @DIRECCION, @EMAIL, @PASSWORD,@ROL)", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@NOMBRE", usuario.NOMBRE);
                    sqlCommand.Parameters.AddWithValue("@APELLIDOS", usuario.APELLIDOS);
                    sqlCommand.Parameters.AddWithValue("@IDENTIFICACION", usuario.IDENTIFICACION);
                    sqlCommand.Parameters.AddWithValue("@TELEFONO", usuario.TELEFONO);
                    sqlCommand.Parameters.AddWithValue("@DIRECCION", usuario.DIRECCION);
                    sqlCommand.Parameters.AddWithValue("@EMAIL", usuario.EMAIL);
                    sqlCommand.Parameters.AddWithValue("@PASSWORD", usuario.PASSWORD);
                    sqlCommand.Parameters.AddWithValue("@ROL", 0);                   

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

        //====================================================
        //Metodos
        //====================================================

        private Usuario ValidarUsuario(LoginRequest loginRequest)
        {

            Usuario usuario = new Usuario();

            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["SIUUU"].ConnectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(@"SELECT USUARIO_ID, NOMBRE, APELLIDOS, 
                                                        IDENTIFICACION, TELEFONO, DIRECCION, EMAIL, PASSWORD, ROL
                                                        FROM USUARIO
                                                        WHERE EMAIL = @EMAIL", sqlConnection);
                sqlCommand.Parameters.AddWithValue("@EMAIL", loginRequest.Correo);

                sqlConnection.Open();

                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                while (sqlDataReader.Read())
                {
                    if (loginRequest.Password.Equals(sqlDataReader.GetString(7)))
                    {
                        usuario.USUARIO_ID = sqlDataReader.GetInt32(0);
                        usuario.NOMBRE = sqlDataReader.GetString(1);
                        usuario.APELLIDOS = sqlDataReader.GetString(2);
                        usuario.IDENTIFICACION = sqlDataReader.GetString(3);
                        usuario.TELEFONO = sqlDataReader.GetInt32(4);
                        usuario.DIRECCION = sqlDataReader.GetString(5);
                        usuario.EMAIL = sqlDataReader.GetString(6);
                        usuario.PASSWORD = sqlDataReader.GetString(7);
                        usuario.ROL = sqlDataReader.GetInt32(8);

                    }
                }

                sqlConnection.Close();
            }

            return usuario;
                  
        }

    }

}