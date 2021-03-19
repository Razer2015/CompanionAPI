using System.Collections.Generic;
using System.Net;

namespace OriginAPI.Models {
    public class SavedAuthCodes {
        public List<AuthCode> AuthCodes { get; set; }

        public SavedAuthCodes() {
            AuthCodes = new List<AuthCode>();
        }
    }

    public class AuthCode {
        public string Email { get; set; }
        public Cookie Sid { get; set; }
    }
}
