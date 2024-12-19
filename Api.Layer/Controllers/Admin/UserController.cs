using AutoMapper;
using Business.Layer.Abstract;
using Business.Layer.Helper;
using DataTransferObject.RequestDto;
using DataTransferObject.ResponseDto;
using Entity.Layer.Entity;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Layer.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Admin")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IValidator<RequestUser> _validator;
        private readonly IValidator<RequestUserUpdate> _validatorUser;
       
        public UserController(IUserService userService,
            IMapper mapper,
            IValidator<RequestUser> validator,
            IValidator<RequestUserUpdate> _validatorUser)
        {
            _mapper = mapper;
            _userService = userService;
            _validator = validator;
            this._validatorUser = _validatorUser;
        }
    

        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser([FromBody] RequestUserUpdate user)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized(ErrorManager.ErrorHandling(401));
            }

            var validation = _validatorUser.Validate(user);
            if (validation.IsValid)
            {
                (bool IsSuccess, int statusCode) result = await _userService.Update(user);
                if (result.IsSuccess)
                {
                    return Ok(result.IsSuccess);
                }
                var data = ErrorManager.ErrorHandling(result.statusCode);
                return (data.StatusCode == 404) ? NotFound(data) : StatusCode(data.StatusCode, data.Error);
            }

            return BadRequest(ErrorManager.ErrorHandling(400, validation.Errors.Select(x => x.PropertyName).ToList()));
        }

        [HttpDelete("DeleteUser/{userId}")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized(ErrorManager.ErrorHandling(401));
            }
            (bool IsSuccess, int statusCode) result = await _userService.Delete(userId);
            if (result.IsSuccess)
            {
                return Ok(result.IsSuccess);
            }
            var data = ErrorManager.ErrorHandling(result.statusCode);
            return (data.StatusCode == 404) ? NotFound(data) : StatusCode(data.StatusCode, data.Error);
        }

        [HttpGet("GetByIdUser/{userId}")]
        public async Task<IActionResult> GetByIdReport(string userId)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized(ErrorManager.ErrorHandling(401));
            }
            var result = await _userService.GetById(userId);
            switch (result.StatusCode)
            {
                case 200:
                    {
                        var mapUser = _mapper.Map<ResponseUser>(result.Item1);
                        return Ok(mapUser);
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

        [HttpGet("GetAllUser")]
        public async Task<IActionResult> GetAllUser()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized(ErrorManager.ErrorHandling(401));
            }
            var result = await _userService.GetAll();
            switch (result.StatusCode)
            {
                case 200:
                    {
                        var mapUser = _mapper.Map<List<ResponseUser>>(result.Item1);
                        return Ok(mapUser);
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
