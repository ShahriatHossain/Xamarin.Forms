namespace MnMFiber.Core.Models.HotoFiber
{
    public class TicketSpotCableModel
    {
        public string SpotId { get; set; }
        public int? NoOfFiberCables { get; set; }
        public string FiberType { get; set; }
        public int? FiberConstructionTypeId { get; set; }
        public decimal? LoopLength { get; set; }
        public int? NoOfExtraFibers { get; set; }

        public int? NoOfDucts { get; set; }
        public string DuctColor { get; set; }
        public int? NoOfSpareDucts { get; set; }
        public string SpareDuctColor { get; set; }

        public decimal? CableInLowDepthLength { get; set; }
        public decimal? CableInPrivatePropertyLength { get; set; }
        public decimal? CableInForestAreaLength { get; set; }
        public decimal? CableInUnderHighwayExpansionLength { get; set; }
        public decimal? CableInInsideHighwayExpansionLength { get; set; }

        public string CableLandmark { get; set; }
        public string CableRemarks { get; set; }

    }
}
