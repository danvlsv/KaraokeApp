using Microsoft.EntityFrameworkCore;

namespace DiscothequeBackEnd.Backend.BookingEdit
{
	public class ApproveBooking: BookingEdit<Booking>
	{
		public ApproveBooking(ApplicationContext context) : base(context)
		{
		}

		protected override void PerformOperation(Booking book)
		{
			book.Status = true;
		}
	}
}
