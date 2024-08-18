using AutoMapper;
using GermanVocabularyAPI.Data;
using GermanVocabularyAPI.DTOs;
using GermanVocabularyAPI.Exception;
using GermanVocabularyAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
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
        try
        {
            var adjectives = await _context.Adjectives.ToListAsync();
            return Ok(_mapper.Map<List<AdjectiveDTO>>(adjectives));
        }
        catch (System.Exception ex)
        {
            return await ExceptionHandler.HandleExceptionAsync(ex);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AdjectiveDTO>> GetAdjective(int id)
    {
        try
        {
            var adjective = await _context.Adjectives.FindAsync(id);

            if (adjective is null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<AdjectiveDTO>(adjective));
        }
        catch (System.Exception ex)
        {
            return await ExceptionHandler.HandleExceptionAsync(ex);
        }
    }

    [HttpPost]
    public async Task<ActionResult<AdjectiveDTO>> PostAdjective(AdjectiveDTO adjectiveDTO)
    {
        try
        {
            if (!await _context.Decks.AnyAsync(d => d.Id == adjectiveDTO.DeckId))
            {
                return NotFound("NotFound Deck");
            }

            var adjective = _mapper.Map<Adjective>(adjectiveDTO);
            _context.Adjectives.Add(adjective);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAdjective", new { id = adjective.Id }, _mapper.Map<AdjectiveDTO>(adjective));
        }
        catch (System.Exception ex)
        {
            return await ExceptionHandler.HandleExceptionAsync(ex);
        }
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> PatchAdjective(int id, [FromBody] JsonPatchDocument<AdjectiveDTO> patchDoc)
    {
        try
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            var adjective = await _context.Adjectives.FindAsync(id);
            if (adjective == null)
            {
                return NotFound();
            }

            var adjectiveDto = _mapper.Map<AdjectiveDTO>(adjective);
            patchDoc.ApplyTo(adjectiveDto, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(adjectiveDto, adjective);
            await _context.SaveChangesAsync();

            return Ok(_mapper.Map<AdjectiveDTO>(adjective));
        }
        catch (System.Exception ex)
        {
            return await ExceptionHandler.HandleExceptionAsync(ex);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutNoun(int id, AdjectiveDTO adjectiveDTO)
    {
        try
        {
            if (id != adjectiveDTO.Id)
            {
                return BadRequest();
            }

            if (!await _context.Decks.AnyAsync(d => d.Id == adjectiveDTO.DeckId))
            {
                return NotFound("NotFound Deck");
            }

            var adjective = await _context.Adjectives.FindAsync(id);

            if(adjective is null)
            {
                return NotFound();
            }
            _mapper.Map(adjectiveDTO, adjective);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        catch (System.Exception ex)
        {
            return await ExceptionHandler.HandleExceptionAsync(ex , async () =>  await NounExistsAsync(id));
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteNoun(int id)
    {
        try
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
        catch (System.Exception ex)
        {
            return await ExceptionHandler.HandleExceptionAsync(ex);
        }
    }

    private async Task<bool> NounExistsAsync(int id)
    {
        return await _context.Adjectives.AnyAsync(e => e.Id == id);
    }
}
