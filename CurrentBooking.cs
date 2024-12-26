using BlazorApp.Backend;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BlazorApp
{
    public class CurrentBooking
    {
        /*public int Id { get; private set; } */    // Уникальный идентификатор брони
		public virtual int RoomNumber { get; private set; }  // Забронированная комната
		public virtual string ? Date { get; private set; } // Дата
		public virtual int? Time { get; private set; } // Время
        public virtual string? Name { get; private set; } = ""; // Имя
        public virtual string? Phone { get; private set; } = "";// Телефон
        public virtual string? Extra { get; private set; } // Дополнительная информация

        public bool Complete { get; set; } = false;

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
            Complete = false;
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

    //Adapter
	public class AdapterToReguralBooking : Booking
	{
        Booking booking;
		public AdapterToReguralBooking(CurrentBooking b)
		{
			booking = new Booking
			{
				RoomNumber = b.RoomNumber,
				Date = b.Date,
				Time = (int)b.Time,
				Name = b.Name,
				Phone = "+7 " + b.Phone,
				Extra = string.IsNullOrWhiteSpace(b.Extra) ? "Нет" : b.Extra,
                Status = b.Complete
			};
		}
	}
}
