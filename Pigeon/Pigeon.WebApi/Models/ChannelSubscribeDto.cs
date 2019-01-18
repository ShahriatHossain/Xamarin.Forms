using Pigeon.DataAccess.Entities;

namespace Pigeon.WebApi.Models
{
    public class ChannelSubscribeDto : BaseDto<ChannelSubscribe>
    {
        public override void Map(ChannelSubscribe model)
        {
            Id = model.Id;
        }
    }
}
