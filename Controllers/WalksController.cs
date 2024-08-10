using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Test.CustomActionFilter;
using Test.Models.Domain;
using Test.Models.DTO;
using Test.Repositories;

namespace Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : Controller
    {
        private IMapper Mapper { get; }
        public IWalkRepository WalkRepository { get; }

        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            Mapper = mapper;
            WalkRepository = walkRepository;
        }


        [HttpGet("all")]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery, [FromQuery] string? sortBy, [FromQuery] bool isAscending = true, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 1000)
        {
            return Ok(Mapper.Map<List<WalkDTO>>(await WalkRepository.GetAllAsync(filterOn,filterQuery,sortBy,isAscending,pageNumber,pageSize)));
        }
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id) { 

            Walk? region = await WalkRepository.GetAsync(id);
            if (region == null)
            {
                return NotFound();
            }
            return Ok(Mapper.Map<WalkDTO>(region));
        }

        [ValidateModelAttribute]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
            Walk walk = new Walk()
            {

            }
            Walk walkDomain = await WalkRepository.CreateAsync(Mapper.Map<Walk>(addWalkRequestDto));
            return CreatedAtAction(nameof(GetById), new { id = walkDomain.Id }, Mapper.Map<WalkDTO>(walkDomain));
        }
        [ValidateModelAttribute]
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody]UpdateWalkRequestDto updateRegionRequestDto)
        {
                Walk? walkDomain = await WalkRepository.UpdateAsync(id, Mapper.Map<Walk>(updateRegionRequestDto));
                if (walkDomain == null)
                {
                    return NotFound();
                }
                return Ok(Mapper.Map<WalkDTO>(walkDomain));
            
        }
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            Walk? walkDomain = await WalkRepository.DeleteAsync(id);
            if(walkDomain == null)
            {
                return NotFound();
            }
            return Ok(Mapper.Map<WalkDTO>(walkDomain));
        }
     //   [HttpPut("{id:guid}")]
     
    }
}
