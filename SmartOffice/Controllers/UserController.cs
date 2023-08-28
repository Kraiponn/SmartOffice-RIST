using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using SmartOffice.Models;
using SmartOffice.Models.ViewModel;
using SmartOffice.ModelsEsmartOffice;
using SmartOffice.ModelsHRMSLocal;
using Microsoft.EntityFrameworkCore;
namespace SmartOffice.Controllers
{
    //[Authorize(Roles="Admin")]
    public class UserController : Controller
    {
        private readonly ESmartOfficeContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<UserController> _logger;
        private readonly HRMSLocalContext _HRdbcontext;
        private readonly ESmartOfficeContext _ESmartcontext;

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required]
            [Display(Name = "Username")]
            public string Email { get; set; }

            [Required]
            [Display(Name = "Divivsion")]
            public string Division { get; set; }

            [Required]
            [Display(Name = "Department")]
            public string Department { get; set; }

            public List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> ApplicationRoles { get; set; }


            [Display(Name = "Role")]
            public string[] ApplicationRoleId { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }



            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }
        public UserController(ESmartOfficeContext context,UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager,
            ILogger<UserController> logger, HRMSLocalContext HRdbcontext, ESmartOfficeContext ESmartcontext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _context = context;
            _HRdbcontext = HRdbcontext;
            _ESmartcontext = ESmartcontext;

        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult UserManage()
        {
            return View();
        }
        public IActionResult AddUser()
        {
            var vm = new InputModel();
           vm.ApplicationRoles = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
           var lm = _context.AspNetRoles.ToList();
            foreach (var temp in lm)
            {
                vm.ApplicationRoles.Add(new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem() { Text = temp.Name, Value = temp.Name });
            }
            return PartialView("_AddUser", vm);
        }

      
        //Add new User
        public async Task<JsonResult> NewUser(InputModel Input)
        {
            var Data = new { status = true, subject = "Create User", detail = "Create user is complete." };
            try
            {
                var user = new ApplicationUser
                {
                    UserName = Input.Email,
                    Email = Input.Email
                    //Division = Input.Division,
                    //Department = Input.Department

                };

                var result = await _userManager.CreateAsync(user, Input.Password);
                var result1 = await _userManager.AddToRolesAsync(user, Input.ApplicationRoleId);
                if (!result.Succeeded && !result1.Succeeded)
                {
                    Data = new { status = true, subject = "Create User", detail = result.ToString() };
                    return Json(Data);
                }
            }
            catch (Exception ex)
            {
                Data = new { status = false, subject = "Create User", detail = ex.Message.ToString() };
                return Json(Data);
            }
           
            _logger.LogInformation("User created a new account with password.");           
            return Json(Data); 
           
        }

        [HttpPost]
        public async Task<JsonResult> DeleteUser(string ID)
        {
            var Iuser = await _userManager.FindByIdAsync(ID);
            var result = await _userManager.DeleteAsync(Iuser);
            if (result.Succeeded)
            {
               var Data = new { status = true, subject = "Delete User", detail = "Delete user "+ Iuser.UserName +"Complete." };
                return Json(Data);
            }
            else
            {
                var Data = new { status = false, subject = "Delete User", detail = "System is can't delete user." };
                return Json(Data);
            }
            //return new JsonResult { Data = new { status = true, subject = "Delete Model", detail = "Delete Model  : " + delete_model.model_name } };

        }
        //Load all user to jquery datatable
        [AllowAnonymous]
        public JsonResult GetUserAsync()
        {
            try
            {
                //var revisions = _context.AspNetUsers.ToList().Select(x => new {
                //   userid_ = x.Id ,
                //   username_ = x.UserName,
                //   email_ = x.Email,
                //   dept = x.Department,
                //   div = x.Division,
                //   userrole = x..,
                //});

                var usersWithRoles = (from user in _context.AspNetUsers
                                      select new
                                      {
                                          UserId = user.Id,
                                          Username = user.UserName,
                                          user.Email,
                                          RoleNames = (from userRole in _context.AspNetUserRoles
                                                       join role in _context.AspNetRoles on userRole.RoleId
                                                       equals role.Id
                                                       where userRole.UserId == user.Id
                                                       select role.Name).ToList()
                                      }).ToList().Select(p => new Users_in_Role_ViewModel()
                                      {
                                          userid_ = p.UserId,
                                          username_ = p.Username,
                                          email_ = p.Email,
                                          role_ = string.Join(",", p.RoleNames)
                                      });
                return Json(new { data = usersWithRoles });
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }
        //Load all user to jquery datatable
        [AllowAnonymous]
        public async Task<JsonResult> GetListUser(string Searchtext)
        {
            if(Searchtext.Length < 6)
                return null;

            var list_emp = await _HRdbcontext.HrmsEmployee.Where(i=>i.Inactive == null).ToListAsync();         
            var listemail = (from h in list_emp select new
            {
                id = h.Codempid,
                text = h.Namempe + " " + h.Codempid
            }).OrderBy(i=>i.text);
            if(Searchtext == "" || Searchtext ==null)
                return Json(listemail);

            var fillter = listemail.Where(i => i.text.Contains(Searchtext.ToUpper()));
            return Json(fillter);
        }
        //Load all user to jquery datatable
        [AllowAnonymous]
        public async Task<JsonResult> GetListEmp(string Prefix)
        {
            if (Prefix == "Mr." || Prefix == "Mrs.")
                return null;

            var list_emp = await _HRdbcontext.HrmsEmployee.Where(i=>i.Inactive == null).ToListAsync();          
            var list_auto = (from h in list_emp                            
                             select new
                             {
                                 id = h.Codempid,
                                 text = h.Namempe
                             });
            //Searching records from list using LINQ query  
            var NameList = (from N in list_auto
                            where N.text.ToLower().Contains(Prefix.ToLower())
                            select new { N.text });
            return Json(NameList);
        }



        [AllowAnonymous]
        public async Task<JsonResult> GetListDiv(string Prefix)
        {
            if (Prefix == " ")
                return null;

            var listitems = await _HRdbcontext.HrmsEmployee.Where(i => i.Department.Contains(Prefix) && i.Inactive == null).Select(i => new {
                id = i.Department,
                text = i.Department
            }).Distinct().ToListAsync();

            return Json(listitems);
        }

        [AllowAnonymous]
        public async Task<JsonResult> GetListDept(string Prefix)
        {
            if (Prefix == " ")
                return null;

            var listitems = await _HRdbcontext.HrmsEmployee.Where(i => i.Department2.Contains(Prefix)&& i.Inactive == null).Select(i => new {
                id = i.Department2,
                text = i.Department2
            }).Distinct().ToListAsync();

            return Json(listitems);
        }
        [AllowAnonymous]
        public async Task<JsonResult> GetListSect(string Prefix)
        {
            if (Prefix == " ")
                return null;


            var listitems = await _HRdbcontext.HrmsEmployee.Where(i => i.Department3.Contains(Prefix)&& i.Inactive == null).Select(i => new {
                id = i.Department3,
                text = i.Department3
            } ).Distinct().ToListAsync(); 
            
            return Json(listitems);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
               _context.Dispose();
            }
            base.Dispose(disposing);
        }        
    }
}