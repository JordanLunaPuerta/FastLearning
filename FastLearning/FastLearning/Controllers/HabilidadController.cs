using FastLearning.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FastLearning.Controllers
{
    public class HabilidadController : Controller
    {
        FastLearningDBEntities db = new FastLearningDBEntities();

        // GET: Habilidad
        public ActionResult Index()
        {
            return View(db.Habilidad.ToList());
        }

        public ActionResult ListarHabilidadPorUsuario(int id = 2)
        {
            Usuario usuario = db.Usuario.Find(id);
            return View(db.Habilidad.Where(s => s.id_usuario == id).ToList());
        }

        // GET: Habilidad/Create
        public ActionResult Crear()
        {
            return View();
        }

        // POST: Habilidad/Create
        [HttpPost]
        public ActionResult Crear(Habilidad habilidad)
        {
            if (!ModelState.IsValid)
                return View();
            try
            {
                habilidad.estado = 1;
                db.Habilidad.Add(habilidad);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error al agregar habilidad", ex);
                return View();
            }
        }

        public ActionResult ListarTemas()
        {
            return PartialView(db.Tema.ToList());
        }

        public ActionResult ListarNiveles()
        {
            return PartialView(db.Nivel.ToList());
        }
    }
}
