using BlazorApp.Components;
using Microsoft.EntityFrameworkCore;
using BlazorApp.Services;
using BlazorApp;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddHttpClient("DbService", client =>
{
	client.BaseAddress = new Uri("https://localhost:7027/"); // URL of DbService
});

builder.Services.AddScoped<DbService>();
builder.Services.AddSingleton<CurrentBooking>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
