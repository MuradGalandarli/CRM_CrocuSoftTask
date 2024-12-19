using AutoMapper.Configuration.Annotations;
using Business.Layer.Abstract;
using DataAccess.Layer.Abstract;
using Entity.Layer.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Shred.Layer.AuthModel;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Business.Layer.Concret
{
    public class AuthManager : IAuthService, ITokenHandler
    {
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly JWT _Jwt;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthManager> _logger;
        private readonly ITeam _team;

        public AuthManager(UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration,
            IOptions<JWT> Jwt,
            ILogger<AuthManager> _logger,
            ITeam team
          )
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            _configuration = configuration;
            _Jwt = Jwt.Value;
            this._logger = _logger;
            _team = team;

        }


        public async Task<(int, string)> Registeration(RegistrationModel model, string role)
        {
            if (await _team.CheckTeam(model.TeamId))
            {
                var userExists = await userManager.FindByNameAsync(model.Email);
                if (userExists != null)
                    return (0, "User already exists");

                AppUser user = new AppUser()
                {
                    Email = model.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = model.Email,
                    Name = model.Name,
                    SurName = model.SurName,
                    Role = role,  
                    TeamId = model.TeamId,
                };
                var createUserResult = await userManager.CreateAsync(user, model.Password);
                if (!createUserResult.Succeeded)
                    return (0, "User creation failed! Please check user details and try again.");

                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));

                await userManager.AddToRoleAsync(user, role);

                return (1, "User created successfully!");
            }
            return (0, "The specified team was not found in the database.");
        }


        public async Task<(int, Token)> Login(LoginModel model)
        {
            Token token = new Token();
            var user = await userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                throw new Exception("Invalid email");
            }

            if (!await userManager.CheckPasswordAsync(user, model.Password))

            {
                throw new Exception("Invalid password");
            }

            token = await GenerateToken(user);
            await UpdateRefreshToken(token, user, 15);
            return (1, token);
        }


        public async Task<Token> GenerateToken(AppUser user)
        {
            Token token = new Token();
            var userRoles = await userManager.GetRolesAsync(user);
            var authClaims = new List<Claim>
            {
              // new Claim(ClaimTypes.NameIdentifier,user.Id),
               new Claim(ClaimTypes.Email, user.Email),
               new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            DateTime date = token.Expiration = DateTime.UtcNow.AddMinutes(45);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _configuration["JWT:ValidIssuer"],
                Audience = _configuration["JWT:ValidAudience"],
                //Expires = DateTime.UtcNow.AddMinutes(2),
                Expires = date,
                SigningCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256),
                Subject = new ClaimsIdentity(authClaims)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var accessToken = tokenHandler.CreateToken(tokenDescriptor);
            token.RefreshToken = GenerateRefreshToken();
            token.AccessToken = tokenHandler.WriteToken(accessToken);
            token.Expiration = date;

            return token;
        }

        public string GenerateRefreshToken()
        {
            byte[] number = new byte[32];
            using RandomNumberGenerator random = RandomNumberGenerator.Create();
            random.GetBytes(number);
            return Convert.ToBase64String(number);
        }

        public async Task UpdateRefreshToken(Token token, AppUser user, int addOnAccessTokenDate)
        {
            if (user != null)
            {
                user.RefreshToken = token.RefreshToken;
                user.RefreshTokenEndDate = token.Expiration.AddMinutes(addOnAccessTokenDate);
                await userManager.UpdateAsync(user);

            }
            else
                throw new Exception("There is not user");
        }

        public async Task<Token> UpdateToken(RefreshTokenModel refreshToken)
        {
            var user = userManager.Users.FirstOrDefault(x => x.RefreshToken == refreshToken.RefreshToken && x.RefreshTokenEndDate > DateTime.UtcNow);
            if (user != null)
            {
                Token token = await GenerateToken(user);
                await UpdateRefreshToken(token, user, 15);
                return token;
            }
            throw new Exception("Refresh token is not valid");
        }

        public async Task<(AppUser, int StatusCode)> DecodeToken(string email)
        {
            try
            {
                if (!string.IsNullOrEmpty(email))
                {
                    AppUser? user = await userManager.FindByEmailAsync(email);
                    return (user, 200);
                }
                return (new AppUser { }, 404);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return (new AppUser { }, 500);
            }
            
        }
    }
}

