using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebClient;

namespace OriginAPI.Create
{
    public class Account
    {
        const string API = "https://api1.origin.com/";
        private GZipWebClient client;

        public Account() {
            client = new GZipWebClient();
        }

        public void Create(string email, string password) {
            try {
                // Initializing
                client.DownloadString("https://accounts.ea.com/connect/auth?response_type=code&redirect_uri=https%3A%2F%2Fwww.ea.com%2Flogin_check&client_id=EADOTCOM-WEB-SERVER&display=web%2Fcreate");
                client.DownloadString(client.ResponseHeaders["Location"]);
                var loginRequestUrl = $"https://signin.ea.com{client.ResponseHeaders["Location"]}";
                client.DownloadString(loginRequestUrl); // Downloads the login page

                // Login
                var reqparm = new System.Collections.Specialized.NameValueCollection();
                reqparm.Add("email", email);
                reqparm.Add("password", password);
                reqparm.Add("confirmPassword", password);
                reqparm.Add("selectSouthKoreaIdentity1", "IPIN");
                reqparm.Add("selectSouthKoreaIdentity2", "IPIN");
                reqparm.Add("originId", "Seeder2019_300");
                reqparm.Add("securityQuestion", "What elementary school did you attend?");
                reqparm.Add("securityAnswer", "None");
                reqparm.Add("country", "FI");
                reqparm.Add("dobMonth", "1");
                reqparm.Add("dobDay", "1");
                reqparm.Add("dobYear", "1987");
                reqparm.Add("friendVisibility", "on");
                reqparm.Add("_achievementsVisibility", "on");
                reqparm.Add("achievementsVisibility", "on");
                reqparm.Add("_emailVisibility", "on");
                reqparm.Add("_xboxVisibility", "on");
                reqparm.Add("_psnVisibility", "on");
                reqparm.Add("_contactMe", "on");
                reqparm.Add("_readAccept", "on");
                reqparm.Add("readAccept", "on");
                reqparm.Add("_rememberMe", "on");
                reqparm.Add("rememberMe", "on");
                reqparm.Add("_eventId", "submit");
                reqparm.Add("thirdPartyCaptchaResponse", "");
                byte[] responsebytes = client.UploadValues(loginRequestUrl, "POST", reqparm);
                string responsebody = Encoding.UTF8.GetString(responsebytes);

                var confirgCreate = $"https://signin.ea.com{client.ResponseHeaders["Location"]}";
                client.DownloadString(confirgCreate); // Downloads the login page

                var arr = responsebody.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None).ToList();
                var nextLocation = arr.FirstOrDefault(x => x.Contains("window.location"));
                var startIndex = nextLocation.IndexOf("\"");
                loginRequestUrl = nextLocation.Substring(startIndex + 1, nextLocation.Length - (startIndex + 2));

                // Finish login
                client.DownloadString(client.ResponseHeaders["Location"]);
            }
            catch (Exception e) {
                Debug.WriteLine($"Error: {e.Message}");
                throw new ApplicationException("Incorrect email or password!");
            }
        }
    }
}
