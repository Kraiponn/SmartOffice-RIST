using Microsoft.AspNetCore.Mvc;
using SmartOffice.Models;
using SmartOffice.ModelsEsmartOffice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartOffice.Views.Components.MenuMainControl
{
    public class MenuMainControlViewComponent : ViewComponent
    {
       
        private readonly ESmartOfficeContext _dbContext;

        public MenuMainControlViewComponent(ESmartOfficeContext dbContext) {
           
            _dbContext = dbContext;
        }
         public async Task<IViewComponentResult> InvokeAsync(string DeptType)
        {
            var model = _dbContext.AspNetGroup.Where(i=>i.GroupCateg != "0" ).ToList();
            return await Task.FromResult((IViewComponentResult)View("Default", model));
        } 
       
    }
}
