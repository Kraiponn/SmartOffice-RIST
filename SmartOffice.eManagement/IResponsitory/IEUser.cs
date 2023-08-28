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
    public interface IEUser
    {
        TupleUser GetGroup(string OpeGroupCateg, string Division, string Section, string Department, string Department2, string Department3, string username, string SpecialGroup);        
        TupleUser GetGenerate(string strObj, string strObMode, string OperationCode, string OpeGroupCateg, string InputKind, string OperationNo, string UserName);

        List<GetMessage> GetMessage();
        List<SelectListItemCateg> GetItemCateglist();

        dynamic AddMessage(string itemCateg, string displayOrder, string message, string startDate, string endDate, string UserName, string ComputerName);
        dynamic DeleteMessage(GetMessage getMessage);
    }
}
