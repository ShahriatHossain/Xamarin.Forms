using System;

namespace MnMFiber.Core.Models.HotoFiber
{
    public class TicketModel
    {
        public long Id { get; set; }
        public string TicketNo { get; set; }
        public int CustomerId { get; set; }
        public int CircleId { get; set; }
        public int ClusterId { get; set; }
        public int PatrollerId { get; set; }
        public string LinkName { get; set; }
        public string SpanFrom { get; set; }
        public string SpanTo { get; set; }
        public decimal? PhysicalScope { get; set; }
        public decimal? OpticalLength { get; set; }
        public decimal? AsBuildDiagrams { get; set; }
        public bool? DfmReportAvailable { get; set; }
        public bool? AsBuildDiagramsAvailable { get; set; }
        public bool? RowPermissionsSubmitted { get; set; }
        public bool? IruRouteDetails { get; set; }

        public int CreatedById { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? UpdatedById { get; set; }
        public bool? Submitted { get; set; }
        public bool? Closed { get; set; }
        public bool? ReAssign { get; set; }
        public int? ReAssignToId { get; set; }
    }
}
