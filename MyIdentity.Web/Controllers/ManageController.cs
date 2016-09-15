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
    public class ManageController : Controller
    {
        private readonly ApplicationUserManager _userManager;
        private readonly ApplicationRoleManager _roleManager;

        #region Private Methods
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
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

        private static RoleViewModel getRole(IdentityRole role)
        {
            if (role == null)
            {
                return null;
            }
            var r = new RoleViewModel();
            populateRole(r, role);
            return r;
        }
        
        private static IdentityRole getIdentityRole(RoleViewModel role)
        {
            if (role == null)
            {
                return null;
            }
            var r = new IdentityRole();
            populateIdentityRole(r, role);
            return r;
        }       
       
        private static void populateRole(RoleViewModel r, IdentityRole role)
        {
            r.RoleID = role.Id;   
            r.Name = role.Name;
        }

        private static void populateIdentityRole(IdentityRole r, RoleViewModel role)
        {            
            r.Name = role.Name;
        }
        #endregion

        public ManageController(ApplicationUserManager userManager, ApplicationRoleManager roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        
        
        public ActionResult Index()
        {
            return View();
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

        public ActionResult UserDetails(string userID)
        {
            if (string.IsNullOrWhiteSpace(userID))
            {
                return HttpNotFound();
            }
            var user = _userManager.FindById(userID);
            return View(getUser(user));
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
        
        public ActionResult RoleList()
        {
            var userRoles = _roleManager.Roles.ToList();
            return View(userRoles);
        }
        
        public ActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateRole(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _roleManager.CreateAsync(getIdentityRole(model));

                if (result.Succeeded)
                {
                    return RedirectToAction("RoleList");
                }
                AddErrors(result);
            }
            return View(model);
        }

        public ActionResult EditRole(string roleID)
        {
            if (!string.IsNullOrWhiteSpace(roleID))
            {
                return HttpNotFound();
            }
            var role = _roleManager.FindById(roleID);

            return View(getRole(role));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditRole(RoleViewModel model)
        {
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
    }
}