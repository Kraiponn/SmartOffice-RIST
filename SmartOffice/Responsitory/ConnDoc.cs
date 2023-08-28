using Microsoft.Extensions.Configuration;
using SmartOffice.Models;
using SmartOffice.ModelsDocControl;
using SmartOffice.ModelsForm;
using SmartOffice.ModelsPRApprove;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SmartOffice.Class
{
    public class ConnDoc
    {
        private IConfiguration configuration;

        public ConnDoc(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

    public IEnumerable<VewDocumentItemList>GetvewDocumentItemList(string code, string docno)
        {
            string constr = configuration.GetConnectionString("DefaultConnection2");
            using (SqlConnection conn = new SqlConnection(constr))
            {
                using (SqlCommand objCmd = new SqlCommand())
                {
                    var strStored = "";

                    if (docno == "" || docno == null)
                    {
                        strStored = "sprFormGetInputItem";
                        objCmd.Parameters.Add(new SqlParameter("@DocCode", code));
                    }
                    else
                    {
                        strStored = "sprFormGetInputItemResult";
                        objCmd.Parameters.Add(new SqlParameter("@DocNo", docno));
                    }
                  
                        conn.Open();
                        objCmd.Connection = conn;
                        objCmd.CommandText = strStored;
                        objCmd.CommandType = CommandType.StoredProcedure;

                        using (var reader = objCmd.ExecuteReader())
                        {

                        while (reader.Read())
                        {
                            yield return new VewDocumentItemList
                            {
                                DocumentNameE = reader["DocumentNameE"].ToString(),
                                DocumentNameT = reader["DocumentNameT"].ToString(),
                                DocumentNameJ = reader["DocumentNameJ"].ToString(),
                                DocumentGroupCode = reader["DocumentGroupCode"].ToString(),
                                DocumentKind = reader["DocumentKind"].ToString(),
                                OpeGroupCateg = reader["OpeGroupCateg"].ToString(),
                                AttachedFile = reader["AttachedFile"].ToString(),
                                EmailSend = reader["EmailSend"].ToString(),
                                AttachedFile1 = reader["AttachedFile1"].ToString(),
                                AttachedFile2 = reader["AttachedFile2"].ToString(),
                                StandardNo = reader["StandardNo"].ToString(),
                                ReviseNo = reader["ReviseNo"].ToString(),
                                Remark = reader["Remark"].ToString(),
                                Language = reader["Language"].ToString(),
                                DocumentCode = reader["DocumentCode"].ToString(),
                                ItemCateg = reader["ItemCateg"].ToString(),
                                InputOption = reader["InputOption"].ToString(),
                                DisplayOrder = Convert.ToInt16(reader["DisplayOrder"]),
                                ItemCategNameE = reader["ItemCategNameE"].ToString(),
                                RemarksTitleE1 = reader["RemarksTitleE1"].ToString(),
                                RemarksTitleE2 = reader["RemarksTitleE2"].ToString(),
                                RemarksTitleE3 = reader["RemarksTitleE3"].ToString(),
                                RemarksTitleE4 = reader["RemarksTitleE4"].ToString(),
                                RemarksTitleE5 = reader["RemarksTitleE5"].ToString(),
                                RemarksTitleE6 = reader["RemarksTitleE6"].ToString(),
                                RemarksTitleE7 = reader["RemarksTitleE7"].ToString(),
                                RemarksTitleE8 = reader["RemarksTitleE8"].ToString(),
                                RemarksTitleE9 = reader["RemarksTitleE9"].ToString(),
                                RemarksTitleE10 = reader["RemarksTitleE10"].ToString(),
                                ItemCategNameT = reader["ItemCategNameT"].ToString(),
                                RemarksTitleT1 = reader["RemarksTitleT1"].ToString(),
                                RemarksTitleT2 = reader["RemarksTitleT2"].ToString(),
                                RemarksTitleT3 = reader["RemarksTitleT3"].ToString(),
                                RemarksTitleT4 = reader["RemarksTitleT4"].ToString(),
                                RemarksTitleT5 = reader["RemarksTitleT5"].ToString(),
                                RemarksTitleT6 = reader["RemarksTitleT6"].ToString(),
                                RemarksTitleT7 = reader["RemarksTitleT7"].ToString(),
                                RemarksTitleT8 = reader["RemarksTitleT8"].ToString(),
                                RemarksTitleT9 = reader["RemarksTitleT9"].ToString(),
                                RemarksTitleT10 = reader["RemarksTitleT10"].ToString(),
                                ItemCategNameJ = reader["ItemCategNameJ"].ToString(),
                                RemarksTitleJ1 = reader["RemarksTitleJ1"].ToString(),
                                RemarksTitleJ2 = reader["RemarksTitleJ2"].ToString(),
                                RemarksTitleJ3 = reader["RemarksTitleJ3"].ToString(),
                                RemarksTitleJ4 = reader["RemarksTitleJ4"].ToString(),
                                RemarksTitleJ5 = reader["RemarksTitleJ5"].ToString(),
                                RemarksTitleJ6 = reader["RemarksTitleJ6"].ToString(),
                                RemarksTitleJ7 = reader["RemarksTitleJ7"].ToString(),
                                RemarksTitleJ8 = reader["RemarksTitleJ8"].ToString(),
                                RemarksTitleJ9 = reader["RemarksTitleJ9"].ToString(),
                                RemarksTitleJ10 = reader["RemarksTitleJ10"].ToString(),
                                InputItemCode = reader["InputItemCode"].ToString().Trim(),
                                ItemNameE = reader["ItemNameE"].ToString(),
                                ItemNameT = reader["ItemNameT"].ToString(),
                                ItemNameJ = reader["ItemNameJ"].ToString(),
                                InputType = reader["InputType"].ToString(),
                                DataType = reader["DataType"].ToString(),
                                DecimalNo = reader["DecimalNo"].ToString(),
                                Required = Convert.ToBoolean(reader["Required"].ToString()),
                                Minlength = reader["Minlength"].ToString(),
                                Maxlength = reader["Maxlength"].ToString(),
                                Min = reader["Min"].ToString(),
                                Max = reader["Max"].ToString(),
                                Step = reader["Step"].ToString(),
                                Unit = reader["Unit"].ToString(),
                                ValueCode = (docno == "" ? reader["ValueCode"].ToString() : reader["FinalResult"].ToString()),
                                InputItemOption = reader["InputItemOption"].ToString(),
                                DefaultValue = reader["DefaultValue"].ToString(),
                                ReadOnly = Convert.ToBoolean(reader["ReadOnly"].ToString()),
                                InputItemListItemCateg = reader["InputItemListItemCateg"].ToString(),
                                ItemCode = reader["ItemCode"].ToString().Trim(),
                                InputItemListDisplayOrder = Convert.ToInt16(reader["InputItemListDisplayOrder"]),
                                valueold = reader["ValueCode"].ToString().Trim(),
                                DetailOption = reader["DetailOption"].ToString() == "" ? "0" : reader["DetailOption"].ToString(),


                            };
                        }
                    }
                   
                   
                }
            }       
        }                
    public IEnumerable<WaitApproveModel>GetWaitApprove()
    {
        string constr = configuration.GetConnectionString("DefaultConnection2");
            //SqlConnection conn = new SqlConnection(constr);
            //SqlCommand objCmd = new SqlCommand();
       
            using (SqlConnection conn = new SqlConnection(constr))
            {
                using (SqlCommand objCmd = new SqlCommand())
                {
                    var strStored = "";


                    strStored = "sprFormGetWaitApprove";
                    
                    conn.Open();
                    objCmd.Connection = conn;
                    objCmd.CommandText = strStored;
                    objCmd.CommandType = CommandType.StoredProcedure;

                    using (var reader = objCmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            yield return new WaitApproveModel
                            {
                                DocumentCode = reader["DocumentCode"].ToString(),
                                DocumentNo = reader["DocumentNo"].ToString(),
                                DocumentNameE = reader["DocumentNameE"].ToString(),
                                DocumentNameT = reader["DocumentNameT"].ToString(),
                                DocumentNameJ = reader["DocumentNameJ"].ToString(),
                                IssuedDate= Convert.ToDateTime( reader["IssuedDate"].ToString()),
                                IssuedDateChange = reader["IssuedDateChange"].ToString(),
                                DocumentStatus = reader["DocumentStatus"].ToString(),
                                ReqDescription1 = reader["ReqDescription1"].ToString(),
                                ReqDescription2 = reader["ReqDescription2"].ToString(),
                                ReqDescription3 = reader["ReqDescription3"].ToString(),
                                ReqOperatorID = reader["ReqOperatorID"].ToString(),
                                ReqOperatorName = reader["ReqOperatorName"].ToString(),
                                RoleID = reader["RoleID"].ToString(),
                                EMPLEVEL = reader["EMPLEVEL"].ToString(),
                                CODEMPID = reader["CODEMPID"].ToString(),
                                NAMEMPE = reader["NAMEMPE"].ToString(),
                                DIVISION = reader["DIVISION"].ToString(),
                                DEPARTMENT = reader["DEPARTMENT"].ToString(),
                                DEPARTMENT2 = reader["DEPARTMENT2"].ToString(),
                                EMAIL1 = reader["EMAIL1"].ToString(),
                                EMAIL2 = reader["EMAIL2"].ToString(),
                                SeqNo = reader["SeqNo"].ToString(),
                                ValLevel = reader["ValLevel"].ToString(),
                                SECTION = reader["SECTION"].ToString(),
                                approvepast = reader["approvepast"].ToString(),
                                CountIssuedDate = reader["CountIssuedDate"].ToString(),
                                Countapprovepast = reader["Countapprovepast"].ToString(),
                            };
                        }
                    }
                    conn.Close();
                    objCmd.Parameters.Clear();
                }
            }    
        }
    public IEnumerable<DocumentEdit> GetEditDocument()
        {
            string constr = configuration.GetConnectionString("DefaultConnection2");
            //SqlConnection conn = new SqlConnection(constr);
            //SqlCommand objCmd = new SqlCommand();

            using (SqlConnection conn = new SqlConnection(constr))
            {
                using (SqlCommand objCmd = new SqlCommand())
                {
                    var strStored = "";
                    strStored = "sprFormGetEditDocument";
                    conn.Open();
                    objCmd.Connection = conn;
                    objCmd.CommandText = strStored;
                    objCmd.CommandType = CommandType.StoredProcedure;

                    using (var reader = objCmd.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            yield return new DocumentEdit
                            {

                                ApprovalDate = reader["ApprovalDate"].ToString(),
                                ApproverOperatorID = reader["ApproverOperatorID"].ToString(),
                                ApproverOperatorName = reader["ApproverOperatorName"].ToString(),
                                DocumentCode = reader["DocumentCode"].ToString(),
                                DocumentNo = reader["DocumentNo"].ToString(),
                                DocumentNameE = reader["DocumentNameE"].ToString(),
                                DocumentNameT = reader["DocumentNameT"].ToString(),
                                DocumentNameJ = reader["DocumentNameJ"].ToString(),
                                IssuedDate = Convert.ToDateTime(reader["IssuedDate"].ToString()),
                                IssuedDateChange = reader["IssuedDateChange"].ToString(),
                                DocumentStatus = reader["DocumentStatus"].ToString(),
                                ReqDescription1 = reader["ReqDescription1"].ToString(),
                                ReqDescription2 = reader["ReqDescription2"].ToString(),
                                ReqDescription3 = reader["ReqDescription3"].ToString(),
                                ReqOperatorID = reader["ReqOperatorID"].ToString(),
                                ReqOperatorName = reader["ReqOperatorName"].ToString(),
                                RoleID = reader["RoleID"].ToString(),
                                SeqNo = reader["SeqNo"].ToString(),
                                ApprovalItemNameE = reader["ApprovalItemNameE"].ToString(),
                                ApprovalItemNameT = reader["ApprovalItemNameT"].ToString(),
                                ApprovalItemNameJ = reader["ApprovalItemNameJ"].ToString(),
                                ApprovalItemCode = reader["ApprovalItemCode"].ToString(),
                                UpdDate = reader["UpdDate"].ToString(),
                                CountIssuedDate = reader["CountIssuedDate"].ToString(),
                            };
                        }
                    }
                    conn.Close();
                    objCmd.Parameters.Clear();
                }

            }         
        }
    public IEnumerable<GroupIssueDocument> GetGroupIssueDocument(string GroupCateg ,string UserId)
        {
            string constr = configuration.GetConnectionString("DefaultConnection2");
            //SqlConnection conn = new SqlConnection(constr);
            //SqlCommand objCmd = new SqlCommand();


            using (SqlConnection conn = new SqlConnection(constr))
            {
                using (SqlCommand objCmd = new SqlCommand())
                {
                    var strStored = "";
                    strStored = "sprFormDocumentIssueGroup";
                    objCmd.Parameters.Add(new SqlParameter("@GroupCateg", GroupCateg));
                    objCmd.Parameters.Add(new SqlParameter("@UserId", UserId));
                    conn.Open();
                    objCmd.Connection = conn;
                    objCmd.CommandText = strStored;
                    objCmd.CommandType = CommandType.StoredProcedure;
                    using (var reader = objCmd.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            yield return new GroupIssueDocument
                            {

                                DocumentCode = reader["DocumentCode"].ToString(),
                                DocumentNo = reader["DocumentNo"].ToString(),
                                DocumentNameE = reader["DocumentNameE"].ToString(),
                                DocumentNameT = reader["DocumentNameT"].ToString(),
                                DocumentNameJ = reader["DocumentNameJ"].ToString(),
                                IssuedDate = Convert.ToDateTime(reader["IssuedDate"].ToString()),
                                IssuedDateChange = reader["IssuedDateChange"].ToString(),
                                DocumentStatus = reader["DocumentStatus"].ToString(),
                                ReqDescription1 = reader["ReqDescription1"].ToString(),
                                ReqDescription2 = reader["ReqDescription2"].ToString(),
                                ReqDescription3 = reader["ReqDescription3"].ToString(),
                                ReqOperatorName = reader["ReqOperatorName"].ToString(),
                                UpdDate = reader["UpdDate"].ToString(),
                                FlagUpdDate = reader["FlagUpdDate"].ToString(),
                                ApprovalDate = reader["ApprovalDate"].ToString(),
                                ApproverOperatorName = reader["ApproverOperatorName"].ToString(),
                                CountIssuedDate = reader["CountIssuedDate"].ToString(),
                                YearMonth = Convert.ToDateTime(reader["IssuedDate"].ToString()).ToString("yyyy/MM"),
                            };
                        }
                    }
                    conn.Close();
                    objCmd.Parameters.Clear();
                }
            }       
        }
        public IEnumerable<MyDocument> GetMyDocument(string username)
        {
            string constr = configuration.GetConnectionString("DefaultConnection2");
            //SqlConnection conn = new SqlConnection(constr);
            //SqlCommand objCmd = new SqlCommand();


            using (SqlConnection conn = new SqlConnection(constr))
            {
                using (SqlCommand objCmd = new SqlCommand())
                {
                    var strStored = "";
                    strStored = "sprFormGetMyDocument";
                    objCmd.Parameters.Add(new SqlParameter("@Username", username));
                    
                    conn.Open();
                    objCmd.Connection = conn;
                    objCmd.CommandText = strStored;
                    objCmd.CommandType = CommandType.StoredProcedure;
                    using (var reader = objCmd.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            yield return new MyDocument
                            {

                                DocumentCode = reader["DocumentCode"].ToString(),
                                DocumentNo = reader["DocumentNo"].ToString(),
                                DocumentNameE = reader["DocumentNameE"].ToString(),
                                DocumentNameT = reader["DocumentNameT"].ToString(),
                                DocumentNameJ = reader["DocumentNameJ"].ToString(),
                                IssuedDate = Convert.ToDateTime(reader["IssuedDate"].ToString()),
                                IssuedDateChange = reader["IssuedDateChange"].ToString(),
                                DocumentStatus = reader["DocumentStatus"].ToString(),
                                ReqDescription1 = reader["ReqDescription1"].ToString(),                                
                                ReqOperatorName = reader["ReqOperatorName"].ToString(),
                                NextApprove = reader["NextApprove"].ToString(),
                                CountIssuedDate = reader["CountIssuedDate"].ToString(),
                                YearMonth = Convert.ToDateTime(reader["IssuedDate"].ToString()).ToString("yyyy/MM"),
                            };
                        }
                    }
                    conn.Close();
                    objCmd.Parameters.Clear();
                }
            }
        }

        public IEnumerable<MyApproved> GetMyApproved(string username)
        {
            string constr = configuration.GetConnectionString("DefaultConnection2");
            //SqlConnection conn = new SqlConnection(constr);
            //SqlCommand objCmd = new SqlCommand();


            using (SqlConnection conn = new SqlConnection(constr))
            {
                using (SqlCommand objCmd = new SqlCommand())
                {
                    var strStored = "";
                    strStored = "sprFormGetMyApproved";
                    objCmd.Parameters.Add(new SqlParameter("@Username", username));

                    conn.Open();
                    objCmd.Connection = conn;
                    objCmd.CommandText = strStored;
                    objCmd.CommandType = CommandType.StoredProcedure;
                    using (var reader = objCmd.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            yield return new MyApproved
                            {

                                DocumentCode = reader["DocumentCode"].ToString(),
                                DocumentNo = reader["DocumentNo"].ToString(),
                                DocumentNameE = reader["DocumentNameE"].ToString(),                                
                                IssuedDate = Convert.ToDateTime(reader["IssuedDate"].ToString()),
                                ApprovalDate = Convert.ToDateTime(reader["ApprovalDate"].ToString()),
                                IssuedDateChange = reader["IssuedDateChange"].ToString(),
                                ApprovalDateChange = reader["ApprovalDateChange"].ToString(),
                                DocumentStatus = reader["DocumentStatus"].ToString(),
                                ReqDescription1 = reader["ReqDescription1"].ToString(),
                                ReqOperatorName = reader["ReqOperatorName"].ToString(),                                
                                YearMonth = Convert.ToDateTime(reader["ApprovalDate"].ToString()).ToString("yyyy/MM"),
                                FlagUpdDate = reader["FlagUpdDate"].ToString(),
                            };
                        }
                    }
                    conn.Close();
                    objCmd.Parameters.Clear();
                }
            }
        }

        public IEnumerable<Flow> GetFlow(string DocNo)
        {
            string constr = configuration.GetConnectionString("DefaultConnection2");
            //SqlConnection conn = new SqlConnection(constr);
            //SqlCommand objCmd = new SqlCommand();

            using (SqlConnection conn = new SqlConnection(constr))
            {
                using (SqlCommand objCmd = new SqlCommand())
                {

                    var strStored = "";


                    strStored = "sprFormGetFlow";
                    objCmd.Parameters.Add(new SqlParameter("@DocNo", DocNo));

                    conn.Open();
                    objCmd.Connection = conn;
                    objCmd.CommandText = strStored;
                    objCmd.CommandType = CommandType.StoredProcedure;

                    using (var reader = objCmd.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            yield return new Flow
                            {

                                DocumentCode = reader["DocumentCode"].ToString(),
                                DocumentNo = reader["DocumentNo"].ToString(),
                                DocumentNameE = reader["DocumentNameE"].ToString(),
                                DocumentNameT = reader["DocumentNameT"].ToString(),
                                DocumentNameJ = reader["DocumentNameJ"].ToString(),
                                ApprovalItemCode = reader["ApprovalItemCode"].ToString(),
                                ApprovalItemNameE = reader["ApprovalItemNameE"].ToString(),
                                ApprovalItemNameT = reader["ApprovalItemNameT"].ToString(),
                                ApprovalItemNameJ = reader["ApprovalItemNameJ"].ToString(),
                                SeqNo = Convert.ToInt32(reader["SeqNo"].ToString()),
                                ApprovalDate = reader["ApprovalDate"].ToString(),
                                Comment = reader["Comment"].ToString(),
                                Judge = reader["Judge"].ToString(),
                                IssuedDate = reader["IssuedDate"].ToString(),
                                DocumentStatus = reader["DocumentStatus"].ToString(),
                                ReqDescription1 = reader["ReqDescription1"].ToString(),
                                ReqDescription2 = reader["ReqDescription2"].ToString(),
                                ReqDescription3 = reader["ReqDescription3"].ToString(),
                                ReqOperatorID = reader["ReqOperatorID"].ToString(),
                                ReqOperatorName = reader["ReqOperatorName"].ToString(),
                                DIVISION = reader["DIVISION"].ToString(),
                                SECTION = reader["SECTION"].ToString(),
                                DEPARTMENT = reader["DEPARTMENT"].ToString(),
                                DEPARTMENT2 = reader["DEPARTMENT2"].ToString(),
                                POSITION = reader["POSITION"].ToString(),
                                EMAIL1 = reader["EMAIL1"].ToString(),
                                EMAIL2 = reader["EMAIL2"].ToString(),
                                RoleID = reader["RoleID"].ToString(),
                                Requirement = reader["Requirement"].ToString(),
                                ApplicationName = reader["ApplicationName"].ToString(),
                                ValLevel = reader["ValLevel"].ToString(),
                                EMPLEVELU = reader["EMPLEVELU"].ToString(),
                                CODEMPIDU = reader["CODEMPIDU"].ToString(),
                                NAMEMPEU = reader["NAMEMPEU"].ToString(),
                                DIVISIONU = reader["DIVISIONU"].ToString(),
                                SECTIONU = reader["SECTIONU"].ToString(),
                                DEPARTMENTU = reader["DEPARTMENTU"].ToString(),
                                DEPARTMENT2U = reader["DEPARTMENT2U"].ToString(),
                                DEPARTMENT3U = reader["DEPARTMENT3U"].ToString(),
                                POSITIONU = reader["POSITIONU"].ToString(),
                                EMAIL1U = reader["EMAIL1U"].ToString(),
                                EMAIL2U = reader["EMAIL2U"].ToString(),
                                checkmin = reader["checkmin"].ToString(),
                                SkipFlag = reader["SkipFlag"] != DBNull.Value ? Convert.ToBoolean(reader["SkipFlag"]) : true,
                                AssignFlag = reader["AssignFlag"].ToString(),
                                AssignFlagRemark = reader["AssignFlagRemark"].ToString(),
                                ApproverOperatorID = reader["ApproverOperatorID"].ToString(),
                                ApproverOperatorName = reader["ApproverOperatorName"].ToString(),
                                ApproverOperatorSign = reader["ApproverOperatorSign"].ToString(),
                                RequirementRemark = reader["RequirementRemark"].ToString(),
                            };
                        }
                    }
                    conn.Close();
                    objCmd.Parameters.Clear();
                }

            }
        }
    public IEnumerable<UserFlow> GetUserFlow(string DocCode, string DocNo, string SeqNo ,string Mode)
    {
        string constr = configuration.GetConnectionString("DefaultConnection2");
        //SqlConnection conn = new SqlConnection(constr);
        //SqlCommand objCmd = new SqlCommand();
            using (SqlConnection conn = new SqlConnection(constr))
            {
                using (SqlCommand objCmd = new SqlCommand())
                {
                    var strStored = "";


                    strStored = "sprFormSelectOPFlow";
                    objCmd.Parameters.Add(new SqlParameter("@DocCode", DocCode ?? ""));
                    objCmd.Parameters.Add(new SqlParameter("@DocNo", DocNo ?? ""));
                    objCmd.Parameters.Add(new SqlParameter("@SeqNo", SeqNo ?? "0"));
                    objCmd.Parameters.Add(new SqlParameter("@Mode", Mode ?? ""));

                    conn.Open();
                    objCmd.Connection = conn;
                    objCmd.CommandText = strStored;
                    objCmd.CommandType = CommandType.StoredProcedure;

                    using (var reader = objCmd.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            var Text = reader["CODEMPID"].ToString() + " : " + reader["NAMEMPE"].ToString() + " : " + reader["OperatorDetail"].ToString();
                            var Value = reader["CODEMPID"].ToString() + " : " + reader["NAMEMPE"].ToString() + " : " + reader["OperatorDetail"].ToString();
                            if (reader["RoleID"].ToString().Trim() == "ALL00")
                            {
                                Text = reader["CODEMPID"].ToString() + " : " + reader["NAMEMPE"].ToString();
                                Value = reader["CODEMPID"].ToString() + " : " + reader["NAMEMPE"].ToString();
                            }
                            yield return new UserFlow
                            {
                                DocumentNo = reader["DocumentNo"].ToString(),
                                Id = int.Parse( reader["Id"].ToString()),
                                checkselect = reader["checkselect"].ToString(),
                                CODEMPID = reader["CODEMPID"].ToString(),
                                NAMEMPT = reader["NAMEMPT"].ToString(),
                                NAMEMPE = reader["NAMEMPE"].ToString(),
                                DIVISION = reader["DIVISION"].ToString(),
                                SECTION = reader["SECTION"].ToString(),
                                DEPARTMENT = reader["DEPARTMENT"].ToString(),
                                DEPARTMENT2 = reader["DEPARTMENT2"].ToString(),
                                DEPARTMENT3 = reader["DEPARTMENT3"].ToString(),
                                ORGANIZATIONNAME = reader["ORGANIZATIONNAME"].ToString(),
                                POSITION = reader["POSITION"].ToString(),
                                EMPLEVEL = reader["EMPLEVEL"].ToString(),
                                EMAIL1 = reader["EMAIL1"].ToString(),
                                EMAIL2 = reader["EMAIL2"].ToString(),
                                FlowID = reader["FlowID"].ToString(),
                                SeqNo = reader["SeqNo"].ToString(),
                                RoleID = reader["RoleID"].ToString(),
                                ApprovalItemCode = reader["ApprovalItemCode"].ToString(),
                                Requirement = reader["Requirement"].ToString(),
                                RequirementRemark = reader["RequirementRemark"].ToString(),
                                AssignFlag = reader["AssignFlag"].ToString(),
                                AssignFlagRemark = reader["AssignFlagRemark"].ToString(),
                                ApprovalItemNameE = reader["ApprovalItemNameE"].ToString(),
                                ApprovalItemNameT = reader["ApprovalItemNameT"].ToString(),
                                ApprovalItemNameJ = reader["ApprovalItemNameJ"].ToString(),                                
                                Text = Text,
                                Value = Value
                            };
                        }
                    }
                    conn.Close();
                    objCmd.Parameters.Clear();
                }
            }
                
    }

        /*////////////////////////////////////////////////////////////////////////////////////////////////////////*/
        public IEnumerable<PRModel> Getprdata(string mode)
        {
            string constr = configuration.GetConnectionString("DefaultConnection5");
            using (SqlConnection conn = new SqlConnection(constr))
            {
                using (SqlCommand objCmd = new SqlCommand())
                {
                    var strStored = "";


                    strStored = "sprPRApproveWaiting";
                    objCmd.Parameters.Add(new SqlParameter("@Mode", mode));


                    conn.Open();
                    objCmd.Connection = conn;
                    objCmd.CommandText = strStored;
                    objCmd.CommandType = CommandType.StoredProcedure;

                    using (var reader = objCmd.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            yield return new PRModel
                            {

                                buyerCode = reader["BuyerCode"].ToString(),
                                buyerName = reader["BuyerNameUNI"].ToString(),
                                count = reader["Count"].ToString(),

                            };
                        }
                    }
                    conn.Close();
                    objCmd.Parameters.Clear();
                }
            }
                   
        }


        /*////////////////////////////////////////////////////////////////////////////////////////////////////////*/
        public PRApporveModel GetSumPR()
        {
            string constr = configuration.GetConnectionString("DefaultConnection6");
            //SqlConnection conn = new SqlConnection(constr);
            //SqlCommand objCmd = new SqlCommand();
            using (SqlConnection conn = new SqlConnection(constr))
            {
                using (SqlCommand objCmd = new SqlCommand())
                {

                    var strStored = "";
                    var sumpr = new PRApporveModel();

                    strStored = "sprRoproSCheckPRRemainByDateTotal";


                    conn.Open();
                    objCmd.Connection = conn;
                    objCmd.CommandText = strStored;
                    objCmd.CommandType = CommandType.StoredProcedure;

                    List<result01> result01 = new List<result01>();
                    List<result02> result02 = new List<result02>();
                    List<result03> result03 = new List<result03>();
                    List<result04> result04 = new List<result04>();
                    List<result05> result05 = new List<result05>();


                    using (SqlDataReader rdr = objCmd.ExecuteReader())
                    {
                        if (rdr != null)
                        {
                            while (rdr.Read())
                            {
                                result01.Add(new result01
                                {
                                    Item = rdr["Item"].ToString(),
                                    OpeDate = Convert.ToDateTime(rdr["OpeDate"]).ToString("dd-MM-yy"),
                                    SumCountOfPR = rdr["SumCountOfPR"].ToString() != "" ? int.Parse(rdr["SumCountOfPR"].ToString()) : 0,
                                    CountOfPRFinish = rdr["CountOfPRFinish"].ToString() != "" ? int.Parse(rdr["CountOfPRFinish"].ToString()) : 0,
                                    CountOfPRRemain = rdr["CountOfPRRemain"].ToString() != "" ? int.Parse(rdr["CountOfPRRemain"].ToString()) : 0,

                                });
                            }
                            sumpr.result01 = result01;

                            if (rdr.NextResult())
                            {
                                while (rdr.Read())
                                {
                                    result02.Add(new result02
                                    {
                                        item = rdr["item"].ToString(),
                                        OpeDate = Convert.ToDateTime(rdr["OpeDate"]).ToString("dd-MM-yy"),
                                        BuyerCode = rdr["BuyerCode"].ToString(),
                                        BuyerName = rdr["BuyerName"].ToString(),
                                        SumCountOfPR = int.Parse(rdr["SumCountOfPR"].ToString()),
                                    });
                                }
                                sumpr.result02 = result02;
                            }
                            if (rdr.NextResult())
                            {
                                while (rdr.Read())
                                {
                                    result03.Add(new result03
                                    {
                                        item = rdr["item"].ToString(),
                                        BuyerCode = rdr["BuyerCode"].ToString(),
                                        BuyerName = rdr["BuyerName"].ToString(),
                                        CntOfPRNo = int.Parse(rdr["CntOfPRNo"].ToString()),
                                    });
                                }
                                sumpr.result03 = result03;
                            }
                            if (rdr.NextResult())
                            {
                                while (rdr.Read())
                                {
                                    result04.Add(new result04
                                    {
                                        item = rdr["item"].ToString(),
                                        Create_Date = Convert.ToDateTime(rdr["Create_Date"].ToString()).ToString("dd-MM-yy"),
                                        BuyerCode = rdr["BuyerCode"].ToString(),
                                        BuyerName = rdr["BuyerName"].ToString(),
                                        CntOfPRNo = int.Parse(rdr["CntOfPRNo"].ToString()),
                                    });
                                }
                                sumpr.result04 = result04;
                            }
                            if (rdr.NextResult())
                            {
                                while (rdr.Read())
                                {
                                    result05.Add(new result05
                                    {
                                        item = rdr["item"].ToString(),
                                        Approve_Date = Convert.ToDateTime(rdr["Approve_Date"].ToString()).ToString("dd-MM-yy"),
                                        Operator = rdr["Operator"].ToString(),
                                        OpeName = rdr["OpeName"].ToString(),
                                        Level_App_Code = rdr["Level_App_Code"].ToString(),
                                        Level_App_Name = rdr["Level_App_Name"].ToString(),
                                        CntPRNo = int.Parse(rdr["CntPRNo"].ToString()),
                                    });
                                }
                                sumpr.result05 = result05;
                            }
                        }



                    }

                    strStored = "sprRoproSCheckPRRemainGrandTotal";
                    objCmd.CommandText = strStored;
                    objCmd.CommandType = CommandType.StoredProcedure;

                    List<Gresult01> Gresult01 = new List<Gresult01>();
                    List<Gresult02> Gresult02 = new List<Gresult02>();
                    List<Gresult03> Gresult03 = new List<Gresult03>();
                    List<Gresult04> Gresult04 = new List<Gresult04>();
                    List<Gresult05> Gresult05 = new List<Gresult05>();

                    using (SqlDataReader rdr = objCmd.ExecuteReader())
                    {
                        if (rdr != null)
                        {
                            while (rdr.Read())
                            {
                                Gresult01.Add(new Gresult01
                                {
                                    Item = rdr["Item"].ToString(),
                                    SumCountOfPR = rdr["SumCountOfPR"].ToString() != "" ? int.Parse(rdr["SumCountOfPR"].ToString()) : 0,
                                    CountOfPRFinish = rdr["CountOfPRFinish"].ToString() != "" ? int.Parse(rdr["CountOfPRFinish"].ToString()) : 0,
                                    CountOfPRRemain = rdr["CountOfPRRemain"].ToString() != "" ? int.Parse(rdr["CountOfPRRemain"].ToString()) : 0,

                                });
                            }
                            sumpr.Gresult01 = Gresult01;
                            if (rdr.NextResult())
                            {
                                while (rdr.Read())
                                {
                                    Gresult02.Add(new Gresult02
                                    {
                                        Item = rdr["item"].ToString(),
                                        BuyerCode = rdr["BuyerCode"].ToString(),
                                        BuyerName = rdr["BuyerName"].ToString(),
                                        FinFlag = rdr["FinFlag"].ToString(),
                                        CountOfPR = int.Parse(rdr["CountOfPR"].ToString()),
                                    });
                                }
                                sumpr.Gresult02 = Gresult02;
                            }
                            if (rdr.NextResult())
                            {
                                while (rdr.Read())
                                {
                                    Gresult03.Add(new Gresult03
                                    {
                                        Item = rdr["Item"].ToString(),
                                        CntOfPRNo = int.Parse(rdr["CntOfPRNo"].ToString()),
                                    });
                                }
                                sumpr.Gresult03 = Gresult03;
                            }
                            if (rdr.NextResult())
                            {
                                while (rdr.Read())
                                {
                                    Gresult04.Add(new Gresult04
                                    {
                                        Item = rdr["Item"].ToString(),
                                        CntOfPRNo = int.Parse(rdr["CntOfPRNo"].ToString()),
                                    });
                                }
                                sumpr.Gresult04 = Gresult04;
                            }
                            if (rdr.NextResult())
                            {
                                while (rdr.Read())
                                {
                                    Gresult05.Add(new Gresult05
                                    {
                                        Item = rdr["Item"].ToString(),
                                        Purchase = int.Parse(rdr["Purchase"].ToString()),
                                        Section_Mrg = int.Parse(rdr["Section_Mrg"].ToString()),
                                        Division_Mgr = int.Parse(rdr["Division_Mgr"].ToString()),
                                        Group_Division_Mgr = int.Parse(rdr["Group_Division_Mgr"].ToString()),
                                        Factory_Mgr = int.Parse(rdr["Factory_Mgr"].ToString()),
                                        President = int.Parse(rdr["President"].ToString()),
                                    });
                                }
                                sumpr.Gresult05 = Gresult05;
                            }
                        }
                    }


                    ///////////////////////////////////Start PR G1////////////////////////////////////////

                    List<G1PR> G1PR = new List<G1PR>();

                    List<Kid0111> Kids1 = new List<Kid0111>();  ///for no

                    foreach (var item1 in Gresult01.Select(x => x.Item).Distinct())
                    {
                        List<Kid01> Kid01 = new List<Kid01>();
                        foreach (var item2 in result01.Where(x => x.Item.Contains(item1)).Select(x => new { x.OpeDate }).Distinct().OrderBy(x => x.OpeDate))
                        {
                            List<Kid011> Kid011 = new List<Kid011>();
                            foreach (var item3 in result02.Where(x => x.OpeDate == item2.OpeDate).Select(x => new { x.BuyerCode }).Distinct().OrderBy(x => x.BuyerCode))
                            {
                                Kid011.Add(new Kid011
                                {
                                    data = result02.Where(x => x.OpeDate == item2.OpeDate && x.BuyerCode == item3.BuyerCode).FirstOrDefault(),
                                    Kids = Kids1,
                                });
                            }

                            Kid01.Add(new Kid01
                            {
                                data = result01.Where(x => x.Item.Contains(item1) && x.OpeDate == item2.OpeDate).FirstOrDefault(),
                                Kids = Kid011,
                            });
                        }

                        G1PR.Add(new G1PR
                        {
                            data = Gresult01.Where(x => x.Item == item1).FirstOrDefault(),
                            Kids = Kid01,
                        });

                    }

                    sumpr.G1PR = G1PR;
                    ///////////////////////////////////End PR G1////////////////////////////////////////

                    ///////////////////////////////////Start PR G2////////////////////////////////////////

                    List<G2PR> G2PR = new List<G2PR>();

                    List<Kid022> Kids2 = new List<Kid022>();  ///for no
                    foreach (var item1 in Gresult02.Select(x => new { x.Item, x.BuyerCode }).Distinct().OrderBy(x => x.BuyerCode))
                    {
                        List<Kid02> Kid02 = new List<Kid02>();
                        foreach (var item2 in result02.Where(x => x.item.Contains(item1.Item) && x.BuyerCode == item1.BuyerCode).Select(x => new { x.BuyerCode, x.OpeDate }).Distinct().OrderBy(x => x.BuyerCode))
                        {
                            Kid02.Add(new Kid02
                            {
                                data = result02.Where(x => x.item.Contains(item1.Item) && x.BuyerCode == item2.BuyerCode && x.OpeDate == item2.OpeDate).FirstOrDefault(),
                                Kids = Kids2,
                            });
                        }
                        G2PR.Add(new G2PR
                        {
                            data = Gresult02.Where(x => x.Item == item1.Item && x.BuyerCode == item1.BuyerCode).FirstOrDefault(),
                            Kids = Kid02,
                        });
                    }
                    sumpr.G2PR = G2PR;
                    //////////////////////////////////////Start PR G2////////////////////////////////////////

                    ///////////////////////////////////Start PR G3////////////////////////////////////////

                    List<G3PR> G3PR = new List<G3PR>();

                    List<Kid033> Kids3 = new List<Kid033>();  ///for no
                    foreach (var item1 in Gresult03)
                    {
                        List<Kid03> Kid03 = new List<Kid03>();
                        foreach (var item2 in result03.Select(x => new { x.BuyerCode }).Distinct().OrderBy(x => x.BuyerCode))
                        {
                            Kid03.Add(new Kid03
                            {
                                data = result03.Where(x => x.BuyerCode == item2.BuyerCode).FirstOrDefault(),
                                Kids = Kids3,
                            });
                        }
                        G3PR.Add(new G3PR
                        {
                            data = Gresult03.FirstOrDefault(),
                            Kids = Kid03,
                        });
                    }
                    sumpr.G3PR = G3PR;
                    //////////////////////////////////////Start PR G3////////////////////////////////////////

                    ///////////////////////////////////Start PR G4////////////////////////////////////////

                    List<G4PR> G4PR = new List<G4PR>();

                    List<Kid044> Kids4 = new List<Kid044>();  ///for no
                    foreach (var item1 in Gresult04)
                    {
                        List<Kid04> Kid04 = new List<Kid04>();
                        foreach (var item2 in result04.Select(x => new { x.BuyerCode, x.Create_Date }).Distinct().OrderBy(x => x.Create_Date).ThenBy(x => x.BuyerCode))
                        {
                            Kid04.Add(new Kid04
                            {
                                data = result04.Where(x => x.BuyerCode == item2.BuyerCode && x.Create_Date == item2.Create_Date).FirstOrDefault(),
                                Kids = Kids4,
                            });
                        }
                        G4PR.Add(new G4PR
                        {
                            data = Gresult04.FirstOrDefault(),
                            Kids = Kid04,
                        });
                    }
                    sumpr.G4PR = G4PR;
                    //////////////////////////////////////Start PR G4////////////////////////////////////////


                    ///////////////////////////////////Start PR G5////////////////////////////////////////

                    List<G5PR> G5PR = new List<G5PR>();

                    List<Kid055> Kids5 = new List<Kid055>();  ///for no
                    foreach (var item1 in Gresult05)
                    {
                        List<Kid05> Kid05 = new List<Kid05>();
                        foreach (var item2 in result05.Select(x => new { x.Approve_Date, x.Operator, x.Level_App_Code }).Distinct()
                            .OrderBy(x => x.Approve_Date).ThenBy(x => x.Level_App_Code).ThenBy(x => x.Operator))
                        {
                            Kid05.Add(new Kid05
                            {
                                data = result05.Where(x => x.Approve_Date == item2.Approve_Date && x.Operator == item2.Operator && x.Level_App_Code == item2.Level_App_Code).FirstOrDefault(),
                                Kids = Kids5,
                            });
                        }
                        G5PR.Add(new G5PR
                        {
                            data = Gresult05.FirstOrDefault(),
                            Kids = Kid05,
                        });
                    }
                    sumpr.G5PR = G5PR;
                    //////////////////////////////////////Start PR G5////////////////////////////////////////
                    conn.Close();
                    objCmd.Parameters.Clear();
                    return sumpr;
                }
            }
        }
    }
}



