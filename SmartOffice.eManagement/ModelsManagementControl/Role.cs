﻿using System;
using System.Collections.Generic;

namespace SmartOffice.eManagement.ModelsManagementControl
{
    public partial class Role
    {
        public Role()
        {
            OperatorRole = new HashSet<OperatorRole>();
        }

        public string RoleId { get; set; }
        public string ApplicationName { get; set; }
        public string Value { get; set; }
        public string Remarks { get; set; }
        public DateTime AddDate { get; set; }
        public DateTime UpdDate { get; set; }
        public string UserName { get; set; }
        public string ComputerName { get; set; }

        public virtual ICollection<OperatorRole> OperatorRole { get; set; }
    }
}
