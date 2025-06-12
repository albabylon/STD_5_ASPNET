using System;
using System.Net.Mail;

namespace WebApiAuthenticationService
{
    // Есть модель User но у внешнего приложения, которое работает с нашей API,
    // появляется потребность в отображении следующей информации: полное имя пользователя, включая имя и фамилию, и принадлежность к РФ.
    // Естественно, модель нашу мы менять не будем, а вот дополнительную модель для отображения мы сделаем.Она и будет называться ViewModel.
    public class UserViewModel
    {
        public Guid Id { get; set; }

        public string FullName { get; set; }

        public bool FromRussia { get; set; } // находится ли пользователь в Российской Федерации

        
        public UserViewModel(User user)
        {
            Id = user.Id;
            FullName = GetFullName(user.FirstName, user.LastName);
            FromRussia = GetFromRussiaValue(user.Email);
        }


        public string GetFullName(string firstName, string lastName)
        {
            return String.Concat(firstName, " ", lastName);
        }

        public bool GetFromRussiaValue(string email)
        {
            MailAddress mailAddress = new MailAddress(email);

            if (mailAddress.Host.Contains(".ru"))
                return true;
            return false;
        }
    }
}
