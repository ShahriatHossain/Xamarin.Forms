using Newtonsoft.Json;

namespace Pigeon.Services.Model
{
    public class NoticeDetail
    {
        //[JsonProperty(PropertyName = "notice")]
        //public string Notice { get; set; }
        //[JsonProperty(PropertyName = "not_voted")]
        //public int NotVoted { get; set; }
        [JsonProperty(PropertyName = "upVoteCount")]
        public int Yes { get; set; }
        [JsonProperty(PropertyName = "downVoteCount")]
        public int No { get; set; }
    }
}
