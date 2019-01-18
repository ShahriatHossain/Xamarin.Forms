using System;
using System.Collections.Generic;

namespace MnMLilon.Service.Model
{
    public class Transaction
    {
        public long Id { get; set; }
        public string FormatNo { get; set; }
        public string RevisionNo { get; set; }
        public DateTime? RevisionDate { get; set; }
        public string CustomerContactPersonName { get; set; }
        public string CustomerContactPersonMobile { get; set; }
        public string CustomerContactPersonSign { get; set; }
        public string CustomerContactPersonDesignation { get; set; }
        public string TechnicianSign { get; set; }
        public string UpdatedByNumber { get; set; }
        public decimal? Longitude { get; set; }
        public decimal? Latitude { get; set; }

        public bool IsReAssignToAnother { get; set; }

        public bool TechnicianSubmitted { get; set; }

        public TransactionDetail TransactionDetailsModel { get; set; }
        public TransactionElectricalInstallation ElectricalInstallationModel { get; set; }
        public List<TransactionBatteryDetail> TransactionBatteryList { get; set; }
    }
}