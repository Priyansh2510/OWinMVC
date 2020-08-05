using BAL.Interface;
using BAL.Model;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using TEST.Models;

namespace TEST.Controllers
{
    public class LoginController : Controller
    {

        private IUserBAL _userBAL = null;

        public LoginController(IUserBAL userBAL)
        {
            _userBAL = userBAL;
        }

        // GET: Logi
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login(string returnURL)
        {
            try
            {
                if (this.Request.IsAuthenticated)
                {
                    return this.RedirectToLocal(returnURL);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
          
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(LoginViewModel loginViewModel, string returnURL)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UserModel userModel = _userBAL.FindValidUser(loginViewModel.UserName, loginViewModel.Password);

                    if (userModel != null && userModel.UserId != null)
                    {
                        this.SignInUser(loginViewModel.UserName, false);

                        return this.RedirectToLocal(returnURL);
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid username or password");
                    }
                }

            }
            catch (Exception ex)
            {

                throw;
            }
            return this.View(loginViewModel);

        }

        private void SignInUser(string username, bool isPersistent)
        {
            var claims = new List<Claim>();

            try
            {
                claims.Add(new Claim(ClaimTypes.Name, username));
                var claimIdenties = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
                var ctx = Request.GetOwinContext();
                var authenticationManager = ctx.Authentication;

                authenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, claimIdenties);

            }
            catch (Exception ex)
            {

                throw;
            }

        }

        private void ClaimIndentities(string username, bool isPersistent)
        {
            var claims = new List<Claim>();

            try
            {
                claims.Add(new Claim(ClaimTypes.Name, username));
                var claimIdenties = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

       
        private ActionResult RedirectToLocal(string returnURL)
        {
            try
            {
                if (Url.IsLocalUrl(returnURL))
                {
                    return this.Redirect(returnURL);
                }
            }
            catch (Exception ex)
            {

                throw;
            }

            return this.RedirectToAction("Logout", "Login");
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Logout()
        {
            var ctx = Request.GetOwinContext();
            var authenticationManager = ctx.Authentication;
            authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Login", "Login");
        }

    }
}