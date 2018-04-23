using FastLearning.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FastLearning.Controllers
{
    public class TemaController : Controller
    {
        FastLearningDBEntities db = new FastLearningDBEntities();
        // GET: Curso
        public ActionResult Index()
        {
            return View(db.Tema.ToList());
        }

        // GET: Curso/Details/5
        public ActionResult Detalles(int id)
        {
            Tema tema = db.Tema.Find(id);
            return View(tema);
        }

        // GET: Curso/Create
        public ActionResult Crear()
        {
            return View();
        }

        // POST: Curso/Create
        [HttpPost]
        public ActionResult Crear(Tema tema)
        {
            if (!ModelState.IsValid)
                return View();
            try
            {
                db.Tema.Add(tema);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error al agregar tema", ex);
                return View();
            }
        }

        // GET: Curso/Edit/5
        public ActionResult Editar(int id)
        {
            Tema tema = db.Tema.Find(id);
            return View(tema);
        }

        // POST: Curso/Edit/5
        [HttpPost]
        public ActionResult Editar(Tema tema)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                Tema t = db.Tema.Find(tema.id);
                t.nombre = tema.nombre;
                t.descripcion = tema.descripcion;
                t.id_curso = tema.id_curso;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult ListarCursos()
        {
            return PartialView(db.Curso.ToList());
        }
    }
}
