using System;
using System.Collections.Generic;

namespace SmartOffice.ModelsDocControl
{
    public partial class ReservationAssets
    {
        public string ReservationId { get; set; }
        public string AssetsId { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }
        public string UnitPrice { get; set; }
        public string Image { get; set; }
        public string Department { get; set; }
        public string Keeper { get; set; }
        public string StorageLocation { get; set; }
        public DateTime? ImportDate { get; set; }
        public string Supplier { get; set; }
        public string ProductCode { get; set; }
        public DateTime? ProductionDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public string Requester { get; set; }
        public string RequesterName { get; set; }
        public string RequesterRemark { get; set; }
        public int Amount { get; set; }
        public string Unit { get; set; }
        public string Status { get; set; }
        public string SelectStatus { get; set; }
        public DateTime? PlanReturn { get; set; }
        public DateTime? ReturnDate { get; set; }
        public string Returner { get; set; }
        public string ReturnerName { get; set; }
        public string ReturnerRemark { get; set; }
    }
}
