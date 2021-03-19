using System.Net;

namespace Shared.Models {
    public class AuthCode {
        public string Email { get; set; }
        public Cookie Sid { get; set; }
    }
}