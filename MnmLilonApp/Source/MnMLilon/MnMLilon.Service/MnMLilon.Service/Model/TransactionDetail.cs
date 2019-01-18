using System;
using System.Collections.Generic;
using MnMLilon.Service.DataAccess.Model;
using Newtonsoft.Json;

namespace MnMLilon.Service.Model
{
    public class TransactionDetail
    {
        public string SiteMake { get; set; }
        public string SiteModel { get; set; }
        public decimal? SiteCapacity { get; set; }
        public string PowerPlantDetails { get; set; }
        public string PowerPlantSoftwareVersion { get; set; }
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

        public string TransactionDocument { get; set; }
    }
}
