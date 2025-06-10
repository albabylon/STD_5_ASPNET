using MvcStartAppNet5.Models.Db.Entities;
using System.Threading.Tasks;

namespace MvcStartAppNet5.Models.Db.Repository
{
    public interface IBlogRepository
    {
        Task AddUser(User user);

        Task<User[]> GetUsers();
    }
}
