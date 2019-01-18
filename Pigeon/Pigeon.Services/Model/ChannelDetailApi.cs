using Newtonsoft.Json;

namespace Pigeon.Services.Model
{
    public class ChannelDetailApi
    {
        [JsonProperty(PropertyName = "org_doc_type")]
        public int OrgDocType { get; set; }
        [JsonProperty(PropertyName = "org_doc")]
        public string OrgDoc { get; set; }
        [JsonProperty(PropertyName = "org_file_type")]
        public string OrgFileType { get; set; }
        [JsonProperty(PropertyName = "org_file_display_name")]
        public string OrgFileDisplayName { get; set; }
    }
}
