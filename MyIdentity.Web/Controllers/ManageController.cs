using Microsoft.AspNet.Identity;
using MyIdentity.Web.Models;
using MyIdentity.Web.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MyIdentity.Web.Controllers
{
    [AllowAnonymous]
    public class ManageController : Controller
    {
        private readonly ApplicationUserManager _userManager;
        public ManageController(ApplicationUserManager userManager)
        {
            _userManager = userManager;
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
        public ActionResult UserList()
        {
            var users = _userManager.Users;
            return View(users);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(RegisterViewModel model)
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
                    return RedirectToAction("UserList", "Manage");
                }
                AddErrors(result);

            }
            return View(model);
        }

        public ActionResult Edit(string id)
        {
            var user = _userManager.FindById(id);

            if (user == null)
            {
                return HttpNotFound();
            }

            return View(UserViewModel.getUser(user));
        }
        
    }
}