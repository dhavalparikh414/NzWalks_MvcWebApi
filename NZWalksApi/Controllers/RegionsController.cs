using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalksApi.Data;
using NZWalksApi.Mappings;
using NZWalksApi.Models.Domain;
using NZWalksApi.Models.DTO;
using NZWalksApi.Repositories;

namespace NZWalksApi.Controllers;

//https://localhost:7240/api/regions
[Route("api/[controller]")]
[ApiController]
public class RegionsController : ControllerBase
{
    private readonly NzWalksDbContext _dbContext;
    private readonly IRegionRepository _regionRepository;
    private readonly IMapper _mapper;

    public RegionsController(NzWalksDbContext dbContext, IRegionRepository regionRepository, IMapper mapper)
    {
        _dbContext = dbContext;
        _regionRepository = regionRepository;
        _mapper = mapper;
    }
    [HttpGet]
    public async Task<IActionResult> GetAllRegionsAsync()
    {
        // Get data from database- domain models

        var regions = await _regionRepository.GetAllAsync();

        // Map domain models to DTOs and return
        return Ok(_mapper.Map<List<RegionDto>>(regions));
    }



    // Get single regionDomainModel by id
    // GET: https://localhost:7240/api/regions/{id}
    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetRegionById([FromRoute] Guid id)
    {
        // Get regionDomainModel domain model from the db
        var region = await _regionRepository.GetByIdAsync(id);
        if (region == null)
        {
            return NotFound();
        }

        // Map domain model to DTO
        return Ok(_mapper.Map<RegionDto>(region));
    }



    // POST: https://localhost:7240/api/regions
    [HttpPost]
    public async Task<IActionResult> CreateRegionAsync([FromBody] AddRegionRequestDto addRegionRequestDto)
    {
        // Map DTO to domain model
        var regionDomainModel = _mapper.Map<Region>(addRegionRequestDto);

        // Add to database
        await _regionRepository.CreateAsync(regionDomainModel);

        // Map domain model back to DTO
        var createdRegionDto = _mapper.Map<RegionDto>(regionDomainModel);

        // Return DTO
        return CreatedAtAction(nameof(GetRegionById), new { id = regionDomainModel.Id }, createdRegionDto);
    }

    [HttpPut]
    [Route("{id:Guid}")]
    public async Task<IActionResult> UpdateRegionAsync([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
    {

        // map dto to domain model
        var regionDomainModel = _mapper.Map<Region>(updateRegionRequestDto);

        // Get region from database (check if region exists)
        regionDomainModel = await _regionRepository.UpdateAsync(id, regionDomainModel);
        if (regionDomainModel == null)
        {
            return NotFound();
        }

        // Map domain model back to DTO
        return Ok(_mapper.Map<RegionDto>(regionDomainModel));

    }

    [HttpDelete]
    [Route("{id:Guid}")]
    public async Task<IActionResult> DeleteRegionAsync([FromRoute] Guid id)
    {
        // Get region from database
        var regionDomainModel = await _regionRepository.DeleteAsync(id);
        if (regionDomainModel == null)
        {
            return NotFound();
        }

        // Return success response
        return Ok(_mapper.Map<RegionDto>(regionDomainModel));
    }
}


