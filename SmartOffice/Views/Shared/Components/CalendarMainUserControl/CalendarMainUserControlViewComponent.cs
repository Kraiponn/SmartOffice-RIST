using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartOffice.Views.Shared.Components.CalendarMainUserControl
{
    public class CalendarMainUserControlViewComponent: ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(string _user)
        {
            ViewBag.user = _user;
            return await Task.FromResult((IViewComponentResult)View("Default"));
        }
    }
}
