using FastLearning.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FastLearning.Controllers
{
    public class SolicitudController : Controller
    {
        FastLearningDBEntities db = new FastLearningDBEntities();
        // GET: Solicitud
        public ActionResult Index()
        {
            return View(db.Solicitud.ToList());
        }

        public ActionResult ListarSolicitudParaEvaluar()
        {
            return View();
        }

        public ActionResult ListarSolicitudPorUsuario(int id=2)
        {
            Usuario usuario = db.Usuario.Find(id);
            return View(db.Solicitud.Where(s=>s.id_usuario==id).ToList());
        }

        public ActionResult ListarVotantesPorSolicitud(int id)
        {
            return View(db.SolicitudUsuario.Where(su=>su.id_solicitud==id).ToList());
        }
        // GET: Solicitud/Create
        public ActionResult Crear()
        {
            return View();
        }

        // POST: Solicitud/Create
        [HttpPost]
        public ActionResult Crear(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("ListarSolicitudPorUsuario");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Editar(int id)
        {
            Solicitud solicitud = db.Solicitud.Find(id);
            return View(solicitud);
        }

        [HttpPost]
        public ActionResult Editar(Solicitud solicitud)
        {
            if (!ModelState.IsValid)
                return View();
            try
            {
                Solicitud s = db.Solicitud.Find(solicitud.id);
                s.nombre = solicitud.nombre;
                s.descripcion = solicitud.descripcion;
                db.SaveChanges();
                return RedirectToAction("ListarSolicitudPorUsuario");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Eliminar(int id)
        {
            Solicitud solicitud = db.Solicitud.Find(id);
            return View(solicitud);
        }
        // POST: Solicitud/Delete/5
        [HttpPost]
        public ActionResult Eliminar(Solicitud solicitud)
        {
            try
            {
                Solicitud s = db.Solicitud.Find(solicitud.id);
                db.Solicitud.Remove(s);
                db.SaveChanges();
                return RedirectToAction("ListarSolicitudPorUsuario");
            }
            catch
            {
                return View();
            }
        }
    }
}
