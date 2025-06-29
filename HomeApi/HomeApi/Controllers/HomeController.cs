﻿using HomeApi.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Text;
using System;
using AutoMapper;
using HomeApi.Contracts.Home;

namespace HomeApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        // Ссылка на объект конфигурации
        private IOptions<HomeOptions> _options;
        private IMapper _mapper;

        // Инициализация конфигурации при вызове конструктора
        public HomeController(IOptions<HomeOptions> options, IMapper mapper)
        {
            _options = options;
            _mapper = mapper;
        }

        /// <summary>
        /// Метод для получения информации о доме
        /// </summary>
        [HttpGet] // Для обслуживания Get-запросов
        [Route("info")] // Настройка маршрута с помощью атрибутов
        public IActionResult Info()
        {
            //// Объект Stringbuilder, в который будем "собирать" результат из конфигурации
            //var pageResult = new StringBuilder();

            //// Проставляем все значения из конфигурации для последующего вывода на страницу
            //pageResult.Append($"Добро пожаловать в API вашего дома!{Environment.NewLine}");
            //pageResult.Append($"Здесь вы можете посмотреть основную информацию.{Environment.NewLine}"); 
            //pageResult.Append($"{Environment.NewLine}"); 
            //pageResult.Append($"Количество этажей:         {_options.Value.FloorAmount}{Environment.NewLine}"); 
            //pageResult.Append($"Стационарный телефон:      {_options.Value.Telephone}{Environment.NewLine}"); 
            //pageResult.Append($"Тип отопления:             {_options.Value.Heating}{Environment.NewLine}"); 
            //pageResult.Append($"Напряжение электросети:    {_options.Value.CurrentVolts}{Environment.NewLine}"); 
            //pageResult.Append($"Подключен к газовой сети:  {_options.Value.GasConnected}{Environment.NewLine}"); 
            //pageResult.Append($"Жилая площадь:             {_options.Value.Area} м2{Environment.NewLine}"); 
            //pageResult.Append($"Материал:                  {_options.Value.Material}{Environment.NewLine}"); 
            //pageResult.Append($"{Environment.NewLine}");
            //pageResult.Append(@$"Адрес:                     {_options.Value.Address.Street} {_options.Value.Address.House}/{_options.Value.Address.Building}{Environment.NewLine}");

            //// Преобразуем результат в строку и выводим, как обычную веб-страницу
            //return StatusCode(200, pageResult.ToString());

            // Получим запрос, "смапив" конфигурацию на модель запроса
            var infoResponse = _mapper.Map<HomeOptions, InfoResponse>(_options.Value);
            // Вернём ответ
            return StatusCode(200, infoResponse);
        }
    }
}
