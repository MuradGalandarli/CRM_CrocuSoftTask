using AutoMapper;
using Business.Layer.Abstract;
using DataTransferObject.ResponseDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Api.Layer.Controllers.User
{
    [Route("api/[controller]")]
    [ApiController]
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
            var result = await _teamService.GetById(id);
            var mapTeam = _mapper.Map<ResponseTeam>(result);
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve,
                WriteIndented = true
            };

            var json = JsonSerializer.Serialize(mapTeam, options);
            return !string.IsNullOrEmpty(json) ? Ok(json) : BadRequest();
        }

        [HttpGet("GetAllTeam")]
        public async Task<IActionResult> GetAllTeam()
        {
          
            var result = await _teamService.GetAll();
            var mapTeam = _mapper.Map<List<ResponseTeam>>(result);
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve,
                WriteIndented = true
            };

            var json = JsonSerializer.Serialize(mapTeam, options);
         
            return json != null ? Ok(json) : BadRequest();

        }
    }
}
