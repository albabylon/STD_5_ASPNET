using System.ComponentModel.DataAnnotations;

namespace HomeApi.Contracts.Devices
{
    /// <summary>
    /// Добавляет поддержку нового устройства.
    /// </summary>
    public class AddDeviceRequest
    {
        [Required] // Указываем все параметры как обязательные
        public string Name { get; set; }
        
        [Required]
        public string Manufacturer { get; set; }
        
        [Required]
        public string Model { get; set; }
        
        [Required]
        public string SerialNumber { get; set; }
        
        [Required] 
        [Range(120, 220, ErrorMessage = "Поддерживаются устройства с напряжением от {1} до {2} вольт")] // Указываем допустимый диапазон значений и даже текст ошибки в случае нарушения
        public int CurrentVolts { get; set; }
        
        [Required]
        public bool GasUsage { get; set; }
        
        [Required]
        public string Location { get; set; }
    }

    //Кстати, если у вас уже есть валидация на атрибутах, но для какого-либо из свойств вы дополнительно хотите добавить ручную валидацию 
    //— правильным будет возвращать текст с ошибками в том же формате, в каком он возвращается при валидации на атрибутах
    //Сделать это можно, используя объект ModelState.
}
