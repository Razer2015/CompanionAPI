using CompanionAPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace CompanionAPI
{
    public class CompanionClient
    {
        private string _session;
        private string _clientVersion = "companion-init";

        public bool Login(string code, out ResponseStatus status) {
            var method = "Companion.loginFromAuthCode";
            if (PostRequest<LoginResponseModel>(method, GenerateRequestData(method, new LoginRequestModel(code)), out var result)) {
                status = result.ResponseStatus;
                _session = result.Result.Id;
                _clientVersion = "companion-9014390";
                return true;
            }
            else {
                status = result.ResponseStatus;
                return false;
            }
        }

        /// <summary>
        /// Get detailed stats of a game for person
        /// </summary>
        /// <param name="game">BF4 = bf4, BF1 = tunguska, BFV = casablanca</param>
        /// <param name="personaId"></param>
        public bool GetDetailedStats(string game, string personaId, out DetailedStatsResponseModel response, out ResponseStatus status) {
            var method = "Stats.detailedStatsByPersonaId";
            if (PostRequest<DetailedStatsResponseModel>(method, GenerateRequestData(method, new DetailedStatsRequestModel(game, personaId)), out var result)) {
                status = result.ResponseStatus;
                response = result.Result;
                return true;
            }
            else {
                status = result.ResponseStatus;
                response = null;
                return false;
            }
        }

        private string GenerateRequestData<T>(string method, T data) {
            return JsonConvert.SerializeObject(new Request<T>(method, data));
        }

        private bool PostRequest<T>(string method, string data, out Response<T> result) {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create($"{Constants.CompanionAPI}{method}");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            httpWebRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            httpWebRequest.AutomaticDecompression = DecompressionMethods.GZip;

            //Get the headers associated with the request.
            WebHeaderCollection myWebHeaderCollection = httpWebRequest.Headers;
            myWebHeaderCollection.Add("X-ClientVersion: " + _clientVersion);
            if (!string.IsNullOrEmpty(_session)) {
                myWebHeaderCollection.Add("X-GatewaySession: " + _session);
            }

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream())) {
                streamWriter.Write(data);
                streamWriter.Flush();
                streamWriter.Close();
            }

            try {
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream())) {
                    var stream = streamReader.ReadToEnd();
                    result = JsonConvert.DeserializeObject<Response<T>>(stream);
                    return true;
                }
            }
            catch (WebException ex) {
                var response = (HttpWebResponse)ex.Response;
                using (var streamReader = new StreamReader(response.GetResponseStream())) {
                    var stream = streamReader.ReadToEnd();
                    result = JsonConvert.DeserializeObject<Response<T>>(stream);
                }

                return false;
            }
        }
    }
}
