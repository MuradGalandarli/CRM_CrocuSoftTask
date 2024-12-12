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
    [Authorize(Roles = "Admin")]
    public class TeamController : ControllerBase
    {
        private readonly ITeamService _teamService;
        private readonly IMapper _mapper;
        private readonly IValidator<RequestTeam> _validator;
        public TeamController(ITeamService _teamService,
            IMapper mapper,
            IValidator<RequestTeam> validator
            )
        {
            this._teamService = _teamService;
            _mapper = mapper;
            _validator = validator;
        }

        [HttpPost("AddTeam")]
        public async Task<IActionResult> AddTeam(RequestTeam team)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized(ErrorManager.ErrorHandling(401));
            }
            var validation = _validator.Validate(team);
            if (validation.IsValid)
            {
                var mapTeam = _mapper.Map<Team>(team);
                (bool IsSuccess, int statusCode) = await _teamService.Add(mapTeam);
                if (IsSuccess)
                {
                    return Ok(IsSuccess);
                }
                var data = ErrorManager.ErrorHandling(statusCode);
                return (data.StatusCode == 404) ? NotFound(data) : StatusCode(data.StatusCode, data.Error);
            }
            return BadRequest(ErrorManager.ErrorHandling(400, validation.Errors.Select(x => x.PropertyName).ToList()));
        }

        [HttpPut("UpdateTeam")]
        public async Task<IActionResult> UpdateTeam(RequestTeam team)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized(ErrorManager.ErrorHandling(401));
            }
            var validation = _validator.Validate(team);
            if (validation.IsValid)
            {
                var mapTeam = _mapper.Map<Team>(team);
                (bool IsSuccess, int statusCode) result = await _teamService.Update(mapTeam);
                if (result.IsSuccess)
                {
                    return Ok(result.IsSuccess);
                }
                var data = ErrorManager.ErrorHandling(result.statusCode);
                return (data.StatusCode == 404) ? NotFound(data) : StatusCode(data.StatusCode, data);
            }
            return BadRequest(ErrorManager.ErrorHandling(400, validation.Errors.Select(x => x.PropertyName).ToList()));
        }

        [HttpDelete("DeleteTeam/{id}")]
        public async Task<IActionResult> DeleteTeam(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized(ErrorManager.ErrorHandling(401));
            }
            (bool IsSuccess, int statusCode) result = await _teamService.Delete(id);
            if (result.IsSuccess)
            {
                return Ok(result.IsSuccess);
            }
            var data = ErrorManager.ErrorHandling(result.statusCode);
            return (data.StatusCode == 404) ? NotFound(data) : StatusCode(data.StatusCode, data.Error);
        }
    }
}
