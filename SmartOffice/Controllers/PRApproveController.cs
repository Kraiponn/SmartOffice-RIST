using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartOffice.EReservation.IResponsitory;
using SmartOffice.eReservation.ModelsDocControl;
using SmartOffice.PRApprove;
using SmartOffice.Class;
using Microsoft.Extensions.Configuration;
using SmartOffice.ModelsPRApprove;
using System.Security.Claims;
using SmartOffice.Models;

namespace SmartOffice.Controllers
{
    public class PRApproveController : Controller
    {
        private readonly PR_APPROVEContext _APPROVEContext;
        private readonly IConfiguration _configuration;

        public PRApproveController(PR_APPROVEContext aPPROVEContext, IConfiguration configuration)
        {
            _APPROVEContext = aPPROVEContext;
            this._configuration = configuration;
        }

        public IActionResult PR()
        {

            //ConnDoc dp = new ConnDoc(_configuration);
            //var result = dp.GetSumPR();
            var u = User.Identity;
            if (User.Identity is ClaimsIdentity identity)
            {

                //if (User.Identity.IsAuthenticated)
                //{
                //    identity.RemoveClaim(identity.FindFirst("GroupCategclick"));
                    
                //}
                if (User.Identity.IsAuthenticated == false)
                {
                    identity.AddClaim(new Claim("GroupCategclick", "CMMUSER000"));
                }
            }
            return View();
        }
        public IActionResult PRDashboard()
        {
            ConnDoc dp = new ConnDoc(_configuration);
            var result = dp.GetSumPR();
            var u = User.Identity;
            if (User.Identity is ClaimsIdentity identity)
            {

                if (User.Identity.IsAuthenticated == false)
                {
                    identity.AddClaim(new Claim("GroupCategclick", "CMMUSER000"));
                }
            }
            return View();
        }
            public JsonResult GetPR(string mode)
        {
            ConnDoc dp = new ConnDoc(_configuration);

            var result = dp.Getprdata(mode).ToList();

            return Json(new { data = result });
        }

        public JsonResult GetSumPR()
        {
            ConnDoc dp = new ConnDoc(_configuration);

            PRApporveModel result = dp.GetSumPR();




            return Json(new { data = result });
        }
    }
}