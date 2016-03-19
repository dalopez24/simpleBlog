using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using simpleBlog.Contracts.Repositories;
using simpleBlog.Model;
using simpleBlog.WebUI.viewModels;

namespace simpleBlog.WebUI.Controllers
{
    public class AuthController : Controller
    {
        private IBaseRepository<User> users;

        public AuthController(IBaseRepository<User> users)
        {
            this.users = users;
        }


        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {

            var user = users.GetAll().FirstOrDefault(u => u.Name == model.Name);

            if (user == null || !user.GetPassword(model.Password))
                ModelState.AddModelError("Name", "Username or password incorrect");

            if (!ModelState.IsValid)
                return View(model);

            FormsAuthentication.SetAuthCookie(model.Name, true);


            if (!string.IsNullOrWhiteSpace(returnUrl))
                return Redirect(returnUrl);


            return RedirectToAction("Home");
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }


    }
}