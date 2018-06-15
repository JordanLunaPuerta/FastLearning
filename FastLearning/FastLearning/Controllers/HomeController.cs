using FastLearning.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Net.Mail;

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
                return RedirectToAction("IniciarSesion");
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
   
        [HttpPost]
        public ActionResult IniciarSesion(Usuario usuario)
        {
            Usuario u = db.Usuario.Where(us => us.email == usuario.email && us.contrasena == usuario.contrasena).FirstOrDefault();
            if (u !=null)
            {
                if (u.rol==1)
                {
                    FormsAuthentication.SetAuthCookie(usuario.email, false);
                    Session["id"] = u.id;
                    Session["nombre"] = u.nombres;
                    if (u.foto == null)
                    {
                        Session["foto"] = "/Image/defecto.png";
                    }
                    else
                    {
                        Session["foto"] = u.foto;
                    }
                    return RedirectToAction("Index", "Clase");
                }
                else if (u.rol == 2)
                {
                    FormsAuthentication.SetAuthCookie(usuario.email, false);
                    Session["id"] = u.id;
                    Session["nombre"] = u.nombres;
                    return RedirectToAction("Index", "Usuario");
                }
                    
            }
            else if(u==null)
            {
                ViewBag.NoValido = "Usuario o contraseña incorrectas";
                return View();
            }

            return View();
        }

        public ActionResult CerrarSesion()
        {
            FormsAuthentication.SignOut();
            Session.Contents.RemoveAll();
            return RedirectToAction("IniciarSesion");
        }

        public ActionResult RecuperarContrasena()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RecuperarContrasena(Usuario usuario)
        {

            Usuario u = db.Usuario.Where(us => us.email == usuario.email).FirstOrDefault();
            if (u != null)
            {
                MailMessage correo = new MailMessage();
                SmtpClient protocolo = new SmtpClient();
                correo.To.Add(u.email);
                correo.From = new MailAddress("fast.learning.admim@gmail.com", "FastLearning - Admin");
                correo.Subject = "Envio de Contraseña";
                correo.Body = "Su contraseña es: " + u.contrasena;
                correo.IsBodyHtml = false;


                protocolo.Credentials = new System.Net.NetworkCredential("fast.learning.admim@gmail.com", "jordan72184894");
                protocolo.Port = 25;
                protocolo.Host = "smtp.gmail.com";
                protocolo.EnableSsl = true;

                try
                {
                    protocolo.Send(correo);
                    ViewBag.Correo = "Se envió su contraseña a su correo";
                }
                catch (Exception)
                {
                    ViewBag.Correo = "Hubo un error";
                    throw;
                }
                return View();
            }
            else
            {
                ViewBag.Correo = "No existe un usuario con este correo";
                return View();
            }

            
        }
    }
}