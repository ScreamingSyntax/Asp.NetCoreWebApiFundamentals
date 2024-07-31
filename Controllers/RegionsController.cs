using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Test.Data;
using Test.Models.Domain;
using Test.Models.DTO;
using Test.Repositories;

namespace Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : Controller
    {
        private readonly TestDbContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(TestDbContext dbContext,
            IRegionRepository regionRepository,
            IMapper mapper
            )
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
            this.dbContext = dbContext;
        }

       //Get All Registions
       [HttpGet("all")]
       public async Task<IActionResult> GetAll()
        {
            var regions = await regionRepository.GetAllAsync();
            var regionsDto =  mapper.Map<List<Region>>(regions);
            return Ok(regionsDto);

        }

        //Get Single Regions
        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var regionDomain = await
            regionRepository.GetByIdAsync(id);
            if (regionDomain == null) {
                return NotFound();
            }
            var regionDto = mapper.Map<RegionDto>(regionDomain);
            return Ok(regionDto);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            var regionDomainModel = mapper.Map<Region>(addRegionRequestDto);
            regionDomainModel = await regionRepository.AddRegionAsync(regionDomainModel);
            var regionDto = mapper.Map<RegionDto>(regionDomainModel);
            return CreatedAtAction(nameof(GetById), new {id = regionDto.Id}, regionDto);
        }
        [HttpPut("{id:Guid}")]
        public async  Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            var regionDomainModel = await regionRepository.UpdateRegionAsync(id, mapper.Map<Region>(updateRegionRequestDto));
            if(regionDomainModel == null)
            {
                return NotFound();
            }
            var regionDto = mapper.Map<RegionDto>(regionDomainModel);
            return Ok(regionDto);
        }
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var domainRegionModel = await dbContext.Regions.FindAsync(id);
            if(domainRegionModel == null)
            {
                return NotFound();
            }
            await  regionRepository.DeleteRegionAsync(id);
            var regionDto = mapper.Map<RegionDto>(domainRegionModel);
            return Ok(regionDto);
        }

    }
}
