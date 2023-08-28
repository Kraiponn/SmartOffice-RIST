
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SmartOffice.Responsitory
{
    public class ConnInquiryData
    {
        private IConfiguration configuration;

        public ConnInquiryData(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public DataTable GetDataInquery(string DocCode, string StartDate, string EndDate, string UserId)

        {
            DataTable dt = new DataTable();
            try
            {              
                var constr1 = configuration.GetConnectionString("DefaultConnection2");
                using (SqlConnection con = new SqlConnection(constr1))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        var strStored = "";
                        cmd.Connection = con;
                        con.Open();
                        strStored = "sprInquiryData";
                        cmd.Parameters.Add(new SqlParameter("@DocCode", DocCode));
                        cmd.Parameters.Add(new SqlParameter("@StartDate", StartDate));
                        cmd.Parameters.Add(new SqlParameter("@EndDate", EndDate));
                        cmd.Parameters.Add(new SqlParameter("@userid", UserId));
                        cmd.CommandText = strStored;
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlDataAdapter adpterdata = new SqlDataAdapter
                        {
                            SelectCommand = cmd
                        };
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
        public DataTable GetDataInquery2(string DocCode, string StartDate, string EndDate, string UserId)

        {
            DataTable dt = new DataTable();
            try
            {
                var constr1 = configuration.GetConnectionString("DefaultConnection2");
                using (SqlConnection con = new SqlConnection(constr1))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        var strStored = "";
                        cmd.Connection = con;
                        con.Open();
                        strStored = "sprInquiryData2";
                        cmd.Parameters.Add(new SqlParameter("@DocCode", DocCode));
                        cmd.Parameters.Add(new SqlParameter("@StartDate", StartDate));
                        cmd.Parameters.Add(new SqlParameter("@EndDate", EndDate));
                        cmd.Parameters.Add(new SqlParameter("@userid", UserId));
                        cmd.CommandText = strStored;
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlDataAdapter adpterdata = new SqlDataAdapter
                        {
                            SelectCommand = cmd
                        };
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
        public DataTable GetDocmentData()

        {
            DataTable dt = new DataTable();
            try
            {
                DataSet ds = new DataSet();
                var constr1 = configuration.GetConnectionString("DefaultConnection");
                using (SqlConnection con = new SqlConnection(constr1))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        var strStored = "";
                        cmd.Connection = con;
                        con.Open();
                        strStored = "SELECT [Param] ,[GroupName],CASE WHEN [MenuNameT] = '' OR [MenuNameT] IS NULL THEN [MenuNameE] ELSE [MenuNameT] END AS MenuNameT FROM [ESmartOffice].[dbo].[vewFormMenu] order by MenuNameT";                        
                        cmd.CommandText = strStored;
                        cmd.CommandType = CommandType.Text;
                        SqlDataAdapter adpterdata = new SqlDataAdapter
                        {
                            SelectCommand = cmd
                        };
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

        public DataTable GetInputItem(string DocCode)

        {
            DataTable dt = new DataTable();
            try
            {
                DataSet ds = new DataSet();
                var constr1 = configuration.GetConnectionString("DefaultConnection2");
                using (SqlConnection con = new SqlConnection(constr1))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        var strStored = "";
                        cmd.Connection = con;
                        con.Open();
                        strStored = "sprInputItemData";
                        cmd.Parameters.Add(new SqlParameter("@DocCode", DocCode));
                        cmd.CommandText = strStored;
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlDataAdapter adpterdata = new SqlDataAdapter
                        {
                            SelectCommand = cmd
                        };
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


    }
}
