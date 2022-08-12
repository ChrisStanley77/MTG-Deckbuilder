using Microsoft.AspNetCore.Mvc;
using Service;
using System;
using System.Net;

namespace Controllers
{
    [ApiController]
    [Route("card")]
    
    public class MyCardController : ControllerBase
    {
        private readonly CardService _card;
        public MyCardController(CardService cardService)
        {
            _card = cardService;
        }

//////////////////////// All Gets //////////////////////////////////////////////////////////////////////////////////////////////

        //Get all cards
        [HttpGet]
        [Route("getallitems")]
        public async Task<List<Card>> GetAllItems() =>
            await _card.GetAsync();

        //Get all cards with specified type
        [HttpGet]
        [Route("getbytype/{type}")]
        public async Task<List<Card>> GetByType(string type) =>
            await _card.GetTypeAsync(type);

        //Get all cards in specified set
        [HttpGet]
        [Route("getbyset/{set}")]
        public async Task<List<Card>> GetBySet(string set) =>
            await _card.GetSetAsync(set);

        //Get all cards by specified color
        [HttpGet]
        [Route("getbycolor/{color}")]
        public async Task<List<Card>> GetByColor(string color) =>
            await _card.GetCardByColorsAsync(color);

        //Get all card by specified converted mana cost
        [HttpGet]
        [Route("getbycmc/{cost}")]
        public async Task<List<Card>> GetByConvertedManaCost(int convertedManaCost) =>
            await _card.GetCardCostAsync(convertedManaCost);

        //Get card by specified id
        [HttpGet("{id}")]
        public async Task<ActionResult<Card>> GetById(string id)
        {
            var TempItem = await _card.GetIdAsync(id);

            if(TempItem is null)
            {
                return NotFound();
            }
            return TempItem;
        }

        //Get card by specified name
        [HttpGet]
        [Route("getbyname/{name}")]
        public async Task<ActionResult<Card>> GetByName(string name)
        {
            var TempItem = await _card.GetNameAsync(name);

            if(TempItem is not null)
            {
                return TempItem;
            }
            return NotFound();
        }

///////////////////////////////// All Posts ////////////////////////////////////////////////////////////////////////////////////

        //Make a card
        [HttpPost]
        [Route("makeitem")]
        public async Task<IActionResult> MakeCard(Card card)
        {
            if(card.Name is not null && card.Image is not null && card.Set is not null && card.Text is not null &&
            card.Type is not null && card.CardColors is not null)
            {
                if(!card.Name.Equals(""))
                {
                    if(!card.Image.Equals(""))
                    {
                        if(!card.Text.Equals(""))
                        {
                            if(!card.Type.Equals(""))
                            {
                                if(!card.Set.Equals(""))
                                {
                                    if(card.ConvertedManaCost >= 0)
                                    {
                                        int AllColorsInList = card.CardColors.Count();
                                        int ValidColors = 0;
                                        foreach(string color in card.CardColors)
                                        {
                                            if(!color.Equals(""))
                                            {
                                                ValidColors++;
                                            }
                                        }
                                        if(ValidColors == AllColorsInList)
                                        {
                                            await _card.CreateAsync(card);
                                            return CreatedAtAction(nameof(GetAllItems), new {id = card.Id}, card);
                                        }                                       
                                    }
                                }
                            }
                        }
                    }
                }               
            }            
            return Conflict();
        }

/////////////////////////////// All Updates /////////////////////////////////////////////////////////////////////////////////

        //Update card by a specified name
        [HttpPut]
        [Route("{name}")]
        public async Task<IActionResult> UpdateCard(string name, Card card)
        {
            //var TempItem = await _item.GetIdAsync(id);
            var TempItem = await _card.GetNameAsync(name);

            if(TempItem!.Id is not null)
            {
                card.Id = TempItem.Id;
                await _card.UpdateAsync(card.Id, card);
                return NoContent();
            }         
            return NotFound();
        }

////////////////////////////// All Deletes //////////////////////////////////////////////////////////////////////////////////

        //Delete card by specified id
        [HttpDelete]
        [Route("{name}")]
        public async Task<IActionResult> DeleteCard(string name)
        {
            var TempItem = await _card.GetNameAsync(name);

            if(TempItem is not null)
            {
                await _card.RemoveAsync(TempItem.Name);
                return NoContent();
            }
            return NotFound();
        } 

        //Delete all cards
        [HttpDelete]
        [Route("deleteall")]
        public async Task<IActionResult> DeleteAllCards()
        {
            await _card.RemoveAllAsync();
            return NoContent();
        }
    }
}