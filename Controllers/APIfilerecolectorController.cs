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

namespace ConcursoRLCU.Controllers
{
    public class APIfilerecolectorController : ApiController
    {
        public List<respuesta> Post()
        {
            new VariablesGlobales();
            List<respuesta> list = new List<respuesta>();
            API cmd = new API();
            Parametros parametros = new Parametros();

            VariablesGlobales global = new VariablesGlobales();
            if (HttpContext.Current.Session["idparticipante"] != null)
            {
                HttpRequest request = HttpContext.Current.Request;
                string text = "";
                try
                {

                    #region subida de archivos
                    //crean la session
                    parametros.idparticipante = Convert.ToInt32(HttpContext.Current.Session["idparticipante"].ToString());
                    parametros.idconcurso = Convert.ToInt32(HttpContext.Current.Session["idconsurso"].ToString());

                    #region crea directorio raiz
                    text = ConfigurationManager.ConnectionStrings["Ruta"].ConnectionString + "/" + parametros.idconcurso + "/" + parametros.idparticipante;

                    if (!Directory.Exists(text))
                    {
                        Directory.CreateDirectory(text);
                    }
                    if (!Directory.Exists(text))
                    {
                        list.Add(new respuesta
                        {
                            numero = "0",
                            mensaje = "No se pudo crear la ruta para la imagen... Comuníquelo al correo que se encuentra en su perfil.",
                            exito = false
                        });

                        return list;
                    }
                    #endregion

                    if (request.Files.Count > 0)
                    {


                        //string name = (string)enumerator.Current;
                        HttpPostedFile httpPostedFile = request.Files[0];
                        string[] array = httpPostedFile.FileName.Split('°');

                        parametros.consulta = 3;// valida cupos
                        parametros.idcategoria = Convert.ToInt32(array[3]);

                        if (cmd.sp_galeria_valida_cupo(parametros))
                        {
                            if (!Directory.Exists(text))
                            {
                                Directory.CreateDirectory(text);
                            }
                            if (Directory.Exists(text))
                            {
                                #region valida existen de archivo
                                if (!File.Exists(text + "/" + array[0] + array[1]))
                                {
                                    #region inserta archivo nuevo
                                    httpPostedFile.SaveAs(text + "/" + array[0] + array[1]);

                                    text = text + "/" + array[1] + array[2];
                                    //conulta 2 inserta en base de datos
                                    parametros.consulta = 2;
                                    parametros.nombre = array[0];
                                    parametros.extencion = array[1];
                                    parametros.descripcion = array[2];

                                    list = cmd.sp_galeria(parametros);

                                    // si insert n base de datos envia error elimina el archivo insertado en la carpeta
                                    if (!list[0].exito)
                                    {
                                        File.Delete(text);
                                    }
                                    #endregion
                                }
                                else
                                {
                                    #region envia mensaje de archivo existente
                                    parametros.consulta = 15;
                                    parametros.nombre = array[1];
                                    parametros.extencion = array[2];
                                    //list = aPILocalModel.sp_evidencias(parametros);
                                    list[0].numero = "10";
                                    list[0].mensaje = "Archivo ya existe. si desea eliminelo y vuelva a subirlo";
                                    list[0].exito = false;
                                    #endregion
                                }
                                #endregion

                            }
                        }
                        else
                        {

                            list.Add(new respuesta
                            {
                                numero = "0",
                                mensaje = "Usted no posee más cupos para esta categoria...",
                                exito = false
                            });
                        }

                        return list;
                    }
                    #endregion
                }
                catch (Exception a)
                {
                    File.Delete(text);
                    list.Add(new respuesta
                    {
                        numero = "0",
                        mensaje = "Error al cargar archivo" + a.Message,
                        exito = false
                    });
                }
                return list;
            }
            else {
                list.Add(new respuesta
                {
                    numero = "-1",
                    mensaje = "Session Expirada",
                    exito = false
                });
                return list;
            }
        }
    }
}
