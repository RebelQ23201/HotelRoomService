using System.Threading.Tasks;

namespace Clean.TokenAuthentication
{
    public interface ITokenManager
    {
        Task<bool> Authenticate(string email);
        Token NewToken();
        bool VerifyToken(string token);
    }
}