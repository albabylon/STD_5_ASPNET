using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Threading.Tasks;
using WebApiAuthenticationService.DAL;
using WebApiAuthenticationService.PLL.Logging;
using WebApiAuthenticationService.PLL.MappingProfiles;
using WebApiAuthenticationService.PLL.Middlewares;

namespace WebApiAuthenticationService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //DI Logger
            services.AddSingleton<ILogger, Logger>();
            //DI Repos
            services.AddSingleton<IUserRepository, UserRepository>();

            //Создание маппера (мэпинг доменной модели и модели представления и DI ее
            var mapperConfig = new MapperConfiguration((v) =>
            {
                v.AddProfile(new MappingProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AuthenticationService", Version = "v1" });
            });

            // настройка аутентификации с cookie
            // если юзер не прошел аутентификацию будет ввозвращаться ошибка 401
            services
                .AddAuthentication(options => options.DefaultScheme = "Cookies")
                .AddCookie("Cookies", options =>
                {
                    options.Events = new Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationEvents
                    {
                        OnRedirectToLogin = redirectContext =>
                        {
                            redirectContext.HttpContext.Response.StatusCode = 401;
                            return Task.CompletedTask;
                        }
                    };
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AuthenticationService v1"));
            }

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            // доавбление мидлвейр через собственный класс расширения для его добавления
            app.UseLogMiddleware();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
