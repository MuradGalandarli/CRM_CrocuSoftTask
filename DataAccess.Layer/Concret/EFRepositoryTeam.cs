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
    public class EFRepositoryTeam : ITeam
    {
        private readonly ApplicationContext _context;
        private readonly ILogger<EFRepositoryTeam> _logger;
        public EFRepositoryTeam(ApplicationContext _context,
             ILogger<EFRepositoryTeam> logger)
        {
            this._context = _context;
            _logger = logger;
        }

        public async Task<bool> Add(Team t)
        {
            try
            {
                await _context.AddAsync(t);
                await _context.SaveChangesAsync();

                if (t.TeamMembers.Count > 0 && t.TeamMembers != null)
                {
                    var team = _context.Teams.OrderByDescending(x => x.TeamId).First();
                    foreach (string name in t.TeamMembers)
                    {
                        await _context.Members.AddAsync(new Member { TeamId = team.TeamId, Name = name });
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

        public async Task<bool> Delete(int id)
        {
            try
            {
                var data = await GetById(id);
                data.IsActive = false;

                if (data.Member != null)
                {
                    foreach (var member in data.Member)
                    {
                        member.IsActive = false;
                    }
                }
                else
                {
                    return false;
                }

                if (data.Projects != null)
                {
                    int projectId = 0;
                    foreach (var project in data.Projects)
                    {
                        projectId = project.ProjectId;
                        project.IsActive = false;
                    }
                    if (projectId > 0)
                    {
                        var Report = await _context.Reaports.Where(x => x.IsActive && x.ProjectId == projectId).ToListAsync();
                        foreach (var report in Report)
                        {
                            report.IsActive = false;
                        }
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

        public async Task<List<Team>> GetAll()
        {
            var fullTiem = await _context.Teams.Where(p => p.IsActive).Include(x => x.Member.Where(x => x.IsActive)).ToListAsync();

            if (fullTiem != null)
            {
                foreach (var item in fullTiem)
                {
                    foreach (var member in item.Member)
                    {
                        if (item.TeamId == member.TeamId)
                        {
                            item.TeamMembers.Add(member.Name);
                        }
                    }
                }
            }
          
            return fullTiem;
        }

        public async Task<Team> GetById(int id)
        {
            try
            {
                var data = _context.Teams.Include(s => s.Member).
                    Include(x=>x.Projects.Where(x=>x.IsActive)).FirstOrDefault(x => x.TeamId == id && x.IsActive);
                if (data != null && data.Member != null)
                {
                    foreach (var team in data.Member)
                    {
                        data.TeamMembers.Add(team.Name);
                    }
                }

                return data != null ? data : new Team { };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new Team { };
            }
        }

        public async Task<bool> Update(Team t)
        {
           
            try
            {
       
                var existingMembers = await _context.Members
                    .Where(x => x.IsActive && x.TeamId == t.TeamId)
                    .ToListAsync();

                foreach (var member in existingMembers)
                {
                  
                    if (!t.TeamMembers.Contains(member.Name))
                    {
                        member.IsActive = false;
                    }
                }

                foreach (var name in t.TeamMembers)
                {
                    
                    if (!existingMembers.Any(x => x.Name == name))
                    {
                        _context.Members.Add(new Member
                        {
                            TeamId = t.TeamId,
                            Name = name,
                            IsActive = true
                        });
                    }
                }

                _context.Teams.Update(t);

                await _context.SaveChangesAsync();

                return true;
            
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }
    }
}
