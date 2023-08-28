using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartOffice.Models;
using SmartOffice.ModelsDocControl;
using SmartOffice.ModelsForm;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SmartOffice.IResponsitory
{
    public interface IDynamicFormService
    {
        Task<Tuple1> GetModelAsync(string code, string docno, string mode, string seq ,string CreateBy);
        Task<dynamic> UpdateForm(Tuple1 Tuple1, IFormCollection frm, string DocNo, string DocCode, string QueryMode,string Comment, string Namebtn, string Idbtn, string Valbtn, string Mode, string signature, string radiochksign, string CreateBy, string CreateName,string Division,string Section,string Department,string Department2,string Department3,string GroupCateg, string Seq);
        dynamic GetFileAttach(string DocNo, string DocCode);
        Task<dynamic> DeletefileAsync(string DocNo, string Filename,string Username);
        Task<dynamic> Attachfile(List<IFormFile> files, string DocNo, string DocCode, string Addressfile, string Username);
        dynamic UploadImage(List<IFormFile> files, string DocNo, string DocCode, string ItemCode, string Username);
        dynamic Base64PDF(string DocNo, string DocCode);
        dynamic Base64PDFManual(string DocCode);
        Task<dynamic> DeleteDraft(string DocNo, string CreateBy, string CreateName,string Division, string Section, string Department, string Department2, string Department3, string GroupCateg);
        dynamic GetTableConst(string code, string docno);
        dynamic GetCol(string code);
        dynamic GetDataInTable(string docid);
        SettingRole GetSettingRoleModel(string GroupCateg);
        dynamic Getmasterrole(string GroupCateg);
        dynamic GetDoc(string GroupCateg);
        dynamic GetmasterFlow(string flowid);
        dynamic Getmasteruser(string role);
        dynamic UpdateRoleMaster(Role role, string Username, string GroupCateg);
        dynamic DeleteRoleMaster(Role role,string Username);
        dynamic DeleteUserMaster(OperatorRole user, string Username);
        dynamic AddRoleMaster(string remarks, string Username, string GroupCateg);
        Task<dynamic> DeleteImage(string DocNo,string ItemCode, string Username);
        dynamic AddUserRoleMaster(string listboxuser, string RoleId, string Username);
        dynamic AddDocMaster(string flowID, string seqNo, string roleID, string roleIDold, string requirement, string requirementRemark, string assignFlag, string assignFlagRemark, string Username, string assignFlagBySeq, string reject);
        dynamic GetFormCondition(string docno, string doccode);
        dynamic GetMenu();
        Task<dynamic> CheckValidateFileMachine(string DocCode, string DocNo);
        List<UserFlow> GetUserFlow(string DocCode);
        dynamic Getprogress(string DocCode, string DocNo);
        byte[] StreamPDF(string DocNo, string DocCode);
        Task<List<string>> GetListAttachFileAsync(string DocNo);
        Task<List<string>> GetItemMachineProcess();
        Task<List<ValueList>> GetOperationName();
        Task<dynamic> ListApproved(List<string> DocumentNo,string EmpID,string EmpName, string Division, string Section, string Department, string Department2, string Department3, string GroupCateg);
        Task<dynamic> GetValueList(string ValueCode);
        Task<dynamic> TransferForm( string DocNo, string DocCode, string Namebtn, string Idbtn, string Valbtn,  string userid , string seq, string OPIDTransfer, string Division, string Section, string Department, string Department2, string Department3, string GroupCateg);
        //void SetCountPageAttachfile();
        dynamic GetSpecialFlow(string id);
        dynamic GetFlowDetail(string id);
        Task<dynamic> AssignFlowsAsync(string id, List<int> Allseq, List<FlowsPeople> AllPeople, string username);
        dynamic GetAllEmp();
        Task SetCountPageAttachfileAsync();
        dynamic GetMyDoc(string UserName);
        dynamic GetGroupDoc(string GroupCateg, string UserName);
        dynamic GetMyApproved(string UserName);
        Task<dynamic> Getuserautodetail(string userid);
        dynamic Getempdata(string empid);
        List<ExportForm> LoadForm(string code, string from, string to, string fromapp, string toapp, string fromseqno, string toseqno, string fromempno, string toempno, string status, string dates, string username);
    }
}
