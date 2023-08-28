using Microsoft.AspNetCore.Http;
using SmartOffice.SurveyApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace SmartOffice.SurveyApp.IResponsitory
{
    public interface ISurveyApp 
    {
        Task<FormModel> GetQuestions(string CateId,string Username);
        Task<bool> SaveFormData(IFormCollection form, string Username,string CreateName,string ComputerName);
        Task<OperatorModel> GetOperatorDataAsync(string Username);
        Task<List<VewFormInputHistory>> GetHistoryInputFormAsync(string Username);
        Task<FormModel> GetQuestionsUpdate(string ItemCateNo, string OperationCode, string ZTCNo, string KeyDate);
        Task<dynamic> DeleteFormData(string ItemCateNo, string OperationCode, string ZTCNo, string KeyDate);
        DataTable ReportDailyCheck(string Dept, string Dept2, string Dept3, string EmpNo);
    }
}
