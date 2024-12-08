using AutoMapper;
using Business.Layer.Abstract;
using DataTransferObject.ResponseDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Layer.Controllers.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectUIController : ControllerBase
    {
        private readonly IProjectService _projectService;
        private readonly IMapper _mapper;
        public ProjectUIController(IProjectService _projectService,
            Mapper mapper)
        {
            this._projectService = _projectService;
            _mapper = mapper;
        }

        [HttpGet("GetByIdProject/{id}")]
        public async Task<IActionResult> GetByIdProject(int id)
        {
            var result = await _projectService.GetById(id);
            var mapResponcePriject = _mapper.Map<ResponseProject>(result);
            return !string.IsNullOrEmpty(mapResponcePriject.ProjectName) ? Ok(mapResponcePriject) : BadRequest();
        }

        [HttpGet("GetAllProject")]
        public async Task<IActionResult> GetAllProject()
        {
            var result = await _projectService.GetAll();
            var mapResponcePriject = _mapper.Map<List<ResponseProject>>(result);
            return mapResponcePriject != null ? Ok(mapResponcePriject) : BadRequest();

        }
    }
}
