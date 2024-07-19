using AutoMapper;
using GermanVocabularyAPI.Data;
using GermanVocabularyAPI.DTOs;
using GermanVocabularyAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GermanVocabularyAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AdjectiveController : ControllerBase
{
    private readonly GermanVocabularyContext _context;
    private readonly IMapper _mapper;

    public AdjectiveController(GermanVocabularyContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AdjectiveDTO>>> GetAdjectives()
    {
        var adjectives = await _context.Nouns.ToListAsync();
        return Ok(_mapper.Map<List<AdjectiveDTO>>(adjectives));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AdjectiveDTO>> GetAdjective(int id)
    {
        var adjective = await _context.Adjectives.FindAsync(id);

        if (adjective is null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<AdjectiveDTO>(adjective));
    }

    [HttpPost]
    public async Task<ActionResult<AdjectiveDTO>> PostAdjective(AdjectiveDTO adjectiveDTO)
    {
        var adjective = _mapper.Map<Adjective>(adjectiveDTO);
        _context.Adjectives.Add(adjective);
        await _context.SaveChangesAsync();

        adjectiveDTO.Id = adjective.Id; 

        return CreatedAtAction("GetAdjective", new { id = adjectiveDTO.Id }, adjectiveDTO);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutNoun(int id, AdjectiveDTO adjectiveDTO)
    {
        if (id != adjectiveDTO.Id)
        {
            return BadRequest();
        }

        var adjective = await _context.Adjectives.FindAsync(id);

        if(adjective is null)
        {
            return NotFound();
        }
        _mapper.Map(adjectiveDTO, adjective);
       
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!NounExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteNoun(int id)
    {
        var adjective = await _context.Adjectives.FindAsync(id);
        if (adjective is null)
        {
            return NotFound();
        }

        _context.Adjectives.Remove(adjective);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool NounExists(int id)
    {
        return _context.Adjectives.Any(e => e.Id == id);
    }
}
