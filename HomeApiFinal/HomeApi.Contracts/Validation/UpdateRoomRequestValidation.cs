using FluentValidation;
using HomeApi.Contracts.Models.Rooms;

namespace HomeApi.Contracts.Validation
{
    /// <summary>
    /// Класс-валидатор запросов обновления комнаты
    /// </summary>
    public class UpdateRoomRequestValidation : AbstractValidator<UpdateRoomRequest>
    {
        public UpdateRoomRequestValidation()
        {
            RuleFor(x => x.NewName).NotEmpty();
            RuleFor(x => x.NewVoltage).NotEmpty().InclusiveBetween(120, 220);
            RuleFor(x => x.NewArea).NotEmpty().Must((a) => a > 0).WithMessage($"Площадь помещения должна быть больше 0");
            RuleFor(x => x.NewGasConnected);
        }
    }
}
