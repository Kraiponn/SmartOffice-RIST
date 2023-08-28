using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartOffice.Data;
using SmartOffice.Models;
using SmartOffice.ModelsHRMSLocal;
using SmartOffice.ModelsEsmartOffice;
using SmartOffice.EmailCore.Models;
using SmartOffice.EmailCore.IResponsitory;
using System.Linq;

namespace SmartOffice.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ForgotPasswordModel : PageModel
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly HRMSLocalContext _HRMSLocal;
        private readonly ESmartOfficeContext _ESmartOffice;
        private readonly ISendEmail _ISendEmail;
        public ForgotPasswordModel(UserManager<ApplicationUser> userManager, HRMSLocalContext HRMSLocal, ESmartOfficeContext ESmartOffice, ISendEmail SendEmail)
        {
            _userManager = userManager;
            _HRMSLocal = HRMSLocal;
            _ESmartOffice = ESmartOffice;
            _ISendEmail = SendEmail;

        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [Display(Name = "UserID")]
            public string UserName { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {
           
            if (ModelState.IsValid)
            {
               

                long i = 1;
                foreach (byte b in Guid.NewGuid().ToByteArray())
                {
                    i *= ((int)b + 1);
                }
                var Keyconfirm = string.Format("{0:x}", i - DateTime.Now.Ticks);
                var username = Input.UserName.Trim();
                string ComputerName = Environment.MachineName;

                try
                {
                    var checkemail = await _HRMSLocal.HrmsEmployee.FindAsync(username);


                    if (checkemail != null)
                    {
                        if (checkemail.Email1 != null || checkemail.Email2 != null)
                        {
                            var PasswordResetdata = _ESmartOffice.ConfirmPasswordReset.Where(c => c.Username == username && c.ActiveStatus == true);
                            _ESmartOffice.ConfirmPasswordReset.RemoveRange(PasswordResetdata);
                            _ESmartOffice.SaveChanges();


                            ConfirmPasswordReset resetPassword = new ConfirmPasswordReset
                            {
                                Username = username,
                                ConfirmId = Keyconfirm,
                                ActiveStatus = true,
                                RequestDate = DateTime.Now,
                                ComputerName = ComputerName

                            };

                            _ESmartOffice.ConfirmPasswordReset.Add(resetPassword);
                            await _ESmartOffice.SaveChangesAsync();
                            var a = _ISendEmail.SendPasswordReset(Keyconfirm, username);
                            return RedirectToPage("./ForgotPasswordConfirmation");
                        }
                        else
                        {
                            return RedirectToPage("./ForgotPasswordConfirmationFail");
                        }
                        
                    }
                    else
                    {
                        return RedirectToPage("./ForgotPasswordConfirmationFail");
                    }
                   
                }
                catch
                {
                    return RedirectToPage("./ForgotPasswordConfirmationFail");
                }
                //String userId = "009254";      
                //var user = await _userManager.FindByNameAsync(userId);
                //if (user == null) { /**/ }

                //// compute the new hash string
                //var newPassword = _userManager.PasswordHasher.HashPassword(user, "111111");
                //user.PasswordHash = newPassword;
                //var res = await _userManager.UpdateAsync(user);

                //if (res.Succeeded) {/**/}
                //else { /**/}
            
            }

            return Page();
        }
    }
}
