using FastLearning.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FastLearning.Controllers
{
    public class HomeController : Controller
    {
        FastLearningDBEntities db = new FastLearningDBEntities();

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult RegistrarUsuario()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegistrarUsuario(Usuario usuario)
        {
            if (!ModelState.IsValid)
                return View();
            if (db.Usuario.Any(u=>u.email ==usuario.email))
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
                ViewBag.Registrado = "Usuario registrado correctamente";
                return View();
            }
            catch (Exception)
            {
                return View();
            }
        }

        public ActionResult IniciarSesion()
        {
            return View();
        }
    }
}