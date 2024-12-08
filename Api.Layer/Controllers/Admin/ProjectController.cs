using AutoMapper;
using Business.Layer.Abstract;
using DataTransferObject.RequestDto;
using Entity.Layer;
using Entity.Layer.Entity;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Api.Layer.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
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
            var validation = _validator.Validate(project);
            if (validation.IsValid)
            {
                var mapProject = _mapper.Map<Project>(project);
                bool IsSuccess = await _projectService.Add(mapProject);
                return IsSuccess ? Ok(IsSuccess) : BadRequest(IsSuccess);
            }
          
              return BadRequest();
            
        }

        [HttpPut("UpdateProject")]
        public async Task<IActionResult> UpdateProject(RequestProject project)
        {
            var validation = _validator.Validate(project);
            if (validation.IsValid)
            {

                var mapProject = _mapper.Map<Project>(project);
                bool IsSuccess = await _projectService.Update(mapProject);
                return IsSuccess ? Ok(IsSuccess) : BadRequest(IsSuccess);
            }
            return BadRequest();
        }

        [HttpGet("DeleteProject/{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            bool IsSuccess = await _projectService.Delete(id);
            return IsSuccess ? Ok(IsSuccess) : BadRequest(IsSuccess);
        }
    }
}
