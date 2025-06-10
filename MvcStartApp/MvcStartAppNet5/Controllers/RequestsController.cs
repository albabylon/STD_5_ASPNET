using Microsoft.AspNetCore.Mvc;
using MvcStartAppNet5.Models.Db.Repository;

namespace MvcStartAppNet5.Controllers
{
    public class RequestsController : Controller
    {
        private readonly ILoggingRepository _repo;

        public RequestsController(ILoggingRepository repo)
        {
            _repo = repo;
        }

        public IActionResult Index()
        {
            var requests = _repo.GetRequests();
            return View(requests);
        }
    }
}
