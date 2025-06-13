using AutoMapper;
using WebApiAuthenticationService.BLL.Models;
using WebApiAuthenticationService.BLL.ViewModels;

namespace WebApiAuthenticationService.PLL.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //мэпим user в userviewmodel
            //(если не ошибюсь если были бы ссылочные типы (классы...), то он бы сделал сам глубокое копирование
            //(в конфигурации мапера только что-то нужно сделать)
            CreateMap<User, UserViewModel>().ConstructUsing(v => new UserViewModel(v));
        }
    }
}
