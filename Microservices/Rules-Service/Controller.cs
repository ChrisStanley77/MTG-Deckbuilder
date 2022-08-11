using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net;
using System.Collections.Generic;


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
        public void StandardRules(List<string> cards){
            bool isValidSize = false;
            bool isValidCardNum = false;
            //Check Total Deck Size
            if(cards.Count() == 60 ){
                isValidSize = true;
            }

            List<string> refList = new List<string> {"Forest", "Mountain", "Island", "Plains", "Wastes"};
            Dictionary<string, int> allCards = new Dictionary<string, int>();
            //Check if there are only 4 or less copies of a card in the deck
            foreach(string card in cards){
                if(allCards.ContainsKey(card) == true){
                    if(allCards[card] > 4 && !refList.Contains(card)){
                        isValidCardNum = false;
                    }
                    else{
                        isValidCardNum = true;
                    }
                    allCards[card] += 1;
                }
                else{
                    allCards.Add(card, 1);
                }
            }
            System.Console.WriteLine(cards.Count());
            foreach(KeyValuePair<string, int> ele2 in allCards)
            {
                Console.WriteLine("{0} : {1}", ele2.Key, ele2.Value);
            }
            System.Console.WriteLine("Deck has 60 cards : {0}", isValidSize);
            System.Console.WriteLine("Deck has only 4 or less copies of each card : {0}", isValidCardNum);
        }
    }
}