using accesoDatos;
using ConcursoRLCU.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace ConcursoRLCU.Data
{
    class consultas : VariablesGlobales
    {
        #region coneccion
        internal bool exito = true;
        internal string mensaje = "";
        public string NumError = "0";

        mySqlQuery MY;
  
        public string servidor = "";

        DataTable DT = new DataTable();
        DataSet DS = new DataSet();

        string DB = "";
        internal consultas(string sede)
        {                    
            carga_coneccion("1", "008");
            MY = new mySqlQuery(server, puerto, datos, usuariodb, contraseñadb);
           
        }



        internal void carga_coneccion(string id, string sede)
        {
            XDocument miXML = XDocument.Load(ConfigurationManager.ConnectionStrings["database"].ConnectionString);

            var data = from coneccion in miXML.Descendants("coneccion")
                       where coneccion.Element("id").Value == id.Trim() && coneccion.Element("sede").Value == sede.Trim() //Consultamos por el atributo
                       select new
                       {
                           server = coneccion.Element("server").Value,
                           puerto = coneccion.Element("puerto").Value,
                           datos = coneccion.Element("datos").Value,
                           usuario = coneccion.Element("usuario").Value,
                           contraseña = coneccion.Element("contraseña").Value,
                       };
            int cont = 0;
            foreach (var link in data)
            {
                cont++;

                server = link.server.ToString().Trim();
                puerto = link.puerto.ToString().Trim();
                datos = link.datos.ToString().Trim();
                usuariodb = link.usuario.ToString().Trim();
                contraseñadb = link.contraseña.ToString().Trim();
            }

            if (cont == 0)
            {

            }

        }

        internal void empaqueta_error_consulta(string error = " Intente más tarde no se ha podido acceder la BD  ", string num = "0")
        {
            this.exito = false;
            this.NumError = num;
            this.mensaje = error + this.DB;
        }


        #endregion

        #region consultas a Db 
        internal DataTable consulta_sp_participantes(Parametros r)
        {
               
            MY._spConsulta("sp_participantes");
           
            MY._spParametros("@_consulta", MySqlDbType.Int32, r.consulta);
            MY._spParametros("@_idparticipante", MySqlDbType.Int32, r.idparticipante);
            MY._spParametros("@_idconcurso", MySqlDbType.Int32, r.idconcurso);
            MY._spParametros("@_nombre", MySqlDbType.VarChar, r.nombre);
            MY._spParametros("@_apellido", MySqlDbType.VarChar, r.apellido);
            MY._spParametros("@_edad", MySqlDbType.Int32, r.edad);
            MY._spParametros("@_cedula", MySqlDbType.VarChar, r.cedula);
            MY._spParametros("@_fecha_nac", MySqlDbType.Date, Convert.ToDateTime(r.fecha_nac));
            MY._spParametros("@_telofono", MySqlDbType.VarChar, r.telofono);
            MY._spParametros("@_celular", MySqlDbType.VarChar, r.celular);
            MY._spParametros("@_email", MySqlDbType.VarChar, r.email);
            MY._spParametros("@_passw", MySqlDbType.VarChar, r.passw);
            MY._spParametros("@_direccion", MySqlDbType.VarChar, r.direccion);
            MY._spParametros("@_imagen", MySqlDbType.LongText, r.imagen);
            MY._spParametros("@_idpais", MySqlDbType.Int32, r.idpais);
            MY._spParametros("@_iduniversidades", MySqlDbType.Int32, r.iduniversidades);
            MY._spParametros("@_es_jurado", MySqlDbType.Int32, r.es_jurado);
            MY._spParametros("@_idsexo", MySqlDbType.Int32, r.idsexo);

            if (MY.estado_cn == true)
            {
                DT = MY._spTable();

                if (MY.error != "")
                {
                    empaqueta_error_consulta(MY.error, this.MY.NumError);
                }
            }
            else
            {
                DB = "MY";
                empaqueta_error_consulta();
            }

            return DT;
        }
        internal DataTable consulta_sp_parametros(Parametros r)
        {
            MY._spConsulta("sp_parametros");

            MY._spParametros("@_consulta", MySqlDbType.Int32, r.consulta);
            MY._spParametros("@_idpais", MySqlDbType.Int32, r.idpais);
            MY._spParametros("@_idconcurso", MySqlDbType.Int32, r.idconcurso);

            if (MY.estado_cn == true)
            {         
                DT = MY._spTable();

                if (MY.error != "")
                {
                    empaqueta_error_consulta(MY.error, this.MY.NumError);
                }
            }
            else
            {
                DB = "MY";
                empaqueta_error_consulta();
            }

            return DT;
        }


        internal DataTable consulta_sp_galeria(Parametros r)
        {
            MY._spConsulta("sp_galeria");

            MY._spParametros("@_consulta", MySqlDbType.Int32, r.consulta);
            MY._spParametros("@_idgaleria", MySqlDbType.Int32, r.idgaleria);
            MY._spParametros("@_idconcurso", MySqlDbType.Int32, r.idconcurso);
            MY._spParametros("@_idparticipante", MySqlDbType.Int32, r.idparticipante);
            MY._spParametros("@_idcategoria", MySqlDbType.Int32, r.idcategoria);
            MY._spParametros("@_nombre", MySqlDbType.VarChar, r.nombre);
            MY._spParametros("@_descripcion", MySqlDbType.VarChar, r.descripcion);
            MY._spParametros("@_extencion", MySqlDbType.VarChar, r.extencion);
            MY._spParametros("@_nivel", MySqlDbType.Int32, r.nivel);

            if (MY.estado_cn == true)
            {
                DT = MY._spTable();

                if (MY.error != "")
                {
                    empaqueta_error_consulta(MY.error, this.MY.NumError);
                }
            }
            else
            {
                DB = "MY";
                empaqueta_error_consulta();
            }

            return DT;
        }

        #endregion
    }
}