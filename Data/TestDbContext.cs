using Microsoft.EntityFrameworkCore;
using Test.Models.Domain;

namespace Test.Data
{
    public class TestDbContext : DbContext
    {
        public TestDbContext(DbContextOptions<TestDbContext> dbContextOptions) : base(dbContextOptions)
        {
            
        }

        public DbSet<Image> Images { get; set; } 
        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Seed data for Difficulties 
            List<Difficulty> difficulties = new List<Difficulty>()
            {
                new Difficulty()
                {
                    Id = Guid.Parse("dfb29660-7935-4381-8259-0972213424c5"),
                    Name = "Easy"
                },
                new Difficulty() {
                    Id = Guid.Parse("36488fd7-8663-4bea-b95d-8097c645f1f1"),
                    Name = "Medium",
                },
                new Difficulty() {
                    Id = Guid.Parse("548c1654-b287-4ea1-b2dc-10e065faa6fa"),
                    Name = "Hard"
                }

            };
            // Seed Difficulties to the db
            modelBuilder.Entity<Difficulty>().HasData(difficulties);

            List<Region> regions = new List<Region>()
            {
                new Region() {
                    Id = Guid.Parse("6d65dd4d-c70e-4bc6-bf2c-939070296e87"),
                    Name = "Aukland",
                    Code = "AKL",
                    RegionImageUrl = "sample-img.jpg"
                },
                new Region() {
                    Id = Guid.Parse("6bc1d8c7-cee9-498e-919d-fe3ef6d07bcd"),
                    Name = "Wellington",
                    Code = "WLG",
                    RegionImageUrl = "wellington-img.jpg"
                },
                new Region() {
                    Id = Guid.Parse("fd548cc5-5a71-4e38-af0b-ff33315f02ac"),
                    Name = "Christchurch",
                    Code = "CHC",
                    RegionImageUrl = "christchurch-img.jpg"
                },
                new Region() {
                    Id = Guid.Parse("0af39885-7618-4539-b334-4ccb9e1ee75b"),
                    Name = "Hamilton",
                    Code = "HLZ",
                    RegionImageUrl = "hamilton-img.jpg"
                },
                new Region() {
                    Id = Guid.Parse("e2521014-b803-440c-9622-18f0b726c5bb"),
                    Name = "Tauranga",
                    Code = "TRG",
                    RegionImageUrl = "tauranga-img.jpg"
                },
                new Region() {
                    Id = Guid.Parse("795ec274-f743-4e3b-a835-c59ca884d7c0"),
                    Name = "Dunedin",
                    Code = "DUD",
                    RegionImageUrl = "dunedin-img.jpg"
                }
            };
            modelBuilder.Entity<Region>().HasData(regions);

        }

    }
}
