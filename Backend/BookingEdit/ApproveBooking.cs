using Microsoft.EntityFrameworkCore;

namespace BlazorApp.Backend.BookingEdit
{
	public class ApproveBooking: BookingEdit<Booking>
	{
		public ApproveBooking(ApplicationDbContext context) : base(context)
		{
		}

		protected override void PerformOperation(Booking book)
		{
			book.Status = true;
		}
	}
}
