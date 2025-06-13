using System;

namespace WebApiAuthenticationService.PLL.Exceptions
{
    public class CustomException : Exception
    {
        public CustomException(string message) : base(message)
        {

        }
    }
}
