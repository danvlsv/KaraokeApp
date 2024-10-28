namespace BlazorApp.Backend
{
    public class Booking
    {
        public int Id { get; set; }       // Уникальный идентификатор брони
        public int RoomNumber { get; set; }  // Забронированная комната
        public string Date { get; set; } // Дата
        public int Time { get; set; } // Время
        public string Name { get; set; } // Имя
        public string Phone {  get; set; } // Телефон
        public string Extra { get; set; } = string.Empty; // Дополнительная информация
        public bool Status { get; set; } = false; // Статус брони принят/нет

    }
}
