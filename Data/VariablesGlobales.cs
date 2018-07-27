using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ConcursoRLCU.Data
{
    class VariablesGlobales
    {
        public consultas cmd;
        public accesoDatos.Utilidades utl = new accesoDatos.Utilidades();
        public accesoDatos.correo correo = new accesoDatos.correo();
        public accesoDatos.Encriptacion encrip = new accesoDatos.Encriptacion();
        //public accesoDatos.Active active;
        public DataTable dt = new DataTable();
        public DataTable dt2 = new DataTable();
        public DataSet ds = new DataSet();

        public string clave = "IIConcurso2017RLCU";
        public string cedula { get; set; }
        public string sede { get; set; }
        public string dominio { get; set; }
        public string usuario { get; set; }
        public string pw { get; set; }
        public string path { get; set; }


        public string puerto { get; set; }
        public string datos { get; set; }
        public string usuariodb { get; set; }
        public string contraseñadb { get; set; }
        public string server { get; set; }
    }
}