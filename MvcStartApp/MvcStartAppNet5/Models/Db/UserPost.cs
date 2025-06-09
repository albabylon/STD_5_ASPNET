using System;

namespace MvcStartAppNet5.Models.Db
{
    //Чтобы отделить модели, предназначенные именно для хранения в базе данных, можем поместить их в подпапку Db.
    
    /// <summary>
    ///  Модель поста в блоге
    /// </summary>
    public class UserPost
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }

        // UserId представляет внешний ключ — указатель на модель User
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
