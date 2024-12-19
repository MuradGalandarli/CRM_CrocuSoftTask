using Business.Layer.Abstract;
using Business.Layer.Validator;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shred.Layer.AuthModel;

namespace Api.Layer.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]

    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IValidator<LoginModel> _loginValidator;
        private readonly IValidator<RegistrationModel> _registrationValidator;

        public AuthController(IAuthService authService,
           IValidator<LoginModel> _loginValidator,
           IValidator<RegistrationModel> _registrationValidator)
        {
            _authService = authService;
            this._loginValidator = _loginValidator;
            this._registrationValidator = _registrationValidator;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody]LoginModel model)
        {
            try
            {
                var validation = _loginValidator.Validate(model);
                if (!validation.IsValid)
                    return BadRequest(validation.Errors.Select(x => x.ErrorMessage));
                var (status, message) = await _authService.Login(model);
                if (status == 0)
                    return BadRequest(message);
                return Ok(message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
         
        [Authorize(Roles ="Admin")]
        [HttpPost]
        [Route("Registeration")]
        public async Task<IActionResult> Register([FromBody]RegistrationModel model)
        {
            try
            {
                var validation = _registrationValidator.Validate(model);
                if (!validation.IsValid)
                    return BadRequest(validation.Errors.Select(x => x.ErrorMessage));
                var (status, message) = await _authService.Registeration(model, UserRoles.User);
                if (status == 0)
                {
                    return BadRequest(message);
                }
                return CreatedAtAction(nameof(Register), model);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("RefreshToken")]
        public async Task<IActionResult> RefreshTokenLogin([FromBody] RefreshTokenModel refreshToken)
        {
            Token newToken = await _authService.UpdateToken(refreshToken);
           return Ok(newToken);     
        }



    }
}
