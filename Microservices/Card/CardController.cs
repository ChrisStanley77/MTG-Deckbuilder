using Microsoft.AspNetCore.Mvc;
using Service;
using System;
using System.Net;

namespace Controllers
{
    [ApiController]
    [Route("card")]
    
    public class MyController : ControllerBase
    {
        private readonly CardService _card;
        public MyController(CardService cardService)
        {
            _card = cardService;
        }

//////////////////////// All Gets //////////////////////////////////////

        //Get IP Address
        [HttpGet]
        [Route("getIP")]
        public string GetIP()
        {
            // Getting host name
            string host = Dns.GetHostName();
            
            // Getting ip address using host name
            IPHostEntry ip = Dns.GetHostEntry(host);
            return ip.AddressList[0].ToString();
        }

        //Get all cards
        [HttpGet]
        [Route("getallitems")]
        public async Task<List<Card>> GetAllItems() =>
            await _card.GetAsync();

        //Get all cards that have a specific type
        [HttpGet]
        [Route("getbytype/{type}")]
        public async Task<List<Card>> GetByType(string type) =>
            await _card.GetTypeAsync(type);

        //Get all cards that are in a specific set
        [HttpGet]
        [Route("getbyset/{set}")]
        public async Task<List<Card>> GetBySet(string set) =>
            await _card.GetSetAsync(set);

        //Get card by id
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

        //Get card by name
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



///////////////////////////////// All Posts /////////////////////////////

        //Make a card
        [HttpPost]
        [Route("makeitem")]
        public async Task<IActionResult> MakeItem(Card card)
        {           
            if(card.Name is not null && card.Image is not null && card.Mana is not null && card.Set is not null && card.Text is not null &&
            card.Type is not null)
            {
                if(!card.Name.Equals(""))
                {
                    if(!card.Image.Equals(""))
                    {
                        if(!card.Mana.Equals(""))
                        {
                            if(!card.Text.Equals(""))
                            {
                                if(!card.Type.Equals(""))
                                {
                                    if(!card.Set.Equals(""))
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
            
            return Conflict();
        }

/////////////////////////////// All Updates /////////////////////////////

        //Update card by id
        [HttpPut]
        [Route("{name}")]
        public async Task<IActionResult> UpdateItem(string name, Card card)
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

////////////////////////////// All Deletes //////////////////////////////

        //Delete card by id
        [HttpDelete]
        [Route("{name}")]
        public async Task<IActionResult> DeleteItem(string name)
        {
            //var DatabaseItem = await _item.GetIdAsync(id);
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