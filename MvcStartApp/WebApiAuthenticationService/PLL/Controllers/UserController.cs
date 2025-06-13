using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Authentication;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApiAuthenticationService.BLL.Models;
using WebApiAuthenticationService.BLL.ViewModels;
using WebApiAuthenticationService.DAL;
using WebApiAuthenticationService.PLL.Exceptions;
using WebApiAuthenticationService.PLL.Logging;

namespace WebApiAuthenticationService.PLL.Controllers
{
    [ExceptionHandler] //подключение фильтра исключений
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase //поменяли Controller на ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public UserController(ILogger logger, IMapper mapper, IUserRepository userRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _userRepository = userRepository;
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

        [Authorize(Roles = "Администратор")] //ограничиваем по роли
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

        // метод HttpPost, в тело которого поместим введённый логин и пароль пользователя.
        [HttpPost]
        [Route("authenticate")]
        public async Task<UserViewModel> Authenticate(string login, string password)
        {
            // валидация введенных значений
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
                throw new ArgumentNullException("Запрос не корректен");

            // возвращает либо пользователя, если он был найден, либо значение null, если пользователь не найден
            User user = _userRepository.GetByLogin(login);

            // если пользователь не найден, мы выбросим исключение
            if (user is null)
                throw new AuthenticationException("Пользователь на найден");

            // проверка парол
            if (user.Password != password)
                throw new AuthenticationException("Введенный пароль не корректен");

            // Claim данные о пользователе, который авторизован в приложении
            var claims = new List<Claim>()
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login), //для аутентификации
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.Name), //для авторизации
            };

            // Хранит все Claim-сы
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(
                claims,
                "AppCookie", //в HttpContext можно найти по этому имени
                ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);

            // вход в систему с помощью куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            // возвращение значения после маппинга модели домена в модель представления
            return _mapper.Map<UserViewModel>(user);
        }
    }
}
