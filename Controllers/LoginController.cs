using ConcursoRLCU.Data;
using ConcursoRLCU.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ConcursoRLCU.Controllers
{
    public class LoginController : Controller
    {

        API cmd = new API();
        private List<respuesta> retorno = new List<respuesta>();
        private Parametros r = new  Parametros();
        VariablesGlobales global = new VariablesGlobales();

        // GET: Login
        public ActionResult Index()
        {
            Session.RemoveAll();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Login u)
        {
          
            Session.RemoveAll(); // remueve todas la sessiones creadas 

            if (ModelState.IsValid) // valida si la informacion esta correcta los campos completos 
            {
                ViewBag.error = "";
                r.consulta = 1;
                r.email = u.correo;
                r.passw = u.Password;
                
                retorno = cmd.consulta_sp_participantes(r);

                if (retorno[0].exito)
                {
                    if (retorno[0].data.Rows.Count == 1)
                    {
                        foreach (DataRow item in retorno[0].data.Rows)
                        {

                            crear_session(
                                item["idparticipante"].ToString(),
                                item["nombre"].ToString() + " " + item["apellido"].ToString(),
                                item["estado"].ToString(),
                                item["es_jurado"].ToString(),
                                item["idconcurso"].ToString()
                                );
                        }
                        return RedirectToAction("../Concurso/Index");
                    }
                    else {
                        ViewBag.error = "hide";
                        ViewBag.mensaje = "Este usuario no existe...";
                        return View(u);
                    }
                }
                else
                {
                    ViewBag.error = "hide";
                    ViewBag.mensaje = "Este usuario no existe...";
                    return View(u);

                }
            }
            else
            {
                ViewBag.error = "hide";


            }
            return View(u);
        }

        public ActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registro(Registro u)
        {

            Session.RemoveAll(); // remueve todas la sessiones creadas 

            if (ModelState.IsValid) // valida si la informacion esta correcta los campos completos 
            {

                if (u.Password == u.Password2)
                {
                    ViewBag.error = "";
                    r.consulta = 2;
                    r.nombre = u.nombre;
                    r.apellido = u.apellido;
                    r.email = u.correo;
                    r.passw = u.Password;
                    

                    retorno = cmd.consulta_sp_participantes(r);

                    if (retorno[0].exito)
                    {

                        //foreach (DataRow item in retorno[0].data.Rows)
                        //{

                        //    crear_session(
                        //        item["idparticipante"].ToString(),
                        //        item["nombre"].ToString() + " " + item["apellido"].ToString(),
                        //        item["estado"].ToString(),
                        //        item["es_jurado"].ToString()
                        //        );
                        //}
                        string link = ConfigurationManager.ConnectionStrings["link"].ConnectionString + "?val=" + global.encrip.Encriptar(u.correo + "°" + u.Password, "Registro");
                        string html = "<html xmlns='http://www.w3.org/1999/xhtml'><head><meta http-equiv='Content-Type' content='text/html; charset=utf-8'/><title>Untitled Document</title></head><body><div style='width:100%;' align='center'><table width='100%' border='0' cellspacing='0' cellpadding='0'> <tr> <td align='center' valign='top' style='background-color:#53636e;' bgcolor='#53636e;'> <br><br><table width='583' border='0' cellspacing='0' cellpadding='0'> <tr> <td align='left' valign='top' bgcolor='#FFFFFF' style='background-color:#FFFFFF;'><img src='http://concursodefotografia.ulatina.edu.pa/Content/email/header.jpg' width='583' height='118'></td></tr><tr> <td align='left' valign='top' bgcolor='#FFFFFF' style='background-color:#FFFFFF;'><table width='100%' border='0' cellspacing='0' cellpadding='0'> <tr> <td width='35' align='left' valign='top'>&nbsp;</td><td align='left' valign='top'><table width='100%' border='0' cellspacing='0' cellpadding='0'> <tr> <td align='center' valign='top'> <div style='color:#245da5; font-family:Times New Roman, Times, serif; font-size:36px;'>“Naturalmente Latina”</div><div style='font-family: Verdana, Geneva, sans-serif; color:#898989; font-size:12px;'>Confirmación de correo</div></td></tr><tr> <td align='left' valign='top'><img src='http://concursodefotografia.ulatina.edu.pa/Content/email/pic1.jpg' width='512' height='296' vspace='10'></td></tr><tr> <td align='left' valign='top' style='font-family:Arial, Helvetica, sans-serif; font-size:12px; color:#525252;'> <div style='color:#3482ad; font-size:19px; text-align:center'>Gracias por registrarte en el concurso por favor accede al a este <a href='" + link + "' target='_blank'>link</a> para terminar el proceso y puedas completar tu datos y subir tus fotografías para el concurso.</td></tr><tr> <td align='left' valign='top' style='font-family:Arial, Helvetica, sans-serif; font-size:12px; color:#525252;'>&nbsp;</td></tr></table></td><td width='35' align='left' valign='top'>&nbsp;</td></tr></table></td></tr><tr> <td align='left' valign='top' bgcolor='#3d90bd' style='background-color:#3d90bd;'><table width='100%' border='0' cellspacing='0' cellpadding='0'> <tr> <td width='35'>&nbsp;</td><td height='50' valign='middle' style='color:#FFFFFF; font-size:11px; font-family:Arial, Helvetica, sans-serif;'><b>Company Address:</b><br>RLCU & Universidad Latina de Panamá </td><td width='35'>&nbsp;</td></tr></table></td></tr></table> <br><br></td></tr></table></div></body></html>";
                        //global.correo.carga_coneccion("0");
                        global.correo.EnviarA(u.correo);
                        global.correo.asunto_contenido(true,html, "Confirmación de cuenta Concurso de fotografía ");
                        global.correo.enviar_correo();                                                


                        return RedirectToAction("../Login/Mensaje_registro");
                    }
                    else {
                        if (retorno[0].numero == "1062")
                        {
                            ViewBag.error = "hide";
                            ViewBag.mensaje = "Este correo ya existe, Si desea autenticarse retorne al Login.";
                            return View(u);
                           
                        }
                        else
                        {
                            ViewBag.error = "hide";
                            ViewBag.mensaje = retorno[0].mensaje;
                            return View(u);
                        }
                       
                    }

                    
                }
                else {
                    ViewBag.error = "hide";
                    ViewBag.mensaje = "Las contraseñas no coinciden por favor vuelva a intentarlo.";
                    return View(u);
                }

            }
            else
            {
                ViewBag.error = "hide";

                return View(u);
            }
           
        }

        public ActionResult Recuperar_password()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Recuperar_password(password u)
        {

            Session.RemoveAll(); // remueve todas la sessiones creadas 

            if (ModelState.IsValid) // valida si la informacion esta correcta los campos completos 
            {
                ViewBag.error = "";
                r.metodo = -1;
                r.consulta = 6;
                r.email = u.correo;



                retorno = cmd.consulta_sp_participantes(r);

                if (retorno[0].exito)
                {
                    if (retorno[0].data.Rows.Count > 0)
                    {
                        try
                        {
                            string password = global.encrip.Desencriptar(retorno[0].data.Rows[0][0].ToString(),global.clave);
                            string html = "<html xmlns='http://www.w3.org/1999/xhtml'><head><meta http-equiv='Content-Type' content='text/html; charset=utf-8'/><title>Untitled Document</title></head><body><div style='width:100%;' align='center'><table width='100%' border='0' cellspacing='0' cellpadding='0'> <tr> <td align='center' valign='top' style='background-color:#53636e;' bgcolor='#53636e;'> <br><br><table width='583' border='0' cellspacing='0' cellpadding='0'> <tr> <td align='left' valign='top' bgcolor='#FFFFFF' style='background-color:#FFFFFF;'><img src='http://concursodefotografia.ulatina.edu.pa/Content/email/header.jpg' width='583' height='118'></td></tr><tr> <td align='left' valign='top' bgcolor='#FFFFFF' style='background-color:#FFFFFF;'><table width='100%' border='0' cellspacing='0' cellpadding='0'> <tr> <td width='35' align='left' valign='top'>&nbsp;</td><td align='left' valign='top'><table width='100%' border='0' cellspacing='0' cellpadding='0'> <tr> <td align='center' valign='top'> <div style='color:#245da5; font-family:Times New Roman, Times, serif; font-size:36px;'>" + password + "</div></tr><tr> <td align='left' valign='top' style='font-family:Arial, Helvetica, sans-serif; font-size:12px; color:#525252;'>&nbsp;</td></tr></table></td><td width='35' align='left' valign='top'>&nbsp;</td></tr></table></td></tr><tr> <td align='left' valign='top' bgcolor='#3d90bd' style='background-color:#3d90bd;'><table width='100%' border='0' cellspacing='0' cellpadding='0'> <tr> <td width='35'>&nbsp;</td><td height='50' valign='middle' style='color:#FFFFFF; font-size:11px; font-family:Arial, Helvetica, sans-serif;'><b>Company Address:</b><br>RLCU & Universidad Latina de Panamá </td><td width='35'>&nbsp;</td></tr></table></td></tr></table> <br><br></td></tr></table></div></body></html>";
                            //global.correo.carga_coneccion("0");
                            global.correo.EnviarA(u.correo);
                            global.correo.asunto_contenido(true, html, "Recuperacion de cuenta Concurso de fotografía RLCU");
                            global.correo.enviar_correo();
                            return RedirectToAction("../Login/");
                        }
                        catch (Exception a) {
                            ViewBag.error = "hide";
                            ViewBag.mensaje = "Error no controlado...";
                            return View(u);
                        }
                    }
                    else {

                        ViewBag.error = "hide";
                        ViewBag.mensaje = "Error correo no existe...";
                        return View(u);
                    }

                    
                }
                else
                {
                    ViewBag.error = "hide";
                    ViewBag.mensaje = retorno[0].mensaje;
                    return View(u);
                }
            }
            else
            {
                ViewBag.error = "hide";

                return View(u);
            }

        }


        public ActionResult Mensaje_registro()
        {
            return View();
        }
        // GET: Validacion_registro
        public ActionResult Validacion_registro(string val)
        {
            ViewBag.mensaje = "";
            if (val == null)
            {
                return RedirectToAction("../Login/Error", new { val = "Error" });
            }
            else {
                try {

                    string[] data =  global.encrip.Desencriptar(val, "Registro").Split('°');

                    r.consulta = 5;
                    r.email = data[0].ToString();
                    r.passw = data[1].ToString();

                    retorno = cmd.consulta_sp_participantes(r);

                    if (retorno[0].exito)
                    {
                        if (retorno[0].data.Rows.Count == 1)
                        {
                            foreach (DataRow item in retorno[0].data.Rows)
                            {

                                crear_session(
                                    item["idparticipante"].ToString(),
                                    item["nombre"].ToString() + " " + item["apellido"].ToString(),
                                    item["estado"].ToString(),
                                    item["es_jurado"].ToString(),
                                    item["idconcurso"].ToString()
                                    );
                            }
                            return RedirectToAction("../Concurso/Index");
                        }
                        else
                        {
                            ViewBag.error = "hide";
                            ViewBag.mensaje = "Este usuario no existe...";
                            return RedirectToAction("../Login/Error", new { val = "Error vuelva a intentarlo. Por Favor" });
                        }
                    }
                    else
                    {
                        ViewBag.error = "hide";
                        ViewBag.mensaje = "Este usuario no existe...";
                        return RedirectToAction("../Login/Error", new { val = "Error vuelva a intentarlo. Por Favor" });
                     

                    }

                }
                catch (Exception a ) {
                    return RedirectToAction("../Login/Error", new { val = "Error" } );
                }
            }
           
        }

        public ActionResult Error(string val) {

            ViewBag.mensaje = val;
            return View();
        }

        private void crear_session(string idUser, string nombre, string estado, string jurado, string idconcurso)
        {
            Session.Add("idconsurso", idconcurso);
            Session.Add("idparticipante", idUser);
            Session.Add("nombre", nombre);
            Session.Add("estado", estado);
            Session.Add("jurado", jurado);

        }
    }

    
}