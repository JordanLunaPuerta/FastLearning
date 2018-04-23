using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FastLearning.Models;

namespace FastLearning.Controllers
{
    public class CursoController : Controller
    {
        FastLearningDBEntities db = new FastLearningDBEntities();
        // GET: Curso
        public ActionResult Index()
        {
            return View(db.Curso.ToList());
        }

        // GET: Curso/Details/5
        public ActionResult Detalles(int id)
        {
            Curso curso = db.Curso.Find(id);
            return View(curso);
        }

        // GET: Curso/Create
        public ActionResult Crear()
        {
            return View();
        }

        // POST: Curso/Create
        [HttpPost]
        public ActionResult Crear(Curso curso)
        {
            if (!ModelState.IsValid)
                return View();
            try
            {
                db.Curso.Add(curso);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error al agregar curso", ex);
                return View();
            }
        }

        // GET: Curso/Edit/5
        public ActionResult Editar(int id)
        {
            Curso curso = db.Curso.Find(id);
            return View(curso);
        }

        // POST: Curso/Edit/5
        [HttpPost]
        public ActionResult Editar(Curso curso)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                Curso c = db.Curso.Find(curso.id);
                c.nombre = curso.nombre;
                c.descripcion = curso.descripcion;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
