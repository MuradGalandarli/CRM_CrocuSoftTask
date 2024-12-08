using AutoMapper;
using Business.Layer.Abstract;
using DataAccess.Layer.Abstract;
using DataTransferObject.RequestDto;
using Entity.Layer.Entity;
using FluentValidation;
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
        private readonly IMapper _mapper;
        private readonly IValidator<RequestReport> _validator;
        public ReportController(IReportService _reportService,
            IMapper mapper,
            IValidator<RequestReport> validator)
        {
            this._reportService = _reportService;
            _mapper = mapper;
            _validator = validator;
        }
        [HttpPost("AddReport")]
        public async Task<IActionResult> AddReport(RequestReport report)
        {
            var validation = _validator.Validate(report);
            if (validation.IsValid)
            {
                var mapReport = _mapper.Map<Report>(report);
                bool IsSuccess = await _reportService.Add(mapReport);
                return IsSuccess ? Ok(IsSuccess) : BadRequest(IsSuccess);
            }
            return BadRequest();
        }

        [HttpPut("UpdateReport")]
        public async Task<IActionResult> UpdateReport(RequestReport report)
        {
            var validation = _validator.Validate(report);
            if (validation.IsValid)
            { 
                var mapReport = _mapper.Map<Report>(report);
                bool IsSuccess = await _reportService.Update(mapReport);
                return IsSuccess ? Ok(IsSuccess) : BadRequest(IsSuccess);
            }
            return BadRequest();    
        }

        [HttpGet("DeleteReport/{id}")]
        public async Task<IActionResult> DeleteReport(int id)
        {
            bool IsSuccess = await _reportService.Delete(id);
            return IsSuccess ? Ok(IsSuccess) : BadRequest(IsSuccess);
        }
    }
}
