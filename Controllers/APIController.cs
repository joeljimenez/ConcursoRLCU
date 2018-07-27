using ConcursoRLCU.Data;
using ConcursoRLCU.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.SessionState;

namespace ConcursoRLCU.Controllers
{
    public class APIController : ApiController
    {
        private VariablesGlobales global = new VariablesGlobales();
        private List<respuesta> retorno = new List<respuesta>();
        private API cmd = new API();

        HttpSessionState session = HttpContext.Current.Session;

        public List<respuesta> Post(Parametros r)
        {

            if (session["idparticipante"] != null)
            {
                r.idparticipante = Convert.ToInt32(session["idparticipante"].ToString());
                r.idconcurso = Convert.ToInt32(session["idconsurso"].ToString());
                #region consulta Perfil del participate 1
                if (r.metodo == 1)
                {
                    this.retorno = this.cmd.consulta_sp_participantes(r);
                    if (retorno[0].exito && r.consulta == 4 ) {
                        session["estado"] = "2";
                    }
                }
                #endregion

                #region consulta parametros 2
                else if (r.metodo == 2)
                {
                   retorno = cmd.sp_parametros(r);
                }
                #endregion

                #region consulta galeria 3
                else if (r.metodo == 3)
                {
                    retorno = cmd.sp_galeria(r);
                }
                #endregion
                #region trabajo con archivos galeria 
                else if (r.metodo == 4)
                {
                    #region eliminar archivo
                    if (r.consulta == 5)
                    {
                        try
                        {
                            string connectionString = ConfigurationManager.ConnectionStrings["Ruta"].ConnectionString + "/" + r.idconcurso + "/" + r.idparticipante;

                            File.Delete(string.Concat(new object[] { connectionString, "/", r.nombre, r.extencion }));

                            this.retorno = this.cmd.sp_galeria(r);
                        }
                        catch (Exception)
                        {
                            this.retorno.Add(new respuesta
                            {
                                numero = "-3",
                                mensaje = "",
                                exito = false
                            });
                        }
                    }
                    #endregion

                    #region reenombrar archivo
                    if (r.consulta == 4)
                    {

                        try
                        {
                            #region valida cambio de nombre
                            if (r.nombre != r.nombre2)
                            {
                                string connectionString = ConfigurationManager.ConnectionStrings["Ruta"].ConnectionString + "/" + r.idconcurso + "/" + r.idparticipante;
                                //nombre viejo
                                string ruta = connectionString + "/" + r.nombre + r.extencion;
                                //nuevo nombre
                                string ruta2 = connectionString + "/" + r.nombre2 + r.extencion;

                                if (File.Exists(ruta))
                                {
                                    if (!File.Exists(ruta2))
                                    {
                                        File.Move(ruta, ruta2);

                                        r.nombre = r.nombre2;
                                        this.retorno = this.cmd.sp_galeria(r);
                                    }
                                    else
                                    {
                                        this.retorno.Add(new respuesta
                                        {
                                            numero = "-6",
                                            mensaje = "este nombre ya existe",
                                            exito = false
                                        });
                                    }
                                }
                                else
                                {
                                    this.retorno.Add(new respuesta
                                    {
                                        numero = "-5",
                                        mensaje = "El archivo a renombrar no existe",
                                        exito = false
                                    });
                                }

                            }
                            #endregion
                            #region si no hay cambio en elnombre
                            else {
                                this.retorno = this.cmd.sp_galeria(r);
                            }
                            #endregion
                        }
                        catch (Exception)
                        {
                            this.retorno.Add(new respuesta
                            {
                                numero = "-4",
                                mensaje = "error al reenombrar archivo",
                                exito = false
                            });
                        }

                    }
                    #endregion
                   
                }
                #endregion



            }
            else
            {
                retorno.Add(new respuesta
                {
                    numero = "-1",
                    mensaje = "Session Expirada",
                    exito = false
                });

            }

            return this.retorno;
        }

    }
}
