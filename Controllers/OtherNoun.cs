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
        var otherNoun = _mapper.Map<OtherNoun>(otherNounDTO);
        _context.OtherNouns.Add(otherNoun);
        await _context.SaveChangesAsync();

        otherNounDTO.Id = otherNoun.Id;

        return CreatedAtAction("GetOtherNoun", new { id = otherNounDTO.Id }, otherNounDTO);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutOtherNoun(int id, OtherNounDTO otherNounDTO)
    {
        if (id != otherNounDTO.Id)
        {
            return BadRequest();
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
