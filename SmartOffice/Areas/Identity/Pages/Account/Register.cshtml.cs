using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;
using Microsoft.Extensions.Logging;
using SmartOffice.ModelsForm;
using SmartOffice.Models;
using SmartOffice.ModelsEsmartOffice;
using SmartOffice.ModelsDocControl;
using SmartOffice.ModelsHRMSLocal;
using System.Drawing;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace SmartOffice.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly ESmartOfficeContext _context;
        private readonly DocumentControlContext _DocumentContext;
        private readonly HRMSLocalContext _HRMSLocalContext;
        private IHostingEnvironment _hostingEnvironment;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            ESmartOfficeContext context,
            DocumentControlContext DocumentControlContext,
            HRMSLocalContext HRMSLocalContext, IHostingEnvironment hostingEnvironment
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _context = context;
            _DocumentContext = DocumentControlContext;
            _HRMSLocalContext = HRMSLocalContext;
            _hostingEnvironment = hostingEnvironment;
        }

        private DynamicViewData _viewBag;

        public dynamic ViewBag
        {
            get
            {
                if (_viewBag == null)
                {
                    _viewBag = new DynamicViewData(() => ViewData);
                }
                return _viewBag;
            }
        }

        [BindProperty]
        public InputModel Input { get; set; }

        [BindProperty]
        public List<CheckList> Inputchecklist { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            public InputModel()
            {
                //GroupCategl = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
                //ApplicationRoleIdl = new List<SelectListItem>();

            }

            [Required]
            [Display(Name = "UserID")]
            public string Email { get; set; }

            //[Required]
            //[EmailAddress]
            //[Display(Name = "Email")]
            //public string Emaill { get; set; }

            [Required]
            [Phone]
            [Display(Name = "Internal Telephone")]
            public string Tel { get; set; }

            //[Required]
            //[Display(Name = "Group")]
            //public string GroupCateg { get; set; }
            //public List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> GroupCategl { get; set; }

            //[Required]           
            //[Display(Name = "Role")]
            //public string ApplicationRoleId { get; set; }
            //public List<SelectListItem> ApplicationRoleIdl { get; set; }


            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }
        public class CheckList
        {
            public string Id { get; set; }
            public string Name { get; set; }
            //[Range(typeof(bool), "true", "true", ErrorMessage = "You gotta tick the box!")]
            public bool Checked { get; set; }
        }

        //public void dropdown()
        //{
        //    Input = new InputModel
        //    {
        //        GroupCategl = _context.AspNetGroup.Where(m => m.Disable == true && m.DivisionCode != null).Select(m => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = m.GroupCateg, Text = m.GroupName }).ToList(),

        //    };
        //}
        //public void checklist()
        //{
        //    var role = _context.AspNetRoles.Where(m => m.Name.ToUpper() != "DEFAULT").Select(m => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = m.Id, Text = m.Name }).ToList();
        //    Inputchecklist = new List<CheckList>();

        //    foreach (var item in role)
        //    {
        //        Inputchecklist.Add(new CheckList() { Id = item.Value, Name = item.Text, Checked = false });
        //    }
        //}

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            //dropdown();
            //checklist();
        }


        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                //bool checkrole = false;
                //foreach (var item in Inputchecklist)
                //{
                //    if(item.Checked == true)
                //    {
                //        checkrole = true;
                //        break;
                //    }
                //}

                var checkuserid = _HRMSLocalContext.HrmsEmployee.Where(x => x.Codempid == Input.Email).FirstOrDefault();
                //if (checkuserid.Count() > 0 && checkrole == true)
                if (checkuserid != null)
                {
                    var checkgroup = _context.AspNetGroup.Where(x => x.Disable == true  &&  x.GroupName == checkuserid.Department2 && x.GroupCateg.Contains("SEC-")).FirstOrDefault();
                    var GroupCateg = "";
                    if(checkgroup != null)
                    {
                        GroupCateg = checkgroup.GroupCateg;
                    }
                    else
                    {
                        GroupCateg = "CMMUSER000";
                    }
                    var user = new ApplicationUser
                    {
                        UserName = Input.Email,
                        NormalizedUserName = Input.Email.ToUpper(),
                        //Email = Input.Emaill,
                        //EmailConfirmed = false,
                        //NormalizedEmail = Input.Emaill.ToUpper(),
                        PhoneNumber = Input.Tel,
                        PhoneNumberConfirmed = true,
                        //GroupCateg = Input.GroupCateg,
                        GroupCateg = GroupCateg,
                        CreateBy = Input.Email,
                        UpdateBy = Input.Email,
                    };

                    var result = await _userManager.CreateAsync(user, Input.Password);
                    if (result.Succeeded)
                    {
                        //Add role user
                        //foreach (var item in Inputchecklist)
                        //{
                        //    if (item.Checked == true)
                        //    {
                        var recordnew = new AspNetUserRoles
                        {
                            //RoleId = item.Id,
                            RoleId = "0000000003", //row public
                            UserId = user.Id,
                            CreateBy = Input.Email,
                            UpdateBy = Input.Email,
                        };

                        _context.AspNetUserRoles.Add(recordnew);
                        _context.SaveChanges();
                        

                        //    }
                        //}
                        await LoginAsync();

                        //_logger.LogInformation("User created a new account with password.");

                        //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        //var callbackUrl = Url.Page(
                        //    "/Account/ConfirmEmail",
                        //    pageHandler: null,
                        //    values: new { userId = user.Id, code = code },
                        //    protocol: Request.Scheme);

                        //await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        //    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                        //await _signInManager.SignInAsync(user, isPersistent: false);
                        //return LocalRedirect(returnUrl);

                        string webRootPath = _hostingEnvironment.WebRootPath + "\\image\\User\\";
                        string fullPath = Path.Combine(webRootPath, "userdefault.jpg");
                        string newFileName = Input.Email;
                        FileInfo f1 = new FileInfo(fullPath);
                        if (f1.Exists)
                        {
                            if (!System.IO.File.Exists(webRootPath + newFileName + f1.Extension))
                            {
                                f1.CopyTo(string.Format("{0}{1}{2}", webRootPath, newFileName, f1.Extension));
                            }

                        }


                        var url = "~/Identity/Account/Login";
                        return Redirect(url);
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "The Username is required.");
                }
            }

            // If we got this far, something failed, redisplay form
            //dropdown();
            //checklist();
            return Page();
        }

        private async Task LoginAsync()
        {
            
            var userData = _context.AspNetUsers.Where(u => u.UserName == Input.Email).SingleOrDefault();
            if (userData != null)
            {                
                userData.LastChangePassword = DateTime.Now;
                _context.Update(userData);
                _context.SaveChanges();
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
            var AspNetUsers = _context.AspNetUsers.Where(i => i.UserName == Input.Email).FirstOrDefault();


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
        }
    }
}
