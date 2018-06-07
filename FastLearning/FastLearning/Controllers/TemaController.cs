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
            if (Session["id"] != null)
            {
                int id_usuario = Convert.ToInt32(Session["id"]);
                Usuario usuario = db.Usuario.Find(id_usuario);
                if (usuario.rol == 2)
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
                else
                {
                    return RedirectToAction("IniciarSesion", "Home");
                }
            }
            else
            {
                return RedirectToAction("IniciarSesion", "Home");
            }
        }

        // GET: Curso/Create
        public ActionResult Crear()
        {          
            if (Session["id"] != null)
            {
                int id_usuario = Convert.ToInt32(Session["id"]);
                Usuario usuario = db.Usuario.Find(id_usuario);
                if (usuario.rol == 2)
                {
                    SelectList cursos = new SelectList(db.Curso.ToList(), "id", "nombre");
                    ViewBag.Cursos = cursos;
                    return View();
                }
                else
                {
                    return RedirectToAction("IniciarSesion", "Home");
                }
            }
            else
            {
                return RedirectToAction("IniciarSesion", "Home");
            }
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
                SelectList cursos = new SelectList(db.Curso.ToList(), "id", "nombre");
                ViewBag.Cursos = cursos;
                ModelState.AddModelError("Error al agregar tema", ex);
                return View();
            }
        }

        public ActionResult CrearPorSolicitud(int id_curso,string nombre)
        {
            if (Session["id"] != null)
            {
                int id_usuario = Convert.ToInt32(Session["id"]);
                Usuario usuario = db.Usuario.Find(id_usuario);
                if (usuario.rol == 2)
                {
                    SelectList cursos = new SelectList(db.Curso.ToList(), "id", "nombre");
                    ViewBag.Cursos = cursos;
                    Tema tema = db.Tema.Create();
                    tema.nombre = nombre;
                    tema.id_curso = id_curso;
                    return View(tema);
                }
                else
                {
                    return RedirectToAction("IniciarSesion", "Home");
                }
            }
            else
            {
                return RedirectToAction("IniciarSesion", "Home");
            }
        }

        [HttpPost]
        public ActionResult CrearPorSolicitud(Tema tema)
        {
            try
            {
                tema.fecha_registro = DateTime.Now;
                db.Tema.Add(tema);
                int id_solicitud = Convert.ToInt32(Session["solicitud"]);
                Solicitud solicitud = db.Solicitud.Find(id_solicitud);
                solicitud.estado = 2;
                db.SaveChanges();
                return RedirectToAction("ListarSolicitudPendientes","Solicitud");
            }
            catch (Exception ex)
            {
                SelectList cursos = new SelectList(db.Curso.ToList(), "id", "nombre");
                ViewBag.Cursos = cursos;
                ModelState.AddModelError("Error al agregar tema", ex);
                return View();
            }
        }

        // GET: Curso/Edit/5
        public ActionResult Editar(int id)
        {           
            if (Session["id"] != null)
            {
                int id_usuario = Convert.ToInt32(Session["id"]);
                Usuario usuario = db.Usuario.Find(id_usuario);
                if (usuario.rol == 2)
                {
                    Tema tema = db.Tema.Find(id);
                    SelectList cursos = new SelectList(db.Curso.ToList(), "id", "nombre");
                    ViewBag.Cursos = cursos;
                    return View(tema);
                }
                else
                {
                    return RedirectToAction("IniciarSesion", "Home");
                }
            }
            else
            {
                return RedirectToAction("IniciarSesion", "Home");
            }
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
