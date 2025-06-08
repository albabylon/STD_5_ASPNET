using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreVSAppNet5
{

    //Класс Startup должен определять два метода:
    //Configure
    //ConfigureServices(опционально)
    public class Startup
    {

        //Метод-конструктор
        //При желании мы можем вызывать в классе Startup метод-конструктор.
        //К примеру, если мы добавим следующий код. Мы сможем пробрасывать и сохранять переменные среды.
        IWebHostEnvironment _env;

        public Startup(IWebHostEnvironment env)
        {
            _env = env;
        }


        // Метод вызывается средой ASP.NET.
        // Используйте его для подключения сервисов приложения
        // Документация:  https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //Здесь происходит регистрация сервисов приложения, которые мы получаем из объекта IServiceCollecton.
            //Сервисы могут регистрироваться с помощью методов-расширений.
            //Например:
            //services.AddMvc(); - если бы мы хотели добавить функционал MVC-приложения
            //services.AddAuthentication(); - если бы мы при создании добавили функционал авторизации
        }

        // Метод вызывается средой ASP.NET.
        // Используйте его для настройки конвейера запросов
        // Этот метод должен быть всегда, и он использует объект IApplicationBuilder для установки обработчиков запроса
        // IWebHostEnvironment содержит информацию о среде запуска приложения и позволяет с ней взаимодействовать.
        // В нашем пустом проекте в методе Configure также происходит и непосредственно обработка запроса
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Проверим, проходит ли запуск в тестовой среде
            // Мы можем задать разное поведение нашего приложения при запуске,
            // в зависимости от того, происходит ли запуск в боевом или тестовом окружении
            //Development — разработческое, при котором происходит программирование и отладка проекта, как правило, используются тестовые данные. Так происходит, к примеру, когда вы запускаете проект из Visual Studio.
            //Testing — тестовое, при котором проект размещается на так называемом «тестовом стенде» — сервере с ненастоящими данными для первичной отладки.
            //Staging — размещение объекта на сервере с конфигурацией и данными, максимально приближенными к реальным, для финального тестирования.
            //Production — так называемая «боевая» среда.Приложение размещается на реальном сервере для взаимодействия с реальными пользователями.
            
            if (env.IsDevelopment() || env.IsStaging())
            {
                // 1. Добавляем компонент, отвечающий за диагностику ошибок (компонент для обработки ошибок — Diagnostics)
                app.UseDeveloperExceptionPage(); //для среды Development будет вызвано это и позволит нам увидеть подробный отчёт об ошибках //и для Staging
            }

            // 2. Добавляем компонент, отвечающий за маршрутизацию (компонент маршрутизации EndpointRoutingMiddleware)
            //Настраиваем маршрутизацию
            //Здесь наше приложение получает возможность назначать входящие запросы определённым маршрутам
            app.UseRouting();


            //Метод Use - тоже позволяет добавить компоненты Middleware в конвейер, но приводит к вызову следующего компонента
            //Перегрузка метода может передавать обработку следующему компоненту в конвейере.
            //Для этого можно использовать перегрузку метода Use, принимающую в качестве параметров контекст запроса HttpContext,
            //и делегат, ссылающийся на следующий компонент в конвейере
            //Добавляем компонент для логирования запросов с использованием метода Use.
            //app.Use(async (context, next) =>
            //{
            //    // Для логирования данных о запросе используем свойства объекта HttpContext
            //    Console.WriteLine($"[{DateTime.Now}]: New request to http://{context.Request.Host.Value + context.Request.Path}");
            //    await next.Invoke();
            //});

            //Метод Map - (а также методы-расширения, начинающиеся с этого слова) сопоставляют путь запроса с делегатом — обработчиком.
            //app.Map("/about", About);

            // Метод Run — это самый простой способ добавления компонента Middleware
            // Добавим в конвейер запросов обработчик самым простым способом
            // Компонент, добавленный таким образом, препятствует передаче запроса далее по конвейеру, то есть обработка на нём прекратится.
            // (UseEndpoints() не увидим, если  перенесем за UseEndpoints(), то увидим)
            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync($"Welcome to the {env.ApplicationName}!");
            //});


            // 3. Добавляем компонент с настройкой маршрутов (EndpointMiddleware, который перехватывает запрос, проверяет его на соответствие определённому паттерну)
            //Здесь определит маршруты, обрабатываемые приложением
            //Тут определено, что все входящие запросы по маршруту "/", то есть непосредственно на главную страницу нашего приложения,
            //получат в ответ строку "Hello World!"
            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapGet("/config", async context =>
            //    {
            //        await context.Response.WriteAsync($"Hello World! {env.EnvironmentName}");
            //    });
            //});


            //Добавляем компонент с настройкой маршрутов для главной страницы
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync($"Welcome to the {env.ApplicationName}!");
                });
            });

            // Все прочие страницы имеют отдельные обработчики
            app.Map("/about", About);
            app.Map("/config", Config);

            // Обработчик для ошибки "страница не найдена"
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync($"Page not found");
            });
        }

        /// <summary>
        ///  Обработчик для страницы About
        /// </summary>
        private void About(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                await context.Response.WriteAsync($"{_env.ApplicationName} - ASP.Net Core tutorial project");
            });
        }

        /// <summary>
        ///  Обработчик для главной страницы
        /// </summary>
        private void Config(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                await context.Response.WriteAsync($"App name: {_env.ApplicationName}. App running configuration: {env.EnvironmentName}");
            });
        }
    }
}
