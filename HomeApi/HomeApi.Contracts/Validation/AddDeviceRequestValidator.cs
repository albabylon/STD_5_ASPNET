﻿using FluentValidation;
using HomeApi.Contracts.Devices;
using System.Linq;

namespace HomeApi.Contracts.Validation
{
    /// <summary>
    /// Класс-валидатор запросов подключения (называется в соотвествие с именем модели)
    /// </summary>
    public class AddDeviceRequestValidator : AbstractValidator<AddDeviceRequest>
    {
        string[] _validLocations;

        /// <summary>
        /// Метод, конструктор, устанавливающий правила
        /// </summary>
        public AddDeviceRequestValidator()
        {
            // Сохраним список допустимых вариантов размещения устройств
            _validLocations = new[]
            {
               "Kitchen",
               "Bathroom",
               "Livingroom",
               "Toilet"
            };

            /* Зададим правила валидации */
            RuleFor(x => x.Name).NotEmpty(); // Проверим на null и на пустое свойство
            RuleFor(x => x.Manufacturer).NotEmpty();
            RuleFor(x => x.Model).NotEmpty();
            RuleFor(x => x.SerialNumber).NotEmpty();
            RuleFor(x => x.CurrentVolts).NotEmpty().InclusiveBetween(120, 220); // Проверим, что значение в заданном диапазоне
            RuleFor(x => x.GasUsage).NotNull();
            RuleFor(x => x.Location).NotEmpty().Must(BeSupported).WithMessage($"Please choose one of the following locations: {string.Join(", ", _validLocations)}");
        }

        /// <summary>
        ///  Метод кастомной валидации для свойства location
        /// </summary>
        private bool BeSupported(string location)
        {
            // Проверим, содержится ли значение в списке допустимых
            return _validLocations.Any(e => e == location);
        }
    }
}
