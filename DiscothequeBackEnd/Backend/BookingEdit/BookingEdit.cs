namespace DiscothequeBackEnd.Backend.BookingEdit
{
	public abstract class BookingEdit<T>
	{
		protected readonly ApplicationContext _context;

		protected BookingEdit(ApplicationContext context)
		{
			_context = context;
		}

		// Template method
		public void Execute(T booking)
		{

			PerformOperation(booking);
			SaveChanges();
		}


		protected abstract void PerformOperation(T booking);

		private void SaveChanges()
		{
			_context.SaveChanges();
		}


	}
}
