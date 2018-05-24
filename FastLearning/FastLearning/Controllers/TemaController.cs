using FastLearning.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace FastLearning.Controllers
{
    public class TemaController : Controller
    {
        FastLearningDBEntities db = new FastLearningDBEntities();
        // GET: Curso
        public ActionResult Index(string currentFilter, string buscar, int? page)
        {
            if (buscar != null)
            {
                page = 1;
            }
            else
            {
                buscar = currentFilter;
            }

            ViewBag.CurrentFilter = buscar;

            List<Tema> temas = db.Tema.ToList();
            if (!String.IsNullOrEmpty(buscar))
            {
                temas = temas.Where(c => c.nombre.Contains(buscar)).ToList();
            }

            int pageSize = 6;
            int pageNumber = (page ?? 1);

            return View(temas.ToPagedList(pageNumber, pageSize));
        }

        // GET: Curso/Create
        public ActionResult Crear()
        {
            SelectList cursos = new SelectList(db.Curso.ToList(), "id", "nombre");
            ViewBag.Cursos = cursos;
            return View();
        }

        // POST: Curso/Create
        [HttpPost]
        public ActionResult Crear(Tema tema)
        {
            try
            {
                tema.fecha_registro = DateTime.Now;
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
            SelectList cursos = new SelectList(db.Curso.ToList(), "id", "nombre");
            ViewBag.Cursos = cursos;
            return View(tema);
        }

        // POST: Curso/Edit/5
        [HttpPost]
        public ActionResult Editar(Tema tema)
        {
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
    }
}
