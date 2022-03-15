using System.Security.Claims;
using System.Threading.Tasks;

namespace Clean.TokenAuthentication
{
    public interface ITokenManager
    {
        Task<bool> Authenticate(string email);
        string NewToken();
        ClaimsPrincipal VerifyToken(string token);
        string getEmailFromToken(string token);
    }
}