using DiscothequeBackEnd.Backend;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<ApplicationContext>(options =>
	options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddScoped<AdminDbManager>();

var app = builder.Build();



// delete booking
app.MapDelete("/reservations/{id}", (int id, AdminDbManager dbManager) => dbManager.DeleteBookingService(id));

// approve booking
app.MapPut("/reservations/{id}", (int id, AdminDbManager dbManager) => dbManager.ApproveBookingService(id));

// get all booking
app.MapGet("/reservations", (AdminDbManager dbManager) => dbManager.GetAllBooking());

// get all not approved bookings
app.MapGet("/reservations/notapproved", (AdminDbManager dbManager) => dbManager.GetAllNotApprovedBooking());

// geta all approved bookings
app.MapGet("/reservations/approved", (AdminDbManager dbManager) => dbManager.GetAllApprovedBooking());


app.Run();
