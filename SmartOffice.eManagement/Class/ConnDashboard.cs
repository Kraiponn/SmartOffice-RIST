using Microsoft.Extensions.Configuration;
using SmartOffice.eManagement.ModelsManagementControl;
using SmartOffice.eManagement.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SmartOffice.eManagement.Class
{
    class ConnDashboard
    {
        private readonly IConfiguration configuration;
        private readonly string constr;
        public ConnDashboard(IConfiguration configuration)
        {
            this.configuration = configuration;
            constr = configuration.GetConnectionString("DefaultConnection7");
        }
        public DataSet GetDashboard(string DashboardId, string StartDate, string EndDate)
        {
            DataSet ds = new DataSet();


            try
            {
                using (SqlConnection conn = new SqlConnection(constr))
                {
                    SqlCommand objCmd = new SqlCommand();
                    var strStored = "";
                    strStored = "sprDbDashboard";
                    objCmd.Parameters.Add(new SqlParameter("@id", DashboardId));
                    objCmd.Parameters.Add(new SqlParameter("@StartDate", StartDate));
                    objCmd.Parameters.Add(new SqlParameter("@EndDate", EndDate));
                

                    objCmd.Connection = conn;
                    objCmd.CommandText = strStored;
                    objCmd.CommandType = CommandType.StoredProcedure;

                    // SqlDataReader need an open conncetion, so check and open it.
                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    // Read data by using Execute Reader
                    //SqlDataReader dr = objCmd.ExecuteReader(CommandBehavior.CloseConnection);
                    var adapter = new SqlDataAdapter(objCmd);

                    adapter.Fill(ds);
                    adapter.Dispose();
                    objCmd.Dispose();
                    conn.Close();
                    int count = ds.Tables.Count;  //it will give the total number of tables in your dataset.
                    return ds;
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
