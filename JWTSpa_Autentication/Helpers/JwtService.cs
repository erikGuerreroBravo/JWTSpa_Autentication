using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWTSpa_Autentication.Helpers
{
    public class JwtService
    {
        private string secureKey = "algo segura";

        public string Generate(int id)
        {
            var bytes = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secureKey));
            var credentials = new SigningCredentials(bytes, algorithm: SecurityAlgorithms.HmacSha256Signature);
            var header = new JwtHeader(credentials);
            var payLoad = new JwtPayload(issuer:id.ToString(), audience:null, claims:null, notBefore:null,expires:DateTime.Today.AddDays(1));
            var securityToken = new JwtSecurityToken(header,payLoad);
            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }
        public JwtSecurityToken Verify(string jwt)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secureKey);
            tokenHandler.ValidateToken(jwt,new TokenValidationParameters { 
               IssuerSigningKey = new SymmetricSecurityKey(key),
               ValidateIssuerSigningKey = true,
               ValidateIssuer = false,
               ValidateAudience = false
            }, out SecurityToken validatedToken);
            return (JwtSecurityToken)validatedToken;
        }
    }
    
    
    
}
