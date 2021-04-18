using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiSegura.Models
{
    public class Factura
    {
        public int FACTURA_ID { get; set; }
        public int USUARIO_ID { get; set; }
        public int PLAN_ID { get; set; }
        public double MONTO_FACTURA { get; set; }
        public int CANT_PRODUCTOS { get; set; }
        public string ESTADO { get; set; }
    }
}
