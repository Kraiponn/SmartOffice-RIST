using Microsoft.Extensions.Configuration;
using SmartOffice.eManagement.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace SmartOffice.eManagement.Class
{
    class ConnUser
    {
        private IConfiguration configuration;
        private string constr;

        public ConnUser(IConfiguration configuration)
        {
            this.configuration = configuration;
            constr = configuration.GetConnectionString("DefaultConnection7");
        }


        public TupleUser GetGroup(string OpeGroupCateg, string Division, string Section, string Department, string Department2, string Department3, string username, string SpecialGroup)
        {

            SqlConnection conn = new SqlConnection(constr);
            SqlCommand objCmd = new SqlCommand();
            var strStored = "";
            var Tuple = new TupleUser();
            try
            {
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

                List<UserOperationGroup> result01 = new List<UserOperationGroup>();

                using (SqlDataReader rdr = objCmd.ExecuteReader())
                {
                    if (rdr != null)
                    {
                        while (rdr.Read())
                        {
                            result01.Add(new UserOperationGroup
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
                                SpecialGroup = rdr["SpecialGroup"].ToString(),
                                OperationSub = Convert.ToBoolean(rdr["OperationSub"].ToString())
                            });
                        }
                        Tuple.userOperationGroups = result01;
                    }
                }
                conn.Close();
                objCmd.Parameters.Clear();
                objCmd.Dispose();

                return Tuple;
            }
            catch
            {
                Tuple = null;
                return null;
            }
            finally
            {
                Tuple = null;
            }
          
        }

        public TupleUser GetGenerate(string strObj, string strObMode, string OperationCode, string OpeGroupCateg, string InputKind, string OperationNo, DataTable dataTable,string UserName)
        {

            SqlConnection conn = new SqlConnection(constr);
            SqlCommand objCmd = new SqlCommand();
            var strStored = "";
            var Tuple = new TupleUser();

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

            List<UserOperationItemList> result01 = new List<UserOperationItemList>();
            List<UserMenulist> result02 = new List<UserMenulist>();

            using (SqlDataReader rdr = objCmd.ExecuteReader())
            {
                if (rdr != null)
                {
                    while (rdr.Read())
                    {
                        result01.Add(new UserOperationItemList
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
                    Tuple.userOperationItems = result01;

                    if (rdr.NextResult())
                    {
                        while (rdr.Read())
                        {
                            result02.Add(new UserMenulist
                            {
                                No = rdr["No"].ToString(),
                                MenuNameE = rdr["MenuNameE"].ToString(),
                                MenuNameT = rdr["MenuNameT"].ToString(),
                                MenuNameJ = rdr["MenuNameJ"].ToString(),
                                UserId = rdr["UserId"].ToString(),
                                Action = rdr["Action"].ToString(),
                                Controller = rdr["Controller"].ToString(),
                                Param = rdr["Param"].ToString(),
                                MenuUrl = rdr["MenuUrl"].ToString(),
                                Image = rdr["Image"].ToString(),
                                IconClass = rdr["IconClass"].ToString(),
                                Badges = rdr["Badges"].ToString(),
                                BadgesName = rdr["BadgesName"].ToString(),
                                Download = rdr["Download"].ToString(),
                                UserName = rdr["UserName"].ToString(),
                            });
                        }
                        Tuple.userMenulists = result02;

                    }
                }
            }
            conn.Close();
            objCmd.Parameters.Clear();
            objCmd.Dispose();

            return Tuple;
        }

        
    }
}
