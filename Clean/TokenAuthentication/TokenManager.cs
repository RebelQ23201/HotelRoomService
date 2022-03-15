using CleanService.DBContext;
using CleanService.IService;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Clean.TokenAuthentication
{
    public class TokenManager : ITokenManager
    {
        private JwtSecurityTokenHandler tokenHandler;
        private byte[] sercetKey;
        private readonly IAccountService<Account> service;

        public TokenManager(IAccountService<Account> service)
        {
            this.service = service;
            tokenHandler = new JwtSecurityTokenHandler();
            sercetKey = Encoding.ASCII.GetBytes("1055354988133-skltrg53r2e8htgiuavug3tebs0ibgfh.apps.googleusercontent.com");
        }
        public async Task<bool> Authenticate(string email)
        {
            Account account = await service.GetEmail(email);
            if (!string.IsNullOrWhiteSpace(email) && account != null)
                return true;
            else return false;
        }

        public string NewToken()
        {
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.Name, "email") }),
                Expires = DateTime.UtcNow.AddMinutes(1),
                /*SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(sercetKey),
                    SecurityAlgorithms.HmacSha256Signature)*/
            };

            tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtString = tokenHandler.WriteToken(token);
            return jwtString;
        }

        public ClaimsPrincipal VerifyToken(string token)
        {
            var claims = tokenHandler.ValidateToken(token,
                new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = false,
                    //IssuerSigningKey = new SymmetricSecurityKey(sercetKey),

                    SignatureValidator = delegate (string token, TokenValidationParameters parameters)
                    {
                        var jwt = new JwtSecurityToken(token);

                        return jwt;
                    },

                    ValidateLifetime = true,
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken); 
            return claims;
        }

        public string getEmailFromToken(string token)
        {
            //get JWT token
            var handler = new JwtSecurityTokenHandler();
            JwtSecurityToken decodedValue = handler.ReadJwtToken(token);

            //get payload, data from the token and turn into json
            JwtPayload payload = decodedValue.Payload;
            var json = payload.SerializeToJson();

            //get each data from the json and get user email
            Dictionary<string?, object> sData = JsonSerializer.Deserialize<Dictionary<string?, object>>(json);
            string email = sData["email"].ToString();

            return email;
        }
    }
}
