using FastLearning.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FastLearning.Controllers
{
    public class UsuarioController : Controller
    {
        FastLearningDBEntities db = new FastLearningDBEntities();

        // GET: Usuario
        public ActionResult Index()
        {
            return View(db.Usuario.ToList());
        }

        // GET: Usuario/Details/5
        public ActionResult Detalles(int id)
        {
            Usuario usuario = db.Usuario.Find(id);
            return View(usuario);
        }

        public ActionResult Perfil()
        {
            int id = 2;
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

        /*public ActionResult ListarHabilidadUsuario(int id)
        {
            string sql = @"select t.nombre as NombreTema, n.descripcion as DescripcionNivel,h.estado as estado 
                            from Habilidad h
                            inner join Usuario u on h.id_usuario=u.id
                            inner join Tema t on h.id_tema=t.id
                            inner join Nivel n on h.id_nivel=n.id
                            where u.id=@id";

            Usuario usuario = db.Usuario.Find(id);
            ViewBag.nombres = usuario.nombres;
            ViewBag.apellidos = usuario.apellidos;
            return View(db.Database.SqlQuery<HabilidadMD>(sql,new SqlParameter("@id",id)).ToList());
        }*/

        public ActionResult ListarHabilidadPorUsuario(int id)
        {
            Usuario usuario = db.Usuario.Find(id);
            ViewBag.id_usuario = usuario.id;
            ViewBag.nombres = usuario.nombres;
            ViewBag.apellidos = usuario.apellidos;
            List<Habilidad> habilidades = db.Habilidad.Where(n => n.id_usuario == id).ToList();
            return View(habilidades);
        }

        public ActionResult ListarSolicitudPorUsuario(int id)
        {
            Usuario usuario = db.Usuario.Find(id);
            ViewBag.nombres = usuario.nombres;
            ViewBag.apellidos = usuario.apellidos;
            List<Solicitud> solicitudes = db.Solicitud.Where(s => s.id_usuario == id).ToList();
            return View(solicitudes);
        }

    }
}
