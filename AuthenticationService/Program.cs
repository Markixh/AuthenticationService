using AuthenticationService.BLL.Services;
using AuthenticationService.DAL.Repositories;
using AuthenticationService.Exceptions;
using AutoMapper;

namespace AuthenticationService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var mapperConfig = new MapperConfiguration(v =>
            {
                v.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();

            // Add services to the container.
            builder.Services.AddSingleton(mapper);
            builder.Services.AddSingleton<BLL.Services.ILogger, Logger>();
            builder.Services.AddSingleton<IUserRepository, UserRepository>();
            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddAuthentication(options => options.DefaultScheme = "Cookies")
                .AddCookie("Cookies", options =>
                {
                    options.Events = new Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationEvents()
                    {
                        OnRedirectToLogin = redirectContex =>
                        {
                            redirectContex.HttpContext.Response.StatusCode = 401;
                            return Task.CompletedTask;
                        }
                    };
                });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthentication();
            app.UseLogMiddleware();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}