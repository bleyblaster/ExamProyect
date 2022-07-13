using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Authentication
{
   public class JWTServices: IJWTServices
    {
        private readonly string _key;
        public JWTServices(string key)
        {
            _key = key;
        }

        public string Authenticate(string userName)
        {
            
            var tokenKey = new SymmetricSecurityKey( Encoding.ASCII.GetBytes(_key));
            var SigningCredentials = new SigningCredentials(tokenKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.UniqueName, userName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Aud, "https://localhost:44306"),
            new Claim(JwtRegisteredClaimNames.Iss, "https://localhost:44306" ),
            
        };

            var token = new JwtSecurityToken(
                audience: "https://localhost:44306",
                issuer: "https://localhost:44306",
                claims: claims,
                expires: new DateTimeOffset(DateTime.UtcNow.AddMinutes(10)).DateTime
                , notBefore: new DateTimeOffset(DateTime.Now).DateTime
                , signingCredentials: SigningCredentials);



            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
