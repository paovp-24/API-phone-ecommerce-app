using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiSegura.Models
{
    public class Plan
    {
        public int PLAN_ID { get; set; }
        public string TIPO_PLAN { get; set; }
        public double TASA_INTERES { get; set; }
    }
}