namespace MnMFiber.Core.Models.HotoFiber
{
    public class TicketSpotModel
    {
        public string Id { get; set; }
        public long TicketId { get; set; }
        public int? SpotTypeId { get; set; }
        public string TowerID { get; set; }
        public string TowerName { get; set; }
        public string TowerAddress { get; set; }
        public bool? CoverOnSpots { get; set; }
        public int? NoOfEnclosures { get; set; }
        public int? NoOfVisibleMhWithEarthing { get; set; }
        public int? NoOfNonVisibleMhWithEarthing { get; set; }
        public bool? RouteMarkersVisibility { get; set; }
        public decimal? RouteMarkersHeight { get; set; }
        public string OtherDescription { get; set; }
        public decimal? ArmoredCableAsAerial { get; set; }
        public decimal? StructuredAerial { get; set; }
        public decimal? TwoPointsDistance { get; set; }
        public int? NoOfRouteMarkers { get; set; }
        public bool? FmsLabellingStatus { get; set; }
        public decimal? TemporaryAerial { get; set; }
        public decimal? ChamberVisibility { get; set; }
        public decimal? ChamberNonVisibility { get; set; }
        public decimal? RouteBuildByHdd { get; set; }
        public decimal? DamagedOsp { get; set; }
        public int? TrayJointsRouteWiseCount { get; set; }
        public int? BuiredJcChamberCount { get; set; }
        public int SerialNo { get; set; }

        public decimal? Longitude { get; set; }
        public decimal? Latitude { get; set; }
        public string SpotImageFileName { get; set; }
        public string SpotImageFileDisplayName { get; set; }
        public string ChamberCoverCondition { get; set; }
        public string ChamberCoverImageFileName { get; set; }
        public string ChamberCoverImageFileDisplayName { get; set; }
        public int? FiberPlacedId { get; set; }
        public string FiberPlacedName { get; set; }
        public string FiberOnRouteLandmark { get; set; }
        public string FiberOnRouteComments { get; set; }


        public string SpotTypeName { get; set; }
        public string FiberType { get; set; }
        public string FiberConstructionType { get; set; }
        public string UpdatedAt { get; set; }
        public TicketSpotCableModel TicketSpotCableModel { get; set; }
        public TicketSpotCrossingModel TicketSpotCrossingModel { get; set; }
    }
}
