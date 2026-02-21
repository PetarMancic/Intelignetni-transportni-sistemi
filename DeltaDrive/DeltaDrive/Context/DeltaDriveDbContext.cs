

using DeltaDrive.Domain;
using Microsoft.EntityFrameworkCore;

public class DeltaDriveDbContext : DbContext
  {
    public DeltaDriveDbContext(DbContextOptions<DeltaDriveDbContext> options)
          : base(options)
    {
       
    }

  

    public DbSet<Passenger> Passengers => Set<Passenger>();
    public DbSet<Vehicle> Vehicles => Set<Vehicle>();
    public DbSet<Ride> Rides => Set<Ride>();
    public DbSet<Rating> Ratings => Set<Rating>();




    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Ignore<Location>();

        modelBuilder.Entity<Vehicle>()
         .OwnsOne(v => v.Location);

        modelBuilder.Entity<Ride>()
            .OwnsOne(r => r.DestinationLocation);
        modelBuilder.Entity<Ride>()
            .OwnsOne(r => r.StartLocation);

    }
}

