using FastLearning.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FastLearning.Controllers
{
    public class ClaseController : Controller
    {
        FastLearningDBEntities db = new FastLearningDBEntities();
        // GET: Clase
        public ActionResult Index()
        {
            List<Clase> clases = db.Clase.ToList();
            return View(clases);
        }

        public ActionResult ListarClasePorInscripcion(int id=2)
        {
            Usuario usuario = db.Usuario.Find(id);
            return View(db.ClaseUsuario.Where(s => s.id_usuario == id).ToList());
        }
        public ActionResult ListarClasePorUsuario(int id = 2)
        {
            Usuario usuario = db.Usuario.Find(id);
            return View(db.Clase.Where(s => s.Habilidad.id_usuario == id).ToList());
        }

        public ActionResult ListarAlumnosPorClase(int id)
        {
            Clase clase = db.Clase.Find(id);
            return View(db.ClaseUsuario.Where(s => s.Clase.id == id).ToList());
        }

        // GET: Clase/Details/5
        public ActionResult Detalles(int id)
        {
            Clase clase = db.Clase.Find(id);
            ViewBag.clase = clase.Habilidad.Tema.nombre;
            return View(clase);
        }

        // GET: Clase/Create
        public ActionResult Crear()
        {
            return View();
        }

        // POST: Clase/Create
        [HttpPost]
        public ActionResult Crear(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("ListarClasePorUsuario");
            }
            catch
            {
                return View();
            }
        }

        // GET: Clase/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Clase/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Clase/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Clase/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
