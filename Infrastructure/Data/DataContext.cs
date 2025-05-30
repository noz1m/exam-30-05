using Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Data;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Car> Cars { get; set; }
    public DbSet<Booking> Bookings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasMany(u => u.Bookings)
            .WithOne(b => b.Users)
            .HasForeignKey(b => b.UserId);

        modelBuilder.Entity<Car>()
            .HasMany(c => c.Bookings)
            .WithOne(b => b.Cars)
            .HasForeignKey(b => b.CarId);

        modelBuilder.Entity<Booking>()
            .HasKey(b => b.Id); 
    }
}
