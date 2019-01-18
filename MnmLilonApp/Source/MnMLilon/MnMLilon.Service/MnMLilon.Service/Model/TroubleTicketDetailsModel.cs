using System;

namespace MnMLilon.Service.Model
{
    public class TroubleTicketDetailsModel
    {
        public long TicketId { get; set; }

        public int? NoOfModulesAtSite { get; set; }
        public int? NoOfJunctionBoxAtSite { get; set; }
        public string SiteObservation { get; set; }


        public string ChargingVoltage { get; set; }
        public string MaxChargingModule { get; set; }
        public string LvdSettings { get; set; }
        public decimal? SiteLoadInAmps { get; set; }

        public bool DgAvailable { get; set; }
        public bool EbAvailable { get; set; }
        public string AlarmsNotice { get; set; }
        public string ServiceEngineerRemarks { get; set; }
        public int? StatusOfComplaints { get; set; }
        public string StatusRemarks { get; set; }
        public string ServiceEngineerRemarksWithRca { get; set; }
        public string Recommendation { get; set; }

        public bool VisualInspection { get; set; }
        public string VisualInspectionRemarks { get; set; }
        public bool CommunicationTest { get; set; }
        public string CommunicationRemarks { get; set; }
        public bool FunctionalTest { get; set; }
        public string FunctionalTestRemarks { get; set; }
        public string CustomerRemarks { get; set; }
        public string FormateNo { get; set; }
        public string RevisionNo { get; set; }
        public DateTime? RevisionDate { get; set; }
        public string RevisionDateString { get; set; }
        public string AssignedPersonSign { get; set; }
        public string CustomerRepresentativeSign { get; set; }

        public string CustomerRepresentativeName { get; set; }
        public string CustomerRepresentativeMobileNo { get; set; }
        public string CustomerRepresentativeDesignation { get; set; }

        public int? ComplaintStatusId { get; set; }

        public string SiteComments { get; set; }

        public string TicketDocument { get; set; }
        public string SolutionImparted { get; set; }
        public int? ProblemRelatedId { get; set; }
        public string NameOfCaller { get; set; }
        public string ContactNoOfCaller { get; set; }
    }
}
