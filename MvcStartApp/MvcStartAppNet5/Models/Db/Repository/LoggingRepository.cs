using Microsoft.EntityFrameworkCore;
using MvcStartAppNet5.Models.Db.Entities;
using System.Threading.Tasks;

namespace MvcStartAppNet5.Models.Db.Repository
{
    public class LoggingRepository : ILoggingRepository
    {
        private readonly BlogContext _context;

        public LoggingRepository(BlogContext context)
        {
            _context = context;
        }


        public async Task AddRequest(Request request)
        {
            var entry = _context.Entry(request);
            if (entry.State == EntityState.Detached)
                await _context.Requests.AddAsync(request);

            await _context.SaveChangesAsync();
        }

        public async Task<Request[]> GetRequests()
        {
            return await _context.Requests.ToArrayAsync();
        }
    }
}
