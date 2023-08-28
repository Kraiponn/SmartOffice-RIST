using System;
using System.Collections.Generic;
using System.Text;

namespace SmartOffice.eManagement.Models
{
    public class TupleForm
    {


        public List<FormOperationGroup>  formOperationGroups { get; set; }
        public List<FormValueList>  formValueLists { get; set; }
        public List<FormOperationItemList>   formOperationItemLists { get; set; }
        public List<FormSelectListItems> formGroupdropdown { set; get; }
        public List<FormCheckBoxListItem> formGroupcheckbox { get; set; }
        public List<FormRadioListItem> formGroupspecial { get; set; }
        public List<FormRadioListItem> formGroupradio { get; set; }
        public List<FormOperationItems>  formOperationItems { get; set; }
        public List<Listdatastable> tabledata { get; set; }
        public List<vewHRMSEmployeeSurveyPIC> vewHRMSEmployeeSurveyPICs { get; set; }
        public List<vewNumberofinfectedPersonsSum> vewNumberofinfectedPersonsSums { get; set; }
        public List<vewOperationItemList_Result_CheckOperationNo> vewOperationItemList_Result_CheckOperationNos { get; set; }
    }

    public class FormOperationGroup
    {
        public string strObj { get; set; }
        public string OperationCode { get; set; }
        public string OperationName { get; set; }
        public string DisplayOrder { get; set; }
        public string InputKind { get; set; }
        public string OpeGroupCode { get; set; }
        public string OpeGroupName { get; set; }
        public string OpeGroupCateg { get; set; }
        public string DisplayPriority { get; set; }
        public string SpecialOperate { get; set; }
        public string SpecialGroup { get; set; }
    }

    public partial class FormValueList
    {
        public string ValueCode { get; set; }
        public string Value { get; set; }
        public string ValueText { get; set; }
        public int? DisplayOrder { get; set; }
    }

    public partial class FormOperationItemList
    {
        public string OperationNo { get; set; }
        public string OperationCode { get; set; }
        public string OperationName { get; set; }
        public string OperationDisplayOrder { get; set; }
        public string InputKind { get; set; }
        public string RoleID { get; set; }
        public string OpeGroupCode { get; set; }
        public string OpeGroupName { get; set; }
        public string OpeGroupCateg { get; set; }
        public string DisplayPriority { get; set; }
        public string SpecialOperate { get; set; }
        public string CategInputOption { get; set; }
        public string CategDisplayOrder { get; set; }
        public string ItemCateg { get; set; }
        public string ItemCategName { get; set; }
        public string Remarks1 { get; set; }
        public string Remarks2 { get; set; }
        public string Remarks3 { get; set; }
        public string Remarks4 { get; set; }
        public string Remarks5 { get; set; }
        public string RemarksTitle1 { get; set; }
        public string RemarksTitle2 { get; set; }
        public string RemarksTitle3 { get; set; }
        public string RemarksTitle4 { get; set; }
        public string RemarksTitle5 { get; set; }
        public string RemarksColor1 { get; set; }
        public string RemarksColor2 { get; set; }
        public string RemarksColor3 { get; set; }
        public string RemarksColor4 { get; set; }
        public string RemarksColor5 { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string InputType { get; set; }
        public string DataType { get; set; }
        public string DecimalNo { get; set; }
        public string Required { get; set; }
        public string Minlength { get; set; }
        public string Maxlength { get; set; }
        public string Min { get; set; }
        public string Max { get; set; }
        public string Step { get; set; }
        public string Unit { get; set; }
        public string ValueCode { get; set; }
        public string valueold { get; set; }
        public string InputOptionItem { get; set; }
        public string DefaultValue { get; set; }
        public string ReadOnly { get; set; }
        public string DetailOption { get; set; }
        public string ItemListDisplayOrder { get; set; }
        public string FinalResult { get; set; }
    
    }

    public class FormSelectListItems
    {
        public string Value { get; set; }
        public string Text { get; set; }
        public string Code { get; set; }
        public bool IsChecked { get; set; }
        public int? Order { get; set; }

        public string OperationCode { get; set; }
        public string ItemCateg { get; set; }
        public string OperationName { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }

    }

    public class FormCheckBoxListItem
    {
        public string ID { get; set; }
        public string Display { get; set; }
        public bool IsChecked { get; set; }
        public string OperationCode { get; set; }
        public string ValueCode { get; set; }
        public string ItemCateg { get; set; }
        public string OperationName { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
    }

    public class FormRadioListItem
    {
        public int? Order { get; set; }
        public string ID { get; set; }
        public string Display { get; set; }
        public bool IsChecked { get; set; }
        public string OperationCode { get; set; }
        public string ValueCode { get; set; }
        public string ItemCateg { get; set; }
        public string OperationName { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }


    }

    public class Formvaluelistinput
    {
        public string Text { get; set; }
    }

    public partial class FormOperationItems
    {
        public string OperationNo { get; set; }
        public string OperationCode { get; set; }
        public string OperationName { get; set; }
        public string OpeGroupCode { get; set; }
        public string OpeGroupName { get; set; }
        public string InputKind { get; set; }       
        public string OpeGroupCateg { get; set; }       
        public DateTime UpdDate { get; set; }
        public string UserName { get; set; }
        public string FinalResult { get; set; }
    }

    public class Listdatastable
    {
        public string Header { get; set; }
        public string FinalResult { get; set; }
        public string Itemcode { get; set; }
        public string Value { get; set; }
        public int DisplayOrder { get; set; }
        public string TableCode { get; set; }
        public int ItemId { get; set; }
        public string InputType { get; set; }
        public string DataType { get; set; }

    }

    public class vewHRMSEmployeeSurveyPIC
    {
        public string CODEMPID { get; set; }
        public string NAMEMPT { get; set; }
        public string NAMEMPE { get; set; }
        public string DIVISION { get; set; }
        public string SECTION { get; set; }
        public string DEPARTMENT { get; set; }
        public string DEPARTMENT2 { get; set; }
        public string DEPARTMENT3 { get; set; }
        public string INACTIVE { get; set; }
        public string SurveyPICCode { get; set; }
        public string PICCode { get; set; }
        public string PICCodeName { get; set; }
        public string Position { get; set; }
    }

    public class vewNumberofinfectedPersonsSum
    {
        public string OperationCode { get; set; }
        public string SurveyPICCode { get; set; }
        public string Dept { get; set; }
        public string Keydata { get; set; }
        public string Result { get; set; }
        
    }

    public class vewOperationItemList_Result_CheckOperationNo
    {
        public string OperationNo { get; set; }
        public string FinalResult { get; set; }

    }

}
