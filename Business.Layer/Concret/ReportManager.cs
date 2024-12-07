using Business.Layer.Abstract;
using DataAccess.Layer.Abstract;
using Entity.Layer.Entity;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Layer.Concret
{
    public class ReportManager : IReportService
    {
        private readonly IReport _report; 
        public ReportManager(IReport report)
        {
            _report = report;
        }
        public async Task<bool> Add(Report t)
        {
           var IsSuccess = await _report.Add(t);
            return IsSuccess;
        }

        public async Task<bool> Delete(int id)
        {
            var IsSuccess = await _report.Delete(id);  
            return IsSuccess;
        }

        public async Task<List<Report>> GetAll()
        {
            var data = await _report.GetAll();
            return data;
        }

        public async Task<Report> GetById(int id)
        {
            var data = await _report.GetById(id);
            return data;
        }

        public async Task<bool> Update(Report t)
        {
            var IsSuccess = await _report.Update(t);
            return IsSuccess;
        }
    }
}
