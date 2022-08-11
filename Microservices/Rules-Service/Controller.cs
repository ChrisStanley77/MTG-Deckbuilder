using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net;
using System.Collection.Generic;


namespace Controllers
{
    [ApiController]
    [Route("rules")]
    public class Controller : ControllerBase
    {

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

        [HttpGet]
        [Route("help")]
        public ActionResult<String> AccountHelp(){
            return "End Point URI's\n1.";
        }

        [HttpPost]
        [Route("standard")]
        public ActionResult<String> StandardRules(List<string> cards){
            bool isValidSize = false;
            bool isValidCardNum = false;
            //Check Total Deck Size
            if(cards.Size > 60 || cards.Size < 60 ){
                isValidSize = true;
            }

            List<string> refList = new List<string> ("Forest", "Mountain", "Island", "Plains", "Wastes");
            Dictionary allCards = new Dictionary<string, int>();
            //Check if there are only 4 or less copies of a card in the deck
            foreach(string card in cards){
                if(allCards.ContainsKey(card) == true){
                    if(allCards[card] == 4 && !refList.Contains(card)){

                    }
                    else{
                        allCards[card] += 1;
                    }
                }
                else{
                    allCards.add(card, 1);
                }
            }

        }
    }
}