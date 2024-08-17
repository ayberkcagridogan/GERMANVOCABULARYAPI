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
public class NounController : ControllerBase
{
    private readonly GermanVocabularyContext _context;
    private readonly IMapper _mapper;

    public NounController(GermanVocabularyContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<NounDTO>>> GetNouns()
    {
        var nouns = await _context.Nouns.ToListAsync();
        return Ok(_mapper.Map<List<NounDTO>>(nouns));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<NounDTO>> GetNoun(int id)
    {
        var noun = await _context.Nouns.FindAsync(id);

        if (noun == null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<NounDTO>(noun));
    }

    [HttpPost]
    public async Task<ActionResult<NounDTO>> PostNoun(NounDTO nounDTO)
    {
        if (!await _context.Decks.AnyAsync(d => d.Id == nounDTO.DeckId))
        {
            return NotFound("NotFound Deck");
        }

        var noun = _mapper.Map<Noun>(nounDTO);
        _context.Nouns.Add(noun);
        await _context.SaveChangesAsync();
        return CreatedAtAction("GetNoun", new { id = noun.Id }, _mapper.Map<NounDTO>(noun));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutNoun(int id, NounDTO nounDTO)
    {
        if (id != nounDTO.Id)
        {
            return BadRequest();
        }
        
        if (!await _context.Decks.AnyAsync(d => d.Id == nounDTO.DeckId))
        {
            return NotFound("NotFound Deck");
        }

        var noun = await _context.Nouns.FindAsync(id);

        if(noun is null)
        {
            return NotFound();
        }
        _mapper.Map(nounDTO, noun);
       
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

    [HttpPatch("{id}")]
    public async Task<IActionResult> PatchNoun(int id, [FromBody] JsonPatchDocument<NounDTO> patchDoc)
    {
        if (patchDoc == null)
        {
            return BadRequest();
        }

        var noun = await _context.Nouns.FindAsync(id);
        if (noun == null)
        {
            return NotFound();
        }

        var nounDto = _mapper.Map<NounDTO>(noun);
        patchDoc.ApplyTo(nounDto, ModelState);

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        _mapper.Map(nounDto, noun);
        await _context.SaveChangesAsync();

        return Ok(_mapper.Map<NounDTO>(noun));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteNoun(int id)
    {
        var noun = await _context.Nouns.FindAsync(id);
        if (noun == null)
        {
            return NotFound();
        }

        _context.Nouns.Remove(noun);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool NounExists(int id)
    {
        return _context.Nouns.Any(e => e.Id == id);
    }
}
