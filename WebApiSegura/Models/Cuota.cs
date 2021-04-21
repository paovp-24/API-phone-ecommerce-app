using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiSegura.Models
{
    public class Cuota
    {
        public int CUOTA_ID { get; set; }
        public string TIPO_CUOTA { get; set; }
        public decimal TASA_INTERES { get; set; }
    }
}