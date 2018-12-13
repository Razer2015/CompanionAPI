using Newtonsoft.Json;

namespace Authentication
{
    public class AccessTokenModelOrigin
    {
        [JsonProperty(PropertyName = "access_token")]
        public string AccessToken { get; set; }
        [JsonProperty(PropertyName = "token_type")]
        public string TokenType { get; set; }
        [JsonProperty(PropertyName = "expires_in")]
        public int ExpiresIn { get; set; }
    }

    public class AccessTokenModelCompanion
    {
        [JsonProperty(PropertyName = "code")]
        public string AccessToken { get; set; }
    }
}
