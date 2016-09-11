using Microsoft.AspNet.Identity;
using MyIdentity.Domain.Entities;
using MyIdentity.Web.Models;
using MyIdentity.Web.Models.CustomAttributes;
using MyIdentity.Web.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MyIdentity.Web.Controllers
{
    [AllowAnonymous]
    public class ManageController : Controller
    {
        private readonly ApplicationUserManager _userManager;
        private readonly ApplicationRoleManager _roleManager;

        #region Private Methods
        public static UserViewModel getUser(IdentityUser user)
        {
            if (user == null)
                return null;

            var u = new UserViewModel();
            populateUser(u, user);
            return u;
        }
        private static void populateUser(UserViewModel u, IdentityUser user)
        {
            u.Id = user.Id;
            u.UserName = user.UserName;
            u.FirstName = user.FirstName;
            u.LastName = user.LastName;
            u.Email = user.Email;
            u.Address = user.Address;
            u.PhoneNumber = user.PhoneNumber;
            u.DateOfBirth = user.DateOfBirth;
        }
        public static IdentityUser getIdentityUser(UserViewModel user)
        {
            if (user == null)
                return null;

            var u = new IdentityUser();
            populateIdentityUser(u, user);
            return u;
        }
        private static void populateIdentityUser(IdentityUser identityUser, UserViewModel user)
        {
            identityUser.Id = user.Id;
            identityUser.UserName = user.UserName;
            identityUser.FirstName = user.FirstName;
            identityUser.LastName = user.LastName;
            identityUser.Email = user.Email;
            identityUser.PhoneNumber = user.PhoneNumber;
            identityUser.DateOfBirth = user.DateOfBirth;
        }
        #endregion

        public ManageController(ApplicationUserManager userManager, ApplicationRoleManager roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        [AuthActivity(_roleManager, AccessLevel = "ReadUserList")]
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

            return View(getUser(user));
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserViewModel model)
        {
            if (ModelState.IsValid)
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
                    return RedirectToAction("UserList");
                }
                AddErrors(result);
                return View(getUser(user));
            }            
            return View(model);
        }
        
        public ActionResult RoleList(string userID)
        {
            if (string.IsNullOrEmpty(userID))
            {
                return HttpNotFound();
            }
            var userRoles = _userManager.GetRoles(userID);
            return View();
        }

        public async Task<ActionResult> ClaimList(string userID)
        {            
            var claims = await _userManager.GetClaimsAsync(userID);

            List<UserClaim> userClaims = new List<UserClaim>();

            foreach (var item in claims)
            {
                userClaims.Add(new UserClaim
                {
                    ClaimType = item.Type,
                    ClaimValue = item.Value
                });
            }
            return View(userClaims);
        }

        public async Task<ActionResult> AddClaim()
        {
            return View();
        }
    }
}