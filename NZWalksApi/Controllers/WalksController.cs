using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalksApi.Models.Domain;
using NZWalksApi.Models.DTO;
using NZWalksApi.Repositories;

namespace NZWalksApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WalksController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IWalkRepository _walkRepository;
    public WalksController(IMapper mapper, IWalkRepository walkRepository)
    {
        _mapper = mapper;
        _walkRepository = walkRepository;
    }

    // get all walks
    // GET: /api/walks

    [HttpGet]
    public async Task<IActionResult> GetAllWalks()
    {
        var walksDomainModel = await _walkRepository.GetAllAsync();
        return Ok(_mapper.Map<List<WalkDto>>(walksDomainModel));
    }
    // CREATE a new walk
    // POST: /api/walks

    [HttpPost]
    public async Task<IActionResult> CreateWalkAsync([FromBody] AddWalkRequestDto addWalkRequestDto)
    {
        // Map DTO to domain model

        // Validate the incoming walk data
        if (addWalkRequestDto == null)
        {
            return BadRequest("Walk data is required.");
        }
        var walkDomainModel = _mapper.Map<Walk>(addWalkRequestDto);

        await _walkRepository.CreateAsync(walkDomainModel);
        // Save the walk to the database (this is just a placeholder, implement your own logic)
        // await _walkRepository.AddAsync(walk);

        // MAP domain model to DTO.

        return Ok(_mapper.Map<WalkDto>(walkDomainModel));
        // Return a response indicating success
        //return CreatedAtAction(nameof(GetWalkById), new { id = walkDomainModel.Id }, walkDomainModel);
    }
}
