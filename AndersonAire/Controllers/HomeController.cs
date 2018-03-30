using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using AndersonAire.Models;

namespace AndersonAire.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.HomeClass = "active";
            ViewBag.ContactClass = string.Empty;
            ViewBag.AboutClass = string.Empty;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            ViewBag.HomeClass = string.Empty;
            ViewBag.ContactClass = string.Empty;
            ViewBag.AboutClass = "active";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            ViewBag.HomeClass = string.Empty;
            ViewBag.ContactClass = "active";
            ViewBag.AboutClass = string.Empty;

            return View();
        }

        [HttpPost]
        public ActionResult Contact(ContactUs model)
        {
            ViewBag.Message = "Your contact page.";
            ViewBag.HomeClass = string.Empty;
            ViewBag.ContactClass = "active";
            ViewBag.AboutClass = string.Empty;

            if (ModelState.IsValid)
            {
                var body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
                var message = new MailMessage();
                message.To.Add(new MailAddress("steve@andersonaire.com"));  // replace with valid value 
                message.From = new MailAddress(model.Email);  // replace with valid value
                message.Subject = model.Subject;
                message.Body = string.Format(body, model.Name, model.Email, model.Message);
                message.IsBodyHtml = true;

                using (var smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = "steve@andersonaire.com",  // replace with valid value
                        Password = System.Configuration.ConfigurationManager.AppSettings["emailPassword"]  // replace with valid value
                    };
                    smtp.Credentials = credential;
                    smtp.Host = "relay-hosting.secureserver.net";
                    smtp.Port = 25;
                    smtp.EnableSsl = false;
                    smtp.Send(message);
                }
            }

            return View();
        }
    }
}