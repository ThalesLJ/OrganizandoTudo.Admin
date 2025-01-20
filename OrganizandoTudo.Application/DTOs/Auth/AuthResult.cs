using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizandoTudo.Application.DTOs.Auth
{
    public class AuthResult
    {
        public bool Success { get; private set; }
        public string Token { get; private set; }
        public string RefreshToken { get; private set; }
        public DateTime ExpiresAt { get; private set; }
        public IEnumerable<string> Errors { get; private set; }

        private AuthResult()
        {
            Success = false;
            Errors = new List<string>();
        }

        public static AuthResult Failed(IEnumerable<string> errors)
        {
            return new AuthResult
            {
                Success = false,
                Errors = errors
            };
        }

        public static AuthResult Successful(string token, string refreshToken, DateTime expiresAt)
        {
            return new AuthResult
            {
                Success = true,
                Token = token,
                RefreshToken = refreshToken,
                ExpiresAt = expiresAt,
                Errors = new List<string>()
            };
        }
    }
}
