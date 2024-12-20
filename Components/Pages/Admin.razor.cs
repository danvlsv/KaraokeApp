using Microsoft.AspNetCore.Components;
using BlazorApp.Backend;

namespace BlazorApp.Components.Pages
{
	public partial class Admin: ComponentBase
	{
		List<Booking> bookings;
		int length = 0;

		public enum BookingsEnum
		{
			All,
			Approved,
			NotApproved
		}

		BookingsEnum curType;

		public void ApproveBooking(int id)
		{
			Console.WriteLine(id);
			DbManager.ApproveBookingService(id);
			DisplayTable(curType);
		}

		public void DeleteBooking(int id)
		{
			DbManager.DeleteBookingService(id);
			DisplayTable(curType);
			StateHasChanged();
		}

		protected override async Task OnInitializedAsync()
		{
			curType = BookingsEnum.All;
			DisplayTable(curType);

		}

		public void DisplayTable(BookingsEnum value)
		{
			switch (value)
			{
				case BookingsEnum.All:
					bookings = DbManager.GetAllBooking();

					break;
				case BookingsEnum.Approved:
					bookings = DbManager.GetAllApprovedBooking();

					break;
				case BookingsEnum.NotApproved:
					bookings = DbManager.GetAllNotApprovedBooking();

					break;
			}
			if (bookings != null)
			{
				length = bookings.Count;
			}

		}
	}
}
