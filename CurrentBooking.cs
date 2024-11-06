using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BlazorApp
{
    public class CurrentBooking
    {
        /*public int Id { get; private set; } */    // Уникальный идентификатор брони
		public int RoomNumber { get; private set; }  // Забронированная комната
		public string Date { get; private set; } // Дата
		public int? Time { get; private set; } // Время
        public string Name { get; private set; } = ""; // Имя
        public string Phone { get; private set; } = "";// Телефон
        public string Extra { get; private set; } // Дополнительная информация

        public event Action OnChange;

        public void SetRoomNumber(int room)
        {
            if (room != RoomNumber)
            {
                Date = null;
                Time = null;
				RoomNumber = room;
				NotifyStateChanged();
			}
            
        }

        public void SetDate(string date)
        {
            if (date!=Date)
            {
                Time = null;
				Date = date;
				NotifyStateChanged();
			}
            
        }

        public void SetTime(int? time)
        {
            Time= time;
            NotifyStateChanged();
        }

        public void ResetData()
        {
            RoomNumber = 0;
			Time = null;
			Date = null;
            Name = null;
            Phone = null;
            Extra = null;
		}

        private void NotifyStateChanged()
        {
            OnChange?.Invoke();
        }

        public void SetName(string name)
        {
            Name = name;
        }

		public void SetPhone(string phone)
		{
			Phone = phone;
		}

		public void SetExtra(string extra)
		{
			Extra = extra;
		}
	}
}
