using System;
using System.Collections.Generic;
using System.Linq;
using BlazorApp.Backend;
using Moq;
using Xunit;
using Bunit;

namespace BlazorApp.Components
{
	public class UnitTester : TestContext // Наследуем от TestContext
	{
		private readonly Mock<DbManager> _mockDbManager;
		private readonly Mock<CurrentBooking> _mockCurrentBooking;
		private readonly Mock<ApplicationDbContext> _mockDbContext;

		public UnitTester()
		{
			_mockCurrentBooking = new Mock<CurrentBooking>();
			_mockDbContext = new Mock<ApplicationDbContext>(); // Создаем мок для ApplicationDbContext
			_mockDbManager = new Mock<DbManager>(_mockDbContext.Object); // Передаем мок в DbManager
		}

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
		public void GetAvailableDates_NotAddsBookedDates()
		{
			// Arrange
			_mockCurrentBooking.Setup(m => m.RoomNumber).Returns(1);  //Устанавливаем 1 комнату
			Services.AddSingleton(_mockCurrentBooking.Object);
			Services.AddSingleton(_mockDbManager.Object);

			var component = RenderComponent<BlazorApp.Components.Calendar.Calendar>(parameters => parameters
				.Add(p => p.ChosenDate, DateOnly.MinValue)); // Рэндер компонента

			// Установите FirstMonday в компоненте
			component.Instance.FirstMonday = new DateOnly(2024, 11, 15);
			var bookedDays = new List<string>
			{
				new DateOnly(2024, 11, 18).ToString(), // Пример забронированного дня
                new DateOnly(2023, 11, 19).ToString()
			};


			_mockDbManager.Setup(m => m.IsDayBooked(It.IsAny<string>(), 1))
						   .Returns((string day, int roomNumber) => bookedDays.Contains(day));

			// Act
			component.Instance.GetAvailableDates();

			// Assert
			var expectedDates = Enumerable.Range(0, 28)
										   .Select(i => component.Instance.FirstMonday.AddDays(i))
										   .Where(d => !bookedDays.Contains(d.ToString()))
										   .ToList();
			Assert.All(expectedDates, expectedDate => Assert.Contains(expectedDate, component.Instance.availableDates));
		}
	}
}
