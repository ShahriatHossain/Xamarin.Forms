using System;
using System.Collections.Generic;

namespace MnMLilon.Service.Model
{
    public class TroubleTicketModel
    {
        public long Id { get; set; }
        public string TicketNo { get; set; }
        public long SiteId { get; set; }
        public int ComplaintTypeId { get; set; }
        public DateTime ComplaintDateTime { get; set; }
        public int? AssignToId { get; set; }
        public string ProblemReportedByCustomer { get; set; }
        public int? CategoryId { get; set; }
        public int? SubCategoryId { get; set; }
        public bool? Submitted { get; set; }
        public bool? Closed { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CreatedById { get; set; }
        public int? UpdatedById { get; set; }
        public DateTime? UpdatedAt { get; set; }

    }

    public class TroubleTicketViewModel : TroubleTicketModel
    {
        public string AssignToName { get; set; }
        public string AssignToContact { get; set; }
        public TroubleTicketDetailsModel TroubleTicketDetails { get; set; }
        public List<TroubleTicketMaterialModel> TroubleTicketMaterials { get; set; }
        public BatteryCommissionSiteModel SiteDetails { get; set; }

        public string StatusName { get; set; }

        public string ComplaintType { get; set; }

        public string Category { get; set; }
        public string SubCategory { get; set; }

        public bool CanEdit { get; set; }

        public DateTime? ComplainClosedAt { get; set; }

        public string AssignedPersonSignPath { get; set; }
        public string CustomerRepresentativeSignPath { get; set; }

        public List<TroubleTicketFaultyAsset> TroubleTicketFaultyAssets { get; set; }
    }
}
