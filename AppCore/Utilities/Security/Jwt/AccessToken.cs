using System;

namespace AppCore.Utilities.Security.Jwt
{
    public class AccessToken
    {
        public string Token { get; set; }

        public DateTime Expiration { get; set; }

        public string RefreshToken { get; set; }

        public DateTime? RefreshTokenEndDate { get; set; }
    }
}
