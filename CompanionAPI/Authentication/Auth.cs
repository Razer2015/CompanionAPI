using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using WebClient;

namespace Authentication
{
    public class Auth
    {
        private GZipWebClient client;

        public string Code { get; set; }

        public Auth() {
            client = new GZipWebClient();
        }

        public void Login(string email, string password, AccessType accessType = AccessType.Origin) {
            try {
                // Initializing
                client.DownloadString("https://accounts.ea.com/connect/auth?response_type=code&client_id=ORIGIN_SPA_ID&display=originXWeb/login&locale=en_US&redirect_uri=https://www.origin.com/views/login.html");
                client.DownloadString(client.ResponseHeaders["Location"]);
                var loginRequestUrl = $"https://signin.ea.com{client.ResponseHeaders["Location"]}";
                client.DownloadString(loginRequestUrl); // Downloads the login page

                // Login
                var reqparm = new System.Collections.Specialized.NameValueCollection();
                reqparm.Add("email", email);
                reqparm.Add("password", password);
                reqparm.Add("_eventId", "submit");
                reqparm.Add("showAgeUp", "true");
                reqparm.Add("googleCaptchaResponse", "");
                reqparm.Add("_rememberMe", "on");
                reqparm.Add("rememberMe", "on");
                byte[] responsebytes = client.UploadValues(loginRequestUrl, "POST", reqparm);
                string responsebody = Encoding.UTF8.GetString(responsebytes);

                var arr = responsebody.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None).ToList();
                var nextLocation = arr.FirstOrDefault(x => x.Contains("window.location"));
                var startIndex = nextLocation.IndexOf("\"");
                loginRequestUrl = nextLocation.Substring(startIndex + 1, nextLocation.Length - (startIndex + 2));

                // TODO: Check if these are even correct
                var endOrChallenge = client.DownloadString(loginRequestUrl);
                if (endOrChallenge.Contains("&_eventId=challenge")) {
                    LoginVerification(loginRequestUrl);
                }
                else if (endOrChallenge.Contains("&_eventId=end")) {
                    client.DownloadString(loginRequestUrl + "&_eventId=end");
                }

                // Finish login
                client.DownloadString(client.ResponseHeaders["Location"]);
                // Origin
                if (accessType == AccessType.Origin) {
                    var resultJson = client.DownloadString("https://accounts.ea.com/connect/auth?client_id=ORIGIN_JS_SDK&response_type=token&redirect_uri=nucleus:rest&prompt=none");
                    var result = JsonConvert.DeserializeObject<AccessTokenModelOrigin>(resultJson);
                    Code = result.AccessToken;
                }
                else if (accessType == AccessType.Companion) {
                    // Companion
                    var resultJson = client.DownloadString("https://accounts.ea.com/connect/auth?client_id=sparta-companion-web&response_type=code&prompt=none&redirect_uri=nucleus:rest");
                    var result = JsonConvert.DeserializeObject<AccessTokenModelCompanion>(resultJson);
                    Code = result.AccessToken;
                }
            }
            catch (Exception e) {
                Debug.WriteLine($"Error: {e.Message}");
                throw new ApplicationException("Incorrect email or password!");
            }
        }

        private void LoginVerification(string loginRequestUrl) {
            client.DownloadString(loginRequestUrl + "&_eventId=challenge");
            loginRequestUrl = $"https://signin.ea.com{client.ResponseHeaders["Location"]}";
            client.DownloadString(loginRequestUrl); // HTML Page for login verification type input

            string type = Prompt.ShowTypeDialog();

            var reqparm = new System.Collections.Specialized.NameValueCollection();
            reqparm.Add("_eventId", "submit");
            reqparm.Add("codeType", type); // EMAIL or APP
            client.UploadValues(loginRequestUrl, "POST", reqparm);

            loginRequestUrl = $"https://signin.ea.com{client.ResponseHeaders["Location"]}";
            client.DownloadString(loginRequestUrl); // HTML Page for login verification code input

            string promptValue = Prompt.ShowDialog("Verification code", "Login Verification");

            reqparm = new System.Collections.Specialized.NameValueCollection();
            reqparm.Add("_eventId", "submit");
            reqparm.Add("_trustThisDevice", "on");
            reqparm.Add("oneTimeCode", promptValue); // The code
            reqparm.Add("trustThisDevice", "on");
            client.UploadValues(loginRequestUrl, "POST", reqparm);
        }
    }
}
