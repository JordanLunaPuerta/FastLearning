using FastLearning.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FastLearning.Controllers
{
    public class DescuentoUsuarioController : Controller
    {
        FastLearningDBEntities db = new FastLearningDBEntities();
        // GET: DescuentoUsuario
        public ActionResult Index()
        {
            int id = Convert.ToInt32(Session["id"]);
            Usuario u = db.Usuario.Find(id);
            ViewBag.Puntos = u.puntos;
            return View(db.DescuentoUsuario.Where(d=>d.id_usuario==id).ToList());
        }

        public ActionResult ListarDescuentosDisponibles()
        {
            List<Descuento> descuentos = db.Descuento.Where(d => d.estado == 1).ToList();
            return PartialView(descuentos);
        }

        public ActionResult CanjearPuntos()
        {
            int id = Convert.ToInt32(Session["id"]);
            Usuario u = db.Usuario.Find(id);
            ViewBag.Puntos = u.puntos;
            SelectList descuentos = new SelectList(db.Descuento.ToList(), "id", "nombre");
            ViewBag.Descuentos = descuentos;
            return View();
        }

        [HttpPost]
        public ActionResult CanjearPuntos(DescuentoUsuario descuentoUsuario)
        {
            int id_usuario = Convert.ToInt32(Session["id"]);
            Usuario usuario = db.Usuario.Find(id_usuario);
            Descuento descuento = db.Descuento.Find(descuentoUsuario.id_descuento);
            int valor_descuento = descuento.valor;
            int puntos_usuario = usuario.puntos;           
            if (valor_descuento > puntos_usuario)
            {
                SelectList descuentos = new SelectList(db.Descuento.ToList(), "id", "nombre");
                ViewBag.Descuentos = descuentos;
                ViewBag.Puntos = usuario.puntos;
                ViewBag.PuntosInsuficientes = "Necesitas más puntos";              
                return View();
            }
            else
            {              
                descuentoUsuario.id_usuario = usuario.id;
                descuentoUsuario.estado_eliminado = 1;
                descuentoUsuario.estado_utilizado = 1;
                descuentoUsuario.fecha_registro = DateTime.Now;
                db.DescuentoUsuario.Add(descuentoUsuario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
}