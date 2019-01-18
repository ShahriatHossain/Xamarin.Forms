using MnMFiber.Core.Models.HotoFiber;

namespace MnMFiber.Core.Dtos.HotoFiber
{
    public class TicketSpotDto : TicketSpotModel
    {
        public string CustomLatitude
        {
            get { return string.Format("Lat :{0}", this.Latitude.ToString()); }
        }

        public string CustomLongitude
        {
            get { return string.Format("Long :{0}", this.Longitude.ToString()); }
        }
    }
}
