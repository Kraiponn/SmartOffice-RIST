using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartOffice.Models;
using SmartOffice.ModelsEsmartOffice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartOffice.Views.Shared.Components.QuickLinkControl
{
    public class QuickLinkControlViewComponent: ViewComponent
    {
        private readonly ESmartOfficeContext _dbContext;
        public QuickLinkControlViewComponent(ESmartOfficeContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = new ModelSubSystems();

            List<SubSystems> subSystems = _dbContext.Set<SubSystems>().FromSql("exec sprQuicklinkMenu").AsNoTracking().ToList();
            model.subSystems = subSystems;
 
            return await Task.FromResult((IViewComponentResult)View("Default", model));
        }
    }
}

