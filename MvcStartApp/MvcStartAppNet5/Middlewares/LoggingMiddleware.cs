using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System;
using System.IO;
using MvcStartAppNet5.Models.Db.Repository;
using MvcStartAppNet5.Models.Db.Entities;

namespace MvcStartAppNet5.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private ILoggingRepository _logRepo;

        /// <summary>
        ///  Middleware-компонент должен иметь конструктор, принимающий RequestDelegate
        /// </summary>
        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        ///  Необходимо реализовать метод Invoke  или InvokeAsync
        /// </summary>
        public async Task InvokeAsync(HttpContext context, ILoggingRepository logRepo)
        {
            _logRepo = logRepo;

            //логирование в консоль
            LogConsole(context);

            //логирование в файл
            //await LogFile(context);

            //логирование в базу
            await LogDb(context);

            // Передача запроса далее по конвейеру
            await _next.Invoke(context);
        }

        private async Task LogDb(HttpContext context)
        {
            var request = new Request
            {
                Date = DateTime.Now,
                Id = Guid.NewGuid(),
                Url = $"http://{context.Request.Host.Value + context.Request.Path}"
            };

            await _logRepo.AddRequest(request);
        }

        private void LogConsole(HttpContext context)
        {
            // Для логирования данных о запросе используем свойста объекта HttpContext
            Console.WriteLine($"[{DateTime.Now}]: New request to http://{context.Request.Host.Value + context.Request.Path}");
        }

        private async Task LogFile(HttpContext context)
        {
            // Строка для публикации в лог
            string logMessage = $"[{DateTime.Now}]: New request to http://{context.Request.Host.Value + context.Request.Path}{Environment.NewLine}";

            // Путь до лога (опять-таки, используем свойства IWebHostEnvironment)
            string logFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Logs", "RequestLog.txt");

            // Используем асинхронную запись в файл
            await File.AppendAllTextAsync(logFilePath, logMessage);
        }
    }
}
