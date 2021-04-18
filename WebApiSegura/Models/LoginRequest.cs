using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiSegura.Models
{
    public class LoginRequest
    {
        public string Correo { get; set; }
        public string Password { get; set; }
    }
}