using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using SmartOffice.eManagement.Class;
using SmartOffice.eManagement.Models;
using SmartOffice.eManagement.ModelsManagementControl;
using SmartOffice.EManagement.IResponsitory;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartOffice.eManagement
{
    public class EUser : IEUser
    {
        private readonly ManagementControlContext _context;

        private IHostingEnvironment _hostingEnvironment;
        private readonly IConfiguration _configuration;
        private IHttpContextAccessor _accessor;

        public EUser(ManagementControlContext context, IHostingEnvironment hostingEnvironment, IConfiguration configuration, IHttpContextAccessor accessor)
        {
            _hostingEnvironment = hostingEnvironment;
            _context = context;
            _configuration = configuration;
            _accessor = accessor;
        }

        public TupleUser GetGroup(string OpeGroupCateg, string Division, string Section, string Department, string Department2, string Department3, string username, string SpecialGroup)
        {
            var dp = new ConnUser(_configuration);
            var data = dp.GetGroup(OpeGroupCateg, Division, Section, Department, Department2, Department3, username, SpecialGroup);
            return data;
        }

        public TupleUser GetGenerate(string strObj, string strObMode, string OperationCode, string OpeGroupCateg, string InputKind, string OperationNo, string UserName)
        {
            var model = new TupleUser { };

            DataTable dataTable = DatatableItem();

            var dp = new ConnUser(_configuration);
            var data = dp.GetGenerate(strObj, strObMode, OperationCode, OpeGroupCateg, InputKind, OperationNo, dataTable, UserName);

            return data;
        }

        private DataTable DatatableItem()
        {
            // Add data item to data table
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("OperationNo", typeof(string));
            dataTable.Columns.Add("OperationCode", typeof(string));
            dataTable.Columns.Add("OperationName", typeof(string));
            dataTable.Columns.Add("OperationDisplayOrder", typeof(int));
            dataTable.Columns.Add("InputKind", typeof(string));
            dataTable.Columns.Add("RoleID", typeof(string));
            dataTable.Columns.Add("OpeGroupCode", typeof(string));
            dataTable.Columns.Add("OpeGroupName", typeof(string));
            dataTable.Columns.Add("OpeGroupCateg", typeof(string));
            dataTable.Columns.Add("DisplayPriority", typeof(int));
            dataTable.Columns.Add("SpecialOperate", typeof(string));
            dataTable.Columns.Add("CategInputOption", typeof(string));
            dataTable.Columns.Add("CategDisplayOrder", typeof(int));
            dataTable.Columns.Add("ItemCateg", typeof(string));
            dataTable.Columns.Add("ItemCategName", typeof(string));
            dataTable.Columns.Add("Remarks1", typeof(string));
            dataTable.Columns.Add("Remarks2", typeof(string));
            dataTable.Columns.Add("Remarks3", typeof(string));
            dataTable.Columns.Add("Remarks4", typeof(string));
            dataTable.Columns.Add("Remarks5", typeof(string));
            dataTable.Columns.Add("RemarksTitle1", typeof(string));
            dataTable.Columns.Add("RemarksTitle2", typeof(string));
            dataTable.Columns.Add("RemarksTitle3", typeof(string));
            dataTable.Columns.Add("RemarksTitle4", typeof(string));
            dataTable.Columns.Add("RemarksTitle5", typeof(string));
            dataTable.Columns.Add("RemarksColor1", typeof(string));
            dataTable.Columns.Add("RemarksColor2", typeof(string));
            dataTable.Columns.Add("RemarksColor3", typeof(string));
            dataTable.Columns.Add("RemarksColor4", typeof(string));
            dataTable.Columns.Add("RemarksColor5", typeof(string));
            dataTable.Columns.Add("ItemCode", typeof(string));
            dataTable.Columns.Add("ItemName", typeof(string));
            dataTable.Columns.Add("InputType", typeof(string));
            dataTable.Columns.Add("DataType", typeof(string));
            dataTable.Columns.Add("DecimalNo", typeof(string));
            dataTable.Columns.Add("Required", typeof(bool));
            dataTable.Columns.Add("Minlength", typeof(string));
            dataTable.Columns.Add("Maxlength", typeof(string));
            dataTable.Columns.Add("Min", typeof(string));
            dataTable.Columns.Add("Max", typeof(string));
            dataTable.Columns.Add("Step", typeof(string));
            dataTable.Columns.Add("Unit", typeof(string));
            dataTable.Columns.Add("InputOptionItem", typeof(string));
            dataTable.Columns.Add("DefaultValue", typeof(string));
            dataTable.Columns.Add("ReadOnly", typeof(bool));
            dataTable.Columns.Add("DetailOption", typeof(string));
            dataTable.Columns.Add("ItemListDisplayOrder", typeof(int));
            dataTable.Columns.Add("FinalResult", typeof(string));
            dataTable.Columns.Add("AddDate", typeof(DateTime));
            dataTable.Columns.Add("UpdDate", typeof(DateTime));
            dataTable.Columns.Add("UserName", typeof(string));
            dataTable.Columns.Add("ComputerName", typeof(string));
            dataTable.Columns.Add("ValueCode", typeof(string));
            return dataTable;
        }


        public List<GetMessage> GetMessage()
        {

            var data = from p in _context.InputItemListMessage
                       join e in _context.ItemCategory
                       on p.ItemCateg equals e.ItemCateg
                       join f in _context.InputItem
                       on p.ItemCode.Trim() equals f.ItemCode
                                             
                       select new GetMessage
                       {
                         ItemCateg=  p.ItemCateg,
                           ItemCategName= e.ItemCategName,
                           ItemCode= p.ItemCode,
                           ItemName =f.ItemName,
                           DisplayOrder =p.DisplayOrder,
                           Message =p.Message,
                           StartMessage =p.StartMessage,
                           EndMessage =p.EndMessage,
                           AddDate =p.AddDate,
                           UpdDate =p.UpdDate,
                           UserName =p.UserName,
                           ComputerName =p.ComputerName
                        };            

            return data.ToList();

        }

        public List<SelectListItemCateg> GetItemCateglist()
        {
            var data = from p in _context.InputItemList
                       join e in _context.ItemCategory
                       on p.ItemCateg equals e.ItemCateg
                       join f in _context.InputItem
                       on p.ItemCode.Trim() equals f.ItemCode

                       select new SelectListItemCateg
                       {
                           ID = e.ItemCateg + "," + f.ItemCode,
                           Name = e.ItemCategName + "," + f.ItemName,
                       };           

            return data.OrderBy(x => x.Name).ToList();
        }


        public dynamic AddMessage(string itemCateg, string displayOrder, string message, string startDate, string endDate, string UserName, string ComputerName)
        {
            var categ = "";
            var code = "";
            var num = 0;
            var display = 1;
            string[] authorsList = itemCateg.Split(",");

            foreach (string author in authorsList)
            {
                if(num == 0)
                {
                    categ = author;
                }
                else
                {
                    code = author;
                }
                num++;
            }
              
            if (displayOrder == null || displayOrder == "")
            {
                //new
                try
                {
                    var checkid = _context.InputItemListMessage.Where(x=>x.ItemCateg == categ && x.ItemCode == code).FirstOrDefault();                   
                    if (checkid != null)
                    {
                        var maxid = _context.InputItemListMessage.Where(x => x.ItemCateg == categ && x.ItemCode == code).Max(x => x.DisplayOrder);
                        display = maxid + 1;
                    }

                    InputItemListMessage _Message = new InputItemListMessage
                    {
                        ItemCateg = categ,
                        ItemCode = code,
                        DisplayOrder = display,
                        Message = message,
                        StartMessage = Convert.ToDateTime( startDate),
                        EndMessage = Convert.ToDateTime(endDate),
                        UserName = UserName,
                        AddDate = DateTime.Now,
                        UpdDate = DateTime.Now,
                        ComputerName = ComputerName

                    };                    
                    _context.InputItemListMessage.Add(_Message);
                    _context.SaveChanges();
                }
                catch (Exception e)
                {
                    var Data1 = new { status = false, subject = "ADD MESSAGE", detail = e.Message };
                    return Data1;
                }
            }
            else
            {
                //edit
                display = Convert.ToInt32(displayOrder);
                try
                {

                    InputItemListMessage _Message = _context.InputItemListMessage.Where(x => x.ItemCateg == categ && x.ItemCode == code && x.DisplayOrder == display).FirstOrDefault();
                    if (_Message != null)
                    {
                        _Message.Message = message;
                        _Message.StartMessage = Convert.ToDateTime(startDate);
                        _Message.EndMessage = Convert.ToDateTime(endDate);
                        _Message.UserName = UserName;
                        _Message.UpdDate = DateTime.Now;
                        _Message.ComputerName = ComputerName;

                        _context.InputItemListMessage.Update(_Message);
                        _context.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    var Data1 = new { status = false, subject = "UPDATE MESSAGE", detail = e.Message };
                    return Data1;
                }
            }

            var Data = new { status = true, subject = "ADD MESSAGE", detail = "Add message complete."};
            return Data;
        }


        public dynamic DeleteMessage(GetMessage getMessage)
        {

            InputItemListMessage _Ctrl = _context.InputItemListMessage.Where(p => p.ItemCateg == getMessage.ItemCateg && p.ItemCode == getMessage.ItemCode && p.DisplayOrder == getMessage.DisplayOrder).FirstOrDefault();
          
            if (_Ctrl != null)
            { 
                _context.InputItemListMessage.Remove(_Ctrl);
                _context.SaveChanges();

                    
                var Data = new { status = true, subject = "DELETE  MESSAGE", detail = "Delete message complete." };
                return Data;
            }
            else
            {
                var Data = new { status = false, subject = "DELETE MESSAGE", detail = "System is can't delete message." };
                return Data;
            }
        }
    }
}
