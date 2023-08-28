
using SmartOffice.eManagement.ModelsManagementControl;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SmartOffice.eManagement.Models;
using System.Data;

namespace SmartOffice.EManagement.IResponsitory
{
    public interface IEForm
    {
        TupleForm GetGroup(string OpeGroupCateg, string Division, string Section, string Department, string Department2, string Department3, string username, string SpecialGroup);
        TupleForm GetInquiry(string strObj, string strObMode, string strObjSub, string OperationCode, string OpeGroupCateg, string InputKind, string OperationNo, string UserName);
        TupleForm GetGenerate(string strObj, string strObMode, string OperationCode, string OpeGroupCateg, string InputKind, string OperationNo, string UserName);
        dynamic MaintenanceForm(TupleForm tupleForm, string strObj, string strObMode, string strObjSub, string OperationCode, string OpeGroupCateg, string InputKind, string OperationNo, IFormCollection frm, string CreateBy);
        DataTable GetReport(string strObj, string strObMode, string OperationCode, string OpeGroupCateg, string InputKind, string OperationNo, string UserName);
        dynamic GetTableConst(string strObj, string strObMode, string OperationCode, string OpeGroupCateg, string InputKind, string OperationNo, string UserName);
        dynamic GetCol(string code);
        Task<dynamic> GetValueList(string ValueCode);
        dynamic GetFormCondition(string OperationNo, string OperationCode);
        dynamic GetSummaryReport(string OperationCode, string StartDate, string EndDate);
    }
}
