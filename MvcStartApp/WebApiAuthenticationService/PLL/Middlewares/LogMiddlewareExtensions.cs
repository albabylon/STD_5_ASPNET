﻿using Microsoft.AspNetCore.Builder;

namespace WebApiAuthenticationService.PLL.Middlewares
{
    public static class LogMiddlewareExtensions
    {
        public static IApplicationBuilder UseLogMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LogMiddleware>();
        }
    }
}
