using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SmartOffice.ModelsHRMSLocal
{
    public partial class HrmsEmployee
    {
        [Key]
        public string Codempid { get; set; }
        public string Namempt { get; set; }
        public string Namempe { get; set; }
        public string Company { get; set; }
        public string Division { get; set; }
        public string Section { get; set; }
        public string Department { get; set; }
        public string Department2 { get; set; }
        public string Department3 { get; set; }
        public string Organizationname { get; set; }
        public string Position { get; set; }
        public double? Emplevel { get; set; }
        public DateTime? Dtebirth { get; set; }
        public string Age { get; set; }
        public string Passport { get; set; }
        public string Nationality { get; set; }
        public string Email1 { get; set; }
        public string Email2 { get; set; }
        public DateTime? WorkingDate { get; set; }
        public DateTime? Inactive { get; set; }
        public string Codcalen { get; set; }
    }
}
