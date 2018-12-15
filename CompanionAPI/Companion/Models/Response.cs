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
        [JsonIgnore]
        Error Error { get; set; }
        [JsonIgnore]
        ResponseStatus ResponseStatus { get; set; }
    }

    public class Response<T> : IResponse<T>
    {
        public string Version { get; }
        public string Id { get; }
        public T Result { get; set; }
        public Error Error { get; set; }
        [JsonProperty("error")]
        private Error ErrorP
        {
            get { return Error; }
            set
            {
                Error = value;
                ResponseStatus.Message = value.Message;
                if (Error.Code.Equals(-32501)) {
                    ResponseStatus.Status = Status.InvalidSession;
                }
                else {
                    ResponseStatus.Status = Status.Error;
                }
            }
        }
        public ResponseStatus ResponseStatus { get; set; }

        public Response() {
            ResponseStatus = new ResponseStatus {
                Status = Status.Success
            };
        }
    }

    public class ResponseStatus
    {
        public Status Status { get; set; }
        public string Message { get; set; }
    }

    public class Error
    {
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("code")]
        public int Code { get; set; }
    }
}
