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
            if (buscar !=null)
            {
                page = 1;
            }
            else
            {
                buscar = currentFilter;
            }

            ViewBag.CurrentFilter = buscar;

            List<Usuario> usuarios = db.Usuario.Where(u=>u.rol==1).ToList();
            if (!String.IsNullOrEmpty(buscar))
            {
                usuarios = usuarios.Where(u=>u.nombres.Contains(buscar) || u.apellidos.Contains(buscar)).ToList();
            }

            int pageSize = 6;
            int pageNumber = (page ?? 1);

            return View(usuarios.ToPagedList(pageNumber,pageSize));
        }

        // GET: Usuario/Details/5
        public ActionResult Detalles(int id)
        {
            Usuario usuario = db.Usuario.Find(id);
            return View(usuario);
        }

        public ActionResult Perfil()
        {
            int id = Convert.ToInt32(Session["id"]);
            Usuario usuario = db.Usuario.Find(id);
            return View(usuario);
        }

        public ActionResult EditarPerfil(int id)
        {
            Usuario usuario = db.Usuario.Find(id);
            return View(usuario);
        }

        // POST: Usuario/Edit/5
        [HttpPost]
        public ActionResult EditarPerfil(Usuario usuario)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                Usuario u = db.Usuario.Find(usuario.id);
                u.nombres = usuario.nombres;
                u.apellidos = usuario.apellidos;
                u.telefono = usuario.telefono;
                u.email = usuario.email;
                u.contrasena = usuario.contrasena;
                u.confirmar_contrasena = usuario.confirmar_contrasena;
                db.SaveChanges();
                return RedirectToAction("Perfil");
            }
            catch
            {
                return View();
            }
        }
        // GET: Usuario/Create
        public ActionResult Crear()
        {
            return View();
        }

        // POST: Usuario/Create
        [HttpPost]
        public ActionResult Crear(Usuario usuario)
        {
            if (!ModelState.IsValid)
                return View();
            if (db.Usuario.Any(u => u.email == usuario.email))
            {
                ViewBag.Duplicado = "Usuario ya existe";
                return View("RegistrarUsuario", usuario);
            }
            try
            {
                usuario.fecha_registro = DateTime.Now;
                usuario.calificacion_alumno = 5;
                usuario.calificacion_profesor = 5;
                usuario.rol = 1;
                usuario.puntos = 0;
                db.Usuario.Add(usuario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return View();
            }
        }

        // GET: Usuario/Edit/5
        public ActionResult Editar(int id)
        {
            Usuario usuario = db.Usuario.Find(id);
            return View(usuario);
        }

        // POST: Usuario/Edit/5
        [HttpPost]
        public ActionResult Editar(Usuario usuario)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                Usuario u = db.Usuario.Find(usuario.id);
                u.nombres = usuario.nombres;
                u.apellidos = usuario.apellidos;
                u.telefono = usuario.telefono;
                u.email = usuario.email;
                u.contrasena = usuario.contrasena;
                u.confirmar_contrasena = usuario.confirmar_contrasena;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Eliminar(int id)
        {
            Usuario usuario = db.Usuario.Find(id);
            db.Usuario.Remove(usuario);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
