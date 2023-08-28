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
    public class ConnDoc
    {
        private IConfiguration configuration;

        public ConnDoc(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public IEnumerable<vewJounalVoucherdata> GetVoucher(string monthperiod, string seccode)
        {
            string constr = configuration.GetConnectionString("DefaultConnection7");
            SqlConnection conn = new SqlConnection(constr);
            SqlCommand objCmd = new SqlCommand();
            var strStored = "";


            strStored = "sprJournalVoucher";
            objCmd.Parameters.Add(new SqlParameter("@SectionCode", seccode));
            objCmd.Parameters.Add(new SqlParameter("@OpMonth", monthperiod));
            conn.Open();
            objCmd.Connection = conn;
            objCmd.CommandText = strStored;
            objCmd.CommandType = CommandType.StoredProcedure;

            using (var reader = objCmd.ExecuteReader())
            {

                while (reader.Read())
                {
                    yield return new vewJounalVoucherdata
                    {

                        OpMonth = reader["OpMonth"].ToString(),
                        LineNumber = reader["LineNumber"].ToString(),
                        Descript = reader["Descript"].ToString(),
                        StkTakingAmount = reader["StkTakingAmount"].ToString(),
                        
                    };
                }
            }
            conn.Close();
            conn.Dispose();
            objCmd.Parameters.Clear();


        }
    }

    
}



