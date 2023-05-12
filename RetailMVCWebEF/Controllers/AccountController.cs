using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using RetailMVCWebEF;
using RetailMVCWebEF.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using System.Security.Claims;
using RetailMVCWebEF.App_Start;
using System.Net.Http;
using static System.Net.Mime.MediaTypeNames;
using RetailMVCWebEF.Models.BL;

namespace RetailMVCWebEF.Controllers
{
    public class AccountController : BaseController

    {
        private Models.ML.GestionHosteleriaGenNHibernateEntities1 db = new Models.ML.GestionHosteleriaGenNHibernateEntities1();

        private ApplicationUserManager _userManager;


        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager)
        {
            UserManager = userManager;

        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                System.Diagnostics.Debug.WriteLine(error);
            }
        }


        // GET: Account

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            return View();
        }

    [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
          

            if (ModelState.IsValid)
            {
                var user = await UserManager.FindAsync(model.Email, model.Password);

                if (user != null)
                {
                    if (user.LockoutEnabled)
                    {
                        ModelState.AddModelError("Password", "Usuario bloqueado.");

                        return View(model);
                    }
                    await SignInAsync(user, model.RememberMe);

                  //  var aux = UserRepository.Find(user.Id);
                  // Application.RegisterOnlineUser("111111", aux.Id, aux.UserName, aux.UserName, "Nombre Entidad", "127.0.0.1");

                    return RedirectToAction("Index", "TPV");
                }
                else
                {
                    ModelState.AddModelError("Password", "Usuario y/o contraseña no válidos.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        protected async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, await user.GenerateUserIdentityAsync(UserManager));
        }

        public ActionResult Register()
        {
            return View();
        }

        [System.Web.Mvc.HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {


                var user = new ApplicationUser { UserName = model.Email, Email = model.Email};
               
                var result = await UserManager.CreateAsync(user, model.Password);

                AddErrors(result);

                if (result.Succeeded)
                {
                //    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error);
                }
            }

            return View(model);
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

      

        // GET: Account/Create
        public ActionResult Create()
        {
            return View();
        }



        // POST: Account/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [System.Web.Mvc.HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] ApplicationUser applicationUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(applicationUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(applicationUser);
        }

    
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
