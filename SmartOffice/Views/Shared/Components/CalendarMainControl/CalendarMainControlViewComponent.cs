using Microsoft.AspNetCore.Mvc;
using SmartOffice.Models;
using SmartOffice.ModelsEsmartOffice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartOffice.Views.Components.PicslideControl
{
    public class CalendarMainControlViewComponent : ViewComponent
    {       
        private readonly ESmartOfficeContext _dbContext;
        public CalendarMainControlViewComponent(ESmartOfficeContext dbContext) {           
            _dbContext = dbContext;
        }
         public async Task<IViewComponentResult> InvokeAsync()
        {                                 
            return await Task.FromResult((IViewComponentResult)View("Default"));
        } 
       
    }
}
