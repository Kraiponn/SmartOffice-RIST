using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using SmartOffice.Models;
using System.IO;
using Microsoft.EntityFrameworkCore;
using SmartOffice.ModelsEsmartOffice;

namespace  SmartOffice.Controllers
{
    public class FooterDeptController : Controller
    {
        private IHostingEnvironment _hostingEnvironment;
       
             private readonly ESmartOfficeContext _ESmartcontext;

        public FooterDeptController(IHostingEnvironment hostingEnvironment,ESmartOfficeContext context)
        {
            _hostingEnvironment = hostingEnvironment;
            _ESmartcontext = context;
        }



/*************************************************************** Footer Start ************************************************************************/
public JsonResult GetFooterHeaderAsync(String PartId)
        {
            try
            {
                var NewsGroup = User.Claims.FirstOrDefault(c => c.Type == "GroupCateg").Value;
                List<ControlConfig> allControl = _ESmartcontext.ControlConfig.Where(i=>i.PartId ==int.Parse(PartId)&& i.GroupCateg == NewsGroup).ToList();             
                return Json(new { data = allControl });
            }
            catch (Exception e)
            {
                var a = e.Message;
                throw new NotImplementedException();
            }
    }
public ActionResult UpdateFooterHead(ControlConfig _ControlConfig)
        {            
            ControlConfig _Ctrl= _ESmartcontext.ControlConfig.Where(p => p.PartId ==_ControlConfig.PartId && p.GroupCateg == _ControlConfig.GroupCateg && p.ConfigOrder == _ControlConfig.ConfigOrder).FirstOrDefault();

            if(_Ctrl != null){
                _Ctrl.TextH = _ControlConfig.TextH;
                _Ctrl.UpdateDate = DateTime.Now;
                _Ctrl.UpdateBy = User.Claims.FirstOrDefault(c => c.Type == "UserName").Value;
                //_Ctrl.ColorTextH = _ControlConfig.ColorTextH;
                //_Ctrl.BgH = _ControlConfig.BgH;                                       
                try
                {
                 
                    _ESmartcontext.ControlConfig.Update(_Ctrl);            
                    _ESmartcontext.SaveChanges();                                                           
               }catch(Exception e){
                var Data1 = new { status = false, subject = "Update Footer", detail = e.Message };
                return Json(Data1);               
               }                  
            }       
              var Data = new { status = true, subject = "Update Footer", detail = "Update Footer Complete." };
                return Json(Data);
        }
        //Delete News
[HttpPost]
public async Task<JsonResult> DeleteFooter(ControlConfig _ControlConfig){
             
            ControlConfig _Ctrl= _ESmartcontext.ControlConfig.Where(p => p.PartId ==_ControlConfig.PartId && p.GroupCateg == _ControlConfig.GroupCateg && p.ConfigOrder == _ControlConfig.ConfigOrder).FirstOrDefault();
           
            if (_Ctrl != null)
            {
                 List<NnewsHeader> TNews =  _ESmartcontext.NnewsHeader.Where(p => p.PartId ==_Ctrl.PartId && p.GroupCateg == _Ctrl.GroupCateg && p.BadgesName == _ControlConfig.TextH).ToList();
                _ESmartcontext.NnewsHeader.RemoveRange(TNews);
                _ESmartcontext.ControlConfig.Remove(_Ctrl);
                await _ESmartcontext.SaveChangesAsync();   
               var Data = new { status = true, subject = "Delete Footer", detail = "Delete Footer Complete." };
                return Json(Data);
            }
            else
            {
                var Data = new { status = false, subject = "Delete Footer", detail = "System is can't delete footer." };
                return Json(Data);
            }   
        }
[HttpPost]
public async Task<JsonResult> AddFooter(ControlConfig _ControlConfig){
             _ControlConfig.CreatedDate = DateTime.Now;
             _ControlConfig.UpdateDate = DateTime.Now;
             _ControlConfig.PartId = 8;
             _ControlConfig.GroupCateg = User.Claims.FirstOrDefault(c => c.Type == "GroupCateg").Value;;
             _ControlConfig.CreateBy =   User.Claims.FirstOrDefault(c => c.Type == "UserName").Value;  
             _ControlConfig.UpdateBy =  User.Claims.FirstOrDefault(c => c.Type == "UserName").Value;  
            int maxLength = _ESmartcontext.ControlConfig.Where(p =>p.PartId ==8 && p.GroupCateg == _ControlConfig.GroupCateg ).Select(x => x.ConfigOrder).DefaultIfEmpty(0).Max();     
            _ControlConfig.ConfigOrder = maxLength+1;
           
            if (_ControlConfig != null)
            {
                try{
                await _ESmartcontext.ControlConfig.AddAsync(_ControlConfig);
                await _ESmartcontext.SaveChangesAsync();
               }catch{
                var Data1 = new { status = false, subject = "ADD Footer", detail = "System is can't add Footer." };
                return Json(Data1);   
               }
               var Data = new { status = true, subject = "Add Footer", detail = "Add Footer Complete." };
                return Json(Data);
            }
            else
            {
                var Data = new { status = false, subject = "Add Footer", detail = "System is can't add footer." };
                return Json(Data);
            }
        }

//  This method return 
public JsonResult GetSelectList_Header()
        {            
              try
            {
                var NewsGroup = User.Claims.FirstOrDefault(c => c.Type == "GroupCateg").Value;
                List<ControlConfig> allControl =  _ESmartcontext.ControlConfig.Where(i=>i.PartId ==8 && i.GroupCateg == NewsGroup).ToList();             
            
                var fmw = allControl.Select(ddl => new
                {
                    id = ddl.TextH,
                    Name = ddl.TextH
                }).OrderBy(o => o.Name).Distinct().ToList();
                    return Json(new { data = fmw });
                }

            catch (Exception e)
            {
                var a = e.Message;
                throw new NotImplementedException();
            }            
        }

public IActionResult Index(String DocCate,String PartId,String NewHOrder )
{   
    return ViewComponent("GalleryControl", new { DeptType = DocCate,PartId=PartId, newstyle = "Gallery",NewHOrder = NewHOrder });
    
}
public FileResult FileDownload(string docname)
{     
    var news = _ESmartcontext.NnewsDetail.Where(i => i.Value == docname).FirstOrDefault();
    string webRootPath = _hostingEnvironment.WebRootPath +"\\image\\"+news.GroupCateg+"\\Footer\\"+news.Value;
    byte[] fileBytes = System.IO.File.ReadAllBytes(Path.Combine(webRootPath));  
    return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet,news.Value1);
 }
 protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
               _ESmartcontext.Dispose();
            }
            base.Dispose(disposing);
        } 
    }
}