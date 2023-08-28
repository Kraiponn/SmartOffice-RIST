using System;
using System.Collections.Generic;
using System.Text;

namespace SmartOffice.eReservation.ModelsDocControl
{
    public partial class ReservationMasterAsset
    {
        public string AssetId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string LocationId { get; set; }
        public string Image { get; set; }
        public bool? Disable { get; set; }
        public string ResponsibleBy { get; set; }
        public string ResponsibleName { get; set; }
        public string ResponsibleTelNo { get; set; }
        public string ResponsibleEmail { get; set; }           
        public DateTime AddDate { get; set; }
        public DateTime UpdDate { get; set; }
        public string UserName { get; set; }
        public string ComputerName { get; set; }
    }
}
