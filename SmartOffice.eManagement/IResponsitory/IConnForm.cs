using SmartOffice.eManagement.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SmartOffice.eManagement.IResponsitory
{
    public interface IConnForm
    {
        TupleForm GetGroup(string OpeGroupCateg, string Division, string Section, string Department, string Department2, string Department3, string username, string SpecialGroup);
        TupleForm GetInquiry(string strObj, string strObMode, string strObjSub, string OperationCode, string OpeGroupCateg, string InputKind, string OperationNo, DataTable dataTable, string UserName);
        DataTable GetReport(string strObj, string strObMode, string OperationCode, string OpeGroupCateg, string InputKind, string OperationNo, DataTable dataTable, string UserName);
        DataTable GetSummaryReport(string OperationCode, string StartDate, string EndDate);
        TupleForm GetGenerate(string strObj, string strObMode, string OperationCode, string OpeGroupCateg, string InputKind, string OperationNo, DataTable dataTable, string UserName);
        dynamic MaintenanceForm(DataTable TableList, DataTable dataTable, string strObj, string strObMode, string strObjSub, string OperationCode, string OpeGroupCateg, string InputKind, string OperationNo, string UserName);
    }
}
