using MnMLilon.Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MnMLilon.Service.DataAccess.Model
{
    public class TransactionDetailTable : BaseItem
    {
        public int TransactionId { get; set; }
        public string SiteMake { get; set; }
        public string SiteModel { get; set; }
        public decimal? SiteCapacity { get; set; }
        public string PowerPlantDetails { get; set; }
        public bool DGDetailsAvailable { get; set; }
        public bool EBDetailsAvailable { get; set; }
        public int? SiteConfigId { get; set; }
        public bool SiteReady { get; set; }
        public string SiteReadyRemarks { get; set; }
        public bool MaterialShortage { get; set; }
        public string MaterialShortageRemarks { get; set; }
        public bool MaterialDamaged { get; set; }
        public string MaterialDamagedRemarks { get; set; }
        public decimal? SmpsChargingVoltage { get; set; }
        public bool BatteryModuleOffMode { get; set; }
        public bool BusBarMounting { get; set; }
        public string BusBarMountingComment { get; set; }
        public bool BatteryMounting { get; set; }
        public string BatteryMountingComment { get; set; }
        public bool CommunicationTestDone { get; set; }
        public string CommunicationTestComment { get; set; }
        public bool FunctionalTestDone { get; set; }
        public string FunctionalTestComment { get; set; }
        public bool SnapShotReport { get; set; }
        public string SnapShotReportComment { get; set; }
        public string Photo1 { get; set; }
        public string Photo2 { get; set; }
        public string BatteryModelNo { get; set; }
        public string BatterySoftwareVersion { get; set; }
    }
}
