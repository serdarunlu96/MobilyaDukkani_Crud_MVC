using Microsoft.EntityFrameworkCore;

namespace MobilyaMagazasi_Crud_MVC.Data
{
    public class MobilyaDbContext : DbContext
    {
        public MobilyaDbContext(DbContextOptions<MobilyaDbContext> options) : base(options)
        {
            
        }

        public DbSet<Mobilya> Mobilyalar => Set<Mobilya>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Mobilya>().HasData(
                new Mobilya() { Id = 1, Name = "Kanepe", Stock = 10},
                new Mobilya() { Id = 2, Name = "Koltuk", Stock = 15 },
                new Mobilya() { Id = 3, Name = "Sandalye", Stock = 20 },
                new Mobilya() { Id = 4, Name = "Tv Ünitesi", Stock = 25 }
                );
        }

    }
}
