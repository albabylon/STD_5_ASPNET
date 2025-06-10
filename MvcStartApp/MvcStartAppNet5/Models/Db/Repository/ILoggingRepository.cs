using MvcStartAppNet5.Models.Db.Entities;
using System.Threading.Tasks;

namespace MvcStartAppNet5.Models.Db.Repository
{
    public interface ILoggingRepository
    {
        Task AddRequest(Request request);

        Task<Request[]> GetRequests();
    }
}
