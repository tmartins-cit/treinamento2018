using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Treinamento.Presentation.WebMVC.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        [HttpGet]
        public ActionResult Index(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public ActionResult Index(Models.LoginModel loginModel, string returnUrl)
        {
            if(loginModel.Login == "admin" && loginModel.Password == "senha")
            {
                FormsAuthentication.SetAuthCookie(loginModel.Login, false);
                return Redirect(returnUrl);
            }
            ViewBag.Message = "Falha na Autenticação";
            return View();
        }

        [HttpPost]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
    }
}