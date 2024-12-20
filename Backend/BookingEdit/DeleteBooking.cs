namespace BlazorApp.Backend.BookingEdit
{
	public class DeleteBooking: BookingEdit<Booking>
	{
		public DeleteBooking(ApplicationDbContext context) : base(context)
		{
		}

		protected override void PerformOperation(Booking book)
		{
			_context.Reservations.Remove(book);
		}
	}
}
