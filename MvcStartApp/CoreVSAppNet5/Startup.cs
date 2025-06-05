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
                app.UseDeveloperExceptionPage(); //для среды Development будет вызвано это и позволит нам увидеть подробный отчёт об ошибках //и для Staging
            }

            //Настраиваем маршрутизацию
            //Здесь наше приложение получает возможность назначать входящие запросы определённым маршрутам
            app.UseRouting();

            //Здесь определит маршруты, обрабатываемые приложением
            //Тут определено, что все входящие запросы по маршруту "/", то есть непосредственно на главную страницу нашего приложения,
            //получат в ответ строку "Hello World!"
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync($"Hello World! {env.EnvironmentName}");
                });
            });
        }
    }

    //Метод-конструктор
    //При желании мы можем вызывать в классе Startup метод-конструктор.
    //К примеру, если мы добавим следующий код. Мы сможем пробрасывать и сохранять переменные среды.

    //IWebHostEnvironment _env;
    //public Startup(IWebHostEnvironment env)
    //{
    //    _env = env;
    //}
}
