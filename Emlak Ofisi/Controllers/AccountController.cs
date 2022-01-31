using Emlak_Ofisi.Identity;
using Emlak_Ofisi.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Emlak_Ofisi.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<ApplicationUser> userManager;
        private RoleManager<ApplicationRole> RoleManager;
        public AccountController()
        {
            var userStore = new UserStore<ApplicationUser>(new IdentityDataContext());
            userManager = new UserManager<ApplicationUser>(userStore);
            var roleStore = new RoleStore<ApplicationRole>(new IdentityDataContext());
            RoleManager = new RoleManager<ApplicationRole>(roleStore);
        }
        public ActionResult SifreDegistir()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult SifreDegistir(SifreDegistirme model)
        {
            if (ModelState.IsValid)
            {
                var user = userManager.ChangePassword(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
                return View("Update");
            }
            return View(model);
        }

        //Get
        public ActionResult Profil()
        {
            var id = HttpContext.GetOwinContext().Authentication.User.Identity.GetUserId();
            var user = userManager.FindById(id);
            var data = new ProfilGuncelleme()
            {
                id = user.Id,
                Name=user.Name,
                Username=user.UserName,
                Surname=user.Surname,
                Email=user.Email
                
            };
            return View(data);
        }
        [HttpPost]
        public ActionResult Profil(ProfilGuncelleme model)
        {
            var user = userManager.FindById(model.id);
            user.Name = model.Name;
            user.Surname = model.Surname;
            user.UserName = model.Username;
            user.Email = model.Email;
            userManager.Update(user);
            return View("Update");
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Login model, string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = userManager.Find(model.Username, model.Password);
                if (user!=null)
                {
                    var authManager = HttpContext.GetOwinContext().Authentication;
                    var identityclaims = userManager.CreateIdentity(user, "ApplicationCookie");
                    var authProperties = new AuthenticationProperties();
                    authProperties.IsPersistent = model.RememberMe;
                    authManager.SignIn(authProperties, identityclaims);
                    if (!String.IsNullOrEmpty(ReturnUrl))
                    {
                        return Redirect(ReturnUrl);
                    }
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("LoginUserError", "Böyle bir kullanıcı yok");
                }
            }
            return View(model);
        }
        public ActionResult Logout()
        {
            var authManager = HttpContext.GetOwinContext().Authentication;
            authManager.SignOut();
            return RedirectToAction("Index","Home");
        }

        public ActionResult Register()
         {
            return View();
         }
        [HttpPost]
        public ActionResult Register(Register model)
        {
            if(ModelState.IsValid)
            {
                var user = new ApplicationUser();
                user.Name = model.Name;
                user.UserName = model.Username;
                user.Email = model.Email;
                var result = userManager.Create(user, model.Password);
                if (result.Succeeded)
                {
                    if (RoleManager.RoleExists("user"))
                    {
                        userManager.AddToRole(user.Id, "user");

                    }
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    ModelState.AddModelError("RegisterUserError", "Kullanıcı Oluşturma Hatası");
                }
            }
            return View(model);
        }
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
    }
}