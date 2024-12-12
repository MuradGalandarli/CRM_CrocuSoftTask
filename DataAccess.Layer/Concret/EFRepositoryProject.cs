using DataAccess.Layer.Abstract;
using Entity.Layer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

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

        public async Task<(bool, int StatusCode)> Add(Project t)
        {
            try
            {
                  bool checkTeam = _context.Teams.Any(x => x.TeamId == t.TeamId && x.IsActive);
                if (checkTeam)
                {
               
                await _context.AddAsync(t);
                    await _context.SaveChangesAsync();
                    return (true,201);
                }
                return (false,404);
            }
           
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return (false,500);
            } 
        }

        public async Task<(bool, int StatusCode)> Delete(int id)
        {
            try
            {
                var data = await GetById(id);
                if (data.StatusCode == 200)
                {
                    data.Item1.IsActive = false;
                    var report = _context.Reaports.Where(x => x.IsActive && x.ProjectId == data.Item1.ProjectId).ToList();

                    if (report != null)
                    {
                        foreach (var item in report)
                        {
                            item.IsActive = false;
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
                return (false,500);
            }
        }
       
        public async Task<(List<Project>, int StatusCode)> GetAll()
        {
            try
            {
                var data = await _context.Projects.Where(p => p.IsActive).ToListAsync();
               
                return data.Count > 0 ? (data,200) : (new List<Project>(),404);
            }
            catch(Exception ex)
            {
                _logger.LogError($"{ex.Message}");
                return (new List<Project>(), 500);
            }

        }

        public async Task<(Project, int StatusCode)> GetById(int id)
        {
            try
            {
                var data = _context.Projects.FirstOrDefault(x => x.ProjectId == id && x.IsActive);
                return data != null ? (data,200) : (new Project { },404);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return (new Project { }, 500);
            }
        }

        public async Task<(bool, int StatusCode)> Update(Project t)
        {
            try
            {
                var checkTeam = _context.Teams.Any(x => x.TeamId == t.TeamId && x.IsActive);
                if (checkTeam)
                {
                    var data = _context.Projects.Update(t);
                    await _context.SaveChangesAsync();
                    return (true,200);
                }
                return (false,404);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return (false, 500);
            }
        }

    }
}
