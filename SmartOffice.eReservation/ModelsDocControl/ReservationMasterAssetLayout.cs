using System;
using System.Collections.Generic;
using System.Text;

namespace SmartOffice.eReservation.ModelsDocControl
{
    public partial class ReservationMasterAssetLayout
    {
        public string LayoutId { get; set; }
        public string AssetId { get; set; }
        public int DisplayOrder { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public bool? Disable { get; set; }
        public DateTime AddDate { get; set; }
        public DateTime UpdDate { get; set; }
        public string UserName { get; set; }
        public string ComputerName { get; set; }
    }
}
