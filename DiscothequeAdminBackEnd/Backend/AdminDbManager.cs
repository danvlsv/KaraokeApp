using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Query;
using System;

namespace DiscothequeBackEnd.Backend
{
    public class AdminDbManager
    {
        private ApplicationContext _context;

		public DbEdit DbEdit;

		public AdminDbManager() 
		{
			DbEdit = new DbEdit(_context);
		}

		public AdminDbManager(ApplicationContext context)
        {
            _context = context;
			DbEdit = new DbEdit(_context);
		}




		public virtual void DeleteBookingService(int ID) // сервис
		{

			var book = GetBookingByID(ID);

			if (book != null) // Проверяем, существует ли бронь
			{
				// вызываем метод удаления
				DbEdit.DeleteBooking.Execute(book);
			}
		}

		public virtual void ApproveBookingService(int ID) // сервис
		{

			var book = GetBookingByID(ID);

			if (book != null) // Проверяем, существует ли бронь
			{
				// вызываем метод
				DbEdit.ApproveBooking.Execute(book);
			}
		}



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



	}
}
