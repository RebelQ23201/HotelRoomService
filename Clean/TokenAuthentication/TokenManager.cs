using CleanService.DBContext;
using CleanService.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clean.TokenAuthentication
{
    public class TokenManager : ITokenManager
    {
        private readonly IAccountService<Account> service;
        private List<Token> listTokens;

        public TokenManager(IAccountService<Account> service)
        {
            this.service = service;
            listTokens = new List<Token>();
        }
        public async Task<bool> Authenticate(string email)
        {
            Account account = await service.GetEmail(email);
            if (!string.IsNullOrWhiteSpace(email) && account != null)
                return true;
            else return false;
        }

        public Token NewToken()
        {
            var token = new Token
            {
                Value = Guid.NewGuid().ToString(),
                ExpiryDate = DateTime.Now.AddHours(2)
            };
            listTokens.Add(token);
            return token;
        }

        public bool VerifyToken(string token)
        {
            if (listTokens.Any(x => x.Value == token && x.ExpiryDate > DateTime.Now))
            { return true; }
            return false;
        }
    }
}
