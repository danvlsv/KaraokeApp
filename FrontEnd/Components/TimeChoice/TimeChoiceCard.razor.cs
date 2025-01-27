using BlazorApp.Services;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Components.TimeChoice
{
	public partial class TimeChoiceCard: ComponentBase
	{
		[Inject]
		CurrentBooking currentBooking { get; set; }

		[Inject]
		public DbService dbService { get; set; }

		protected override async Task OnInitializedAsync()
		{
			ChosenDate = currentBooking.Date;
			ChosenRoom = currentBooking.RoomNumber;
			if (currentBooking.Time != null)
			{
				ChooseTime(currentBooking.Time);

			}
			BookedTime = await dbService.GetAllBookedTimeOfDay(ChosenDate, ChosenRoom);
			// AddBooking();
			//await GetBookedTimes();
			// DeleteBooking();
		}


		public string ChosenDate { get; set; }
		public int ChosenRoom { get; set; }


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

		//private List<Booking> bookings = null;
		public List<int> BookedTime;


		public void ChooseTime(int? hour)
		{
			ChosenTime = hour;
			currentBooking.SetTime(hour);
		}


		public async Task GetBookedTimes()
		{
			BookedTime = await dbService.GetAllBookedTimeOfDay(ChosenDate, ChosenRoom);
			await InvokeAsync(StateHasChanged);
		}

		
	}
}
