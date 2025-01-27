using DiscothequeBackEnd.Backend;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<ApplicationContext>(options =>
	options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddScoped<DbManager>();

var app = builder.Build();


// add booking
app.MapPost("/reservations", (Booking booking, DbManager dbManager) => dbManager.AddNewBooking(booking));

// get all booked time of day
app.MapGet("/bookedTime/{date}/{roomNumber}",(string date,int roomNumber,DbManager dbManager) => dbManager.GetAllBookedTimeOfDay(date, roomNumber));

// check if date is booked
app.MapGet("/isDayBooked/{date}/{roomNumber}", (string date, int roomNumber, DbManager dbManager) => dbManager.IsDayBooked(date, roomNumber));




app.Run();
