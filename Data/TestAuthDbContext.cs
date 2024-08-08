using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Test.Data
{
    public class TestAuthDbContext : IdentityDbContext
    {
        public TestAuthDbContext(DbContextOptions<TestAuthDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            string readerRoleId = "b76a85c8-51c7-47ec-b584-aab1101db267";
            string writerRoleId = "a646c6e3-ce88-43d3-8429-7f705dacf5c9";

            var roles = new List<IdentityRole>()
            {     
                new IdentityRole
                {
                    Id= readerRoleId,
                    ConcurrencyStamp = readerRoleId,
                    Name="Reader",
                    NormalizedName = "Reader".ToUpper(),
                },
                new IdentityRole
                {
                    Id = writerRoleId,
                    ConcurrencyStamp = writerRoleId,
                    Name = "Writer",
                    NormalizedName = "Writer".ToUpper()
                }
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
