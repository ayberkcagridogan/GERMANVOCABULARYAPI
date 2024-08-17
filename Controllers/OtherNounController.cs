using AutoMapper;
using GermanVocabularyAPI.Data;
using GermanVocabularyAPI.DTOs;
using GermanVocabularyAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GermanVocabularyAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OtherNounController : ControllerBase
{
    private readonly GermanVocabularyContext _context;
    private readonly IMapper _mapper;

    public OtherNounController(GermanVocabularyContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<OtherNounDTO>>> GetOtherNouns()
    {
        var otherNouns = await _context.OtherNouns.ToListAsync();
        return Ok(_mapper.Map<List<OtherNoun>>(otherNouns));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<OtherNounDTO>> GetOtherNoun(int id)
    {
        var otherNoun = await _context.OtherNouns.FindAsync(id);

        if (otherNoun == null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<OtherNoun>(otherNoun));
    }

    [HttpPost]
    public async Task<ActionResult<Verb>> PostOtherNoun(OtherNounDTO otherNounDTO)
    {
        if (!await _context.Decks.AnyAsync(d => d.Id == otherNounDTO.DeckId))
        {
            return NotFound("NotFound Deck");
        }

        var otherNoun = _mapper.Map<OtherNoun>(otherNounDTO);
        _context.OtherNouns.Add(otherNoun);
        await _context.SaveChangesAsync();
        return CreatedAtAction("GetOtherNoun", new { id = otherNoun.Id }, _mapper.Map<OtherNounDTO>(otherNoun));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutOtherNoun(int id, OtherNounDTO otherNounDTO)
    {
        if (id != otherNounDTO.Id)
        {
            return BadRequest();
        }

        if (!await _context.Decks.AnyAsync(d => d.Id == otherNounDTO.DeckId))
        {
            return NotFound("NotFound Deck");
        }

        var otherNoun = await _context.OtherNouns.FindAsync(id);

        if(otherNoun is null)
        {
            return NotFound();
        }
        
        _mapper.Map(otherNounDTO, otherNoun);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!OtherNounExists(id))
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
    
    [HttpPatch("{id}")]
    public async Task<IActionResult> PatchOtherNoun(int id, [FromBody] JsonPatchDocument<OtherNounDTO> patchDoc)
    {
        if (patchDoc == null)
        {
            return BadRequest();
        }

        var otherNoun = await _context.OtherNouns.FindAsync(id);
        if (otherNoun == null)
        {
            return NotFound();
        }

        var otherNounDto = _mapper.Map<OtherNounDTO>(otherNoun);
        patchDoc.ApplyTo(otherNounDto, ModelState);

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        _mapper.Map(otherNounDto, otherNoun);
        await _context.SaveChangesAsync();

        return Ok(_mapper.Map<OtherNounDTO>(otherNoun));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOtherNoun(int id)
    {
        var otherNoun = await _context.OtherNouns.FindAsync(id);
        if (otherNoun == null)
        {
            return NotFound();
        }

        _context.OtherNouns.Remove(otherNoun);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool OtherNounExists(int id)
    {
        return _context.OtherNouns.Any(e => e.Id == id);
    }
}
