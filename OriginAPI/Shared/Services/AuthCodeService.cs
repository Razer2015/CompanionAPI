using Newtonsoft.Json;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace Shared.Services {
    public interface IAuthCodeService {
        void AddOrRefresh(string email, Cookie cookie);
        void Remove(string email);
        bool TryGetSid(string email, out Cookie sid);
    }

    public class AuthCodeService : IAuthCodeService {
        private List<AuthCode> _authCodes;
        private readonly string _filePath;

        public AuthCodeService(string filePath) {
            _filePath = filePath;
            Read();
        }

        public void AddOrRefresh(string email, Cookie cookie) {
            if (_authCodes == null)
                _authCodes = new List<AuthCode>();

            var authCode = _authCodes.FirstOrDefault(x => x.Email.Equals(email));

            if (authCode != null) {
                authCode.Sid = cookie;
            }
            else {
                _authCodes.Add(new AuthCode {
                    Email = email,
                    Sid = cookie
                });
            }

            Save();
        }

        public void Remove(string email) {
            if (_authCodes == null)
                _authCodes = new List<AuthCode>();

            var authCode = _authCodes.RemoveAll(x => x.Email.Equals(email));

            Save();
        }

        public bool TryGetSid(string email, out Cookie sid) {
            var authCode = _authCodes?.FirstOrDefault(x => x.Email.Equals(email));
            sid = authCode?.Sid;

            return authCode != null;
        }

        private void Read() {
            if (!File.Exists(_filePath)) return;

            try {
                _authCodes = JsonConvert.DeserializeObject<List<AuthCode>>(File.ReadAllText(_filePath));
            }
            catch (Exception e) {
                Console.WriteLine(e);
            }
        }

        private void Save() {
            try {
                lock (_authCodes) {
                    File.WriteAllText(_filePath, JsonConvert.SerializeObject(_authCodes));
                }
            }
            catch (Exception e) {
                Console.WriteLine(e);
            }
        }
    }
}
