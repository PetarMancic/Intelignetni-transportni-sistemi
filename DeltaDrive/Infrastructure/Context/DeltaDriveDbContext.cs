

using Microsoft.EntityFrameworkCore;

public class DeltaDriveDbContext : DbContext
  {
    public DeltaDriveDbContext(DbContextOptions<DeltaDriveDbContext> options)
          : base(options)
    {
       
    }

  

    public DbSet<Passenger> Passengers => Set<Passenger>();
    public DbSet<VehicleItem> Vehicles => Set<VehicleItem>();
    public DbSet<Ride> Rides => Set<Ride>();
    public DbSet<RideRating> Ratings => Set<RideRating>();




    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Ignore<Location>();

        modelBuilder.Entity<VehicleItem>()
         .OwnsOne(v => v.Location);

        modelBuilder.Entity<Ride>()
            .OwnsOne(r => r.DestinationLocation);
        modelBuilder.Entity<Ride>()
            .OwnsOne(r => r.StartLocation);

    }
}

