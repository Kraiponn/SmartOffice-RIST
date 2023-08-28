using Microsoft.AspNetCore.Mvc;
using SmartOffice.Models;
using SmartOffice.ModelsEsmartOffice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartOffice.Views.Components.PicslideControl
{
    public class PicslideControlViewComponent : ViewComponent
    {
       
        private readonly ESmartOfficeContext _dbContext;

        public PicslideControlViewComponent(ESmartOfficeContext dbContext) {
           
            _dbContext = dbContext;
        }
         public async Task<IViewComponentResult> InvokeAsync(string GroupCateg)
        {

            var model = new ImgSlideSetupmaster();
            var partid = _dbContext.ControlPart.Where(x => x.PartId == 6).Select(x=>x.PartId).FirstOrDefault();
            model.imgHeaders = _dbContext.ImgHeader.Where(x => x.Disable == false && x.ImgType != "LEFT" && x.GroupCateg == GroupCateg && x.PartId == partid && (x.StartDate <= DateTime.Now.Date && x.EndDate >= DateTime.Now.Date)).ToList();
            model.imgTextHeaders = _dbContext.ImgTextHeader.Where(x => x.GroupCateg == GroupCateg && x.PartId == partid).ToList();
            

            
            return await Task.FromResult((IViewComponentResult)View("Default",model));
        } 
       
    }
}
