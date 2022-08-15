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
        public ActionResult<Boolean> StandardRules(List<string> deck){
            bool isValidSize = false;
            bool isValidCardNum = CheckCardAmounts(4, deck);
            //Check Total Deck Size
            if(deck.Count() == 60 ){
                isValidSize = true;
            }
            
            System.Console.WriteLine("Deck has 60 cards : {0}", isValidSize);
            System.Console.WriteLine("Deck size : {0}/60", deck.Count());
            if(isValidCardNum && isValidSize){
                return true;
            }
            else{
                return false;
            }
        }


        [HttpPost]
        [Route("commander")]
        public ActionResult<Boolean> CommanderRules(List<Card> deck){
            bool isValidSize = false;
            bool isValidCardNum = false;
            bool isValidColors = false;
            Card commander = deck[0];

            //Check Total Deck Size
            if(deck.Count() == 100 ){
                isValidSize = true;
            }

            //Check if all cards are the same colors as commander
            List<Card> invalidColoredCards = new List<Card>();
            System.Console.WriteLine(commander.ToString());
            foreach(Card card in deck){
                if(!card.Type.Contains("Land")){
                    var cardIsInCommanderColors = card.CardColors.Any(x => commander.CardColors.Any(y => y == x));
                    System.Console.WriteLine("Card : {0}\nCard Colors : {1}\n Shares Colors : {2}", card.Name, card.CardColors[0].ToString(), cardIsInCommanderColors);
                    if(!cardIsInCommanderColors){
                        invalidColoredCards.Add(card);
                    }
                }
            }

            System.Console.WriteLine("Deck has 100 cards : {0}", isValidSize);
            System.Console.WriteLine("Deck size : {0}/100", deck.Count());

            if(isValidCardNum && isValidSize && isValidColors){
                return true;
            }
            else{
                return false;
            }
        }

        //Helper Methods
        public bool CheckCardAmounts(int maxAmount, List<string> deck){
            bool isValidNum = false;
            List<string> refList = new List<string> {"Forest", "Mountain", "Island", "Plains", "Swamp", "Wastes"};
            Dictionary<string, int> allCards = new Dictionary<string, int>();
            //Check if there are only 4 or less copies of a card in the deck
            foreach(string card in deck){
                if(allCards.ContainsKey(card) == true){
                    if(allCards[card] > 4 && !refList.Contains(card)){
                        isValidNum = false;
                    }
                    else{
                        isValidNum = true;
                    }
                    allCards[card] += 1;
                }
                else{
                    allCards.Add(card, 1);
                }
            } 
            System.Console.WriteLine(deck.Count());
            foreach(KeyValuePair<string, int> ele2 in allCards)
            {
                Console.WriteLine("{0} : {1}", ele2.Key, ele2.Value);
            }
            System.Console.WriteLine("Deck has only 4 or less copies of each card : {0}", isValidNum);
            return isValidNum;
        }
    }
}