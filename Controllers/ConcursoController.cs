using System.Web.Mvc;

namespace ConcursoRLCU.Controllers
{
    public class ConcursoController : Controller
    {
        // GET: Concurso
        public ActionResult Index()
        {
            ViewBag.titulo = "PORTAFOLIO";
            ViewBag.portafolio = " w3-text-teal";
            ViewBag.info = " ";
            ViewBag.jurado = " ";
            if (Session["idparticipante"] != null) {

                ViewBag.nombre = Session["nombre"];

                if (Session["jurado"].ToString() == "1")
                {
                    ViewBag.super = " ";
                }
                else {
                    ViewBag.super = " w3-hide ";
                }

                if (Session["estado"].ToString() == "2" && Session["jurado"].ToString() == "0")
                {

                    return View();
                }
                else if (Session["estado"].ToString() == "2" && Session["jurado"].ToString() == "1")
                {
                    var estado = Session["estado"].ToString();

                    return View();
                }
                else if (Session["estado"].ToString() == "2")
                {
                    return RedirectToAction("../Concurso/Info");
                }
                else {
                    return RedirectToAction("../Login");
                }
            }
            else
            {

                return RedirectToAction("../Login");
            }

        }

        public ActionResult Info()
        {
            ViewBag.titulo = "PERFIL";
            ViewBag.portafolio = " ";
            ViewBag.info = " w3-text-teal";
            ViewBag.jurado = " ";
            if (Session["idparticipante"] != null)
            {
                ViewBag.nombre = Session["nombre"];
                if (Session["jurado"].ToString() == "1")
                {
                    ViewBag.super = " ";
                }
                else
                {
                    ViewBag.super = " w3-hide ";
                }
                return View();
            }
            else
            {

                return RedirectToAction("../Login");
            }
        }

        public ActionResult Jurado()
        {
            ViewBag.titulo = "JURADO";
            ViewBag.portafolio = " ";
            ViewBag.info = " ";
            ViewBag.jurado = " w3-text-teal";
            if (Session["idparticipante"] != null)
            {
                ViewBag.nombre = Session["nombre"];
                if (Session["estado"].ToString() == "2" && Session["jurado"].ToString() == "1")
                {
                    ViewBag.super = " ";
                    return View();
                }
                else
                {

                    return RedirectToAction("../Login");
                }
            }
            else
            {

                return RedirectToAction("../Login");
            }

        }

       
    }
}