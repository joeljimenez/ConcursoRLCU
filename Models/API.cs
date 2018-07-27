using ConcursoRLCU.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConcursoRLCU.Models
{
    public class API
    {
        private VariablesGlobales global = new VariablesGlobales();
        private List<respuesta> retorno = new List<respuesta>();
        internal List<respuesta> consulta_sp_participantes(Parametros r)
        {
            if (r.metodo == 0)
            {
                r.passw = this.global.encrip.Encriptar(r.passw, global.clave);
            }
            else {
                r.passw = "";
            }
            this.global.cmd = new consultas("000");
            this.global.dt = this.global.cmd.consulta_sp_participantes(r);


            return this.respuesta();
        }

        internal List<respuesta> sp_parametros(Parametros r)
        {
            this.global.cmd = new consultas("000");
            this.global.dt = this.global.cmd.consulta_sp_parametros(r);


            return this.respuesta();
        }

        internal List<respuesta> sp_galeria(Parametros r)
        {
            this.global.cmd = new consultas("000");
            this.global.dt = this.global.cmd.consulta_sp_galeria(r);
            
            return this.respuesta();
        }
        internal bool sp_galeria_valida_cupo(Parametros r)
        {

            bool retorno = false;
            global.cmd = new consultas("000");
            global.dt = this.global.cmd.consulta_sp_galeria(r);

            if (global.dt.Rows.Count > 0)
            {
                int usados = Convert.ToInt32(global.dt.Rows[0]["usados"].ToString());
                int cupos = Convert.ToInt32(global.dt.Rows[0]["cupos"].ToString());
                if (usados < cupos)
                {
                    retorno = true;
                }
            }


            return retorno;
        }
        private List<respuesta> respuesta()
        {
            if (this.global.cmd.exito)
            {
                if (this.global.dt.Rows.Count > 0)
                {
                    this.retorno.Add(new respuesta
                    {
                        data = this.global.dt,
                        exito = true,
                        mensaje = "Exito Total"
                    });
                }
                else
                {
                    this.retorno.Add(new respuesta
                    {
                        exito = false,
                        mensaje = "No Existes Registros Para Mostar...",
                        numero = this.global.cmd.NumError
                    });
                }
            }
            else
            {
                this.retorno.Add(new respuesta
                {
                    exito = false,
                    mensaje = "Error al ejecutar consulta..." + this.global.cmd.mensaje,
                    numero = this.global.cmd.NumError
                });
               
            }
            return this.retorno;
        }

        
    }

    internal class DataRows
    {
    }
}