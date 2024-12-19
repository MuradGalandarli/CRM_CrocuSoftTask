
using Business.Layer.Abstract;
using DataTransferObject.RequestDto;
using Entity.Layer.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Runtime.InteropServices;

namespace Business.Layer.Concret
{
    public class UserManager : IUserService
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<UserManager> _logger;
        public UserManager(UserManager<AppUser> userManager,
            ILogger<UserManager> _logger)
        {
            _userManager = userManager;
            this._logger = _logger;
        }
     
        public async Task<(bool, int StatusCode)> Delete(string userId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user != null && user.Role != "Admin")
                {
                    await _userManager.DeleteAsync(user);
                    return (true, 200);
                }
                else
                {
                    return (false, 404);
                }
                  
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return (false, 500);
            }


                
        }

        public async Task<(List<AppUser>, int StatusCode)> GetAll()
        {
            try
            {
                var data = await _userManager.Users.ToListAsync();
                return data.Count > 0 ? (data, 200) : (new List<AppUser> { }, 404);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return (new List<AppUser> { }, 500);
            }
         
        }

        public async Task<(AppUser, int StatusCode)> GetById(string userId)
        {
            try
            {
                var data = await _userManager.FindByIdAsync(userId);
                return data != null ? (data, 200) : (new AppUser { }, 404);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}");
                return (new AppUser { }, 500);  
            }
        }

        public async Task<(bool, int StatusCode)> Update(RequestUserUpdate t)
        {
            try
            {
                
                var user = await _userManager.FindByIdAsync(t.UserId);
                if (user != null)
                {
                    user.Name = t.Name;
                    user.SurName  = t.SurName;
                    user.TeamId = t.TeamId;

                    var result = await _userManager.UpdateAsync(user);
                    return (true, 200);
                }
                else
                {
                    return (false, 404);
                }
               
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return (false, 500);
            }
            
        }
    }
}
