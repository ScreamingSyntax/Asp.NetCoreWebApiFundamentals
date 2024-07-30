using Microsoft.AspNetCore.Mvc;
using Test.Data;
using Test.Models.Domain;
using Test.Models.DTO;

namespace Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : Controller
    {
        private readonly TestDbContext dbContext;

        public RegionsController(TestDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

       //Get All Registions
       [HttpGet("all")]
       public IActionResult GetAll()
        {
            var regions = dbContext.Regions.ToList();
            var regionDto = new List<RegionDto>();
            foreach (var region in regions)
            {
                regionDto.Add(new RegionDto()
                {
                    Id = region.Id,
                    RegionImageUrl = region.RegionImageUrl,
                    Code = region.Code,
                    Name = region.Name
                });
            }
            return Ok(regionDto);

        }

        //Get Single Regions
        [HttpGet("{id:Guid}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var regionDomain = 
            dbContext.Regions.Find(id);
            if (regionDomain == null) {
                return NotFound();
            }
            var regionDto = new RegionDto() { Name = regionDomain.Name,
            Code = regionDomain.Code,
            Id= id,
            RegionImageUrl= regionDomain.RegionImageUrl,
            };
            return Ok(regionDto);
        }
        [HttpPost]
        public IActionResult Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            var regionDomainModel = new Region
            {
                Code = addRegionRequestDto.Code,

                Name = addRegionRequestDto.Name,
                RegionImageUrl = addRegionRequestDto.RegionImageUrl
            };
            dbContext.Regions.Add(regionDomainModel);
            dbContext.SaveChanges();
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                RegionImageUrl = regionDomainModel.RegionImageUrl,
                Name = regionDomainModel.Name,
                Code = regionDomainModel.Code,
            };
            return CreatedAtAction(nameof(GetById), new {id = regionDto.Id}, regionDto);
        }
        [HttpPut("{id:Guid}")]
        public IActionResult Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            var regionDomainModel = dbContext.Regions.Find(id);
            if(regionDomainModel == null)
            {
                return NotFound();
            }
            regionDomainModel.Code = updateRegionRequestDto.Code;
            regionDomainModel.Name = updateRegionRequestDto.Name;
            regionDomainModel.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;
            dbContext.SaveChanges();
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl,
            };
            return Ok(regionDto);
        }
        [HttpDelete("{id:Guid}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            var domainRegionModel = dbContext.Regions.Find(id);
            if(domainRegionModel == null)
            {
                return NotFound();
            }
            dbContext.Remove(domainRegionModel.Id);
            var regionDto = new RegionDto
            {
                Code = domainRegionModel.Code,
                Id = domainRegionModel.Id,
                Name = domainRegionModel.Name,
                RegionImageUrl = domainRegionModel.RegionImageUrl,
            };
            return Ok(regionDto);
        }

    }
}
