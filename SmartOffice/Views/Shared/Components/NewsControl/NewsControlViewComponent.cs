using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using SmartOffice.Models;
using SmartOffice.Models.ViewModel;
using Microsoft.EntityFrameworkCore;
using SmartOffice.ModelsEsmartOffice;
using System.Collections.Generic;

namespace SmartOffice.Views.Components.NewsControl
{
    public class NewsControlViewComponent : ViewComponent
    {
        private readonly ESmartOfficeContext _Doccontext;
        public NewsControlViewComponent(ESmartOfficeContext Doccontext)
        {
            _Doccontext = Doccontext;
        }
        public async Task<IViewComponentResult> InvokeAsync(string DeptType, string newstyle, string _NewHOrder, string _NewDOrder)
        {
           
            switch (newstyle)
            {
                
                case "Normal":
                    //using(ESmartOfficeContext context =new ESmartOfficeContext()) { 
                    if (DeptType == "Mainnews")
                    {
                        List<NnewsHeader> listnews = _Doccontext.NnewsHeader.Include(i => i.NnewsDetail).Where(i => i.NewsType == "News" && i.NnewsDetail.Any(x => x.CreatedDate != null) && i.Disable == true &&
                        (i.StartDate <= DateTime.Now.Date && i.EndDate >= DateTime.Now.Date)).OrderByDescending(i => i.UpdateDate).Take(12).ToList();
                        ViewNewsModel model = new ViewNewsModel()
                        {
                            News = listnews.ToList(),
                            Department = DeptType
                        };

                        ViewBag.DeptType = "Mainnews";
                        return await Task.FromResult((IViewComponentResult)View("Default", model));
                    }
                    if (DeptType == "Mainnews1")
                    {
                        List<NnewsHeader> listnews = _Doccontext.NnewsHeader.Include(i => i.NnewsDetail).Where(i => i.NewsType == "News" && i.NnewsDetail.Any(x => x.CreatedDate != null) && i.Disable == true &&
                        (i.StartDate <= DateTime.Now.Date && i.EndDate >= DateTime.Now.Date)).OrderByDescending(i => i.UpdateDate).Take(100).ToList();
                        ViewNewsModel model = new ViewNewsModel()
                        {
                            News = listnews.ToList(),
                            Department = DeptType
                        };

                        ViewBag.DeptType = "Mainnews";
                        return await Task.FromResult((IViewComponentResult)View("Default", model));
                    }
                    else if (DeptType == "Allnews")
                    {
                        List<NnewsHeader> listnews = _Doccontext.NnewsHeader.Include(i => i.NnewsDetail).Where(i => i.NewsType == "News" && i.NnewsDetail.Any(x => x.CreatedDate != null) &&
                        (i.StartDate <= DateTime.Now.Date && i.EndDate >= DateTime.Now.Date)).OrderByDescending(i => i.UpdateDate).Take(12).ToList();

                        ViewNewsModel model = new ViewNewsModel()
                        {
                            News = listnews.ToList(),
                            Department = DeptType
                        };

                        ViewBag.DeptType = "Allnews";
                        return await Task.FromResult((IViewComponentResult)View("Default", model));
                    }
                    else
                    {
                        var listnews = await _Doccontext.NnewsHeader.Include(i => i.NnewsDetail).Where(i => i.GroupCateg == DeptType && i.NnewsDetail.Any(NnewsDetail => NnewsDetail.CreatedDate != null) && 
                        i.NewsType == "News" && i.Disable == false  && (i.StartDate <= DateTime.Now.Date && i.EndDate >= DateTime.Now.Date)).OrderByDescending(i => i.NewHorder).Take(12).ToListAsync();
                        ViewNewsModel model = new ViewNewsModel()
                        {
                            News = listnews.ToList(),
                            Department = DeptType
                        };
                        ViewBag.DeptType = DeptType;
                        return await Task.FromResult((IViewComponentResult)View("Default", model));
                    };

                //};                                    
                case "Slide":
                    //using(ESmartOfficeContext context =new ESmartOfficeContext()) { 
                    if (DeptType == "Mainnews")
                    {
                        var listnews = await _Doccontext.NnewsHeader.Include(i => i.NnewsDetail).Where(i => i.NewsType == "Slide" && i.NnewsDetail.Any(x => x.CreatedDate != null))
                            .OrderByDescending(i => i.NewHorder).Take(9).ToListAsync();
                        ViewNewsModel model = new ViewNewsModel()
                        {
                            News = listnews.ToList(),
                            Department = DeptType
                        };
                        return await Task.FromResult((IViewComponentResult)View("slidenews", model));
                    }
                    else
                    {
                        var listnews = await _Doccontext.NnewsHeader.Include(i => i.NnewsDetail).Where(i => i.GroupCateg == DeptType && i.NewsType == "Slide" && i.NnewsDetail.Any(x => x.CreatedDate != null))
                            .OrderByDescending(i => i.NewHorder).Take(15).ToListAsync();
                        ViewNewsModel model = new ViewNewsModel()
                        {
                            News = listnews.ToList(),
                            Department = DeptType
                        };
                        return await Task.FromResult((IViewComponentResult)View("slidenews", model));
                    };
                //};                    
                case "Manage":
                    return await Task.FromResult((IViewComponentResult)View("ManageNews", DeptType));
                case "NewsDetail":

                    var news = await _Doccontext.NnewsHeader.Include(i => i.NnewsDetail).Where(i => i.PartId == 7 && i.NnewsDetail.Any(x=>x.CreatedDate !=null) && i.GroupCateg == DeptType && i.NewHorder == int.Parse(_NewHOrder) 
                    && i.NnewsDetail.Any(x => x.CreatedDate != null)).OrderBy(i=>i.CreatedDate).FirstOrDefaultAsync();

                    //update count read                    
                    NnewsHeader result = (from p in _Doccontext.NnewsHeader
                                          where p.PartId == 7 && p.GroupCateg == DeptType && p.NewHorder == int.Parse(_NewHOrder)
                                          select p).SingleOrDefault();

                    result.CountRead = news.CountRead + 1;
                    _Doccontext.SaveChanges();


                    var  thisitem = new ViewNewsTable()
                        {
                            Title1 = news.Title1,
                            Disable = news.Disable,
                            CreatedDate = Convert.ToDateTime(news.CreatedDate).ToString("dddd,dd MMMM yyyy HH:mm"),
                            CreateBy = news.CreateBy,
                            UpdateDate = Convert.ToDateTime(news.UpdateDate).ToString("dddd,dd MMMM yyyy HH:mm"),
                            GroupCateg = news.GroupCateg,
                            NewsType = news.NewsType,
                            Title2 = news.Title2,
                            UpdateBy = news.UpdateBy,
                            imagePath = news.NnewsDetail.Where(i=>i.ItemType=="image"|| i.ItemType == "video").LastOrDefault() == null ? "" : "~/image/" + news.GroupCateg + "/News/" + news.NnewsDetail.Where(i => i.ItemType == "image" || i.ItemType == "video").LastOrDefault().Value,
                            NnewsDetail = news.NnewsDetail,
                            ItemType = news.NnewsDetail.Where(i => i.ItemType == "image" || i.ItemType == "video").OrderBy(i => i.CreatedDate).LastOrDefault() == null ? "" : news.NnewsDetail.Where(i => i.ItemType == "image" || i.ItemType == "video").OrderBy(i => i.CreatedDate).LastOrDefault().ItemType,
                            Value1 = news.NnewsDetail.Where(i => i.ItemType == "image" || i.ItemType == "video").OrderBy(i => i.CreatedDate).LastOrDefault() == null ? "" : news.NnewsDetail.Where(i => i.ItemType == "image" || i.ItemType == "video").OrderBy(i => i.CreatedDate).LastOrDefault().Value1,
                            CountRead = news.CountRead
                        };

                    foreach (var item in thisitem.NnewsDetail)
                    {
                        item.Value = "~/image/" + news.GroupCateg + "/" + news.NewsType + "/" + item.Value;
                    }

                    return await Task.FromResult((IViewComponentResult)View("NewsDetail", thisitem));
           
                case "Pr":
                    //using(ESmartOfficeContext context =new ESmartOfficeContext()) { 

                    var news1 = await _Doccontext.NnewsHeader.Include(i => i.NnewsDetail).Where(i => i.PartId == 7 && i.GroupCateg == DeptType && i.NewHorder == int.Parse(_NewHOrder)
                    && i.NnewsDetail.Any(x => x.CreatedDate != null)).OrderBy(i => i.CreatedDate).FirstOrDefaultAsync();


                        var  thisitem1 = new ViewNewsTable()
                        {
                            Title1 = news1.Title1,
                            Disable = news1.Disable,
                            CreatedDate = Convert.ToDateTime(news1.CreatedDate).ToString("dddd,dd MMMM yyyy HH:mm"),
                            CreateBy = news1.CreateBy,
                            UpdateDate = Convert.ToDateTime(news1.UpdateDate).ToString("dddd,dd MMMM yyyy HH:mm"),
                            GroupCateg = news1.GroupCateg,
                            NewsType = news1.NewsType,
                            Title2 = news1.Title2,
                            UpdateBy = news1.UpdateBy,
                            imagePath = news1.NnewsDetail.Where(i => i.ItemType == "image" || i.ItemType == "video").OrderBy(i => i.CreatedDate).LastOrDefault() == null ? "" : "~/image/" + news1.GroupCateg + "/Slide/" + news1.NnewsDetail.Where(i => i.ItemType == "image" || i.ItemType == "video").OrderBy(i => i.CreatedDate).LastOrDefault().Value,
                            NnewsDetail = news1.NnewsDetail,
                            ItemType = news1.NnewsDetail.Where(i => i.ItemType == "image" || i.ItemType == "video").OrderBy(i => i.CreatedDate).LastOrDefault() == null ? "" : news1.NnewsDetail.Where(i => i.ItemType == "image" || i.ItemType == "video").OrderBy(i => i.CreatedDate).LastOrDefault().ItemType,
                            Value1 = news1.NnewsDetail.Where(i => i.ItemType == "image" || i.ItemType == "video").OrderBy(i => i.CreatedDate).LastOrDefault() == null ? "" : news1.NnewsDetail.Where(i => i.ItemType == "image" || i.ItemType == "video").OrderBy(i => i.CreatedDate).LastOrDefault().Value1
                        };

                    return await Task.FromResult((IViewComponentResult)View("Pr", thisitem1));
    
                //}
                case "ViewAll":
                    //using(ESmartOfficeContext context =new ESmartOfficeContext()) { 


                    if (DeptType == "Mainnews")
                    {
                        ViewBag.DeptType = "Mainnews";
                        return await Task.FromResult((IViewComponentResult)View("ViewAll"));

                        
                    }
                    else
                    {

                        /*   var listnews = context.NnewsHeader.Where(i=> i.NewsType=="Slide").Include(i=>i.NnewsDetail).OrderByDescending(i=>i.NewHorder).Take(200);
                          ViewNewsModel model = new ViewNewsModel(){
                              news = listnews.ToList(),
                              department = DeptType
                          }; */
                        ViewBag.DeptType = DeptType;
                        return await Task.FromResult((IViewComponentResult)View("ViewAll"));
                    }
                //};
                case "Viewpage":
                    //using(ESmartOfficeContext context =new ESmartOfficeContext()) { 

                    var news2 = await _Doccontext.NnewsHeader.Include(i => i.NnewsDetail).Where(i => i.PartId == 8 && i.GroupCateg == DeptType && i.NewHorder == int.Parse(_NewHOrder)
                    && i.NnewsDetail.Any(x => x.CreatedDate != null)).OrderBy(i => i.CreatedDate).FirstOrDefaultAsync();


                        var  thisitem2 = new ViewNewsTable()
                        {
                            Title1 = news2.Title1,
                            Disable = news2.Disable,
                            CreatedDate = Convert.ToDateTime(news2.CreatedDate).ToString("dddd,dd MMMM yyyy HH:mm"),
                            CreateBy = news2.CreateBy,
                            UpdateDate = Convert.ToDateTime(news2.UpdateDate).ToString("dddd,dd MMMM yyyy HH:mm"),
                            GroupCateg = news2.GroupCateg,
                            NewsType = news2.NewsType,
                            Title2 = news2.Title2,
                            UpdateBy = news2.UpdateBy,
                            imagePath = news2.NnewsDetail.Where(i => i.ItemType == "image" || i.ItemType == "video").OrderBy(i => i.CreatedDate).LastOrDefault() == null ? "" : "~/image/" + news2.GroupCateg + "/Footer/" + news2.NnewsDetail.Where(i => i.ItemType == "image" || i.ItemType == "video").OrderBy(i => i.CreatedDate).LastOrDefault().Value,
                            NnewsDetail = news2.NnewsDetail,
                            ItemType = news2.NnewsDetail.Where(i => i.ItemType == "image" || i.ItemType == "video").OrderBy(i => i.CreatedDate).LastOrDefault() == null ? "" : news2.NnewsDetail.Where(i => i.ItemType == "image" || i.ItemType == "video").OrderBy(i => i.CreatedDate).LastOrDefault().ItemType,
                            Value1 = news2.NnewsDetail.Where(i => i.ItemType == "image" || i.ItemType == "video").OrderBy(i => i.CreatedDate).LastOrDefault() == null ? "" : news2.NnewsDetail.Where(i => i.ItemType == "image" || i.ItemType == "video").OrderBy(i => i.CreatedDate).LastOrDefault().Value1
                        };
                    foreach (var item in thisitem2.NnewsDetail)
                    {
                        item.Value = "~/image/" + news2.GroupCateg + "/" + news2.NewsType + "/" + item.Value;
                    }

                    return await Task.FromResult((IViewComponentResult)View("NewsDetail", thisitem2));
             
                //}

                default:
                    return await Task.FromResult((IViewComponentResult)View("Default", DeptType));

            }
            //return View("Default", DeptType);

        }

    }
}
