using BlazorApp.Services;
using Microsoft.AspNetCore.Components;
using System.Text.RegularExpressions;

namespace BlazorApp.Components.CustomerData
{
	public partial class CustomerData: ComponentBase
	{
		[Parameter]
		public RenderFragment? LeftButton { get; set; }
		[Parameter]
		public RenderFragment? RightButton { get; set; }

		[Inject]
		CurrentBooking currentBooking { get; set; }

		int roomNumber;
		string date;
		int? hour;

		public string name = "";

		string tempPhone = "";
		public string phone = "";
		Regex phoneRegex = new Regex("^[0-9 ]+$");

		string extra = "";

		protected override async Task OnInitializedAsync()
		{

			roomNumber = currentBooking.RoomNumber;
			date = currentBooking.Date;
			hour = currentBooking.Time;
			name = currentBooking.Name;
			phone = currentBooking.Phone;
			extra = currentBooking.Extra;

		}

		private void TempSave()
		{
			currentBooking.SetName(name);
			currentBooking.SetPhone(phone);
			Console.WriteLine($"\n\n{name}\n{phone}\n{extra}");
			currentBooking.SetExtra(extra);
		}

		public bool CheckData()
		{
			if (name != null && phone != null)
			{
				// Console.WriteLine((name.Length > 0) && (Regex.Replace(phone, @"\s+", "").Length == 10));
				return ((name.Length > 0) && (Regex.Replace(phone, @"\s+", "").Length == 10) && (phoneRegex.IsMatch(phone)));
			}
			return false;
		}

		public void ConfirmBooking()
		{
			if (CheckData())
			{
				var book = new Booking
				{
					RoomNumber = currentBooking.RoomNumber,
					Date = currentBooking.Date,
					Time = (int)currentBooking.Time,
					Name = currentBooking.Name,
					Phone = "+7 " + currentBooking.Phone,
					Extra = string.IsNullOrWhiteSpace(currentBooking.Extra) ? "Нет" : currentBooking.Extra
				};
				//dbService.AddNewBooking(currentBooking.RoomNumber,
				//	currentBooking.Date,
				//	(int)currentBooking.Time,
				//	currentBooking.Name,
				//	"+7 " + currentBooking.Phone,
				//	string.IsNullOrWhiteSpace(currentBooking.Extra) ? "Нет" : currentBooking.Extra);
				dbService.AddNewBooking(book);


				currentBooking.Complete = true;

				NavigationManager.NavigateTo("/");
			}


		}

		private void PhoneInput()
		{


			if (phone != null)
			{


				switch (Regex.Replace(phone, @"\s+", "").Length)
				{

					case 10:

						phone = Regex.Replace(phone, @"\s+", "");
						phone = phone.Insert(3, " ");
						phone = phone.Insert(7, " ");
						phone = phone.Insert(10, " ");
						TempSave();
						return;


				}

				TempSave();

			}
		}
	}
}
