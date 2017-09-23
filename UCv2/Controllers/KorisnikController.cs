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
    public class KorisnikController : Controller
    {      
        private StripContext context = new StripContext();
        // GET: Korisnik
        public ActionResult Index()
        {
            if (Session["KorisnikId"] == null && Session["AdminId"] == null)
            {
                return RedirectToAction("LoginAction", "Login");
            }
            return View(context.Korisnici.ToList());
        }
        [HttpGet]
        public ActionResult Create()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "Korisnik", Value = "1" });
            items.Add(new SelectListItem { Text = "Napredni korisnik", Value = "2" });
            ViewBag.Uloga = items;



            return View(new Korisnik());
        }

        [HttpPost]
        public ActionResult Create(Korisnik korisnik)
        {
            if (ModelState.IsValid)
            {
                
                context.Korisnici.Add(korisnik);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View(korisnik);
            }
        }
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (Session["KorisnikId"] == null && Session["AdminId"] == null)
            {
                return RedirectToAction("LoginAction", "Login");
            }
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "Korisnik", Value = "1" });
            items.Add(new SelectListItem { Text = "Napredni korisnik", Value = "2" });
            ViewBag.Uloga = items;
            Korisnik korisnik = context.Korisnici.Find(id);
            if (korisnik == null)
            {
                return HttpNotFound();
            }
            return View(korisnik);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Korisnik korisnik)
        {
            if (Session["KorisnikId"] == null && Session["AdminId"] == null)
            {
                return RedirectToAction("LoginAction", "Login");
            }

            if (ModelState.IsValid)
            {
                context.Entry(korisnik).State = EntityState.Modified;
                context.SaveChanges();            
                return RedirectToAction("Index");
            }
            return View(korisnik);
        }
    }
}