using CoreVSAppNet5.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CoreVSAppNet5
{

    //����� Startup ������ ���������� ��� ������:
    //Configure
    //ConfigureServices(�����������)
    public class Startup
    {

        //�����-�����������
        //��� ������� �� ����� �������� � ������ Startup �����-�����������.
        //� �������, ���� �� ������� ��������� ���. �� ������ ������������ � ��������� ���������� �����.
        IWebHostEnvironment _env;

        public Startup(IWebHostEnvironment env)
        {
            _env = env;
        }


        // ����� ���������� ������ ASP.NET.
        // ����������� ��� ��� ����������� �������� ����������
        // ������������:  https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //����� ���������� ����������� �������� ����������, ������� �� �������� �� ������� IServiceCollecton.
            //������� ����� ���������������� � ������� �������-����������.
            //��������:
            //services.AddMvc(); - ���� �� �� ������ �������� ���������� MVC-����������
            //services.AddAuthentication(); - ���� �� �� ��� �������� �������� ���������� �����������
        }

        // ����� ���������� ������ ASP.NET.
        // ����������� ��� ��� ��������� ��������� ��������
        // ���� ����� ������ ���� ������, � �� ���������� ������ IApplicationBuilder ��� ��������� ������������ �������
        // IWebHostEnvironment �������� ���������� � ����� ������� ���������� � ��������� � ��� �����������������.
        // � ����� ������ ������� � ������ Configure ����� ���������� � ��������������� ��������� �������
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // ��������� ����������� ������
            app.UseStaticFiles();

            // ��������, �������� �� ������ � �������� �����
            // �� ����� ������ ������ ��������� ������ ���������� ��� �������,
            // � ����������� �� ����, ���������� �� ������ � ������ ��� �������� ���������
            //Development � ��������������, ��� ������� ���������� ���������������� � ������� �������, ��� �������, ������������ �������� ������. ��� ����������, � �������, ����� �� ���������� ������ �� Visual Studio.
            //Testing � ��������, ��� ������� ������ ����������� �� ��� ���������� ��������� ������ � ������� � ������������ ������� ��� ��������� �������.
            //Staging � ���������� ������� �� ������� � ������������� � �������, ����������� ������������� � ��������, ��� ���������� ������������.
            //Production � ��� ���������� �������� �����.���������� ����������� �� �������� ������� ��� �������������� � ��������� ��������������.

            if (env.IsDevelopment() || env.IsStaging())
            {
                // 1. ��������� ���������, ���������� �� ����������� ������ (��������� ��� ��������� ������ � Diagnostics)
                app.UseDeveloperExceptionPage(); //��� ����� Development ����� ������� ��� � �������� ��� ������� ��������� ����� �� ������� //� ��� Staging
            }

            // 2. ��������� ���������, ���������� �� ������������� (��������� ������������� EndpointRoutingMiddleware)
            //����������� �������������
            //����� ���� ���������� �������� ����������� ��������� �������� ������� ����������� ���������
            app.UseRouting();


            //����� Use - ���� ��������� �������� ���������� Middleware � ��������, �� �������� � ������ ���������� ����������
            //���������� ������ ����� ���������� ��������� ���������� ���������� � ���������.
            //��� ����� ����� ������������ ���������� ������ Use, ����������� � �������� ���������� �������� ������� HttpContext,
            //� �������, ����������� �� ��������� ��������� � ���������
            //��������� ��������� ��� ����������� �������� � �������������� ������ Use.
            //app.Use(async (context, next) =>
            //{
            //    // ��� ����������� ������ � ������� ���������� �������� ������� HttpContext
            //    Console.WriteLine($"[{DateTime.Now}]: New request to http://{context.Request.Host.Value + context.Request.Path}");
            //    await next.Invoke();
            //});

            //����� Map - (� ����� ������-����������, ������������ � ����� �����) ������������ ���� ������� � ��������� � ������������.
            //app.Map("/about", About);

            // ����� Run � ��� ����� ������� ������ ���������� ���������� Middleware
            // ������� � �������� �������� ���������� ����� ������� ��������
            // ���������, ����������� ����� �������, ������������ �������� ������� ����� �� ���������, �� ���� ��������� �� �� �����������.
            // (UseEndpoints() �� ������, ����  ��������� �� UseEndpoints(), �� ������)
            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync($"Welcome to the {env.ApplicationName}!");
            //});


            // 3. ��������� ��������� � ���������� ��������� (EndpointMiddleware, ������� ������������� ������, ��������� ��� �� ������������ ������������ ��������)
            //����� ��������� ��������, �������������� �����������
            //��� ����������, ��� ��� �������� ������� �� �������� "/", �� ���� ��������������� �� ������� �������� ������ ����������,
            //������� � ����� ������ "Hello World!"
            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapGet("/config", async context =>
            //    {
            //        await context.Response.WriteAsync($"Hello World! {env.EnvironmentName}");
            //    });
            //});


            //���������� ����� Use, ����� ������ ����������� ������ �� ���������
            //app.Use(async (context, next) =>
            //{
            //    // ������ ��� ���������� � ���
            //    string logMessage = $"[{DateTime.Now}]: New request to http://{context.Request.Host.Value + context.Request.Path}{Environment.NewLine}";

            //    // ���� �� ���� (���������� �������� IWebHostEnvironment)
            //    string logFilePath = Path.Combine(env.ContentRootPath, "Logs", "RequestLog.txt");

            //    // ���������� ����������� ������ � ����
            //    await File.AppendAllTextAsync(logFilePath, logMessage);

            //    await next.Invoke();
            //});

            // ���������� ����������� � �������������� �� �������������� ����
            app.UseMiddleware<LoggingMiddleware>();

            //��������� ��������� � ���������� ��������� ��� ������� ��������
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync($"Welcome to the {env.ApplicationName}!");
                });
            });

            // ��� ������ �������� ����� ��������� �����������
            app.Map("/about", About);
            app.Map("/config", Config);

            // ���������� ��� ������ "�������� �� �������"
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync($"Page not found");
            });
            //��� ���
            // ������������ ������ HTTP
            //app.UseStatusCodePages();

            //����������� ����� ��� � ����� �� ���������
            Console.WriteLine($"Launching project from: {env.ContentRootPath}");
        }

        /// <summary>
        ///  ���������� ��� �������� About
        /// </summary>
        private void About(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                await context.Response.WriteAsync($"{_env.ApplicationName} - ASP.Net Core tutorial project");
            });
        }

        /// <summary>
        ///  ���������� ��� ������� ��������
        /// </summary>
        private void Config(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                await context.Response.WriteAsync($"App name: {_env.ApplicationName}. App running configuration: {_env.EnvironmentName}");
            });
        }
    }
}
