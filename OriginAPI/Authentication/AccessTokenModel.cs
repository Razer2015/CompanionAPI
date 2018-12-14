using Newtonsoft.Json;
using System;

namespace Origin.Authentication
{
    public class AccessTokenModelOrigin
    {
        [JsonProperty(PropertyName = "access_token")]
        public string AccessToken { get; set; }
        [JsonProperty(PropertyName = "token_type")]
        public string TokenType { get; set; }
        [JsonProperty(PropertyName = "expires_in")]
        private int Expires_In {
            get { return ExpiresIn; }
            set {
                ExpiresIn = value;
                ExpiredTime = DateTime.Now.AddSeconds(ExpiresIn);
            }
        }

        [JsonIgnore]
        public int ExpiresIn { get; set; }
        [JsonIgnore]
        public DateTime ExpiredTime { get; set; }
        [JsonIgnore]
        public bool Expired { get => DateTime.Now > ExpiredTime; }
    }

    public class AccessTokenModelCompanion
    {
        [JsonProperty(PropertyName = "code")]
        public string AccessToken { get; set; }
    }
}
