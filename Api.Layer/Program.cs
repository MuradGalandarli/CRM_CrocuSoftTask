
using Business.Layer.Abstract;
using Business.Layer.Concret;
using Business.Layer.Validator;
using DataAccess.Layer;
using DataAccess.Layer.Abstract;
using DataAccess.Layer.Concret;
using DataTransferObject.RequestDto;
using Entity.Layer;
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
            /*
                        builder.Services.AddDbContext<ApplicationContext>(options =>
                        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

                        builder.Services.AddDefaultIdentity<IdentityUser>(options =>
                options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationContext>();
            */


           var jwt = builder.Configuration.GetSection("JWT").Get<JWT>();
        
            builder.Services.AddScoped<IAuthService,AuthManager>();

            builder.Services.AddSingleton<ILoggerProvider, NLogLoggerProvider>();

            builder.Services.AddScoped<IProject, EFRepositoryProject>();
            builder.Services.AddScoped<IProjectService, ProjectManager>();
            builder.Services.AddScoped<IReport, EFRepositoryReport>();
            builder.Services.AddScoped<IReportService, ReportManager>();
            builder.Services.AddScoped<ITeam, EFRepositoryTeam>();
            builder.Services.AddScoped<ITeamService, TeamManager>();

            
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            builder.Services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddIdentity<IdentityUser,IdentityRole>(options =>
                 options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationContext>();



          /*  builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("conn")));

            // For Identity  
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                            .AddEntityFrameworkStores<ApplicationContext>()
                            .AddDefaultTokenProviders();*/
            // Adding Authentication  
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })

                        // Adding Jwt Bearer  
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
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
