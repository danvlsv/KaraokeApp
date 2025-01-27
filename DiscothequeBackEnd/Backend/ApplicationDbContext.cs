namespace DiscothequeBackEnd.Backend;
using Microsoft.EntityFrameworkCore;

public class ApplicationContext : DbContext
{
    	

	public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
	{
	}

	public ApplicationContext()
	{
		Database.EnsureCreated();
	}

	// Определяем DbSet для каждой модели, которую мы хотим использовать
	public DbSet<Booking> Reservations { get; set; }


	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=discotheque;Username=postgres;Password=Sdv150308; IncludeErrorDetail =True");
	}

}



