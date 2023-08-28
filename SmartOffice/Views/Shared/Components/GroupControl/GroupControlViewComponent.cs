using Microsoft.AspNetCore.Mvc;
using SmartOffice.Models;
using SmartOffice.ModelsEsmartOffice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartOffice.Views.Components.GroupControl
{
    public class GroupControlViewComponent : ViewComponent
    {
       
        private readonly ESmartOfficeContext _dbContext;

        public GroupControlViewComponent(ESmartOfficeContext dbContext) {
           
            _dbContext = dbContext;
        }
         public async Task<IViewComponentResult> InvokeAsync(string DeptType)
        {


            var model = (from p in _dbContext.AspNetGroup
                         join e in _dbContext.Division
             on new { p.DivisionCode }
             equals new { e.DivisionCode }
                         where p.Disable == true
                         select new GroupDiv
                         {
                             DivisionCode = p.DivisionCode,
                             DivisionName = e.DivisionName,
                             GroupCateg = p.GroupCateg,
                             GroupName = p.GroupName,
                             OrderNo = Convert.ToInt32(p.OrderNo),
                             PathFile = p.ImagePath == null ? "" : "~/image/" + p.ImagePath,
                             OrderNodiv = Convert.ToInt32(e.OrderNo),

                         });
                        
                        
                        
             List<GroupDiv> returndata = model.ToList();
           


            return await Task.FromResult((IViewComponentResult)View("Default", returndata));


        } 
       
    }
}
