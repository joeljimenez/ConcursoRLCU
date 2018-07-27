using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConcursoRLCU.Models
{
    public class password
    {
        [Required(ErrorMessage = "Por favor ingrese su correo", AllowEmptyStrings = false)]
        public string correo { get; set; }
    }
}