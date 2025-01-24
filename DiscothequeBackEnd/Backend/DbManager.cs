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




		#region calls DbEdit

		public virtual void DeleteBookingService(int ID) // сервисa
		{

			var book = GetBookingByID(ID);

			if (book != null) // Проверяем, существует ли продукт
			{
				// вызываем метод удаления
				DbEdit.DeleteBooking.Execute(book);
			}
		}

		public virtual void ApproveBookingService(int ID) // сервис
		{

			var book = GetBookingByID(ID);

			if (book != null) // Проверяем, существует ли продукт
			{
				// вызываем метод
				DbEdit.ApproveBooking.Execute(book);
			}
		}

		public virtual void AddNewBooking(Booking book)
		{
			DbEdit.AddBooking.Execute(book);
		}

		#endregion





		#region No Saves

		public virtual List<Booking> GetAllBooking()
		{
			return _context.Reservations.ToList(); // Получаем все брони из базы данных
		}


		public virtual Booking? GetBookingByID(int ID) // репозиторий
		{
			return _context.Reservations.FirstOrDefault(booking => booking.Id == ID); // Получаем бронь по индексу
		}


		public virtual List<Booking> GetAllNotApprovedBooking()
		{
			return _context.Reservations.Where(p => p.Status == false).ToList(); // Получаем брони которые ещё не подтвердили
		}

		public virtual List<Booking> GetAllApprovedBooking()
		{
			return _context.Reservations.Where(p => p.Status == true).ToList(); // Получаем брони которые ещё не подтвердили
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

#endregion 

#if DEBUG

		public void DeleteAllBooking()
		{
			var allBooking = _context.Reservations.ToList();
			_context.Reservations.RemoveRange(allBooking); // Удаляем все брони
			_context.SaveChanges(); // Сохраняем изменения в базе данных
		}

#endif

	

	}
}
