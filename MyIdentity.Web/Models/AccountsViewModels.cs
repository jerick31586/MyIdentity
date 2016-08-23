using MyIdentity.Web.Models.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyIdentity.Web.Models
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} to {1} characters long", MinimumLength = 6)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }

        [Display(Name = "Date of Birth")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DateOfBirth { get; set; }
        
    }
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Username")]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }                
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class UserViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DateOfBirth { get; set; }   
        
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
            u.PhoneNumber = user.PhoneNumber;
            u.DateOfBirth = user.DateOfBirth;
        }        
    }
    public class ChangePasswordViewModel
    {
        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} to {1} characters long", MinimumLength = 6)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]        
        [Display(Name = "New Password")]
        public string ConfirmPassword { get; set; }
    }
}