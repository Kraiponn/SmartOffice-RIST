using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmartOffice.Models;
using SmartOffice.Models.ViewModel;
using Microsoft.EntityFrameworkCore;
using SmartOffice.ModelsEsmartOffice;

namespace SmartOffice.Views.Shared.Components.FooterDeptControl
{
    public class FooterDeptControlViewComponent : ViewComponent
    {
        private readonly ESmartOfficeContext _Doccontext;
        public FooterDeptControlViewComponent(ESmartOfficeContext Doccontext)
        {
            _Doccontext = Doccontext;
        }
        public async Task<IViewComponentResult> InvokeAsync(string DeptType,string newstyle,string _NewHOrder,string _NewDOrder)
        {
            
                var listcontrol = _Doccontext.ControlConfig.Where(i=>i.GroupCateg==DeptType && i.PartId==8).OrderBy(i=>i.ConfigOrder).ToList();
                var listnews = _Doccontext.NnewsHeader.Where(i=>i.GroupCateg==DeptType && i.PartId==8 && i.NewsType=="Footer").Include(i=>i.NnewsDetail).OrderBy(i=>i.NewHorder).ToList();
                FooterDeptViewModel Footer = new FooterDeptViewModel(){
                    ControlConfigs = listcontrol,
                    NnewsHeaders = listnews
                };
               return await Task.FromResult((IViewComponentResult)View("Default", Footer));
       
              }
            

            //return View("Default", DeptType);
          
    }
    public class FileDownloadController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

