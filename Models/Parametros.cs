using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConcursoRLCU.Models
{
    public class Parametros
    {
        

        public int metodo { get; set; }
        public int consulta { get; set; }
        public int idparticipante { get; set; }
        public string nombre { get; set; }
        public string nombre2 { get; set; }
        public string apellido { get; set; }
        public int edad { get; set; }
        public string cedula { get; set; }
        public DateTime fecha_nac { get; set; }
        public string telofono { get; set; }
        public string celular { get; set; }
        public string email { get; set; }
        public string passw { get; set; }
        public string direccion { get; set; }
        public string imagen { get; set; }
        public int idpais { get; set; }
        public int iduniversidades { get; set; }
        public int es_jurado { get; set; }
        public int idsexo { get; set; }
        public int nivel { get; set; }
        public int idgaleria { get; set; }
        public int idconcurso { get; set; }
        public int idcategoria { get; set; }
        public string descripcion { get; set; }
        public string extencion { get; set; }
    }
}