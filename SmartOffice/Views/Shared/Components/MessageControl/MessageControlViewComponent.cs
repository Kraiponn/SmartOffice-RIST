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
using SmartOffice.eManagement.ModelsManagementControl;

namespace SmartOffice.Views.Components.MenuMainControl
{
    public class MessageControlViewComponent : ViewComponent
    {
       
        private readonly ManagementControlContext _dbContext;

        public MessageControlViewComponent(ManagementControlContext dbContext) {
           
            _dbContext = dbContext;
        }
         public async Task<IViewComponentResult> InvokeAsync(string ItemCateg, string ItemCode)
        {

            var model = _dbContext.InputItemListMessage.Where(x => x.ItemCateg == ItemCateg && x.ItemCode == ItemCode && (x.StartMessage <= DateTime.Now && x.EndMessage >= DateTime.Now)).ToList();
                 
            return await Task.FromResult((IViewComponentResult)View("Default", model));
        } 
       
    }
}
