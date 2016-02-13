using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CloudDocPicker.Models.RestApi
{
    public class ExactJObject
    {
        [JsonObject(MemberSerialization.OptIn)]
        public class Root
        {
            [JsonProperty(PropertyName = "d")]
            public Result D { get; internal set; }
        }

        [JsonObject(MemberSerialization.OptIn)]
        public class Result
        {
            [JsonProperty(PropertyName = "results")]
            public IEnumerable<Document> Results { get; internal set; }
        }

        [JsonObject(MemberSerialization.OptIn)]
        public class Metadata
        {
            [JsonProperty(PropertyName = "uri")]
            public string Uri { get; internal set; }

            [JsonProperty(PropertyName = "type")]
            public string Type { get; internal set; }
        }

        [JsonObject(MemberSerialization.OptIn)]
        public class Document
        {
            [JsonProperty(PropertyName = "__metadata")]
            public Metadata Metadata { get; internal set; }

            [JsonProperty(PropertyName = "ID")]
            public string ID { get; internal set; }

            [JsonProperty(PropertyName = "Subject")]
            public string Subject { get; internal set; }

            [JsonProperty(PropertyName = "DocumentDate")]
            public string DocumentDate { get; internal set; }

            [JsonProperty(PropertyName = "Type")]
            public string Type { get; internal set; }

            [JsonProperty(PropertyName = "AccountName")]
            public string Account { get; internal set; }

            [JsonProperty(PropertyName = "CategoryDescription")]
            public string CategoryDescription { get; internal set; }
        }
    }
}
