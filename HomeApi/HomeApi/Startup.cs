using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using HomeApi.Configuration;
using HomeApi.Mapping;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using AutoMapper;

namespace HomeApi
{
    public class Startup
    {
        // �� ����� ��� ��� ������� � private IConfiguration Configuration
        //public Startup(IConfiguration configuration)
        //{
        //    Configuration = configuration;
        //}

        // ����� IOptions ����� ���������� ��� ������ �����������, ������, ����� ������� � �� ��� ������
        /// <summary>
        /// �������� ������������ �� ����� Json
        /// </summary>
        private IConfiguration Configuration { get; } = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddJsonFile("appsettings.Development.json")
            .AddJsonFile("HomeOptions.json")
            .Build();


        public void ConfigureServices(IServiceCollection services)
        {
            // ���������� ������ IOptions
            services.Configure<HomeOptions>(Configuration);
            // ����� ��� ��� �������� ���������
            //services.Configure<HomeOptions>(opt =>
            //{
            //    opt.Area = 120;
            //});
            // ����� ����� ��������� ������. ��������� ������ ����� (��������� Json-������) 
            //services.Configure<Address>(Configuration.GetSection("Address"));

            // ���������� ����������� - ������ ������� ����� ����� AutoMapper.Extensions.Microsoft.DependencyInjection
            //var assembly = Assembly.GetAssembly(typeof(MappingProfile));
            //services.AddAutoMapper(assembly);

            // ���������� �����������
            var mapperConfig = new MapperConfiguration((v) =>
            {
                v.AddProfile(new MappingProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            // ��� �� ����� �������������, �� � MVC �� ����� ������ AddControllersWithViews()
            services.AddControllers();
            
            // ������������ �������������� ��������� ������������ WebApi � �������������� Swagger
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "HomeApi", Version = "v1" }); });
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // ����������� ����������� ��� ������� ��� ���������� ��������
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HomeApi v1"));
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            // ������������ �������� � �������������
            app.UseEndpoints(endpoints =>
            {
                // ������������ ��� ��������������� ������������� ��������� � �������������.
                // ������ ��� ��� ������������� ������� ���������� ��������.
                endpoints.MapControllers();
            });
        }
    }
}
