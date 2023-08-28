using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using SmartOffice.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Net;
using SmartOffice.ModelsEsmartOffice;
using SmartOffice.ModelsHRMSLocal;
using SmartOffice.ModelsDocControl;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace SmartOffice.Areas.Identity.Pages.Account
{

    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly ESmartOfficeContext _SmartOfficeContext;
        private readonly HRMSLocalContext _HRMSLocalContext;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly DocumentControlContext _DocumentContext;
        private IConfiguration _configuration;
        
        public LoginModel(SignInManager<ApplicationUser> signInManager, ILogger<LoginModel> logger, ESmartOfficeContext SmartOfficeContext,
            HRMSLocalContext HRMSLocalContext, UserManager<ApplicationUser> UserManager, DocumentControlContext DocumentControlContext,
            IConfiguration configuration)
        {
            _HRMSLocalContext = HRMSLocalContext;
            _SmartOfficeContext = SmartOfficeContext;
            _signInManager = signInManager;
            _logger = logger;
            _userManager = UserManager;
            _DocumentContext = DocumentControlContext;
            //returnUrl = returnUrl ?? Url.Content("~/");
            _configuration = configuration;           
        }

        [BindProperty]
        public InputModel Input { get; set; }

        //public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required]
            [Display(Name = "UserID")]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }

        public void OnGet(string returnUrl = null)
        {

            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl = returnUrl ?? Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            //await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);


            ReturnUrl = returnUrl;


        }
        
        
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, true, lockoutOnFailure: false);
                if (result.Succeeded)
                {                                                
                    int DayChangePassword = _configuration.GetValue<int>("AppSettings:DayChangePassword");

                    var AspNetUsers = _SmartOfficeContext.AspNetUsers.Where(j => j.UserName == Input.Email).FirstOrDefault();
                    var cc = Convert.ToDateTime( AspNetUsers.LastChangePassword).AddDays(DayChangePassword);
                    if (cc < DateTime.Now)
                    {
                        var PasswordResetdata = _SmartOfficeContext.ConfirmPasswordReset.Where(c => c.Username == Input.Email && c.ActiveStatus == true);
                        _SmartOfficeContext.ConfirmPasswordReset.RemoveRange(PasswordResetdata);
                        _SmartOfficeContext.SaveChanges();


                        long i = 1;
                        foreach (byte b in Guid.NewGuid().ToByteArray())
                        {
                            i *= ((int)b + 1);
                        }
                        var Keyconfirm = string.Format("{0:x}", i - DateTime.Now.Ticks);

                        ConfirmPasswordReset resetPassword = new ConfirmPasswordReset
                        {
                            Username = Input.Email,
                            ConfirmId = Keyconfirm,
                            ActiveStatus = true,
                            RequestDate = DateTime.Now,
                            ComputerName = Environment.MachineName
                        };
                        _SmartOfficeContext.ConfirmPasswordReset.Add(resetPassword);
                        await _SmartOfficeContext.SaveChangesAsync();


                        await _signInManager.SignOutAsync();
                        return RedirectToPage("./ResetPasswordWithEmail", new { confirmationID = Keyconfirm });
                    }

                    await LoginAsync();

                    if (returnUrl != null)
                    {
                        if (returnUrl.StartsWith("http"))
                        {
                            return RedirectPermanent(returnUrl);
                        }
                        else
                        {
                            return LocalRedirect(returnUrl);
                        }
                    }

                    return RedirectToAction("Privacy", "Home");
                }
                if (result.RequiresTwoFactor)
                {
                    await LoginAsync();
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return Page();
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();






        }

        private async Task LoginAsync()
        {

            var userData = _SmartOfficeContext.AspNetUsers.Where(u => u.UserName == Input.Email).SingleOrDefault();
            if (userData != null)
            {
                userData.LastLogin = DateTime.Now;
                _SmartOfficeContext.Update(userData);
                _SmartOfficeContext.SaveChanges();
            }

            ApplicationUser applicationUser = await _userManager.FindByNameAsync(Input.Email);
            if (applicationUser != null)
            {
                var claims = await _userManager.GetClaimsAsync(applicationUser);
                List<Claim> userRemoveClaims = claims.ToList();
                foreach (Claim claim in userRemoveClaims)
                {
                    await _userManager.RemoveClaimAsync(applicationUser, claim);
                }
            }


            var HrmsEmployee = _HRMSLocalContext.HrmsEmployee.Where(i => i.Codempid == Input.Email).FirstOrDefault();
            var AspNetUsers = _SmartOfficeContext.AspNetUsers.Where(i => i.UserName == Input.Email).FirstOrDefault();

            await _userManager.AddClaimAsync(applicationUser, new Claim("Codempid", HrmsEmployee.Codempid == null ? "" : HrmsEmployee.Codempid.ToString()));
            await _userManager.AddClaimAsync(applicationUser, new Claim("Company", HrmsEmployee.Company == null ? "" : HrmsEmployee.Company.ToString()));
            await _userManager.AddClaimAsync(applicationUser, new Claim("Department", HrmsEmployee.Department == null ? "" : HrmsEmployee.Department.ToString()));
            await _userManager.AddClaimAsync(applicationUser, new Claim("Department2", HrmsEmployee.Department2 == null ? "" : HrmsEmployee.Department2.ToString()));
            await _userManager.AddClaimAsync(applicationUser, new Claim("Department3", HrmsEmployee.Department3 == null ? "" : HrmsEmployee.Department3.ToString()));
            await _userManager.AddClaimAsync(applicationUser, new Claim("Division", HrmsEmployee.Division == null ? "" : HrmsEmployee.Division.ToString()));
            await _userManager.AddClaimAsync(applicationUser, new Claim("Dtebirth", HrmsEmployee.Dtebirth == null ? "" : Convert.ToDateTime(HrmsEmployee.Dtebirth).ToString("dd/MM/yyyy")));
            await _userManager.AddClaimAsync(applicationUser, new Claim("Email1", HrmsEmployee.Email1 == null ? "" : HrmsEmployee.Email1.ToString()));
            await _userManager.AddClaimAsync(applicationUser, new Claim("Emplevel", HrmsEmployee.Emplevel == null ? "" : HrmsEmployee.Emplevel.ToString()));
            await _userManager.AddClaimAsync(applicationUser, new Claim("Namempe", HrmsEmployee.Namempe == null ? "" : HrmsEmployee.Namempe.ToString()));
            await _userManager.AddClaimAsync(applicationUser, new Claim("Namempt", HrmsEmployee.Namempt == null ? "" : HrmsEmployee.Namempt.ToString()));
            await _userManager.AddClaimAsync(applicationUser, new Claim("Nationality", HrmsEmployee.Nationality == null ? "" : HrmsEmployee.Nationality.ToString()));
            await _userManager.AddClaimAsync(applicationUser, new Claim("Organizationname", HrmsEmployee.Organizationname == null ? "" : HrmsEmployee.Organizationname.ToString()));
            await _userManager.AddClaimAsync(applicationUser, new Claim("Passport", HrmsEmployee.Passport == null ? "" : HrmsEmployee.Passport.ToString()));
            await _userManager.AddClaimAsync(applicationUser, new Claim("Position", HrmsEmployee.Position == null ? "" : HrmsEmployee.Position.ToString()));
            await _userManager.AddClaimAsync(applicationUser, new Claim("Section", HrmsEmployee.Section == null ? "" : HrmsEmployee.Section.ToString()));
            await _userManager.AddClaimAsync(applicationUser, new Claim("Age", HrmsEmployee.Age == null ? "" : HrmsEmployee.Age.ToString()));

            await _userManager.AddClaimAsync(applicationUser, new Claim("Id", AspNetUsers.Id == null ? "" : AspNetUsers.Id.ToString()));
            await _userManager.AddClaimAsync(applicationUser, new Claim("UserName", AspNetUsers.UserName == null ? "" : AspNetUsers.UserName.ToString()));
            await _userManager.AddClaimAsync(applicationUser, new Claim("GroupCateg", AspNetUsers.GroupCateg == null ? "" : AspNetUsers.GroupCateg.ToString()));
            await _userManager.AddClaimAsync(applicationUser, new Claim("PhoneNumber", AspNetUsers.PhoneNumber == null ? "" : AspNetUsers.PhoneNumber.ToString()));
            await _userManager.AddClaimAsync(applicationUser, new Claim("Email2", HrmsEmployee.Email2 == null ? "" : HrmsEmployee.Email2.ToString()));
            await _userManager.AddClaimAsync(applicationUser, new Claim("Password", AspNetUsers.PasswordHash == null ? "" : AspNetUsers.PasswordHash.ToString()));
            await _userManager.AddClaimAsync(applicationUser, new Claim("GroupCategclick", ""));
            await _userManager.AddClaimAsync(applicationUser, new Claim("Bldg", AspNetUsers.Bldg == null ? "" : AspNetUsers.Bldg.ToString()));
            await _userManager.AddClaimAsync(applicationUser, new Claim("LastLogin", AspNetUsers.LastLogin == null ? "" : AspNetUsers.LastLogin.ToString()));
            await _userManager.AddClaimAsync(applicationUser, new Claim("LastChangePassword", AspNetUsers.LastChangePassword == null ? "" : AspNetUsers.LastChangePassword.ToString()));

            var GenderT = "";
            var GenderE = "";
            var Gender = HrmsEmployee.Namempt == null ? "" : HrmsEmployee.Namempt.ToString();
            if (Gender != "")
            {
                List<string> includedWords = new List<string>() { "น.ส.", "นางสาว", "นาง" };
                bool string_contains_words = includedWords.Exists(o => Gender.Contains(o));
                if (string_contains_words == true)
                {
                    GenderT = "หญิง";
                    GenderE = "Female";

                }
                else
                {
                    GenderT = "ชาย";
                    GenderE = "Male";
                }
            }

            await _userManager.AddClaimAsync(applicationUser, new Claim("GenderT", GenderT));
            await _userManager.AddClaimAsync(applicationUser, new Claim("GenderE", GenderE));
            _logger.LogInformation(applicationUser.UserName.ToString() + ">> logged in.");

        }
    }
}
