using Microsoft.Extensions.Configuration;
using SmartOffice.IResponsitory;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SmartOffice.Responsitory
{
    public class InqueryDataService: IInqueryDataService
    {
        private readonly IConfiguration _configuration;
        public InqueryDataService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public DataTable GetDataInquery(string DocCode, string StartDate, string EndDate ,string UserId)
        {
            var dp = new ConnInquiryData(_configuration);
            var data = dp.GetDataInquery(DocCode, StartDate, EndDate, UserId);
            return data;
        }
        public DataTable GetDataInquery2(string DocCode, string StartDate, string EndDate, string UserId)
        {
            var dp = new ConnInquiryData(_configuration);
            var data = dp.GetDataInquery2(DocCode, StartDate, EndDate, UserId);
            return data;
        }
        public DataTable GetDocmentData()
        {
            var dp = new ConnInquiryData(_configuration);
            var data = dp.GetDocmentData();
            return data;
        }

        public DataTable GetInputItem(string DocCode)
        {
            var dp = new ConnInquiryData(_configuration);
            var data = dp.GetInputItem(DocCode);
            return data;
        }
    }
}
