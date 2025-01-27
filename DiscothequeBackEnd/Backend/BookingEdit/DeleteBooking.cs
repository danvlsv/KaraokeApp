namespace DiscothequeBackEnd.Backend.BookingEdit
{
	public class DeleteBooking: BookingEdit<Booking>
	{
		public DeleteBooking(ApplicationContext context) : base(context)
		{
		}

		protected override void PerformOperation(Booking book)
		{
			_context.Reservations.Remove(book);
		}
	}
}
