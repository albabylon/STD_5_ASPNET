using AutoMapper;

namespace WebApiAuthenticationService
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            //мэпим user в userviewmodel
            CreateMap<User, UserViewModel>().ConstructUsing(v => new UserViewModel(v));
        }
    }
}
