using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Threading.Tasks;

namespace CoreVSAppNet5
{
    public class Program
    {
        /// <summary>
        ///  Точка входа - метод Main
        /// </summary>
        public static void Main(string[] args)
        {
            //Для запуска Core-приложения нам необходим хост, в роли которого выступает объект IHost. Его мы можем получить, вызвав в методе.
            //Далее у хоста IHost вызываются методы Build() и Run() уже для запуска приложения
            CreateHostBuilder(args).Build().Run();
            //Теперь наше приложение запущено, и сервер слушает HTTP-запросы.
            //Каким образом это происходит, можно проследить в классе Startup.cs.
            //Там описывается основная логика подключения внутренних сервисов, а также обслуживания HTTP-запросов по определённым маршрутам. 
        }

        /// <summary>
        /// Статический метод, создающий и настраивающий IHostBuilder -
        /// объект, который в свою очередь создает хост для развертывания нашего приложения
        /// </summary>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    // Переопределяем путь до статических файлов по умолчанию
                    webBuilder.UseWebRoot("Views");
                });

        //Основные процессы, происходящие Host.CreateDefaultBuilder(args):
        //- Устанавливается корневая папка - каталог приложения, где при сборке проекта будет осуществляться поиск файлов проекта(например, веб - страниц для отображения).
        //- Подгружаются переменные среды и аргументы командной строки.
        //- Загружается конфигурация приложения из файлов appsettings.json.
        //- Подключается механизм логирования.

        //ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
        //- В конфигурацию загружаются переменные среды (типан настройки стартовые). Пример из нашего appsettings.json: "ASPNETCORE_ENVIRONMENT": "Development"
        //- Запускается тот самый кроссплатформенный сервер Kestrel, на котором будет развёрнуто приложение.
        //- Добавляется компонент Host Filtering, позволяющий настраивать для Kestrel веб-адреса.
        //- Если приложение нужно развернуть в Windows-окружении на IIS, то здесь также выполняется интеграция с IIS.

        //Дальше вызовом метода webBuilder.UseStartup<Startup>() будет определён и подключён класс Startup,
        //в котором непосредственно запускаются настраиваемые сервисы.
    }
}
