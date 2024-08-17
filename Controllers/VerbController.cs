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
public class VerbController : ControllerBase
{
    private readonly GermanVocabularyContext _context;
    private readonly IMapper _mapper;

    public VerbController(GermanVocabularyContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<VerbDTO>>> GetVerbs()
    {
        var verbs = await _context.Verbs.ToListAsync();
        return Ok(_mapper.Map<List<VerbDTO>>(verbs));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<VerbDTO>> GetVerb(int id)
    {
        var verb = await _context.Verbs.FindAsync(id);

        if (verb == null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<VerbDTO>(verb));
    }

    [HttpPost]
    public async Task<ActionResult<Verb>> PostVerb(VerbDTO verbDTO)
    {
        if (!await _context.Decks.AnyAsync(d => d.Id == verbDTO.DeckId))
        {
            return NotFound("NotFound Deck");
        }

        var verb = _mapper.Map<Verb>(verbDTO);
        _context.Verbs.Add(verb);
        await _context.SaveChangesAsync();
        return CreatedAtAction("GetVerb", new { id = verb.Id }, _mapper.Map<VerbDTO>(verb));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutNoun(int id, VerbDTO verbDTO)
    {
        if (id != verbDTO.Id)
        {
            return BadRequest();
        }

        if (!await _context.Decks.AnyAsync(d => d.Id == verbDTO.DeckId))
        {
            return NotFound("NotFound Deck");
        }

        var verb = await _context.Verbs.FindAsync(id);

        if(verb is null)
        {
            return NotFound();
        }

        _mapper.Map(verbDTO, verb);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await VerbExistsAsync(id))
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
    public async Task<IActionResult> PatchVerb(int id, [FromBody] JsonPatchDocument<VerbDTO> patchDoc)
    {
        if (patchDoc == null)
        {
            return BadRequest();
        }

        var verb = await _context.Verbs.FindAsync(id);
        if (verb == null)
        {
            return NotFound();
        }

        var verbDto = _mapper.Map<VerbDTO>(verb);
        patchDoc.ApplyTo(verbDto, ModelState);

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        _mapper.Map(verbDto, verb);
        await _context.SaveChangesAsync();

        return Ok(_mapper.Map<VerbDTO>(verb));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteVerb(int id)
    {
        var verb = await _context.Verbs.FindAsync(id);
        if (verb == null)
        {
            return NotFound();
        }

        _context.Verbs.Remove(verb);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private async Task<bool> VerbExistsAsync(int id)
    {
        return await _context.Verbs.AnyAsync(e => e.Id == id);
    }
}
