using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace StudyFlashCard.Controllers;

[ApiController]
[Route("[controller]")]
public class CardController : ControllerBase
{

    private readonly cardsContext _db;

    private readonly ILogger<CardController> _logger;

    public CardController(ILogger<CardController> logger, cardsContext db)
    {
        _db = db;
        _logger = logger;
    }

    [HttpGet(Name = "GetCardsByQuery")]
    public IEnumerable<Card> GetCardsByQuery(long? type, bool? known)
    {
        var query = _db.Cards.AsQueryable();
        if (type is not null)
        {
            query = query.Where(card => card.Type == type);
        }
        if (known is not null)
        {
            query = query.Where(card => card.Known == known);
        }
        return query.ToList();
    }

    //[HttpGet(Name = "GetAllCards")]
    //public IEnumerable<Card> Get()
    //{
    //    Console.WriteLine("All cards");
    //    return _db.Cards.ToList();
    //}


    [HttpGet("{id}")]
    public async Task<ActionResult<Card>> GetOneCard(long id)
    {
        var card = await _db.Cards.FindAsync(id);

        if (card == null)
        {
            return NotFound();
        }

        return card;
    }

    [HttpPost(Name ="CreateACard")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Card>> PostNewCard([FromBody] Card card)
    {
        _db.Cards.Add(card);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetOneCard), new { id = card.Id }, card);

    }
}

