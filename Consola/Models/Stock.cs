using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Consola.Models
{
    public class Stock
    {
        public int idStock { get; set; }

        public int codigo { get; set; }

        public string nombreProducto { get; set; }

        public int unidad { get; set; }

        public string codigoBodega { get; set; }

        public int idProveedor { get; set; }

        public bool estadoStock { get; set; }

    }
}