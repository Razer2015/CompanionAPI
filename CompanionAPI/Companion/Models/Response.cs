using Newtonsoft.Json;

namespace CompanionAPI.Models
{
    interface IResponse<T>
    {
        [JsonProperty("jsonrpc")]
        string Version { get; }
        [JsonProperty("id")]
        string Id { get; }
        [JsonProperty("result")]
        T Result { get; set; }
    }

    public class Response<T> : IResponse<T>
    {
        public string Version { get; }
        public string Id { get; }
        public T Result { get; set; }
    }
}
