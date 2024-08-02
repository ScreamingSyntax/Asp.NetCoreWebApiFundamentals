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


        public async Task<List<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 1000)
        {
            var walks = TestDbContext.Walks.Include("Difficulty").Include("Region").AsQueryable();
            //Filtering
            if(string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if (filterOn.Equals("Name",StringComparison.OrdinalIgnoreCase))
                { 
                    walks = walks.Where(x=>x.Name.Contains(filterQuery));  
                }

            }
            
            //Sorting
            if(string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.Name) : walks.OrderByDescending(x => x.Name);   
                }
                else if (sortBy.Equals("Length", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending? walks.OrderBy(x=> x.LengthInKm) : walks.OrderByDescending(x=> x.LengthInKm);
                }
            }

            // Pagination 
            var skiResults = (pageNumber - 1) * pageSize;
            return await walks.Skip(skiResults).Take(pageSize).ToListAsync();
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
