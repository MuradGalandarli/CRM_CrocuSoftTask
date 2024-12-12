using Business.Layer.Abstract;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Shred.Layer.AuthModel;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Business.Layer.Concret
{
    public class AuthManager:IAuthService
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly JWT  _Jwt;
        private readonly IConfiguration _configuration;
        public AuthManager(UserManager<IdentityUser> userManager, 
            RoleManager<IdentityRole> roleManager, 
            IConfiguration configuration,
            IOptions<JWT> Jwt)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            _configuration = configuration;
            _Jwt = Jwt.Value;
        }
        /*   public async Task<(int, string)> Registeration(RegistrationModel model, string role)
           {
               var userExists = await userManager.FindByEmailAsync(model.Email);
               if (userExists != null)
                   return (0, "User already exists");

               IdentityUser user = new IdentityUser()
               {
                   Email = model.Email,
                   SecurityStamp = Guid.NewGuid().ToString(),

               };
               var createUserResult = await userManager.CreateAsync(user, model.Password);
               if (!createUserResult.Succeeded)
                   return (0, "User creation failed! Please check user details and try again.");

               if (!await roleManager.RoleExistsAsync(role))
                   await roleManager.CreateAsync(new IdentityRole(role));

               userManager.AddToRoleAsync(user, role);

               return (1, "User created successfully!");
           }*/

        /*  public async Task<(int, string)> Registeration(RegistrationModel model, string role)
          {
              // Kullanıcı zaten var mı kontrol et
              var userExists = await userManager.FindByEmailAsync(model.Email);
              if (userExists != null)
                  return (0, "User already exists");

              // Yeni kullanıcı oluşturma
              IdentityUser user = new IdentityUser()
              {
                  UserName = model.Email,
                  Email = model.Email,
                  SecurityStamp = Guid.NewGuid().ToString(),
              };

              var createUserResult = await userManager.CreateAsync(user, model.Password);
              if (!createUserResult.Succeeded)
                  return (0, "User creation failed! Please check user details and try again.");

              // Rol mevcut değilse oluştur
              if (!await roleManager.RoleExistsAsync(role))
                  await roleManager.CreateAsync(new IdentityRole(role));

              // Kullanıcıyı role ekle
              var addToRoleResult = await userManager.AddToRoleAsync(user, role);
              if (!addToRoleResult.Succeeded)
                  return (0, "Failed to add user to role.");

              // Başarılı sonuç
              return (1, "User created successfully!");
          }
  */


        public async Task<(int, string)> Registeration(RegistrationModel model, string role)
        {
            var userExists = await userManager.FindByNameAsync(model.Email);
            if (userExists != null)
                return (0, "User already exists");

            IdentityUser user = new IdentityUser()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Email
            };
            var createUserResult = await userManager.CreateAsync(user, model.Password);
            if (!createUserResult.Succeeded)
                return (0, "User creation failed! Please check user details and try again.");

            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole(role));

           await userManager.AddToRoleAsync(user, role);

            return (1, "User created successfully!");
        }


        public async Task<(int, string)> Login(LoginModel model)
        {
            var user = await userManager.FindByNameAsync(model.Email);
            if (user == null)
                return (0, "Invalid username");
            if (!await userManager.CheckPasswordAsync(user, model.Password))
                return (0, "Invalid password");
           
            var userRoles = await userManager.GetRolesAsync(user);
            var authClaims = new List<Claim>
            {
               new Claim(ClaimTypes.Email, user.Email),
               new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }
            string token = GenerateToken(authClaims);
            return (1, token);
        }
        private string GenerateToken(IEnumerable<Claim> claims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _configuration["JWT:ValidIssuer"],
                Audience = _configuration["JWT:ValidAudience"],
                Expires = DateTime.UtcNow.AddHours(3),
                SigningCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256),
                Subject = new ClaimsIdentity(claims)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        //private string GenerateToken(IEnumerable<Claim> claims)
        //{

        //    var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_Jwt.Secret));

        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {
        //        Issuer = _configuration[_Jwt.ValidIssuer],
        //        Audience = _configuration[_Jwt.ValidAudience],
        //        Expires = DateTime.UtcNow.AddHours(3),
        //        SigningCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256),
        //        Subject = new ClaimsIdentity(claims)
        //    };

        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var token = tokenHandler.CreateToken(tokenDescriptor);
        //    return tokenHandler.WriteToken(token);
        //}
    }
}

