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

        public ActionResult Index(string buscar)
        {
            Session.Contents.Remove("error");
            int id_usuario = Convert.ToInt32(Session["id"]);
            int id_estado = 1;
            List<Clase> clases = db
                .Clase
                .Where(c=>c.id_estado==id_estado && c.Habilidad.Usuario.id!=id_usuario && c.vacantes>c.ClaseUsuario.Count).ToList();
            if (!String.IsNullOrEmpty(buscar))
            {
                clases = clases.Where(c => c.Habilidad.Tema.nombre.Contains(buscar)).ToList();
            }
            return View(clases);
        }

        public ActionResult ParcialListarHabilidadPorUsuario()
        {
            int id = Convert.ToInt32(Session["id"]);
            List<Habilidad> habilidades = db.Habilidad.Where(h => h.id_usuario == id).ToList();          
            return PartialView(habilidades);
        }

        public ActionResult ListarClasePorInscripcion()
        {
            int id = Convert.ToInt32(Session["id"]);
            Usuario usuario = db.Usuario.Find(id);
            return View(db.ClaseUsuario.Where(s => s.id_usuario == id).ToList());
        }
        public ActionResult ListarClasePorUsuario()
        {
            int id = Convert.ToInt32(Session["id"]);
            Usuario usuario = db.Usuario.Find(id);
            return View(db.Clase.Where(s => s.Habilidad.id_usuario == id).ToList());
        }

        public ActionResult ListarAlumnosPorClase(int id)
        {
            Clase clase = db.Clase.Find(id);
            return View(db.ClaseUsuario.Where(s => s.Clase.id == id).ToList());
        }

        public ActionResult Detalles(int id)
        {
            Clase clase = db.Clase.Find(id);
            ViewBag.clase = clase.Habilidad.Tema.nombre;
            ViewBag.Error = Session["error"];
            return View(clase);
        }

         

        public ActionResult InscribirseEnClase(int id)
        {
            Session["error"] = "";
            int id_usuario = Convert.ToInt32(Session["id"]);
            Clase clase = db.Clase.Find(id);
            Usuario usuario = db.Usuario.Find(id_usuario);
            if (db.ClaseUsuario.Find(clase.id, usuario.id) == null)
            {
                Session["error"] = "";
                ClaseUsuario claseusuario = db.ClaseUsuario.Create();
                claseusuario.id_clase = clase.id;
                claseusuario.id_usuario = usuario.id;
                db.ClaseUsuario.Add(claseusuario);
                db.SaveChanges();
            }
            else
            {
                Session["error"] = "Usted ya esta inscrito a esta clase";
                return RedirectToAction("Detalles","Clase", new { id=clase.id});
            }

            return RedirectToAction("Index");
        }

        public ActionResult Crear()
        {          
            return View();
        }

        
        [HttpPost]
        public ActionResult Crear(Clase clase)
        {
            if (!ModelState.IsValid)
                return View();
            try
            {
                clase.fecha_registro = DateTime.Now;
                clase.id_estado = 1;
                db.Clase.Add(clase);
                db.SaveChanges();
                return RedirectToAction("ListarClasePorUsuario");
            }
            catch
            {
                return View();
            }
        }

    }
}
