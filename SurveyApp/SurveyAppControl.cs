using SmartOffice.SurveyApp.IResponsitory;
using SmartOffice.SurveyApp.ModelsManagementControl;
using SmartOffice.SurveyApp.ViewModel;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Data;

using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Text;

namespace SmartOffice.SurveyApp
{
    public class SurveyAppControl : ISurveyApp
    {
        private readonly ManagementControlContext _context;
        private readonly IConfiguration _configuration;
   
        public SurveyAppControl(ManagementControlContext context, IConfiguration configuration)
        {
            _configuration = configuration;
            _context = context;
        }
        public async Task<FormModel> GetQuestions(string CateId, string Username)
       {

            FormModel Surveyform;
            try
            {
                Surveyform = new FormModel();
                var CheckOperationCode = _context.SupplementItemCateg.Where(i => i.Ztcno == Username && i.ItemCateg == CateId).FirstOrDefault();


                if (CheckOperationCode == null)
                    CheckOperationCode = _context.SupplementItemCateg.Where(i => i.Ztcno == "*" && i.ItemCateg == CateId).FirstOrDefault();

                if (CheckOperationCode == null)
                    return Surveyform;

                var OperationName = _context.Operation.Where(i => i.OperationCode == CheckOperationCode.OperationCode).FirstOrDefault(); ;
                Surveyform.ItemCateg = CateId;
                Surveyform.OperationCode = CheckOperationCode.OperationCode;
                Surveyform.OperationName = OperationName.OperationName;

                //var CheckExitData = _context.SupplementItemDetail.Where(i => i.Ztcno == Username && i.KeyTime.Date == DateTime.Now.Date && i.OperationCode == CheckOperationCode.OperationCode).FirstOrDefault();
                var ItemCate = await _context.ItemCategory.Where(i => i.ItemCateg == CateId).Select(i => new ViewItemCategory
                {

                    ItemCateg = i.ItemCateg,
                    ItemCategName = i.ItemCategName,
                    Remarks1 = i.Remarks1,
                    Remarks2 = i.Remarks2,
                    Remarks3 = i.Remarks3,
                    Remarks4 = i.Remarks4,
                    Remarks5 = i.Remarks5,
                    RemarksTitle1 = i.RemarksTitle1,
                    RemarksTitle2 = i.RemarksTitle2,
                    RemarksTitle3 = i.RemarksTitle3,
                    RemarksTitle4 = i.RemarksTitle4,
                    RemarksTitle5 = i.RemarksTitle5,
                    RemarksColor1 = i.RemarksColor1,
                    RemarksColor2 = i.RemarksColor2,
                    RemarksColor3 = i.RemarksColor3,
                    RemarksColor4 = i.RemarksColor4,
                    RemarksColor5 = i.RemarksColor5,
                    AddDate = i.AddDate,
                    UpdDate = i.UpdDate,
                    UserName = i.UserName,
                    ComputerName = i.ComputerName
                }).FirstOrDefaultAsync();
                Surveyform.ItemCategory = ItemCate;




                //if (CheckExitData != null)
                //{
                //    Surveyform.OperatorType = "Update";
                //    var ItemsInput = await _context.VewSupplementItemDetail.Where(i => i.Ztcno == Username
                //    && i.KeyTime.Date == DateTime.Now.Date && i.OperationCode == CheckOperationCode.OperationCode
                //    && i.ItemCateg == CateId).Select(e => new ViewInputItem
                //    {
                //        ItemCode = e.ItemCode,
                //        ItemName = e.ItemName,
                //        InputType = e.InputType,
                //        DataType = e.DataType,
                //        DecimalNo = e.DecimalNo,
                //        Unit = e.Unit,
                //        ValueCode = e.ValueCode,
                //        InputOption = e.InputOption,
                //        DefaultValue = e.DefaultValue,
                //        Calculation = e.Calculation,
                //        ReadOnly = e.ReadOnly,
                //        DisplayOrder = e.DisplayOrder,
                //        SupplementId = e.SupplementId,
                //        FinalResult = e.FinalResult,
                //        KeyTime = e.KeyTime,
                //        AddDate = e.AddDate,
                //        UpdDate = e.UpdDate
                //    }).OrderBy(i => i.DisplayOrder).ToListAsync();

                //    Surveyform.ItemCategory.InputItems = ItemsInput;
                //    Surveyform.KeyTime = ItemsInput.FirstOrDefault().KeyTime.Date.ToShortDateString();
                //}
                //else
                //{
                Surveyform.OperatorType = "Insert";
                Surveyform.KeyTime = DateTime.Now.Date.ToShortDateString();

                if (ItemCate != null)
                {

                    var listItemOperator = _context.VewMaster_Supplement_Item.Where(i => i.OperationCode == Surveyform.OperationCode && i.Ztcno == Username).Select(i => i.ItemCode).ToList();

                    if (listItemOperator.Count() == 0)
                        listItemOperator = _context.VewMaster_Supplement_Item.Where(i => i.OperationCode == Surveyform.OperationCode && i.Ztcno == "*").Select(i => i.ItemCode).ToList();

                    var ItemsInput = await _context.InputItemList.Where(i => listItemOperator.Contains(i.ItemCode))
                        .Join(_context.InputItem,
                              p => p.ItemCode,
                              e => e.ItemCode,
                              (p, e) => new ViewInputItem
                              {
                                  ItemCode = e.ItemCode,
                                  ItemName = e.ItemName,
                                  InputType = e.InputType,
                                  DataType = e.DataType,
                                  DecimalNo = e.DecimalNo,
                                  Unit = e.Unit,
                                  ValueCode = e.ValueCode,
                                  InputOption = e.InputOption,
                                  DefaultValue = e.DefaultValue,
                                  Calculation = e.Calculation,
                                  ReadOnly = e.ReadOnly,
                                  AddDate = e.AddDate,
                                  UpdDate = e.UpdDate,
                                  UserName = e.UserName,
                                  ComputerName = e.ComputerName,
                                  DisplayOrder = p.DisplayOrder
                              }).OrderBy(i => i.DisplayOrder).ToListAsync();

                    Surveyform.ItemCategory.InputItems = ItemsInput;
                }
                //}


                //List<ValueList> ListValuList = _context.ValueList.ToList();
                foreach (var itemvalue in Surveyform.ItemCategory.InputItems)
                {
                    if (itemvalue.ValueCode != null)
                    {
                        var ListValuList = _context.ValueList.Where(i => i.ValueCode == itemvalue.ValueCode).Select(i => new ViewValueList
                        {
                            ValueCode = i.ValueCode,
                            Value = i.Value,
                            ValueText = i.ValueText,
                            DisplayOrder = i.DisplayOrder,
                            AddDate = i.AddDate,
                            UpdDate = i.UpdDate,
                            UserName = i.UserName,
                            ComputerName = i.ComputerName

                        }).OrderBy(i => i.DisplayOrder).ToList();


                        itemvalue.ValueLists = ListValuList;
                    }

                }

                return Surveyform;
            }
            catch
            {
                Surveyform = new FormModel();
                return Surveyform;
            }
            finally
            {
                Surveyform=null;
            }
          
        }
        public async Task<bool> SaveFormData(IFormCollection form, string Username, string CreateName, string ComputerName)
        {

            FormModel Formdata;
            try
            {
                Formdata = new FormModel();
                string CateId = form["ItemCateg"];
                string OperatorType = form["OperatorType"];
                string OperationCode = form["OperationCode"];
                //string KeyTime = form["KeyTime"];
                var inputdate = DateTime.Now.Date;
                bool CheckCVD004 = form.Any(i => i.Key == "CVD004");


                if (CheckCVD004 == true)
                    inputdate = Convert.ToDateTime(form.Where(i => i.Key == "CVD004").First().Value);


             
                if (OperatorType != "Update")
                {
                    Formdata = await this.GetQuestions(CateId, Username);

                }
                else
                {
                    Formdata = await this.GetQuestionsUpdate(CateId, OperationCode, Username, inputdate.Date.ToShortDateString());

                }


                foreach (var formitem in form)
                {
                    if (Formdata.ItemCategory.InputItems.Any(w => w.ItemCode == formitem.Key))
                    {
                        var InputItemsTemp = Formdata.ItemCategory.InputItems.Single(w => w.ItemCode == formitem.Key);
                        InputItemsTemp.FinalResult = formitem.Value;

                    }

                }

                var HistoryForm = _context.VewFormInputHistory.Any(i => i.ZTCNo == Username && Convert.ToDateTime(i.KeyDate).Date == inputdate.Date && i.OperationCode == Formdata.OperationCode);

                if (HistoryForm == false)
                    HistoryForm = _context.VewFormInputHistoryBydate.Any(i => i.ZTCNo == Username && Convert.ToDateTime(i.KeyDate).Date == inputdate.Date && i.OperationCode == Formdata.OperationCode);

                if (Formdata.OperatorType == "Insert" && HistoryForm == false)
                {

                    var ItemInput = Formdata.ItemCategory.InputItems.Select(i => new SupplementItemDetail
                    {

                        SupplementId = i.SupplementId,
                        OperationCode = OperationCode,
                        OperationName = Formdata.OperationName,
                        Ztcno = Username,
                        Ztcname = CreateName,
                        ItemCode = i.ItemCode,
                        ItemName = i.ItemName,
                        KeyTime = inputdate,
                        FinalResult = i.FinalResult,
                        AddDate = DateTime.Now,
                        UpdDate = DateTime.Now,
                        UserName = Username,
                        ComputerName = ComputerName
                    });
                    _context.SupplementItemDetail.AddRange(ItemInput);



                }
                else
                {
                    var AllSupplementId = Formdata.ItemCategory.InputItems.Select(i => i.SupplementId);
                    var ListSupplementItemDetail = _context.SupplementItemDetail.Where(i => AllSupplementId.Contains(i.SupplementId)).ToList();


                    foreach (var item in ListSupplementItemDetail)
                    {
                        var listitem = Formdata.ItemCategory.InputItems.Where(i => i.SupplementId == item.SupplementId).FirstOrDefault();
                        item.KeyTime = inputdate;
                        item.UpdDate = DateTime.Now;
                        item.FinalResult = listitem.FinalResult;
                    }
                    //var ItemInput =Formdata.ItemCategory.InputItems.Select(i => new SupplementItemDetail
                    //{
                    //    SupplementId = i.SupplementId,
                    //    OperationCode = OperationCode,
                    //    OperationName = Formdata.OperationName,
                    //    Ztcno = Username,
                    //    Ztcname = CreateName,
                    //    ItemCode = i.ItemCode,
                    //    ItemName = i.ItemName,
                    //    KeyTime = i.KeyTime,
                    //    FinalResult = i.FinalResult,
                    //    AddDate = i.AddDate,
                    //    UpdDate = DateTime.Now,
                    //    UserName = Username,
                    //    ComputerName = ComputerName
                    //}).ToList(); ;
                    _context.SupplementItemDetail.UpdateRange(ListSupplementItemDetail);

                }
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                var aa = ex.Message;
                Formdata = null;
                return false;
            }
            finally
            {
                Formdata = null;
            }
        }
        public async Task<OperatorModel> GetOperatorDataAsync(string Username)
        {
            OperatorModel OperatorData;
            try
            {
                OperatorData = new OperatorModel();
                var SupplementOperations = await _context.SupplementItemCateg.Where(i => i.Ztcno == Username)
                   .Join(_context.Operation,
                         p => p.OperationCode,
                         e => e.OperationCode,
                         (p, e) => new SupplementOperation
                         {
                             OperationCode = p.OperationCode,
                             ItemCateg = p.ItemCateg,
                             InputOption = p.InputOption,
                             DisplayOrder = p.DisplayOrder,
                             OperationName = e.OperationName,
                             OpeGroupCode = e.OpeGroupCode,
                             InputKind = e.InputKind,

                         }).OrderBy(i => i.DisplayOrder).ToListAsync();


                var SupplementOperations1 = await _context.SupplementItemCateg.Where(i => i.Ztcno == "*")
                      .Join(_context.Operation,
                            p => p.OperationCode,
                            e => e.OperationCode,
                            (p, e) => new SupplementOperation
                            {
                                OperationCode = p.OperationCode,
                                ItemCateg = p.ItemCateg,
                                InputOption = p.InputOption,
                                DisplayOrder = p.DisplayOrder,
                                OperationName = e.OperationName,
                                OpeGroupCode = e.OpeGroupCode,
                                InputKind = e.InputKind,

                            }).OrderBy(i => i.DisplayOrder).ToListAsync();

                var checkcon = SupplementOperations.Select(i => i.OperationCode);

                var uniqueList = SupplementOperations1.Where(i => !checkcon.Contains(i.OperationCode)).ToList();

                SupplementOperations.AddRange(uniqueList);

                OperatorData.SupplementOperations = SupplementOperations;

                return OperatorData;
            }
            catch
            {
                return null;
            }
            finally
            {
                OperatorData = null;
            }
           
        }
        public async Task<List<VewFormInputHistory>> GetHistoryInputFormAsync(string Username)
        {
            try
            {
                var HistoryForm = await _context.VewFormInputHistory.Where(i => i.ZTCNo == Username).OrderByDescending(i=>i.KeyDate).Take(100).ToListAsync();

                var HistoryFormBydate = await _context.VewFormInputHistoryBydate.Where(i => i.ZTCNo == Username).OrderByDescending(i => i.KeyDate).Take(100).ToListAsync();
                foreach (var item in HistoryFormBydate)
                {
                    var newitem = new VewFormInputHistory()
                    {
                        ItemCateg= item.ItemCateg,
                        KeyDate =item.KeyDate,
                        OperationCode = item.OperationCode,
                        OperationName =item.OperationName,
                        ZTCNo = item.ZTCNo
                    };
                    HistoryForm.Add(newitem);
                }

                return HistoryForm;
            }
            catch (Exception ex)
            {
                var aa = ex.Message;
                return null;
            }
            
        }

        public async Task<FormModel> GetQuestionsUpdate(string ItemCateNo, string OperationCode, string ZTCNo, string KeyDate)
        {
            FormModel Surveyform = new FormModel();

            SupplementItemCateg CheckOperationCode = new SupplementItemCateg();
            try
            {
                CheckOperationCode = _context.SupplementItemCateg.Where(i => i.Ztcno == ZTCNo && i.ItemCateg == ItemCateNo).FirstOrDefault();

                if (CheckOperationCode == null)
                    CheckOperationCode = _context.SupplementItemCateg.Where(i => i.Ztcno == "*" && i.ItemCateg == ItemCateNo).FirstOrDefault();


                if (CheckOperationCode == null)
                    return Surveyform;

                var OperationName = _context.Operation.Where(i => i.OperationCode == CheckOperationCode.OperationCode).FirstOrDefault(); ;
                Surveyform.ItemCateg = ItemCateNo;
                Surveyform.OperatorType = "Update";
                Surveyform.OperationCode = CheckOperationCode.OperationCode;
                Surveyform.OperationName = OperationName.OperationName;
                var inputdate = Convert.ToDateTime(KeyDate);
                var CheckExitData = _context.SupplementItemDetail.Where(i => i.Ztcno == ZTCNo && i.KeyTime.Date == inputdate.Date && i.OperationCode == CheckOperationCode.OperationCode).FirstOrDefault();
                if (CheckExitData == null)
                {
                    return await this.GetQuestions(ItemCateNo, ZTCNo);
                }


                var ItemCate = await _context.ItemCategory.Where(i => i.ItemCateg == ItemCateNo).Select(i => new ViewItemCategory
                {

                    ItemCateg = i.ItemCateg,
                    ItemCategName = i.ItemCategName,
                    Remarks1 = i.Remarks1,
                    Remarks2 = i.Remarks2,
                    Remarks3 = i.Remarks3,
                    Remarks4 = i.Remarks4,
                    Remarks5 = i.Remarks5,
                    RemarksTitle1 = i.RemarksTitle1,
                    RemarksTitle2 = i.RemarksTitle2,
                    RemarksTitle3 = i.RemarksTitle3,
                    RemarksTitle4 = i.RemarksTitle4,
                    RemarksTitle5 = i.RemarksTitle5,
                    RemarksColor1 = i.RemarksColor1,
                    RemarksColor2 = i.RemarksColor2,
                    RemarksColor3 = i.RemarksColor3,
                    RemarksColor4 = i.RemarksColor4,
                    RemarksColor5 = i.RemarksColor5,
                    AddDate = i.AddDate,
                    UpdDate = i.UpdDate,
                    UserName = i.UserName,
                    ComputerName = i.ComputerName
                }).FirstOrDefaultAsync();
                Surveyform.ItemCategory = ItemCate;

                var ItemsInput = await _context.VewSupplementItemDetail.Where(i => i.Ztcno == ZTCNo
                && i.KeyTime.Date == inputdate.Date && i.OperationCode == CheckOperationCode.OperationCode
                && i.ItemCateg == ItemCateNo).Select(e => new ViewInputItem
                {
                    ItemCode = e.ItemCode,
                    ItemName = e.ItemName,
                    InputType = e.InputType,
                    DataType = e.DataType,
                    DecimalNo = e.DecimalNo,
                    Unit = e.Unit,
                    ValueCode = e.ValueCode,
                    InputOption = e.InputOption,
                    DefaultValue = e.DefaultValue,
                    Calculation = e.Calculation,
                    ReadOnly = e.ReadOnly,
                    DisplayOrder = e.DisplayOrder,
                    SupplementId = e.SupplementId,
                    FinalResult = e.FinalResult,
                    KeyTime = e.KeyTime,
                    AddDate = e.AddDate,
                    UpdDate = e.UpdDate
                }).OrderBy(i => i.DisplayOrder).ToListAsync();

                Surveyform.ItemCategory.InputItems = ItemsInput;
                Surveyform.KeyTime = ItemsInput.FirstOrDefault().KeyTime.Date.ToShortDateString();


                //List<ValueList> ListValuList = _context.ValueList.ToList();
                foreach (var itemvalue in Surveyform.ItemCategory.InputItems)
                {
                    if (itemvalue.ValueCode != null)
                    {
                        var ListValuList = _context.ValueList.Where(i => i.ValueCode == itemvalue.ValueCode).Select(i => new ViewValueList
                        {
                            ValueCode = i.ValueCode,
                            Value = i.Value,
                            ValueText = i.ValueText,
                            DisplayOrder = i.DisplayOrder,
                            AddDate = i.AddDate,
                            UpdDate = i.UpdDate,
                            UserName = i.UserName,
                            ComputerName = i.ComputerName

                        }).OrderBy(i => i.DisplayOrder).ToList();
                        itemvalue.ValueLists = ListValuList;
                    }
                }


                return Surveyform;
            }
            catch
            {
                return null;
            }
            finally
            {
                Surveyform = null;
                CheckOperationCode = null;

            }
             
        }
        public async Task<dynamic> DeleteFormData(string ItemCateNo, string OperationCode, string ZTCNo, string KeyDate)
        {
            try
            {
                var Data = new { status = true, subject = "Delete Data", detail = "Delete data is complete." };
                var inputdate = Convert.ToDateTime(KeyDate);
                var CheckExitData = await _context.SupplementItemDetail.Where(i => i.Ztcno == ZTCNo && i.KeyTime.Date == inputdate.Date && i.OperationCode == OperationCode).ToListAsync();
                _context.SupplementItemDetail.RemoveRange(CheckExitData);
                _context.SaveChanges();
                return Data;
            }
            catch
            {
                var Data = new { status = true, subject = "Delete Data", detail = "Delete data is not complete!!!." };
                return Data;
            }
            
        }
        public DataTable GetReportDailyCheck(string Dept, string Dept2, string Dept3, string EmpNo)
        {
            DataTable dt = new DataTable();
            try
            {

                string constr = _configuration.GetConnectionString("DefaultConnection7");
                SqlConnection conn = new SqlConnection(constr);
                SqlCommand objCmd = new SqlCommand();
                var strStored = "";
                strStored = "sprGetEmployeeCovidCheckList";
                objCmd.Parameters.Add(new SqlParameter("@DepName", Dept));
                objCmd.Parameters.Add(new SqlParameter("@DepName2", Dept2));
                objCmd.Parameters.Add(new SqlParameter("@DepName3", Dept3));
                objCmd.Parameters.Add(new SqlParameter("@EmployeeID", EmpNo));
                conn.Open();
                objCmd.Connection = conn;
                objCmd.CommandText = strStored;
                objCmd.CommandType = CommandType.StoredProcedure;
                DataSet ds = new DataSet();
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        con.Open();
                        SqlDataAdapter adpterdata = new SqlDataAdapter
                        {
                            SelectCommand = objCmd
                        };
                        adpterdata.Fill(dt);
                        con.Close();
                        return dt;
                    }
                }
            }
            catch (Exception e)
            {
                var dsa = e;
                return dt;
            }
            finally
            {
                dt.Dispose();
            }
        }
        public string DataTableToJSONWithStringBuilder(DataTable table)
        {
            var JSONString = new StringBuilder();
            if (table.Rows.Count > 0)
            {
                JSONString.Append("[");
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    JSONString.Append("{");
                    for (int j = 0; j < table.Columns.Count; j++)
                    {
                        if (j < table.Columns.Count - 1)
                        {
                            JSONString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + table.Rows[i][j].ToString() + "\",");
                        }
                        else if (j == table.Columns.Count - 1)
                        {
                            JSONString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + table.Rows[i][j].ToString() + "\"");
                        }
                    }
                    if (i == table.Rows.Count - 1)
                    {
                        JSONString.Append("}");
                    }
                    else
                    {
                        JSONString.Append("},");
                    }
                }
                JSONString.Append("]");
            }
            return JSONString.ToString();
        }
        public DataTable ReportDailyCheck(string Dept, string Dept2, string Dept3, string EmpNo)
        {
            var receivedata = this.GetReportDailyCheck(Dept, Dept2, Dept3, EmpNo);

            return receivedata;
        }

       
       
    }
}
