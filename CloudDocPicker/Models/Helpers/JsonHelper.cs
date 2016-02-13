using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CloudDocPicker.Models.Helpers
{
    public static class JsonHelper
    {
        public static T ParseJson<T>(string json) where T : class, new()
        {
            var jobject = JObject.Parse(json);
            return JsonConvert.DeserializeObject<T>(jobject.ToString());
        }

        public static JObject JObjectParse(string json)
        {
            return JObject.Parse(json);
        }
    }
}
