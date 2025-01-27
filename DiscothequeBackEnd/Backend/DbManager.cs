using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Query;
using System;

namespace DiscothequeBackEnd.Backend
{
    public class DbManager
    {
        private ApplicationContext _context;

		public DbEdit DbEdit;

		public DbManager() 
		{
			DbEdit = new DbEdit(_context);
		}

		public DbManager(ApplicationContext context)
        {
            _context = context;
			DbEdit = new DbEdit(_context);
		}



		public virtual void AddNewBooking(Booking book)
		{
			DbEdit.AddBooking.Execute(book);
		}


		//public virtual Booking? GetBookingByID(int ID) // репозиторий
		//{
		//	return _context.Reservations.FirstOrDefault(booking => booking.Id == ID); // Получаем бронь по индексу
		//}


		public virtual List<int> GetAllBookedTimeOfDay(string date, int room)
		{
			return _context.Reservations
				.Where(p => p.Date == date & p.RoomNumber == room)
				.Select(p => p.Time)
				.ToList(); // Получаем всё занятое время заданной даты
		}

		public virtual bool IsDayBooked(string date, int room)
		{
			var list = GetAllBookedTimeOfDay(date,room);
			if (list.Count >= 24)
			{

				return true; // весь день занят
			}
			else
			{
				
				return false; // есть свободные слоты
			}
		}



	}
}
