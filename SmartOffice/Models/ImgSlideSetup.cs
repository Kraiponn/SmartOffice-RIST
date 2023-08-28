
using SmartOffice.ModelsEsmartOffice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartOffice.Models
{
    public class ImgSlideSetupmaster
    {
        public List<ImgSlideSetup> imgslide { get; set; }
        public List<ImgHeader> imgHeaders { get; set; }
        public List<ImgTextHeader> imgTextHeaders { get; set; }
       

    }
    public class ImgSlideSetup
    {
        public int PartId { get; set; }
        public string GroupCateg { get; set; }
        public int ImgOrder { get; set; }
        public string ImgPath { get; set; }
        public string ImgActive { get; set; }
        public string ImgType { get; set; }
        public bool? Link { get; set; }
        public string LinkName { get; set; }
        public string LinkPath { get; set; }
        public bool Download { get; set; }
        public bool? Disable { get; set; }

        public string TextD { get; set; }

        public string TextH { get; set; }
        public bool? LinkH { get; set; }
        public string LinkPathH { get; set; }
        public bool DownloadH { get; set; }

        public string PartName { get; set; }
    }
}
