namespace BlazorApp
{
    public class CurrentBooking
    {
        /*public int Id { get; private set; } */    // Уникальный идентификатор брони
		public int RoomNumber { get; private set; }  // Забронированная комната
		public string Date { get; private set; } // Дата
		public int? Time { get; private set; } // Время
		public string Name { get; private set; } // Имя
        public string Phone { get; private set; } // Телефон
        public string Extra { get; private set; } = string.Empty; // Дополнительная информация

        public event Action OnChange;

        public void SetRoomNumber(int room)
        {
            if (room != RoomNumber)
            {
                Date = null;
                Time = null;
            }
            RoomNumber = room;
            NotifyStateChanged();
        }

        public void SetDate(string date)
        {
            Date = date;
            NotifyStateChanged();
        }

        private void NotifyStateChanged()
        {
            OnChange?.Invoke();
        }
    }
}
