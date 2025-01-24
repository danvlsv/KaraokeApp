using Microsoft.AspNetCore.Components;
using BlazorApp.Services;

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
			adminDbService.ApproveBookingService(id);
			StateHasChanged();
			DisplayTable(curType);
		}

		public void DeleteBooking(int id)
		{
			adminDbService.DeleteBookingService(id);
			DisplayTable(curType);
			StateHasChanged();
		}

		protected override async Task OnInitializedAsync()
		{
			curType = BookingsEnum.All;
			await DisplayTable(curType);
			StateHasChanged();
		}

		public async Task DisplayTable(BookingsEnum value)
		{
			switch (value)
			{
				case BookingsEnum.All:
					bookings = await adminDbService.GetAllBooking();

					break;
				case BookingsEnum.Approved:
					bookings = await adminDbService.GetAllApprovedBooking();

					break;
				case BookingsEnum.NotApproved:
					bookings = await adminDbService.GetAllNotApprovedBooking();

					break;
			}
			if (bookings != null)
			{
				length = bookings.Count;
			}

		}
	}
}
