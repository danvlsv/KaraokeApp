namespace BlazorApp.Backend.BookingEdit
{
	public class AddBooking : BookingEdit<Booking>
	{
		public AddBooking(ApplicationDbContext context) : base(context)
		{
		}

		protected override void PerformOperation(Booking book)
		{
			_context.Reservations.Add(book);
		}
	}
}
