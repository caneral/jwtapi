using AppCore.Entity.Identity;
using AppCore.Extensions;
using AppCore.Utilities.Security.Encryption;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;

namespace AppCore.Utilities.Security.Jwt
{
    public class TokenHelper : ITokenHelper
    {
        private readonly TokenOptions _tokenOptions;

        private DateTime _accessTokenExpiration;
        private DateTime _refreshTokenExpiration;

        public TokenHelper(IOptions<TokenOptions> tokenOptions)
        {
            this._tokenOptions = tokenOptions.Value;
        }

        public AccessToken CreateToken(AppUser user, List<AppClaim> appClaims, int personId)
        {
            _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
            _refreshTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.RefreshTokenExpiration);

            var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
            var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
            var jwt = CreateJwtSecurityToken(_tokenOptions, user, signingCredentials, appClaims, personId);
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwt);

            return new AccessToken
            {
                Token = token,
                Expiration = _accessTokenExpiration,
                RefreshToken = CreateRefreshToken(),
                RefreshTokenEndDate = _refreshTokenExpiration
            };


        }

        public JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, AppUser user,
            SigningCredentials signingCredentials, List<AppClaim> appClaims, int personId)
        {
            var jwt = new JwtSecurityToken(
                tokenOptions.Issuer,
                tokenOptions.Audience,
                expires: _accessTokenExpiration,
                notBefore: DateTime.Now,
                claims: SetClaims(user, appClaims, personId),
                signingCredentials: signingCredentials
            );
            return jwt;
        }

        private IEnumerable<Claim> SetClaims(AppUser user, List<AppClaim> appClaims, int personId)
        {
            var claims = new List<Claim>();
            claims.AddNameIdentifier(personId.ToString());
            claims.AddEmail(user.Email);
            claims.AddName($"{user.FirstName} {user.LastName}");
            claims.AddRoles(appClaims.Select(c => c.Name).ToArray());
            return claims;
        }

        public string CreateRefreshToken()
        {
            byte[] number = new byte[32];
            using (RandomNumberGenerator random = RandomNumberGenerator.Create())
            {
                random.GetBytes(number);
                return Convert.ToBase64String(number);
            }
        }
    }
}