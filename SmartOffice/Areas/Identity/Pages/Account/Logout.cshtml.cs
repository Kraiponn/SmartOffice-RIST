using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using SmartOffice.Hubs;
using SmartOffice.IResponsitory;
using SmartOffice.Models;
using SmartOffice.ModelsEsmartOffice;

namespace SmartOffice.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<LogoutModel> _logger;
        private readonly IHubContext<NotiHub> _notificationUserHubContext;
        private readonly IUserConnectionManager _userConnectionManager;
        private readonly ESmartOfficeContext _context;
        public LogoutModel(SignInManager<ApplicationUser> signInManager, ILogger<LogoutModel> logger, IHubContext<NotiHub> notificationUserHubContext, IUserConnectionManager userConnectionManager, ESmartOfficeContext context)
        {
            _signInManager = signInManager;
            _logger = logger;
            _notificationUserHubContext = notificationUserHubContext;
            _userConnectionManager = userConnectionManager;
            _context = context;
        }

        public async Task<IActionResult> OnGet(string returnUrl = null)
        {

            // var connectionid = _userConnectionManager.GetUserConnections(User.Identity.Name).FirstOrDefault();
            //if(connectionid!=null)
            // {
            //     _userConnectionManager.RemoveUserConnection(connectionid);
            // }
            _logger.LogInformation(User.Identity.Name + " >> logged out.");
            RemoveClaim();
            await _signInManager.SignOutAsync();


            if (returnUrl != null)
            {
                return RedirectPermanent(returnUrl);
            }
            else
            {
                return Page();
            }


        }

        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            //var connectionid = _userConnectionManager.GetUserConnections(User.Identity.Name).FirstOrDefault();
            //if (connectionid != null)
            //{
                //_userConnectionManager.RemoveUserConnection(User.Identity.Name);
            //}
            _logger.LogInformation(User.Identity.Name + " >> logged out.");
            RemoveClaim();
            await _signInManager.SignOutAsync();
          
           
            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                return Page();
            }
        }
        private void RemoveClaim()
        {
            var user = User as ClaimsPrincipal;
            var identity = user.Identity as ClaimsIdentity;

            var userData = _context.AspNetUsers.Where(u => u.UserName == identity.Name).SingleOrDefault();
            if (userData != null)
            {
                userData.LastLogout = DateTime.Now;
                _context.Update(userData);
                _context.SaveChanges();
            }

            var claimNameList = identity.Claims.Select(x => x.Type).ToList();

            foreach (var name in claimNameList)
            {
                var claimA = identity.Claims.FirstOrDefault(x => x.Type == name);
                if (claimA != null)
                    identity.RemoveClaim(claimA);

            }           
        }
    }
}