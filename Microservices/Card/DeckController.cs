using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using Services;

namespace Controllers
{
    [ApiController]
    [Route("deck")]
    
    public class MyDeckController : ControllerBase
    {
        private readonly DeckService _deck;
        public MyDeckController(DeckService deckService)
        {
            _deck = deckService;
        }

//////////////////////// All Gets //////////////////////////////////////////////////////////////////////////////////////////////

        //Get all decks
        [HttpGet]
        [Route("getallitems")]
        public async Task<List<Deck>> GetAllItems() =>
            await _deck.GetAsync();

        //Get deck by specified id
        [HttpGet("{id}")]
        public async Task<ActionResult<Deck>> GetById(string id)
        {
            var TempItem = await _deck.GetIdAsync(id);

            if(TempItem is null)
            {
                return NotFound();
            }
            return TempItem;
        }

        //Get deck by specified name
        [HttpGet]
        [Route("getbyname/{name}")]
        public async Task<ActionResult<Deck>> GetByName(string name)
        {
            var TempItem = await _deck.GetNameAsync(name);

            if(TempItem is not null)
            {
                return TempItem;
            }
            return NotFound();
        }

///////////////////////////////// All Posts ////////////////////////////////////////////////////////////////////////////////////

        //Make a deck
        [HttpPost]
        [Route("makeitem")]
        public async Task<IActionResult> MakeDeck(Deck deck)
        {
            await _deck.CreateAsync(deck);
            return CreatedAtAction(nameof(GetAllItems), new {id = deck.Id}, deck);
        }

/////////////////////////////// All Updates /////////////////////////////////////////////////////////////////////////////////

        //Update Deck by a specified name
        [HttpPut]
        [Route("{name}")]
        public async Task<IActionResult> UpdateDeck(string name, Deck deck)
        {
            var TempItem = await _deck.GetNameAsync(name);

            if(TempItem!.Id is not null)
            {
                deck.Id = TempItem.Id;
                await _deck.UpdateAsync(deck.Id, deck);
                return NoContent();
            }         
            return NotFound();
        }

////////////////////////////// All Deletes //////////////////////////////////////////////////////////////////////////////////

        //Delete deck by specified id
        [HttpDelete]
        [Route("{name}")]
        public async Task<IActionResult> DeleteDeck(string name)
        {
            var TempItem = await _deck.GetNameAsync(name);

            if(TempItem is not null)
            {
                await _deck.RemoveAsync(TempItem.Name);
                return NoContent();
            }
            return NotFound();
        } 

        //Delete all decks
        [HttpDelete]
        [Route("deleteall")]
        public async Task<IActionResult> DeleteAllDecks()
        {
            await _deck.RemoveAllAsync();
            return NoContent();
        }
    }
}