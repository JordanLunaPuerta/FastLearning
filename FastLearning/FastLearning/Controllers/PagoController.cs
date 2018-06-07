using FastLearning.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FastLearning.Controllers
{
    public class PagoController : Controller
    {
        FastLearningDBEntities db = new FastLearningDBEntities();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PagoPorClase(int id)
        {
            Pago pago = db.Pago.Find(id);
            ClaseUsuario claseUsuario = db.ClaseUsuario.Where(cu => cu.id_pago == id).FirstOrDefault();
            return PartialView(claseUsuario);
        }
    }
}