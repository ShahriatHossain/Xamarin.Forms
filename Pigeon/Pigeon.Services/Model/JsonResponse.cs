using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pigeon.Services.Model
{
    public class JsonResponse
    {
        public JsonResponse(string json)
        {
            JObject jObject = JObject.Parse(json);
            JToken jSuccess = jObject["y_json"];
            JToken jOutput = jObject["o_json"];

            jsonSuccess = jSuccess.ToObject<JsonSuccess>();
            jsonOutput = jOutput.ToObject<JsonOutput>();

        }
        public JsonSuccess jsonSuccess { get; set; }
        public JsonOutput jsonOutput { get; set; }
    }
}
