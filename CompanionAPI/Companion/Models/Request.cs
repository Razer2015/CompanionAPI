using Newtonsoft.Json;
using static CompanionAPI.Helpers.HelperMethods;

namespace CompanionAPI.Models
{
    interface IRequest<T>
    {
        [JsonProperty("jsonrpc")]
        string Version { get; }
        [JsonProperty("id")]
        string Id { get; }
        [JsonProperty("method")]
        string Method { get; set; }
        [JsonProperty("params")]
        T Params { get; set; }
    }

    public class Request<T> : IRequest<T>
    {
        public string Version { get => Constants.RPCVersion; }
        public string Id { get => CreateUUID(); }
        public string Method { get; set; }
        public T Params { get; set; }

        public Request(string method, T parameters) {
            Method = method;
            Params = parameters;
        }
    }

    public class RequestParams
    {
        [JsonProperty("game")]
        public string Game { get; set; }
        [JsonProperty("gameId")]
        public string GameId { get; set; }
        [JsonProperty("settings")]
        public Settings Settings { get; set; }
        [JsonProperty("personaId")]
        public string PersonaId { get; set; }
        [JsonProperty("platform")]
        public string Platform { get; set; }
        [JsonProperty("code")]
        public string Code { get; set; }
        [JsonProperty("redirectUri")]
        public string RedirectUri { get; set; }
    }

    public class Settings
    {
        [JsonProperty("role")]
        public string Role { get; set; }
    }
}
