using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Proje.Models;

namespace Proje.Controllers
{
    public class MailController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Mail()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Mail(Proje.Models.Form userModel)
        {
            try
            {
            
                WebMail.SmtpServer = "smtp.gmail.com";
                WebMail.EnableSsl = true;
                WebMail.UserName = userModel.userMail;
                WebMail.Password = userModel.userPassword; 
                WebMail.SmtpPort = 587;
                userModel.userMessage = "Ad : " + userModel.userName + "\n" +
                                        "Soyad : " + userModel.userSurname + "\n" + "\n" +
                                        "Cinsiyet: " + userModel.userGender + "\n" +
                                        "Doğum Günü: " + userModel.userBirthday + "\n" +
                                        "Telefon Numarası: " + userModel.userTel + "\n" +
                                        "Okul Adı: " + userModel.userSchoolName + "\n" +
                                        "Okul Bitiş Yılı: " + userModel.userSchoolEndYear + "\n" +
                                        "Mezun Olduğu Ortalama: " + userModel.userGrade + "\n" +
                                        "Mesaj: " + userModel.userMessage + "\n";

                WebMail.Send(
                userModel.userMail, // alıcının mail adresi                 
                userModel.userSubject, //mailin konusu 
                userModel.userMessage, //mailin kendisi 
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

                //database kaydetme
                 Form.mailEkle(userModel.userName,
                                    userModel.userSurname,
                                    userModel.userBirthday,
                                    userModel.userGender,
                                    userModel.userTel,
                                    userModel.userSchoolName,
                                    userModel.userSchoolEndYear,
                                    userModel.userGrade,
                                    userModel.userMail,
                                    userModel.userPassword,
                                    userModel.userSubject,
                                    userModel.userMessage);

                return RedirectToAction("Gönderildi");
            }
            catch (Exception ex)
            {
                ViewData.ModelState.AddModelError("_HATA", ex.Message);
                return View("Mail");
            }

        }
        public string Gonderildi()
        {
            return "İletiniz başarıyla gönderildi";

        }
    }
}