using Microsoft.AspNetCore.Mvc;
using MvcStartAppNet5.Models.Db.Entities;
using MvcStartAppNet5.Models.Db.Repository;
using System.Threading.Tasks;

namespace MvcStartAppNet5.Controllers
{
    public class UsersController : Controller
    {
        private readonly IBlogRepository _repo;

        public UsersController(IBlogRepository repo)
        {
            _repo = repo;
        }

        // отдельная страничка, где посетители смогут просматривать всех авторов (то есть всех пользователей)
        public async Task<IActionResult> Index()
        {
            var authors = await _repo.GetUsers();
            return View(authors);
        }

        [HttpGet] // отображает форму
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost] // когда нажали отправить форму (заносит в бд и пишет результат)
        public async Task<IActionResult> Register(User newUser)
        {
            //await _repo.AddUser(newUser);
            //return Content($"Registration successful, {newUser.FirstName}");

            await _repo.AddUser(newUser);
            return View(newUser);
        }
    }
}
