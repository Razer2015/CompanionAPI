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

        public void Login(string code) {
            var method = "Companion.loginFromAuthCode";
            var result = PostRequest<LoginResponseModel>(method, GenerateRequestData(method, new LoginRequestModel(code)));

            _session = result.Id;
            _clientVersion = "companion-9014390";
        }

        /// <summary>
        /// Get detailed stats of a game for person
        /// </summary>
        /// <param name="game">BF4 = bf4, BF1 = tunguska, BFV = casablanca</param>
        /// <param name="personaId"></param>
        public DetailedStatsResponseModel GetDetailedStats(string game, string personaId) {
            var method = "Stats.detailedStatsByPersonaId";
            var result = PostRequest<DetailedStatsResponseModel>(method, GenerateRequestData(method, new DetailedStatsRequestModel(game, personaId)));

            return result;
        }

        private string GenerateRequestData<T>(string method, T data) {
            return JsonConvert.SerializeObject(new Request<T>(method, data));
        }

        private T PostRequest<T>(string method, string data) {
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

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream())) {
                var result = streamReader.ReadToEnd();
                return JsonConvert.DeserializeObject<Response<T>>(result).Result;
            }
        }
    }
}
