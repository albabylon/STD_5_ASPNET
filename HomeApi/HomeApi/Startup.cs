using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeApi.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace HomeApi
{
    public class Startup
    {
        // не нужна так как задаетс в private IConfiguration Configuration
        //public Startup(IConfiguration configuration)
        //{
        //    Configuration = configuration;
        //}

        // Через IOptions можно передавать ему строки подключения, адреса, ключи доступа и всё что угодно
        /// <summary>
        /// Загрузка конфигурации из файла Json
        /// </summary>
        private IConfiguration Configuration { get; } = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddJsonFile("appsettings.Development.json")
            .AddJsonFile("HomeOptions.json")
            .Build();


        public void ConfigureServices(IServiceCollection services)
        {
            // Подключить сервис IOptions
            services.Configure<HomeOptions>(Configuration);
            // Можно еще так изменять настройки
            //services.Configure<HomeOptions>(opt =>
            //{
            //    opt.Area = 120;
            //});
            // Можно часть загрузить только. Загружаем только адрес (вложенный Json-объект) 
            //services.Configure<Address>(Configuration.GetSection("Address"));

            // Нам не нужны представления, но в MVC бы здесь стояло AddControllersWithViews()
            services.AddControllers();
            
            // поддерживает автоматическую генерацию документации WebApi с использованием Swagger
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "HomeApi", Version = "v1" }); });
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Проставляем специфичные для запуска при разработке свойства
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HomeApi v1"));
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            // Сопоставляем маршруты с контроллерами
            app.UseEndpoints(endpoints =>
            {
                // Используется для автоматического сопоставления маршрутов с контроллерами.
                // Теперь нам нет необходимости вручную определять маршруты.
                endpoints.MapControllers();
            });
        }
    }
}
