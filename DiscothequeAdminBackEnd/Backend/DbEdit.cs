using DiscothequeBackEnd.Backend.BookingEdit;
using Microsoft.EntityFrameworkCore;

namespace DiscothequeBackEnd.Backend
{
	public class DbEdit
	{
		protected readonly ApplicationContext _context;

		public DbEdit(ApplicationContext context)
		{
			_context = context;
			DeleteBooking = new DeleteBooking(_context);
			ApproveBooking = new ApproveBooking(_context);
			AddBooking = new AddBooking(_context);	
		}

		public DeleteBooking DeleteBooking;
		public ApproveBooking ApproveBooking;
		public AddBooking AddBooking;


	}
}
