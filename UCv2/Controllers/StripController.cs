using Comic.Context;
using Comic.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Comic.Controllers
{
    public class StripController : Controller
    {
        private StripContext context = new StripContext();
        // GET: Strip
        public ActionResult Index(string search)
        {
            if (Session["KorisnikId"] == null && Session["AdminId"] == null)
            {
                return RedirectToAction("LoginAction", "Login");
            }
            var activeUserId = (int)Session["KorisnikId"];
            var userComicsList = context.Comics.ToList().Where(x => x.OwnerId == activeUserId);
            if (!String.IsNullOrEmpty(search))
            {
                userComicsList = userComicsList.Where(s => s.Name.ToLower().Contains(search));
            }
            return View(userComicsList);
        }
        [HttpGet]
        public ActionResult Create()
        {
            if (Session["KorisnikId"] == null && Session["AdminId"] == null)
            {
                return RedirectToAction("LoginAction", "Login");
            }
            List<SelectListItem> items = new List<SelectListItem>();
            var allCategories = context.Categories.ToList();
            foreach (var cat in allCategories)
            {
                items.Add(new SelectListItem { Text = cat.Name, Value = cat.Name });
            }
            ViewBag.Kategorija = items;
            return View(new Models.Comic());
        }

        [HttpPost]
        public ActionResult Create(Models.Comic comic, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    var photo = new FilePath
                    {
                        FileName = System.IO.Path.GetFileName(upload.FileName),
                        FileType = FileType.Photo
                    };
                    comic.FilePaths = new List<FilePath>();
                    comic.FilePaths.Add(photo);
                }

                var activeUserId = (int)Session["KorisnikId"];
                comic.OwnerId = activeUserId;
                context.Comics.Add(comic);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View(comic);
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
            var allCategories = context.Categories.ToList();
            foreach (var cat in allCategories)
            {
                items.Add(new SelectListItem { Text = cat.Name, Value = cat.Name });
            }
            ViewBag.Kategorija = items;



            //Strip strip = context.Stripovi.Find(id);
            Models.Comic comic = context.Comics.Include(i => i.FilePaths).SingleOrDefault(i => i.Id == id);
            if (comic == null)
            {
                return HttpNotFound();
            }
            return View(comic);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Models.Comic comic, HttpPostedFileBase upload)
        {
            if (Session["KorisnikId"] == null && Session["AdminId"] == null)
            {
                return RedirectToAction("LoginAction", "Login");
            }
            if (ModelState.IsValid)
            {


                if (upload != null && upload.ContentLength > 0)
                {
                    if (context.FilePaths.Select(x => x.Comic.Id == comic.Id).Count() > 0)
                    {
                        var l = context.FilePaths.Where(x => x.Comic.Id == comic.Id).First();

                        context.FilePaths.Remove(l);

                    }
                    var i = context.FilePaths.ToList();
                    var j = i.Last().FilePathId;
                    var photo = new FilePath
                    {
                        FileName = System.IO.Path.GetFileName(upload.FileName),
                        FileType = FileType.Photo,
                        Comic = comic,
                    };
                    comic.FilePaths = new List<FilePath>();
                    comic.FilePaths.Add(photo);
                    context.FilePaths.Add(photo);
                }
                context.Entry(comic).State = EntityState.Modified;

                context.SaveChanges();
                if (Session["KorisnikId"] != null)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("AllComics");
                }

            }
            return View(comic);
        }

        public ActionResult AllComics(string search)
        {
            if (Session["AdminId"] == null)
            {
                return RedirectToAction("LoginAction", "Login");
            }

            var comics = (IEnumerable<Models.Comic>)context.Comics.ToList();
            if (!String.IsNullOrEmpty(search))
            {
                 comics = comics.Where(s => s.Name.ToLower().Contains(search));
            }
            return View(comics);
        }
    }
}