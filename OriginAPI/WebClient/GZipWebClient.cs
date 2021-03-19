using System;
using System.Net;

namespace WebClient
{
    public class GZipWebClient : System.Net.WebClient
    {
        public GZipWebClient() {
            CookieContainer = new CookieContainer();
            this.ResponseCookies = new CookieCollection();
        }

        public CookieContainer CookieContainer { get; set; }
        public CookieCollection ResponseCookies { get; set; }
        public string Accept { get; set; }
        public string AuthToken { get; set; }
        public string Authorization { get; set; }

        protected override WebRequest GetWebRequest(Uri address) {
            var request = (HttpWebRequest)base.GetWebRequest(address);
            request.CookieContainer = CookieContainer;
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            request.AllowAutoRedirect = false;
            if (!string.IsNullOrEmpty(Accept)) {
                request.Accept = Accept;
            }
            if (!string.IsNullOrEmpty(Authorization)) {
                request.Headers.Add("Authorization", "Bearer " + Authorization);
            }
            if (!string.IsNullOrEmpty(AuthToken)) {
                request.Headers.Add("authtoken", AuthToken);
            }
            return request;
        }

        protected override WebResponse GetWebResponse(WebRequest request) {
            HttpWebResponse response = null;
            try {
                response = (HttpWebResponse)base.GetWebResponse(request);
            }
            catch (WebException e) {
                if (e.Message.Contains("302"))
                    response = (HttpWebResponse)e.Response;
            }
            try {
                this.ResponseCookies = response.Cookies;
                return response;
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
                return response;
            }
        }
    }
}
