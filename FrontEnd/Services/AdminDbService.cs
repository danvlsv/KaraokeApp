

using System.Text;
using System.Text.Json;

namespace BlazorApp.Services
{
	public class AdminDbService
	{
		private readonly HttpClient _httpClient;

		public AdminDbService(IHttpClientFactory httpClientFactory)
		{
			_httpClient = httpClientFactory.CreateClient("AdminDbService");
		}


		internal async void ApproveBookingService(int id)
		{
			var updateData = new { Status = "Approved" }; 

			// Serialize the update data to JSON
			var jsonContent = JsonSerializer.Serialize(updateData);
			var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
			var response = await _httpClient.PutAsync($"/reservations/{id}",content);

		}

		internal async void DeleteBookingService(int id)
		{
			var response = await _httpClient.DeleteAsync($"/reservations/{id}");

		}

		internal async  Task<List<Booking>> GetAllApprovedBooking()
		{
			var response = await _httpClient.GetAsync($"/reservations/approved");
			if (response.IsSuccessStatusCode)
			{

				var jsonResponse = await response.Content.ReadAsStringAsync();

				// Deserialize the JSON response into a List of Booking objects
				var bookings = JsonSerializer.Deserialize<List<Booking>>(jsonResponse, new JsonSerializerOptions
				{
					PropertyNameCaseInsensitive = true 
				});

				return bookings == null ? new List<Booking>(0) : bookings;
			}
			else
				return new List<Booking>();
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
					PropertyNameCaseInsensitive = true 
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
					PropertyNameCaseInsensitive = true 
				});

				return bookings == null ? new List<Booking>(0) : bookings;
			}
			else
				return new List<Booking>();
		}

		
	}
}
