//Для моделей запросов/ответов сделаем отдельную директорию, и назовём её Contracts
namespace HomeApi.Contracts.Home
{
    //InfoResponse очень похожа на наш объект HomeOptions и на первый взгляд нам не нужна.
    //Почему бы нам просто не использовать здесь объект HomeOptions, преобразовывая его в JSON и возвращая пользователю?
    //Но это было бы нарушениям принципа единства ответственности — класс должен служить только одной цели.
    //Поэтому, если ваши внутренние модели содержат как раз те данные, которые нужно вернуть клиенту, использовать их для этого всё равно нельзя.
    //Для представления запросов — ответов уместно будет использовать отдельные модели, здесь мы их расположим в папке Contracts.

    //Для удобства все классы запросов и ответов по возможности должны заканчиваться на слово ~Request (для запросов), или ~Response (для ответов).
    /// <summary>
    /// Информация о вашем доме (модель ответа)
    /// </summary>
    public class InfoResponse
    {
        public int FloorAmount { get; set; }
        public string Telephone { get; set; }
        public string Heating { get; set; }
        public int CurrentVolts { get; set; }
        public bool GasConnected { get; set; }
        public int Area { get; set; }
        public string Material { get; set; }
        public AddressInfo AddressInfo { get; set; }
    }

    public class AddressInfo
    {
        public int House { get; set; }
        public int Building { get; set; }
        public string Street { get; set; }
    }
}
