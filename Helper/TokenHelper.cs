using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Shinetech.Common.Helper
{
    public class TokenHelper
    {
        private readonly string _signingKey = "";
        public TokenHelper(string signingKey)
        {
            _signingKey = signingKey;
        }
        /// <summary>
        /// generate access token
        /// </summary>
        /// <param name="claims"></param>
        /// <returns></returns>
        public string GenerateAccessToken(Claim[] claims, Dictionary<string, Object> keyValuePairs, int expireMinutes = 10)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_signingKey));
            var token = new JwtSecurityToken(
                claims: claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddMinutes(expireMinutes),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );
            foreach (var item in keyValuePairs)
            {
                token.Payload[item.Key] = item.Value;
            }

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        /// <summary>
        /// generate refresh token
        /// </summary>
        /// <returns></returns>
        public string GenerateRefreshToken(string token)
        {
            var hash = CommonCryptoHelper.HashPassword(token.Substring(7, 15) + token.Substring(1, 9));
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(hash));
        }
        public bool ValidateRefreshToken(string refreshToken, string token)
        {
            var tokenHash = Encoding.UTF8.GetString(Convert.FromBase64String(refreshToken));
            return CommonCryptoHelper.VerifyPassword(tokenHash, token.Substring(7, 15) + token.Substring(1, 9));
        }

        /// <summary>
        ///get claims form token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public ClaimsPrincipal GetPrincipalFromAccessToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();

            try
            {
                return handler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_signingKey)),
                    ValidateLifetime = false
                }, out SecurityToken validatedToken);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
