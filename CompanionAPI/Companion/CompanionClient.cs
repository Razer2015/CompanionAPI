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
            if (PostRequest<LoginViewModel>(method, new RequestParams { Code = code, RedirectUri = "nucleus:rest" }, out var result)) {
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
        /// Get persona info as career
        /// </summary>
        /// <param name="token"></param>
        /// <param name="personaId"></param>
        /// <returns></returns>
        public StatsViewModel GetPersonaInfo(string token, string personaId) {
            var info = new CareerViewModel();
            // Get career
            for (int i = 0; i < 2; i++) {
                if (GetCareer(personaId, out var career, out var responseStatus)) {
                    info = career;
                    break;
                }
                else {
                    if (responseStatus.Status == Status.InvalidSession) {
                        // Login and retrieve session token
                        if (!Login(token, out responseStatus)) {
                            throw new Exception("Error: Invalid session and couldn't refresh.");
                        }
                    }
                    else {
                        throw new Exception("Error: Unknown error.");
                    }
                }
            }

            return info;
        }

        /// <summary>
        /// Get all persona info
        /// </summary>
        /// <param name="token"></param>
        /// <param name="personaId"></param>
        /// <param name="game"></param>
        /// <param name="platform"></param>
        /// <returns></returns>
        public StatsViewModel GetPersonaInfo(string token, string personaId, string game, string platform) {
            var info = new DetailedStatsViewModel();
            // Get detailed stats
            for (int i = 0; i < 2; i++) {
                if (GetDetailedStats(game, personaId, out var stats, out var responseStatus)) {
                    info = stats;
                    break;
                }
                else {
                    if (responseStatus.Status == Status.InvalidSession) {
                        // Login and retrieve session token
                        if (!Login(token, out responseStatus)) {
                            throw new Exception("Error: Invalid session and couldn't refresh.");
                        }
                    }
                    else {
                        throw new Exception("Error: Unknown error.");
                    }
                }
            }

            // Get emblem
            for (int i = 0; i < 2; i++) {
                if (GetEquippedEmblem(personaId, platform, out var emblemUrl, out var responseStatus)) {
                    info.EmblemUrl = emblemUrl;
                    break;
                }
                else {
                    if (responseStatus.Status == Status.InvalidSession) {
                        // Login and retrieve session token
                        if (!Login(token, out responseStatus)) {
                            throw new Exception("Error: Invalid session and couldn't refresh.");
                        }
                    }
                    else {
                        throw new Exception("Error: Unknown error.");
                    }
                }
            }

            return info;
        }

        /// <summary>
        /// Get detailed stats of a game for person
        /// </summary>
        /// <param name="game">BF4 = bf4, BF1 = tunguska, BFV = casablanca</param>
        /// <param name="personaId"></param>
        public bool GetDetailedStats(string game, string personaId, out DetailedStatsViewModel response, out ResponseStatus status) {
            var method = "Stats.detailedStatsByPersonaId";
            if (PostRequest<DetailedStatsViewModel>(method, new RequestParams { Game = game, PersonaId = personaId }, out var result)) {
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

        /// <summary>
        /// Get the equipped emblem url of the persona
        /// </summary>
        /// <param name="personaId"></param>
        /// <param name="platform"></param>
        /// <param name="emblemUrl"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public bool GetEquippedEmblem(string personaId, string platform, out string emblemUrl, out ResponseStatus status) {
            var method = "Emblems.getEquippedEmblem";
            platform = (string.IsNullOrEmpty(platform) ? "pc" : platform);
            if (PostRequest<string>(method, new RequestParams { PersonaId = personaId, Platform = platform }, out var result)) {
                status = result.ResponseStatus;
                emblemUrl = result.Result;
                return true;
            }
            else {
                status = result.ResponseStatus;
                emblemUrl = null;
                return false;
            }
        }

        public bool GetCareer(string personaId, out CareerViewModel career, out ResponseStatus status) {
            var method = "Stats.getCareerForOwnedGamesByPersonaId";
            if (PostRequest<CareerViewModel>(method, new RequestParams { PersonaId = personaId }, out var result)) {
                status = result.ResponseStatus;
                career = result.Result;
                return true;
            }
            else {
                status = result.ResponseStatus;
                career = null;
                return false;
            }
        }

        private string GenerateRequestData(string method, RequestParams data) {
            return JsonConvert.SerializeObject(new Request<RequestParams>(method, data), 
                Formatting.None,
                new JsonSerializerSettings {
                    NullValueHandling = NullValueHandling.Ignore
                });
        }

        private bool PostRequest<T>(string method, RequestParams @params, out Response<T> result) {
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
                streamWriter.Write(GenerateRequestData(method, @params));
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
