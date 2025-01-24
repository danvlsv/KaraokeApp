using DiscothequeBackEnd.Backend;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<ApplicationContext>(options =>
	options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));



//builder.Services.AddScoped<dbManager>();
builder.Services.AddScoped<DbManager>();

var app = builder.Build();


//app.MapGet("/", () => "Hello World!");

// delete booking
app.MapDelete("/reservations/{id}", (int id, DbManager dbManager) => dbManager.DeleteBookingService(id));

// approve booking
app.MapPut("/reservations/{id}", (int id, DbManager dbManager) => dbManager.ApproveBookingService(id));

// add booking
app.MapPost("/reservations", (Booking booking, DbManager dbManager) => dbManager.AddNewBooking(booking));

// get all booking
app.MapGet("/reservations", (DbManager dbManager) => dbManager.GetAllBooking());

// get all not approved bookings
app.MapGet("/reservations/notapproved", (DbManager dbManager) => dbManager.GetAllNotApprovedBooking());

// geta all approved bookings
app.MapGet("/reservations/approved", (DbManager dbManager) => dbManager.GetAllApprovedBooking());

// get all booked time of day
app.MapGet("/bookedTime/{date}/{roomNumber}",(string date,int roomNumber,DbManager dbManager) => dbManager.GetAllBookedTimeOfDay(date, roomNumber));

// check if date is booked
app.MapGet("/isDayBooked/{date}/{roomNumber}", (string date, int roomNumber, DbManager dbManager) => dbManager.IsDayBooked(date, roomNumber));




app.Run();
