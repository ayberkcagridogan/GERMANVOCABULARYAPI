using AutoMapper;
using GermanVocabularyAPI.Data;
using GermanVocabularyAPI.DTOs;
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
            var decks = await _context.Decks.Include(d => d.Cards)
                                            .ToListAsync();

            return Ok(_mapper.Map<IEnumerable<DeckDTO>>(decks));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DeckDTO>> GetDeck(int id)
        {
            var deck = await _context.Decks.Include(d => d.Cards)
                                           .FirstOrDefaultAsync(d => d.Id == id);
            if (deck == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<DeckDTO>(deck));
        }

        [HttpPost]
        public async Task<ActionResult<DeckDTO>> PostDeck(DeckDTO deckDTO)
        {
            var deck = _mapper.Map<Deck>(deckDTO);
            _context.Decks.Add(deck);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetDeck", new { id = deck.Id }, _mapper.Map<DeckDTO>(deck));
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchDeck(int id, [FromBody] JsonPatchDocument<DeckDTO> patchDoc)
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

        [HttpPut("{id}")]
        public async Task<IActionResult> PutDeck(int id, DeckDTO deckDTO)
        {
            if (id != deckDTO.Id)
            {
                return BadRequest();
            }
            var deck = _mapper.Map<Deck>(deckDTO);
            _context.Entry(deck).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeckExists(id))
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
        public async Task<IActionResult> DeleteDeck(int id)
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

        private bool DeckExists(int id)
        {
            return _context.Decks.Any(e => e.Id == id);
        }
    }
}
