using AutoMapper;
using Business.Layer.Abstract;
using DataTransferObject.ResponseDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Layer.Controllers.User
{
    [Route("api/[controller]")]
    [ApiController]
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
            var result = await _reportService.GetById(id);
            var mapReport = _mapper.Map<ResponseReport>(result);
            return mapReport != null ? Ok(mapReport) : BadRequest();
        }

        [HttpGet("GetAllReport")]
        public async Task<IActionResult> GetAllReport()
        {
            var result = await _reportService.GetAll();
            var mapReport = _mapper.Map<List<ResponseReport>>(result);
            return mapReport != null ? Ok(mapReport) : BadRequest();
        }
    }
}
