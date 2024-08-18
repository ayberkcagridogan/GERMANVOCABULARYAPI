using AutoMapper;
using GermanVocabularyAPI.Data;
using GermanVocabularyAPI.DTOs;
using GermanVocabularyAPI.Exception;
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
        try
        {
            var nouns = await _context.Nouns.ToListAsync();
            return Ok(_mapper.Map<List<NounDTO>>(nouns));    
        }
        catch (System.Exception ex)
        {
            return await ExceptionHandler.HandleExceptionAsync(ex);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<NounDTO>> GetNoun(int id)
    {
        try
        {
            var noun = await _context.Nouns.FindAsync(id);

            if (noun == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<NounDTO>(noun));   
        }
        catch (System.Exception ex)
        {
            return await ExceptionHandler.HandleExceptionAsync(ex);
        }
    }

    [HttpPost]
    public async Task<ActionResult<NounDTO>> PostNoun(NounDTO nounDTO)
    {
        try
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
        catch (System.Exception ex)
        {
            return await ExceptionHandler.HandleExceptionAsync(ex);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutNoun(int id, NounDTO nounDTO)
    {      
        try
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
            await _context.SaveChangesAsync();
            return NoContent();
        }
        catch (System.Exception ex)
        {
            return await ExceptionHandler.HandleExceptionAsync(ex, async () => await NounExistsAsync(id));
        }
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> PatchNoun(int id, [FromBody] JsonPatchDocument<NounDTO> patchDoc)
    {
        try
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
        catch (System.Exception ex)
        {
            return await ExceptionHandler.HandleExceptionAsync(ex);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteNoun(int id)
    {
        try
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
        catch (System.Exception ex)
        {
            return await ExceptionHandler.HandleExceptionAsync(ex);
        }
    }

    private async Task<bool> NounExistsAsync(int id)
    {
        return await _context.Nouns.AnyAsync(e => e.Id == id);
    }
}
