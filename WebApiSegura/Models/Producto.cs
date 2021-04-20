using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiSegura.Models
{
    public class Producto
    {
        public int PRODUCTO_ID { get; set; }
        public string NOMBRE { get; set; }
        public string DETALLES { get; set; }
        public string IMAGEN { get; set; }
        public string GARANTIA { get; set; }
        public decimal PRECIO { get; set; }
        public int STOCK { get; set; }

    }
}