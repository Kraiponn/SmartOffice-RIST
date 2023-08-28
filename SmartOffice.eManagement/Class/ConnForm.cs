using Microsoft.Extensions.Configuration;
using SmartOffice.eManagement.IResponsitory;
using SmartOffice.eManagement.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace SmartOffice.eManagement.Class
{
    public class ConnForm: IConnForm
    {
        private readonly IConfiguration configuration;
        private readonly string constr;

        public ConnForm(IConfiguration configuration)
        {
            this.configuration = configuration;
            constr = configuration.GetConnectionString("DefaultConnection7");
        }


        public TupleForm GetGroup(string OpeGroupCateg, string Division, string Section, string Department, string Department2, string Department3, string username, string SpecialGroup)
        {

            TupleForm Tuple;
            List<FormOperationGroup> result01 = new List<FormOperationGroup>();
            try
            {
                using (SqlConnection conn = new SqlConnection(constr))
                {
                    using (SqlCommand objCmd = new SqlCommand())
                    {
                        Tuple = new TupleForm();
                        var strStored = "";
                        strStored = "sprDbOperationGroup";
                        objCmd.Parameters.Add(new SqlParameter("@OpeGroupCateg", OpeGroupCateg));
                        objCmd.Parameters.Add(new SqlParameter("@Division", Division));
                        objCmd.Parameters.Add(new SqlParameter("@Section", Section));
                        objCmd.Parameters.Add(new SqlParameter("@Department", Department));
                        objCmd.Parameters.Add(new SqlParameter("@Department2", Department2));
                        objCmd.Parameters.Add(new SqlParameter("@Department3", Department3));
                        objCmd.Parameters.Add(new SqlParameter("@UserName", username));
                        objCmd.Parameters.Add(new SqlParameter("@SpecialGroup", SpecialGroup));
                        conn.Open();
                        objCmd.Connection = conn;
                        objCmd.CommandText = strStored;
                        objCmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader rdr = objCmd.ExecuteReader())
                        {
                            if (rdr != null)
                            {
                                while (rdr.Read())
                                {
                                    result01.Add(new FormOperationGroup
                                    {
                                        strObj = rdr["strObj"].ToString(),
                                        OperationCode = rdr["OperationCode"].ToString(),
                                        OperationName = rdr["OperationName"].ToString(),
                                        DisplayOrder = rdr["DisplayOrder"].ToString(),
                                        InputKind = rdr["InputKind"].ToString(),
                                        OpeGroupCode = rdr["OpeGroupCode"].ToString(),
                                        OpeGroupName = rdr["OpeGroupName"].ToString(),
                                        OpeGroupCateg = rdr["OpeGroupCateg"].ToString(),
                                        DisplayPriority = rdr["DisplayPriority"].ToString(),
                                        SpecialOperate = rdr["SpecialOperate"].ToString(),
                                        SpecialGroup = rdr["SpecialGroup"].ToString()
                                    });
                                }
                                Tuple.formOperationGroups = result01;
                            }
                        }
                        conn.Close();
                        objCmd.Parameters.Clear();
                        return Tuple;
                    }
                }
            }
            catch
            {
                Tuple = null;
                result01 = null;
                return null;
            }
            finally
            {
                Tuple = null;
                result01 = null;
            }
           
        }                     
        public TupleForm GetInquiry(string strObj, string strObMode,string strObjSub, string OperationCode, string OpeGroupCateg, string InputKind, string OperationNo, DataTable dataTable,string UserName)
        {

            List<FormOperationItems> result01 = new List<FormOperationItems>();
            List<vewHRMSEmployeeSurveyPIC> result02 = new List<vewHRMSEmployeeSurveyPIC>();
            List<vewNumberofinfectedPersonsSum> result03 = new List<vewNumberofinfectedPersonsSum>();
            List<vewOperationItemList_Result_CheckOperationNo> result04 = new List<vewOperationItemList_Result_CheckOperationNo>();
            try
            {
                using (SqlConnection conn = new SqlConnection(constr))
                {
                    using (SqlCommand objCmd = new SqlCommand())
                    {
                        var strStored = "";
                        var Tuple = new TupleForm();

                        strStored = "sprDbInputChk";
                        objCmd.Parameters.Add(new SqlParameter("@strObj", strObj));
                        objCmd.Parameters.Add(new SqlParameter("@strObMode", strObMode));
                        objCmd.Parameters.Add(new SqlParameter("@strObjSub", strObjSub));
                        objCmd.Parameters.Add(new SqlParameter("@OperationCode", OperationCode));
                        objCmd.Parameters.Add(new SqlParameter("@OpeGroupCateg", OpeGroupCateg));
                        objCmd.Parameters.Add(new SqlParameter("@OperationNo", OperationNo));
                        objCmd.Parameters.Add(new SqlParameter("@InputKind", InputKind));
                        objCmd.Parameters.Add(new SqlParameter("@UserName", UserName));
                        objCmd.Parameters.Add(new SqlParameter("@TableOperationItemList", SqlDbType.Structured)
                        {
                            Value = dataTable,
                            TypeName = "dbo.TableOperationItemList"
                        });
                        objCmd.Parameters.Add(new SqlParameter("@_Message", SqlDbType.NVarChar, 200)
                        {
                            Value = "",
                            Direction = ParameterDirection.Output
                        });
                        objCmd.Parameters.Add(new SqlParameter("@_OperationNo", SqlDbType.Char, 25)
                        {
                            Value = "",
                            Direction = ParameterDirection.Output
                        });

                        conn.Open();
                        objCmd.Connection = conn;
                        objCmd.CommandText = strStored;
                        objCmd.CommandType = CommandType.StoredProcedure;

                  

                        using (SqlDataReader rdr = objCmd.ExecuteReader())
                        {
                            if (rdr != null)
                            {
                                while (rdr.Read())
                                {
                                    result01.Add(new FormOperationItems
                                    {

                                        OperationNo = rdr["OperationNo"].ToString(),
                                        OperationCode = rdr["OperationCode"].ToString(),
                                        OperationName = rdr["OperationName"].ToString(),
                                        InputKind = rdr["InputKind"].ToString(),
                                        OpeGroupCode = rdr["OpeGroupCode"].ToString(),
                                        OpeGroupName = rdr["OpeGroupName"].ToString(),
                                        OpeGroupCateg = rdr["OpeGroupCateg"].ToString(),
                                        UpdDate = Convert.ToDateTime(rdr["UpdDate"].ToString()),
                                        UserName = rdr["UserName"].ToString(),
                                        FinalResult = rdr["FinalResult"].ToString()

                                    });
                                }
                                Tuple.formOperationItems = result01;

                                if (rdr.NextResult())
                                {
                                    while (rdr.Read())
                                    {
                                        result02.Add(new vewHRMSEmployeeSurveyPIC
                                        {
                                            CODEMPID = rdr["CODEMPID"].ToString(),
                                            NAMEMPT = rdr["NAMEMPT"].ToString(),
                                            NAMEMPE = rdr["NAMEMPE"].ToString(),
                                            DIVISION = rdr["DIVISION"].ToString(),
                                            SECTION = rdr["SECTION"].ToString(),
                                            DEPARTMENT = rdr["DEPARTMENT"].ToString(),
                                            DEPARTMENT2 = rdr["DEPARTMENT2"].ToString(),
                                            DEPARTMENT3 = rdr["DEPARTMENT3"].ToString(),
                                            INACTIVE = rdr["INACTIVE"].ToString(),
                                            SurveyPICCode = rdr["SurveyPICCode"].ToString(),
                                            PICCode = rdr["PICCode"].ToString(),
                                            PICCodeName = rdr["PICCodeName"].ToString(),
                                            Position = rdr["Position"].ToString()
                                        });
                                    }
                                    Tuple.vewHRMSEmployeeSurveyPICs = result02;
                                }

                                if (rdr.NextResult())
                                {
                                    while (rdr.Read())
                                    {
                                        result03.Add(new vewNumberofinfectedPersonsSum
                                        {
                                            OperationCode = rdr["OperationCode"].ToString(),
                                            SurveyPICCode = rdr["SurveyPICCode"].ToString(),
                                            Dept = rdr["Dept"].ToString(),
                                            Keydata = rdr["Keydata"].ToString(),
                                            Result = rdr["Result"].ToString(),

                                        });
                                    }
                                    Tuple.vewNumberofinfectedPersonsSums = result03;
                                }

                                if (rdr.NextResult())
                                {
                                    while (rdr.Read())
                                    {
                                        result04.Add(new vewOperationItemList_Result_CheckOperationNo
                                        {
                                            OperationNo = rdr["OperationNo"].ToString(),
                                            FinalResult = rdr["FinalResult"].ToString(),

                                        });
                                    }
                                    Tuple.vewOperationItemList_Result_CheckOperationNos = result04;
                                }
                            }
                        }
                        conn.Close();
                        objCmd.Parameters.Clear();
                        return Tuple;
                    }
                }
            }
            catch (Exception e)
            {
                var xxx = e.Message.ToString();
                result01 = null;
                result02 = null;
                result03 = null;
                result03 = null;
                return null;
            }
            finally
            {
                result01 = null;
                result02 = null;
                result03 = null;
                result03 = null;
            }

          
         
        }
        public DataTable GetReport(string strObj, string strObMode, string OperationCode, string OpeGroupCateg, string InputKind, string OperationNo, DataTable dataTable, string UserName)
        {
            DataTable dt = new DataTable();
           
        
            try
            {
                using (SqlConnection conn = new SqlConnection(constr))
                {
                    using (SqlCommand objCmd = new SqlCommand())
                    {

                        var strStored = "";
                        strStored = "sprDbInputChk";
                        objCmd.Parameters.Add(new SqlParameter("@strObj", strObj));
                        objCmd.Parameters.Add(new SqlParameter("@strObMode", strObMode));
                        objCmd.Parameters.Add(new SqlParameter("@strObjSub", ""));
                        objCmd.Parameters.Add(new SqlParameter("@OperationCode", OperationCode));
                        objCmd.Parameters.Add(new SqlParameter("@OpeGroupCateg", OpeGroupCateg));
                        objCmd.Parameters.Add(new SqlParameter("@OperationNo", OperationNo));
                        objCmd.Parameters.Add(new SqlParameter("@InputKind", InputKind));
                        objCmd.Parameters.Add(new SqlParameter("@UserName", UserName));
                        objCmd.Parameters.Add(new SqlParameter("@TableOperationItemList", SqlDbType.Structured)
                        {
                            Value = dataTable,
                            TypeName = "dbo.TableOperationItemList"
                        });
                        objCmd.Parameters.Add(new SqlParameter("@_Message", SqlDbType.NVarChar, 200)
                        {
                            Value = "",
                            Direction = ParameterDirection.Output
                        });
                        objCmd.Parameters.Add(new SqlParameter("@_OperationNo", SqlDbType.Char, 25)
                        {
                            Value = "",
                            Direction = ParameterDirection.Output
                        });

                        objCmd.Connection = conn;
                        objCmd.CommandText = strStored;
                        objCmd.CommandType = CommandType.StoredProcedure;
                        objCmd.CommandTimeout = 180;
                        // SqlDataReader need an open conncetion, so check and open it.
                        if (conn.State != ConnectionState.Open)
                            conn.Open();

                        // Read data by using Execute Reader
                        using (SqlDataReader dr = objCmd.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            dt.Load(dr);
                            conn.Close();
                            objCmd.Parameters.Clear();
                            return dt;
                        }                                                
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
        public DataTable GetSummaryReport(string OperationCode, string StartDate, string EndDate)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            try
            {
              
                using (SqlConnection conn = new SqlConnection(constr))
                {
                    using (SqlCommand objCmd = new SqlCommand())
                    {
                        var strStored = "";
                        strStored = "sprDbSummaryReport";
                        objCmd.Parameters.Add(new SqlParameter("@OperationCode", OperationCode));
                        objCmd.Parameters.Add(new SqlParameter("@StartDate", StartDate));
                        objCmd.Parameters.Add(new SqlParameter("@EndDate", EndDate));
                        conn.Open();
                        objCmd.Connection = conn;
                        objCmd.CommandText = strStored;
                        objCmd.CommandType = CommandType.StoredProcedure;

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
                                adpterdata.Dispose();
                                conn.Close();
                                objCmd.Parameters.Clear();
                                return dt;
                            }
                        }
                    }
                }
                  
            }
            catch (Exception e)
            {
                ds.Dispose();
                dt.Dispose();
                var dsa = e;
                return dt;
            }
            finally
            {
                ds.Dispose();
                dt.Dispose();
            }
        }
        public TupleForm GetGenerate(string strObj, string strObMode, string OperationCode, string OpeGroupCateg, string InputKind, string OperationNo, DataTable dataTable, string UserName)
        {

            List<FormOperationItemList> result01 = new List<FormOperationItemList>();
            List<FormValueList> result02 = new List<FormValueList>();
            TupleForm Tuple;

            try
            {
                Tuple = new TupleForm();
                using (SqlConnection conn = new SqlConnection(constr))
                {
                    using (SqlCommand objCmd = new SqlCommand())
                    {
                        var strStored = "";
                        strStored = "sprDbInputChk";
                        objCmd.Parameters.Add(new SqlParameter("@strObj", strObj));
                        objCmd.Parameters.Add(new SqlParameter("@strObMode", strObMode));
                        objCmd.Parameters.Add(new SqlParameter("@strObjSub", ""));
                        objCmd.Parameters.Add(new SqlParameter("@OperationCode", OperationCode));
                        objCmd.Parameters.Add(new SqlParameter("@OpeGroupCateg", OpeGroupCateg));
                        objCmd.Parameters.Add(new SqlParameter("@OperationNo", OperationNo));
                        objCmd.Parameters.Add(new SqlParameter("@InputKind", InputKind));
                        objCmd.Parameters.Add(new SqlParameter("@UserName", UserName));
                        objCmd.Parameters.Add(new SqlParameter("@TableOperationItemList", SqlDbType.Structured)
                        {
                            Value = dataTable,
                            TypeName = "dbo.TableOperationItemList"
                        });
                        objCmd.Parameters.Add(new SqlParameter("@_Message", SqlDbType.NVarChar, 200)
                        {
                            Value = "",
                            Direction = ParameterDirection.Output
                        });
                        objCmd.Parameters.Add(new SqlParameter("@_OperationNo", SqlDbType.Char, 25)
                        {
                            Value = "",
                            Direction = ParameterDirection.Output
                        });

                        conn.Open();
                        objCmd.Connection = conn;
                        objCmd.CommandText = strStored;
                        objCmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader rdr = objCmd.ExecuteReader())
                        {
                            if (rdr != null)
                            {
                                while (rdr.Read())
                                {
                                    result01.Add(new FormOperationItemList
                                    {

                                        OperationNo = rdr["OperationNo"].ToString(),
                                        OperationCode = rdr["OperationCode"].ToString(),
                                        OperationName = rdr["OperationName"].ToString(),
                                        OperationDisplayOrder = rdr["OperationDisplayOrder"].ToString(),
                                        InputKind = rdr["InputKind"].ToString(),
                                        RoleID = rdr["RoleID"].ToString(),
                                        OpeGroupCode = rdr["OpeGroupCode"].ToString(),
                                        OpeGroupName = rdr["OpeGroupName"].ToString(),
                                        OpeGroupCateg = rdr["OpeGroupCateg"].ToString(),
                                        DisplayPriority = rdr["DisplayPriority"].ToString(),
                                        SpecialOperate = rdr["SpecialOperate"].ToString(),
                                        CategInputOption = rdr["CategInputOption"].ToString(),
                                        CategDisplayOrder = rdr["CategDisplayOrder"].ToString(),
                                        ItemCateg = rdr["ItemCateg"].ToString(),
                                        ItemCategName = rdr["ItemCategName"].ToString(),
                                        Remarks1 = rdr["Remarks1"].ToString(),
                                        Remarks2 = rdr["Remarks2"].ToString(),
                                        Remarks3 = rdr["Remarks3"].ToString(),
                                        Remarks4 = rdr["Remarks4"].ToString(),
                                        Remarks5 = rdr["Remarks5"].ToString(),
                                        RemarksTitle1 = rdr["RemarksTitle1"].ToString(),
                                        RemarksTitle2 = rdr["RemarksTitle2"].ToString(),
                                        RemarksTitle3 = rdr["RemarksTitle3"].ToString(),
                                        RemarksTitle4 = rdr["RemarksTitle4"].ToString(),
                                        RemarksTitle5 = rdr["RemarksTitle5"].ToString(),
                                        RemarksColor1 = rdr["RemarksColor1"].ToString(),
                                        RemarksColor2 = rdr["RemarksColor2"].ToString(),
                                        RemarksColor3 = rdr["RemarksColor3"].ToString(),
                                        RemarksColor4 = rdr["RemarksColor4"].ToString(),
                                        RemarksColor5 = rdr["RemarksColor5"].ToString(),
                                        ItemCode = rdr["ItemCode"].ToString(),
                                        ItemName = rdr["ItemName"].ToString(),
                                        InputType = rdr["InputType"].ToString(),
                                        DataType = rdr["DataType"].ToString(),
                                        DecimalNo = rdr["DecimalNo"].ToString(),
                                        Required = rdr["Required"].ToString(),
                                        Minlength = rdr["Minlength"].ToString(),
                                        Maxlength = rdr["Maxlength"].ToString(),
                                        Min = rdr["Min"].ToString(),
                                        Max = rdr["Max"].ToString(),
                                        Step = rdr["Step"].ToString(),
                                        Unit = rdr["Unit"].ToString(),
                                        ValueCode = (OperationNo == "" ? rdr["ValueCode"].ToString() : rdr["FinalResult"].ToString()),
                                        valueold = rdr["ValueCode"].ToString(),
                                        InputOptionItem = rdr["InputOptionItem"].ToString(),
                                        DefaultValue = rdr["DefaultValue"].ToString(),
                                        ReadOnly = rdr["ReadOnly"].ToString(),
                                        DetailOption = rdr["DetailOption"].ToString(),
                                        ItemListDisplayOrder = rdr["ItemListDisplayOrder"].ToString(),
                                        FinalResult = rdr["FinalResult"].ToString()
                                    });
                                }
                                Tuple.formOperationItemLists = result01;

                                if (rdr.NextResult())
                                {
                                    while (rdr.Read())
                                    {
                                        result02.Add(new FormValueList
                                        {
                                            ValueCode = rdr["ValueCode"].ToString(),
                                            Value = rdr["Value"].ToString(),
                                            ValueText = rdr["ValueText"].ToString(),
                                            DisplayOrder = int.Parse(rdr["DisplayOrder"].ToString()),
                                        });
                                    }
                                    Tuple.formValueLists = result02;
                                }
                            }
                        }
                        conn.Close();
                        objCmd.Parameters.Clear();
                        return Tuple;
                    }
                }
            }
            catch (Exception e)
            {
                var xx = e.Message;
                result01 = null;
                result02 = null;
                Tuple = null;
                return null;
            }
            finally
            {
                result01 = null;
                result02 = null;
                Tuple = null;
                //Force garbage collection.
                GC.Collect();

                // Wait for all finalizers to complete before continuing.
                GC.WaitForPendingFinalizers();
            }                
        }
        public dynamic MaintenanceForm(DataTable TableList, DataTable dataTable,  string strObj, string strObMode, string strObjSub, string OperationCode, string OpeGroupCateg, string InputKind, string OperationNo, string UserName)
        {
            var Tuple = new TupleForm();
            try
            {
                using (SqlConnection conn = new SqlConnection(constr))
                {
                    using (SqlCommand objCmd = new SqlCommand())
                    {
                        var strStored = "";                    
                        strStored = "sprDbInputChk";
                        objCmd.Parameters.Add(new SqlParameter("@strObj", strObj));
                        objCmd.Parameters.Add(new SqlParameter("@strObMode", strObMode));
                        objCmd.Parameters.Add(new SqlParameter("@strObjSub", strObjSub));
                        objCmd.Parameters.Add(new SqlParameter("@OperationCode", OperationCode));
                        objCmd.Parameters.Add(new SqlParameter("@OpeGroupCateg", OpeGroupCateg));
                        objCmd.Parameters.Add(new SqlParameter("@OperationNo", OperationNo));
                        objCmd.Parameters.Add(new SqlParameter("@InputKind", InputKind));
                        objCmd.Parameters.Add(new SqlParameter("@UserName", UserName));
                        objCmd.Parameters.Add(new SqlParameter("@TableItemTableType", SqlDbType.Structured)
                        {
                            Value = TableList,
                            TypeName = "dbo.TableItemTableType"
                        });
                        objCmd.Parameters.Add(new SqlParameter("@TableOperationItemList", SqlDbType.Structured)
                        {
                            Value = dataTable,
                            TypeName = "dbo.TableOperationItemList"
                        });
                        objCmd.Parameters.Add(new SqlParameter("@_Message", SqlDbType.NVarChar, 200)
                        {
                            Value = "",
                            Direction = ParameterDirection.Output
                        });
                        objCmd.Parameters.Add(new SqlParameter("@_OperationNo", SqlDbType.Char, 25)
                        {
                            Value = "",
                            Direction = ParameterDirection.Output
                        });


                        conn.Open();
                        objCmd.Connection = conn;
                        objCmd.CommandText = strStored;
                        objCmd.CommandType = CommandType.StoredProcedure;
                        objCmd.ExecuteReader();
                  
                        var DataComplete = new { status = true, subject = strObjSub + " Item", detail = "Successfully!" };
                        conn.Close();
                        objCmd.Parameters.Clear();
                        return DataComplete;
                    }
                }

                       
            }
            catch (SqlException ex)
            {
                Tuple = null;
                var Data = new { status = false, subject = strObjSub + " Item", detail = ex.Message.ToString() };
                return Data;

            }
            finally
            {
                Tuple = null;
            }
        }
    }
}
