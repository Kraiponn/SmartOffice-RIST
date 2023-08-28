using Microsoft.AspNetCore.Mvc;
using SmartOffice.Models;
using SmartOffice.ModelsDocControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SmartOffice.ModelsEsmartOffice;
using SmartOffice.ModelsForm;

namespace SmartOffice.Views.Components.MenuMainControl
{
    public class MenuCMMHeaderControlViewComponent : ViewComponent
    {
       
        private readonly ESmartOfficeContext _dbContext;

        public MenuCMMHeaderControlViewComponent(ESmartOfficeContext dbContext) {
           
            _dbContext = dbContext;
        }
         public async Task<IViewComponentResult> InvokeAsync()
        {
           
            var model = new ModelMenuCMMHeader();

            List<MenuCMMHeader> menuCMMHeaders = await _dbContext.Set<MenuCMMHeader>().FromSql("exec sprMenuCMMHeader").ToListAsync();
            model.menuCMMHeaders = menuCMMHeaders;

            return await Task.FromResult((IViewComponentResult)View("Default", model));
        } 
       
    }
}
