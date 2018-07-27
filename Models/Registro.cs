using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConcursoRLCU.Models
{
    public class Registro
    {
        [Required(ErrorMessage = "Por favor ingrese su nombre", AllowEmptyStrings = false)]
        public string nombre { get; set; }

        [Required(ErrorMessage = "Por favor ingrese su apellido", AllowEmptyStrings = false)]
        public string apellido { get; set; }

        [Required(ErrorMessage = "Por favor ingrese su correo", AllowEmptyStrings = false)]
        public string correo { get; set; }

        [Required(ErrorMessage = "Por favor ingrese su contraseña", AllowEmptyStrings = false)]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Por favor repita su contraseña", AllowEmptyStrings = false)]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
        public string Password2 { get; set; }

        public bool error { get; set; }
    }
}