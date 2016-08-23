using Microsoft.AspNet.Identity;
using MyIdentity.Web.Controllers.CustomController;
using MyIdentity.Web.Models;
using MyIdentity.Web.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MyIdentity.Web.Controllers
{
    [Authorize]
    public class AccountController : AppController
    {
        private readonly ApplicationUserManager _userManager;
        public AccountController(ApplicationUserManager userManager)
        {
            _userManager = userManager;
        }

        #region Private Methods
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private UserViewModel getUserModel(IdentityUser user)
        {
            //Regex emailRegex = new Regex(@"^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            //emailRegex.IsMatch
            if (user == null)
                return null;

            var u = new UserViewModel();
            populateUser(u, user);
            return u;
        }

        private void populateUser(UserViewModel u, IdentityUser user)
        {
            u.Id = user.Id;
            u.UserName = user.UserName;
            u.FirstName = user.FirstName;
            u.LastName = user.LastName;
            u.Email = user.Email;
            u.PhoneNumber = user.PhoneNumber;
            u.DateOfBirth = user.DateOfBirth;
        }

        private IdentityUser getIdentityUser(UserViewModel user)
        {
            var u = new IdentityUser();
            populateIdentityUser(u, user);
            return u;
        }

        private void populateIdentityUser(IdentityUser u, UserViewModel user)
        {
            u.Id = user.Id;
            u.UserName = user.UserName;
            u.FirstName = user.FirstName;
            u.LastName = user.LastName;
            u.Email = user.Email;
            u.PhoneNumber = user.PhoneNumber;
            u.DateOfBirth = user.DateOfBirth;
        }        
        #endregion

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    Address = model.Address,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber.ToString(),
                    DateOfBirth = model.DateOfBirth
                };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    //Redirect user to home page
                    var userIdentity = _userManager.CreateIdentity(user, "ApplicationCookie");
                    var ctx = Request.GetOwinContext();
                    ctx.Authentication.SignIn(userIdentity);
                    return RedirectToAction("Index", "Home");
                }
                AddErrors(result);

            }
            return View(model);
        }

        [AllowAnonymous]        
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.UserName);

                if (user != null)
                {
                    if(_userManager.CheckPassword(user, model.Password))
                    {
                        var identity = _userManager.CreateIdentity(user, "ApplicationCookie");
                        var ctx = Request.GetOwinContext();
                        ctx.Authentication.SignIn(identity);
                        return RedirectToAction("Index", "Home");
                    }
                }
                ModelState.AddModelError("", "username or password is incorrect");
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            var ctx = Request.GetOwinContext();
            ctx.Authentication.SignOut("ApplicationCookie");
            return RedirectToAction("Login", "Account");            
        }

        public ActionResult Settings()
        {
            var user = _userManager.FindByName(CurrentUser.UserName);
            return View(user);
        }
        
        public ActionResult Edit(string id)
        {
            var user = _userManager.FindById(id);

            if (user == null)
            {
                return HttpNotFound();
            }            
            
            return View(getUserModel(user));
        }        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserViewModel model)
        {
            var user = _userManager.FindById(model.Id);

            if (user == null)
            {
                return HttpNotFound();
            }

            populateIdentityUser(user, model);

            var result = _userManager.Update(user);

            if (result.Succeeded)
            {
                return RedirectToAction("Settings");
            }
            AddErrors(result);

            return View(getUserModel(user));
        }

        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            var result = await _userManager.ChangePasswordAsync(User.Identity.GetUserId(), model.Password, model.ConfirmPassword);

            if (result.Succeeded)
            {
                return RedirectToAction("Settings");
            }
            AddErrors(result);
            return View(model);
        }
        public ActionResult Delete(string id)
        {
            var user = _userManager.FindById(id);

            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);            
        }

        [HttpPost]
        public async Task<ActionResult> Delete(IdentityUser model)
        {
            var user = _userManager.FindById(model.Id);
            
            if (user == null)
            {
                return HttpNotFound();
            }

            var result = await _userManager.DeleteAsync(user);

            if(result.Succeeded)
            {
                var ctx = Request.GetOwinContext();
                ctx.Authentication.SignOut("ApplicationCookie");
                return RedirectToAction("Login", "Account");
            }
            return View(model);            
        }
    }
}