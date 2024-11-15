using BlazorApp.Backend;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Components.TimeChoice
{
	public partial class TimeChoiceCard: ComponentBase
	{
		[Inject]
		CurrentBooking currentBooking { get; set; }

		protected override async Task OnInitializedAsync()
		{
			ChosenDate = currentBooking.Date;
			ChosenRoom = currentBooking.RoomNumber;
			if (currentBooking.Time != null)
			{
				ChooseTime(currentBooking.Time);

			}

			// AddBooking();
			await GetAvailableTimes();
			// DeleteBooking();
		}


		string ChosenDate { get; set; }
		int ChosenRoom { get; set; }


		int? ChosenTime { get; set; } = null;


		string SetBorder(int time)
		{
			return ChosenTime == time ? "5px solid #BD3838" : "5px solid transparent";

		}


		[Parameter]
		public RenderFragment? LeftButton { get; set; }
		[Parameter]
		public RenderFragment? RightButton { get; set; }


		//Миша добавилл код ниже

		private List<Booking> bookings = null;
		private List<int> Time = null;


		private void ChooseTime(int? hour)
		{
			ChosenTime = hour;
			currentBooking.SetTime(hour);
		}


		private async Task GetAvailableTimes()
		{
			Time = DbManager.GetAllBookedTimeOfDay(ChosenDate, ChosenRoom);
		}

		
	}
}
