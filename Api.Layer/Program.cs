using Business.Layer.Abstract;
using Business.Layer.Concret;
using Business.Layer.Validator;
using DataAccess.Layer;
using DataAccess.Layer.Abstract;
using DataAccess.Layer.Concret;
using DataTransferObject.DtoProfile;
using DataTransferObject.RequestDto;
using Entity.Layer.Entity;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NLog.Extensions.Logging;
using Shred.Layer.AuthModel;
using System.Text;

namespace Api.Layer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var jwt = builder.Configuration.GetSection("JWT").Get<JWT>();
            builder.Services.Configure<JWT>(builder.Configuration.GetSection("JWT"));

            builder.Services.AddScoped<IAuthService, AuthManager>();

            builder.Services.AddSingleton<ILoggerProvider, NLogLoggerProvider>();

            builder.Services.AddScoped<IProject, EFRepositoryProject>();
            builder.Services.AddScoped<IProjectService, ProjectManager>();
            builder.Services.AddScoped<IReport, EFRepositoryReport>();
            builder.Services.AddScoped<IReportService, ReportManager>();
            builder.Services.AddScoped<ITeam, EFRepositoryTeam>();
            builder.Services.AddScoped<ITeamService, TeamManager>();
            builder.Services.AddScoped<IUserService, UserManager>();

            builder.Services.AddScoped<Token>();

            builder.Services.AddAutoMapper(typeof(DriveProfile).Assembly);
            
            builder.Services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
                 options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationContext>();


                builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })

                        .AddJwtBearer(options =>
                        {
                            options.SaveToken = true;
                            options.RequireHttpsMetadata = false;
                            options.TokenValidationParameters = new TokenValidationParameters()
                            {
                                ValidateIssuer = true,
                                ValidateAudience = true,
                                ValidAudience = jwt.ValidAudience,
                                ValidIssuer = jwt.ValidIssuer,
                                ClockSkew = TimeSpan.Zero,
                                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Secret))
                            };
                        });


            builder.Services.AddControllers();

            builder.Services.AddScoped<IValidator<RequestProject>, ProjectValidator>();
            builder.Services.AddScoped<IValidator<RequestTeam>, TeamValidator>();
            builder.Services.AddScoped<IValidator<RequestReport>, ReportValidator>();
            builder.Services.AddScoped<IValidator<RegistrationModel>,RegistrationModelValidator>();
            builder.Services.AddScoped<IValidator<LoginModel>, LoginModelValidator>();
            builder.Services.AddScoped<IValidator<RequestUser>, RequestUserValidator>();
            builder.Services.AddScoped<IValidator<RequestUserUpdate>, RequestUserUpdateValidator>();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    policy =>
                    {
                        policy.AllowAnyOrigin()
                              .AllowAnyHeader()
                              .AllowAnyMethod();
                    });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            /*  if (app.Environment.IsDevelopment())
              { 
                  app.UseSwagger();
                  app.UseSwaggerUI();
              }*/
            if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                });
            }
            app.UseCors("AllowAll");
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
