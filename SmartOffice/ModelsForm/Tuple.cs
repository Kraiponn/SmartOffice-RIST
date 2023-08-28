using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using SmartOffice.Models;
using SmartOffice.ModelsForm;
using SmartOffice.ModelsDocControl;

namespace SmartOffice.ModelsForm
{

    
    

    public class Tuple1
    {
        public List<ValueList> ValueList { get; set; }
        public List<VewDocumentItemList> vewDocumentItemList { get; set; }
        public List<DocumentItemDetail> DocumentItemDetail { get; set; }
       
        public List<Flow> Flows { get; set; }
        public List<CheckBoxFlow> CheckBoxFlows { get; set; }
        public List<UserFlow> UserFlow { get; set; }
        public List<UserFlowByRole> UserFlowByRole { get; set; }
        
        public List<SelectListItem> GroupDopdown { set; get; }
        public List<CheckBoxListItem> Groupcheckbox { get; set; }
        public List<RadioListItem> Groupspecial { get; set; }



        public List<RadioListItem> Groupradio { get; set; }
        public List<Language> Language { get; set; }
        public string Selectradio { get; set; }
        public string Comment { get; set; }
        public string Dateapprove { get; set; }
        public List<Listdatas> Tabledata { get; set; }

        public List<HrmsEmployee> HrmsEmployees { get; set; }

             
        public string SelectedUsersTransfer { get; set; }
        public SelectList SelectUsersTransferList { get; set; }

        public List<UsersTransfer> UsersTransferList { get; set; }

    }
    public partial class UsersTransfer
    {
        public string ID { get; set; }
        public string Name { get; set; }
    }


    public partial class HrmsEmployee
    {
        public string Value { get; set; }
        public string Text { get; set; }
       
    }

    public class Listdatas
    {
        public string Header { get; set; }
        public string Value { get; set; }
        public string Itemcode { get; set; }
        public string ValueE { get; set; }
        public string ValueT { get; set; }
        public string ValueJ { get; set; }
        public int DisplayOrder { get; set; }
        public string TableCode { get; set; }
        public int ItemId { get; set; }
        public string InputType { get; set; }
        public string DataType { get; set; }

    }

    public class TypeTableModel
    {
        public string Itemcode { get; set; }
        public List<Listdatas> Listdata { get; set; }
    }
    public class SelectListItem
    {
        public string Value { get; set; }
        public string Text { get; set; }
        public string Code { get; set; }
        public bool IsChecked { get; set; }
        public int Order { get; set; }

        public string DocumentCode { get; set; }
        public string ItemCateg { get; set; }
        public string DocumentNameE { get; set; }
        public string DocumentNameT { get; set; }
        public string DocumentNameJ { get; set; }
        public string ItemCode { get; set; }
        public string ItemNameE { get; set; }
        public string ItemNameT { get; set; }
        public string ItemNameJ { get; set; }

    }

    public class CheckBoxListItem
    {
        public string ID { get; set; }
        public string Display { get; set; }
        public bool IsChecked { get; set; }
        public string DocumentCode { get; set; }
        public string ValueCode { get; set; }
        public string ItemCateg { get; set; }
        public string DocumentNameE { get; set; }
        public string DocumentNameT { get; set; }
        public string DocumentNameJ { get; set; }
        public string ItemCode { get; set; }
        public string ItemNameE { get; set; }
        public string ItemNameT { get; set; }
        public string ItemNameJ { get; set; }
    }

    public class RadioListItem
    {
        public int Order { get; set; }
        public string ID { get; set; }
        public string Display { get; set; }
        public bool IsChecked { get; set; }
        public string DocumentCode { get; set; }
        public string ValueCode { get; set; }
        public string ItemCateg { get; set; }
        public string DocumentNameE { get; set; }
        public string DocumentNameT { get; set; }
        public string DocumentNameJ { get; set; }
        public string ItemCode { get; set; }
        public string ItemNameE { get; set; }
        public string ItemNameT { get; set; }
        public string ItemNameJ { get; set; }


    }

    public class Valuelistinput
    {
        
        
        public string Text { get; set; }
       
    }

    public class VewDocumentItemList
    {
        public string DocumentCode { get; set; }
        public string ItemCateg { get; set; }
        public string InputOption { get; set; }
        public int    DisplayOrder { get; set; }
        public string DocumentGroupCode { get; set; }
        public string DocumentNameE { get; set; }
        public string DocumentNameT { get; set; }
        public string DocumentNameJ { get; set; }
        public string DocumentKind { get; set; }
        public string OpeGroupCateg { get; set; }
        public string AttachedFile { get; set; }
        public string EmailSend { get; set; }
        public string ItemCategNameT { get; set; }
        public string ItemCategNameE { get; set; }
        public string ItemCategNameJ { get; set; }
        public string RemarksTitleT1 { get; set; }
        public string RemarksTitleT2 { get; set; }
        public string RemarksTitleT3 { get; set; }
        public string RemarksTitleT4 { get; set; }
        public string RemarksTitleT5 { get; set; }
        public string RemarksTitleT6 { get; set; }
        public string RemarksTitleT7 { get; set; }
        public string RemarksTitleT8 { get; set; }
        public string RemarksTitleT9 { get; set; }
        public string RemarksTitleT10 { get; set; }
        public string RemarksTitleE1 { get; set; }
        public string RemarksTitleE2 { get; set; }
        public string RemarksTitleE3 { get; set; }
        public string RemarksTitleE4 { get; set; }
        public string RemarksTitleE5 { get; set; }
        public string RemarksTitleE6 { get; set; }
        public string RemarksTitleE7 { get; set; }
        public string RemarksTitleE8 { get; set; }
        public string RemarksTitleE9 { get; set; }
        public string RemarksTitleE10 { get; set; }
        public string RemarksTitleJ1 { get; set; }
        public string RemarksTitleJ2 { get; set; }
        public string RemarksTitleJ3 { get; set; }
        public string RemarksTitleJ4 { get; set; }
        public string RemarksTitleJ5 { get; set; }
        public string RemarksTitleJ6 { get; set; }
        public string RemarksTitleJ7 { get; set; }
        public string RemarksTitleJ8 { get; set; }
        public string RemarksTitleJ9 { get; set; }
        public string RemarksTitleJ10 { get; set; }
        public string InputItemListItemCateg { get; set; }
        public string ItemCode { get; set; }
        public int InputItemListDisplayOrder { get; set; }
        public string ItemNameE { get; set; }
        public string ItemNameT { get; set; }
        public string ItemNameJ { get; set; }
        public string InputItemCode { get; set; }
        public string InputType { get; set; }
        public string DataType { get; set; }
        public string DecimalNo { get; set; }
        public string Unit { get; set; }
        public string ValueCode { get; set; }
        public string InputItemOption { get; set; }
        public string DefaultValue { get; set; }
        public bool ReadOnly { get; set; }
        public bool Required { get; set; }
        public string Maxlength { get; set; }
        public string Minlength { get; set; }
        public string Max { get; set; }
        public string Min { get; set; }
        public string Step { get; set; }
        public string valueold { get; set; }
        public string Language { get; set; }
        public string AttachedFile1 { get; set; }
        public string AttachedFile2 { get; set; }
        public string StandardNo { get; set; }
        public string ReviseNo { get; set; }
        public string Remark { get; set; }
        public string DetailOption { get; set; }
    }

    public class Flow
    {
        public string DocumentCode { get; set; }
        public string DocumentNo { get; set; }
        public string DocumentNameE { get; set; }
        public string DocumentNameT { get; set; }
        public string DocumentNameJ { get; set; }
        public string ApprovalItemCode { get; set; }
        public string ApprovalItemNameE { get; set; }
        public string ApprovalItemNameT { get; set; }
        public string ApprovalItemNameJ { get; set; }
        public int SeqNo { get; set; }
        public string ApprovalDate { get; set; }
        public string Comment { get; set; }
        public string Judge { get; set; }
        public string IssuedDate { get; set; }
        public string DocumentStatus { get; set; }
        public string ReqDescription1 { get; set; }
        public string ReqDescription2 { get; set; }
        public string ReqDescription3 { get; set; }
        public string ReqOperatorID { get; set; }
        public string ReqOperatorName { get; set; }
        public string DIVISION { get; set; }
        public string SECTION { get; set; }
        public string DEPARTMENT { get; set; }
        public string DEPARTMENT2 { get; set; }
        public string POSITION { get; set; }
        public string EMAIL1 { get; set; }
        public string EMAIL2 { get; set; }
        public string RoleID { get; set; }
        public string Requirement { get; set; }
        public string ApplicationName { get; set; }
        public string ValLevel { get; set; }
        public string EMPLEVELU { get; set; }
        public string CODEMPIDU { get; set; }
        public string NAMEMPEU { get; set; }
        public string DIVISIONU { get; set; }
        public string SECTIONU { get; set; }
        public string DEPARTMENTU { get; set; }
        public string DEPARTMENT2U { get; set; }
        public string DEPARTMENT3U { get; set; }
        public string POSITIONU { get; set; }
        public string EMAIL1U { get; set; }
        public string EMAIL2U { get; set; }
        public string checkmin { get; set; }
        public Boolean SkipFlag { get; set; }
        public string AssignFlag { get; set; }
        public string AssignFlagRemark { get; set; }
        public string ApproverOperatorID { get; set; }
        public string ApproverOperatorName { get; set; }
        public string ApproverOperatorSign { get; set; }
        public string RequirementRemark { get; set; }
    }

    

    public class DocumentEdit
    {
        public string ApprovalDate { get; set; }
        public string ApproverOperatorID { get; set; }
        public string ApproverOperatorName { get; set; }
        public string DocumentCode { get; set; }
        public string DocumentNo { get; set; }
        public string DocumentNameE { get; set; }
        public string DocumentNameT { get; set; }
        public string DocumentNameJ { get; set; }
        public DateTime IssuedDate { get; set; }
        public string IssuedDateChange { get; set; }
        public string DocumentStatus { get; set; }
        public string ReqDescription1 { get; set; }
        public string ReqDescription2 { get; set; }
        public string ReqDescription3 { get; set; }
        public string ReqOperatorID { get; set; }
        public string ReqOperatorName { get; set; }
        public string RoleID { get; set; }
        public string SeqNo { get; set; }
        public string ApprovalItemNameE { get; set; }
        public string ApprovalItemNameT { get; set; }
        public string ApprovalItemNameJ { get; set; }
        public string ApprovalItemCode { get; set; }
        public string UpdDate { get; set; }
        public string CountIssuedDate { get; set; }
    }

    public class GroupIssueDocument
    {
        public string DocumentNo { get; set; }
        public string DocumentCode { get; set; }
        public string DocumentNameE { get; set; }
        public string DocumentNameT { get; set; }
        public string DocumentNameJ { get; set; }
        public string DocumentGroupCode { get; set; }
        public string DocumentKind { get; set; }
        public string OpeGroupCateg { get; set; }
        public string EmailSend { get; set; }
        public string AttachedFile { get; set; }
        public string AttachedFile1 { get; set; }
        public string AttachedFile2 { get; set; }
        public string StandardNo { get; set; }
        public string ReviseNo { get; set; }
        public string Remark { get; set; }
        public string Language { get; set; }
        public DateTime IssuedDate { get; set; }
        public string IssuedDateChange { get; set; }
        public string ReqDescription1 { get; set; }
        public string ReqDescription2 { get; set; }
        public string ReqDescription3 { get; set; }
        public string ReqOperatorId { get; set; }
        public string ReqOperatorName { get; set; }
        public string DocumentStatus { get; set; }
        public string AddDate { get; set; }
        public string UpdDate { get; set; }
        public string UserName { get; set; }
        public string ComputerName { get; set; }        
        public string FlagUpdDate { get; set; }
        public string ApprovalDate { get; set; }
        public string ApproverOperatorName { get; set; }
        public string CountIssuedDate { get; set; }
        public string YearMonth { get; set; }
    }

    public class MyDocument
    {
        public string DocumentNo { get; set; }
        public string DocumentCode { get; set; }
        public string DocumentNameE { get; set; }
        public string DocumentNameT { get; set; }
        public string DocumentNameJ { get; set; }
        public string ReqDescription1 { get; set; }
        public string ReqOperatorName { get; set; }
        public DateTime IssuedDate { get; set; }
        public string IssuedDateChange { get; set; }
        public string DocumentStatus { get; set; }
        public string NextApprove { get; set; }
        public string CountIssuedDate { get; set; }
        public string YearMonth { get; set; }
    }

    public class MyApproved
    {
        public string DocumentNo { get; set; }
        public string DocumentCode { get; set; }
        public string DocumentNameE { get; set; }
        public string ReqDescription1 { get; set; }
        public string ReqOperatorName { get; set; }
        public DateTime IssuedDate { get; set; }
        public DateTime ApprovalDate { get; set; }
        public string IssuedDateChange { get; set; }
        public string ApprovalDateChange { get; set; }
        public string DocumentStatus { get; set; }
        public string YearMonth { get; set; }
        public string FlagUpdDate { get; set; }
    }



    public class WaitApproveModel
    {
        public string DocumentCode { get; set; }
        public string DocumentNo { get; set; }
        public string DocumentNameE { get; set; }
        public string DocumentNameT { get; set; }
        public string DocumentNameJ { get; set; }
        public DateTime IssuedDate { get; set; }
        public string IssuedDateChange { get; set; }
        public string DocumentStatus { get; set; }
        public string ReqDescription1 { get; set; }
        public string ReqDescription2 { get; set; }
        public string ReqDescription3 { get; set; }
        public string ReqOperatorID { get; set; }
        public string ReqOperatorName { get; set; }
        public string RoleID { get; set; }
        public string EMPLEVEL { get; set; }
        public string CODEMPID { get; set; }
        public string NAMEMPE { get; set; }
        public string DIVISION { get; set; }
        public string DEPARTMENT { get; set; }
        public string DEPARTMENT2 { get; set; }
        public string EMAIL1 { get; set; }
        public string EMAIL2 { get; set; }
        public string SeqNo { get; set; }
        public string ValLevel { get; set; }
        public string SECTION { get; set; }
        public string approvepast { get; set; }
        public string CountIssuedDate { get; set; }
        public string Countapprovepast { get; set; }
    }

    public class CheckBoxFlow
    {
        public string FlowID { get; set; }
        public int SeqNo { get; set; }
        public string RoleID { get; set; }
        public string ApprovalItemCode { get; set; }
        public string ApprovalItemNameE { get; set; }
        public string ApprovalItemNameT { get; set; }
        public string ApprovalItemNameJ { get; set; }
        public string Requirement { get; set; }
        public bool SkipFlag { get; set; }
        public string ApprovalDate { get; set; }
        public string RequirementRemark { get; set; }         
    }

    public class UserFlow
    {
        public string DocumentNo { get; set; }
        public int Id { get; set; }
        public string checkselect { get; set; }
        public string CODEMPID { get; set; }
        public string NAMEMPT { get; set; }
        public string NAMEMPE { get; set; }
        public string DIVISION { get; set; }
        public string SECTION { get; set; }
        public string DEPARTMENT { get; set; }
        public string DEPARTMENT2 { get; set; }
        public string DEPARTMENT3 { get; set; }
        public string ORGANIZATIONNAME { get; set; }
        public string POSITION { get; set; }
        public string EMPLEVEL { get; set; }
        public string EMAIL1 { get; set; }
        public string EMAIL2 { get; set; }
        public string FlowID { get; set; }
        public string SeqNo { get; set; }
        public string RoleID { get; set; }
        public string ApprovalItemCode { get; set; }
        public string Requirement { get; set; }
        public string RequirementRemark { get; set; }
        public string AssignFlag { get; set; }
        public string AssignFlagRemark { get; set; }
        public string ApprovalItemNameE { get; set; }
        public string ApprovalItemNameT { get; set; }
        public string ApprovalItemNameJ { get; set; }
        public string Text { get; set; }
        public string Value { get; set; }
    }

    public class UserFlowByRole
    {
       
        public string FlowID { get; set; }
        public int SeqNo { get; set; }
        public string RoleID { get; set; }
        public string ApprovalItemNameE { get; set; }
        public string ApprovalItemNameT { get; set; }
        public string ApprovalItemNameJ { get; set; }
        public string[] SelectedValues { get; set; }
    }
}


