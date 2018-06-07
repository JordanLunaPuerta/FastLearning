using FastLearning.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace FastLearning.Controllers
{
    public class ClaseController : Controller
    {

        FastLearningDBEntities db = new FastLearningDBEntities();

        public void CambiarEstado()
        {
            List<Clase> clases = db.Clase.ToList();
            DateTime fecha_actual = DateTime.Now;
            foreach (Clase clase in clases)
            {
                int resultado_inicial = DateTime.Compare(fecha_actual, clase.fecha_inicial);
                int resultado_final = DateTime.Compare(fecha_actual, clase.fecha_final);
                if (resultado_inicial >= 0)
                {
                    clase.id_estado = 2;
                }
                if(resultado_final >= 0)
                {
                    clase.id_estado = 3;
                }
            }
            db.SaveChanges();
        }

        public ActionResult Index(string buscar)
        {
            if (Session["id"]!=null)
            {
                int id_usuario = Convert.ToInt32(Session["id"]);
                Usuario usuario = db.Usuario.Find(id_usuario);
                if (usuario.rol == 1)
                {
                    CambiarEstado();
                    Session.Contents.Remove("error");
                    int id_estado = 1;
                    List<Clase> clases = db
                        .Clase
                        .Where(c => c.id_estado == id_estado && c.Habilidad.Usuario.id != id_usuario && c.vacantes > c.ClaseUsuario.Count && c.fecha_inicial>=DateTime.Now).ToList();
                    if (!String.IsNullOrEmpty(buscar))
                    {
                        clases = clases.Where(c => c.Habilidad.Tema.nombre.Contains(buscar)).ToList();
                    }
                    return View(clases.OrderByDescending(c => c.fecha_registro));
                }
                else
                {
                    return RedirectToAction("IniciarSesion", "Home");
                }
                
            }
            else
            {
                return RedirectToAction("IniciarSesion","Home");
            }
            
        }

        public ActionResult ListarClasePorInscripcion()
        {
            if (Session["id"] != null)
            {
                int id = Convert.ToInt32(Session["id"]);
                Usuario usuario = db.Usuario.Find(id);
                if (usuario.rol == 1)
                {
                    CambiarEstado();
                    return View(db.ClaseUsuario.Where(s => s.id_usuario == id).ToList().OrderByDescending(c => c.Clase.fecha_registro));
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

        public ActionResult Mensaje()
        {
            CambiarEstado();
            return PartialView();
        }
        
        public ActionResult CalificarAlumnos(int id)
        {
            if (Session["id"] != null)
            {
                int id_usuario = Convert.ToInt32(Session["id"]);
                Usuario usuario = db.Usuario.Find(id_usuario);
                if (usuario.rol == 1)
                {
                    CambiarEstado();
                    Clase clase = db.Clase.Find(id);
                    DateTime fecha_actual = DateTime.Now;
                    List<ClaseUsuario> cu = db.ClaseUsuario.Where(c=>c.id_clase==clase.id).ToList();
                    int resultado = DateTime.Compare(fecha_actual, clase.fecha_final);

                    if (resultado <= 0 && clase.id_estado == 2  && cu.Where(cus=>cus.calificacion_alumno!=null).Count()==0)
                    {
                        List<ClaseUsuario> claseUsuarios = db.ClaseUsuario.Where(cus=>cus.id_clase==clase.id).ToList();
                        return PartialView(claseUsuarios);
                    }
                    else
                    {
                        CambiarEstado();
                        return PartialView("Mensaje");
                    }
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
        public ActionResult CalificarAlumnos(List<ClaseUsuario> claseUsuarios)
        {
            foreach (ClaseUsuario cu in claseUsuarios)
            {
                ClaseUsuario claseUsuario = db.ClaseUsuario.Find(cu.id_clase,cu.id_usuario);
                claseUsuario.calificacion_alumno = cu.calificacion_alumno;
                claseUsuario.Usuario.calificacion_alumno= (claseUsuario.Usuario.calificacion_alumno + cu.calificacion_alumno)/ 2;
                claseUsuario.Usuario.puntos = claseUsuario.Usuario.puntos + Convert.ToInt32(cu.calificacion_alumno);
                claseUsuario.Usuario.confirmar_contrasena = claseUsuario.Usuario.contrasena;
            }
            db.SaveChanges();
            CambiarEstado();
            return RedirectToAction("ListarClasePorUsuario");
        
        }
       

        public ActionResult CalificarProfesor(int id)
        {
            if (Session["id"] != null)
            {
                int id_usuario = Convert.ToInt32(Session["id"]);
                Usuario usuario = db.Usuario.Find(id_usuario);
                if (usuario.rol == 1)
                {
                    CambiarEstado();
                    Clase clase = db.Clase.Find(id);
                    DateTime fecha_actual = DateTime.Now;
                    ClaseUsuario cu = db.ClaseUsuario.Find(clase.id, usuario.id);
                    int resultado = DateTime.Compare(fecha_actual, clase.fecha_final);

                    if (resultado <= 0 && clase.id_estado == 2 && cu.calificacion_profesor == null)
                    {
                        ClaseUsuario claseUsuario = db.ClaseUsuario.Find(clase.id, usuario.id);
                        return PartialView(claseUsuario);
                    }
                    else
                    {
                        CambiarEstado();
                        return PartialView("Mensaje");
                    }
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
        public ActionResult CalificarProfesor(ClaseUsuario claseUsuario)
        {
            int id_usuario = Convert.ToInt32(Session["id"]);
            Usuario usuario = db.Usuario.Find(id_usuario);
            ClaseUsuario cu = db.ClaseUsuario.Find(claseUsuario.id_clase, usuario.id);
            cu.calificacion_profesor = claseUsuario.calificacion_profesor;
            cu.Clase.Habilidad.Usuario.calificacion_profesor = (cu.Clase.Habilidad.Usuario.calificacion_profesor + cu.calificacion_profesor)/2;
            cu.Clase.Habilidad.Usuario.confirmar_contrasena = cu.Clase.Habilidad.Usuario.contrasena;
            db.SaveChanges();
            CambiarEstado();
            return RedirectToAction("ListarClasePorInscripcion");
        }
        public ActionResult ListarClasePorUsuario()
        {
            if (Session["id"] != null)
            {
                int id = Convert.ToInt32(Session["id"]);
                Usuario usuario = db.Usuario.Find(id);
                if (usuario.rol == 1)
                {
                    CambiarEstado();
                    return View(db.Clase.Where(c => c.Habilidad.id_usuario == id && c.id_estado != 4).ToList().OrderByDescending(c => c.fecha_registro));
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

        public ActionResult ListarAlumnosPorClase(int id)
        {
            if (Session["id"] != null)
            {
                int id_usuario = Convert.ToInt32(Session["id"]);
                Usuario usuario = db.Usuario.Find(id_usuario);
                if (usuario.rol == 1)
                {
                    CambiarEstado();
                    Clase clase = db.Clase.Find(id);
                    return View(db.ClaseUsuario.Where(s => s.Clase.id == id).ToList());
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

        public ActionResult Detalles(int id)
        {
            if (Session["id"] != null)
            {
                int id_usuario = Convert.ToInt32(Session["id"]);
                Usuario usuario = db.Usuario.Find(id_usuario);
                if (usuario.rol == 1)
                {
                    CambiarEstado();
                    Clase clase = db.Clase.Find(id);                    
                    ViewBag.clase = clase.Habilidad.Tema.nombre;
                    ViewBag.Error = Session["error"];
                    return View(clase);
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

        public ActionResult InscribirseEnClase(int id)
        {
            if (Session["id"] != null)
            {
                int id_usuario = Convert.ToInt32(Session["id"]);
                Usuario usuario = db.Usuario.Find(id_usuario);
                if (usuario.rol == 1)
                {
                    Session["ClaseInscrita"] = id;
                    SelectList descuentos = new SelectList(
                        db.DescuentoUsuario.Where
                        (du => du.id_usuario == id_usuario && du.estado_eliminado == 1 && du.estado_utilizado == 1)
                        .ToList()
                        .OrderBy(du => du.Descuento.nombre), "id", "Descuento.nombre");
                    ViewBag.Descuentos = descuentos;
                    return PartialView();
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
        public ActionResult InscribirseEnClase(string id_descuento_usuario)
        {           
            int id_usuario = Convert.ToInt32(Session["id"]);
            Usuario usuario = db.Usuario.Find(id_usuario);
            Clase clase = db.Clase.Find(Convert.ToInt32(Session["ClaseInscrita"]));
            Session["error"] = "";
            if (db.ClaseUsuario.Find(clase.id, usuario.id) == null)
            {
                if (!String.IsNullOrEmpty(id_descuento_usuario))
                {
                    Session["error"] = "";
                    ClaseUsuario claseusuario = db.ClaseUsuario.Create();
                    claseusuario.id_clase = clase.id;
                    claseusuario.id_usuario = usuario.id;
                    Pago pago = db.Pago.Create();
                    claseusuario.id_pago = pago.id;
                    pago.monto_inicial = clase.precio;
                    pago.id_descuento_usuario = Convert.ToInt32(id_descuento_usuario);
                    DescuentoUsuario descuentoUsuario = db.DescuentoUsuario.Find(Convert.ToInt32(id_descuento_usuario));
                    pago.monto_final = clase.precio - (clase.precio * descuentoUsuario.Descuento.numero);
                    descuentoUsuario.estado_utilizado = 2;
                    pago.fecha_registro = DateTime.Now;
                    db.ClaseUsuario.Add(claseusuario);
                    db.Pago.Add(pago);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    Session["error"] = "";
                    ClaseUsuario claseusuario = db.ClaseUsuario.Create();
                    claseusuario.id_clase = clase.id;
                    claseusuario.id_usuario = usuario.id;
                    Pago pago = db.Pago.Create();
                    claseusuario.id_pago = pago.id;
                    pago.monto_inicial = clase.precio;
                    pago.monto_final = clase.precio;
                    pago.fecha_registro = DateTime.Now;
                    db.ClaseUsuario.Add(claseusuario);
                    db.Pago.Add(pago);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            else
            {
                Session["error"] = "Usted ya esta inscrito a esta clase";
                return RedirectToAction("Detalles", "Clase", new { id = clase.id });
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
                    CambiarEstado();
                    SelectList habilidades = new SelectList(db.Habilidad.Where(h => h.id_usuario == id_usuario).ToList().OrderBy(h=>h.Tema.nombre), "id", "Tema.nombre");
                    ViewBag.Habilidades = habilidades;
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
        public ActionResult Crear(Clase clase)
        {
            try
            {
                if (clase.hora_final<=clase.hora_inicial && clase.fecha_final<=clase.fecha_inicial)
                {
                    int id = Convert.ToInt32(Session["id"]);
                    SelectList habilidades = new SelectList(db.Habilidad.Where(h => h.id_usuario == id).ToList(), "id", "Tema.nombre");
                    ViewBag.Habilidades = habilidades;
                    ViewBag.FechaMal = "La fecha final tiene que ser mayor que la inicial";
                    ViewBag.HoraMal = "La hora final tiene que ser mayor que la inicial";
                    return View();                   
                }
                else if (clase.hora_final<=clase.hora_inicial)
                {
                    int id = Convert.ToInt32(Session["id"]);
                    SelectList habilidades = new SelectList(db.Habilidad.Where(h => h.id_usuario == id).ToList(), "id", "Tema.nombre");
                    ViewBag.Habilidades = habilidades;
                    ViewBag.HoraMal = "La hora final tiene que ser mayor que la inicial";
                    return View();
                }
                else if(clase.fecha_final<=clase.fecha_inicial)
                {
                    int id = Convert.ToInt32(Session["id"]);
                    SelectList habilidades = new SelectList(db.Habilidad.Where(h => h.id_usuario == id).ToList(), "id", "Tema.nombre");
                    ViewBag.Habilidades = habilidades;
                    ViewBag.FechaMal = "La fecha final tiene que ser mayor que la inicial";
                    return View();
                }
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

        public ActionResult Eliminar(int id)
        {
            if (Session["id"] != null)
            {
                int id_usuario = Convert.ToInt32(Session["id"]);
                Usuario usuario = db.Usuario.Find(id_usuario);
                if (usuario.rol == 1)
                {
                    CambiarEstado();
                    Clase clase = db.Clase.Find(id);
                    if (clase.ClaseUsuario.Count == 0)
                    {
                        db.Clase.Remove(clase);
                        db.SaveChanges();
                        return RedirectToAction("ListarClasePorUsuario");
                    }
                    return RedirectToAction("ListarClasePorUsuario");
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
