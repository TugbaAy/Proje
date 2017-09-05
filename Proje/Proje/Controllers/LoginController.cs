using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proje.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Autherize(Proje.Models.Login userModel)
        {
            using (Models.ProjeEntities1 db = new Models.ProjeEntities1())
            {
              var userDetails = db.Logins.Where(x => x.yetkili_name == userModel.yetkili_name 
                                                && 
                                                x.yetkili_password == userModel.yetkili_password).FirstOrDefault();

                if (userDetails == null)
                {
                //    userModel.LoginErrorMessage = "Wrong username or password. ";
                    return View("Login", userModel);
                }
                else
                {
                    Session["userId"] = userDetails.yetkili_id;
                    Session["UserName"] = userDetails.yetkili_name;
                    return RedirectToAction("Panel", "Panel");
                }
            }
        }

        public ActionResult LogOut() 
        {
            Session["UserId"] = null;
            Session.Abandon();
            return RedirectToAction("Index", "Home");

        }
    }
}