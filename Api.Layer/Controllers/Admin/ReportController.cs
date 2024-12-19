using AutoMapper;
using Business.Layer.Abstract;
using Business.Layer.Helper;
using DataTransferObject.RequestDto;
using Entity.Layer.Entity;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Layer.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,User")]
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
        public async Task<IActionResult> AddReport([FromBody] RequestReport report)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized(ErrorManager.ErrorHandling(401));
            }
            var validation = _validator.Validate(report);
            if (validation.IsValid)
            {
                var mapReport = _mapper.Map<Report>(report);
                (bool IsSuccess, int statusCode) result = await _reportService.Add(mapReport);
                if (result.IsSuccess)
                {
                    return Ok(result.IsSuccess);
                }
                var data = ErrorManager.ErrorHandling(result.statusCode);
                return (data.StatusCode == 404) ? NotFound(data) : StatusCode(data.StatusCode, data.Error);
            }
            return BadRequest(ErrorManager.ErrorHandling(400, validation.Errors.Select(x => x.PropertyName).ToList()));
        }

        [HttpPut("UpdateReport")]
        public async Task<IActionResult> UpdateReport([FromBody] RequestReport report)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized(ErrorManager.ErrorHandling(401));
            }

            var validation = _validator.Validate(report);
            if (validation.IsValid)
            {
                var mapReport = _mapper.Map<Report>(report);
                (bool IsSuccess, int statusCode) result = await _reportService.Update(mapReport);
                if (result.IsSuccess)
                {
                    return Ok(result.IsSuccess);
                }
                var data = ErrorManager.ErrorHandling(result.statusCode);
                return (data.StatusCode == 404) ? NotFound(data) : StatusCode(data.StatusCode, data.Error);
            }

            return BadRequest(ErrorManager.ErrorHandling(400, validation.Errors.Select(x => x.PropertyName).ToList()));
        }

        [HttpDelete("DeleteReport/{id}")]
        public async Task<IActionResult> DeleteReport(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized(ErrorManager.ErrorHandling(401));
            }

            (bool IsSuccess, int statusCode) result = await _reportService.Delete(id);
            if (result.IsSuccess)
            {
                return Ok(result.IsSuccess);
            }
            var data = ErrorManager.ErrorHandling(result.statusCode);
            return (data.StatusCode == 404) ? NotFound(data) : StatusCode(data.StatusCode, data.Error);
        }
    }
}
