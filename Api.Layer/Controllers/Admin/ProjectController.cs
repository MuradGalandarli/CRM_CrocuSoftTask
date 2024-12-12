using AutoMapper;
using Business.Layer.Abstract;
using Business.Layer.Helper;
using DataTransferObject.RequestDto;
using Entity.Layer;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Layer.Controllers.Admin
{
  
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;
        private readonly IMapper _mapper;
        private readonly IValidator<RequestProject> _validator;
        public ProjectController(IProjectService _projectService,
            IMapper mapper,
            IValidator<RequestProject> validator)
        {
            this._projectService = _projectService;
            _mapper = mapper;
            _validator = validator;
        }

        [HttpPost("AddProject")]
        public async Task<IActionResult> AddReport(RequestProject project)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized(ErrorManager.ErrorHandling(401));
            }

            var validation = _validator.Validate(project);
            if (validation.IsValid)
            {
                var mapProject = _mapper.Map<Project>(project);
                (bool IsSuccess, int statusCode) result = await _projectService.Add(mapProject);
                if (result.IsSuccess)
                {
                    return Ok(result.IsSuccess);
                }
                var data = ErrorManager.ErrorHandling(result.statusCode);
                return (data.StatusCode == 404) ? NotFound(data) : StatusCode(data.StatusCode, data.Error);
            }
            return BadRequest(ErrorManager.ErrorHandling(400, validation.Errors.Select(x => x.PropertyName).ToList()));
        }

        [HttpPut("UpdateProject")]
        public async Task<IActionResult> UpdateProject(RequestProject project)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized(ErrorManager.ErrorHandling(401));
            }

            var validation = _validator.Validate(project);
            if (validation.IsValid)
            {
                var mapProject = _mapper.Map<Project>(project);
                (bool IsSuccess, int statusCode) result = await _projectService.Update(mapProject);
                if (result.IsSuccess)
                {
                    return Ok(result.IsSuccess);
                }
                var error = ErrorManager.ErrorHandling(result.statusCode);
                return (error.StatusCode == 404) ? NotFound(error) : StatusCode(error.StatusCode, error);
            }
            return BadRequest(ErrorManager.ErrorHandling(400, validation.Errors.Select(x => x.PropertyName).ToList()));
        }

        [HttpDelete("DeleteProject/{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized(ErrorManager.ErrorHandling(401));
            }
            (bool IsSuccess, int statusCode) result = await _projectService.Delete(id);
            if (result.IsSuccess)
            {
                return Ok(result.IsSuccess);
            }
            var data = ErrorManager.ErrorHandling(result.statusCode);
            return (data.StatusCode == 404) ? NotFound(data) : StatusCode(data.StatusCode, data.Error);
        }

    }
}
