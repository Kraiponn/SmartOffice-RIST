using System;
using System.Collections.Generic;

namespace SmartOffice.SurveyApp.ModelsManagementControl
{
    public partial class ItemCategory
    {
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
        public DateTime AddDate { get; set; }
        public DateTime UpdDate { get; set; }
        public string UserName { get; set; }
        public string ComputerName { get; set; }
        public virtual ICollection<InputItem> InputItems { get; set; }
    }
}
