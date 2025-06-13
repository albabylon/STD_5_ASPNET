using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApiAuthenticationService.PLL.Exceptions
{
    // фильтр исключений
    public class ExceptionHandler : ActionFilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            string message = "Произошла непредвиденная ошибка. Администрация сайта спешит на помощь";

            if (context.Exception is CustomException)
            {
                message = context.Exception.Message;
            }

            context.Result = new BadRequestObjectResult(message);
        }
    }
}

//для каждого типа исключений, существует свой интерфейс, который должен быть реализован в пользовательском типе:
//Фильтр действий - IActionFilter -	Используется для добавления дополнительной логики до или после выполнения методов действия.
//Фильтр аутентификации - IAuthenticationFilter - Используется для принудительной аутентификации пользователей или клиентов перед выполнением методов действия.
//Фильтр авторизации - IAuthorizationFilter - Используется для ограничения доступа к методам действий для определенных пользователей или групп.
//Фильтр исключений - IExceptionFilter - Используется для обработки всех необработанных исключений в веб-API.
//Фильтр для переопределения - IOverrideFilter - Используется для переопределения другого фильтра.
