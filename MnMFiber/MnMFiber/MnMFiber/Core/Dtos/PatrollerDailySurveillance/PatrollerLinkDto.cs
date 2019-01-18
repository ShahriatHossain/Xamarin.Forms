using MnMFiber.Core.Models.PatrollerDailySurveillance;
using System.Collections.Generic;

namespace MnMFiber.Core.Dtos.PatrollerDailySurveillance
{
    public class PatrollerLinkDto
    {
        public long TicketId { get; set; }
        public string LinkName { get; set; }
        public string SpanFrom { get; set; }
        public string SpanTo { get; set; }

        public List<TicketSpotLatitudeLongitudeModel> SpotList { get; set; }
    }
}
