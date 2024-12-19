using Azure.Core;
using Entity.Layer.Entity;
using Shred.Layer.AuthModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Business.Layer.Abstract
{
    public interface ITokenHandler
    {
        Task<Token> GenerateToken (AppUser user);
        string GenerateRefreshToken();
        Task UpdateRefreshToken(Token token,AppUser user,int addOnAccessTokenDate);
        Task<(AppUser,int StatusCode)> DecodeToken(string email);
    }
}
