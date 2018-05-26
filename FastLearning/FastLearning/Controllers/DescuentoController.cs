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
            if (buscar != null)
            {
                page = 1;
            }
            else
            {
                buscar = currentFilter;
            }

            ViewBag.CurrentFilter = buscar;
            List<Descuento> descuentos = db.Descuento.Where(d=>d.estado==1).ToList();
            if (!String.IsNullOrEmpty(buscar))
            {
                descuentos = descuentos.Where(d => d.nombre.Contains(buscar)).ToList();
            }

            int pageSize = 6;
            int pageNumber = (page ?? 1);
            return View(descuentos.ToPagedList(pageNumber, pageSize));
        }

        

        public ActionResult Crear()
        {
            return View();
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
            Descuento descuento = db.Descuento.Find(id);
            return View(descuento);
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
            Descuento descuento= db.Descuento.Find(id);
            descuento.estado = 2;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}