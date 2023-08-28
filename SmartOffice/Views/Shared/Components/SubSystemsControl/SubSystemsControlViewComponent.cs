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
    public class SubSystemsControlViewComponent : ViewComponent
    {
       
        private readonly ESmartOfficeContext _dbContext;

        public SubSystemsControlViewComponent(ESmartOfficeContext dbContext) {
           
            _dbContext = dbContext;
        }
         public async Task<IViewComponentResult> InvokeAsync()
        {
           
            var model = new ModelSubSystems();

            List<SubSystems> subSystems = _dbContext.Set<SubSystems>().FromSql("exec sprSubSystems").AsNoTracking().ToList();
            model.subSystems = subSystems.Where(i => i.GroupSub == "SUB SYSTEMS").ToList();
       

            return await Task.FromResult((IViewComponentResult)View("Default", model));
        } 
       
    }
}
