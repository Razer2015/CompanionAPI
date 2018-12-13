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
}
