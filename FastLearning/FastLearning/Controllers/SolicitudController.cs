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

        public ActionResult Index(string buscar)
        {
            int id_usuario = Convert.ToInt32(Session["id"]);
            Session.Contents.Remove("solicitud");
            Session.Contents.Remove("errorsolicitud");
            List<Solicitud> solicitudes = db.Solicitud.Where(s=>s.estado==1 && s.id_usuario!=id_usuario).ToList();
            if (!String.IsNullOrEmpty(buscar))
            {
                solicitudes = solicitudes.Where(c => c.nombre.Contains(buscar)).ToList();
            }
            return View(solicitudes);
        }

        public ActionResult ListarSolicitudPendientes(string buscar)
        {
            List<Solicitud> solicitudes = db.Solicitud.Where(s => s.estado == 1).ToList();
            if (!String.IsNullOrEmpty(buscar))
            {
                solicitudes = solicitudes.Where(c => c.nombre.Contains(buscar)).ToList();
            }
            return View(solicitudes);
        }

        public ActionResult ListarSolicitudAprobadas(string buscar)
        {
            List<Solicitud> solicitudes = db.Solicitud.Where(s => s.estado == 2).ToList();
            if (!String.IsNullOrEmpty(buscar))
            {
                solicitudes = solicitudes.Where(c => c.nombre.Contains(buscar)).ToList();
            }
            return View(solicitudes);
        }

        public ActionResult AprobarSolicitud(int id)
        {
            Solicitud solicitud = db.Solicitud.Find(id);
            solicitud.estado = 2;
            db.SaveChanges();
            return RedirectToAction("ListarSolicitudPendientes","Solicitud");
        }

        //public ActionResult CancelarAprobacion(int id)
        //{
        //    Solicitud solicitud = db.Solicitud.Find(id);
        //    solicitud.estado = 1;
        //    db.SaveChanges();
        //    return RedirectToAction("ListarSolicitudAprobadas", "Solicitud");
        //}
      
        public ActionResult ListarSolicitudPorUsuario()
        {
            int id_usuario = Convert.ToInt32(Session["id"]);
            Usuario usuario = db.Usuario.Find(id_usuario);
            return View(db.Solicitud.Where(s=>s.id_usuario==id_usuario).ToList());
        }

        public ActionResult ListarSolicitudPorVotacion()
        {
            int id_usuario = Convert.ToInt32(Session["id"]);
            Usuario usuario = db.Usuario.Find(id_usuario);
            return View(db.SolicitudUsuario.Where(su=>su.id_usuario==id_usuario).ToList());
        }

        public ActionResult EliminarVoto(int id)
        {
            int id_usuario = Convert.ToInt32(Session["id"]);
            Solicitud solicitud = db.Solicitud.Find(id);
            SolicitudUsuario su = db.SolicitudUsuario.Find(solicitud.id,id_usuario);
            db.SolicitudUsuario.Remove(su);
            db.SaveChanges();
            return RedirectToAction("ListarSolicitudPorVotacion","Solicitud");
        }

        public ActionResult ListarVotantesPorSolicitud(int id)
        {          
            return View(db.SolicitudUsuario.Where(su=>su.id_solicitud==id).ToList());
        }

        public ActionResult ListarVotantesPorSolicitudVotada(int id)
        {
            return View(db.SolicitudUsuario.Where(su => su.id_solicitud == id).ToList());
        }

        public ActionResult ListarVotantesPorSolicitudAdmin(int id)
        {
            return View(db.SolicitudUsuario.Where(su => su.id_solicitud == id).ToList());
        }

        public ActionResult PartialListarVotantesPorSolicitud()
        {
            int id = Convert.ToInt32(Session["solicitud"]);
            return PartialView(db.SolicitudUsuario.Where(su => su.id_solicitud == id).ToList());
        }

        public ActionResult VotarPorSolicitud(int id)
        {
            Solicitud solicitud= db.Solicitud.Find(id);
            Session["solicitud"] = solicitud.id;
            ViewBag.ErrorSolicitud = Session["errorsolicitud"];
            return View();
        }

        [HttpPost]
        public ActionResult VotarPorSolicitud(SolicitudUsuario su)
        {
            Session["errorsolicitud"] = "";
            int id_usuario = Convert.ToInt32(Session["id"]);
            int id_solicitud = Convert.ToInt32(Session["solicitud"]);
            try
            {
                if (db.SolicitudUsuario.Find(id_solicitud,id_usuario)== null)
                {
                    Session["errorsolicitud"] = "";
                    su.id_solicitud = id_solicitud;
                    su.id_usuario = id_usuario;
                    su.fecha_registro = DateTime.Now;
                    db.SolicitudUsuario.Add(su);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    Session["errorsolicitud"] = "Usted ya votó por esta solicitud";
                    return RedirectToAction("VotarPorSolicitud","Solicitud", new { id=id_solicitud});
                }               
            }
            catch (Exception)
            {

                return View();
            }
        }

        public ActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Crear(Solicitud solicitud)
        {
            int id = Convert.ToInt32(Session["id"]);
            if (!ModelState.IsValid)
                return View();
            try
            {
                solicitud.id_usuario = id;
                solicitud.fecha_registro = DateTime.Now;
                solicitud.estado = 1;
                db.Solicitud.Add(solicitud);
                db.SaveChanges();

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
            db.Solicitud.Remove(solicitud);
            db.SaveChanges();
            return RedirectToAction("ListarSolicitudPorUsuario");
        }

        public ActionResult EliminarPendiente(int id)
        {
            Solicitud solicitud = db.Solicitud.Find(id);
            db.Solicitud.Remove(solicitud);
            db.SaveChanges();
            return RedirectToAction("ListarSolicitudPendientes");
        }

    }
}
