using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proje.Models;
using System.Web.Helpers;

namespace Proje.Controllers
{
    public class PanelController : Controller
    {
        Models.ProjeEntities1 db;
        // GET: Panel
        public ActionResult Panel()
        {
            try
            {
                int sID = Convert.ToInt32(Session["userId"].ToString());

                string sName = Session["UserName"].ToString();
                
                db = new Models.ProjeEntities1();

                List<Models.Form> formList = new List<Models.Form>();
                
                formList = db.Forms.OrderByDescending(x => x.userId).ToList();
                
                return View(formList);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Login");
            }

        }

        public ActionResult FormDetails(int ID)
        {
            db = new Models.ProjeEntities1();
            
            Models.Form form =  db.Forms.Where(x => x.userId == ID).FirstOrDefault();
            
            return View(form);
        }

        public ActionResult Yanitla(int ID) 
        {
            db = new Models.ProjeEntities1();
            Models.Form form = db.Forms.Where(x => x.userId == ID).FirstOrDefault();

            try
            {

                WebMail.SmtpServer = "smtp.gmail.com";
                WebMail.EnableSsl = true;
                WebMail.UserName = "yetkili12345@gmail.com";
                WebMail.Password = "admin123456789";
                WebMail.SmtpPort = 587;
                form.userMessage = form.yetkiliMessage;

                WebMail.Send(
                form.userMail, // alıcının mail adresi
                "İş Başvurusu Yanıt", //mailin konusu
                form.userMessage, //mailin kendisi
                null,
                null,
                null,
                true,
                null,
                null,
                null,
                null,
                null,
                null
                );

                return RedirectToAction("Gönderildi");
            }
            catch (Exception ex)
            {
                ViewData.ModelState.AddModelError("_HATA", ex.Message);
                return View("Panel");
            }
        }
       
    }
}