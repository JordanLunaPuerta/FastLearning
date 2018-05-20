using FastLearning.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FastLearning.Controllers
{
    public class DescuentoUsuarioController : Controller
    {
        FastLearningDBEntities db = new FastLearningDBEntities();
        // GET: DescuentoUsuario
        public ActionResult Index()
        {
            int id = Convert.ToInt32(Session["id"]);
            Usuario u = db.Usuario.Find(id);
            ViewBag.Puntos = u.puntos;
            return View(db.DescuentoUsuario.Where(d=>d.id_usuario==id).ToList());
        }

        public ActionResult CanjearPuntos(int id=2)
        {
            Usuario u = db.Usuario.Find(id);
            ViewBag.Puntos = u.puntos;
            return View();
        }

        [HttpPost]
        public ActionResult CanjearPuntos()
        {
            try
            {
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}