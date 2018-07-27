using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ConcursoRLCU.Models
{
    public class respuesta
    {
        public DataTable data { get; set; }

        public string numero { get; set; }

        public bool exito { get; set; }

        public string mensaje { get; set; }

    }
}