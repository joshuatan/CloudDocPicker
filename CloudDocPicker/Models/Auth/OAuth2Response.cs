using Newtonsoft.Json;

namespace CloudDocPicker.Models.Auth
{
    [JsonObject(MemberSerialization.OptIn)]
    public class OAuth2Response
    {
        [JsonProperty(PropertyName = "access_token")]
        public string AccessToken { get; internal set; }

        [JsonProperty(PropertyName = "uid")]
        public string Uid { get; internal set; }

        [JsonProperty(PropertyName = "token_type")]
        public string TokenType { get; internal set; }
    }
}
