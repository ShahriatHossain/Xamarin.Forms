using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pigeon.Services.Model
{
    public class JsonOutput
    {
        [JsonProperty("otp")]
        public int OtpCode { get; set; }

        [JsonProperty("user_id")]
        public int UserId { get; set; }
    }
}