using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartOffice.Models;
using SmartOffice.ModelsEsmartOffice;

namespace SmartOffice.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ResetPasswordWithEmailModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ESmartOfficeContext _ESmartOffice;
        public ResetPasswordWithEmailModel(UserManager<ApplicationUser> userManager, ESmartOfficeContext ESmartOffice)
        {
            _userManager = userManager;
             _ESmartOffice = ESmartOffice; 
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]        
            public string Username { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            public string Code { get; set; }
        }

        public IActionResult OnGet(string confirmationID = null)
        {
            ConfirmPasswordReset newpass = _ESmartOffice.ConfirmPasswordReset.Where(i => i.ConfirmId.Trim() == confirmationID.Trim() && i.ActiveStatus == true).FirstOrDefault();
            if (confirmationID == null || newpass == null)
            {
                return BadRequest("A code must be supplied for password reset.");
            }
            else
            {
                Input = new InputModel
                {
                    Code = confirmationID
                };
                return Page();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.FindByNameAsync(Input.Username);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToPage("./ResetPasswordConfirmation");
            }

            string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);    
            var result = await _userManager.ResetPasswordAsync(user, resetToken, Input.Password);
            if (result.Succeeded)
            {
                AspNetUsers userData = _ESmartOffice.AspNetUsers.Where(i => i.UserName.Trim() == Input.Username).FirstOrDefault();
                if (userData != null)
                {
                    userData.LastChangePassword = DateTime.Now;                    
                    await _ESmartOffice.SaveChangesAsync();
                }


                ConfirmPasswordReset newpass =  _ESmartOffice.ConfirmPasswordReset.Where(i=>i.ConfirmId.Trim() ==Input.Code.Trim()).FirstOrDefault();
                if (newpass!= null)
                {
                    newpass.ConfrimDate = DateTime.Now;
                    newpass.ActiveStatus = false;
                   await _ESmartOffice.SaveChangesAsync();
                }
                return RedirectToPage("./ResetPasswordConfirmation");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return Page();
        }
    }
}
