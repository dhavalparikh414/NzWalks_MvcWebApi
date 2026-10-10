using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalksApi.Data;
using NZWalksApi.Models.Domain;
using NZWalksApi.Models.DTO;

namespace NZWalksApi.Controllers
{
    //https://localhost:7240/api/regions
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NzWalksDbContext _dbContext;
        public RegionsController(NzWalksDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult GetAllRegions()
        {
            // Get data from database- domain models

            var regions = _dbContext.Regions.ToList();

            // Map domain models to DTOs
            var regionsDTO = new List<RegionDto>();
            foreach (var region in regions)
            {
                regionsDTO.Add(new RegionDto
                {
                    Id = region.Id,
                    Name = region.Name,
                    Code = region.Code,
                    RegionImageUrl = region.RegionImageUrl
                });
            }

            // Return DTOs
            return Ok(regionsDTO);
        }

        // Get single regionDomainModel by id
        // GET: https://localhost:7240/api/regions/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetRegionById(Guid id)
        {
            // Get regionDomainModel domain model from the db
            var region = _dbContext.Regions.FirstOrDefault(x => x.Id == id);
            if (region == null)
            {
                return NotFound();
            }

            // Map domain model to DTO
            var regionDTO = new RegionDto
            {
                Id = region.Id,
                Name = region.Name,
                Code = region.Code,
                RegionImageUrl = region.RegionImageUrl
            };

            // Return DTO
            return Ok(regionDTO);
        }

        // POST: https://localhost:7240/api/regions
        [HttpPost]
        public IActionResult CreateRegion([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            // Map DTO to domain model
            var regionDomainModel = new Region
            {
                Name = addRegionRequestDto.Name,
                Code = addRegionRequestDto.Code,
                RegionImageUrl = addRegionRequestDto.RegionImageUrl
            };

            // Add to database
            _dbContext.Regions.Add(regionDomainModel);
            _dbContext.SaveChanges();

            // Map domain model back to DTO
            var createdRegionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Name = regionDomainModel.Name,
                Code = regionDomainModel.Code,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };

            // Return DTO
            return CreatedAtAction(nameof(GetRegionById), new { id = createdRegionDto.Id }, createdRegionDto);
        }
    }
}


