using AutoMapper;
using Business.Layer.Abstract;
using DataAccess.Layer.Concret;
using DataTransferObject.RequestDto;
using Entity.Layer;
using Entity.Layer.Entity;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Layer.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
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
            var validation = _validator.Validate(team);
            if (validation.IsValid)
            {
                var mapTeam = _mapper.Map<Team>(team);
                bool IsSuccess = await _teamService.Add(mapTeam);
                return IsSuccess ? Ok(IsSuccess) : BadRequest(IsSuccess);
            }
            return BadRequest();
        }

        [HttpPut("UpdateTeam")]
        public async Task<IActionResult> UpdateTeam(RequestTeam team)
        {
            var validation = _validator.Validate(team);
            if (validation.IsValid)
            {
                var mapTeam = _mapper.Map<Team>(team);
                bool IsSuccess = await _teamService.Update(mapTeam);
                return IsSuccess ? Ok(IsSuccess) : BadRequest(IsSuccess);
            }
            return BadRequest();    
        }

        [HttpGet("DeleteTeam/{id}")]
        public async Task<IActionResult> DeleteTeam(int id)
        {
            bool IsSuccess = await _teamService.Delete(id);
            return IsSuccess ? Ok(IsSuccess) : BadRequest(IsSuccess);
        }
    }
}
