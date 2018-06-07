using FastLearning.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FastLearning.Controllers
{
    public class DescuentoController : Controller
    {
        FastLearningDBEntities db = new FastLearningDBEntities();

        public ActionResult Index(string currentFilter, string buscar, int? page)
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
                    List<Descuento> descuentos = db.Descuento.Where(d => d.estado == 1).ToList();
                    if (!String.IsNullOrEmpty(buscar))
                    {
                        descuentos = descuentos.Where(d => d.nombre.Contains(buscar)).ToList();
                    }

                    int pageSize = 6;
                    int pageNumber = (page ?? 1);
                    return View(descuentos.ToPagedList(pageNumber, pageSize));
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

        public ActionResult Crear()
        {
            if (Session["id"] != null)
            {
                int id_usuario = Convert.ToInt32(Session["id"]);
                Usuario usuario = db.Usuario.Find(id_usuario);
                if (usuario.rol == 2)
                {
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
        public ActionResult Crear(Descuento descuento)
        {
            try
            {
                descuento.fecha_registro = DateTime.Now;
                descuento.estado = 1;
                db.Descuento.Add(descuento);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex) 
            {
                ModelState.AddModelError("Error al agregar descuento", ex);
                return View();
            }
        }
        public ActionResult Editar(int id)
        {
            
            if (Session["id"] != null)
            {
                int id_usuario = Convert.ToInt32(Session["id"]);
                Usuario usuario = db.Usuario.Find(id_usuario);
                if (usuario.rol == 2)
                {
                    Descuento descuento = db.Descuento.Find(id);
                    return View(descuento);
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
        public ActionResult Editar(Descuento descuento)
        {
            try
            {
                Descuento d = db.Descuento.Find(descuento.id);
                d.nombre = descuento.nombre;
                d.numero = descuento.numero;
                d.valor = descuento.valor;
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
            if (Session["id"] != null)
            {
                int id_usuario = Convert.ToInt32(Session["id"]);
                Usuario usuario = db.Usuario.Find(id_usuario);
                if (usuario.rol == 2)
                {
                    Descuento descuento = db.Descuento.Find(id);
                    if (descuento.DescuentoUsuario.Count == 0)
                    {
                        db.Descuento.Remove(descuento);
                    }
                    else
                    {
                        descuento.estado = 2;
                    }
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