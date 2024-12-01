using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Query;
using System;

namespace BlazorApp.Backend
{
    public class DbManager
    {
        private readonly ApplicationDbContext _context;

        public DbManager(ApplicationDbContext context)
        {
            _context = context;
        }

		//public void AddNewBooking(int roomNum, string date,int time,string name,string phone,string extra)
  //      {
  //          var book = new Booking
  //          {
  //              RoomNumber = roomNum,
  //              Date = date,
  //              Time = time,
  //              Name = name,
  //              Phone = phone,
  //              Extra = extra
  //          };

  //          _context.Reservations.Add(book);
  //          _context.SaveChanges(); // Сохраняем изменения в базе данных
  //      }

		public virtual void AddNewBooking(Booking book)
		{
			_context.Reservations.Add(book);
			_context.SaveChanges(); // Сохраняем изменения в базе данных
		}

        public virtual List<Booking> GetAllBooking()
        {
            return _context.Reservations.ToList(); // Получаем все брони из базы данных
        }

		public void DeleteAllBooking()
		{
			var allBooking = _context.Reservations.ToList();
			_context.Reservations.RemoveRange(allBooking); // Удаляем все брони
			_context.SaveChanges(); // Сохраняем изменения в базе данных
		}

		//public void DeleteBooking(int ID)
		//{
		//	var methods = _context.Reservations.FirstOrDefault(booking => booking.Id == ID);  // Получаем бронь по индексу
		//	var book = methods;
				
		//	if (book != null) // Проверяем, существует ли продукт
		//	{
		//		// вызываем метод удаления
		//		_context.Reservations.Remove(book); // Удаляем бронь
		//		_context.SaveChanges(); // Сохраняем изменения в базе данных
		//	}
		//}





		public virtual Booking? GetBookingByID(int ID) // репозиторий
		{
			return _context.Reservations.FirstOrDefault(booking => booking.Id == ID); // Получаем бронь по индексу
		}


		public virtual void DeleteBooking(Booking book) // репозиторий
		{
			_context.Reservations.Remove(book); // Удаляем бронь
			_context.SaveChanges(); // Сохраняем изменения в базе данных
		}


		public virtual void DeleteBookingService(int ID) // сервис
		{
			
			var book = GetBookingByID(ID);

			if (book != null) // Проверяем, существует ли продукт
			{
				// вызываем метод удаления
				DeleteBooking(book);
			}
		}





		public List<Booking> GetAllNotApprovedBooking()
		{
			return _context.Reservations.Where(p => p.Status == false).ToList(); // Получаем брони которые ещё не подтвердили
		}

		public List<Booking> GetAllApprovedBooking()
		{
			return _context.Reservations.Where(p => p.Status == true).ToList(); // Получаем брони которые ещё не подтвердили
		}

		//public void ApproveBooking(int ID) 
		//{
		//	var book = _context.Reservations.FirstOrDefault(booking => booking.Id == ID); // Получаем бронь по индексу
		//	if (book != null) // Проверяем, существует ли бронь
		//	{
		//		book.Status = true; // Обновляем статус брони
		//		_context.SaveChanges(); // Сохраняем изменения в базе данных
		//	}
		//}

		public virtual void ApproveBookingService(int ID) // сервис
		{

			Booking? book = GetBookingByID(ID);

			if (book != null) // Проверяем, существует ли продукт
			{
				// вызываем метод удаления
				ApproveBooking(book);
			}
		}

		public virtual void ApproveBooking(Booking book) // репозиторий
		{
			book.Status = true; // Обновляем статус брони
			_context.SaveChanges(); // Сохраняем изменения в базе данных
		}



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
