using Business.Layer.Abstract;
using Entity.Layer;
using Entity.Layer.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Layer.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;
        public ProjectController(IProjectService _projectService)
        {
            this._projectService = _projectService;
        }
        [HttpPost("AddProject")]
        public async Task<IActionResult> AddReport(Project project)
        {
            bool IsSuccess = await _projectService.Add(project);

            return IsSuccess ? Ok(IsSuccess) : BadRequest(IsSuccess);
        }

        [HttpPut("UpdateProject")]
        public async Task<IActionResult> UpdateProject(Project project)
        {
            bool IsSuccess = await _projectService.Update(project);
            return IsSuccess ? Ok(IsSuccess) : BadRequest(IsSuccess);
        }

        [HttpGet("DeleteProject/{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            bool IsSuccess = await _projectService.Delete(id);
            return IsSuccess ? Ok(IsSuccess) : BadRequest(IsSuccess);
        }
    }
}
