using System.ComponentModel.DataAnnotations.Schema;

namespace Pigeon.DataAccess.Entities
{
    [Table("PricingType")]
    public abstract class PricingType : BaseModel
    {
        public string Type { get; set; }
        public abstract bool CanCreateChannel(int channelCount);
        public abstract bool CanCreateNotice(int noticeCount);
    }

    public class FreePricingType : PricingType
    {
        public override bool CanCreateChannel(int channelCount) => channelCount <= 15;
        public override bool CanCreateNotice(int noticeCount) => noticeCount <= 20;
    }
    public class StandardPricingType : PricingType
    {
        public override bool CanCreateChannel(int channelCount) => channelCount <= 30;
        public override bool CanCreateNotice(int noticeCount) => noticeCount <= 50;
    }
    public class PlusPricingType : PricingType
    {
        public override bool CanCreateChannel(int channelCount) => true;
        public override bool CanCreateNotice(int noticeCount) => true;
    }
}
