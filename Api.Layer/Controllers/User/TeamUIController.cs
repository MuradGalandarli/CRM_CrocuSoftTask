using Business.Layer.Abstract;
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
        public TeamUIController(ITeamService _teamService)
        {
            this._teamService = _teamService;
        }

        [HttpGet("GetByIdTeam/{id}")]
        public async Task<IActionResult> GetByIdTeam(int id)
        {
            var result = await _teamService.GetById(id);
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve,
                WriteIndented = true
            };

            var json = JsonSerializer.Serialize(result, options);
            return !string.IsNullOrEmpty(json) ? Ok(json) : BadRequest();
        }

        [HttpGet("GetAllTeam")]
        public async Task<IActionResult> GetAllTeam()
        {
          
            var result = await _teamService.GetAll();
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve,
                WriteIndented = true
            };

            var json = JsonSerializer.Serialize(result, options);
         
            return json != null ? Ok(json) : BadRequest();

        }
    }
}
