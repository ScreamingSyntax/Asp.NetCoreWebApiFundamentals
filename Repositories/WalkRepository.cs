using Microsoft.EntityFrameworkCore;
using Test.Data;
using Test.Models.Domain;

namespace Test.Repositories
{
    public class WalkRepository : IWalkRepository
    {
        public TestDbContext TestDbContext { get; }

        public WalkRepository(TestDbContext testDbContext)
        {
            TestDbContext = testDbContext;
        }


        public async Task<Walk> CreateAsync(Walk walk)
        {
            await TestDbContext.Walks.AddAsync(walk);
            await TestDbContext.SaveChangesAsync();
            return walk;
        }


        public async Task<List<Walk>> GetAllAsync()
        {
            return await TestDbContext.Walks.Include("Difficulty").Include("Region").ToListAsync();
        }

        public async Task<Walk?> GetAsync(Guid id)
        {
            return await TestDbContext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Walk?> UpdateAsync(Guid id, Walk walk)
        {
            Walk? walkDomain = await TestDbContext.Walks.FindAsync(id);
            if (walkDomain == null)
            {
                return null;
            }
            walkDomain.Name = walk.Name;
            walkDomain.Description = walk.Description;
            walkDomain.DifficultyId = walk.DifficultyId;
            walkDomain.LengthInKm = walk.LengthInKm;
            walkDomain.RegionId = walk.RegionId;
            await TestDbContext.SaveChangesAsync();
            return walkDomain;
        }

        public async Task<Walk?> DeleteAsync(Guid id)
        {
            Walk? walkDomain = await TestDbContext.Walks.
                Include("Difficulty").
                Include("Region").
                FirstOrDefaultAsync(x=> x.Id == id);
            if (walkDomain == null)
            {
                return null;
            }
            TestDbContext.Walks.Remove(walkDomain);
            await TestDbContext.SaveChangesAsync();
            return walkDomain;
        }
    }
}
