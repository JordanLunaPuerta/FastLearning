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

        public ActionResult ListarHabilidadPorUsuario()
        {
            if (Session["id"] != null)
            {
                int id_usuario = Convert.ToInt32(Session["id"]);
                Usuario usuario = db.Usuario.Find(id_usuario);
                if (usuario.rol == 1)
                {
                    return View(db.Habilidad.Where(s => s.id_usuario == id_usuario && s.estado == 1).ToList().OrderByDescending(h => h.fecha_registro));
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

        public JsonResult ListarTemaPorCurso(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            return Json(db.Tema.Where(t => t.id_curso == id).ToList().OrderBy(t=>t.nombre),JsonRequestBehavior.AllowGet);
        }

        // GET: Habilidad/Create
        public ActionResult Crear()
        {
            if (Session["id"] != null)
            {
                int id_usuario = Convert.ToInt32(Session["id"]);
                Usuario usuario = db.Usuario.Find(id_usuario);
                if (usuario.rol == 1)
                {
                    ViewBag.Cursos = new SelectList(db.Curso.ToList().OrderBy(t => t.nombre), "id", "nombre");
                    ViewBag.Temas = new SelectList(db.Tema.ToList(), "id", "nombre");
                    ViewBag.Niveles = new SelectList(db.Nivel.ToList(), "id", "descripcion");
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
                ViewBag.Cursos = new SelectList(db.Curso.ToList(), "id", "nombre");
                ViewBag.Temas = new SelectList(db.Tema.ToList(), "id", "nombre");
                ViewBag.Niveles = new SelectList(db.Nivel.ToList(), "id", "descripcion");
                return View();
            }
        }

        public ActionResult Editar(int id)
        {          
            if (Session["id"] != null)
            {
                int id_usuario = Convert.ToInt32(Session["id"]);
                Usuario usuario = db.Usuario.Find(id_usuario);
                if (usuario.rol == 1)
                {
                    Habilidad habilidad = db.Habilidad.Find(id);
                    ViewBag.Niveles = new SelectList(db.Nivel.ToList(), "id", "descripcion");
                    return View(habilidad);
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
            if (Session["id"] != null)
            {
                int id_usuario = Convert.ToInt32(Session["id"]);
                Usuario usuario = db.Usuario.Find(id_usuario);
                if (usuario.rol == 1)
                {
                    Habilidad habilidad = db.Habilidad.Find(id);
                    if (habilidad.Clase.Count == 0)
                    {
                        db.Habilidad.Remove(habilidad);
                    }
                    else
                    {
                        habilidad.estado = 2;
                    }
                    db.SaveChanges();
                    return RedirectToAction("ListarHabilidadPorUsuario");
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
    }
}
