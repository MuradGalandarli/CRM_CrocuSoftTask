using AutoMapper;
using Business.Layer.Abstract;
using Business.Layer.Helper;
using DataTransferObject.ResponseDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Layer.Controllers.User
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Admin,User")]
    public class ProjectUIController : ControllerBase
    {
        private readonly IProjectService _projectService;
        private readonly IMapper _mapper;
        public ProjectUIController(IProjectService _projectService,
            IMapper mapper)
        {
            this._projectService = _projectService;
            _mapper = mapper;
        }

        [HttpGet("GetByIdProject/{id}")]
        public async Task<IActionResult> GetByIdProject(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized(ErrorManager.ErrorHandling(401));
            }
            var result = await _projectService.GetById(id);
            switch (result.StatusCode)
            {
                case 200:
                    {
                        var mapResponcePriject = _mapper.Map<ResponseProject>(result.Item1);
                        return Ok(mapResponcePriject);
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

        [HttpGet("GetAllProject")]
        public async Task<IActionResult> GetAllProject()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized(ErrorManager.ErrorHandling(401));
            }
            var result = await _projectService.GetAll();
            switch (result.StatusCode)
            {
                case 200:
                    {
                        var mapResponseProject = _mapper.Map<List<ResponseProject>>(result.Item1);
                        return Ok(mapResponseProject);
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
