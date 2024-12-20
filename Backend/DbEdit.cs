using BlazorApp.Backend.BookingEdit;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace BlazorApp.Backend
{
	public class DbEdit
	{
		protected readonly ApplicationDbContext _context;

		public DbEdit(ApplicationDbContext context)
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
