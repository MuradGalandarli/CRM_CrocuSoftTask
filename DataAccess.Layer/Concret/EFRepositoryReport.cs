using DataAccess.Layer.Abstract;
//using Entity.Layer;
using Entity.Layer.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace DataAccess.Layer.Concret
{
    public class EFRepositoryReport : IReport 
    {
        private readonly ApplicationContext _context;
        private readonly ILogger<EFRepositoryReport> _logger;
        public EFRepositoryReport(ApplicationContext _context,
             ILogger<EFRepositoryReport> logger)
        {
            this._context = _context;
            _logger = logger;
        }

        public async Task<(bool, int StatusCode)> Add(Report t)
        {
            try
            {
                var checkProject = await _context.Projects.AnyAsync(x => x.IsActive && x.ProjectId == t.ProjectId);
                if (checkProject)
                {
                    await _context.AddAsync(t);
                    await _context.SaveChangesAsync();
                    return (true,200);
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
        
        public async Task<(List<Report>, int StatusCode)> GetAll()
        {
            var data = await _context.Reaports.Where(p => p.IsActive).ToListAsync();
            return data.Count > 0 ? (data, 200) : (new List<Report>(), 404);
        }

        public async Task<(Report, int StatusCode)> GetById(int id)
        {
            try
            {
                var data = _context.Reaports.FirstOrDefault(x => x.ReaportId == id && x.IsActive);
                return data != null ? (data,200) : (new Report { }, 404);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return (new Report { },500) ;
            }
        }

        public async Task<(bool, int StatusCode)> Update(Report t)
        {
            try
            {
                var checkProject = await _context.Projects.AnyAsync(x => x.IsActive && x.ProjectId == t.ProjectId);
                if (checkProject)
                {
                    var data = _context.Reaports.Update(t);
                    await _context.SaveChangesAsync();
                    return (true,200);
                }
                return (false,404);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return (false,500);
            }
        }
    }
}
