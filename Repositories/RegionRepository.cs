using Microsoft.EntityFrameworkCore;
using Test.Data;
using Test.Models.Domain;

namespace Test.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly TestDbContext testDbContext;

        public RegionRepository(TestDbContext testDbContext)
        {
            this.testDbContext = testDbContext;
        }
        public async Task<Region?> GetByIdAsync(Guid id)
        {
            return await testDbContext.Regions.FindAsync(id);
        }
        public async Task<Region> AddRegionAsync(Region region)
        {
            await this.testDbContext.Regions.AddAsync(region);
            await this.testDbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region?> DeleteRegionAsync(Guid regionId)
        {
            var region = await this.testDbContext.Regions.FindAsync(regionId);
            if (region != null)
            {

                this.testDbContext.Regions.Remove(region!);
               await this.testDbContext.SaveChangesAsync();
                return region;
            }
            return null;
        }


        public async Task<List<Region>> GetAllAsync()
        {
            return await testDbContext.Regions.ToListAsync();
        }

       

        public async Task<Region?> UpdateRegionAsync(Guid id, Region region)
        {
            var existingRegion = await testDbContext.Regions.FindAsync(id);
            if (existingRegion == null)
            {
                return null;
            }
            existingRegion!.RegionImageUrl = region.RegionImageUrl;
            existingRegion!.Code = region.Code;
            existingRegion!.Name = region.Name;
            await testDbContext.SaveChangesAsync();
            return existingRegion;
        }
    }
}
