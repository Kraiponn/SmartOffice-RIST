﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartOffice.Models
{
   
    public class ModelSubSystems
    {
        public List<SubSystems> subSystems { get; set; }
    }
    public class SubSystems
    {
        public string MenuNameE { get; set; }
        public string MenuNameT { get; set; }
        public string MenuNameJ { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
        public string Param { get; set; }
        public string MenuUrl { get; set; }
        public string IconClass { get; set; }
        public string Badges { get; set; }
        public string BadgesName { get; set; }
        public bool Download { get; set; }
        public int DisplayOrder { get; set; }
        public string GroupCateg { get; set; }
        public string GroupName { get; set; }
        public string Image { get; set; }
        public int MenuIdentity { get; set; }
        public int MenuIdentityParent { get; set; }
        public string GroupSub { get; set; }

    }
}
