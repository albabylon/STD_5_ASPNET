using System.Collections.Generic;

namespace WebApiAuthenticationService
{
    public interface IUserRepository
    {
        public IEnumerable<User> GetAll();

        public User GetByLogin(string login);
    }
}
