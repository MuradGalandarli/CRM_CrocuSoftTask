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

        public async Task<bool> Add(Report t)
        {
            try
            {
                var checkProject = await _context.Projects.AnyAsync(x => x.IsActive && x.ProjectId == t.ProjectId);
                if (checkProject)
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
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        public async Task<List<Report>> GetAll()
        {
            var data = await _context.Reaports.Where(p => p.IsActive).ToListAsync();
            return data;
        }

        public async Task<Report> GetById(int id)
        {
            try
            {
                var data = _context.Reaports.FirstOrDefault(x => x.ReaportId == id && x.IsActive);
                return data != null ? data : new Report { };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new Report { };
            }
        }

        public async Task<bool> Update(Report t)
        {
            try
            {
                var checkProject = await _context.Projects.AnyAsync(x => x.IsActive && x.ProjectId == t.ProjectId);
                if (checkProject)
                {
                    var data = _context.Reaports.Update(t);
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
    }
}
