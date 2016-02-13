using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CloudDocPicker.Models.RestApi
{
    public class DropboxJObject
    {
        [JsonObject(MemberSerialization.OptIn)]
        public class File
        {
            [JsonProperty(PropertyName = "size")]
            public string Size { get; internal set; }

            [JsonProperty(PropertyName = "hash")]
            public string Hash { get; internal set; }

            [JsonProperty(PropertyName = "bytes")]
            public int Bytes { get; internal set; }

            [JsonProperty(PropertyName = "thumb_exists")]
            public bool ThumbExists { get; internal set; }

            [JsonProperty(PropertyName = "rev")]
            public string Revision { get; internal set; }

            [JsonProperty(PropertyName = "modified")]
            public DateTime Modified { get; internal set; }

            [JsonProperty(PropertyName = "path")]
            public string Path { get; internal set; }

            [JsonProperty(PropertyName = "is_dir")]
            public bool IsDirectory { get; internal set; }

            [JsonProperty(PropertyName = "icon")]
            public string Icon { get; internal set; }

            [JsonProperty(PropertyName = "root")]
            public string Root { get; internal set; }

            [JsonProperty(PropertyName = "contents")]
            public IEnumerable<File> Contents { get; internal set; }
        }
    }
}
