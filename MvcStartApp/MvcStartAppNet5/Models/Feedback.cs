namespace MvcStartAppNet5.Models
{
    //Можно привязать к ней контекст и сохранять в базу при необходимости (но нам нужна для ajax)
    //Это у нас будет чисто внутренняя модель данных в приложении
    public class Feedback
    {
        public string From { get; set; }
        public string Text { get; set; }
    }
}
