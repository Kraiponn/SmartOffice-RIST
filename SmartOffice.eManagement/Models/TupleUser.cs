using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartOffice.eManagement.Models
{
    public class TupleUser
    {


        public List<UserOperationGroup> userOperationGroups { get; set; }        
        public List<UserOperationItemList>  userOperationItems { get; set; }
        public List<UserMenulist> userMenulists { get; set; }

        public SelectList ddlItemCateg { get; set; }
        public SelectList ddlItemCode { get; set; }

    }

    public class UserOperationGroup
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
        public bool OperationSub { get; set; }
    }

    

    public partial class UserOperationItemList
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

    public class UserMenulist
    {
        public string No { get; set; }
        public string MenuNameE { get; set; }
        public string MenuNameT { get; set; }
        public string MenuNameJ { get; set; }
        public string UserId { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
        public string Param { get; set; }
        public string MenuUrl { get; set; }
        public string Image { get; set; }
        public string IconClass { get; set; }
        public string Badges { get; set; }
        public string BadgesName { get; set; }
        public string Download { get; set; }
        public string UserName { get; set; }
    }

    public class SelectListItemCateg
    {
        public string ID { get; set; }
        public string Name { get; set; }
    }

    public class SelectListItemCode
    {
        public string ID { get; set; }
        public string Name { get; set; }
    }


    public partial class GetMessage
    {
        public string ItemCateg { get; set; }
        public string ItemCategName { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public int DisplayOrder { get; set; }
        public string Message { get; set; }
        public DateTime StartMessage { get; set; }
        public DateTime EndMessage { get; set; }
        public DateTime AddDate { get; set; }
        public DateTime UpdDate { get; set; }
        public string UserName { get; set; }
        public string ComputerName { get; set; }
    }


}
