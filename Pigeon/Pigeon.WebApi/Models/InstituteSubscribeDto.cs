using Pigeon.DataAccess.Entities;

namespace Pigeon.WebApi.Models
{
    public class InstituteSubscribeDto : BaseDto<InstituteSubscribe>
    {
        public override void Map(InstituteSubscribe model)
        {
            Id = model.Id;
        }
    }
}
