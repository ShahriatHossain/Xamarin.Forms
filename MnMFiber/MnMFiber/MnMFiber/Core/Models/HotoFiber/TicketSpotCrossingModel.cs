namespace MnMFiber.Core.Models.HotoFiber
{
    public class TicketSpotCrossingModel
    {
        public string SpotId { get; set; }
        public bool? BridgeCrossing { get; set; }
        public decimal? BridgeCrossingInMeters { get; set; }
        public bool? BridgeProtectionAtBothEnds { get; set; }
        public bool? RailwayCrossing { get; set; }
        public decimal? RailwayCrossingInMeters { get; set; }
        public bool? RailwayProtectionAtBothEnds { get; set; }
        public bool? CulvertCrossing { get; set; }
        public decimal? CulvertCrossingInMeters { get; set; }
        public bool? CulvertProtectionAtBothEnds { get; set; }
        public bool? RoadCrossing { get; set; }
        public decimal? RoadCrossingInMeters { get; set; }
        public bool? RoadProtectionAtBothEnds { get; set; }
        public bool? OtherCrossing { get; set; }
        public bool? ProtectionOnCrossing { get; set; }
    }
}
