namespace BlazorApp.Backend.BookingEdit
{
	public abstract class BookingEdit<T>
	{
		protected readonly ApplicationDbContext _context;

		protected BookingEdit(ApplicationDbContext context)
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
