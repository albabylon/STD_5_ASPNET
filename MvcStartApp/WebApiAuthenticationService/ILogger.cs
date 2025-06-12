﻿using System.Threading.Tasks;

namespace WebApiAuthenticationService
{
    public interface ILogger
    {
        public void WriteEvent(string eventMessage);

        public void WriteError(string errorMessage);
    }
}
