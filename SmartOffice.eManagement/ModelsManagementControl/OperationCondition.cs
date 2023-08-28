﻿using System;
using System.Collections.Generic;

namespace SmartOffice.eManagement.ModelsManagementControl
{
    public partial class OperationCondition
    {
        public int Id { get; set; }
        public string OperationCode { get; set; }
        public string Template { get; set; }
        public string Design { get; set; }
        public string Condition { get; set; }
        public string Value { get; set; }
    }
}
