using Business.Layer.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Layer.Controllers.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportUIController : ControllerBase
    {
        private readonly IReportService _reportService;
        public ReportUIController(IReportService _reportService)
        {
            this._reportService = _reportService;
        }
        [HttpGet("GetByIdReport/{id}")]
        public async Task<IActionResult> GetByIdReport(int id)
        {
            var result = await _reportService.GetById(id);
            var a = Response.StatusCode;
            return !string.IsNullOrEmpty(result.Title) ? Ok(result) : BadRequest();
        }

        [HttpGet("GetAllReport")]
        public async Task<IActionResult> GetAllReport()
        {
            var result = await _reportService.GetAll();
          
            return !result.Any() ? Ok(result) : BadRequest();
        }
    }
}
