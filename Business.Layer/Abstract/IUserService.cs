using DataTransferObject.RequestDto;
using Entity.Layer.Entity;
using Shred.Layer.AuthModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Layer.Abstract
{
    public interface IUserService
    {
        public Task<(AppUser, int StatusCode)> GetById(string userId);
        public Task<(List<AppUser>, int StatusCode)> GetAll();
        public Task<(bool, int StatusCode)> Update(RequestUserUpdate t);
        public Task<(bool, int StatusCode)> Delete(string userId);
    }
}
