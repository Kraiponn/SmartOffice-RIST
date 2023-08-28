using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartOffice.Models.ViewModel;
using Microsoft.EntityFrameworkCore;
using SmartOffice.ModelsDocControl;
using SmartOffice.IResponsitory;
using System.Security.Claims;
using SmartOffice.ModelsHRMSLocal;
using System.Data.SqlClient;
using SmartOffice.Models;
using SmartOffice.ModelsEsmartOffice;
using SmartOffice.ModelsForm;

namespace SmartOffice.Services
{
    public class SearchEngineService: ISearchEngineService
    {
        private readonly DocumentControlContext _DocumentContext;  
        private readonly HRMSLocalContext _HRMSLocalContext;
        private readonly ESmartOfficeContext _ESmartOfficeContext;

        public SearchEngineService(DocumentControlContext DocumentControlContext, HRMSLocalContext HRMSLocalContext , ESmartOfficeContext ESmartOfficeContext)
        {
            _DocumentContext = DocumentControlContext;
            _HRMSLocalContext = HRMSLocalContext;
            _ESmartOfficeContext = ESmartOfficeContext;
        }

        public async Task<SearchViewModel> GetSearch(string SearchTxt, string UserId)
        {
            var listform = await GetSearchFormAsync(SearchTxt);
            var listformdata = await GetSearchFormData(SearchTxt,UserId);
            var listRoomsdata = await GetSearchRoomData(SearchTxt);
            var listTelephonesdata = await GetSearchTelephone(SearchTxt);
            SearchViewModel newdata = new SearchViewModel()
            {
                ListDocument = listform,
                ListDocumentData = listformdata,
                ListRoomData = listRoomsdata,
                ListTelephoneData = listTelephonesdata,
            };
            return newdata;
        }

        public async Task<List<DocumentResult>> GetSearchFormAsync(string txtsearch)
        {
            //var listDocSearch = await _DocumentContext.Document.Where(i => i.DocumentNameE.ToLower().Contains(txtsearch.ToLower())
            //                                                   || i.DocumentNameT.ToLower().Contains(txtsearch.ToLower())
            //                                                   || i.DocumentNameJ.ToLower().Contains(txtsearch.ToLower())).ToListAsync();

            var listDocall = await _ESmartOfficeContext.Set<FormMenu>().FromSql("exec sprFormMenu").AsNoTracking().ToListAsync();
            var listDocSearch = listDocall.Where(i => (i.MenuNameE != null && i.MenuNameE.ToLower().Contains(txtsearch.ToLower()))
                                            || (i.MenuNameT != null && i.MenuNameT.ToLower().Contains(txtsearch.ToLower()))
                                            || (i.MenuNameJ != null && i.MenuNameJ.ToLower().Contains(txtsearch.ToLower()))).ToList();

            List<DocumentResult> listDoc = new List<DocumentResult>();
            int ii = 1;
            foreach (var item in listDocSearch)
            {
                listDoc.Add(new DocumentResult()
                {
                    No = ii,
                    DocumentName = item.MenuNameE + ": " + item.MenuNameT,
                    DocumentPath = "/DynamicForm/Index?code=" + item.Param,
                    DocumentCode = item.Param.Trim()
                });
                ii++;
            }

            return listDoc;
        }

        public async Task<List<DocumentData>> GetSearchFormData(string txtsearch ,string UserId)
        {
            try
            {
                SqlParameter _TxtSearch = new SqlParameter("@TextSearch", txtsearch);
                SqlParameter _UserId = new SqlParameter("@userid", UserId);
                List<VewSearchDocument> listDocSearch = await _DocumentContext.Set<VewSearchDocument>().FromSql("exec sprFormSearchDocument @TextSearch,@userid", _TxtSearch, _UserId).AsNoTracking().ToListAsync();
               
                List<DocumentData> listDoc = new List<DocumentData>();
                int ii = 1;
                foreach (var item in listDocSearch)
                {
                    listDoc.Add(new DocumentData()
                    {
                        No = ii,
                        DocumentName = item.DocumentNameE + ": " + item.DocumentNameT,
                        DocumentID = item.DocumentNo.Trim(),
                        DocumentCode = item.DocumentCode.Trim(),
                        DocumentSubject = item.FinalResult,
                        ReqOperatorName = item.ReqOperatorName,
                        DocumentPath = "/DynamicForm/Index?code=" + item.DocumentCode
                    });
                    ii++;
                }
                return listDoc;
            }
            catch (Exception ex)
            {
                var aa = ex.Message;
                return null;
            }
            

           
        }
        public async Task<List<RoomData>> GetSearchRoomData(string txtsearch)
        {
            var listDocSearch = await _DocumentContext.SearchRoom.Where(i => (i.Name.ToLower().Contains(txtsearch.ToLower()) || i.Subject.ToLower().Contains(txtsearch.ToLower())) && i.StartDate >= DateTime.Now).Take(200).OrderBy(i=>i.OperatorID).ToListAsync();

            List<RoomData> listDadt = new List<RoomData>();
            int ii = 1;
            foreach (var item in listDocSearch)
            {
                listDadt.Add(new RoomData()
                {
                    Name = item.Name,
                    StartDate = item.StartDate.ToString(),
                    EndDate = item.EndDate.ToString(),
                    Subject = item.Subject.Trim(),
                    OperatorID = item.OperatorID,
                    OperatorName = item.OperatorName
                });
                ii++;
            }

            return listDadt;
        }
        public async Task<List<InternalTelephone>> GetSearchTelephone(string txtsearch)
        {
            var listDataSearch = await _HRMSLocalContext.InternalTelephone.Where(i => i.Name.ToLower().Contains(txtsearch.ToLower())).ToListAsync();

            List<InternalTelephone> listDadt = new List<InternalTelephone>();
            int ii = 1;
            foreach (var item in listDataSearch)
            {
                listDadt.Add(new  SmartOffice.ModelsHRMSLocal.InternalTelephone()
                {
                    Name = item.Name,
                   Group2 = item.Group2,
                   Group3 = item.Group3,
                    TelNo = item.TelNo,
                    Id   = item.Id
                });
                ii++;
            }




            return listDadt;
        }
    }
}