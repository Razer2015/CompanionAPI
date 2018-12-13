using Newtonsoft.Json;

namespace CompanionAPI.Models
{
    public class LoginRequestModel
    {
        [JsonProperty("code")]
        public string Code { get; set; }
        [JsonProperty("redirectUri")]
        public string RedirectUri { get; set; }

        public LoginRequestModel(string code, string redirectUri = "nucleus:rest") {
            Code = code;
            RedirectUri = redirectUri;
        }
    }

    public class LoginResponseModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    }
}
