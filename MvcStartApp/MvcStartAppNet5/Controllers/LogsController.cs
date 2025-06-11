using Microsoft.AspNetCore.Mvc;
using MvcStartAppNet5.Models.Db.Repository;
using System.Threading.Tasks;

namespace MvcStartAppNet5.Controllers
{
    public class LogsController : Controller
    {
        private readonly ILoggingRepository _repo;

        public LogsController(ILoggingRepository repo)
        {
            _repo = repo;
        }

        public async Task<IActionResult> Index()
        {
            var requests = await _repo.GetRequests();
            return View(requests);
        }
    }
}
