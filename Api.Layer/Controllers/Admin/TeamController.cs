using Business.Layer.Abstract;
using Entity.Layer;
using Entity.Layer.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Layer.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly ITeamService _teamService;
        public TeamController(ITeamService _teamService)
        {
            this._teamService = _teamService;
        }
        [HttpPost("AddTeam")]
        public async Task<IActionResult> AddTeam(Team team)
        {
            bool IsSuccess = await _teamService.Add(team);

            return IsSuccess ? Ok(IsSuccess) : BadRequest(IsSuccess);
        }

        [HttpPut("UpdateTeam")]
        public async Task<IActionResult> UpdateTeam(Team team)
        {
            bool IsSuccess = await _teamService.Update(team);
            return IsSuccess ? Ok(IsSuccess) : BadRequest(IsSuccess);
        }

        [HttpGet("DeleteTeam/{id}")]
        public async Task<IActionResult> DeleteTeam(int id)
        {
            bool IsSuccess = await _teamService.Delete(id);
            return IsSuccess ? Ok(IsSuccess) : BadRequest(IsSuccess);
        }
    }
}
