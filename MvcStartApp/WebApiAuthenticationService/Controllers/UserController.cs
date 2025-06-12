using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;

namespace WebApiAuthenticationService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase //поменяли Controller на ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRep;

        public UserController(ILogger logger, IMapper mapper, IUserRepository userRepo)
        {
            _logger = logger;
            _mapper = mapper;
            _userRep = userRepo;

            logger.WriteEvent("Сообщение о событии в программе");
            logger.WriteError("Сообщение об ошибки в программе");
        }

        [HttpGet]
        public User GetUser()
        {
            return new User()
            {
                Id = Guid.NewGuid(),
                FirstName = "Ivan",
                LastName = "Abramov",
                Email = "abramov@gmail.com",
                Password = "12345",
                Login = "abram1337",
            };
        }

        [HttpGet]
        [Route("viewmodel")]
        public UserViewModel GetUserViewModel()
        {
            User user = new User()
            {
                Id = Guid.NewGuid(),
                FirstName = "Ivan",
                LastName = "Abramov",
                Email = "abramov@gmail.com",
                Password = "12345",
                Login = "abram1337",
            };

            //создание viewmodel при помощи mapper вместо UserViewModel userViewModel = new UserViewModel(user);
            var userViewModel = _mapper.Map<UserViewModel>(user);

            return userViewModel;
        }
    }
}
