using Business.Layer.Abstract;
using DataAccess.Layer.Abstract;
using Entity.Layer.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Api.Layer.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;
        public ReportController(IReportService _reportService)
        {
            this._reportService = _reportService;
        }
        [HttpPost("AddReport")]
        public async Task<IActionResult> AddReport(Report report)
        {
            bool IsSuccess = await _reportService.Add(report);

            return IsSuccess ? Ok(IsSuccess) : BadRequest(IsSuccess);
        }

        [HttpPut("UpdateReport")]
        public async Task<IActionResult> UpdateReport(Report report)
        {
            bool IsSuccess = await _reportService.Update(report);
            return IsSuccess ? Ok(IsSuccess) : BadRequest(IsSuccess);
        }

        [HttpGet("DeleteReport/{id}")]
        public async Task<IActionResult> DeleteReport(int id)
        {
            bool IsSuccess = await _reportService.Delete(id);
            return IsSuccess ? Ok(IsSuccess) : BadRequest(IsSuccess);
        }
    }
}
