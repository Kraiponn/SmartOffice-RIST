using Microsoft.AspNetCore.Mvc;
using ControlDocument;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace ControlDocument.Controllers
{
   
    public class DynamicFormController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly DocumentControlContext _DocumentContext;
        public DynamicFormController(IConfiguration configuration, DocumentControlContext DocumentControlContext)
        {
           
            this._configuration = configuration;
            this._DocumentContext = DocumentControlContext;
        }
      

        public IActionResult Index()
        {
             return View();
        }
        public IActionResult CreateForm()
        {

            return View();
        }
        public IActionResult ViewForm(string id)
        {
            ViewBag.id = id;
            return View();
        }
        public IActionResult EditForm(string id)
        {
            ViewBag.id = id;
            return View();
        }   
        
         //แบบ store 
        [HttpPost]
        public IActionResult Load_Add(Tuple2 Tuple2)
        {
             var CreateBy = User.Claims.FirstOrDefault(c => c.Type == "UserName").Value;
             string DocNo = Tuple2.vewInputItemList2.FirstOrDefault().DocumentNo;
            string ComputerName = System.Net.Dns.GetHostName();

            try
            {
                int chkout = 0;
                DataTable dataTable = new DataTable();
                        dataTable.Columns.Add("DocumentNo", typeof(string));
                        dataTable.Columns.Add("DocumentCode", typeof(string)); 
                        dataTable.Columns.Add("ItemCateg", typeof(string)); 
                        dataTable.Columns.Add("ItemCategName", typeof(string)); 
                        dataTable.Columns.Add("ItemCode", typeof(string)); 
                        dataTable.Columns.Add("ItemName", typeof(string)); 
                        dataTable.Columns.Add("FinalResult", typeof(string));
                        dataTable.Columns.Add("AddDate", typeof(DateTime)); 
                        dataTable.Columns.Add("UpdDate", typeof(DateTime));  
                        dataTable.Columns.Add("UserName", typeof(string)); 
                        dataTable.Columns.Add("ComputerName", typeof(string));  
                foreach (var item in Tuple2.vewInputItemList2)
                {
                    if (item.DocumentNo != null)
                    {
                        DataRow workRow = dataTable.NewRow();  
                        workRow["DocumentNo"] = item.DocumentNo;
                        workRow["DocumentCode"] =item.DocumentCode; 
                        workRow["ItemCateg"] = item.ItemCateg; 
                        workRow["ItemCategName"] = item.ItemCategName; 
                        workRow["ItemCode"] = item.ItemCode; 
                        workRow["ItemName"] = item.ItemName; 
                        workRow["FinalResult"] =item.FinalResult; 
                        workRow["AddDate"] =DateTime.Now; 
                        workRow["UpdDate"] =DateTime.Now; 
                        workRow["UserName"] = CreateBy; 
                        workRow["ComputerName"] = ComputerName;   
                        dataTable.Rows.Add(workRow);                                    
                    }
                }

                  //แบบ Store
                         chkout =  _DocumentContext.Database.ExecuteSqlCommand("EXEC dbo.SaveItemDetail @TblMulti={0}, @DeptType={1}, @DocNo={2}",dataTable, "A",DocNo);
                        
                if (chkout ==0)
                {                   
                    var Data = new { status = false, subject = "Add Item", detail = "Add not Successfully!" };
                    return Json(Data);
                }
                else
                {                  
                    var Data = new { status = true, subject = "Add Item", detail = "Added Successfully!" };
                    return Json(Data);
                }
            }
            catch (Exception e)
            {                                             
                    var Data = new { status = false, subject = "Add Item", detail =  e.Message.ToString() };
                    return Json(Data);
            }           
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
               //_context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}