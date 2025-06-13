using System.Collections.Generic;
using WebApiAuthenticationService.BLL.Models;

namespace WebApiAuthenticationService.DAL
{
    public interface IUserRepository
    {
        public IEnumerable<User> GetAll();

        public User GetByLogin(string login);
    }
}
