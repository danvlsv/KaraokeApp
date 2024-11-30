using System;
using System.Collections.Generic;
using System.Linq;
using BlazorApp.Backend;
using Moq;
using Xunit;
using Bunit;
using Microsoft.AspNetCore.Routing;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.EntityFrameworkCore;



namespace BlazorApp.Components
{
	public class UnitTester : TestContext // Наследуем от TestContext
	{
		private readonly Mock<DbManager> _mockDbManager;
		private readonly Mock<CurrentBooking> _mockCurrentBooking;
		private readonly Mock<ApplicationDbContext> _mockDbContext;
		private ApplicationDbContext _context; // Добавлено поле для контекста
		 

		public UnitTester()
		{
			_mockCurrentBooking = new Mock<CurrentBooking>();
			_mockDbContext = new Mock<ApplicationDbContext>(); // Создаем мок для ApplicationDbContext
			_mockDbManager = new Mock<DbManager>(_mockDbContext.Object); // Передаем мок в DbManager
		}


		#region Calendar.GetAvailableDates() Testing
		[Fact]
		public void GetAvailableDates_AddsNotBookedDates()
		{
			// Arrange
			_mockCurrentBooking.Setup(m => m.RoomNumber).Returns(1); //Устанавливаем 1 комнату
			Services.AddSingleton(_mockCurrentBooking.Object);
			Services.AddSingleton(_mockDbManager.Object);

			// Настройка мока для DbManager
			_mockDbManager.Setup(m => m.IsDayBooked(It.IsAny<string>(), 1)).Returns(false); //Устанавливаем чтобы IsDayBooked всегда возвращал false 

			var component = RenderComponent<BlazorApp.Components.Calendar.Calendar>(parameters => parameters
				.Add(p => p.ChosenDate, DateOnly.MinValue)); // Рэндер компонента

			// Act
			component.Instance.GetAvailableDates();

			// Assert
			// Проверяем, что доступные даты были добавлены
			Assert.NotEmpty(component.Instance.availableDates);
		}


		[Fact]
		public void GetAvailableDates_AllDatesBooked()
		{
			// Arrange
			_mockCurrentBooking.Setup(m => m.RoomNumber).Returns(1); //Устанавливаем 1 комнату
			Services.AddSingleton(_mockCurrentBooking.Object);
			Services.AddSingleton(_mockDbManager.Object);

			// Настройка мока для DbManager
			_mockDbManager.Setup(m => m.IsDayBooked(It.IsAny<string>(), 1)).Returns(true); //Устанавливаем чтобы IsDayBooked всегда возвращал false 

			var component = RenderComponent<BlazorApp.Components.Calendar.Calendar>(parameters => parameters
				.Add(p => p.ChosenDate, DateOnly.MinValue)); // Рэндер компонента

			// Act
			component.Instance.GetAvailableDates();


			// Assert
			// Проверяем, что доступные даты были добавлены
			Assert.Empty(component.Instance.availableDates);
			//Assert.NotEmpty(component.Instance.availableDates);
		}
		#endregion

		#region TimeChoiceCard.GetBookedTimes() Testing
		
		[Fact]
		public void GetBookedTime_HasAvailableTime()
		{
			// Arrange
			_mockCurrentBooking.Setup(m => m.RoomNumber).Returns(1); //Устанавливаем 1 комнату
			///*_mockCurrentBooking*/.Object.SetDate("30.11.2024");
			_mockCurrentBooking.Setup(m => m.Date).Returns("30.04.2004"); //устанавливаем дату 30.04.2024
			Services.AddSingleton(_mockCurrentBooking.Object);
			Services.AddSingleton(_mockDbManager.Object);

			// Настройка мока для DbManager
			_mockDbManager.Setup(m => m.GetAllBookedTimeOfDay(It.IsAny<string>(), 1)).Returns([6, 8, 12]); // 3 занятых промежутка

			var component = RenderComponent<BlazorApp.Components.TimeChoice.TimeChoiceCard>(); // Рэндер компонента

			// Act
			component.Instance.GetBookedTimes();

			// Assert
			// Проверяем, что доступные даты были добавлены
			Assert.True(component.Instance.BookedTime.Count ==3);
			//Assert.NotEmpty(component.Instance.Time);
			//Assert.NotEmpty(component.Instance.availableDates);
		}


		[Fact]
		public void GetBookedTime_NoAvailableTime()
		{
			// Arrange
			_mockCurrentBooking.Setup(m => m.RoomNumber).Returns(1); //Устанавливаем 1 комнату
			///*_mockCurrentBooking*/.Object.SetDate("30.11.2024");
			_mockCurrentBooking.Setup(m => m.Date).Returns("30.04.2004"); //устанавливаем дату 30.04.2024
			Services.AddSingleton(_mockCurrentBooking.Object);
			Services.AddSingleton(_mockDbManager.Object);

			// Настройка мока для DbManager
			List<int> array = new List<int>();
			for (int i = 0; i < 24; i++)
			{
				array.Add(i);
			}

			Console.WriteLine(array);

			_mockDbManager.Setup(m => m.GetAllBookedTimeOfDay(It.IsAny<string>(), 1)).Returns(array);

			var component = RenderComponent<BlazorApp.Components.TimeChoice.TimeChoiceCard>(); // Рэндер компонента

			// Act
			component.Instance.GetBookedTimes();

			// Assert
			// Проверяем, что доступные даты были добавлены

			Assert.Equal(24,component.Instance.BookedTime.Count);

		}



		#endregion


		#region DbManager Testing

		//[Fact]
		//public void ApproveBookingService_BookingExists()
		//{
		//	_mockDbManager.Setup(p => p.GetBookingByID(2)).Returns(new Booking());
		//	Services.AddSingleton(_mockCurrentBooking.Object);
		//	Services.AddSingleton(_mockDbManager.Object);
			
			
		//	_mockDbManager.Object.ApproveBookingService(2);
		//}

		[Fact]
		public void ApproveBookingService_NoBooking()
		{
			_mockDbManager.Setup(p => p.GetBookingByID(2)).Returns((Booking)null);
			Services.AddSingleton(_mockCurrentBooking.Object);
			Services.AddSingleton(_mockDbManager.Object);


			_mockDbManager.Object.ApproveBookingService(2);
			_mockDbManager.Verify(mock => mock.ApproveBookingService(2), Times.Once());
		}





		//[Fact]
		//public void DeleteBookingService_BookingExists()
		//{
		//	// Arrange
		//	var book = new Booking
		//	{
		//		Id = 2,
		//		RoomNumber = 1,
		//		Date = "30.04.2004",
		//		Time = 2,
		//		Name = "Dan",
		//		Phone = "+79519515021",
		//		Extra = "Нет",
		//	};

		//	// Setup mock behavior
		//	_mockDbManager.Setup(p => p.GetBookingByID(2)).Returns(book);
		//	_mockDbManager.Setup(p => p.DeleteBooking(book)).Verifiable();

		//	Services.AddSingleton(_mockCurrentBooking.Object);
		//	Services.AddSingleton(_mockDbManager.Object);

		//	// Act
		//	_mockDbManager.Object.AddNewBooking(book);
		//	_mockDbManager.Object.DeleteBookingService(2);

		//	// Assert
		//	_mockDbManager.Verify(p => p.DeleteBooking(book), Times.Once);
		//	_mockDbManager.Verify(p => p.GetBookingByID(2), Times.Once);
		//}


		[Fact]
		public void DeleteBookingService_NoBooking()
		{
			_mockDbManager.Setup(p => p.GetBookingByID(2)).Returns((Booking)null);
			Services.AddSingleton(_mockCurrentBooking.Object);
			Services.AddSingleton(_mockDbManager.Object);


			_mockDbManager.Object.DeleteBookingService(2);
		}

		#endregion

		//[Fact]
		//public void ConfirmBooking_CorrectData()
		//{
		//	_mockCurrentBooking.Setup(m => m.RoomNumber).Returns(1);
		//	_mockCurrentBooking.Setup(m => m.Date).Returns("30.04.2004");
		//	_mockCurrentBooking.Setup(m => m.Time).Returns(2);
		//	_mockCurrentBooking.Setup(m => m.Phone).Returns("9519515021"); 
		//	_mockCurrentBooking.Setup(m => m.Name).Returns("Dan");

		//	Services.AddSingleton(_mockCurrentBooking.Object);
		//	Services.AddSingleton(_mockDbManager.Object);

		//	var component = RenderComponent<BlazorApp.Components.CustomerData.CustomerData>(); // Рэндер компонента

		//	component.Instance.ConfirmBooking();
		//}

		[Fact]
		public void ConfirmBooking_IncorrectData()
		{
			_mockCurrentBooking.Setup(m => m.RoomNumber).Returns(1);
			_mockCurrentBooking.Setup(m => m.Date).Returns("30.04.2004");
			_mockCurrentBooking.Setup(m => m.Time).Returns(2);
			_mockCurrentBooking.Setup(m => m.Phone).Returns("95f9515021");
			_mockCurrentBooking.Setup(m => m.Name).Returns("Dan");

			Services.AddSingleton(_mockCurrentBooking.Object);
			Services.AddSingleton(_mockDbManager.Object);

			var component = RenderComponent<BlazorApp.Components.CustomerData.CustomerData>(); // Рэндер компонента

			component.Instance.ConfirmBooking();

			_mockDbManager.Verify(mock=>mock.AddNewBooking(It.IsAny<Booking>()),Times.Once());	

			
		}



	}
}
