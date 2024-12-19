using Azure.Core;
using Entity.Layer.Entity;
using Shred.Layer.AuthModel;

namespace Business.Layer.Abstract
{
    public interface IAuthService
    {
        Task<(int, string)> Registeration(RegistrationModel model, string role);
        Task<(int, Token)> Login(LoginModel model);
        Task<Token> UpdateToken(RefreshTokenModel refreshToken);
        Task<(AppUser,int StatusCode)> DecodeToken(string email);

    }
}
