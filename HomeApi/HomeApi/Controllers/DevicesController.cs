using AutoMapper;
using HomeApi.Configuration;
using HomeApi.Contracts.Devices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System.IO;
using System.Linq;

namespace HomeApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DevicesController : ControllerBase
    {
        private readonly IHostEnvironment _env;
        private readonly IOptions<HomeOptions> _options;
        private readonly IMapper _mapper;

        public DevicesController(IHostEnvironment env, IOptions<HomeOptions> options, IMapper mapper)
        {
            _env = env;
            _options = options;
            _mapper = mapper;
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

            if (string.IsNullOrEmpty(filePath))
                return StatusCode(404, $"Инструкция для производителя {manufacturer} не найденона сервере. Проверьте название!");

            string fileType = "application/pdf"; //свойства ответа для клиента (заголовки)
            string fileName = $"{manufacturer}.pdf"; //свойства ответа для клиента (заголовки)

            return PhysicalFile(filePath, fileType, fileName); //возвращает физический объект и проставляет заголовки для файла
        }

        /// <summary>
        /// Просмотр списка подключенных устройств
        /// </summary>
        [HttpGet]
        [Route("")]
        public IActionResult Get()
        {
            return StatusCode(200, "Устройства отсутствуют");
        }

        /// <summary>
        /// Добавление нового устройства
        /// </summary>
        [HttpPost]
        [Route("Add")]
        public IActionResult Add([FromBody] AddDeviceRequest request) // Атрибут, указывающий, откуда брать значение объекта и Объект запроса
        {
            //if (request.CurrentVolts < 120)
            //{
            //    // Добавляем для клиента информативную ошибку (валидация как через атрибут в модели)
            //    ModelState.AddModelError("currentVolts", "Устройства с напряжением меньше 120 вольт не поддерживаются!");
            //    return BadRequest(ModelState);
            //}

            //валидация через fluentvalidator (AddDeviceRequestValidator)

            return StatusCode(200, $"Устройство {request.Name} добавлено!");
        }
    }
}
