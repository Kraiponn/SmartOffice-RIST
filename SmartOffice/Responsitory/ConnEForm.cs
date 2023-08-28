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
    public class ConnEForm
    {
        private IConfiguration configuration;

        public ConnEForm(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

       
        public TupleFormChartDataLabels GetChartDatalabels(DateTime startdate, DateTime enddate, int nochart, string mode)
        {
            DataSet ds = new DataSet();
            TupleFormChartDataLabels tupleFormChartDataLabels = new TupleFormChartDataLabels();
            ds = GetDataChart(startdate, enddate, nochart,  mode);
            List<Labels> datalabels = new List<Labels>();
            try
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    datalabels.Add(new Labels()
                    {
                        _datanochart = Convert.ToInt32(row["nochart"].ToString()),
                        _datatypechart = row["typechart"].ToString().Trim(),
                        _datalabels = row["labels"].ToString().Trim(),
                        _datalabel = row["label"].ToString().Trim(),
                        _dataamount = row["amount"].ToString(),


                    });
                }

                tupleFormChartDataLabels.labels = datalabels.ToList();
                return tupleFormChartDataLabels;
            }
            catch
            {
                return tupleFormChartDataLabels;
            }
            finally
            {
                ds.Dispose();
                tupleFormChartDataLabels = null;
                datalabels = null;
            }
          
        }

        public TupleFormMasterDashboard GetMasterCharge(DateTime startdate, DateTime enddate, int nochart, string mode)
        {
            TupleFormMasterDashboard tupleFormMasterDashboard = new TupleFormMasterDashboard();
            List<FormMasterDashboard> formMasterDashboards = new List<FormMasterDashboard>();
            try
            {
                using (var ds = GetDataChart(startdate, enddate, nochart, mode))
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        formMasterDashboards.Add(new FormMasterDashboard()
                        {
                            nochart = Convert.ToInt32(row["nochart"].ToString()),
                            namechart = row["namechart"].ToString().Trim(),
                            typechart = row["typechart"].ToString().Trim(),
                            code = row["code"].ToString().Trim(),
                            sub = row["sub"].ToString().Trim()
                        });
                    }

                    tupleFormMasterDashboard.formMasterDashboards = formMasterDashboards.ToList();
                    return tupleFormMasterDashboard;
                }
            }
            catch
            {
                tupleFormMasterDashboard = null;
                formMasterDashboards = null;
                return null;
            }
            finally
            {
                tupleFormMasterDashboard = null;
                formMasterDashboards = null;
            }
                       
        }


        public DataSet GetDataChart(DateTime startdate, DateTime enddate, int chartno, string mode)
        {
            var strStored = "";
            strStored = "sprDbDashboardSummary  '" + startdate.ToString("yyyy-MM-dd") + "','" + enddate.ToString("yyyy-MM-dd") + "' , " + chartno + ", '" + mode + "'";
            using (var ds = GetDataSet(strStored))
            {               
                return ds;
            }
                
        }

        

        public DataTable GetDatatables(string Sql)
        {
            DataTable dt = new DataTable();
            try
            {
                string query = Sql;
                string constr = configuration.GetConnectionString("DefaultConnection7");
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
                        cmd.Parameters.Clear();
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
        public DataSet GetDataSet(string Sql)
        {
            DataSet ds = new DataSet();
            try
            {
                string query = Sql;
                string constr = configuration.GetConnectionString("DefaultConnection7");
                //string constr = ConfigurationManager.ConnectionStrings["BOIDbContext1"].ConnectionString; 
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        con.Open();
                        SqlDataAdapter adpterdata = new SqlDataAdapter
                        {
                            SelectCommand = new SqlCommand(query, con)
                        };
                        adpterdata.Fill(ds);
                        con.Close();
                        cmd.Parameters.Clear();
                        return ds;
                    }
                }
            }
            catch (Exception e)
            {
                var dsa = e;
                return ds;
            }
            finally
            {
                ds.Dispose();
            }
        }
    }
}
