using AutoMapper;
using Business.Layer.Abstract;
using Business.Layer.Helper;
using DataTransferObject.ResponseDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace Api.Layer.Controllers.User
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Admin,User")]
    public class DeCodeTokenController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;
        public DeCodeTokenController(IAuthService _authService,
            IMapper mapper)
        {
            this._authService = _authService;
            _mapper = mapper;
        }
        [HttpPost("Profile")]
        public async Task<IActionResult> Profile()
        {
          string header = Request.Headers.Authorization;
             var token = header.Split(' ');
            var handler = new JwtSecurityTokenHandler();
          
            var read = handler.ReadJwtToken(token[1]);
            var email = read.Payload.FirstOrDefault(x=>x.Key == "email").Value;
            
           var result = await _authService.DecodeToken((string)email);
            switch(result.StatusCode)
            {
                case 200:
                    {
                        var mapUser = _mapper.Map<ResponseUser> (result.Item1);
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
