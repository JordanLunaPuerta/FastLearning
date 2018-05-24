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

        public ActionResult ListarHabilidadPorUsuario()
        {
            int id = Convert.ToInt32(Session["id"]);
            Usuario usuario = db.Usuario.Find(id);
            return View(db.Habilidad.Where(s => s.id_usuario == id && s.estado==1).ToList());
        }

        // GET: Habilidad/Create
        public ActionResult Crear()
        {
            SelectList temas = new SelectList(db.Tema.ToList(), "id", "nombre");
            ViewBag.Temas = temas;
            SelectList niveles = new SelectList(db.Nivel.ToList(), "id", "descripcion");
            ViewBag.Niveles = niveles;
            return View();
        }

        // POST: Habilidad/Create
        [HttpPost]
        public ActionResult Crear(Habilidad habilidad)
        {
            int id = Convert.ToInt32(Session["id"]);
            try
            {
                habilidad.fecha_registro = DateTime.Now;
                habilidad.id_usuario = id;
                habilidad.estado = 1;
                db.Habilidad.Add(habilidad);
                db.SaveChanges();
                return RedirectToAction("ListarHabilidadPorUsuario");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error al agregar habilidad", ex);
                return View();
            }
        }

        public ActionResult Editar(int id)
        {
            Habilidad habilidad = db.Habilidad.Find(id);
            SelectList niveles = new SelectList(db.Nivel.ToList(), "id", "descripcion");
            ViewBag.Niveles = niveles;
            return View(habilidad);
        }

        [HttpPost]
        public ActionResult Editar(Habilidad habilidad)
        {
            try
            {
                Habilidad h = db.Habilidad.Find(habilidad.id);
                h.id_nivel = habilidad.id_nivel;
                db.SaveChanges();
                return RedirectToAction("ListarHabilidadPorUsuario");
            }
            catch (Exception)
            {

                return View();
            }
        }

        public ActionResult Eliminar(int id)
        {
            Habilidad habilidad = db.Habilidad.Find(id);
            habilidad.estado = 2;
            db.SaveChanges();
            return RedirectToAction("ListarHabilidadPorUsuario");
        }
    }
}
