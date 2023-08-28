using Microsoft.AspNetCore.Http;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SmartOffice.ModelsDocControl;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartOffice.IResponsitory;

namespace SmartOffice.Responsitory
{
    public class VisitorCounterMiddleware
    {
        private readonly RequestDelegate _requestDelegate;
        private readonly IHitControl _HitControl;
        public VisitorCounterMiddleware(RequestDelegate requestDelegate,IHitControl HitControl)
        {
            _requestDelegate = requestDelegate;
            _HitControl = HitControl;
        }

        public async Task Invoke(HttpContext context)
        {
            string visitorId = context.Request.Cookies["VisitorId"];
            //var visitorall = context.Request.Cookies["VisitorId"].Count();
            if (visitorId == null)
            {

                
                context.Response.Cookies.Append("VisitorId", Guid.NewGuid().ToString(), new CookieOptions()
                {
                    Path = "/",
                    HttpOnly = true,
                    Secure = false,
                });
                string ComputerName = context.Connection.RemoteIpAddress.ToString();
               await _HitControl.AddhitAsync(ComputerName);
                //return;
                //don the necessary staffs here to save the count by one
                //DateTime today = DateTime.Now.Date;
                //var v = _DocumentContext.HitCounter.Where(i=>i.Ipaddress.Equals(visitorId))
            }

            await _requestDelegate(context);
        }
    }
}
