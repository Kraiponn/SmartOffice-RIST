using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SmartOffice.eManagement.Class;
using SmartOffice.eManagement.IResponsitory;
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
    public class EForm : IEForm
    {
        private readonly ManagementControlContext _context;

        private readonly IHostingEnvironment _hostingEnvironment;
        //private readonly IConfiguration _configuration;
        private IHttpContextAccessor _accessor;
        private readonly IConnForm _IConnForm;
        public EForm(ManagementControlContext context, IHostingEnvironment hostingEnvironment,IHttpContextAccessor accessor, IConnForm Iconnform)
        {
            _hostingEnvironment = hostingEnvironment;
            _context = context;
            //_configuration = configuration;
            _accessor = accessor;
            _IConnForm = Iconnform;
            //dp = new ConnForm(_configuration);
        }

        public TupleForm GetGroup(string OpeGroupCateg, string Division, string Section, string Department, string Department2, string Department3, string username, string SpecialGroup)
        {
            //var dp = new ConnForm(_configuration);
            var data = _IConnForm.GetGroup(OpeGroupCateg, Division, Section, Department, Department2, Department3, username, SpecialGroup);
            //dp = null;
            return data;
        }

        public TupleForm GetInquiry(string strObj, string strObMode, string strObjSub, string OperationCode, string OpeGroupCateg, string InputKind, string OperationNo, string UserName)
        {
           
            using (DataTable dataTable = DatatableItem())
            {
               // var dp = new ConnForm(_configuration);
                var data = _IConnForm.GetInquiry(strObj, strObMode, strObjSub, OperationCode, OpeGroupCateg, InputKind, OperationNo, dataTable, UserName);
               // dp = null;
                return data;
            }
               
        }

        public DataTable GetReport(string strObj, string strObMode, string OperationCode, string OpeGroupCateg, string InputKind, string OperationNo, string UserName)
        {

            using (DataTable dataTable = DatatableItem())
            {
                //var dp = new ConnForm(_configuration);
                var data = _IConnForm.GetReport(strObj, strObMode, OperationCode, OpeGroupCateg, InputKind, OperationNo, dataTable, UserName);
               // dp = null;
                return data;
            }
        }


        public TupleForm GetGenerate(string strObj, string strObMode, string OperationCode, string OpeGroupCateg, string InputKind, string OperationNo, string UserName)
        {
            TupleForm model = null;
            DataTable dataTable = DatatableItem();
            List<FormSelectListItems> SelectListItem = new List<FormSelectListItems> { };
            List<FormCheckBoxListItem> CheckBoxListItem = new List<FormCheckBoxListItem> { };
            List<FormRadioListItem> RadioListItem = new List<FormRadioListItem> { };
            List<FormRadioListItem> CheckSpeItem = new List<FormRadioListItem> { };
           // ConnForm dp = new ConnForm(_configuration);
            List<Formvaluelistinput> valuelistinput;

            try
            {
                var data = _IConnForm.GetGenerate(strObj, strObMode, OperationCode, OpeGroupCateg, InputKind, OperationNo, dataTable, UserName);
                var value = data.formOperationItemLists.Where(x => x.valueold != "" && x.valueold != null).OrderBy(x => x.ItemListDisplayOrder).ToList();
                model = new TupleForm { };
                foreach (var itema in value)
                {

                    var valuechk = data.formValueLists.Where(x => x.ValueCode.Trim() == itema.valueold.Trim()).OrderBy(x => x.ValueCode.Trim() + x.Value.Trim() == itema.ValueCode.Trim()).ThenBy(n => n.DisplayOrder).ToList();
                    foreach (var itemval in valuechk)
                    {

                       valuelistinput = new List<Formvaluelistinput> { };

                        string authors = itema.ValueCode.Trim();
                        string[] authorsList = authors.Split(",");

                        foreach (string author in authorsList)
                        {
                            valuelistinput.Add(new Formvaluelistinput
                            {
                                Text = author
                            });
                        }

                        bool checkedinput = false;
                        var maptext = valuelistinput.Where(x => x.Text.Trim() == itemval.ValueCode.Trim() + itemval.Value.Trim()).FirstOrDefault();
                        if (maptext != null)
                        {
                            checkedinput = true;
                        }

                        if (itema.InputType == "combo")
                        {
                            SelectListItem.Add(new FormSelectListItems
                            {
                                Value = itemval.ValueCode + itemval.Value,
                                Text = itemval.ValueText.Trim(),
                                Code = itemval.ValueCode.Trim(),
                                IsChecked = checkedinput,
                                Order = itemval.DisplayOrder,
                                OperationCode = itema.OperationCode,
                                ItemCateg = itema.ItemCateg,
                                OperationName = itema.OperationName,
                                ItemCode = itema.ItemCode,
                                ItemName = itema.ItemName,

                            });
                        }
                        else if (itema.InputType == "checkbox")
                        {
                            CheckBoxListItem.Add(new FormCheckBoxListItem
                            {
                                Display = itemval.ValueText.Trim(),
                                ValueCode = itemval.ValueCode.Trim(),
                                ID = itemval.ValueCode + itemval.Value,
                                IsChecked = checkedinput,
                                OperationCode = itema.OperationCode,
                                ItemCateg = itema.ItemCateg,
                                OperationName = itema.OperationName,
                                ItemCode = itema.ItemCode,
                                ItemName = itema.ItemName,
                            });
                        }
                        else if (itema.InputType == "cbandtxt")
                        {
                            CheckBoxListItem.Add(new FormCheckBoxListItem
                            {
                                Display = itemval.ValueText.Trim(),
                                ValueCode = itemval.ValueCode.Trim(),
                                ID = itemval.ValueCode + itemval.Value,
                                IsChecked = checkedinput,
                                OperationCode = itema.OperationCode,
                                ItemCateg = itema.ItemCateg,
                                OperationName = itema.OperationName,
                                ItemCode = itema.ItemCode,
                                ItemName = itema.ItemName,
                            });
                        }
                        else if (itema.InputType == "radio")
                        {
                            RadioListItem.Add(new FormRadioListItem
                            {
                                Display = itemval.ValueText.Trim(),
                                Order = itemval.DisplayOrder,
                                ValueCode = itemval.ValueCode.Trim(),
                                ID = itemval.ValueCode + itemval.Value,
                                IsChecked = checkedinput,
                                OperationCode = itema.OperationCode,
                                ItemCateg = itema.ItemCateg,
                                OperationName = itema.OperationName,
                                ItemCode = itema.ItemCode,
                                ItemName = itema.ItemName,
                            });
                        }
                        else if (itema.InputType == "checkspe")
                        {
                            CheckSpeItem.Add(new FormRadioListItem
                            {
                                Display = itemval.ValueText.Trim(),
                                Order = itemval.DisplayOrder,
                                ValueCode = itemval.ValueCode.Trim(),
                                ID = itemval.ValueCode + itemval.Value,
                                IsChecked = checkedinput,
                                OperationCode = itema.OperationCode,
                                ItemCateg = itema.ItemCateg,
                                OperationName = itema.OperationName,
                                ItemCode = itema.ItemCode,
                                ItemName = itema.ItemName,
                            });
                        }
                        valuelistinput = null;
                    }
                };


                model.formOperationItemLists = data.formOperationItemLists.ToList();

                //Header Text input page
                var dataPicCodeDept = _IConnForm.GetInquiry(strObj, "Inquiry", "HeaderText", OperationCode, OpeGroupCateg, InputKind, OperationNo, dataTable, UserName);
                var PicCodeDept = dataPicCodeDept.vewHRMSEmployeeSurveyPICs.Select(x => x.DEPARTMENT).FirstOrDefault();

                if(PicCodeDept != null && PicCodeDept != "") { 
                    foreach (var item in model.formOperationItemLists.Where(x=>x.Remarks1 == ""))
                    {
                        item.Remarks1 = PicCodeDept;
                    }
                }
                model.formGroupdropdown = SelectListItem.ToList();
                model.formGroupcheckbox = CheckBoxListItem.ToList();
                model.formGroupradio = RadioListItem.OrderBy(x => x.ValueCode).ThenBy(x => x.OperationCode).ThenBy(x => x.ItemCateg).ThenBy(x => x.ItemCode).ThenBy(x => x.Order).ToList();
                model.formGroupspecial = CheckSpeItem.OrderBy(x => x.ValueCode).ThenBy(x => x.OperationCode).ThenBy(x => x.ItemCateg).ThenBy(x => x.ItemCode).ThenBy(x => x.Order).ToList();

                return model;
            }
            catch
            {
                model = null;
                dataTable.Dispose();
                SelectListItem = null;
                CheckBoxListItem = null;
                RadioListItem = null;
                CheckSpeItem = null;
               // dp = null;
                valuelistinput = null;
                return null;
            }
            finally
            {
                model = null;
                dataTable.Dispose();
                SelectListItem = null;
                CheckBoxListItem = null;
                RadioListItem = null;
                CheckSpeItem = null;
                valuelistinput = null;
               // dp = null;
            }
           
        }

        private DataTable DatatableItem()
        {
            // Add data item to data table
            using (DataTable dataTable = new DataTable())
            {
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
                     
        }

        private DataTable ListTableData()
        {
            // Add data item to data table
      
            using (DataTable datatable = new DataTable())
            {
                datatable.Columns.Add("OperationCode", typeof(string));
                datatable.Columns.Add("DisplayOrder", typeof(int));
                datatable.Columns.Add("TableCode", typeof(string));
                datatable.Columns.Add("InputItemCode", typeof(string));
                datatable.Columns.Add("Value", typeof(string));
                datatable.Columns.Add("InputType", typeof(string));
                datatable.Columns.Add("DataType", typeof(string));
                datatable.Columns.Add("ValueCode", typeof(string));
                datatable.Columns.Add("ItemCode", typeof(string));
                datatable.Columns.Add("OperationNo", typeof(string));
                datatable.Columns.Add("FinalResult", typeof(string));
                datatable.Columns.Add("AddDate", typeof(DateTime));
                datatable.Columns.Add("UpdDate", typeof(DateTime));
                datatable.Columns.Add("UserName", typeof(string));
                datatable.Columns.Add("ComputerName", typeof(string));
                datatable.Columns.Add("ItemId", typeof(int));
                return datatable;
            }
               
        }


        public dynamic MaintenanceForm(TupleForm tupleForm, string strObj, string strObMode, string strObjSub, string OperationCode, string OpeGroupCateg, string InputKind, string OperationNo, IFormCollection frm, string CreateBy)
        {
            string ComputerName = _accessor.HttpContext.Connection.RemoteIpAddress.ToString();
            var AddItem = AddDocItem(tupleForm, frm, CreateBy, ComputerName);
            DataTable ListTableData = AddListTableData(tupleForm, OperationCode, CreateBy, ComputerName);

          //  var dp = new ConnForm(_configuration);
            var data = _IConnForm.MaintenanceForm(ListTableData,AddItem.Item1, strObj, strObMode, strObjSub, OperationCode, OpeGroupCateg, InputKind, OperationNo, CreateBy);
            ListTableData = null;
            return data;
        }

        private DataTable AddListTableData(TupleForm tupleForm, string OperationCode, string CreateBy, string ComputerName)
        {
            //DataTable ListTableData = this.ListTableData();
            using (DataTable ListTableData = this.ListTableData())
            {
                if (tupleForm.tabledata != null)
                {
                    foreach (var item in tupleForm.tabledata)
                    {
                        var dataID = item.Header ?? "";

                        var display = _context.ValueTable.Where(x => x.Id == (dataID != "" ? Convert.ToInt16(dataID) : 0)).Select(x => x.DisplayOrder).FirstOrDefault();
                        DataRow workRow = ListTableData.NewRow();
                        workRow["OperationCode"] = OperationCode == null ? "" : OperationCode.Trim();
                        workRow["DisplayOrder"] = display;
                        workRow["TableCode"] = item.TableCode == null ? "" : item.TableCode.ToString().Trim();
                        workRow["InputItemCode"] = item.Itemcode == null ? "" : item.Itemcode.ToString().Trim();
                        workRow["Value"] = item.Value == null ? "" : item.Value.ToString().Trim();
                        workRow["InputType"] = item.InputType ?? "";
                        workRow["DataType"] = item.DataType ?? "";
                        workRow["ValueCode"] = null;
                        workRow["ItemCode"] = item.Header ?? "";
                        workRow["OperationNo"] = null;
                        workRow["FinalResult"] = item.FinalResult == null ? "" : item.FinalResult.ToString().Trim();
                        workRow["AddDate"] = DateTime.Now;
                        workRow["UpdDate"] = DateTime.Now;
                        workRow["UserName"] = CreateBy;
                        workRow["ComputerName"] = ComputerName;
                        workRow["ItemId"] = item.ItemId;

                        ListTableData.Rows.Add(workRow);
                        // Add data item to data table end
                    }
                }
                return ListTableData;
            }
              
        }
        private Tuple<DataTable> AddDocItem(TupleForm tupleForm, IFormCollection frm, string CreateBy, string ComputerName)
        {
            // Add data item to data table
            //DataTable dataTable = DatatableItem();
            try
            {
                using (DataTable dataTable = this.DatatableItem())
                {
                    if (tupleForm.formOperationItemLists != null)
                    {
                        foreach (var item in tupleForm.formOperationItemLists)
                        {
                            if (item != null)
                            {
                                DataRow workRow = dataTable.NewRow();

                                workRow["OperationNo"] = item.OperationNo == null ? "" : item.OperationNo.ToString().Trim();
                                workRow["OperationCode"] = item.OperationCode == null ? "" : item.OperationCode.ToString().Trim();
                                workRow["OperationName"] = item.OperationName == null ? "" : item.OperationName.ToString().Trim();
                                workRow["OperationDisplayOrder"] = item.OperationDisplayOrder;
                                workRow["InputKind"] = item.InputKind == null ? "" : item.InputKind.ToString().Trim();
                                workRow["RoleID"] = item.RoleID == null ? "" : item.RoleID.ToString().Trim();
                                workRow["OpeGroupCode"] = item.OpeGroupCode == null ? "" : item.OpeGroupCode.ToString().Trim();
                                workRow["OpeGroupName"] = item.OpeGroupName == null ? "" : item.OpeGroupName.ToString().Trim();
                                workRow["OpeGroupCateg"] = item.OpeGroupCateg == null ? "" : item.OpeGroupCateg.ToString().Trim();
                                workRow["DisplayPriority"] = item.DisplayPriority;
                                workRow["SpecialOperate"] = item.SpecialOperate == null ? "" : item.SpecialOperate.ToString().Trim();
                                workRow["CategInputOption"] = item.CategInputOption == null ? "" : item.CategInputOption.ToString().Trim();
                                workRow["CategDisplayOrder"] = item.CategDisplayOrder;
                                workRow["ItemCateg"] = item.ItemCateg == null ? "" : item.ItemCateg.ToString().Trim();
                                workRow["ItemCategName"] = item.ItemCategName == null ? "" : item.ItemCategName.ToString().Trim();
                                workRow["Remarks1"] = item.Remarks1 == null ? "" : item.Remarks1.ToString().Trim();
                                workRow["Remarks2"] = item.Remarks2 == null ? "" : item.Remarks2.ToString().Trim();
                                workRow["Remarks3"] = item.Remarks3 == null ? "" : item.Remarks3.ToString().Trim();
                                workRow["Remarks4"] = item.Remarks4 == null ? "" : item.Remarks4.ToString().Trim();
                                workRow["Remarks5"] = item.Remarks5 == null ? "" : item.Remarks5.ToString().Trim();
                                workRow["RemarksTitle1"] = item.RemarksTitle1 == null ? "" : item.RemarksTitle1.ToString().Trim();
                                workRow["RemarksTitle2"] = item.RemarksTitle2 == null ? "" : item.RemarksTitle2.ToString().Trim();
                                workRow["RemarksTitle3"] = item.RemarksTitle3 == null ? "" : item.RemarksTitle3.ToString().Trim();
                                workRow["RemarksTitle4"] = item.RemarksTitle4 == null ? "" : item.RemarksTitle4.ToString().Trim();
                                workRow["RemarksTitle5"] = item.RemarksTitle5 == null ? "" : item.RemarksTitle5.ToString().Trim();
                                workRow["RemarksColor1"] = item.RemarksColor1 == null ? "" : item.RemarksColor1.ToString().Trim();
                                workRow["RemarksColor2"] = item.RemarksColor2 == null ? "" : item.RemarksColor2.ToString().Trim();
                                workRow["RemarksColor3"] = item.RemarksColor3 == null ? "" : item.RemarksColor3.ToString().Trim();
                                workRow["RemarksColor4"] = item.RemarksColor4 == null ? "" : item.RemarksColor4.ToString().Trim();
                                workRow["RemarksColor5"] = item.RemarksColor5 == null ? "" : item.RemarksColor5.ToString().Trim();
                                workRow["ItemCode"] = item.ItemCode == null ? "" : item.ItemCode.ToString().Trim();
                                workRow["ItemName"] = item.ItemName == null ? "" : item.ItemName.ToString().Trim();
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
                                workRow["InputOptionItem"] = item.InputOptionItem == null ? "" : item.InputOptionItem.ToString().Trim();
                                workRow["DefaultValue"] = item.DefaultValue == null ? "" : item.DefaultValue.ToString().Trim();
                                workRow["ReadOnly"] = item.ReadOnly;
                                workRow["DetailOption"] = item.DetailOption == null ? "" : item.DetailOption.ToString().Trim();
                                workRow["ItemListDisplayOrder"] = item.ItemListDisplayOrder;

                                var val = "";
                                if (item.InputType.ToLower() == "checkbox")
                                {
                                    var getvalue = tupleForm.formGroupcheckbox.Where(x => x.IsChecked == true
                                    && x.OperationCode.Trim() == item.OperationCode.Trim()
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
                                        var getvalue = tupleForm.formGroupradio.Where(x => x.ID == displayorder && x.ValueCode.Trim() == item.valueold.Trim()).FirstOrDefault();
                                        val = Convert.ToString(getvalue.ID);
                                    }

                                }
                                else if (item.InputType.ToLower() == "checkspe")
                                {
                                    string displayorder = frm[item.ItemCode];
                                    if (displayorder != null)
                                    {
                                        var getvalue = tupleForm.formGroupspecial.Where(x => x.ID == displayorder && x.ValueCode.Trim() == item.valueold.Trim()).FirstOrDefault();
                                        val = Convert.ToString(getvalue.ID);
                                    }

                                }
                                else if (item.InputType.ToLower() == "cbandtxt")
                                {
                                    string displayorder = frm[item.ItemCode];
                                    if (displayorder != null)
                                    {
                                        var getvalue = tupleForm.formGroupspecial.Where(x => x.ID == displayorder && x.ValueCode.Trim() == item.valueold.Trim()).FirstOrDefault();
                                        val = Convert.ToString(getvalue.ID);
                                    }

                                }
                                else
                                {

                                    val = item.ValueCode == null ? "" : item.ValueCode.ToString().Trim();
                                }
                                workRow["FinalResult"] = val;
                                workRow["AddDate"] = DateTime.Now;
                                workRow["UpdDate"] = DateTime.Now;
                                workRow["UserName"] = CreateBy;
                                workRow["ComputerName"] = ComputerName;
                                workRow["ValueCode"] = item.valueold == null ? "" : item.valueold.ToString().Trim();

                                dataTable.Rows.Add(workRow);
                            }

                        }
                    }

                    return new Tuple<DataTable>(dataTable);
                }
            }
            catch(Exception ex)
            {
                var res = ex.Message;
                return new Tuple<DataTable>(null);
            }
          
             
        }


        public dynamic GetTableConst(string strObj, string strObMode, string OperationCode, string OpeGroupCateg, string InputKind, string OperationNo, string UserName)
        {
                     
           // DataTable dataTable = DatatableItem();
            // var dp = new ConnForm(_configuration);
            using (DataTable dataTable = this.DatatableItem())
            {
                var data = _IConnForm.GetGenerate(strObj, strObMode, OperationCode, OpeGroupCateg, InputKind, OperationNo, dataTable, UserName);

                var tabletype = data.formOperationItemLists.Where(i => i.InputType == "table").OrderBy(x => x.ItemListDisplayOrder).Select(x => new
                {
                    InputItemCode = x.ItemCode.Trim(),
                    x.ItemListDisplayOrder,
                    ValueCode = x.valueold.Trim(),

                }).ToList();

                if (OperationNo != "" && OperationNo != null)
                {
                    var allcol = _context.OperationItemValueTableDetail.Where(p => p.OperationNo.Trim() == OperationNo.Trim()).ToList();
                    var allItem = allcol.Select(i => i.InputItemCode).Distinct();

                    List<ListTypeTableModel> newlist = new List<ListTypeTableModel>();

                    foreach (var item in allItem)
                    {
                        ListTypeTableModel newitem = new ListTypeTableModel();
                        var listdata = allcol.Where(i => i.InputItemCode == item).Select(p => new Models.Listdatas()
                        {
                            //[ValueTable].id
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



        }
    
        public dynamic GetCol(string code)
        {
            try
            {
                var allcol = _context.ValueTable.Where(p => p.TableCode.Trim() == code.Trim()).Select(p => new ValueTable()
                {
                    Value = p.Value,                    
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

        public async Task<dynamic> GetValueList(string ValueCode)
        {
            if (ValueCode == " ")
                return null;

            var listitems = await _context.ValueList.Where(i => i.ValueCode == ValueCode).Select(i => new {
                id = i.ValueText,
                text = i.ValueText,
                display = i.DisplayOrder
            }).Distinct().OrderBy(x=>x.display).ToListAsync();

            return listitems;
        }

        public dynamic GetFormCondition(string OperationNo, string OperationCode)
        {
            if (OperationNo == "" || OperationNo == null)
            {
                var flow = _context.OperationCondition.Where(i => i.OperationCode == OperationCode).ToList();
                return flow;
            }
            else
            {
                var flow = _context.OperationConditionHist.Where(i => i.OperationCode == OperationCode && i.OperationNo == OperationNo).ToList();
                return flow;
            }

        }

        public dynamic GetSummaryReport(string OperationCode, string StartDate, string EndDate)
        {           
            //var dp = new ConnForm(_configuration);
            var data = _IConnForm.GetSummaryReport(OperationCode, StartDate, EndDate);
            //data.Columns.RemoveAt(0);
            var JsonString = ConvertToJson(data);          
            return JsonString;
        }
        private dynamic ConvertToJson(DataTable datain)
        {
            string JSONresult;
            JSONresult = JsonConvert.SerializeObject(datain);
            dynamic json = JsonConvert.DeserializeObject(JSONresult);
            return JSONresult;
        }
    }
}
