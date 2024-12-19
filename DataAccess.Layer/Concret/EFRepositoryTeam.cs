using DataAccess.Layer.Abstract;
using Entity.Layer;
using Entity.Layer.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Layer.Concret
{
    public class EFRepositoryTeam : ITeam
    {
        private readonly ApplicationContext _context;
        private readonly ILogger<EFRepositoryTeam> _logger;
        private readonly UserManager<AppUser> _userManager;
        public EFRepositoryTeam(ApplicationContext _context,
             ILogger<EFRepositoryTeam> logger,
             UserManager<AppUser> _userManager)
        {
            this._context = _context;
            _logger = logger;
            this._userManager = _userManager;
        }

        public async Task<(bool, int StatusCode)> Add(Team t)
        {
            try
            {
                await _context.AddAsync(t);
                await _context.SaveChangesAsync();

                await _context.SaveChangesAsync();
                return (true, 200);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return (false, 500);
            }
        }

        public async Task<bool> CheckTeam(int teamId)
        {
            bool isSuccess = await _context.Teams.AnyAsync(x => x.IsActive && x.TeamId == teamId);
            return isSuccess;
        }

        public async Task<(bool, int StatusCode)> Delete(int id)
        {
            try
            {
                var data = await GetById(id);
                if (data.Item1 != null)
                {
                    data.Item1.IsActive = false;

                    if (data.Item1.Projects != null)
                    {
                        var projectId = data.Item1.Projects.Where(x => x.IsActive).Select(x => x.ProjectId).ToList();
                        var projectIsActive = await _context.Projects.Where(x => projectId.Contains(x.ProjectId)).ToListAsync();
                        foreach (var item in projectIsActive)
                        {
                            item.IsActive = false;
                        }

                    }
                    if (data.Item1.AppUsers != null)
                    {
                        var projectId = data.Item1.AppUsers.Select(x => x.TeamId).ToList();
                        var user = await _context.AppUser.Where(x => projectId.Contains(x.TeamId)).ToListAsync();
                        
                        foreach (var item in user)
                        {
                          await _userManager.DeleteAsync(item);
                        }

                    }
                    await _context.SaveChangesAsync();
                    return (true, 200);
                }
                return (false, 404);
            }
          
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return (false, 500);
            }
        }

        public async Task<(List<Team>, int StatusCode)> GetAll()
        {
            try
            {
                var fullTeam = await _context.Teams.Where(p => p.IsActive).
                    Include(x => x.AppUsers).Include(x => x.Projects.Where(x => x.IsActive)).ToListAsync();

                if (fullTeam.Count > 0)
                {
                    return (fullTeam, 200);
                }
                return (new List<Team> { }, 404);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return (new List<Team>(), 500);
            }

        }

        public async Task<(Team, int StatusCode)> GetById(int id)
        {
            try
            {
                var data = _context.Teams.
                    Include(x => x.Projects.Where(x => x.IsActive)).Include(x => x.AppUsers)
                    .FirstOrDefault(x => x.TeamId == id && x.IsActive);

                return data != null ? (data, 200) : (new Team { }, 404);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return (new Team { }, 500);
            }
        }

        public async Task<(bool, int StatusCode)> Update(Team t)
        {
            try
            {
                _context.Teams.Update(t);
                await _context.SaveChangesAsync();

                return (true, 200);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return (false, 500);
            }
        }

    }
}
