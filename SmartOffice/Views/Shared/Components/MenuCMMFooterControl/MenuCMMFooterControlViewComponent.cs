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
    public class MenuCMMFooterControlViewComponent : ViewComponent
    {
       
        private readonly ESmartOfficeContext _dbContext;

        public MenuCMMFooterControlViewComponent(ESmartOfficeContext dbContext) {
           
            _dbContext = dbContext;
        }
         public async Task<IViewComponentResult> InvokeAsync()
        {
           
            var model = new ModelMenuCMMFooter();

            List<MenuCMMFooter> menuCMMFooters = _dbContext.Set<MenuCMMFooter>().FromSql("exec sprMenuCMMFooter").ToList();
            model.menuCMMFooters = menuCMMFooters;

            return await Task.FromResult((IViewComponentResult)View("Default", model));
        } 
       
    }
}
