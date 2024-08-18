using AutoMapper;
using GermanVocabularyAPI.Data;
using GermanVocabularyAPI.DTOs;
using GermanVocabularyAPI.Exception;
using GermanVocabularyAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GermanVocabularyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeckController : ControllerBase
    {
        private readonly GermanVocabularyContext _context;
        private readonly IMapper _mapper;

        public DeckController(GermanVocabularyContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DeckDTO>>> GetDecks()
        {
            try
            {
                var decks = await _context.Decks.Include(d => d.Cards)
                                            .ToListAsync();

                return Ok(_mapper.Map<IEnumerable<DeckDTO>>(decks));
            }
            catch (System.Exception ex)
            {
                return await ExceptionHandler.HandleExceptionAsync(ex);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DeckDTO>> GetDeck(int id)
        {
            try
            {
                var deck = await _context.Decks.Include(d => d.Cards)
                                               .FirstOrDefaultAsync(d => d.Id == id);
                if (deck == null)
                {
                    return NotFound();
                }
                return Ok(_mapper.Map<DeckDTO>(deck));                
            }
            catch (System.Exception ex)
            {
                return await ExceptionHandler.HandleExceptionAsync(ex);
            }
        }

        [HttpPost]
        public async Task<ActionResult<DeckDTO>> PostDeck(DeckDTO deckDTO)
        {
            try
            {
                var deck = _mapper.Map<Deck>(deckDTO);
                _context.Decks.Add(deck);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetDeck", new { id = deck.Id }, _mapper.Map<DeckDTO>(deck));                
            }
            catch (System.Exception ex)
            {
                return await ExceptionHandler.HandleExceptionAsync(ex);
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchDeck(int id, [FromBody] JsonPatchDocument<DeckDTO> patchDoc)
        {
            try
            {
                if (patchDoc == null)
                {
                    return BadRequest();
                }

                var deck = await _context.Decks.FindAsync(id);
                if (deck == null)
                {
                    return NotFound();
                }

                var deckDto = _mapper.Map<DeckDTO>(deck);
                patchDoc.ApplyTo(deckDto, ModelState);

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                _mapper.Map(deckDto, deck);
                await _context.SaveChangesAsync();

                return Ok(_mapper.Map<DeckDTO>(deck));                
            }
            catch (System.Exception ex)
            {
                return await ExceptionHandler.HandleExceptionAsync(ex);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutDeck(int id, DeckDTO deckDTO)
        {
            try
            {
                if (id != deckDTO.Id)
                {
                    return BadRequest();
                }
                var deck = _mapper.Map<Deck>(deckDTO);
                _context.Entry(deck).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (System.Exception ex)
            {
                return await ExceptionHandler.HandleExceptionAsync(ex , async () => await DeckExistsAsync(id));
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDeck(int id)
        {
            try
            {
                var deck = await _context.Decks.FindAsync(id);
                if (deck == null)
                {
                    return NotFound();
                }
                _context.Decks.Remove(deck);
                await _context.SaveChangesAsync();
                return NoContent();                
            }
            catch (System.Exception ex)
            {
                return await ExceptionHandler.HandleExceptionAsync(ex);
            }
        }

        private async Task<bool> DeckExistsAsync(int id)
        {
            return await _context.Decks.AnyAsync(e => e.Id == id);
        }
    }
}
