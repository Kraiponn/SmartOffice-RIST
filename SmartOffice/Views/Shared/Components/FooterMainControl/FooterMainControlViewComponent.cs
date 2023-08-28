using Microsoft.AspNetCore.Mvc;
using SmartOffice.Models;
using SmartOffice.ModelsEsmartOffice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartOffice.Views.Components.FooterMainControl
{
    public class FooterMainControlViewComponent : ViewComponent
    {
       
        private readonly ESmartOfficeContext _dbContext;

        public FooterMainControlViewComponent(ESmartOfficeContext dbContext) {
           
            _dbContext = dbContext;
        }
         public async Task<IViewComponentResult> InvokeAsync(string DeptType)
        {
            var model = _dbContext.AspNetGroup.ToList();
            return await Task.FromResult((IViewComponentResult)View("Default", model));
        } 
       
    }
}
