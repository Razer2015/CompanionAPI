using Newtonsoft.Json;

namespace CompanionAPI.Models
{
    public class LoginViewModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    }
}
