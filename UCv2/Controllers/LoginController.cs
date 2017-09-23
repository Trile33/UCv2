using Comic.Context;
using Comic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Comic.Controllers
{
    public class LoginController : Controller
    {
        private StripContext context = new StripContext();
        // GET: Login
        public ActionResult LoginAction()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LoginAction(User korisnik)
        {
            if (ModelState.IsValid)
            {
                
                    var obj = context.Users.Where(a => a.Username.Equals(korisnik.Username) && a.Password.Equals(korisnik.Password)).FirstOrDefault();
                    if (obj != null)
                    {
                        Session["KorisnikId"] = obj.Id;
                        Session["Username"] = obj.Username.ToString();
                        Session["Uloga"] = obj.Role;
                        Session["Korisnik"] = obj;
                        return RedirectToAction("Index","Strip");
                    }
                    else
                        {
                        var adm = context.Admins.Where(a => a.Username.Equals(korisnik.Username) && a.Password.Equals(korisnik.Password)).FirstOrDefault();
                    if (adm != null)
                    {
                        Session["AdminId"] = adm.Id.ToString();
                       
                        return RedirectToAction("Index", "Home");
                    }
                    
                }
                
            }
            return View(korisnik);
        }
        [HttpGet]
        public ActionResult Logout(User korisnik)
        {
            if (ModelState.IsValid)
            {
                Session["KorisnikId"] = null;
                Session["AdminId"] = null;
            }
            return RedirectToAction("LoginAction", "Login");

        }

        public ActionResult UserDashBoard()
        {
            //if (Session["UserID"] != null)
            //{
            //    return View();
            //}
            //else
            //{
            //    return RedirectToAction("Login");
            //}
            return RedirectToAction("Login");
        }
    }
}  
    
