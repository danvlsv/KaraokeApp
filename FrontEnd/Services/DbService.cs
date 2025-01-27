

using System.Text;
using System.Text.Json;

namespace BlazorApp.Services
{
	public class DbService
	{
		private readonly HttpClient _httpClient;

		public DbService(IHttpClientFactory httpClientFactory)
		{
			_httpClient = httpClientFactory.CreateClient("DbService");
		}

		internal async void AddNewBooking(Booking book)
		{
			var jsonContent = JsonSerializer.Serialize(book);
			var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
			var response =  await _httpClient.PostAsync("/reservations",content);

		}
		

		internal async Task<List<int>> GetAllBookedTimeOfDay(string chosenDate, int chosenRoom)
		{
			var response = await _httpClient.GetAsync($"/bookedTime/{chosenDate}/{chosenRoom}");
			if (response.IsSuccessStatusCode)
			{
				// Read the response content as a string
				var jsonResponse = await response.Content.ReadAsStringAsync();


				var bookedTimes = JsonSerializer.Deserialize<List<int>>(jsonResponse, new JsonSerializerOptions
				{
					PropertyNameCaseInsensitive = true 
				});

				return bookedTimes==null? new List<int>(0): bookedTimes;
			}
			else
				return new List<int>();
		}


		internal async Task<bool> IsDayBooked(string dayString, int chosenRoomNumber)
		{
			var response = await _httpClient.GetAsync($"/isDayBooked/{dayString}/{chosenRoomNumber}");
			if (response.IsSuccessStatusCode)
			{
				// Read the response content as a string
				var jsonResponse = await response.Content.ReadAsStringAsync();

				// Deserialize the JSON response into a List of integers (assuming the booked times are integers)
				var bookedTimes = JsonSerializer.Deserialize<bool>(jsonResponse, new JsonSerializerOptions
				{
					PropertyNameCaseInsensitive = true 
				});

				return bookedTimes;
			}
			else
				return false;
		}
	}
}
