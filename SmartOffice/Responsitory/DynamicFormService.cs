using Microsoft.Extensions.Configuration;
using SmartOffice.Models;
using SmartOffice.ModelsForm;
using SmartOffice.ModelsDocControl;
using System;
using System.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data.SqlClient;
using SmartOffice.IResponsitory;
using System.Collections.Generic;
using System.IO;
using SelectListItem = SmartOffice.ModelsForm.SelectListItem;
using SmartOffice.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using SmartOffice.ModelsEsmartOffice;
using SmartOffice.ModelsHRMSLocal;
using Microsoft.Extensions.Logging;
using SmartOffice.EManagement.IResponsitory;
using iTextSharp.text.pdf;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace SmartOffice.Class
{
    public class DynamicFormService : IDynamicFormService
    {
        private readonly ESmartOfficeContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly DocumentControlContext _DocumentContext;
        private readonly IPDFFormService _samplePDFFormService;
        private IHostingEnvironment _hostingEnvironment;
        private readonly ILogger<DynamicFormService> _logger;
        private readonly HRMSLocalContext _hRMSLocalContext;
        private IHttpContextAccessor _accessor;
        private readonly IMachineOperation _machineOperation;

        public DynamicFormService(UserManager<ApplicationUser> userManager, IConfiguration configuration, DocumentControlContext DocumentControlContext,
          ILogger<DynamicFormService> logger, IPDFFormService PDFFormService,
     IHostingEnvironment hostingEnvironment, ESmartOfficeContext context, HRMSLocalContext hRMSLocalContext
          , IHttpContextAccessor accessor, IMachineOperation machineOperation)
        {
            _userManager = userManager;
            _hostingEnvironment = hostingEnvironment;
            this._configuration = configuration;
            this._DocumentContext = DocumentControlContext;
            _context = context;
            _samplePDFFormService = PDFFormService;
            _hRMSLocalContext = hRMSLocalContext;
            _accessor = accessor;
            _logger = logger;
            _machineOperation = machineOperation;
        }



        //------------------------------------Get Method--------------------------------------------------------------------------------------//
        public dynamic GetFileAttach(string DocNo, string DocCode)
        {
            try
            {
                List<AttachFile> allfile = _DocumentContext.AttachFile.Where(p => p.DocumentNo == DocNo && p.DocumentCode == DocCode).ToList().Select(p => new AttachFile()
                {
                    No = p.No,
                    ComputerName = p.ComputerName,
                    DocumentCode = p.DocumentCode.Trim(),
                    DocumentNo = p.DocumentNo.Trim(),
                    FileName = p.FileName,
                    Path = p.Path,
                    UpdDate = p.UpdDate,
                    UserName = p.UserName,
                    AddDate = p.AddDate
                }).ToList();

                var data = new { data = allfile };
                return data;
            }
            catch (Exception ex)
            {
                var a = ex.Message;
                throw new NotImplementedException();
            }
        }
        public dynamic GetTableConst(string code, string docno)
        {

            ConnDoc dp = new ConnDoc(_configuration);
            var tabletype = dp.GetvewDocumentItemList(code, docno ?? "").Where(i => i.InputType == "table").OrderBy(x => x.InputItemListDisplayOrder).Select(x => new
            {
                InputItemCode = x.InputItemCode.Trim(),
                x.DisplayOrder,
                ValueCode = x.valueold.Trim(),

            }).ToList();

            if (docno != "" && docno != null)
            {
                var allcol = _DocumentContext.DocumentItemValueTableDetail.Where(p => p.DocumentNo.Trim() == docno.Trim()).ToList();
                var allItem = allcol.Select(i => i.InputItemCode).Distinct();

                List<ListTypeTableModel> newlist = new List<ListTypeTableModel>();

                foreach (var item in allItem)
                {
                    ListTypeTableModel newitem = new ListTypeTableModel();
                    var listdata = allcol.Where(i => i.InputItemCode == item).Select(p => new Models.Listdatas()
                    {
                        Header = p.ItemCode.Trim(),
                        Value = p.FinalResult,
                        ItemId = p.ItemId,
                        InputType = p.InputType,
                        DataType = p.DataType

                    }).ToList();
                    newitem.Itemcode = item.Trim();
                    newitem.Listdata = listdata;
                    newlist.Add(newitem);
                }
                return new { alltable = tabletype, allarray = newlist };
            }
            return new { alltable = tabletype };
        }
        public dynamic GetCol(string code)
        {
            try
            {

                var allcol = _DocumentContext.ValueTable.Where(p => p.TableCode.Trim() == code.Trim()).Select(p => new ValueTable()
                {
                    ValueT = p.ValueT,
                    ValueE = p.ValueE,
                    ValueJ = p.ValueJ,
                    InputType = p.InputType,
                    Id = p.Id,
                    DisplayOrder = p.DisplayOrder,
                    TableCode = p.TableCode.Trim(),
                    ValueCode = p.ValueCode.Trim(),
                    DataType = p.DataType

                }).OrderBy(i => i.DisplayOrder).ToList();
                return new { allCol = allcol };
            }
            catch (Exception ex)
            {
                var a = ex.Message;
                throw new NotImplementedException();
            }
        }
        public dynamic GetDataInTable(string docid)
        {
            try
            {

                var allcol = _DocumentContext.DocumentItemValueTableDetail.Where(p => p.DocumentNo.Trim() == docid.Trim()).ToList();
                var allItem = allcol.Select(i => i.InputItemCode).Distinct();

                List<ListTypeTableModel> newlist = new List<ListTypeTableModel>();

                foreach (var item in allItem)
                {
                    ListTypeTableModel newitem = new ListTypeTableModel();
                    var listdata = allcol.Where(i => i.InputItemCode == item).Select(p => new Models.Listdatas()
                    {
                        Header = p.ItemCode,
                        Value = p.FinalResult,
                        InputType = p.InputType,
                        DataType = p.DataType
                    }).ToList();
                    newitem.Itemcode = item;
                    newitem.Listdata = listdata;
                    newlist.Add(newitem);
                }

                return new { allarray = newlist };

            }
            catch (Exception ex)
            {
                var a = ex.Message;
                throw new NotImplementedException();
            }
        }
        public SettingRole GetSettingRoleModel(string GroupCateg)
        {
            var model = new SettingRole
            {
                roleHrmsEmployees = _hRMSLocalContext.HrmsEmployee.Where(x => x.Inactive == null || x.Inactive >= DateTime.Now).ToList().Select(p => new RoleHrmsEmployee()
                {
                    value = p.Codempid,
                    text = p.Codempid + " : " + p.Namempe,
                }).OrderBy(x => x.text).ToList(),

                dataRoles = _DocumentContext.Role.Where(x => x.ApplicationName == GroupCateg).ToList().Select(p => new DataRole()
                {
                    value = p.RoleId,
                    text = p.RoleId + " : " + p.Remarks,
                }).OrderBy(x => x.text).ToList()
            };
            return model;
        }
        public dynamic Getmasterrole(string GroupCateg)
        {
            try
            {
                var ApprovalFlow = _DocumentContext.ApprovalFlow.ToList();
                var datamaster = _DocumentContext.Role.Where(x => x.ApplicationName == GroupCateg).OrderBy(x => x.ApplicationName);

                List<Role> newlist = new List<Role>();

                foreach (var item in datamaster)
                {
                    Role newitem = new Role();
                    var ListFlowId = ApprovalFlow.Where(i => i.RoleId == item.RoleId).Select(x => new { x.FlowId }).Distinct().ToList();
                    var Flow = "";
                    foreach (var itemListFlowId in ListFlowId)
                    {
                        Flow = Flow + " " + itemListFlowId.FlowId;
                    }
                    newitem.RoleId = item.RoleId;
                    newitem.ApplicationName = item.ApplicationName;
                    newitem.Value = Flow.Trim();
                    newitem.Remarks = item.Remarks;
                    newitem.AddDate = item.AddDate;
                    newitem.UpdDate = item.UpdDate;
                    newitem.UserName = item.UserName;
                    newitem.ComputerName = item.ComputerName;
                    newlist.Add(newitem);
                }

                return new { data = newlist };
            }
            catch (Exception e)
            {
                var a = e.Message;
                throw new NotImplementedException();
            }
        }
        public dynamic GetDoc(string GroupCateg)
        {
            try
            {
                List<FormMenu> datamenus = _context.Set<FormMenu>().FromSql("exec sprFormMenu").ToList();
                var datamenus1 = datamenus.Where(x => x.GroupCateg == GroupCateg && !(string.IsNullOrEmpty(x.Param))).ToList();
                var ApprovalFlow = _DocumentContext.ApprovalFlow.ToList();
                var ApprovalItem = _DocumentContext.ApprovalItem.ToList();
                var Role = _DocumentContext.Role.ToList();
                var data = from p in ApprovalFlow
                           join e in ApprovalItem
                           on p.ApprovalItemCode equals e.ApprovalItemCode
                           join f in datamenus1
                           on p.FlowId.Trim() equals f.Param.Trim()
                           join g in Role
                           on p.RoleId.Trim() equals g.RoleId.Trim()
                           where (f.GroupCateg == GroupCateg && !(String.IsNullOrEmpty(f.Param)))
                           select new
                           {
                               FlowID = p.FlowId,
                               MenuName = p.FlowId + " " + f.MenuNameT + " " + f.MenuNameE + " " + f.MenuNameJ,
                               p.SeqNo,
                               RoleID = p.RoleId,
                               p.ApprovalItemCode,
                               e.ApprovalItemNameE,
                               e.ApprovalItemNameT,
                               e.ApprovalItemNameJ,
                               p.Requirement,
                               p.RequirementRemark,
                               p.AssignFlag,
                               p.AssignFlagRemark,
                               g.Remarks,
                               p.Reject,
                               p.AssignFlagBySeq
                           };


                return new { data = data.OrderBy(x => x.MenuName) };
            }
            catch (Exception e)
            {
                var a = e.Message;
                throw new NotImplementedException();
            }
        }
        public dynamic GetmasterFlow(string flowid)
        {
            try
            {
                var flow = (from p in _DocumentContext.ApprovalFlow
                            join e in _DocumentContext.ApprovalItem
                on new { p.ApprovalItemCode }
                equals new { e.ApprovalItemCode }
                            where p.FlowId == flowid
                            select new
                            {
                                FlowID = p.FlowId.Trim(),
                                p.SeqNo,
                                RoleID = p.RoleId.Trim(),
                                ApprovalItemCode = p.ApprovalItemCode.Trim(),
                                ApprovalItemNameE = e.ApprovalItemNameE.Trim(),
                                ApprovalItemNameT = e.ApprovalItemNameT.Trim(),
                                ApprovalItemNameJ = e.ApprovalItemNameJ.Trim(),
                                Requirement = p.Requirement.Trim(),
                                p.RequirementRemark,
                                p.AssignFlag,
                                p.AssignFlagRemark,

                            }).OrderBy(i => i.SeqNo).ToList();


                return new { data = flow };
            }
            catch (Exception e)
            {
                var a = e.Message;
                throw new NotImplementedException();
            }
        }
        public dynamic Getmasteruser(string role)
        {
            try
            {
                var userrole = _DocumentContext.OperatorRole.Where(x => x.RoleId == role).Select(x => x.OperatorId).ToList();
                var user = _hRMSLocalContext.HrmsEmployee.Where(x => userrole.Contains(x.Codempid)).ToList();


                return new { data = user };
            }
            catch (Exception e)
            {
                var a = e.Message;
                throw new NotImplementedException();
            }
        }

        public dynamic GetFormCondition(string docno, string doccode)
        {
            if (docno == "" || docno == null || docno.Substring(0, 1) == "D")
            {
                var flow = _DocumentContext.DocumentCondition.Where(i => i.DocumentCode == doccode).ToList();
                return flow;
            }
            else
            {
                var flow = _DocumentContext.DocumentConditionHist.Where(i => i.DocumentCode == doccode && i.DocumentNo == docno).ToList();
                return flow;
            }

        }
        public dynamic GetMenu()
        {
            try
            {
                var datamaster = _context.Set<FormMenu>().FromSql("exec sprFormMenu").AsNoTracking().ToList();
                var datamaster1 = datamaster.Where(x => !(String.IsNullOrEmpty(x.Param))).OrderBy(x => x.GroupName).ThenBy(x => x.MenuNameT).ThenBy(x => x.MenuNameE).ThenBy(x => x.MenuNameJ).ToList();

                return new { data = datamaster1 };
            }
            catch (Exception e)
            {
                var a = e.Message;
                throw new NotImplementedException();
            }
        }
        public List<UserFlow> GetUserFlow(string DocCode)
        {
            var dp = new ConnDoc(_configuration);
            var UserFlow = dp.GetUserFlow(DocCode, "", "", "").OrderBy(x => x.FlowID).ThenBy(x => x.SeqNo).ToList(); //all user by flowID           
            return UserFlow;

        }
        public dynamic Getprogress(string DocCode, string DocNo)
        {

            var progress = _DocumentContext.DocumentItemProgress.Where(x => x.DocumentCode == DocCode && x.DocumentNo == DocNo).OrderBy(x => x.SeqNo).ToList();
            return progress;

        }
        public async Task<List<string>> GetListAttachFileAsync(string DocNo)
        {
            return await _DocumentContext.AttachFile.Where(x => x.DocumentNo == DocNo).OrderBy(i => i.AddDate).Select(i => i.FileName).ToListAsync();
        }
        public async Task<Tuple1> GetModelAsync(string code, string docno, string mode, string seq, string CreateBy)
        {
            var model = new Tuple1 { };
            var dp = new ConnDoc(_configuration);
            try
            {

                model.Language = await _DocumentContext.Language.ToListAsync();

                model.Flows = dp.GetFlow(docno ?? "").ToList();

                if (docno == "" || docno == null || docno.Substring(0, 1) == "D")
                {
                    model.UserFlow = dp.GetUserFlow(code, docno, seq, "1").ToList(); //all user by flowID

                    model.UserFlowByRole = dp.GetUserFlow(code, docno, seq, "1").Select(x => new { x.FlowID, x.RoleID, x.SeqNo, x.ApprovalItemNameE, x.ApprovalItemNameJ, x.ApprovalItemNameT }).Distinct().ToList().Select(p => new UserFlowByRole()
                    {
                        FlowID = p.FlowID,
                        RoleID = p.RoleID,
                        SeqNo = int.Parse(p.SeqNo),
                        ApprovalItemNameE = p.ApprovalItemNameE,
                        ApprovalItemNameJ = p.ApprovalItemNameJ,
                        ApprovalItemNameT = p.ApprovalItemNameT
                    }).OrderBy(x => x.FlowID).ThenBy(x => x.SeqNo).ToList();
                }
                else
                {
                    model.UserFlow = dp.GetUserFlow(code, docno, seq, "2").Where(x => x.SeqNo != seq).ToList();

                    model.UserFlowByRole = dp.GetUserFlow(code, docno, seq, "2").Where(x => x.SeqNo != seq).Select(x => new { x.FlowID, x.RoleID, x.SeqNo, x.ApprovalItemNameE, x.ApprovalItemNameJ, x.ApprovalItemNameT }).Distinct().ToList().Select(p => new UserFlowByRole()
                    {
                        FlowID = p.FlowID,
                        RoleID = p.RoleID,
                        SeqNo = int.Parse(p.SeqNo),
                        ApprovalItemNameE = p.ApprovalItemNameE,
                        ApprovalItemNameJ = p.ApprovalItemNameJ,
                        ApprovalItemNameT = p.ApprovalItemNameT,
                        SelectedValues = model.UserFlow.Where(x => x.FlowID == p.FlowID && x.RoleID == p.RoleID && x.SeqNo == p.SeqNo && x.checkselect == "1").OrderBy(x => x.Id).Select(x => x.Value.Trim()).ToArray()
                    }).OrderBy(x => x.FlowID).ThenBy(x => x.SeqNo).ToList();
                }



                model.HrmsEmployees = _hRMSLocalContext.HrmsEmployee.Where(x => x.Inactive == null && x.Emplevel >= 3).ToList().Select(p => new ModelsForm.HrmsEmployee()
                {
                    Value = p.Codempid + " : " + p.Namempe,
                    Text = p.Codempid + " : " + p.Namempe,
                }).OrderBy(x => x.Text).ToList();

                model.UsersTransferList = dp.GetUserFlow(code, docno, seq, "").Where(x => x.SeqNo == seq && x.CODEMPID != CreateBy).Select(x => new { x.Text, x.CODEMPID }).Distinct().ToList().Select(p => new UsersTransfer()
                {
                    ID = p.CODEMPID,
                    Name = p.Text
                }).OrderBy(x => x.Name).ToList();

                IEnumerable<UsersTransfer> user = model.UsersTransferList;
                model.SelectUsersTransferList = new SelectList(user, "ID", "Name");


                if (docno == "" || docno == null || docno.Substring(0, 1) == "D")
                {
                    var flow = (from p in _DocumentContext.ApprovalFlow
                                join e in _DocumentContext.ApprovalItem
                    on new { p.ApprovalItemCode }
                    equals new { e.ApprovalItemCode }
                                where p.FlowId == code
                                select new CheckBoxFlow
                                {
                                    FlowID = p.FlowId.Trim(),
                                    SeqNo = p.SeqNo,
                                    RoleID = p.RoleId.Trim(),
                                    ApprovalItemCode = p.ApprovalItemCode,
                                    ApprovalItemNameE = e.ApprovalItemNameE,
                                    ApprovalItemNameT = e.ApprovalItemNameT,
                                    ApprovalItemNameJ = e.ApprovalItemNameJ,
                                    Requirement = p.Requirement,
                                    SkipFlag = false,
                                    ApprovalDate = "",
                                    RequirementRemark = p.RequirementRemark,
                                }).OrderBy(i => i.SeqNo).ToList();
                    model.CheckBoxFlows = flow;

                }
                else
                {

                    var Data = _DocumentContext.DocumentItemProgress.Where(e => e.DocumentNo == docno && e.DocumentCode == code)
                        .Select(e => new { e.DocumentCode, e.SeqNo, e.RoleId, e.ApprovalItemCode, e.ApprovalItemNameE, e.ApprovalItemNameT, e.ApprovalItemNameJ, e.Requirement, e.SkipFlag, e.RequirementRemark }).Distinct().ToList();

                    model.CheckBoxFlows = Data.ToList().Select(e => new CheckBoxFlow()
                    {
                        FlowID = e.DocumentCode.Trim(),
                        SeqNo = e.SeqNo,
                        RoleID = e.RoleId.Trim(),
                        ApprovalItemCode = e.ApprovalItemCode,
                        ApprovalItemNameE = e.ApprovalItemNameE,
                        ApprovalItemNameT = e.ApprovalItemNameT,
                        ApprovalItemNameJ = e.ApprovalItemNameJ,
                        Requirement = e.Requirement,
                        SkipFlag = Convert.ToBoolean(e.SkipFlag),
                        ////ApprovalDate = e.ApprovalDate == null ? "" : Convert.ToString(e.ApprovalDate),
                        ApprovalDate = "",
                        RequirementRemark = e.RequirementRemark,
                    }).OrderBy(i => i.SeqNo).ToList();
                }



                if (docno == "" || docno == null)
                {

                    model.vewDocumentItemList = dp.GetvewDocumentItemList(code, docno ?? "").OrderBy(x => x.InputItemListDisplayOrder).ToList();

                    var value = model.vewDocumentItemList.Where(x => x.ValueCode != "" && x.ValueCode != null).OrderBy(x => x.InputItemListDisplayOrder).ToList();

                    List<SelectListItem> SelectListItem = new List<SelectListItem> { };
                    List<CheckBoxListItem> CheckBoxListItem = new List<CheckBoxListItem> { };
                    List<RadioListItem> RadioListItem = new List<RadioListItem> { };
                    List<RadioListItem> CheckSpeItem = new List<RadioListItem> { };

                    foreach (var itema in value)
                    {

                        var valuechk = _DocumentContext.ValueList.Where(x => x.ValueCode.Trim() == itema.ValueCode.Trim()).OrderBy(x => x.ValueCode == itema.ValueCode).ThenBy(n => n.DisplayOrder).ToList();
                        foreach (var itemval in valuechk)
                        {
                            var text = "";
                            var checkL = false;
                            string[] words = itema.Language.Split(',');
                            foreach (string word in words)
                            {
                                var W = word.Trim().ToUpper();


                                if (W.Contains("T"))
                                {
                                    if (itemval.ValueE != itemval.ValueT || checkL == false)
                                    {
                                        text = text + " " + itemval.ValueT;
                                        checkL = true;
                                    }

                                }
                                else if (W.Contains("E"))
                                {
                                    if (itemval.ValueE != itemval.ValueT || checkL == false)
                                    {
                                        text = text + " " + itemval.ValueE;
                                        checkL = true;
                                    }

                                }
                                else if (W.Contains("J"))
                                {
                                    text = text + " " + itemval.ValueJ;

                                }

                            }



                            if (itema.InputType == "combo")
                            {
                                SelectListItem.Add(new SelectListItem
                                {
                                    Value = Convert.ToString(itemval.Id),
                                    Text = text.Trim(),
                                    Code = itemval.ValueCode.Trim(),
                                    IsChecked = false,
                                    Order = itemval.DisplayOrder,
                                    DocumentCode = itema.DocumentCode,
                                    ItemCateg = itema.ItemCateg,
                                    DocumentNameE = itema.DocumentNameE,
                                    DocumentNameT = itema.DocumentNameT,
                                    DocumentNameJ = itema.DocumentNameJ,
                                    ItemCode = itema.ItemCode,
                                    ItemNameE = itema.ItemNameE,
                                    ItemNameT = itema.ItemNameT,
                                    ItemNameJ = itema.ItemNameJ
                                });
                            }
                            else if (itema.InputType == "checkbox")
                            {
                                CheckBoxListItem.Add(new CheckBoxListItem
                                {
                                    Display = text.Trim(),
                                    ValueCode = itemval.ValueCode,
                                    ID = itemval.Id,
                                    IsChecked = false,
                                    DocumentCode = itema.DocumentCode,
                                    ItemCateg = itema.ItemCateg,
                                    DocumentNameE = itema.DocumentNameE,
                                    DocumentNameT = itema.DocumentNameT,
                                    DocumentNameJ = itema.DocumentNameJ,
                                    ItemCode = itema.ItemCode,
                                    ItemNameE = itema.ItemNameE,
                                    ItemNameT = itema.ItemNameT,
                                    ItemNameJ = itema.ItemNameJ
                                });
                            }
                            else if (itema.InputType == "cbandtxt")
                            {
                                CheckBoxListItem.Add(new CheckBoxListItem
                                {
                                    Display = text.Trim(),
                                    ValueCode = itemval.ValueCode,
                                    ID = itemval.Id,
                                    IsChecked = false,
                                    DocumentCode = itema.DocumentCode,
                                    ItemCateg = itema.ItemCateg,
                                    DocumentNameE = itema.DocumentNameE,
                                    DocumentNameT = itema.DocumentNameT,
                                    DocumentNameJ = itema.DocumentNameJ,
                                    ItemCode = itema.ItemCode,
                                    ItemNameE = itema.ItemNameE,
                                    ItemNameT = itema.ItemNameT,
                                    ItemNameJ = itema.ItemNameJ
                                });
                            }
                            else if (itema.InputType == "radio")
                            {
                                RadioListItem.Add(new RadioListItem
                                {
                                    Display = text.Trim(),
                                    Order = itemval.DisplayOrder,
                                    ValueCode = itemval.ValueCode,
                                    ID = itemval.Id.Trim(),
                                    IsChecked = false,
                                    DocumentCode = itema.DocumentCode,
                                    ItemCateg = itema.ItemCateg,
                                    DocumentNameE = itema.DocumentNameE,
                                    DocumentNameT = itema.DocumentNameT,
                                    DocumentNameJ = itema.DocumentNameJ,
                                    ItemCode = itema.ItemCode,
                                    ItemNameE = itema.ItemNameE,
                                    ItemNameT = itema.ItemNameT,
                                    ItemNameJ = itema.ItemNameJ
                                });
                            }
                            else if (itema.InputType == "checkspe")
                            {
                                CheckSpeItem.Add(new RadioListItem
                                {
                                    Display = text.Trim(),
                                    Order = itemval.DisplayOrder,
                                    ValueCode = itemval.ValueCode,
                                    ID = itemval.Id.Trim(),
                                    IsChecked = false,
                                    DocumentCode = itema.DocumentCode,
                                    ItemCateg = itema.ItemCateg,
                                    DocumentNameE = itema.DocumentNameE,
                                    DocumentNameT = itema.DocumentNameT,
                                    DocumentNameJ = itema.DocumentNameJ,
                                    ItemCode = itema.ItemCode,
                                    ItemNameE = itema.ItemNameE,
                                    ItemNameT = itema.ItemNameT,
                                    ItemNameJ = itema.ItemNameJ
                                });
                            }

                        }
                    };

                    model.GroupDopdown = SelectListItem.ToList();
                    model.Groupcheckbox = CheckBoxListItem.ToList();
                    model.Groupradio = RadioListItem.ToList();
                    model.Groupspecial = CheckSpeItem.ToList();
                }
                else
                {

                    model.vewDocumentItemList = dp.GetvewDocumentItemList(code, docno).OrderBy(x => x.InputItemListDisplayOrder).ToList();
                    var value = model.vewDocumentItemList.Where(x => x.valueold != "" && x.valueold != null).OrderBy(x => x.InputItemListDisplayOrder).ToList();

                    List<SelectListItem> SelectListItem = new List<SelectListItem> { };
                    List<CheckBoxListItem> CheckBoxListItem = new List<CheckBoxListItem> { };
                    List<RadioListItem> RadioListItem = new List<RadioListItem> { };
                    List<RadioListItem> CheckSpeItem = new List<RadioListItem> { };

                    foreach (var itema in value)
                    {
                        List<Valuelistinput> valuelistinput = new List<Valuelistinput> { };

                        string authors = itema.ValueCode.Trim();
                        string[] authorsList = authors.Split(",");

                        foreach (string author in authorsList)
                        {
                            valuelistinput.Add(new Valuelistinput
                            {
                                Text = author
                            });
                        }

                        var Valuechk = _DocumentContext.ValueList.Where(x => x.ValueCode.Trim() == itema.valueold.Trim()).OrderBy(x => x.ValueCode == itema.valueold).ThenBy(n => n.DisplayOrder).ToList();
                        foreach (var itemval in Valuechk)
                        {
                            var text = "";
                            var checkL = false;
                            string[] words = itema.Language.Split(',');
                            foreach (string word in words)
                            {
                                var W = word.Trim().ToUpper();


                                if (W.Contains("T"))
                                {
                                    if (itemval.ValueE != itemval.ValueT || checkL == false)
                                    {
                                        text = text + " " + itemval.ValueT;
                                        checkL = true;
                                    }
                                }
                                else if (W.Contains("E"))
                                {
                                    if (itemval.ValueE != itemval.ValueT || checkL == false)
                                    {
                                        text = text + " " + itemval.ValueE;
                                        checkL = true;
                                    }
                                }
                                else if (W.Contains("J"))
                                {
                                    text = text + " " + itemval.ValueJ;

                                }
                            }

                            if (itema.InputType == "combo")
                            {
                                bool checkedinput = false;
                                var maptext = valuelistinput.Where(x => x.Text.Trim() == itemval.Id).FirstOrDefault();
                                if (maptext != null)
                                {
                                    checkedinput = true;
                                }
                                SelectListItem.Add(new SelectListItem
                                {
                                    Value = itemval.Id,
                                    Text = text.Trim(),
                                    Code = itemval.ValueCode.Trim(),
                                    IsChecked = checkedinput,
                                    Order = itemval.DisplayOrder,
                                    DocumentCode = itema.DocumentCode,
                                    ItemCateg = itema.ItemCateg,
                                    DocumentNameE = itema.DocumentNameE,
                                    DocumentNameT = itema.DocumentNameT,
                                    DocumentNameJ = itema.DocumentNameJ,
                                    ItemCode = itema.ItemCode,
                                    ItemNameE = itema.ItemNameE,
                                    ItemNameT = itema.ItemNameT,
                                    ItemNameJ = itema.ItemNameJ
                                });
                            }
                            else if (itema.InputType == "checkbox")
                            {
                                bool checkedinput = false;
                                var maptext = valuelistinput.Where(x => x.Text.Trim() == Convert.ToString(itemval.Id)).FirstOrDefault();
                                if (maptext != null)
                                {
                                    checkedinput = true;
                                }

                                CheckBoxListItem.Add(new CheckBoxListItem
                                {
                                    Display = text.Trim(),
                                    ID = itemval.Id.Trim(),
                                    IsChecked = checkedinput,
                                    ValueCode = itemval.ValueCode,
                                    DocumentCode = itema.DocumentCode,
                                    ItemCateg = itema.ItemCateg,
                                    DocumentNameE = itema.DocumentNameE,
                                    DocumentNameT = itema.DocumentNameT,
                                    DocumentNameJ = itema.DocumentNameJ,
                                    ItemCode = itema.ItemCode,
                                    ItemNameE = itema.ItemNameE,
                                    ItemNameT = itema.ItemNameT,
                                    ItemNameJ = itema.ItemNameJ
                                });
                            }
                            else if (itema.InputType == "radio")
                            {
                                bool checkedinput = false;
                                var maptext = valuelistinput.Where(x => x.Text.Trim() == Convert.ToString(itemval.Id)).FirstOrDefault();
                                if (maptext != null)
                                {
                                    checkedinput = true;
                                }
                                RadioListItem.Add(new RadioListItem
                                {
                                    Display = text.Trim(),
                                    Order = itemval.DisplayOrder,
                                    ID = itemval.Id.Trim(),
                                    IsChecked = checkedinput,
                                    ValueCode = itemval.ValueCode,
                                    DocumentCode = itema.DocumentCode,
                                    ItemCateg = itema.ItemCateg,
                                    DocumentNameE = itema.DocumentNameE,
                                    DocumentNameT = itema.DocumentNameT,
                                    DocumentNameJ = itema.DocumentNameJ,
                                    ItemCode = itema.ItemCode,
                                    ItemNameE = itema.ItemNameE,
                                    ItemNameT = itema.ItemNameT,
                                    ItemNameJ = itema.ItemNameJ
                                });
                            }
                            else if (itema.InputType == "checkspe")
                            {
                                bool checkedinput = false;
                                var maptext = valuelistinput.Where(x => x.Text.Trim() == Convert.ToString(itemval.Id)).FirstOrDefault();
                                if (maptext != null)
                                {
                                    checkedinput = true;
                                }
                                CheckSpeItem.Add(new RadioListItem
                                {
                                    Display = text.Trim(),
                                    Order = itemval.DisplayOrder,
                                    ID = itemval.Id.Trim(),
                                    IsChecked = checkedinput,
                                    ValueCode = itemval.ValueCode,
                                    DocumentCode = itema.DocumentCode,
                                    ItemCateg = itema.ItemCateg,
                                    DocumentNameE = itema.DocumentNameE,
                                    DocumentNameT = itema.DocumentNameT,
                                    DocumentNameJ = itema.DocumentNameJ,
                                    ItemCode = itema.ItemCode,
                                    ItemNameE = itema.ItemNameE,
                                    ItemNameT = itema.ItemNameT,
                                    ItemNameJ = itema.ItemNameJ
                                });
                            }

                        }
                    };
                    model.GroupDopdown = SelectListItem.ToList();
                    model.Groupcheckbox = CheckBoxListItem.ToList();
                    model.Groupradio = RadioListItem.ToList();
                    model.Groupspecial = CheckSpeItem.ToList();
                }
                return model;
            }
            catch (Exception e)
            {
                var x = e.ToString();
                model = null;
                dp = null;
                //for refresh page 
                return null;
            }
            finally
            {
                model = null;
                dp = null;

            }

        }
        public async Task<List<string>> GetItemMachineProcess()
        {
            return await _DocumentContext.DocumentItem.Where(i => i.DocumentCode == "AC018" && i.DocumentStatus == "Process").Select(i => i.DocumentNo).ToListAsync();
        }
        public async Task<List<ValueList>> GetOperationName()
        {
            var AllList = await _DocumentContext.ValueList.Where(i => i.ValueCode.Trim() == "TP0007").Distinct().OrderBy(i => i.ValueE).ToListAsync();
            return AllList;
        }
        public async Task<dynamic> GetValueList(string ValueCode)
        {
            if (ValueCode == " ")
                return null;

            var listitems = await _DocumentContext.ValueList.Where(i => i.ValueCode == ValueCode).Select(i => new
            {
                id = i.ValueE,
                text = i.ValueE
            }).Distinct().ToListAsync();

            return listitems;
        }
        public dynamic GetSpecialFlow(string id)
        {
            try
            {
                string constr = _configuration.GetConnectionString("DefaultConnection2");
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        DataTable dt = new DataTable();
                        var strStored = "";
                        cmd.Connection = con;
                        con.Open();
                        strStored = "SELECT FlowSetting FROM ApprovalFlowSpecial where DocumentCode = @id";
                        cmd.Parameters.Add(new SqlParameter("@id", id.Trim()));
                        cmd.CommandText = strStored;
                        cmd.CommandType = CommandType.Text;
                        SqlDataAdapter adpterdata = new SqlDataAdapter
                        {
                            SelectCommand = cmd
                        };
                        adpterdata.Fill(dt);
                        con.Close();
                        return dt;
                    }
                }
            }
            catch (Exception e)
            {
                var dsa = e.Message;
                return null;
            }

        }
        public dynamic GetFlowDetail(string id)
        {
            try
            {
                string constr = _configuration.GetConnectionString("DefaultConnection2");
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        DataTable dt = new DataTable();
                        var strStored = "";
                        cmd.Connection = con;
                        con.Open();
                        strStored = "SELECT SeqNo,ApprovalItemNameE FROM Document_Item_Progress where DocumentNo = @id AND SeqNo >1";
                        cmd.Parameters.Add(new SqlParameter("@id", id.Trim()));
                        cmd.CommandText = strStored;
                        cmd.CommandType = CommandType.Text;
                        SqlDataAdapter adpterdata = new SqlDataAdapter
                        {
                            SelectCommand = cmd
                        };
                        adpterdata.Fill(dt);
                        con.Close();
                        return dt;
                    }
                }
            }
            catch (Exception e)
            {
                var dsa = e.Message;
                return null;
            }

        }
        public dynamic GetAllEmp()
        {
            try
            {
                string constr = _configuration.GetConnectionString("DefaultConnection3");
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        DataTable dt = new DataTable();
                        var strStored = "";
                        cmd.Connection = con;
                        con.Open();
                        strStored = "SELECT CODEMPID as id,CODEMPID+':'+NAMEMPE as text FROM HRMS_Employee where INACTIVE IS NULL AND EMPLEVEL >=3";
                        cmd.CommandText = strStored;
                        cmd.CommandType = CommandType.Text;
                        SqlDataAdapter adpterdata = new SqlDataAdapter
                        {
                            SelectCommand = cmd
                        };
                        adpterdata.Fill(dt);
                        con.Close();
                        return dt;
                    }
                }
            }
            catch (Exception e)
            {
                var dsa = e.Message;
                return null;
            }

        }
        public async Task<dynamic> AssignFlowsAsync(string id, List<int> Allseq, List<FlowsPeople> AllPeople, string username)
        {

            try
            {
                string ComputerName = _accessor.HttpContext.Connection.RemoteIpAddress.ToString();


                var allempid = AllPeople.Select(i => i.Empname).Distinct().ToList();

                //ดึงข้อมูลพนักงานทั้งหมดที่อยู่ใน AllPeople
                var empinfo = await _hRMSLocalContext.HrmsEmployee.Where(i => allempid.Contains(i.Codempid)).ToListAsync();

                // ดึง itemprogress ทั้งหมดที่ยังไม่มีการ  Approve และ seq ตรงกับที่ config ไว้
                var itemAll = await _DocumentContext.DocumentItemProgress.Where(i => i.DocumentNo == id && Allseq.Contains(i.SeqNo)).ToListAsync();

                // สร้าง list DocumentItemProgress ไว้สำหรับ Flow ใหม่
                List<DocumentItemProgress> newassign = new List<DocumentItemProgress>();
                foreach (var item in Allseq)
                {

                    //ดึงคนแรกใน seq ขึ้นมาเพื่อเป็นต้นแบบข้อมูล
                    var getseqitem = itemAll.Where(i => i.SeqNo == item).FirstOrDefault();
                    // ดึงคนทั้งหมดที่อยู่ใน Seq นี้
                    var propleinseq = AllPeople.Where(i => i.Seq == item).ToList();
                    if (propleinseq.Count() == 0)
                    {
                        // ลบ item ที่ดึงมาจากดาต้าเบส ในกรณีที่ ไม่มีการ Assign 
                        //var removeitem = itemAll.Where(i => i.SeqNo == item).ToList();
                        //foreach (var _itemremove in removeitem)
                        //{
                        //    itemAll.Remove(_itemremove);
                        //}
                        DocumentItemProgress newpeopleassign = new DocumentItemProgress()
                        {
                            //newpeopleassign = getseqitem;
                            DocumentNo = getseqitem.DocumentNo,
                            SeqNo = getseqitem.SeqNo,
                            DocumentCode = getseqitem.DocumentCode,
                            DocumentNameE = getseqitem.DocumentNameE,
                            DocumentNameT = getseqitem.DocumentNameT,
                            DocumentNameJ = getseqitem.DocumentNameJ,
                            RoleId = "ALL00",
                            ApprovalItemCode = getseqitem.ApprovalItemCode,
                            ApprovalItemNameE = getseqitem.ApprovalItemNameE,
                            ApprovalItemNameT = getseqitem.ApprovalItemNameT,
                            ApprovalItemNameJ = getseqitem.ApprovalItemNameJ,
                            Requirement = getseqitem.Requirement,
                            RequirementRemark = getseqitem.RequirementRemark,
                            SkipFlag = true,
                            AssignFlag = "0",
                            AssignFlagRemark = "AND",
                            ApprovalDate = null,
                            ApproverOperatorId = "",
                            ApproverOperatorName = null,
                            ApproverOperatorSign = getseqitem.ApproverOperatorSign,
                            Reject = getseqitem.Reject,
                            AddDate = DateTime.Now,
                            UpdDate = DateTime.Now,
                            UserName = username,
                            ComputerName = ComputerName,
                            AssignFlagBySeq = getseqitem.AssignFlagBySeq
                        };
                        newassign.Add(newpeopleassign);
                    }
                    else
                    {


                        //ทำการสร้างไอเทมใหม่จากที่ copy มา
                        if (getseqitem != null)
                        {
                            foreach (var itempeopleassign in propleinseq)
                            {
                                var thisempinfo = empinfo.Where(i => i.Codempid == itempeopleassign.Empname).FirstOrDefault();
                                DocumentItemProgress newpeopleassign = new DocumentItemProgress()
                                {
                                    //newpeopleassign = getseqitem;
                                    DocumentNo = getseqitem.DocumentNo,
                                    SeqNo = getseqitem.SeqNo,
                                    DocumentCode = getseqitem.DocumentCode,
                                    DocumentNameE = getseqitem.DocumentNameE,
                                    DocumentNameT = getseqitem.DocumentNameT,
                                    DocumentNameJ = getseqitem.DocumentNameJ,
                                    RoleId = "ALL00",
                                    ApprovalItemCode = getseqitem.ApprovalItemCode,
                                    ApprovalItemNameE = getseqitem.ApprovalItemNameE,
                                    ApprovalItemNameT = getseqitem.ApprovalItemNameT,
                                    ApprovalItemNameJ = getseqitem.ApprovalItemNameJ,
                                    Requirement = getseqitem.Requirement,
                                    RequirementRemark = getseqitem.RequirementRemark,
                                    SkipFlag = false,
                                    AssignFlag = "0",
                                    AssignFlagRemark = "AND",
                                    ApprovalDate = null,
                                    ApproverOperatorId = itempeopleassign.Empname,
                                    ApproverOperatorName = thisempinfo.Namempe,
                                    ApproverOperatorSign = getseqitem.ApproverOperatorSign,
                                    Reject = getseqitem.Reject,
                                    AddDate = DateTime.Now,
                                    UpdDate = DateTime.Now,
                                    UserName = username,
                                    ComputerName = ComputerName,
                                    AssignFlagBySeq = getseqitem.AssignFlagBySeq
                                };
                                newassign.Add(newpeopleassign);
                            }

                        }
                    }


                    //ลบ item ที่มีการ Approved ไปแล้วออกจาก List ที่จะทำการลบทิ้ง
                    var removeitem = itemAll.Where(i => i.SeqNo == item && i.ApprovalDate != null).ToList();
                    foreach (var _itemremove in removeitem)
                    {
                        itemAll.Remove(_itemremove);
                    }




                }

                if (newassign.Count() != 0)
                {
                    _DocumentContext.DocumentItemProgress.RemoveRange(itemAll);
                    _DocumentContext.SaveChanges();
                    //await _DocumentContext.DocumentItemProgress.AddRangeAsync(newassign);
                    //await _DocumentContext.SaveChangesAsync();

                    foreach (var item in newassign)
                    {
                        _DocumentContext.DocumentItemProgress.Add(item);
                        await _DocumentContext.SaveChangesAsync();
                    }

                }
            }
            catch (Exception e)
            {
                var Data1 = new { status = false, subject = "ASSIGN FLOW", detail = e.Message };
                return Data1;
            }

            var Data = new { status = true, subject = "ASSIGN FLOW", detail = "ASSIGN FLOWS COMPLETE." };
            return Data;
        }
        public dynamic GetMyDoc(string UserName)
        {

            //MyDoc
            //var MyDocument1 = _DocumentContext.DocumentItem.Where(i => i.ReqOperatorId.Trim() == UserName.Trim()).ToList();
            ConnDoc dp = new ConnDoc(_configuration);
            var MyDocument1 = dp.GetMyDocument(UserName).ToList();

            var MyDocument = MyDocument1.Select(p => new
            {
                DocumentNo = p.DocumentNo.Trim(),
                DocumentCode = p.DocumentCode.ToString().Trim(),
                DocumentNameE = p.DocumentNameE.Trim(),
                DocumentNameT = p.DocumentNameT.Trim(),
                DocumentNameJ = p.DocumentNameJ.Trim(),
                ReqDescription1 = p.ReqDescription1.Trim(),
                ReqOperatorName = p.ReqOperatorName.Trim(),
                p.IssuedDate,
                p.IssuedDateChange,
                DocumentStatus = p.DocumentStatus.Trim() ?? "",
                p.NextApprove,
                p.CountIssuedDate,
                p.YearMonth
            }).OrderByDescending(i => i.IssuedDate).ToList();


            return MyDocument;
        }

        public dynamic GetMyApproved(string UserName)
        {

            //MyDoc
            //var MyDocument1 = _DocumentContext.DocumentItem.Where(i => i.ReqOperatorId.Trim() == UserName.Trim()).ToList();
            ConnDoc dp = new ConnDoc(_configuration);
            var MyApproved = dp.GetMyApproved(UserName).ToList();

            var MyAppr = MyApproved.Select(p => new
            {
                DocumentNo = p.DocumentNo.Trim(),
                DocumentCode = p.DocumentCode.ToString().Trim(),
                DocumentNameE = p.DocumentNameE.Trim(),
                ReqDescription1 = p.ReqDescription1.Trim(),
                ReqOperatorName = p.ReqOperatorName.Trim(),
                p.IssuedDate,
                p.IssuedDateChange,
                DocumentStatus = p.DocumentStatus.Trim() ?? "",
                p.ApprovalDate,
                p.ApprovalDateChange,
                p.YearMonth,
                p.FlagUpdDate
            }).ToList().OrderByDescending(i => i.YearMonth).ThenByDescending(i => i.ApprovalDate);
            return MyAppr;
        }


        public dynamic GetGroupDoc(string GroupCateg, string UserName)
        {
            ConnDoc dp = new ConnDoc(_configuration);
            var GroupDoc = dp.GetGroupIssueDocument(GroupCateg, UserName).ToList();
            //GroupIssue
            var GroupIssueDocument = GroupDoc.OrderByDescending(i => i.IssuedDate).Select(x => new
            {
                DocumentNo = x.DocumentNo.Trim(),
                DocumentCode = x.DocumentCode.Trim(),
                DocumentNameE = x.DocumentNameE.Trim(),
                DocumentNameT = x.DocumentNameT.Trim(),
                DocumentNameJ = x.DocumentNameJ.Trim(),
                ReqDescription1 = x.ReqDescription1.Trim(),
                ReqOperatorName = x.ReqOperatorName.Trim(),
                x.IssuedDate,
                x.IssuedDateChange,
                DocumentStatus = x.DocumentStatus.Trim() ?? "",
                x.FlagUpdDate,
                x.ApprovalDate,
                x.ApproverOperatorName,
                x.CountIssuedDate,
                x.YearMonth,
            }).ToList();


            return GroupIssueDocument;
        }
        //------------------------------------End Get Method--------------------------------------------------------------------------------------//



        //------------------------------------Insert Method--------------------------------------------------------------------------------------//
        public async Task<dynamic> Attachfile(List<IFormFile> files, string DocNo, string DocCode, string Addressfile, string Username)
        {
            if (files != null && files.Count > 0)
            {
                Addressfile = Addressfile == null ? "" : Addressfile.Trim();
                var CreateBy = Username;
                string ComputerName = _accessor.HttpContext.Connection.RemoteIpAddress.ToString();

                foreach (IFormFile item in files)
                {

                    //string fileName = ContentDispositionHeaderValue.Parse(item.ContentDisposition).FileName.Trim('"').Replace(" ", "");
                    string fileName = ContentDispositionHeaderValue.Parse(item.ContentDisposition).FileName.Trim('"');
                    AttachFile attfile = _DocumentContext.AttachFile.Where(p => p.DocumentNo == DocNo && p.FileName == fileName).FirstOrDefault();
                    if (attfile != null)
                    {
                        //have
                        attfile.FileName = fileName;
                        attfile.Path = Addressfile == "" ? "../Attach/" + attfile.DocumentCode.Trim() + "/" + attfile.DocumentNo.Trim() + "/" + fileName : Addressfile + fileName;
                        attfile.UpdDate = DateTime.Now;
                        attfile.UserName = CreateBy;
                        attfile.ComputerName = ComputerName;

                        if (Addressfile == "")
                        {
                            System.IO.File.Delete(_hostingEnvironment.WebRootPath + "/Attach/" + attfile.DocumentCode.Trim() + "/" + attfile.DocumentNo.Trim() + "/" + attfile.FileName);

                            string webRootPath = _hostingEnvironment.WebRootPath + "\\Attach\\" + DocCode;
                            string newPath = Path.Combine(webRootPath, DocNo);
                            if (!Directory.Exists(newPath))
                            {
                                Directory.CreateDirectory(newPath);
                            }
                            string extension = Path.GetExtension(item.FileName);
                            string fullPath = Path.Combine(newPath, fileName);
                            using (var stream = new FileStream(fullPath, FileMode.Create))
                            {
                                item.CopyTo(stream);

                            }
                            //_logger.LogTrace(CreateBy + " Add file >>> " + attfile.FileName + " >> docNo: " + DocNo);


                        }


                        ////send mail to username old
                        //var _UserIDOLD = new SqlParameter("@UserIDOLD", attfile.UserName);
                        //var _UserIDNEW = new SqlParameter("@UserIDNEW", CreateBy);
                        //var _DocumentNo = new SqlParameter("@DocumentNo", DocNo);
                        //var _FileName = new SqlParameter("@FileName", fileName);

                        ////Sent to store
                        //var sql = "EXEC dbo.sprFormSaveMode @UserIDOLD,@UserIDNEW,@DocumentNo,@FileName";
                        //await _DocumentContext.Database.ExecuteSqlCommandAsync(sql, _UserIDOLD, _UserIDNEW, _DocumentNo, _FileName);



                    }
                    else
                    {
                        //new
                        if (Addressfile == "")
                        {
                            string webRootPath = _hostingEnvironment.WebRootPath + "\\Attach\\" + DocCode;
                            string newPath = Path.Combine(webRootPath, DocNo);
                            if (!Directory.Exists(newPath))
                            {
                                Directory.CreateDirectory(newPath);
                            }
                            string extension = Path.GetExtension(item.FileName);
                            string fullPath = Path.Combine(newPath, fileName);
                            using (var stream = new FileStream(fullPath, FileMode.Create))
                            {
                                item.CopyTo(stream);

                            }
                            //_logger.LogTrace(CreateBy + " Add file >>> " + attfile.FileName + " >> docNo: " + DocNo);
                        }
                        else
                        {
                            string webRootPath = _hostingEnvironment.WebRootPath + "\\Attach\\" + DocCode;
                            string newPath = Path.Combine(webRootPath, DocNo);
                            if (!Directory.Exists(newPath))
                            {
                                Directory.CreateDirectory(newPath);
                            }
                        }
                        var new_attfile = new AttachFile()
                        {
                            DocumentNo = DocNo,
                            DocumentCode = DocCode,
                            FileName = fileName,
                            Path = Addressfile == "" ? "../Attach/" + DocCode + "/" + DocNo + "/" + fileName : Addressfile + fileName,
                            AddDate = DateTime.Now,
                            UpdDate = DateTime.Now,
                            UserName = CreateBy,
                            ComputerName = ComputerName

                        };
                        _DocumentContext.AttachFile.Add(new_attfile);
                        //_logger.LogTrace(CreateBy + " Add file >>> " + attfile.FileName + " >> docNo: " + DocNo);
                    }

                    try
                    {
                        _DocumentContext.SaveChanges();

                    }
                    catch (Exception e)
                    {
                        var Data1 = new { status = false, subject = "Add Attach File", detail = e.Message };
                        return Data1;
                    }
                }
            }
            await SetCountPageAttachfileAsync();
            var Data = new { status = true, subject = "Add Attach File", detail = "Attach file complete." };
            return Data;
        }
        public dynamic UploadImage(List<IFormFile> files, string DocNo, string DocCode, string ItemCode, string Username)
        {
            var CreateBy = Username;
            string ComputerName = _accessor.HttpContext.Connection.RemoteIpAddress.ToString();
            if (files != null && files.Count > 0)
            {
                var destinationSize = 480;
                foreach (IFormFile item in files)
                {

                    string fileName = ContentDispositionHeaderValue.Parse(item.ContentDisposition).FileName.Trim('"');
                    DocumentItemDetail attfile = _DocumentContext.DocumentItemDetail.Where(p => p.DocumentNo == DocNo && p.ItemCode == ItemCode).FirstOrDefault();
                    if (attfile != null)
                    {
                        if (!string.IsNullOrEmpty(attfile.FinalResult))
                        {
                            if (Directory.Exists(_hostingEnvironment.WebRootPath + "/Attach/" + attfile.DocumentCode.Trim() + "/" + attfile.DocumentNo.Trim() + "/" + ItemCode.Trim()))
                            {
                                DirectoryInfo di = new DirectoryInfo(_hostingEnvironment.WebRootPath + "/Attach/" + attfile.DocumentCode.Trim() + "/" + attfile.DocumentNo.Trim() + "/" + ItemCode.Trim());
                                foreach (FileInfo file in di.GetFiles())
                                {
                                    file.Delete();
                                }
                            }

                        }

                        attfile.FinalResult = fileName;
                        attfile.UpdDate = DateTime.Now;
                        attfile.UserName = CreateBy;
                        attfile.ComputerName = ComputerName;

                        string webRootPath = _hostingEnvironment.WebRootPath + "\\Attach\\" + attfile.DocumentCode.Trim() + "\\" + attfile.DocumentNo.Trim();
                        string newPath = Path.Combine(webRootPath, ItemCode.Trim());
                        if (!Directory.Exists(newPath))
                        {
                            Directory.CreateDirectory(newPath);
                        }
                        string extension = Path.GetExtension(item.FileName);
                        string fullPath = Path.Combine(newPath, fileName);
                        using (var destinationImage = new Bitmap(destinationSize, destinationSize))
                        {
                            using (var graphics = Graphics.FromImage(destinationImage))
                            {
                                graphics.Clear(Color.White);
                                using (var sourceImage = Image.FromStream(item.OpenReadStream(), true, true))
                                {
                                    var s = Math.Max(sourceImage.Width, sourceImage.Height);
                                    var w = destinationSize * sourceImage.Width / s;
                                    var h = destinationSize * sourceImage.Height / s;
                                    var x = (destinationSize - w) / 2;
                                    var y = (destinationSize - h) / 2;

                                    // Use alpha blending in case the source image has transparencies.
                                    graphics.CompositingMode = CompositingMode.SourceOver;

                                    // Use high quality compositing and interpolation.
                                    graphics.CompositingQuality = CompositingQuality.HighQuality;
                                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

                                    graphics.DrawImage(sourceImage, x, y, w, h);
                                }
                            }
                            destinationImage.Save(fullPath, System.Drawing.Imaging.ImageFormat.Jpeg);
                        }
                    }
                    try
                    {
                        _DocumentContext.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        var Data1 = new { status = false, subject = "Upload Image", detail = e.Message };
                        return Data1;
                    }
                }
            }
            var Data = new { status = true, subject = "Upload Image", detail = "Upload Image complete." };
            return Data;
        }
        public dynamic AddRoleMaster(string remarks, string Username, string GroupCateg)
        {
            try
            {
                string ComputerName = _accessor.HttpContext.Connection.RemoteIpAddress.ToString(); ;

                var maxid = _DocumentContext.Role.Where(x => x.RoleId.Contains("SP")).Select(x => x.RoleId).DefaultIfEmpty("SP000").Max();

                var roleid = "SP001";
                if (maxid != null && maxid != "")
                {
                    var id = Convert.ToString(Convert.ToInt32(maxid.Replace("SP", "")) + 1);
                    if (id.Length == 1)
                    {
                        roleid = "SP00" + id;
                    }
                    else if (id.Length == 2)
                    {
                        roleid = "SP0" + id;
                    }
                    else if (id.Length == 3)
                    {
                        roleid = "SP" + id;
                    }
                }

                Role _Role = new Role
                {
                    RoleId = roleid,
                    ApplicationName = GroupCateg,
                    Remarks = remarks,
                    ComputerName = ComputerName,
                    UserName = Username,
                    AddDate = DateTime.Now,
                    UpdDate = DateTime.Now,
                };

                _DocumentContext.Role.Add(_Role);
                _DocumentContext.SaveChanges();
                _logger.LogTrace(Username + " Add Role ID: " + _Role.RoleId);
            }
            catch (Exception e)
            {
                var Data1 = new { status = false, subject = "ADD ROLE", detail = e.Message };
                return Data1;
            }

            var Data = new { status = true, subject = "ADD ROLE", detail = "Add role complete." };

            return Data;
        }
        public dynamic AddUserRoleMaster(string listboxuser, string RoleId, string Username)
        {
            try
            {
                string ComputerName = _accessor.HttpContext.Connection.RemoteIpAddress.ToString();

                string[] authorsList = listboxuser.Split(",");

                foreach (string author in authorsList)
                {
                    OperatorRole operatorRole = new OperatorRole
                    {
                        RoleId = RoleId,
                        OperatorId = author,
                        ComputerName = ComputerName,
                        UserName = Username,
                        AddDate = DateTime.Now,
                        UpdDate = DateTime.Now,
                    };

                    _DocumentContext.OperatorRole.Add(operatorRole);
                    _DocumentContext.SaveChanges();
                    _logger.LogTrace(Username + " Add Emp ID: " + operatorRole.OperatorId + "To Role ID: " + operatorRole.RoleId);
                }
            }
            catch (Exception e)
            {
                var Data1 = new { status = false, subject = "ADD USER", detail = e.Message };
                return Data1;
            }

            var Data = new { status = true, subject = "ADD USER", detail = "Add user complete." };
            return Data;

        }
        public dynamic AddDocMaster(string flowID, string seqNo, string roleID, string roleIDold, string requirement,
            string requirementRemark, string assignFlag, string assignFlagRemark, string Username, string assignFlagBySeq, string reject)
        {
            try
            {
                string ComputerName = _accessor.HttpContext.Connection.RemoteIpAddress.ToString();

                string constr = _configuration.GetConnectionString("DefaultConnection2");
                //SqlConnection conn = new SqlConnection(constr);
                //SqlCommand objCmd = new SqlCommand();                
                using (SqlConnection conn = new SqlConnection(constr))
                {
                    var assignFlagBySeqX = "";
                    if (assignFlagBySeq == null)
                    {
                        assignFlagBySeqX = "NULL";
                    }
                    else
                    {
                        assignFlagBySeqX = "'" + assignFlagBySeq + "'";
                    }

                    var rejectX = "";
                    if (reject == null)
                    {
                        rejectX = "NULL";
                    }
                    else
                    {
                        rejectX = "'" + reject + "'";
                    }

                    var requirementRemarkX = "";
                    if (requirementRemark == null)
                    {
                        requirementRemarkX = "NULL";
                    }
                    else
                    {
                        requirementRemarkX = "'" + requirementRemark + "'";
                    }

                    using (SqlCommand objCmd = new SqlCommand())
                    {
                        var strStored = "";

                        strStored = " UPDATE ApprovalFlow  SET RoleID = '" + roleID + "', Requirement = '" + requirement + "', RequirementRemark = " + requirementRemarkX +
                        " , AssignFlag = '" + assignFlag + "' , AssignFlagRemark = '" + assignFlagRemark + "' ,  UserName = '" + Username +
                        "' , ComputerName = '" + ComputerName + "', UpdDate = convert(date, '" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "')" +
                        " , AssignFlagBySeq = " + assignFlagBySeqX + " , Reject = " + rejectX + "" +
                        " Where FlowID = '" + flowID + "' and SeqNo = " + seqNo + " and RoleID = '" + roleIDold + "'";
                        conn.Open();
                        objCmd.Connection = conn;
                        objCmd.CommandText = strStored;
                        objCmd.ExecuteNonQueryAsync();
                        conn.Close();
                    }
                }



            }
            catch (Exception e)
            {
                var Data1 = new { status = false, subject = "EDIT FLOW", detail = e.Message };
                return Data1;
            }

            var Data = new { status = true, subject = "EDIT FLOW", detail = "Edit flow complete." };
            return Data;
        }


        //------------------------------------End Insert Method--------------------------------------------------------------------------------------//


        //------------------------------------Update Method--------------------------------------------------------------------------------------//
        public dynamic UpdateRoleMaster(Role role, string Username, string GroupCateg)
        {
            string ComputerName = _accessor.HttpContext.Connection.RemoteIpAddress.ToString(); ;
            Role _Ctrl = _DocumentContext.Role.Where(p => p.RoleId == role.RoleId).FirstOrDefault();

            if (_Ctrl != null)
            {
                _Ctrl.Remarks = role.Remarks;
                _Ctrl.UpdDate = DateTime.Now;
                _Ctrl.UserName = Username;
                _Ctrl.ComputerName = ComputerName;
                try
                {
                    _DocumentContext.Role.Update(_Ctrl);
                    _logger.LogTrace(Username + "Update Role Name >>> " + _Ctrl.ApplicationName);
                    _DocumentContext.SaveChanges();
                }
                catch (Exception e)
                {
                    var Data1 = new { status = false, subject = "UPDATE ROLE", detail = e.Message };
                    return Data1;
                }
            }
            var Data = new { status = true, subject = "UPDATE ROLE", detail = "Update role complete." };
            if (_Ctrl != null)
            {
                ((IDisposable)_Ctrl).Dispose();
            }
            return Data;
        }
        public async Task<dynamic> AutoIssueMachine(Tuple1 Tuple1, IFormCollection frm, string DocNo, string DocCode, string QueryMode, string Comment,
           string Namebtn, string Idbtn, string Valbtn, string Mode, string signature, string radiochksign, string CreateBy, string CreateName,
           string Division, string Section, string Department, string Department2, string Department3, string GroupCateg)
        {
            DocNo = DocNo ?? "";
            try
            {
                if ((DocNo == "" || DocNo == null || DocNo.Substring(0, 1) == "D") && Tuple1.UserFlowByRole != null && Idbtn == "BTN004") // ���� Confirm                
                {
                    if (Tuple1.UserFlowByRole.Count() > 0)
                    {
                        foreach (var item in Tuple1.UserFlowByRole)
                        {
                            var userselect = item.SelectedValues;
                            if (userselect == null && Tuple1.CheckBoxFlows != null)
                            {
                                var checkSkip = Tuple1.CheckBoxFlows.Where(i => i.SeqNo == item.SeqNo && i.SkipFlag == false).FirstOrDefault();
                                if (checkSkip != null)
                                {
                                    var Data = new { status = false, subject = "Add Item", detail = "Please select user for approve step >> " + item.ApprovalItemNameT + " : " + item.ApprovalItemNameE };
                                    return Data;
                                }
                            }
                            else
                            {
                                if (userselect == null)
                                {
                                    var Data = new { status = false, subject = "Add Item", detail = "Please select user for approve step >> " + item.ApprovalItemNameT + " : " + item.ApprovalItemNameE };
                                    return Data;
                                }
                            }
                        }

                    }
                }

                //var remoteIpAddress = HttpContext.Connection.RemoteIpAddress;
                string ComputerName = _accessor.HttpContext.Connection.RemoteIpAddress.ToString();
                var Subject = "";

                string Return_DocNo = "";

                // Add data item to data table
                var AddItem = AddDocItem(Tuple1, DocNo, DocCode, frm, Subject, CreateBy, ComputerName);
                DataTable dataTable = AddItem.Item1;
                Subject = AddItem.Item2;

                // define datatable for keep tempdata typetable
                DataTable ListTableData = AddListTableData(Tuple1, DocCode, CreateBy, ComputerName);

                // define datatable for keep tempdata typetable
                DataTable ListUserFlow = this.ListUserFlow();
                DataTable ListCondition = this.ListCondition();



                //skip Flow
                var skipflow = "";
                int numflow = 0;
                if (Tuple1.CheckBoxFlows != null)
                {
                    var getvalueflow = Tuple1.CheckBoxFlows.Where(x => x.SkipFlag == true).ToList();
                    foreach (var itemgetvalue in getvalueflow)
                    {
                        if (numflow == 0)
                        {
                            skipflow = Convert.ToString(itemgetvalue.SeqNo);
                        }
                        else
                        {
                            skipflow = skipflow + "," + Convert.ToString(itemgetvalue.SeqNo);
                        }
                        numflow++;
                    }
                }

                //slect user flow

                if ((DocNo == "" || DocNo == null || DocNo.Substring(0, 1) == "D") && Tuple1.UserFlowByRole != null && Idbtn == "BTN004") // ���� Confirm               
                {
                    foreach (var item in Tuple1.UserFlowByRole.Where(x => x.SelectedValues != null))
                    {
                        foreach (var valueuserselect in item.SelectedValues)
                        {
                            var CODEMPID = "";
                            var NAMEMPE = "";
                            string[] authorsList = valueuserselect.Split(" : ");
                            int index = 0;
                            foreach (string author in authorsList)
                            {
                                if (index == 0)
                                {
                                    CODEMPID = author;
                                }
                                else if (index == 1)
                                {
                                    NAMEMPE = author;
                                }
                                index++;
                            }

                            DataRow workRow = ListUserFlow.NewRow();
                            workRow["FlowID"] = item.FlowID == null ? "" : item.FlowID.Trim();
                            workRow["CODEMPID"] = CODEMPID.Trim().ToUpper();
                            workRow["NAMEMPE"] = NAMEMPE.Trim().ToUpper();
                            workRow["SeqNo"] = item.SeqNo;
                            workRow["RoleID"] = item.RoleID ?? "";
                            ListUserFlow.Rows.Add(workRow);
                        }
                    }
                }

                //condition
                foreach (var item in _DocumentContext.DocumentCondition.Where(x => x.DocumentCode == DocCode))
                {
                    DataRow workRow = ListCondition.NewRow();
                    workRow["DocumentNo"] = null;
                    workRow["DocumentCode"] = item.DocumentCode;
                    workRow["Template"] = item.Template;
                    workRow["Design"] = item.Design;
                    workRow["Condition"] = item.Condition;
                    workRow["Value"] = item.Value;
                    ListCondition.Rows.Add(workRow);
                }

                //-----------------Add data and Approve Reject Cancel-------------------------------------------------
                var DocScope = _DocumentContext.Document.Where(i => i.DocumentCode == DocCode).Select(i => i.OpeGroupCateg).FirstOrDefault();
                var TblMulti = new SqlParameter("@TblMulti", SqlDbType.Structured)
                {
                    Value = dataTable,
                    TypeName = "dbo.ItemTableType"
                };

                var TblListTable = new SqlParameter("@TblListTable", SqlDbType.Structured)
                {
                    Value = ListTableData,
                    TypeName = "dbo.TableItemTableType"
                };

                var TblUserFlow = new SqlParameter("@TblUserFlow", SqlDbType.Structured)
                {
                    Value = ListUserFlow,
                    TypeName = "dbo.TableUserFlow"
                };

                var TblCondition = new SqlParameter("@TblCondition", SqlDbType.Structured)
                {
                    Value = ListCondition,
                    TypeName = "dbo.TableCondition"
                };

                var _DeptType = new SqlParameter("@DeptType", DocScope);
                var _DocCode = new SqlParameter("@DocCode", DocCode);
                var _DocNo = new SqlParameter("@DocNo", DocNo);
                var _R_DocNo = new SqlParameter("@U_DocumentNo", SqlDbType.NVarChar, 200)
                {
                    Value = "",
                    Direction = ParameterDirection.Output
                };
                var ErrorMessage = new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, 200)
                {
                    Value = "",
                    Direction = ParameterDirection.Output
                };

                if (Mode == "edit")
                {
                    Valbtn = "E";
                }


                var _Mode = new SqlParameter("@Mode", Valbtn ?? "");
                var _Comment = new SqlParameter("@Comment", Comment ?? "");
                var _Subject = new SqlParameter("@Subject", Subject.Trim());
                var _Skipflow = new SqlParameter("@Skipflow", skipflow);

                //Signature
                var signuser = _context.AspNetUsers.Where(x => x.UserName == CreateBy).FirstOrDefault();
                var _Signature = new SqlParameter("@Signature", radiochksign == "1" ? "" : radiochksign == "2" ? signuser.OperatorSign ?? "" : signature ?? "");

                //Date Approve
                var DateApprove = Tuple1.Dateapprove + " " + DateTime.Now.TimeOfDay;
                var _DateApprove = new SqlParameter("@DateApprove", Convert.ToDateTime(DateApprove).ToString("yyyy-MM-dd HH:mm:ss"));

                //Sent to store
                var sql = "EXEC dbo.sprFormSaveMode @TblCondition,@TblUserFlow,@TblListTable,@TblMulti, @DeptType, @DocNo, @DocCode,@Mode,@Comment,@Subject,@Skipflow,@Signature,@DateApprove, @ErrorMessage OUTPUT, @U_DocumentNo OUTPUT";
                await _DocumentContext.Database.ExecuteSqlCommandAsync(sql, TblCondition, TblUserFlow, TblListTable, TblMulti, _DeptType, _DocNo, _DocCode, _Mode, _Comment, _Subject, _Skipflow, _Signature, _DateApprove, ErrorMessage, _R_DocNo);
                Return_DocNo = _R_DocNo.Value.ToString().Trim();
                _logger.LogInformation(CreateBy + " - " + Valbtn + " DocNo: " + Return_DocNo);

                //Rename Folder file Attach when change from Daft to Release DocNo.
                if (Return_DocNo != DocNo && DocNo != "")
                {

                    //log.Warn(User.Identity.Name + " - " + Mode + " DocNo: " + Return_DocNo);
                    //Rename folder attach doc no
                    string webRootPath = _hostingEnvironment.WebRootPath + "\\Attach\\" + DocCode;
                    string oldPath = Path.Combine(webRootPath, DocNo);
                    string newPath = Path.Combine(webRootPath, Return_DocNo);
                    if (Directory.Exists(oldPath))
                    {
                        Directory.Move(oldPath, newPath);
                        //update database attach doc no                      
                        var some = _DocumentContext.AttachFile.Where(x => x.DocumentNo == DocNo).ToList();
                        some.ForEach(a =>
                        {
                            a.DocumentNo = Return_DocNo.Trim();
                            a.Path = a.Path.Replace(DocNo.Trim(), Return_DocNo.Trim());
                            //a.UpdDate = DateTime.Now;
                            //a.UserName = CreateBy;
                            //a.ComputerName = ComputerName;
                        }
                        );
                        _DocumentContext.AttachFile.UpdateRange(some);
                        _DocumentContext.SaveChanges();

                    }
                }

                //update database attach new doc no    //for backup file attach                   
                var some2 = _DocumentContext.AttachFile.Where(x => x.DocumentNo == Return_DocNo).ToList();
                some2.ForEach(a =>
                {
                    a.UpdDate = DateTime.Now;
                });
                _DocumentContext.AttachFile.UpdateRange(some2);
                _DocumentContext.SaveChanges();

                dataTable.Dispose();
                ListTableData.Dispose();
                ListUserFlow.Dispose();
                ListCondition.Dispose();



                var DataComplete = new { status = true, subject = "Add Item", detail = "Added Successfully!", docno = Return_DocNo };
                return DataComplete;

            }
            catch (SqlException ex)
            {
                var Data = new { status = false, subject = "Add Item", detail = ex.Message.ToString() };
                return Data;

            }
            finally
            {
                // Boardcastnotify();
            }
        }
        public async Task<dynamic> UpdateForm(Tuple1 Tuple1, IFormCollection frm, string DocNo, string DocCode, string QueryMode, string Comment,
         string Namebtn, string Idbtn, string Valbtn, string Mode, string signature, string radiochksign, string CreateBy, string CreateName,
         string Division, string Section, string Department, string Department2, string Department3, string GroupCateg, string Seq)
        {
            DocNo = DocNo ?? "";


            //Function Check Approve



            try
            {
                //if ((DocNo == "" || DocNo == null || DocNo.Substring(0, 1) == "D") && Tuple1.UserFlowByRole != null && Idbtn == "BTN004") // ���� Confirm
                if (Tuple1.UserFlowByRole != null && Idbtn == "BTN004") // ���� Confirm
                {
                    if (Tuple1.UserFlowByRole.Count() > 0)
                    {
                        foreach (var item in Tuple1.UserFlowByRole)
                        {
                            var userselect = item.SelectedValues;
                            if (userselect == null && Tuple1.CheckBoxFlows != null)
                            {
                                var checkSkip = Tuple1.CheckBoxFlows.Where(i => i.SeqNo == item.SeqNo && i.SkipFlag == true).FirstOrDefault();
                                if (checkSkip == null)
                                {
                                    var Data = new { status = false, subject = "Add Item", detail = "Please select user for approve step >> " + item.ApprovalItemNameT + " : " + item.ApprovalItemNameE };
                                    return Data;
                                }
                            }
                            else
                            {
                                if (userselect == null)
                                {
                                    var Data = new { status = false, subject = "Add Item", detail = "Please select user for approve step >> " + item.ApprovalItemNameT + " : " + item.ApprovalItemNameE };
                                    return Data;
                                }
                            }
                        }

                    }
                }

                //var remoteIpAddress = HttpContext.Connection.RemoteIpAddress;
                string ComputerName = _accessor.HttpContext.Connection.RemoteIpAddress.ToString();
                var Subject = "";

                string Return_DocNo = "";

                // Add data item to data table
                var AddItem = AddDocItem(Tuple1, DocNo, DocCode, frm, Subject, CreateBy, ComputerName);
                DataTable dataTable = AddItem.Item1;
                Subject = AddItem.Item2;

                // define datatable for keep tempdata typetable
                DataTable ListTableData = AddListTableData(Tuple1, DocCode, CreateBy, ComputerName);

                // define datatable for keep tempdata typetable
                DataTable ListUserFlow = this.ListUserFlow();
                DataTable ListCondition = this.ListCondition();



                //skip Flow
                var skipflow = "";
                int numflow = 0;
                if (Tuple1.CheckBoxFlows != null)
                {
                    var getvalueflow = Tuple1.CheckBoxFlows.Where(x => x.SkipFlag == true).ToList();
                    foreach (var itemgetvalue in getvalueflow)
                    {
                        if (numflow == 0)
                        {
                            skipflow = Convert.ToString(itemgetvalue.SeqNo);
                        }
                        else
                        {
                            skipflow = skipflow + "," + Convert.ToString(itemgetvalue.SeqNo);
                        }
                        numflow++;
                    }
                }

                //slect user flow

                //if ((DocNo == "" || DocNo == null || DocNo.Substring(0, 1) == "D") && Tuple1.UserFlowByRole != null && Idbtn == "BTN004") // ���� Confirm
                if (Tuple1.UserFlowByRole != null && Idbtn == "BTN004") // ���� Confirm
                {
                    foreach (var item in Tuple1.UserFlowByRole.Where(x => x.SelectedValues != null))
                    {
                        foreach (var valueuserselect in item.SelectedValues)
                        {
                            var CODEMPID = "";
                            var NAMEMPE = "";
                            string[] authorsList = valueuserselect.Split(" : ");
                            int index = 0;
                            foreach (string author in authorsList)
                            {
                                if (index == 0)
                                {
                                    CODEMPID = author;
                                }
                                else if (index == 1)
                                {
                                    NAMEMPE = author;
                                }
                                index++;
                            }

                            DataRow workRow = ListUserFlow.NewRow();
                            workRow["FlowID"] = item.FlowID == null ? "" : item.FlowID.Trim();
                            workRow["CODEMPID"] = CODEMPID.Trim().ToUpper();
                            workRow["NAMEMPE"] = NAMEMPE.Trim().ToUpper();
                            workRow["SeqNo"] = item.SeqNo;
                            workRow["RoleID"] = item.RoleID ?? "";
                            ListUserFlow.Rows.Add(workRow);
                        }
                    }
                }

                //condition
                //foreach (var item in _DocumentContext.DocumentCondition.Where(x => x.DocumentCode == DocCode))
                //{
                //    DataRow workRow = ListCondition.NewRow();
                //    workRow["DocumentNo"] = null;
                //    workRow["DocumentCode"] = item.DocumentCode;
                //    workRow["Template"] = item.Template;
                //    workRow["Design"] = item.Design;
                //    workRow["Condition"] = item.Condition;
                //    workRow["Value"] = item.Value;
                //    ListCondition.Rows.Add(workRow);
                //}

                //-----------------Add data and Approve Reject Cancel-------------------------------------------------
                var DocScope = _DocumentContext.Document.Where(i => i.DocumentCode == DocCode).Select(i => i.OpeGroupCateg).FirstOrDefault();
                var TblMulti = new SqlParameter("@TblMulti", SqlDbType.Structured)
                {
                    Value = dataTable,
                    TypeName = "dbo.ItemTableType"
                };

                var TblListTable = new SqlParameter("@TblListTable", SqlDbType.Structured)
                {
                    Value = ListTableData,
                    TypeName = "dbo.TableItemTableType"
                };

                var TblUserFlow = new SqlParameter("@TblUserFlow", SqlDbType.Structured)
                {
                    Value = ListUserFlow,
                    TypeName = "dbo.TableUserFlow"
                };

                var TblCondition = new SqlParameter("@TblCondition", SqlDbType.Structured)
                {
                    Value = ListCondition,
                    TypeName = "dbo.TableCondition"
                };

                var _DeptType = new SqlParameter("@DeptType", DocScope);
                var _DocCode = new SqlParameter("@DocCode", DocCode);
                var _DocNo = new SqlParameter("@DocNo", DocNo);
                var _R_DocNo = new SqlParameter("@U_DocumentNo", SqlDbType.NVarChar, 200)
                {
                    Value = "",
                    Direction = ParameterDirection.Output
                };
                var ErrorMessage = new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, 200)
                {
                    Value = "",
                    Direction = ParameterDirection.Output
                };

                if (Mode == "edit")
                {
                    Valbtn = "E";
                }


                var _Mode = new SqlParameter("@Mode", Valbtn ?? "");
                var _Comment = new SqlParameter("@Comment", Comment ?? "");
                var _Subject = new SqlParameter("@Subject", Subject.Trim());
                var _Skipflow = new SqlParameter("@Skipflow", skipflow);

                //Signature
                var signuser = _context.AspNetUsers.Where(x => x.UserName == CreateBy).FirstOrDefault();
                var _Signature = new SqlParameter("@Signature", radiochksign == "1" ? "" : radiochksign == "2" ? signuser.OperatorSign ?? "" : signature ?? "");

                //Date Approve
                var DateApprove = Tuple1.Dateapprove + " " + DateTime.Now.TimeOfDay;
                var _DateApprove = new SqlParameter("@DateApprove", Convert.ToDateTime(DateApprove).ToString("yyyy-MM-dd HH:mm:ss"));

                //Sent to store
                var sql = "EXEC dbo.sprFormSaveMode @TblCondition,@TblUserFlow,@TblListTable,@TblMulti, @DeptType, @DocNo, @DocCode,@Mode,@Comment,@Subject,@Skipflow,@Signature,@DateApprove, @ErrorMessage OUTPUT, @U_DocumentNo OUTPUT";
                await _DocumentContext.Database.ExecuteSqlCommandAsync(sql, TblCondition, TblUserFlow, TblListTable, TblMulti, _DeptType, _DocNo, _DocCode, _Mode, _Comment, _Subject, _Skipflow, _Signature, _DateApprove, ErrorMessage, _R_DocNo);
                Return_DocNo = _R_DocNo.Value.ToString().Trim();
                _logger.LogInformation(CreateBy + " - " + Valbtn + " DocNo: " + Return_DocNo);

                //Rename Folder file Attach when change from Daft to Release DocNo.
                if (Return_DocNo != DocNo && DocNo != "" && Return_DocNo != "" && Return_DocNo != null)
                {

                    //log.Warn(User.Identity.Name + " - " + Mode + " DocNo: " + Return_DocNo);
                    //Rename folder attach doc no
                    string webRootPath = _hostingEnvironment.WebRootPath + "\\Attach\\" + DocCode;
                    string oldPath = Path.Combine(webRootPath, DocNo);
                    string newPath = Path.Combine(webRootPath, Return_DocNo);


                    if (Return_DocNo != "")
                    {
                        if (Directory.Exists(newPath))
                        {

                            Directory.Delete(newPath, true);
                        }
                    }


                    if (Directory.Exists(oldPath))
                    {

                        Directory.Move(oldPath, newPath);
                        //update database attach doc no                      
                        var some = _DocumentContext.AttachFile.Where(x => x.DocumentNo == DocNo).ToList();
                        some.ForEach(a =>
                        {
                            a.DocumentNo = Return_DocNo.Trim();
                            a.Path = a.Path.Replace(DocNo.Trim(), Return_DocNo.Trim());
                            //a.UpdDate = DateTime.Now;
                            //a.UserName = CreateBy;
                            //a.ComputerName = ComputerName;
                        }
                        );
                        _DocumentContext.AttachFile.UpdateRange(some);
                        _DocumentContext.SaveChanges();



                    }
                }



                var checkdoccode = _DocumentContext.DocumentUploadPDFStamp.Where(x => x.DocumentCode == DocCode).FirstOrDefault();
                if (checkdoccode != null)
                {
                    StampApproveSPPDF(DocCode, Return_DocNo);
                }

                //update database attach new doc no    //for backup file attach                   
                var some2 = _DocumentContext.AttachFile.Where(x => x.DocumentNo == Return_DocNo).ToList();
                some2.ForEach(a =>
                {
                    a.UpdDate = DateTime.Now;
                });
                _DocumentContext.AttachFile.UpdateRange(some2);
                _DocumentContext.SaveChanges();


                dataTable.Dispose();
                ListTableData.Dispose();
                ListUserFlow.Dispose();
                ListCondition.Dispose();




                var DataComplete = new { status = true, subject = "Add Item", detail = "Added Successfully!", docno = Return_DocNo };
                return DataComplete;

            }
            catch (SqlException ex)
            {
                var Data = new { status = false, subject = "Add Item", detail = ex.Message.ToString() };
                return Data;

            }
            finally
            {
                //Force garbage collection.
                GC.Collect();
                // Wait for all finalizers to complete before continuing.
                GC.WaitForPendingFinalizers();
                //send real time notification  to user
                // Boardcastnotify();
            }
        }



        public async Task<dynamic> TransferForm(string DocNo, string DocCode, string Namebtn, string Idbtn, string Valbtn, string userid, string seq, string OPIDTransfer,
            string Division, string Section, string Department, string Department2, string Department3, string GroupCateg)
        {
            try
            {
                string ComputerName = _accessor.HttpContext.Connection.RemoteIpAddress.ToString();
                var _DocNo = new SqlParameter("@DocNo", DocNo ?? "");
                var _DocCode = new SqlParameter("@DocCode", DocCode ?? "");
                var _Seq = new SqlParameter("@Seq", seq ?? "");
                var _Valbtn = new SqlParameter("@Valbtn", Valbtn ?? "");
                var _Userid = new SqlParameter("@Userid", userid ?? "");
                var _OPIDTransfer = new SqlParameter("@OPIDTransfer", OPIDTransfer ?? "");
                var _ComputerName = new SqlParameter("@ComputerName", ComputerName ?? "");

                var ErrorMessage = new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, 200)
                {
                    Value = "",
                    Direction = ParameterDirection.Output
                };


                //Sent to store
                var sql = "EXEC dbo.sprFormTransferApprove @DocNo,@DocCode,@Seq,@Valbtn,@Userid,@OPIDTransfer,@ComputerName, @ErrorMessage OUTPUT";
                await _DocumentContext.Database.ExecuteSqlCommandAsync(sql, _DocNo, _DocCode, _Seq, _Valbtn, _Userid, _OPIDTransfer, _ComputerName, ErrorMessage);

                _logger.LogInformation(userid + " - " + Valbtn + " DocNo: " + DocNo);




                var DataComplete = new { status = true, subject = "Transfer", detail = "Transfer Successfully!" };
                return DataComplete;
            }
            catch (SqlException ex)
            {
                var Data = new { status = false, subject = "Transfer", detail = ex.Message.ToString() };
                return Data;

            }
            finally
            {
                //send real time notification  to user
                // Boardcastnotify();
            }
        }


        private void StampApproveSPPDF(string DocCode, string DocNo)
        {
            var Allfile = _DocumentContext.AttachFile.Where(x => x.DocumentNo == DocNo).OrderBy(i => i.AddDate).Select(i => i.FileName).ToList();
            if (Allfile.Count >= 1)
            {
                PDFServiceForSPPDF(DocCode, DocNo, Allfile[0].ToString());
            }
        }

        //------------------------------------End Update Method--------------------------------------------------------------------------------------//


        //------------------------------------Delete Method--------------------------------------------------------------------------------------//
        public async Task<dynamic> DeletefileAsync(string DocNo, string Filename, string Username)
        {
            try
            {
                var attfile = _DocumentContext.AttachFile.Where(p => p.DocumentNo.Trim() == DocNo.Trim() && p.No == int.Parse(Filename)).FirstOrDefault();
                if (attfile != null)
                {
                    if (attfile.UserName == Username || attfile.UserName == "SYSTEMP")
                    {
                        _DocumentContext.AttachFile.Remove(attfile);
                        _logger.LogWarning(Username + " >> Delete file >>" + attfile.FileName);
                        await _DocumentContext.SaveChangesAsync();
                        if (attfile.Path.StartsWith("../Attach"))
                        {
                            File.Delete(_hostingEnvironment.WebRootPath + "/Attach/" + attfile.DocumentCode.Trim() + "/" + attfile.DocumentNo.Trim() + "/" + attfile.FileName);
                            _logger.LogTrace(Username + " Delete file >>> " + attfile.FileName);
                        }

                        return new { status = true, subject = "Delete File", detail = "Delete File Complete." };
                    }
                    else
                    {
                        return new { status = false, subject = "Delete File", detail = "Can't delete File." };
                    }

                }
                else
                {
                    var Data = new { status = false, subject = "Delete File", detail = "System is can't delete File." };
                    return Data;
                }
            }
            catch (Exception e)
            {
                var Data1 = new { status = false, subject = "Delete File", detail = e.Message };
                return Data1;
            }
        }
        public async Task<dynamic> DeleteImage(string DocNo, string ItemCode, string Username)
        {
            try
            {
                var CreateBy = Username;
                string ComputerName = _accessor.HttpContext.Connection.RemoteIpAddress.ToString();
                //var attfile = _DocumentContext.AttachFile.Where(p => p.DocumentNo.Trim() == DocNo.Trim() && p.No == int.Parse(Filename) && p.UserName == Username).FirstOrDefault();
                DocumentItemDetail attfile = _DocumentContext.DocumentItemDetail.Where(p => p.DocumentNo == DocNo && p.ItemCode == ItemCode).FirstOrDefault();
                if (attfile != null)
                {

                    if (Directory.Exists(_hostingEnvironment.WebRootPath + "/Attach/" + attfile.DocumentCode.Trim() + "/" + attfile.DocumentNo.Trim() + "/" + ItemCode.Trim()))
                    {
                        DirectoryInfo di = new DirectoryInfo(_hostingEnvironment.WebRootPath + "/Attach/" + attfile.DocumentCode.Trim() + "/" + attfile.DocumentNo.Trim() + "/" + ItemCode.Trim());
                        foreach (FileInfo file in di.GetFiles())
                        {
                            file.Delete();
                        }
                    }
                    attfile.FinalResult = null;
                    attfile.UpdDate = DateTime.Now;
                    attfile.UserName = CreateBy;
                    attfile.ComputerName = ComputerName;
                    await _DocumentContext.SaveChangesAsync();





                    return new { status = true, subject = "Delete Image", detail = "Delete Image Complete." };

                }
                else
                {
                    var Data = new { status = false, subject = "Delete Image", detail = "System is can't delete Image." };
                    return Data;
                }
            }
            catch (Exception e)
            {
                var Data1 = new { status = false, subject = "Delete File", detail = e.Message };
                return Data1;
            }
        }
        public async Task<dynamic> DeleteDraft(string DocNo, string CreateBy, string CreateName, string Division, string Section, string Department, string Department2, string Department3, string GroupCateg)
        {


            try
            {
                var _DocNo = new SqlParameter("@DocNo", DocNo ?? "");
                var sql = "EXEC dbo.sprFormDeleteDraft @DocNo";
                await _DocumentContext.Database.ExecuteSqlCommandAsync(sql, _DocNo);
                var attfile = _DocumentContext.AttachFile.Where(p => p.DocumentNo.Trim() == DocNo.Trim()).ToList();
                if (attfile.Count() != 0)
                {

                    string webRootPath = _hostingEnvironment.WebRootPath + "\\Attach\\" + attfile.First().DocumentCode.Trim() + "\\" + DocNo;
                    Directory.Delete(webRootPath, true);
                    _DocumentContext.AttachFile.RemoveRange(attfile);
                    _DocumentContext.SaveChanges();

                }

                _logger.LogInformation(CreateBy + " Delete Draft No >>> " + DocNo);


            }
            catch (Exception e)
            {
                var Data1 = new { status = false, subject = "Delete Document Draft", detail = e.Message };
                return Data1;
            }
            finally
            {
                //send real time notification  to user
                //Boardcastnotify();
            }
            var Data = new { status = true, subject = "Delete Document Draft", detail = "Delete Complete." };
            return Data;
        }
        public dynamic DeleteRoleMaster(Role role, string Username)
        {
            try
            {
                Role _Ctrl = _DocumentContext.Role.Where(p => p.RoleId == role.RoleId).FirstOrDefault();
                if (_Ctrl != null)
                {
                    _DocumentContext.Role.Remove(_Ctrl);
                    _DocumentContext.SaveChanges();
                    _logger.LogTrace(Username + "Delete Role Name >>> " + _Ctrl.ApplicationName);
                    var Data = new { status = true, subject = "DELETE ROLE", detail = "Delete role complete." };
                    return Data;
                }
                else
                {
                    var Data = new { status = false, subject = "DELETE ROLE", detail = "System is can't delete role." };
                    return Data;
                }
            }
            catch (Exception e)
            {
                var Data1 = new { status = false, subject = "DELETE ROLE", detail = e.Message };
                return Data1;
            }
        }
        public dynamic DeleteUserMaster(OperatorRole user, string Username)
        {
            try
            {
                OperatorRole _Ctrl = _DocumentContext.OperatorRole.Where(p => p.RoleId == user.RoleId && p.OperatorId == user.OperatorId).FirstOrDefault();

                if (_Ctrl != null)
                {
                    _DocumentContext.OperatorRole.Remove(_Ctrl);
                    _DocumentContext.SaveChanges();
                    var Data = new { status = true, subject = "DELETE USER", detail = "Delete user complete." };
                    _logger.LogTrace(Username + " Delete Role user >>> " + _Ctrl.OperatorId + ">> From Role ID: " + _Ctrl.RoleId);
                    return Data;
                }
                else
                {
                    var Data = new { status = false, subject = "DELETE USER", detail = "System is can't delete user." };
                    return Data;
                }
            }
            catch (Exception e)
            {
                var Data1 = new { status = false, subject = "DELETE USER", detail = e.Message };
                return Data1;
            }
        }
        //------------------------------------End Delete Method--------------------------------------------------------------------------------------//


        //------------------------------------SignalR Method--------------------------------------------------------------------------------------//
        //private void Boardcastnotify()
        //{
        //    var all = _userConnectionManager.GetAllUserConnections();
        //    List<string> JobStatus;
        //    ConnDoc dp; 

        //    try
        //    {
        //        dp = new ConnDoc(_configuration);
        //        foreach (var item in all)
        //        {
        //            JobStatus = new List<string>();
        //            var Mode = "";
        //            var thisgroup = _context.AspNetUsers.Where(i => i.UserName == item).FirstOrDefault();
        //            var thisuserdata = _hRMSLocalContext.HrmsEmployee.Where(i => i.Codempid == item).FirstOrDefault();
        //            //var username = User.Identity.Name;

        //            var ApproveDoc = dp.GetWaitApprove(Mode, thisuserdata.Division, thisuserdata.Section, thisuserdata.Department, thisuserdata.Department2, thisuserdata.Department3).ToList();
        //            var EditDoc = dp.GetEditDocument().ToList();
        //            var GroupDoc = dp.GetGroupIssueDocument(thisgroup.GroupCateg).ToList();



        //            //Approve
        //            var ApproveDocument = ApproveDoc.Where(i => (i.CODEMPID == item || (i.ReqOperatorID == item && i.CODEMPID == ""))
        //            && i.DocumentStatus.Trim() != "Complete" && i.DocumentStatus.Trim() != "Cancel" && i.DocumentStatus.Trim() != "Draft").Distinct().OrderByDescending(i => i.IssuedDate).Select(x => new
        //            {
        //                DocumentNo = x.DocumentNo.Trim(),
        //                DocumentCode = x.DocumentCode.Trim(),
        //                DocumentNameE = x.DocumentNameE.Trim(),
        //                DocumentNameT = x.DocumentNameT.Trim(),
        //                DocumentNameJ = x.DocumentNameJ.Trim(),
        //                ReqDescription1 = x.ReqDescription1.Trim(),
        //                ReqOperatorName = x.ReqOperatorName.Trim(),
        //                x.IssuedDate,
        //                DocumentStatus = x.DocumentStatus.Trim() ?? "",
        //                x.SeqNo
        //            }).Distinct().ToList();
        //            var dataApprove = new
        //            {
        //                rows = ApproveDocument
        //            };

        //            //Edit
        //            var EditDocument = EditDoc.Where(i => i.ApproverOperatorID.Trim() == item
        //            && i.DocumentStatus != "Complete" && i.DocumentStatus.Trim() != "Cancel" && i.DocumentStatus.Trim() != "Draft").Distinct().OrderByDescending(i => i.IssuedDate).Select(x => new
        //            {
        //                DocumentNo = x.DocumentNo.Trim(),
        //                DocumentCode = x.DocumentCode.Trim(),
        //                DocumentNameE = x.DocumentNameE.Trim(),
        //                DocumentNameT = x.DocumentNameT.Trim(),
        //                DocumentNameJ = x.DocumentNameJ.Trim(),
        //                ReqDescription1 = x.ReqDescription1.Trim(),
        //                ReqOperatorName = x.ReqOperatorName.Trim(),
        //            //IssuedDate = x.IssuedDate == null ? "" : Convert.ToDateTime(x.IssuedDate).ToString("dd/MM/yy HH:mm"),
        //            x.IssuedDate,
        //                DocumentStatus = x.DocumentStatus.Trim() ?? "",
        //                x.UpdDate,
        //                x.SeqNo

        //            }).Distinct().ToList();
        //            var dataEdit = new
        //            {
        //                rows = EditDocument
        //            };

        //            //Pending Status
        //            JobStatus.Add(ApproveDocument.Distinct().Count().ToString());

        //            //Reject 
        //            JobStatus.Add(ApproveDocument.Where(i => i.DocumentStatus.Trim() == "Reject").Distinct().Count().ToString());


        //            //Over time
        //            JobStatus.Add(ApproveDocument.Where(i => ((DateTime.Now - Convert.ToDateTime(i.IssuedDate)).Days >= 7)).Distinct().Count().ToString());



        //            //MyDoc
        //            var MyDocument = (from p in _DocumentContext.DocumentItem
        //                              where p.ReqOperatorId == item
        //                              select new
        //                              {
        //                                  DocumentNo = p.DocumentNo.Trim(),
        //                                  DocumentCode = p.DocumentCode.ToString().Trim(),
        //                                  DocumentNameE = p.DocumentNameE.Trim(),
        //                                  DocumentNameT = p.DocumentNameT.Trim(),
        //                                  DocumentNameJ = p.DocumentNameJ.Trim(),
        //                                  ReqDescription1 = p.ReqDescription1.Trim(),
        //                                  ReqOperatorName = p.ReqOperatorName.Trim(),
        //                                  p.IssuedDate,
        //                                  DocumentStatus = p.DocumentStatus.Trim() ?? ""
        //                              }).OrderByDescending(i => i.IssuedDate).ToList();
        //            var dataMyDoc = new
        //            {
        //                rows = MyDocument
        //            };
        //            //Pending Status
        //            JobStatus.Add(MyDocument.Where(i => i.DocumentStatus != "Complete" && i.DocumentStatus.Trim() != "Cancel" && i.DocumentStatus.Trim() != "Draft").Distinct().Count().ToString());

        //            //Reject 
        //            JobStatus.Add(MyDocument.Where(i => i.DocumentStatus.Trim() == "Reject").Distinct().Count().ToString());


        //            //Over time
        //            JobStatus.Add(MyDocument.Where(i => i.DocumentStatus != "Complete" && i.DocumentStatus.Trim() != "Cancel" && i.DocumentStatus.Trim() != "Draft"
        //            && ((DateTime.Now - Convert.ToDateTime(i.IssuedDate)).Days >= 7)).Distinct().Count().ToString());


        //            //GroupIssue
        //            var GroupIssueDocument = GroupDoc.Distinct().OrderByDescending(i => i.IssuedDate).Select(x => new
        //            {
        //                DocumentNo = x.DocumentNo.Trim(),
        //                DocumentCode = x.DocumentCode.Trim(),
        //                DocumentNameE = x.DocumentNameE.Trim(),
        //                DocumentNameT = x.DocumentNameT.Trim(),
        //                DocumentNameJ = x.DocumentNameJ.Trim(),
        //                ReqDescription1 = x.ReqDescription1.Trim(),
        //                ReqOperatorName = x.ReqOperatorName.Trim(),
        //                x.IssuedDate,
        //                DocumentStatus = x.DocumentStatus.Trim() ?? ""
        //            }).Distinct().ToList();
        //            var dataGroupIssue = new
        //            {
        //                rows = GroupIssueDocument
        //            };


        //            var connections = _userConnectionManager.GetUserConnections(item);
        //            if (connections != null && connections.Count > 0)
        //            {
        //                foreach (var connectionId in connections)
        //                {
        //                    //var return_data = ListDoc.Count();
        //                    //Random rnd = new Random();
        //                    //int month = rnd.Next(1, 13);
        //                    var return_data = new { dataapprove = dataApprove, datamydoc = dataMyDoc, itemCount = dataApprove.rows.Count(), dataedit = dataEdit, datastatus = JobStatus, datagroupissue = dataGroupIssue };

        //                     _notificationUserHubContext.Clients.Client(connectionId).SendAsync("ReceiveDocPending", return_data);//send to user 
        //                }
        //            }
        //        }
        //    }
        //    catch
        //    {
        //        all = null;
        //        JobStatus = null;
        //        dp = null;
        //        //Force garbage collection.
        //        GC.Collect();
        //        // Wait for all finalizers to complete before continuing.
        //        GC.WaitForPendingFinalizers();
        //    }
        //    finally
        //    {
        //        all = null;
        //        JobStatus = null;
        //        dp = null;
        //        //Force garbage collection.
        //        GC.Collect();
        //        // Wait for all finalizers to complete before continuing.
        //        GC.WaitForPendingFinalizers();
        //    }


        //}
        //------------------------------------End signalR Method--------------------------------------------------------------------------------------//


        //------------------------------------Service Method--------------------------------------------------------------------------------------//
        public dynamic Base64PDF(string DocNo, string DocCode)
        {
            //if (DocCode == "IS001")
            //{

            //pdf stamp only
            var datapdfstamp = _DocumentContext.DocumentUploadPDFStamp.Where(x => x.DocumentCode == DocCode).ToList();
            if (datapdfstamp.Count > 0 && DocNo != null)
            {

                var status = _DocumentContext.AttachFile.Where(x => x.DocumentNo == DocNo).Where(x => x.FileName.Contains("SYSTEMPSTAMP-")).Select(x => x.Path).FirstOrDefault();

                List<string> attachfile = new List<string>();

                if (status == null)
                {
                    attachfile.Add(_DocumentContext.AttachFile.Where(x => x.DocumentNo == DocNo).OrderBy(x => x.No).Select(x => x.Path).FirstOrDefault());
                }
                else
                {
                    attachfile.Add(_DocumentContext.AttachFile.Where(x => x.DocumentNo == DocNo).Where(x => x.FileName.Contains("SYSTEMPSTAMP-")).Select(x => x.Path).FirstOrDefault());
                }
                return attachfile;
            }

            //check referent in CMN063 get document add to pdf
            var CheckRefDoc = _DocumentContext.DocumentItemDetail.Where(i => i.DocumentNo == DocNo && i.ItemCode.Trim() == "CMN063" && i.FinalResult != "" && i.FinalResult != null).ToList();
            if (CheckRefDoc.Count > 0)
            {
                return Base64PDFMultiDoc(DocNo, DocCode);
            }

            DocNo = DocNo ?? "";

            var Syspath = _DocumentContext.Language.Where(i => i.GroupCode == "pathPdfTemplate").ToList();
            SqlParameter _DocNo = new SqlParameter("@DocNo", DocNo.Trim());

            List<ModelItemList_Result> dynamics = _DocumentContext.Set<ModelItemList_Result>().FromSql("exec sprFormGetInputItemResult @DocNo", _DocNo).AsNoTracking().ToList();
            List<DocumentItemValueTableDetail> alltable = new List<DocumentItemValueTableDetail>();
            if (DocNo != null)
            {
                foreach (var item in dynamics.Where(i => i.InputType == "table"))
                {
                    var itemtable = _DocumentContext.DocumentItemValueTableDetail.Where(i => i.InputItemCode.Trim() == item.ItemCode.Trim() && i.TableCode.Trim() == item.ValueCode.Trim() && i.DocumentNo.Trim() == DocNo.Trim()).ToList();
                    alltable.AddRange(itemtable);
                }
            }



            //List<DocumentItemProgress> appname = _DocumentContext.DocumentItemProgress.Where(i => i.DocumentNo.Trim() == DocNo).ToList();
            List<ApprovalFlow> approvalFlows = _DocumentContext.ApprovalFlow.Where(i => i.FlowId.Trim() == DocCode.Trim()).ToList();


            ConnDoc dp = new ConnDoc(_configuration);


            List<Flow> appname = dp.GetFlow(DocNo ?? "").ToList();

            if (DocNo != "")
            {
                DataTable retVal = new DataTable();
                var DocItem = _DocumentContext.DocumentItem.Find(DocNo.Trim());
                string PdfName = Syspath.Where(i => i.Code.Trim() == "PAH001").FirstOrDefault().Value1.Trim() + dynamics.FirstOrDefault().DocumentCode.Trim() + "_" + DocItem.ReviseNo.Trim() + ".pdf";



                try
                {
                    using (Stream pdfInputStream = new FileStream(path: PdfName, mode: FileMode.Open))
                    using (Stream resultPDFStream = _samplePDFFormService.FillForm(pdfInputStream, dynamics, alltable, appname, approvalFlows, "last"))
                    {
                        resultPDFStream.Flush();
                        resultPDFStream.Position = 0;
                        var data1 = "data:application/pdf;base64," + Convert.ToBase64String(ReadToEnd(resultPDFStream));
                        List<string> data = new List<string>
                        {
                            data1
                        };
                        //return File(ReadToEnd(resultPDFStream), System.Net.Mime.MediaTypeNames.Application.Octet, "test.pdf");
                        return data;
                    }
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }

            }
            else
            {
                var DocItem = _DocumentContext.Document.Find(DocCode.Trim());
                string PdfName = Syspath.Where(i => i.Code.Trim() == "PAH001").FirstOrDefault().Value1.Trim() + DocCode.Trim() + "_" + DocItem.ReviseNo.Trim() + ".pdf";
                try
                {

                    using (Stream pdfInputStream = new FileStream(path: PdfName, mode: FileMode.Open))
                    using (Stream resultPDFStream = _samplePDFFormService.FillForm(pdfInputStream, dynamics, alltable, appname, approvalFlows, "last"))
                    {
                        resultPDFStream.Flush();
                        resultPDFStream.Position = 0;
                        var data1 = "data:application/pdf;base64," + Convert.ToBase64String(ReadToEnd(resultPDFStream));
                        List<string> data = new List<string>
                        {
                            data1
                        };
                        //return File(ReadToEnd(resultPDFStream), System.Net.Mime.MediaTypeNames.Application.Octet, "test.pdf");
                        return data;
                    }
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }

            }
        }

        public dynamic Base64PDFManual(string DocCode)
        {
            var Syspath = _DocumentContext.Language.Where(i => i.GroupCode == "pathPdfTemplate").ToList();

            string PdfName = Syspath.Where(i => i.Code.Trim() == "PAH002").FirstOrDefault().Value1.Trim() + DocCode.Trim() + ".pdf";

            try
            {

                byte[] bytes = null;
                using (FileStream fs = new FileStream(path: PdfName, mode: FileMode.Open, access: FileAccess.Read))
                {
                    bytes = new byte[fs.Length];
                    fs.Read(bytes, 0, (int)fs.Length);
                }

                string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                var data1 = "data:application/pdf;base64," + base64String;

                List<string> data = new List<string>
                        {
                            data1
                        };
                return data;

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        public dynamic Base64PDFMultiDoc(string DocNo, string DocCode)
        {


            DocNo = DocNo ?? "";

            var Syspath = _DocumentContext.Language.Where(i => i.GroupCode == "pathPdfTemplate").ToList();
            SqlParameter _DocNo = new SqlParameter("@DocNo", DocNo.Trim());
            List<ModelItemList_Result> dynamics = _DocumentContext.Set<ModelItemList_Result>().FromSql("exec sprFormGetInputItemResult @DocNo", _DocNo).AsNoTracking().ToList();
            List<DocumentItemValueTableDetail> alltable = new List<DocumentItemValueTableDetail>();
            if (DocNo != null)
            {
                foreach (var item in dynamics.Where(i => i.InputType == "table"))
                {
                    var itemtable = _DocumentContext.DocumentItemValueTableDetail.Where(i => i.InputItemCode.Trim() == item.ItemCode.Trim() && i.TableCode.Trim() == item.ValueCode.Trim() && i.DocumentNo.Trim() == DocNo.Trim()).ToList();
                    alltable.AddRange(itemtable);
                }
            }



            //List<DocumentItemProgress> appname = _DocumentContext.DocumentItemProgress.Where(i => i.DocumentNo.Trim() == DocNo).ToList();
            List<ApprovalFlow> approvalFlows = _DocumentContext.ApprovalFlow.Where(i => i.FlowId.Trim() == DocCode.Trim()).ToList();


            ConnDoc dp = new ConnDoc(_configuration);

            List<Flow> appname = dp.GetFlow(DocNo ?? "").ToList();

            if (DocNo != "")
            {
                DataTable retVal = new DataTable();

                var DocItem = _DocumentContext.DocumentItem.Find(DocNo.Trim());
                string PdfName = Syspath.Where(i => i.Code.Trim() == "PAH001").FirstOrDefault().Value1.Trim() + dynamics.FirstOrDefault().DocumentCode.Trim() + "_" + DocItem.ReviseNo.Trim() + ".pdf";

                try
                {
                    List<string> Data = new List<string>();
                    var pdfInputStream = new FileStream(path: PdfName, mode: FileMode.Open);
                    var resultPDFStream = _samplePDFFormService.FillForm(pdfInputStream, dynamics, alltable, appname, approvalFlows, "last");
                    resultPDFStream.Flush();
                    resultPDFStream.Position = 0;
                    var Pdf1 = "data:application/pdf;base64," + Convert.ToBase64String(ReadToEnd(resultPDFStream));
                    pdfInputStream.Dispose();
                    resultPDFStream.Dispose();
                    Data.Add(Pdf1);

                    //check referent in CMN063 get document add to pdf
                    var CheckRefDoc = _DocumentContext.DocumentItemDetail.Where(i => i.DocumentNo == DocNo && i.ItemCode.Trim() == "CMN063" && i.FinalResult != "" && i.FinalResult != null).ToList();

                    if (CheckRefDoc != null)
                    {
                        foreach (var item in CheckRefDoc)
                        {
                            var DocumentData = _DocumentContext.DocumentItem.Where(i => i.DocumentNo == item.FinalResult.Trim()).FirstOrDefault();
                            if (DocumentData != null)
                            {
                                SqlParameter _DocNo1 = new SqlParameter("@DocNo", DocumentData.DocumentNo.Trim());
                                List<ModelItemList_Result> dynamics1 = _DocumentContext.Set<ModelItemList_Result>().FromSql("exec sprFormGetInputItemResult @DocNo", _DocNo1).AsNoTracking().ToList();
                                List<DocumentItemValueTableDetail> alltable1 = new List<DocumentItemValueTableDetail>();

                                foreach (var item1 in dynamics1.Where(i => i.InputType == "table"))
                                {
                                    var itemtable1 = _DocumentContext.DocumentItemValueTableDetail.Where(i => i.InputItemCode.Trim() == item1.ItemCode.Trim() && i.TableCode.Trim() == item1.ValueCode.Trim() && i.DocumentNo.Trim() == DocumentData.DocumentNo.Trim()).ToList();
                                    alltable1.AddRange(itemtable1);
                                }

                                //List<DocumentItemProgress> appname = _DocumentContext.DocumentItemProgress.Where(i => i.DocumentNo.Trim() == DocNo).ToList();
                                List<ApprovalFlow> approvalFlows1 = _DocumentContext.ApprovalFlow.Where(i => i.FlowId.Trim() == DocumentData.DocumentCode.Trim()).ToList();
                                List<Flow> appname1 = dp.GetFlow(DocumentData.DocumentNo.Trim() ?? "").ToList();

                                var PdfName1 = Syspath.Where(i => i.Code.Trim() == "PAH001").FirstOrDefault().Value1.Trim() + DocumentData.DocumentCode.Trim() + "_" + DocumentData.ReviseNo.Trim() + ".pdf";
                                var pdfInputStream1 = new FileStream(path: PdfName1, mode: FileMode.Open);
                                var resultPDFStream1 = _samplePDFFormService.FillForm(pdfInputStream1, dynamics1, alltable1, appname1, approvalFlows1, "last");
                                resultPDFStream1.Flush();
                                resultPDFStream1.Position = 0;
                                var Pdf2 = "data:application/pdf;base64," + Convert.ToBase64String(ReadToEnd(resultPDFStream1));
                                Data.Add(Pdf2);
                                pdfInputStream1.Dispose();
                                resultPDFStream1.Dispose();
                            }

                        }

                    }

                    //return File(ReadToEnd(resultPDFStream), System.Net.Mime.MediaTypeNames.Application.Octet, "test.pdf");
                    return Data;
                    // copy.Close();
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }

            }
            else
            {
                var DocItem = _DocumentContext.Document.Find(DocCode.Trim());
                string PdfName = Syspath.Where(i => i.Code.Trim() == "PAH001").FirstOrDefault().Value1.Trim() + DocCode.Trim() + "_" + DocItem.ReviseNo.Trim() + ".pdf";
                try
                {
                    using (Stream pdfInputStream = new FileStream(path: PdfName, mode: FileMode.Open))
                    using (Stream resultPDFStream = _samplePDFFormService.FillForm(pdfInputStream, dynamics, alltable, appname, approvalFlows, "last"))
                    {
                        resultPDFStream.Flush();
                        resultPDFStream.Position = 0;

                        var data1 = "data:application/pdf;base64," + Convert.ToBase64String(ReadToEnd(resultPDFStream));
                        List<string> data = new List<string>
                        {
                            data1
                        };

                        return data;
                    }
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }

            }
        }
        private static byte[] Combine(byte[] first, byte[] second)
        {
            return first.Concat(second).ToArray();
        }
        public byte[] StreamPDF(string DocNo, string DocCode)
        {
            DocNo = DocNo ?? "";
            ConnDoc dp = new ConnDoc(_configuration);
            DataTable retVal = new DataTable();
            List<Flow> appname = dp.GetFlow(DocNo ?? "").ToList();
            List<ApprovalFlow> approvalFlows = _DocumentContext.ApprovalFlow.Where(i => i.FlowId.Trim() == DocCode.Trim()).ToList();
            try
            {
                var Syspath = _DocumentContext.Language.Where(i => i.GroupCode == "pathPdfTemplate").ToList();
                SqlParameter _DocNo = new SqlParameter("@DocNo", DocNo.Trim());
                List<ModelItemList_Result> dynamics = _DocumentContext.Set<ModelItemList_Result>().FromSql("exec sprFormGetInputItemResult @DocNo", _DocNo).AsNoTracking().ToList();
                List<DocumentItemValueTableDetail> alltable = new List<DocumentItemValueTableDetail>();
                if (DocNo != null)
                {
                    foreach (var item in dynamics.Where(i => i.InputType == "table"))
                    {
                        var itemtable = _DocumentContext.DocumentItemValueTableDetail.Where(i => i.InputItemCode.Trim() == item.ItemCode.Trim() && i.TableCode.Trim() == item.ValueCode.Trim() && i.DocumentNo.Trim() == DocNo.Trim()).ToList();
                        alltable.AddRange(itemtable);
                    }
                }




                if (DocNo != "")
                {


                    var DocItem = _DocumentContext.DocumentItem.Find(DocNo.Trim());
                    string PdfName = Syspath.Where(i => i.Code.Trim() == "PAH001").FirstOrDefault().Value1.Trim() + dynamics.FirstOrDefault().DocumentCode.Trim() + "_" + DocItem.ReviseNo.Trim() + ".pdf";
                    using (Stream pdfInputStream = new FileStream(path: PdfName, mode: FileMode.Open))
                    using (Stream resultPDFStream = _samplePDFFormService.FillForm(pdfInputStream, dynamics, alltable, appname, approvalFlows, ""))
                    {
                        resultPDFStream.Flush();
                        resultPDFStream.Position = 0;
                        //var data = "data:application/pdf;base64," + Convert.ToBase64String(ReadToEnd(resultPDFStream));
                        //return File(ReadToEnd(resultPDFStream), System.Net.Mime.MediaTypeNames.Application.Octet, "test.pdf");
                        return ReadToEnd(resultPDFStream);
                    }

                }
                else
                {
                    var DocItem = _DocumentContext.Document.Find(DocCode.Trim());
                    string PdfName = Syspath.Where(i => i.Code.Trim() == "PAH001").FirstOrDefault().Value1.Trim() + DocCode.Trim() + "_" + DocItem.ReviseNo.Trim() + ".pdf";


                    using (Stream pdfInputStream = new FileStream(path: PdfName, mode: FileMode.Open))
                    using (Stream resultPDFStream = _samplePDFFormService.FillForm(pdfInputStream, dynamics, alltable, appname, approvalFlows, ""))
                    {
                        resultPDFStream.Flush();
                        resultPDFStream.Position = 0;
                        return ReadToEnd(resultPDFStream);
                    }
                }
            }
            catch
            {
                dp = null;
                retVal.Dispose();
                appname = null;
                approvalFlows = null;
                return null;
            }
            finally
            {
                dp = null;
                appname = null;
                approvalFlows = null;
                retVal.Dispose();
            }

        }
        public static byte[] ReadToEnd(System.IO.Stream stream)
        {
            long originalPosition = 0;

            if (stream.CanSeek)
            {
                originalPosition = stream.Position;
                stream.Position = 0;
            }

            try
            {
                byte[] readBuffer = new byte[4096];

                int totalBytesRead = 0;
                int bytesRead;

                while ((bytesRead = stream.Read(readBuffer, totalBytesRead, readBuffer.Length - totalBytesRead)) > 0)
                {
                    totalBytesRead += bytesRead;

                    if (totalBytesRead == readBuffer.Length)
                    {
                        int nextByte = stream.ReadByte();
                        if (nextByte != -1)
                        {
                            byte[] temp = new byte[readBuffer.Length * 2];
                            Buffer.BlockCopy(readBuffer, 0, temp, 0, readBuffer.Length);
                            Buffer.SetByte(temp, totalBytesRead, (byte)nextByte);
                            readBuffer = temp;
                            totalBytesRead++;
                        }
                    }
                }

                byte[] buffer = readBuffer;
                if (readBuffer.Length != totalBytesRead)
                {
                    buffer = new byte[totalBytesRead];
                    Buffer.BlockCopy(readBuffer, 0, buffer, 0, totalBytesRead);
                }
                return buffer;
            }
            finally
            {
                if (stream.CanSeek)
                {
                    stream.Position = originalPosition;
                }
            }
        }
        public async Task<dynamic> CheckValidateFileMachine(string DocCode, string DocNo)
        {
            var Allfile = await _DocumentContext.AttachFile.Where(x => x.DocumentNo == DocNo).OrderBy(i => i.AddDate).Select(i => new ResultValidateModel
            {
                FileName = i.FileName,
                CheckResult = true
            }).Take(1).ToListAsync();

            foreach (var item in Allfile)
            {
                var Checkresult = _machineOperation.ValidateFileMachineReport(item.FileName, DocCode, DocNo);
                item.CheckResult = Checkresult.Item1;
                item.HtmlTable = Checkresult.Item2;
            }
            return Allfile;
        }
        public System.Drawing.Image LoadImage(string ImageString)
        {

            byte[] bytes = Convert.FromBase64String(ImageString);
            System.Drawing.Image image;
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                image = System.Drawing.Image.FromStream(ms);
            }

            return image;
        }
        private DataTable DatatableItem()
        {
            // Add data item to data table
            using (DataTable dataTable = new DataTable())
            {
                dataTable.Columns.Add("DocumentNameE", typeof(string));
                dataTable.Columns.Add("DocumentNameT", typeof(string));
                dataTable.Columns.Add("DocumentNameJ", typeof(string));
                dataTable.Columns.Add("DocumentGroupCode", typeof(string));
                dataTable.Columns.Add("DocumentKind", typeof(string));
                dataTable.Columns.Add("OpeGroupCateg", typeof(string));
                dataTable.Columns.Add("AttachedFile", typeof(string));
                dataTable.Columns.Add("EmailSend", typeof(string));
                dataTable.Columns.Add("AttachedFile1", typeof(string));
                dataTable.Columns.Add("AttachedFile2", typeof(string));
                dataTable.Columns.Add("StandardNo", typeof(string));
                dataTable.Columns.Add("ReviseNo", typeof(string));
                dataTable.Columns.Add("Remark", typeof(string));
                dataTable.Columns.Add("Language", typeof(string));
                dataTable.Columns.Add("DocumentCode", typeof(string));
                dataTable.Columns.Add("ItemCateg", typeof(string));
                dataTable.Columns.Add("InputOption", typeof(string));
                dataTable.Columns.Add("DisplayOrder", typeof(int));
                dataTable.Columns.Add("ItemCategNameE", typeof(string));
                dataTable.Columns.Add("RemarksTitleE1", typeof(string));
                dataTable.Columns.Add("RemarksTitleE2", typeof(string));
                dataTable.Columns.Add("RemarksTitleE3", typeof(string));
                dataTable.Columns.Add("RemarksTitleE4", typeof(string));
                dataTable.Columns.Add("RemarksTitleE5", typeof(string));
                dataTable.Columns.Add("RemarksTitleE6", typeof(string));
                dataTable.Columns.Add("RemarksTitleE7", typeof(string));
                dataTable.Columns.Add("RemarksTitleE8", typeof(string));
                dataTable.Columns.Add("RemarksTitleE9", typeof(string));
                dataTable.Columns.Add("RemarksTitleE10", typeof(string));
                dataTable.Columns.Add("ItemCategNameT", typeof(string));
                dataTable.Columns.Add("RemarksTitleT1", typeof(string));
                dataTable.Columns.Add("RemarksTitleT2", typeof(string));
                dataTable.Columns.Add("RemarksTitleT3", typeof(string));
                dataTable.Columns.Add("RemarksTitleT4", typeof(string));
                dataTable.Columns.Add("RemarksTitleT5", typeof(string));
                dataTable.Columns.Add("RemarksTitleT6", typeof(string));
                dataTable.Columns.Add("RemarksTitleT7", typeof(string));
                dataTable.Columns.Add("RemarksTitleT8", typeof(string));
                dataTable.Columns.Add("RemarksTitleT9", typeof(string));
                dataTable.Columns.Add("RemarksTitleT10", typeof(string));
                dataTable.Columns.Add("ItemCategNameJ", typeof(string));
                dataTable.Columns.Add("RemarksTitleJ1", typeof(string));
                dataTable.Columns.Add("RemarksTitleJ2", typeof(string));
                dataTable.Columns.Add("RemarksTitleJ3", typeof(string));
                dataTable.Columns.Add("RemarksTitleJ4", typeof(string));
                dataTable.Columns.Add("RemarksTitleJ5", typeof(string));
                dataTable.Columns.Add("RemarksTitleJ6", typeof(string));
                dataTable.Columns.Add("RemarksTitleJ7", typeof(string));
                dataTable.Columns.Add("RemarksTitleJ8", typeof(string));
                dataTable.Columns.Add("RemarksTitleJ9", typeof(string));
                dataTable.Columns.Add("RemarksTitleJ10", typeof(string));
                dataTable.Columns.Add("InputItemCode", typeof(string));
                dataTable.Columns.Add("ItemNameE", typeof(string));
                dataTable.Columns.Add("ItemNameT", typeof(string));
                dataTable.Columns.Add("ItemNameJ", typeof(string));
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
                dataTable.Columns.Add("ValueCode", typeof(string));
                dataTable.Columns.Add("InputItemOption", typeof(string));
                dataTable.Columns.Add("DefaultValue", typeof(string));
                dataTable.Columns.Add("ReadOnly", typeof(bool));
                dataTable.Columns.Add("InputItemListItemCateg", typeof(string));
                dataTable.Columns.Add("ItemCode", typeof(string));
                dataTable.Columns.Add("InputItemListDisplayOrder", typeof(int));
                dataTable.Columns.Add("DocumentNo", typeof(string));
                dataTable.Columns.Add("FinalResult", typeof(string));
                dataTable.Columns.Add("AddDate", typeof(DateTime));
                dataTable.Columns.Add("UpdDate", typeof(DateTime));
                dataTable.Columns.Add("UserName", typeof(string));
                dataTable.Columns.Add("ComputerName", typeof(string));
                dataTable.Columns.Add("DetailOption", typeof(string));
                return dataTable;
            }

        }
        private DataTable ListTableData()
        {
            // Add data item to data table
            using (DataTable datatable = new DataTable())
            {
                datatable.Columns.Add("DocumentCode", typeof(string));
                datatable.Columns.Add("DisplayOrder", typeof(int));
                datatable.Columns.Add("TableCode", typeof(string));
                datatable.Columns.Add("InputItemCode", typeof(string));
                datatable.Columns.Add("ValueE", typeof(string));
                datatable.Columns.Add("ValueT", typeof(string));
                datatable.Columns.Add("ValueJ", typeof(string));
                datatable.Columns.Add("InputType", typeof(string));
                datatable.Columns.Add("DataType", typeof(string));
                datatable.Columns.Add("ValueCode", typeof(string));
                datatable.Columns.Add("ItemCode", typeof(string));
                datatable.Columns.Add("DocumentNo", typeof(string));
                datatable.Columns.Add("FinalResult", typeof(string));
                datatable.Columns.Add("AddDate", typeof(DateTime));
                datatable.Columns.Add("UpdDate", typeof(DateTime));
                datatable.Columns.Add("UserName", typeof(string));
                datatable.Columns.Add("ComputerName", typeof(string));
                datatable.Columns.Add("ItemId", typeof(int));
                return datatable;
            }

        }
        private DataTable ListUserFlow()
        {
            // Add data item to data table    
            using (DataTable ListUserFlow = new DataTable())
            {
                ListUserFlow.Columns.Add("FlowID", typeof(string));
                ListUserFlow.Columns.Add("CODEMPID", typeof(string));
                ListUserFlow.Columns.Add("NAMEMPE", typeof(string));
                ListUserFlow.Columns.Add("SeqNo", typeof(int));
                ListUserFlow.Columns.Add("RoleID", typeof(string));
                return ListUserFlow;
            }
        }
        private DataTable ListCondition()
        {
            // Add data item to data table
            using (DataTable ListCondition = new DataTable())
            {
                ListCondition.Columns.Add("DocumentNo", typeof(string));
                ListCondition.Columns.Add("DocumentCode", typeof(string));
                ListCondition.Columns.Add("Template", typeof(string));
                ListCondition.Columns.Add("Design", typeof(string));
                ListCondition.Columns.Add("Condition", typeof(string));
                ListCondition.Columns.Add("Value", typeof(string));
                return ListCondition;
            }
        }
        private Tuple<DataTable, string> AddDocItem(Tuple1 tuple1, string DocNo, string DocCode, IFormCollection frm, string Subject, string CreateBy, string ComputerName)
        {

            using (DataTable dataTable = DatatableItem())
            {
                foreach (var item in tuple1.vewDocumentItemList)
                {
                    if (item != null)
                    {
                        DataRow workRow = dataTable.NewRow();

                        workRow["DocumentNameE"] = item.DocumentNameE == null ? "" : item.DocumentNameE.ToString().Trim();
                        workRow["DocumentNameT"] = item.DocumentNameT == null ? "" : item.DocumentNameT.ToString().Trim();
                        workRow["DocumentNameJ"] = item.DocumentNameJ == null ? "" : item.DocumentNameJ.ToString().Trim();
                        workRow["DocumentGroupCode"] = item.DocumentGroupCode == null ? "" : item.DocumentGroupCode.ToString().Trim();
                        workRow["DocumentKind"] = item.DocumentKind == null ? "" : item.DocumentKind.ToString().Trim();
                        workRow["OpeGroupCateg"] = item.OpeGroupCateg == null ? "" : item.OpeGroupCateg.ToString().Trim();
                        workRow["AttachedFile"] = item.AttachedFile == null ? "" : item.AttachedFile.ToString().Trim();
                        workRow["EmailSend"] = item.EmailSend == null ? "" : item.EmailSend.ToString().Trim();
                        workRow["AttachedFile1"] = item.AttachedFile1 == null ? "" : item.AttachedFile1.ToString().Trim();
                        workRow["AttachedFile2"] = item.AttachedFile2 == null ? "" : item.AttachedFile2.ToString().Trim();
                        workRow["StandardNo"] = item.StandardNo == null ? "" : item.StandardNo.ToString().Trim();
                        workRow["ReviseNo"] = item.ReviseNo == null ? "" : item.ReviseNo.ToString().Trim();
                        workRow["Remark"] = item.Remark == null ? "" : item.Remark.ToString().Trim();
                        workRow["Language"] = item.Language == null ? "" : item.Language.ToString().Trim();
                        workRow["DocumentCode"] = item.DocumentCode == null ? "" : item.DocumentCode.ToString().Trim();
                        workRow["ItemCateg"] = item.ItemCateg == null ? "" : item.ItemCateg.ToString().Trim();
                        workRow["InputOption"] = item.InputOption == null ? "" : item.InputOption.ToString().Trim();
                        workRow["DisplayOrder"] = item.DisplayOrder;
                        workRow["ItemCategNameE"] = item.ItemCategNameE == null ? "" : item.ItemCategNameE.ToString().Trim();
                        workRow["RemarksTitleE1"] = item.RemarksTitleE1 == null ? "" : item.RemarksTitleE1.ToString().Trim();
                        workRow["RemarksTitleE2"] = item.RemarksTitleE2 == null ? "" : item.RemarksTitleE2.ToString().Trim();
                        workRow["RemarksTitleE3"] = item.RemarksTitleE3 == null ? "" : item.RemarksTitleE3.ToString().Trim();
                        workRow["RemarksTitleE4"] = item.RemarksTitleE4 == null ? "" : item.RemarksTitleE4.ToString().Trim();
                        workRow["RemarksTitleE5"] = item.RemarksTitleE5 == null ? "" : item.RemarksTitleE5.ToString().Trim();
                        workRow["RemarksTitleE6"] = item.RemarksTitleE6 == null ? "" : item.RemarksTitleE6.ToString().Trim();
                        workRow["RemarksTitleE7"] = item.RemarksTitleE7 == null ? "" : item.RemarksTitleE7.ToString().Trim();
                        workRow["RemarksTitleE8"] = item.RemarksTitleE8 == null ? "" : item.RemarksTitleE8.ToString().Trim();
                        workRow["RemarksTitleE9"] = item.RemarksTitleE9 == null ? "" : item.RemarksTitleE9.ToString().Trim();
                        workRow["RemarksTitleE10"] = item.RemarksTitleE10 == null ? "" : item.RemarksTitleE10.ToString().Trim();
                        workRow["ItemCategNameT"] = item.ItemCategNameT == null ? "" : item.ItemCategNameT.ToString().Trim();
                        workRow["RemarksTitleT1"] = item.RemarksTitleT1 == null ? "" : item.RemarksTitleT1.ToString().Trim();
                        workRow["RemarksTitleT2"] = item.RemarksTitleT2 == null ? "" : item.RemarksTitleT2.ToString().Trim();
                        workRow["RemarksTitleT3"] = item.RemarksTitleT3 == null ? "" : item.RemarksTitleT3.ToString().Trim();
                        workRow["RemarksTitleT4"] = item.RemarksTitleT4 == null ? "" : item.RemarksTitleT4.ToString().Trim();
                        workRow["RemarksTitleT5"] = item.RemarksTitleT5 == null ? "" : item.RemarksTitleT5.ToString().Trim();
                        workRow["RemarksTitleT6"] = item.RemarksTitleT6 == null ? "" : item.RemarksTitleT6.ToString().Trim();
                        workRow["RemarksTitleT7"] = item.RemarksTitleT7 == null ? "" : item.RemarksTitleT7.ToString().Trim();
                        workRow["RemarksTitleT8"] = item.RemarksTitleT8 == null ? "" : item.RemarksTitleT8.ToString().Trim();
                        workRow["RemarksTitleT9"] = item.RemarksTitleT9 == null ? "" : item.RemarksTitleT9.ToString().Trim();
                        workRow["RemarksTitleT10"] = item.RemarksTitleT10 == null ? "" : item.RemarksTitleT10.ToString().Trim();
                        workRow["ItemCategNameJ"] = item.ItemCategNameJ == null ? "" : item.ItemCategNameJ.ToString().Trim();
                        workRow["RemarksTitleJ1"] = item.RemarksTitleJ1 == null ? "" : item.RemarksTitleJ1.ToString().Trim();
                        workRow["RemarksTitleJ2"] = item.RemarksTitleJ2 == null ? "" : item.RemarksTitleJ2.ToString().Trim();
                        workRow["RemarksTitleJ3"] = item.RemarksTitleJ3 == null ? "" : item.RemarksTitleJ3.ToString().Trim();
                        workRow["RemarksTitleJ4"] = item.RemarksTitleJ4 == null ? "" : item.RemarksTitleJ4.ToString().Trim();
                        workRow["RemarksTitleJ5"] = item.RemarksTitleJ5 == null ? "" : item.RemarksTitleJ5.ToString().Trim();
                        workRow["RemarksTitleJ6"] = item.RemarksTitleJ6 == null ? "" : item.RemarksTitleJ6.ToString().Trim();
                        workRow["RemarksTitleJ7"] = item.RemarksTitleJ7 == null ? "" : item.RemarksTitleJ7.ToString().Trim();
                        workRow["RemarksTitleJ8"] = item.RemarksTitleJ8 == null ? "" : item.RemarksTitleJ8.ToString().Trim();
                        workRow["RemarksTitleJ9"] = item.RemarksTitleJ9 == null ? "" : item.RemarksTitleJ9.ToString().Trim();
                        workRow["RemarksTitleJ10"] = item.RemarksTitleJ10 == null ? "" : item.RemarksTitleJ10.ToString().Trim();
                        workRow["InputItemCode"] = item.InputItemCode == null ? "" : item.InputItemCode.ToString().Trim();
                        workRow["ItemNameE"] = item.ItemNameE == null ? "" : item.ItemNameE.ToString().Trim();
                        workRow["ItemNameT"] = item.ItemNameT == null ? "" : item.ItemNameT.ToString().Trim();
                        workRow["ItemNameJ"] = item.ItemNameJ == null ? "" : item.ItemNameJ.ToString().Trim();
                        workRow["InputType"] = item.InputType == null ? "" : item.InputType.ToString().Trim();
                        workRow["DataType"] = item.DataType == null ? "" : item.DataType.ToString().Trim();
                        workRow["DecimalNo"] = item.DecimalNo;
                        workRow["Required"] = item.Required;
                        workRow["Minlength"] = item.Minlength;
                        workRow["Maxlength"] = item.Maxlength;
                        workRow["Min"] = item.Min;
                        workRow["Max"] = item.Max;
                        workRow["Step"] = item.Step;
                        workRow["Unit"] = item.Unit == null ? "" : item.Unit.ToString().Trim();
                        workRow["ValueCode"] = item.valueold == null ? "" : item.valueold.ToString().Trim();
                        workRow["InputItemOption"] = item.InputItemOption == null ? "" : item.InputItemOption.ToString().Trim();
                        workRow["DefaultValue"] = item.DefaultValue == null ? "" : item.DefaultValue.ToString().Trim();
                        workRow["ReadOnly"] = item.ReadOnly;
                        workRow["InputItemListItemCateg"] = item.InputItemListItemCateg == null ? "" : item.InputItemListItemCateg.ToString().Trim();
                        workRow["ItemCode"] = item.ItemCode == null ? "" : item.ItemCode.ToString().Trim();
                        workRow["InputItemListDisplayOrder"] = item.InputItemListDisplayOrder;
                        workRow["DocumentNo"] = DocNo != "" ? DocNo : "";
                        workRow["DetailOption"] = item.DetailOption;

                        DocCode = item.DocumentCode == null ? "" : item.DocumentCode.ToString().Trim();

                        var val = "";
                        if (item.InputType.ToLower() == "checkbox")
                        {
                            var getvalue = tuple1.Groupcheckbox.Where(x => x.IsChecked == true
                            && x.DocumentCode.Trim() == item.DocumentCode.Trim()
                            && x.ItemCateg.Trim() == item.ItemCateg.Trim()
                            && x.ItemCode.Trim() == item.ItemCode.Trim()).ToList();

                            int num = 0;
                            foreach (var itemgetvalue in getvalue)
                            {
                                if (num == 0)
                                {
                                    val = Convert.ToString(itemgetvalue.ID);
                                }
                                else
                                {
                                    val = val + "," + Convert.ToString(itemgetvalue.ID);
                                }
                                num++;
                            }
                        }
                        else if (item.InputType.ToLower() == "radio")
                        {
                            string displayorder = frm[item.ItemCode];
                            if (displayorder != null)
                            {
                                var getvalue = tuple1.Groupradio.Where(x => x.ID == displayorder && x.ValueCode.Trim() == item.valueold.Trim()).FirstOrDefault();
                                val = Convert.ToString(getvalue.ID);
                            }

                        }
                        else if (item.InputType.ToLower() == "checkspe")
                        {
                            string displayorder = frm[item.ItemCode];
                            if (displayorder != null)
                            {
                                var getvalue = tuple1.Groupspecial.Where(x => x.ID == displayorder && x.ValueCode.Trim() == item.valueold.Trim()).FirstOrDefault();
                                val = Convert.ToString(getvalue.ID);
                            }

                        }
                        else if (item.InputType.ToLower() == "cbandtxt")
                        {
                            string displayorder = frm[item.ItemCode];
                            if (displayorder != null)
                            {
                                var getvalue = tuple1.Groupspecial.Where(x => x.ID == displayorder && x.ValueCode.Trim() == item.valueold.Trim()).FirstOrDefault();
                                val = Convert.ToString(getvalue.ID);
                            }

                        }
                        else
                        {
                            if (item.InputItemListDisplayOrder == 0 || item.InputItemListDisplayOrder == -1)
                            {
                                var valueinput = item.ValueCode == null ? "" : item.ValueCode.ToString().Trim();
                                var findvalue = _DocumentContext.ValueList.Where(x => x.Id == valueinput).FirstOrDefault();
                                if (findvalue != null)
                                {
                                    valueinput = findvalue.ValueT + " " + findvalue.ValueE + " " + findvalue.ValueJ;
                                }
                                Subject = Subject + " " + valueinput;
                            }
                            val = item.ValueCode == null ? "" : item.ValueCode.ToString().Trim();
                        }
                        workRow["FinalResult"] = val;
                        workRow["AddDate"] = DateTime.Now;
                        workRow["UpdDate"] = DateTime.Now;
                        workRow["UserName"] = CreateBy;
                        workRow["ComputerName"] = ComputerName;
                        dataTable.Rows.Add(workRow);
                        workRow = null;
                    }

                }

                return new Tuple<DataTable, string>(dataTable, Subject);
            }

        }
        private DataTable AddListTableData(Tuple1 tuple1, string DocCode, string CreateBy, string ComputerName)
        {
            using (DataTable ListTableData = this.ListTableData())
            {
                if (tuple1.Tabledata != null)
                {
                    foreach (var item in tuple1.Tabledata)
                    {
                        var dataID = item.Header ?? "";
                        //DisplayOrder  ÍÔ§µÒÁ master DisplayOrder ValueTable
                        var display = _DocumentContext.ValueTable.Where(x => x.Id == (dataID != "" ? Convert.ToInt16(dataID) : 0)).Select(x => x.DisplayOrder).FirstOrDefault();
                        DataRow workRow = ListTableData.NewRow();
                        workRow["DocumentCode"] = DocCode == null ? "" : DocCode.Trim();
                        //workRow["DisplayOrder"] = item.DisplayOrder;
                        workRow["DisplayOrder"] = display;
                        workRow["TableCode"] = item.TableCode == null ? "" : item.TableCode.ToString().Trim();
                        workRow["InputItemCode"] = item.Itemcode == null ? "" : item.Itemcode.ToString().Trim();
                        workRow["ValueE"] = item.ValueE == null ? "" : item.ValueE.ToString().Trim();
                        workRow["ValueT"] = item.ValueT == null ? "" : item.ValueT.ToString().Trim();
                        workRow["ValueJ"] = item.ValueJ == null ? "" : item.ValueJ.ToString().Trim();
                        workRow["InputType"] = item.InputType ?? "";
                        workRow["DataType"] = item.DataType ?? "";
                        workRow["ValueCode"] = null;
                        workRow["ItemCode"] = item.Header ?? "";
                        workRow["DocumentNo"] = null;
                        workRow["FinalResult"] = item.Value == null ? "" : item.Value.ToString().Trim();
                        workRow["AddDate"] = DateTime.Now;
                        workRow["UpdDate"] = DateTime.Now;
                        workRow["UserName"] = CreateBy;
                        workRow["ComputerName"] = ComputerName;
                        workRow["ItemId"] = item.ItemId;
                        //workRow["InputItemCode"] = item.Header;

                        ListTableData.Rows.Add(workRow);
                        workRow = null;
                        // Add data item to data table end
                    }
                }
                return ListTableData;
            }

        }
        public void PDFServiceForSPPDF(string DocCode, string DocNo, String DocName)
        {
            string webRootPath = _hostingEnvironment.WebRootPath + "\\Attach\\" + DocCode + "\\" + DocNo + "\\" + DocName;
            string webRootPathOut = _hostingEnvironment.WebRootPath + "\\Attach\\" + DocCode + "\\" + DocNo + "\\" + "SYSTEMPSTAMP-" + DocName;

            File.Delete(webRootPathOut);

            PdfReader pdfReader = null;
            pdfReader = new PdfReader(webRootPath);
            var PdfApprove = StreamPDF(DocNo ?? "", DocCode);
            using (var fileStream = new FileStream(webRootPathOut, FileMode.Create, FileAccess.Write))
            {
                //var document = new iTextSharp.text.Document(pdfReader.GetPageSizeWithRotation(1));
                var document = new iTextSharp.text.Document();
                var writer = PdfWriter.GetInstance(document, fileStream);

                document.Open();

                for (var i = 1; i <= pdfReader.NumberOfPages; i++) //To
                {
                    var pagesize = pdfReader.GetPageSizeWithRotation(i);
                    document.SetPageSize(pagesize);
                    document.NewPage();
                    var importedPage = writer.GetImportedPage(pdfReader, i);
                    var contentByte = writer.DirectContent;
                    // contentByte.BeginText();
                    // contentByte.SetFontAndSize(baseFont, 12);
                    var checkoptionpage = _DocumentContext.DocumentUploadPDFStamp.Where(x => x.DocumentCode == DocCode && x.PageTo == i).FirstOrDefault();
                    if (checkoptionpage != null)
                    {
                        if (checkoptionpage.PageTo == i)
                        {
                            PdfImportedPage imp1 = writer.GetImportedPage(new PdfReader(PdfApprove), checkoptionpage.PageForm); //From
                            contentByte.AddTemplate(imp1, (float)checkoptionpage.X, (float)checkoptionpage.Y);
                        }
                    }
                    // contentByte.EndText();
                    contentByte.AddTemplate(importedPage, 0, 0);
                }
                document.Close();
                writer.Close();
            }
            pdfReader.Close();


            AttachFile _Ctrl = _DocumentContext.AttachFile.Where(p => p.DocumentNo == DocNo && p.FileName == "SYSTEMPSTAMP-" + DocName).FirstOrDefault();
            if (_Ctrl == null)
            {
                {
                    //add attach
                    AttachFile attachFile = new AttachFile
                    {
                        DocumentNo = DocNo,
                        FileName = "SYSTEMPSTAMP-" + DocName,
                        DocumentCode = DocCode,
                        Path = "../Attach/" + DocCode + "/" + DocNo + "/" + "SYSTEMPSTAMP-" + DocName,
                        ComputerName = "SYSTEMP",
                        UserName = "SYSTEMP",
                        AddDate = DateTime.Now,
                        UpdDate = DateTime.Now,
                    };
                    _DocumentContext.AttachFile.Add(attachFile);
                    _DocumentContext.SaveChanges();
                }
            }
            var CheckComplete = _DocumentContext.DocumentItem.Any(i => i.DocumentStatus == "Complete" && i.DocumentNo == DocNo);
            if (CheckComplete == true)
            {
                File.Delete(webRootPath); //Delete File old
                File.Move(webRootPathOut, webRootPath); // Move New to path old

                //Delete File
                AttachFile _CheckDeleteFile = _DocumentContext.AttachFile.Where(p => p.DocumentNo == DocNo && p.FileName == "SYSTEMPSTAMP-" + DocName).FirstOrDefault();
                if (_CheckDeleteFile != null)
                {
                    _DocumentContext.AttachFile.Remove(_CheckDeleteFile);
                    _DocumentContext.SaveChanges();
                }
            }
        }
        public async Task<dynamic> ListApproved(List<string> DocumentNo, string EmpID, string EmpName, string Division, string Section, string Department, string Department2, string Department3, string GroupCateg)
        {
            try
            {
                string ComputerName = _accessor.HttpContext.Connection.RemoteIpAddress.ToString();
                var _Mode = new SqlParameter("@Mode", "A");
                var _EmpID = new SqlParameter("@EmpID", EmpID);
                var _EmpName = new SqlParameter("@EmpName", EmpName);
                var _Comment = new SqlParameter("@Comment", "-");
                var _ComputerName = new SqlParameter("@ComputerName", ComputerName);
                var _Signature = new SqlParameter("@Signature", "");
                var _DateApprove = new SqlParameter("@DateApprove", DateTime.Now);
                foreach (var item in DocumentNo)
                {

                    var chkDoccode = _DocumentContext.DocumentItem.Where(x => x.DocumentNo == item.Trim()).FirstOrDefault();
                    var _DocNo = new SqlParameter("@DocNo", item.Trim());
                    var _DocCode = new SqlParameter("@DocCode", chkDoccode.DocumentCode.Trim());

                    var sql = "EXEC dbo.[sprFormSaveFormList] @DocNo,@DocCode,@EmpID,@EmpName,@Mode,@Comment,@ComputerName,@Signature,@DateApprove";
                    await _DocumentContext.Database.ExecuteSqlCommandAsync(sql, _DocNo, _DocCode, _EmpID, _EmpName, _Mode, _Comment, _ComputerName, _Signature, _DateApprove);
                    _logger.LogInformation(EmpID + " - Approve DocNo: " + item.Trim());

                    //for stamp only on pdf
                    var checkdoccode = _DocumentContext.DocumentUploadPDFStamp.Where(x => x.DocumentCode == chkDoccode.DocumentCode.Trim()).FirstOrDefault();
                    if (checkdoccode != null)
                    {
                        StampApproveSPPDF(chkDoccode.DocumentCode.Trim(), item.Trim());
                    }
                }

                var DataComplete = new { status = true, subject = "Approved All Items", detail = "Added Successfully!" };

                return DataComplete;
            }
            catch (Exception ex)
            {
                var DataComplete = new { status = false, subject = "Approved All Items", detail = ex.Message };
                return DataComplete;
            }
            finally
            {
                //send real time notification  to user
                //Boardcastnotify();
            }
        }

        //public Tuple<Boolean, string> CheckApprove()
        //{
        //    if (_DocumentContext.DocumentConditionHist.Any(i => i.Template == "approve" && i.DocumentNo == model.FirstOrDefault().DocumentNo) && model.FirstOrDefault().DocumentNo != "" && !model.FirstOrDefault().DocumentNo.StartsWith("D"))
        //    {
        //    }
        //}
        //------------------------------------End Service Method--------------------------------------------------------------------------------------//
        public async Task SetCountPageAttachfileAsync()
        {
            List<AttachFile> attfile = await _DocumentContext.AttachFile.Where(p => p.TotalPage == 0).ToListAsync();
            if (attfile.Count > 0)
            {
                foreach (var item in attfile)
                {
                    if (File.Exists(_hostingEnvironment.WebRootPath + "\\Attach\\" + item.DocumentCode.Trim() + "\\" + item.DocumentNo.Trim() + "\\" + item.FileName.Trim()))
                    {
                        string ppath = _hostingEnvironment.WebRootPath + "\\Attach\\" + item.DocumentCode.Trim() + "\\" + item.DocumentNo.Trim() + "\\" + item.FileName.Trim();

                        string ext = Path.GetExtension(ppath);

                        if (ext == ".pdf")
                        {
                            try
                            {
                                PdfReader pdfReader = null;
                                pdfReader = new PdfReader(ppath);
                                int numberOfPages = pdfReader.NumberOfPages;
                                item.TotalPage = numberOfPages;
                                pdfReader.Close();
                            }
                            catch
                            {

                            }

                        }
                        else if (ext == ".jpg" || ext == ".png" || ext == ".jpeg")
                        {
                            item.TotalPage = 1;
                        }
                        else
                        {
                            item.TotalPage = 3;
                        }

                    }
                    else
                    {
                        item.TotalPage = 5;
                    }

                    item.UpdDate = DateTime.Now;
                    //await _DocumentContext.SaveChangesAsync();
                }
                _DocumentContext.AttachFile.UpdateRange(attfile);
                _DocumentContext.SaveChanges();
            }
        }

        public async Task<dynamic> Getuserautodetail(string userid)
        {
            var listitems = await _hRMSLocalContext.HrmsEmployee.Where(i => i.Codempid == userid).Select(i => new
            {
                CMN067 = i.Namempe,
                CMN068 = i.Department3,
                CMN069 = i.Department2,
                CMN070 = i.Department,
                CMN071 = i.WorkingDate.HasValue ? i.WorkingDate.Value.ToString("dd/MM/yyyy") : "",
                CMN072 = i.Position,
                CMN073 = i.Emplevel.HasValue ? i.Emplevel.Value.ToString() : "",
                CMN074 = i.Codcalen,
                CMN075 = i.Namempt,
                CMN076 = i.Age,
                CMN077 = i.Dtebirth.HasValue ? i.Dtebirth.Value.ToString("dd/MM/yyyy") : "",
                CMN078 = i.Organizationname,
                CMN079 = i.Section,
                CMN080 = i.Company,
                CMN081 = i.Nationality,
                CMN082 = i.Email1,
                CMN083 = i.Email2,
            }).ToListAsync();

            return listitems;
        }

        public dynamic Getempdata(string empid)
        {
            var empdata = _hRMSLocalContext.HrmsEmployee.Where(i => i.Codempid.Trim() == empid.Trim()).Select(i => new
            {
                Namempe = i.Namempe ?? "-",
                Department3 = i.Department3 ?? "-",
                Department2 = i.Department2 ?? "-",
                Department = i.Department ?? "-",
                WorkingDate = i.WorkingDate.HasValue ? i.WorkingDate.Value.ToString("dd/MM/yyyy") : "-",
                Position = i.Position ?? "-",
                Emplevel = i.Emplevel.HasValue ? i.Emplevel.Value.ToString() : "-",
                Codcalen = i.Codcalen ?? "-",
                Namempt = i.Namempt ?? "-",
                Age = i.Age ?? "-",
                Dtebirth = i.Dtebirth.HasValue ? i.Dtebirth.Value.ToString("dd/MM/yyyy") : "-",
                Organizationname = i.Organizationname ?? "-",
                Section = i.Section ?? "-",
                Company = i.Company ?? "-",
                Nationality = i.Nationality ?? "-",
                Email1 = i.Email1 ?? "-",
                Email2 = i.Email2 ?? "-",
            }).FirstOrDefault();

            return empdata;
        }

        public List<ExportForm> LoadForm(string code , string from, string to, string fromapp , string toapp , string fromseqno, string toseqno,  string fromempno, string toempno,  string status, string dates , string username)
        {
            SqlParameter _code = new SqlParameter("@code", code ?? "");
            SqlParameter _from = new SqlParameter("@from", from ?? "");
            SqlParameter _to = new SqlParameter("@to", to ?? "");
            SqlParameter _fromapp = new SqlParameter("@fromapp", fromapp ?? "");
            SqlParameter _toapp = new SqlParameter("@toapp", toapp ?? "");
            SqlParameter _fromseqno = new SqlParameter("@fromseqno", fromseqno ?? "");
            SqlParameter _toseqno = new SqlParameter("@toseqno", toseqno ?? "");
            SqlParameter _fromempno = new SqlParameter("@fromempno", fromempno ?? "");
            SqlParameter _toempno = new SqlParameter("@toempno", toempno ?? "");
            SqlParameter _status = new SqlParameter("@status", status ?? "");
            SqlParameter _username = new SqlParameter("@username", username ?? "");
            List<ExportForm> datas = _DocumentContext.Set<ExportForm>().FromSql("exec sprInquiryExportForm @code ,@from ,@to ,@fromapp ,@toapp ,@fromseqno , @toseqno , @fromempno ,@toempno ,@status ,@username",
                _code, _from , _to , _fromapp , _toapp , _fromseqno , _toseqno , _fromempno , _toempno , _status , _username).AsNoTracking().ToList();
          
            if (datas.Count > 0)
            {
                var Syspath = _DocumentContext.Language.Where(i => i.GroupCode == "pathPdfTemplate").ToList();
                
                string path = Path.Combine(_hostingEnvironment.WebRootPath + "\\TempFile\\" + dates);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                foreach (var documentdata in datas)
                {
                    var Return_DocNo = documentdata.DocumentNo.Trim();
                    var Return_FileName = documentdata.FileName;
                    

                    SqlParameter _DocNo = new SqlParameter("@DocNo", Return_DocNo);
                    List<ModelItemList_Result> dynamics = _DocumentContext.Set<ModelItemList_Result>().FromSql("exec sprFormGetInputItemResult @DocNo", _DocNo).AsNoTracking().ToList();
                    List<DocumentItemValueTableDetail> alltable = new List<DocumentItemValueTableDetail>();

                    foreach (var item in dynamics.Where(i => i.InputType == "table"))
                    {
                        var itemtable = _DocumentContext.DocumentItemValueTableDetail.Where(i => i.InputItemCode.Trim() == item.ItemCode.Trim() && i.TableCode.Trim() == item.ValueCode.Trim() && i.DocumentNo.Trim() == Return_DocNo.Trim()).ToList();
                        alltable.AddRange(itemtable);
                    }

                    List<ApprovalFlow> approvalFlows = _DocumentContext.ApprovalFlow.Where(i => i.FlowId.Trim() == documentdata.DocumentCode.Trim()).ToList();

                    ConnDoc dp = new ConnDoc(_configuration);

                    List<Flow> appname = dp.GetFlow(Return_DocNo ?? "").ToList();


                    DataTable retVal = new DataTable();
                    var DocItem = _DocumentContext.DocumentItem.Find(Return_DocNo);
                    string PdfName = Syspath.Where(i => i.Code.Trim() == "PAH001").FirstOrDefault().Value1.Trim() + dynamics.FirstOrDefault().DocumentCode.Trim() + "_" + DocItem.ReviseNo.Trim() + ".pdf";

                    using (Stream pdfInputStream = new FileStream(path: PdfName, mode: FileMode.Open))
                    using (Stream resultPDFStream = _samplePDFFormService.FillForm(pdfInputStream, dynamics, alltable, appname, approvalFlows, "last"))
                    {
                        if (resultPDFStream != null)
                        {
                            resultPDFStream.Flush();
                            resultPDFStream.Position = 0;

                            using (var fileStream = new FileStream(Path.Combine(path, Return_FileName), FileMode.Create))
                            {
                                resultPDFStream.CopyTo(fileStream);
                            }
                        }

                    }

                }
            }
            return datas;
        }
    }
}