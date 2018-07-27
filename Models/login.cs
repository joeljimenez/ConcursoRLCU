using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConcursoRLCU.Models
{
    public class Login
    {
        [Required(ErrorMessage = "Por favor ingrese su correo", AllowEmptyStrings = false)]
        public string correo { get; set; }

        [Required(ErrorMessage = "Por favor ingrese su contraseña", AllowEmptyStrings = false)]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
        public string Password { get; set; }

        public bool error { get; set; }
    }
}