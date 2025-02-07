﻿using BlazorApp.Services;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Components.Calendar
{
	public partial class Calendar : ComponentBase
	{
		[Inject]
		CurrentBooking currentBooking { get; set; }

		[Inject]
		public DbService dbService { get; set; }

		[Parameter]
		public DateOnly ChosenDate { get; set; }

		int chosenRoomNumber;

		protected override async Task OnInitializedAsync()
		{

			chosenRoomNumber = currentBooking.RoomNumber;
			if (currentBooking.Date != null)
			{
				ChosenDate = DateOnly.Parse(currentBooking.Date);
				await ChosenDateChanged.InvokeAsync(ChosenDate);
			}
			else
			{
				ChosenDate = DateOnly.MinValue;
			}


			FirstMonday = StartOfWeek();
			GetAvailableDates();
		}

		[Parameter]
		public EventCallback<DateOnly> ChosenDateChanged { get; set; }

		public List<DateOnly> availableDates = new List<DateOnly>();
		

		DateOnly today = DateOnly.FromDateTime(DateTime.Now);

		public static DateOnly StartOfWeek()
		{
			//Console.Clear();
			DateOnly today = DateOnly.FromDateTime(DateTime.Now);
			int diff = ((int)today.DayOfWeek + 5) % 6;
			
			return today.AddDays(-diff);
		}




		public async Task DateChoice(string date)
		{

			ChosenDate = DateOnly.Parse(date);
			currentBooking.SetDate(date);
			await ChosenDateChanged.InvokeAsync(ChosenDate);

		}

		public DateOnly FirstMonday;
		// = StartOfWeek()
		DateOnly LastDay;


		



		public async void GetAvailableDates()
		{
			LastDay = FirstMonday.AddDays(4 * 7);
			for (DateOnly day = DateOnly.FromDateTime(DateTime.Now); day <= LastDay; day = day.AddDays(1))
			{
				string dayString = day.ToString();
				
				if (await dbService.IsDayBooked(dayString, chosenRoomNumber) == false)
				{
					availableDates.Add(day);
				}
			}
			await InvokeAsync(StateHasChanged);

		}


	}
}
