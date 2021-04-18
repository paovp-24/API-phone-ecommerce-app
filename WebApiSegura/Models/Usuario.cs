using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiSegura.Models
{
    public class Usuario
    {

        public int USUARIO_ID { get; set; }
        public string NOMBRE { get; set; }
        public string APELLIDOS { get; set; }
        public string IDENTIFICACION { get; set; }
        public int TELEFONO { get; set; }
        public string DIRECCION { get; set; }
        public string EMAIL { get; set; }
        public string PASSWORD { get; set; }
        public int ROL { get; set; }

        public string CadenaToken { set; get; }
    }
}