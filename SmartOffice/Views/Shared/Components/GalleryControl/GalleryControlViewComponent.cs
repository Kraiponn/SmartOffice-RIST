using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using SmartOffice.Models;
using SmartOffice.Models.ViewModel;
using Microsoft.EntityFrameworkCore;
using SmartOffice.ModelsEsmartOffice;

namespace SmartOffice.Views.Shared.Components.GalleryControl
{
    public class GalleryControlViewComponent : ViewComponent
    {
        private readonly ESmartOfficeContext _Doccontext;
        public GalleryControlViewComponent(ESmartOfficeContext Doccontext)
        {
            _Doccontext = Doccontext;
        }
        public async Task<IViewComponentResult> InvokeAsync(string DeptType,string PartId,string Newstyle,string NewHOrder)
        {
            if (Newstyle == "Gallery")
            {
                var news = _Doccontext.NnewsHeader.Where(i => i.PartId == 8 && i.GroupCateg == DeptType && i.NewHorder == int.Parse(NewHOrder)).Include(i => i.NnewsDetail).Select(p => new ViewNewsTable()
                {
                    Title1 = p.Title1,
                    Disable = p.Disable,
                    CreateBy = p.CreateBy,
                    GroupCateg = p.GroupCateg,
                    NewsType = p.NewsType,
                    Title2 = p.Title2,
                    UpdateBy = p.UpdateBy,
                    NnewsDetail = p.NnewsDetail
                }).FirstOrDefault();
                return await Task.FromResult((IViewComponentResult)View("Default", news));

            }
            else if(Newstyle == "Random")
            {
                var random = new Random();
                var list = _Doccontext.NnewsHeader.Where(i => i.PartId == 8 && i.GroupCateg == DeptType && i.ChildType==4).Select(i => i.NewHorder).ToList();
                int index = random.Next(list.Count);

                var news = _Doccontext.NnewsHeader.Where(i => i.PartId == 8 && i.GroupCateg == DeptType && i.NewHorder == int.Parse(list[index].ToString())).Include(i => i.NnewsDetail).Select(p => new ViewNewsTable()
                {
                    Title1 = p.Title1,
                    Disable = p.Disable,
                    CreateBy = p.CreateBy,
                    GroupCateg = p.GroupCateg,
                    NewsType = p.NewsType,
                    Title2 = p.Title2,
                    UpdateBy = p.UpdateBy,
                    NnewsDetail = p.NnewsDetail
                }).FirstOrDefault();
                return await Task.FromResult((IViewComponentResult)View("Gallery", news));

            }

            return await Task.FromResult((IViewComponentResult)View("Default"));

        }
    }
}
