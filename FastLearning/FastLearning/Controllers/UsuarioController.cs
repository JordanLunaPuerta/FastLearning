using FastLearning.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace FastLearning.Controllers
{
    public class UsuarioController : Controller
    {
        FastLearningDBEntities db = new FastLearningDBEntities();

        // GET: Usuario
        public ActionResult Index(string currentFilter,string buscar, int? page)
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

                    List<Usuario> usuarios = db.Usuario.Where(u => u.rol == 1).ToList();
                    if (!String.IsNullOrEmpty(buscar))
                    {
                        usuarios = usuarios.Where(u => u.nombres.Contains(buscar) || u.apellidos.Contains(buscar)).ToList();
                    }

                    int pageSize = 6;
                    int pageNumber = (page ?? 1);

                    return View(usuarios.ToPagedList(pageNumber, pageSize));
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

        public ActionResult UsuarioReportes(string currentFilter, string buscar, int? page)
        {
            if(Session["id"] != null)
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

                    List<Usuario> usuarios = db.Usuario.Where(u => u.rol == 1).ToList();
                    if (!String.IsNullOrEmpty(buscar))
                    {

                        usuarios = usuarios
                            .Where(u => u.fecha_registro.Value.Month==Convert.ToDateTime(buscar).Month 
                            && u.fecha_registro.Value.Year== Convert.ToDateTime(buscar).Year)
                            .ToList();
                    }
                    int cantidad = usuarios.Count();
                    ViewBag.Cantidad = cantidad.ToString();
                    int pageSize = 6;
                    int pageNumber = (page ?? 1);

                    return View(usuarios.ToPagedList(pageNumber, pageSize));
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

        // GET: Usuario/Details/5
        public ActionResult Detalles(int id)
        {
            
            if (Session["id"] != null)
            {
                int id_usuario = Convert.ToInt32(Session["id"]);
                Usuario usuario = db.Usuario.Find(id_usuario);
                if (usuario.rol == 2)
                {
                    Usuario u = db.Usuario.Find(id);
                    if (u.foto == null)
                    {
                        ViewBag.Foto = "/Image/defecto.png";
                    }
                    else
                    {
                        ViewBag.Foto = u.foto;
                    }
                    return View(u);
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


        public ActionResult PerfilProfesor(int id)
        {
            if (Session["id"] != null)
            {
                int id_usuario = Convert.ToInt32(Session["id"]);
                Usuario usuario = db.Usuario.Find(id_usuario);
                if (usuario.rol == 1)
                {
                    Usuario u = db.Usuario.Find(id);
                    List<Clase> clases = db.Clase.Where(c => c.id_estado == 3 && c.Habilidad.Usuario.id == u.id).ToList();
                    ViewBag.Clases = clases.Count().ToString();
                    if (u.foto == null)
                    {
                        ViewBag.Foto = "/Image/defecto.png";
                    }
                    else
                    {
                        ViewBag.Foto = u.foto;
                    }
                    return PartialView(u);
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


        public ActionResult Perfil()
        {
            
            if (Session["id"] != null)
            {
                int id_usuario = Convert.ToInt32(Session["id"]);
                Usuario usuario = db.Usuario.Find(id_usuario);
                if (usuario.rol == 1)
                {
                    List<Clase>  clases = db.Clase.Where(c => c.id_estado==3 && c.Habilidad.Usuario.id==usuario.id).ToList();
                    ViewBag.Clases = clases.Count().ToString();
                    if (usuario.foto == null)
                    {
                        ViewBag.Foto = "/Image/defecto.png";
                    }
                    else
                    {
                        ViewBag.Foto = usuario.foto;
                    }
                    return View(usuario);
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

        public ActionResult EditarPerfil(int id)
        {
            
            if (Session["id"] != null)
            {
                int id_usuario = Convert.ToInt32(Session["id"]);
                Usuario usuario = db.Usuario.Find(id_usuario);
                if (usuario.rol == 1)
                {
                    Usuario u = db.Usuario.Find(id);
                    if (usuario.foto == null)
                    {
                        ViewBag.Foto = "/Image/defecto.png";
                    }
                    else
                    {
                        ViewBag.Foto = u.foto;
                    }
                    return View(u);
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

        // POST: Usuario/Edit/5
        [HttpPost]
        public ActionResult EditarPerfil(Usuario usuario)
        {

            if (ModelState.IsValid)
            {
                Usuario u = db.Usuario.Find(usuario.id);
                u.nombres = usuario.nombres;
                u.apellidos = usuario.apellidos;
                u.telefono = usuario.telefono;
                u.foto = usuario.foto;
                Session["foto"] = u.foto;
                u.email = usuario.email;
                u.contrasena = usuario.contrasena;
                u.confirmar_contrasena = usuario.confirmar_contrasena;
                db.SaveChanges();
                return RedirectToAction("Perfil");
            }
            else
            {
                if (usuario.foto == null)
                {
                    ViewBag.Foto = "/Image/defecto.png";
                }
                else
                {
                    ViewBag.Foto = usuario.foto;
                }
                return View(usuario);
            }
        }
       
        public ActionResult Eliminar(int id)
        {
            
            if (Session["id"] != null)
            {
                int id_usuario = Convert.ToInt32(Session["id"]);
                Usuario usuario = db.Usuario.Find(id_usuario);
                if (usuario.rol == 2)
                {
                    Usuario u = db.Usuario.Find(id);
                    db.Usuario.Remove(u);
                    db.SaveChanges();
                    return RedirectToAction("Index");
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
