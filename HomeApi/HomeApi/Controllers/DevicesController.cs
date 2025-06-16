using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Linq;

namespace HomeApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DevicesController : ControllerBase
    {
        private readonly IHostEnvironment _env;

        public DevicesController(IHostEnvironment env)
        {
            _env = env;
        }

        /// <summary>
        /// Поиск и загрузка инструкции по использованию устройства
        /// </summary>
        //два атрибута с методами - так не надо делать
        [HttpGet] //по нему получим и сам файл и заголовки
        [HttpHead] //по нему получим только заголовки файла (чтобы не качать целиком файл)
        [Route("{manufacturer}")]
        public IActionResult GetManual([FromRoute] string manufacturer) //передача параметра из url (передает все что после .../Devices/)
        {
            var staticPath = Path.Combine(_env.ContentRootPath, "Static");
            var filePath = Directory
                .GetFiles(staticPath)
                .FirstOrDefault(f => f.Split("\\").Last().Split('.')[0] == manufacturer);

            if(string.IsNullOrEmpty(filePath))
                return StatusCode(404, $"Инструкция для производителя {manufacturer} не найденона сервере. Проверьте название!");

            string fileType = "application/pdf"; //свойства ответа для клиента (заголовки)
            string fileName = $"{manufacturer}.pdf"; //свойства ответа для клиента (заголовки)

            return PhysicalFile(filePath, fileType, fileName); //возвращает физический объект и проставляет заголовки для файла
        }
    }
}
