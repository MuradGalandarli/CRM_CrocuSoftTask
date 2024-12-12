using AutoMapper;
using Business.Layer.Abstract;
using Business.Layer.Helper;
using DataTransferObject.ResponseDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Layer.Controllers.User
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Admin,User")]
    
    public class ReportUIController : ControllerBase
    {
        private readonly IReportService _reportService;
        private readonly IMapper _mapper;
        public ReportUIController(IReportService _reportService,
            IMapper _mapper)
        {
            this._reportService = _reportService;
            this._mapper = _mapper;
        }

        [HttpGet("GetByIdReport/{id}")]
        public async Task<IActionResult> GetByIdReport(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized(ErrorManager.ErrorHandling(401));
            }
            var result = await _reportService.GetById(id);
            switch (result.StatusCode)
            {
                case 200:
                    {
                        var mapReport = _mapper.Map<ResponseReport>(result.Item1);
                        return Ok(mapReport);
                    }
                case 404:
                    {
                        return NotFound(ErrorManager.ErrorHandling(404));
                    }
                case 500:
                    {
                        return StatusCode(result.StatusCode, ErrorManager.ErrorHandling(500));
                    }

                default: return StatusCode(500, new { Error = "Unexpected error occurred", StatusCode = result.StatusCode });
            }
        }

        [HttpGet("GetAllReport")]
        public async Task<IActionResult> GetAllReport()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized(ErrorManager.ErrorHandling(401));
            }

            var result = await _reportService.GetAll();
            switch (result.StatusCode)
            {
                case 200:
                    {
                        var mapReport = _mapper.Map<List<ResponseReport>>(result.Item1);
                        return Ok(mapReport);
                    }
                case 404:
                    {
                        return NotFound(ErrorManager.ErrorHandling(404));
                    }
                case 500:
                    {
                        return StatusCode(result.StatusCode, ErrorManager.ErrorHandling(500));
                    }
                default: return StatusCode(500, new { Error = "Unexpected error occurred", StatusCode = result.StatusCode });
            }
        }
    }
}
