using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartOffice.Models;
using SmartOffice.ModelsEsmartOffice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartOffice.Views.Shared.Components.FavoriteMenuControl
{
    public class FavoriteMenuControlViewComponent : ViewComponent
    {
        private readonly ESmartOfficeContext _dbContext;
        public FavoriteMenuControlViewComponent(ESmartOfficeContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = new ModelSubSystems();
            List<SubSystems> subSystems = _dbContext.Set<SubSystems>().FromSql("exec sprFavoriteMenu").ToList();
            model.subSystems = subSystems;

            return await Task.FromResult((IViewComponentResult)View("Default", model));
        }
    }
}
