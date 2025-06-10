using Microsoft.AspNetCore.Mvc;
using MvcStartAppNet5.Models;
using System.Diagnostics;

namespace MvcStartAppNet5.Controllers
{
    public class FeedbackController : Controller
    {
        /// <summary>
        ///  Метод, возвращающий страницу с отзывами (срабатывает тогда, когда пользователь заходит на страницу с отзывами)
        /// </summary>
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        /// <summary>
        /// Метод для Ajax-запросов (возвращает строку с HTTP-кодом)
        /// </summary>
        [HttpPost]
        public IActionResult Add(Feedback feedback)
        {
            return StatusCode(200, $"{feedback.From}, спасибо за отзыв!");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
