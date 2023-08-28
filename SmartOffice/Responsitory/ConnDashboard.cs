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
    public class ConnDashboard
    {
        private IConfiguration configuration;

        public ConnDashboard(IConfiguration configuration)
        {
            this.configuration = configuration;
        }



        public DataChartAllformDoc GetAllformDoc(string ChartCatg)
        {
            DataSet ds = new DataSet();
            DataChartAllformDoc _DataAllFormDoc = new DataChartAllformDoc();

            ds = GetCountDocDept(ChartCatg);

            List<DataChartDayDetailunit> countDocList = new List<DataChartDayDetailunit>();
            List<DataChartDayDetailNameDepart> DeptList = new List<DataChartDayDetailNameDepart>();
            try
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    DeptList.Add(new DataChartDayDetailNameDepart()
                    {
                        _data = row["OpeGroupCateg"].ToString().Trim(),
                    });

                    countDocList.Add(new DataChartDayDetailunit()
                    {
                        _data = Convert.ToInt32(row["countDoc"].ToString()),
                    });

                }


                _DataAllFormDoc.NameDepart = DeptList.ToList();
                _DataAllFormDoc.CountDoc = countDocList.ToList();
                return _DataAllFormDoc;
            }
            catch
            {
                return _DataAllFormDoc;
            }
            finally
            {
                _DataAllFormDoc = null;
                ds.Dispose();
            }

           

        }

        public DataChartcurrentmonthDoc GetCurrentmonthlyDoc(string ChartCatg)
        {

            DataSet ds1 = new DataSet();
            DataChartcurrentmonthDoc _DataAllFormDoc = new DataChartcurrentmonthDoc();
            List<DataChartDayDetailNameDepart> DeptList = new List<DataChartDayDetailNameDepart>();
            List<DataChartDayDetailunit> DaftDocList = new List<DataChartDayDetailunit>();
            List<DataChartDayDetailunit> ProcessDocList = new List<DataChartDayDetailunit>();
            List<DataChartDayDetailunit> CompleteDocList = new List<DataChartDayDetailunit>();
            try
            {
                ds1 = GetCountDocDept(ChartCatg);
               

                foreach (DataRow row in ds1.Tables[0].Rows)
                {
                    DeptList.Add(new DataChartDayDetailNameDepart()
                    {
                        _data = row["GroupName"].ToString().Trim(),
                    });

                    DaftDocList.Add(new DataChartDayDetailunit()
                    {
                        _data = Convert.ToInt32(row["Draft"].ToString()),
                    });
                    ProcessDocList.Add(new DataChartDayDetailunit()
                    {
                        _data = Convert.ToInt32(row["Process"].ToString()),
                    });
                    CompleteDocList.Add(new DataChartDayDetailunit()
                    {
                        _data = Convert.ToInt32(row["Complete"].ToString()),
                    });

                }

                _DataAllFormDoc.NameDepart = DeptList.ToList();
                _DataAllFormDoc.DaftDoc = DaftDocList.ToList();
                _DataAllFormDoc.ProcessDoc = ProcessDocList.ToList();
                _DataAllFormDoc.CompleteDoc = CompleteDocList.ToList();

                return _DataAllFormDoc;
            }
            catch
            {
                return _DataAllFormDoc;
            }
            finally
            {
                _DataAllFormDoc =null;
                DeptList = null;
                ProcessDocList = null;
                CompleteDocList = null;
                DaftDocList = null;
                ds1.Dispose();
            }        
        }
         
        public DataSet GetCountDocDept(string ChartCatg)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlCommand objCmd = new SqlCommand();
                var strStored = "";
                strStored = "sprzEDashboard '" + ChartCatg + "'";
                ds = GetDataSet(strStored);
                return ds;
            }
            catch
            {
                return ds;
            }
            finally
            {
                ds.Dispose();
            }
           
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
            finally
            {
                ds.Dispose();
            }
        }
       
    }
}
