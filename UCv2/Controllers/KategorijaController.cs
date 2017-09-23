using Comic.Context;
using Comic.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Comic.Controllers
{
    public class KategorijaController : Controller
    {
        private StripContext context = new StripContext();
        // GET: Kategorija
        public ActionResult Index()
        {
            if (Session["KorisnikId"] == null && Session["AdminId"] == null)
            {
                return RedirectToAction("LoginAction", "Login");
            }
            return View(context.Categories.ToList());
        }
        [HttpGet]
        public ActionResult Create()
        {

            return View(new Category());
        }

        [HttpPost]
        public ActionResult Create(Category kategorija)
        {
            if (ModelState.IsValid)
            {
                context.Categories.Add(kategorija);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View(kategorija);
            }
        }

        [HttpGet]
        public ActionResult ListAll(string naziv)
        {
            var stripovi = context.Comics.ToList().Where(x => x.Category.Equals(naziv));
            return View(stripovi);
        }

        //[HttpPost]
        //public ActionResult ListAll(Kategorija kategorija)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        context.Kategorije.Add(kategorija);
        //        context.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    else
        //    {
        //        return View(kategorija);
        //    }
        //}

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (Session["KorisnikId"] == null && Session["AdminId"] == null)
            {
                return RedirectToAction("LoginAction", "Login");
            }
            Category kategorija = context.Categories.Find(id);
            Session["ImeKat"] = kategorija.Name;
            if (kategorija == null)
            {
                return HttpNotFound();
            }
            return View(kategorija);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category kategorija)
        {
            if (Session["KorisnikId"] == null && Session["AdminId"] == null)
            {
                return RedirectToAction("LoginAction", "Login");
            }

            if (ModelState.IsValid)
            {
                var ime = Session["ImeKat"].ToString();
                var list = context.Comics.ToList().Where(x => x.Category.Name == ime);
                context.Entry(kategorija).State = EntityState.Modified;
                context.SaveChanges();
                
                foreach(var strip in list)
                {
                    strip.Category = kategorija;

                }
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(kategorija);
        }

       


    }
}