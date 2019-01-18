using System;
using System.Collections.Generic;

namespace MnMLilon.Service.Model
{
    public class BatteryCommissionSiteModel
    {
        public long Id { get; set; }
        public string SiteID { get; set; }
        public string SiteName { get; set; }
        public string SiteAddress { get; set; }
        public int? SiteConfigId { get; set; }
        public string SiteConfigName { get; set; }
        public int? CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int? CircleId { get; set; }
        public string CircleName { get; set; }
        public int? ClusterId { get; set; }
        public string ClusterName { get; set; }

        public string BatterySoftwareVersion { get; set; }
        public string PowerPlantSoftwareVersion { get; set; }
        public decimal? Longitude { get; set; }
        public decimal? Latitude { get; set; }

        public string SiteMake { get; set; }
        public string SiteModel { get; set; }
        public decimal? SiteCapacity { get; set; }


        public List<BatteryCommissionSiteBattery> BatteryList { get; set; }
    }

    public class BatteryCommissionSiteBattery
    {
        public long Id { get; set; }
        public long SiteId { get; set; }
        public int ModuleNo { get; set; }
        public string BatterySerialNo { get; set; }
        public DateTime? CommissionDate { get; set; }
        public DateTime? WarrantyExpired { get; set; }
        public bool Damaged { get; set; }
        public string DamagedBatterySerialNo { get; set; }
    }

    public class PendingTroubleTicketModel
    {
        public long Id { get; set; }
        public string TicketNo { get; set; }
        public string SiteID { get; set; }
        public string SiteName { get; set; }
        public string SiteAddress { get; set; }
        public long SiteId { get; set; }
    }
}
