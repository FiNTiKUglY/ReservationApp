using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ReservationApp.Entities;

namespace ReservationApp.Data
{
    public class ApplicationContext: IdentityDbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
        {
        }

        public new DbSet<User> Users { get; set; }

        public new DbSet<Tag> Tags { get; set; }

        public new DbSet<Service> Services { get; set; }

        public new DbSet<Room> Rooms { get; set; }

        public new DbSet<Review> Reviews { get; set; }

        public new DbSet<Region> Regions { get; set; }

        public new DbSet<Order> Orders { get; set; }

        public new DbSet<Hotel> Hotels { get; set; }

        public new DbSet<Holding> Holdings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("AspNetUsers");

            base.OnModelCreating(modelBuilder);
        }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
            optionsBuilder.UseLazyLoadingProxies();
		}

	}
}
