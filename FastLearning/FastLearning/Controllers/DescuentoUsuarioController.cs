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
            return View(db.DescuentoUsuario.Where(d=>d.id_usuario==id && d.estado_eliminado==1).ToList().OrderByDescending(d=>d.fecha_registro));
        }

        public ActionResult ListarDescuentosDisponibles()
        {
            List<Descuento> descuentos = db.Descuento.Where(d => d.estado == 1).ToList();
            return PartialView(descuentos.OrderBy(d=>d.valor));
        }

        public ActionResult CanjearPuntos()
        {
            int id = Convert.ToInt32(Session["id"]);
            Usuario u = db.Usuario.Find(id);
            ViewBag.Puntos = u.puntos;
            SelectList descuentos = new SelectList(db.Descuento.ToList().OrderBy(d => d.valor), "id", "nombre");
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
                SelectList descuentos = new SelectList(db.Descuento.ToList().OrderBy(d => d.valor), "id", "nombre");
                ViewBag.Descuentos = descuentos;
                ViewBag.Puntos = usuario.puntos;
                ViewBag.PuntosInsuficientes = "No tienes los puntos necesarios";
                return View();
            }
            else
            {
                descuentoUsuario.id_usuario = usuario.id;
                descuentoUsuario.estado_eliminado = 1;
                descuentoUsuario.estado_utilizado = 1;
                descuentoUsuario.fecha_registro = DateTime.Now;
                Session["puntos"] = valor_descuento;
                db.DescuentoUsuario.Add(descuentoUsuario);
                usuario.puntos = usuario.puntos - valor_descuento;
                usuario.confirmar_contrasena = usuario.contrasena;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Eliminar(int id)
        {
            DescuentoUsuario descuentoUsuario = db.DescuentoUsuario.Find(id);
            descuentoUsuario.estado_eliminado = 2;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}