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
        public async Task<(bool, int StatusCode)> Add(Report t)
        {
           var IsSuccess = await _report.Add(t);
            return IsSuccess;
        }

        public async Task<(bool, int StatusCode)> Delete(int id)
        {
            var IsSuccess = await _report.Delete(id);  
            return IsSuccess;
        }

        public async Task<(List<Report>, int StatusCode)> GetAll()
        {
            var data = await _report.GetAll();
            return data;
        }

        public async Task<(Report, int StatusCode)> GetById(int id)
        {
            var data = await _report.GetById(id);
            return data;
        }

        public async Task<(bool, int StatusCode)> Update(Report t)
        {
            var IsSuccess = await _report.Update(t);
            return IsSuccess;
        }
    }
}
