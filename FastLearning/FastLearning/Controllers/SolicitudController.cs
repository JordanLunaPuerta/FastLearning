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
            if (Session["id"] != null)
            {
                int id_usuario = Convert.ToInt32(Session["id"]);
                Usuario usuario = db.Usuario.Find(id_usuario);
                if (usuario.rol == 1)
                {
                    Session.Contents.Remove("solicitud");
                    Session.Contents.Remove("errorsolicitud");
                    List<Solicitud> solicitudes = db.Solicitud.Where(s => s.estado == 1 && s.id_usuario != id_usuario).ToList();
                    if (!String.IsNullOrEmpty(buscar))
                    {
                        solicitudes = solicitudes.Where(c => c.nombre.Contains(buscar)).ToList();
                    }
                    return View(solicitudes.OrderByDescending(s => s.fecha_registro));
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

        public ActionResult ListarSolicitudPendientes(string buscar)
        {
            if (Session["id"] != null)
            {
                int id_usuario = Convert.ToInt32(Session["id"]);
                Usuario usuario = db.Usuario.Find(id_usuario);
                if (usuario.rol == 2)
                {
                    Session.Contents.Remove("solicitud");
                    List<Solicitud> solicitudes = db.Solicitud.Where(s => s.estado == 1).ToList();
                    if (!String.IsNullOrEmpty(buscar))
                    {
                        solicitudes = solicitudes.Where(c => c.nombre.Contains(buscar)).ToList();
                    }
                    return View(solicitudes.OrderByDescending(s => s.fecha_registro));
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

        public ActionResult ListarSolicitudAprobadas(string buscar)
        {
            
            if (Session["id"] != null)
            {
                int id_usuario = Convert.ToInt32(Session["id"]);
                Usuario usuario = db.Usuario.Find(id_usuario);
                if (usuario.rol == 2)
                {
                    List<Solicitud> solicitudes = db.Solicitud.Where(s => s.estado == 2).ToList();
                    if (!String.IsNullOrEmpty(buscar))
                    {
                        solicitudes = solicitudes.Where(c => c.nombre.Contains(buscar)).ToList();
                    }
                    return View(solicitudes.OrderByDescending(s => s.fecha_registro));
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

        public ActionResult AprobarSolicitud(int id)
        {
            if (Session["id"] != null)
            {
                int id_usuario = Convert.ToInt32(Session["id"]);
                Usuario usuario = db.Usuario.Find(id_usuario);
                if (usuario.rol == 2)
                {
                    Solicitud solicitud = db.Solicitud.Find(id);
                    Session["solicitud"] = solicitud.id;
                    return RedirectToAction("CrearPorSolicitud", "Tema", new { solicitud.id_curso,solicitud.nombre});
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
                    

        //public ActionResult CancelarAprobacion(int id)
        //{
        //    Solicitud solicitud = db.Solicitud.Find(id);
        //    solicitud.estado = 1;
        //    db.SaveChanges();
        //    return RedirectToAction("ListarSolicitudAprobadas", "Solicitud");
        //}
      
        public ActionResult ListarSolicitudPorUsuario()
        {
            
            if (Session["id"] != null)
            {
                int id_usuario = Convert.ToInt32(Session["id"]);
                Usuario usuario = db.Usuario.Find(id_usuario);
                if (usuario.rol == 1)
                {
                    return View(db.Solicitud.Where(s => s.id_usuario == id_usuario).ToList().OrderByDescending(s => s.fecha_registro));
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

        public ActionResult ListarSolicitudPorVotacion()
        {
            
            if (Session["id"] != null)
            {
                int id_usuario = Convert.ToInt32(Session["id"]);
                Usuario usuario = db.Usuario.Find(id_usuario);
                if (usuario.rol == 1)
                {
                    return View(db.SolicitudUsuario.Where(su => su.id_usuario == id_usuario).ToList().OrderByDescending(s => s.fecha_registro));
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

        public ActionResult EliminarVoto(int id)
        {            
            if (Session["id"] != null)
            {
                int id_usuario = Convert.ToInt32(Session["id"]);
                Usuario usuario = db.Usuario.Find(id_usuario);
                if (usuario.rol == 1)
                {
                    Solicitud solicitud = db.Solicitud.Find(id);
                    SolicitudUsuario su = db.SolicitudUsuario.Find(solicitud.id, id_usuario);
                    db.SolicitudUsuario.Remove(su);
                    db.SaveChanges();
                    return RedirectToAction("ListarSolicitudPorVotacion", "Solicitud");
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

        public ActionResult ListarVotantesPorSolicitud(int id)
        {                     
            if (Session["id"] != null)
            {
                int id_usuario = Convert.ToInt32(Session["id"]);
                Usuario usuario = db.Usuario.Find(id_usuario);
                if (usuario.rol == 1)
                {
                    return View(db.SolicitudUsuario.Where(su => su.id_solicitud == id).ToList());
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

        public ActionResult ListarVotantesPorSolicitudVotada(int id)
        {
            
            if (Session["id"] != null)
            {
                int id_usuario = Convert.ToInt32(Session["id"]);
                Usuario usuario = db.Usuario.Find(id_usuario);
                if (usuario.rol == 1)
                {
                    return View(db.SolicitudUsuario.Where(su => su.id_solicitud == id).ToList());
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

        public ActionResult ListarVotantesPorSolicitudAdmin(int id)
        {           
            if (Session["id"] != null)
            {
                int id_usuario = Convert.ToInt32(Session["id"]);
                Usuario usuario = db.Usuario.Find(id_usuario);
                if (usuario.rol == 2)
                {
                    return View(db.SolicitudUsuario.Where(su => su.id_solicitud == id).ToList());
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

        public ActionResult PartialListarVotantesPorSolicitud()
        {           
            if (Session["id"] != null)
            {
                int id_usuario = Convert.ToInt32(Session["id"]);
                Usuario usuario = db.Usuario.Find(id_usuario);
                if (usuario.rol == 1)
                {
                    int id = Convert.ToInt32(Session["solicitud"]);
                    return PartialView(db.SolicitudUsuario.Where(su => su.id_solicitud == id).ToList());
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

        public ActionResult VotarPorSolicitud(int id)
        {
            
            if (Session["id"] != null)
            {
                int id_usuario = Convert.ToInt32(Session["id"]);
                Usuario usuario = db.Usuario.Find(id_usuario);
                if (usuario.rol == 1)
                {
                    Solicitud solicitud = db.Solicitud.Find(id);
                    Session["solicitud"] = solicitud.id;
                    ViewBag.ErrorSolicitud = Session["errorsolicitud"];
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
            if (Session["id"] != null)
            {
                int id_usuario = Convert.ToInt32(Session["id"]);
                Usuario usuario = db.Usuario.Find(id_usuario);
                if (usuario.rol == 1)
                {
                    SelectList cursos = new SelectList(db.Curso.ToList().OrderBy(c=>c.nombre),"id","nombre");
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
                SelectList cursos = new SelectList(db.Curso.ToList().OrderBy(c => c.nombre), "id", "nombre");
                ViewBag.Cursos = cursos;
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
                    SelectList cursos = new SelectList(db.Curso.ToList().OrderBy(c => c.nombre), "id", "nombre");
                    ViewBag.Cursos = cursos;
                    Solicitud solicitud = db.Solicitud.Find(id);
                    return View(solicitud);
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
        public ActionResult Editar(Solicitud solicitud)
        {
            if (!ModelState.IsValid)
                return View();
            try
            {
                Solicitud s = db.Solicitud.Find(solicitud.id);
                s.nombre = solicitud.nombre;
                s.descripcion = solicitud.descripcion;
                s.id_curso = solicitud.id_curso;
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
            
            if (Session["id"] != null)
            {
                int id_usuario = Convert.ToInt32(Session["id"]);
                Usuario usuario = db.Usuario.Find(id_usuario);
                if (usuario.rol == 1)
                {
                    Solicitud solicitud = db.Solicitud.Find(id);
                    db.Solicitud.Remove(solicitud);
                    db.SaveChanges();
                    return RedirectToAction("ListarSolicitudPorUsuario");
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

        public ActionResult EliminarPendiente(int id)
        {
            
            if (Session["id"] != null)
            {
                int id_usuario = Convert.ToInt32(Session["id"]);
                Usuario usuario = db.Usuario.Find(id_usuario);
                if (usuario.rol == 2)
                {
                    Solicitud solicitud = db.Solicitud.Find(id);
                    db.Solicitud.Remove(solicitud);
                    db.SaveChanges();
                    return RedirectToAction("ListarSolicitudPendientes");
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
