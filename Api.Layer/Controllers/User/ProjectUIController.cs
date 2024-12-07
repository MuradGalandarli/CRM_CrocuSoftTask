using Business.Layer.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Layer.Controllers.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectUIController : ControllerBase
    {
        private readonly IProjectService _projectService;
        public ProjectUIController(IProjectService _projectService)
        {
            this._projectService = _projectService;
        }

        [HttpGet("GetByIdProject/{id}")]
        public async Task<IActionResult> GetByIdProject(int id)
        {
            var result = await _projectService.GetById(id);

            return !string.IsNullOrEmpty(result.ProjectName) ? Ok(result) : BadRequest();
        }

        [HttpGet("GetAllProject")]
        public async Task<IActionResult> GetAllProject()
        {
            var result = await _projectService.GetAll();

            return result.Any() ? Ok(result) : BadRequest();

        }
    }
}
