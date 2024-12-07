using DataAccess.Layer.Abstract;
using Entity.Layer;
using Entity.Layer.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Layer.Concret
{
    public class EFRepositoryProject : IProject
    {
        private readonly ApplicationContext _context;
        private readonly ILogger<EFRepositoryProject> _logger;  
        public EFRepositoryProject(ApplicationContext _context,
             ILogger<EFRepositoryProject> logger)
        {
            this._context = _context;
            _logger = logger;
        }

        public async Task<bool> Add(Project t)
        {
            try
            {
               bool checkTeam = _context.Teams.Any(x => x.TeamId == t.TeamId && x.IsActive);
                if (checkTeam)
                {
                    await _context.AddAsync(t);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
            
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var data = await GetById(id);
                data.IsActive = false;
                if(data.Reports != null)
                {
                    foreach(var item in data.Reports)
                    {
                        item.IsActive = false;
                    }
                }
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        public async Task<List<Project>> GetAll()
        {
            var data = await _context.Projects.Where(p => p.IsActive).ToListAsync();
            return data;
        }

        public async Task<Project> GetById(int id)
        {
            try
            {
                var data = _context.Projects.FirstOrDefault(x => x.ProjectId == id && x.IsActive);
                return data != null ? data : new Project { };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new Project { };
            }
        }

        public async Task<bool> Update(Project t)
        {
            try
            {
                var checkTeam = _context.Teams.Any(x => x.TeamId == t.TeamId && x.IsActive);
                if (checkTeam)
                {
                    var data = _context.Projects.Update(t);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }
    }
}
