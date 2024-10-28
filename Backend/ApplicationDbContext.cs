namespace BlazorApp.Backend;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
    {
    }

    // Определяем DbSet для каждой модели, которую мы хотим использовать
    public DbSet<Booking> Reservations { get; set; }
}



