using Microsoft.Extensions.Configuration;
using SmartOffice.Models;
using SmartOffice.Models.ViewModel;
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
    public class ConnJobStatus
    {
        private IConfiguration configuration;

        public ConnJobStatus(IConfiguration configuration)
        {
            this.configuration = configuration;
        }



        public DataForm GetDataForm(string strCatgChart, DateTime startdate, DateTime enddate , string userid)
        {
            DataSet ds = new DataSet();
            DataForm dataForm = new DataForm();

            ds = GetDataChart(strCatgChart , startdate, enddate, userid);

            List<DeptForm> deptForms = new List<DeptForm>();
            List<CountForm> countForms = new List<CountForm>();

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                deptForms.Add(new DeptForm()
                {
                    _data = row["GroupName"].ToString().Trim(),
                });

                countForms.Add(new CountForm()
                {
                    _data = Convert.ToInt32(row["Amount"].ToString()),
                });

            }

            dataForm.deptForms = deptForms.ToList();
            dataForm.countForms = countForms.ToList();
        
            return dataForm;

        }

        public DataI GetDataI(string strCatgChart, DateTime startdate, DateTime enddate, string userid)
        {
            DataSet ds = new DataSet();
            DataI dataI = new DataI();

            ds = GetDataChart(strCatgChart, startdate, enddate, userid);

            List<DeptI> deptIs = new List<DeptI>();
            List<CountI> countIs = new List<CountI>();

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                deptIs.Add(new DeptI()
                {
                    _data = row["GroupName"].ToString().Trim(),
                });

                countIs.Add(new CountI()
                {
                    _data = Convert.ToInt32(row["Amount"].ToString()),
                });

            }

            dataI.deptIs = deptIs.ToList();
            dataI.countIs = countIs.ToList();

            return dataI;

        }

        public DataC GetDataC(string strCatgChart, DateTime startdate, DateTime enddate, string userid)
        {
            DataSet ds = new DataSet();
            DataC dataC = new DataC();

            ds = GetDataChart(strCatgChart, startdate, enddate, userid);

            List<DeptC> deptCs = new List<DeptC>();
            List<CountC> countCs = new List<CountC>();

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                deptCs.Add(new DeptC()
                {
                    _data = row["GroupName"].ToString().Trim(),
                });

                countCs.Add(new CountC()
                {
                    _data = Convert.ToInt32(row["Cancel"].ToString()),
                });

            }

            dataC.deptCs = deptCs.ToList();
            dataC.countCs = countCs.ToList();

            return dataC;

        }

        public DataCO GetDataCO(string strCatgChart, DateTime startdate, DateTime enddate, string userid)
        {
            DataSet ds = new DataSet();
            DataCO dataCO = new DataCO();

            ds = GetDataChart(strCatgChart, startdate, enddate, userid);

            List<DeptCO> deptCOs = new List<DeptCO>();
            List<CountCO> countCOs = new List<CountCO>();

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                deptCOs.Add(new DeptCO()
                {
                    _data = row["GroupName"].ToString().Trim(),
                });

                countCOs.Add(new CountCO()
                {
                    _data = Convert.ToInt32(row["Complete"].ToString()),
                });

            }

            dataCO.deptCOs = deptCOs.ToList();
            dataCO.countCOs = countCOs.ToList();

            return dataCO;

        }

        public DataD GetDataD(string strCatgChart, DateTime startdate, DateTime enddate, string userid)
        {
            DataSet ds = new DataSet();
            DataD dataD = new DataD();

            ds = GetDataChart(strCatgChart, startdate, enddate, userid);

            List<DeptD> deptDs = new List<DeptD>();
            List<CountD> countDs = new List<CountD>();

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                deptDs.Add(new DeptD()
                {
                    _data = row["GroupName"].ToString().Trim(),
                });

                countDs.Add(new CountD()
                {
                    _data = Convert.ToInt32(row["Draft"].ToString()),
                });

            }

            dataD.deptDs = deptDs.ToList();
            dataD.countDs = countDs.ToList();

            return dataD;

        }

        public DataP GetDataP(string strCatgChart, DateTime startdate, DateTime enddate, string userid)
        {
            DataSet ds = new DataSet();
            DataP dataP = new DataP();

            ds = GetDataChart(strCatgChart, startdate, enddate, userid);

            List<DeptP> deptPs = new List<DeptP>();
            List<CountP> countPs = new List<CountP>();

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                deptPs.Add(new DeptP()
                {
                    _data = row["GroupName"].ToString().Trim(),
                });

                countPs.Add(new CountP()
                {
                    _data = Convert.ToInt32(row["Process"].ToString()),
                });

            }

            dataP.deptPs = deptPs.ToList();
            dataP.countPs = countPs.ToList();

            return dataP;

        }

        public DataR GetDataR(string strCatgChart, DateTime startdate, DateTime enddate, string userid)
        {
            DataSet ds = new DataSet();
            DataR dataR = new DataR();

            ds = GetDataChart(strCatgChart, startdate, enddate, userid);

            List<DeptR> deptRs = new List<DeptR>();
            List<CountR> countRs = new List<CountR>();

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                deptRs.Add(new DeptR()
                {
                    _data = row["GroupName"].ToString().Trim(),
                });

                countRs.Add(new CountR()
                {
                    _data = Convert.ToInt32(row["Reject"].ToString()),
                });

            }

            dataR.deptRs = deptRs.ToList();
            dataR.countRs = countRs.ToList();

            return dataR;

        }


        public DataTableData GetTableData(DateTime startdate, DateTime enddate, string mode, string userid,string label ,string title)
        {
            DataSet ds = new DataSet();
            DataTableData dataTableData = new DataTableData();

            ds = GetDataTable(startdate, enddate,mode, userid, label,title);

            List<TableData> tableDatas = new List<TableData>();
            

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                tableDatas.Add(new TableData()
                {
                    DocumentNo = row["DocumentNo"].ToString().Trim(),
                    DocumentNameE = row["DocumentNameE"].ToString().Trim(),
                    DocumentNameT = row["DocumentNameT"].ToString().Trim(),
                    DocumentNameJ = row["DocumentNameJ"].ToString().Trim(),
                    IssuedDate = Convert.ToDateTime( row["IssuedDate"].ToString()),
                    ReqOperatorName = row["ReqOperatorName"].ToString().Trim(),
                    DocumentStatus = row["DocumentStatus"].ToString().Trim(),
                    ReqDescription1 = row["ReqDescription1"].ToString().Trim(),
                    DocumentCode = row["DocumentCode"].ToString().Trim(),
                });               
            }

            dataTableData.tableDatas = tableDatas.ToList();
            

            return dataTableData;

        }


        public DataSet GetDataChart(string strCatgChart, DateTime startdate, DateTime enddate, string userid)
        {
            DataSet ds = new DataSet();
            SqlCommand objCmd = new SqlCommand();
            var strStored = "";
            strStored = "sprFormJobStatus '" + strCatgChart + "', '" + startdate.ToString("yyyy-MM-dd") + "','" + enddate.ToString("yyyy-MM-dd") + "','" + userid + "'";
            ds = GetDataSet(strStored);
            
            return ds;
        }

        public DataSet GetDataTable(DateTime startdate, DateTime enddate,string mode, string userid, string label, string title)
        {
            DataSet ds = new DataSet();
            SqlCommand objCmd = new SqlCommand();
            var strStored = "";
            strStored = "sprFormJobStatusDetail '" + startdate.ToString("yyyy-MM-dd") + "','" + enddate.ToString("yyyy-MM-dd") + "','" + userid + "','" + label + "','" + title + "','" + mode + "'";
            ds = GetDataSet(strStored);

            return ds;
        }

        public DataTable GetDatatables(string Sql)
        {
            DataTable dt = new DataTable();
            try
            {
                string query = Sql;
                string constr = configuration.GetConnectionString("DefaultConnection2");
                DataSet ds = new DataSet();
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        con.Open();
                        SqlDataAdapter adpterdata = new SqlDataAdapter();
                        adpterdata.SelectCommand = new SqlCommand(query, con);
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
        }
        public DataSet GetDataSet(string Sql)
        {
            DataSet ds = new DataSet();
            try
            {
                string query = Sql;
                string constr = configuration.GetConnectionString("DefaultConnection2");
                //string constr = ConfigurationManager.ConnectionStrings["BOIDbContext1"].ConnectionString; 
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        con.Open();
                        SqlDataAdapter adpterdata = new SqlDataAdapter();
                        adpterdata.SelectCommand = new SqlCommand(query, con);
                        adpterdata.Fill(ds);
                        con.Close();
                        return ds;
                    }
                }
            }
            catch (Exception e)
            {
                var dsa = e;
                return ds;
            }
        }
    }
}
