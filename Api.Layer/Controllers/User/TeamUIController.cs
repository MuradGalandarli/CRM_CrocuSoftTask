using AutoMapper;
using Business.Layer.Abstract;
using Business.Layer.Helper;
using DataTransferObject.ResponseDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Api.Layer.Controllers.User
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,User")]
    public class TeamUIController : ControllerBase
    {
        private readonly ITeamService _teamService;
        private readonly IMapper _mapper;
        public TeamUIController(ITeamService _teamService,
            IMapper mapper)
        {
            this._teamService = _teamService;
            _mapper = mapper;
        }

        [HttpGet("GetByIdTeam/{id}")]
        public async Task<IActionResult> GetByIdTeam(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized(ErrorManager.ErrorHandling(401));
            }
            var result = await _teamService.GetById(id);
            switch (result.StatusCode)
            {
                case 200:
                    {
                        var mapTeam = _mapper.Map<ResponseTeam>(result.Item1);

                        var options = new JsonSerializerOptions
                        {
                            ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve,
                            WriteIndented = true
                        };

                        var json = JsonSerializer.Serialize(mapTeam, options);
                        return Ok(json);
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

        [HttpGet("GetAllTeam")]
        public async Task<IActionResult> GetAllTeam()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized(ErrorManager.ErrorHandling(401));
            }
            var result = await _teamService.GetAll();
            switch (result.StatusCode)
            {
                case 200:
                    {
                        var mapTeam = _mapper.Map<List<ResponseTeam>>(result.Item1);
                        var options = new JsonSerializerOptions
                        {
                            ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve,
                            WriteIndented = true
                        };

                        var json = JsonSerializer.Serialize(mapTeam, options);

                        return Ok(json);
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

