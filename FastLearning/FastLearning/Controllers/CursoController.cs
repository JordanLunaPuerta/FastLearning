﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FastLearning.Models;
using PagedList;

namespace FastLearning.Controllers
{
    public class CursoController : Controller
    {
        FastLearningDBEntities db = new FastLearningDBEntities();
        // GET: Curso
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
                    List<Curso> cursos = db.Curso.ToList();
                    if (!String.IsNullOrEmpty(buscar))
                    {
                        cursos = cursos.Where(c => c.nombre.Contains(buscar)).ToList();
                    }
                    int pageSize = 6;
                    int pageNumber = (page ?? 1);

                    return View(cursos.ToPagedList(pageNumber, pageSize));
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

        // GET: Curso/Create
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

        // POST: Curso/Create
        [HttpPost]
        public ActionResult Crear(Curso curso)
        {
            try
            {
                curso.fecha_registro = DateTime.Now;
                db.Curso.Add(curso);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error al agregar curso", ex);
                return View();
            }
        }

        // GET: Curso/Edit/5
        public ActionResult Editar(int id)
        {            
            if (Session["id"] != null)
            {
                int id_usuario = Convert.ToInt32(Session["id"]);
                Usuario usuario = db.Usuario.Find(id_usuario);
                if (usuario.rol == 2)
                {
                    Curso curso = db.Curso.Find(id);
                    return View(curso);
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

        // POST: Curso/Edit/5
        [HttpPost]
        public ActionResult Editar(Curso curso)
        {
            try
            {
                Curso c = db.Curso.Find(curso.id);
                c.nombre = curso.nombre;
                c.descripcion = curso.descripcion;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
