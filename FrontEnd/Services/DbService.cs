

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
			//throw new NotImplementedException();
		}

		internal async void ApproveBookingService(int id)
		{
			var updateData = new { Status = "Approved" }; // Assuming you want to change the status to "Approved"

			// Serialize the update data to JSON
			var jsonContent = JsonSerializer.Serialize(updateData);
			var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
			var response = await _httpClient.PutAsync($"/reservations/{id}",content);
			//throw new NotImplementedException();
		}

		internal async void DeleteBookingService(int id)
		{
			var response = await _httpClient.DeleteAsync($"/reservations/{id}");
			//throw new NotImplementedException();
		}

		internal async  Task<List<Booking>> GetAllApprovedBooking()
		{
			var response = await _httpClient.GetAsync($"/reservations/approved");
			if (response.IsSuccessStatusCode)
			{
				// Read the response content as a string
				var jsonResponse = await response.Content.ReadAsStringAsync();

				// Deserialize the JSON response into a List of Booking objects
				var bookings = JsonSerializer.Deserialize<List<Booking>>(jsonResponse, new JsonSerializerOptions
				{
					PropertyNameCaseInsensitive = true // Optional: to handle case sensitivity
				});

				return bookings == null ? new List<Booking>(0) : bookings;
			}
			else
				return new List<Booking>();
		}

		internal async Task<List<int>> GetAllBookedTimeOfDay(string chosenDate, int chosenRoom)
		{
			var response = await _httpClient.GetAsync($"/bookedTime/{chosenDate}/{chosenRoom}");
			if (response.IsSuccessStatusCode)
			{
				// Read the response content as a string
				var jsonResponse = await response.Content.ReadAsStringAsync();

				// Deserialize the JSON response into a List of integers (assuming the booked times are integers)
				var bookedTimes = JsonSerializer.Deserialize<List<int>>(jsonResponse, new JsonSerializerOptions
				{
					PropertyNameCaseInsensitive = true // Optional: to handle case sensitivity
				});

				return bookedTimes==null? new List<int>(0): bookedTimes;
			}
			else
				return new List<int>();
		}

		internal async Task<List<Booking>> GetAllBooking()
		{
			var response = await _httpClient.GetAsync($"/reservations");
			if (response.IsSuccessStatusCode)
			{
				// Read the response content as a string
				var jsonResponse = await response.Content.ReadAsStringAsync();

				// Deserialize the JSON response into a List of Booking objects
				var bookings = JsonSerializer.Deserialize<List<Booking>>(jsonResponse, new JsonSerializerOptions
				{
					PropertyNameCaseInsensitive = true // Optional: to handle case sensitivity
				});

				return bookings == null ? new List<Booking>(0) : bookings;
			}
			else
				return new List<Booking>();
		}

		internal async Task<List<Booking>> GetAllNotApprovedBooking()
		{
			var response = await _httpClient.GetAsync($"/reservations/notapproved");
			if (response.IsSuccessStatusCode)
			{
				// Read the response content as a string
				var jsonResponse = await response.Content.ReadAsStringAsync();

				// Deserialize the JSON response into a List of Booking objects
				var bookings = JsonSerializer.Deserialize<List<Booking>>(jsonResponse, new JsonSerializerOptions
				{
					PropertyNameCaseInsensitive = true // Optional: to handle case sensitivity
				});

				return bookings == null ? new List<Booking>(0) : bookings;
			}
			else
				return new List<Booking>();
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
					PropertyNameCaseInsensitive = true // Optional: to handle case sensitivity
				});

				return bookedTimes;
			}
			else
				return false;
		}
	}
}
