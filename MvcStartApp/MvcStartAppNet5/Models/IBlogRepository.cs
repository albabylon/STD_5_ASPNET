using MvcStartAppNet5.Models.Db;
using System.Threading.Tasks;

namespace MvcStartAppNet5.Models
{
    public interface IBlogRepository
    {
        Task AddUser(User user);

        Task<User[]> GetUsers();
    }
}
