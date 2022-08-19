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
        public ActionResult<Boolean> StandardRules(List<Card> deck){
            bool isValidSize = false;
            bool isValidCardNum = CheckCardAmounts(4, deck);
            //Check Total Deck Size
            if(deck.Count() == 60 ){
                isValidSize = true;
            }
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
            bool isValidCardNum = CheckCardAmounts(1, deck);
            bool isValidColors = false;
            List<string> CommanderColors = ChangeCardColorsToList(deck[0].CardColors, true);

            //Check Total Deck Size
            if(deck.Count() == 100 ){
                isValidSize = true;
            }

            //Check if all cards are the same colors as commander
            List<Card> invalidColoredCards = new List<Card>();
            foreach(Card card in deck){
                if(!card.Type.Contains("Land")){
                    var cardColors = ChangeCardColorsToList(card.CardColors, false);
                    var cardIsInCommanderColors = cardColors.Any(x => CommanderColors.Any(y => y == x));
                    if(!cardIsInCommanderColors){
                        invalidColoredCards.Add(card);
                    }
                }
            }
            if(isValidCardNum && isValidSize && isValidColors){
                return true;
            }
            else{
                return false;
            }
        }

        //Helper Methods
        public bool CheckCardAmounts(int maxAmount, List<Card> deck){
            bool isValidNum = false;
            List<string> refList = new List<string> {"Forest", "Mountain", "Island", "Plains", "Swamp", "Wastes"};
            Dictionary<string, int> allCards = new Dictionary<string, int>();
            //Check if there are only 4 or less copies of a card in the deck
            foreach(Card card in deck){
                if(allCards.ContainsKey(card.Name) == true){
                    if(allCards[card.Name] > maxAmount && !refList.Contains(card.Name)){
                        isValidNum = false;
                    }
                    else{
                        isValidNum = true;
                    }
                    allCards[card.Name] += 1;
                }
                else{
                    allCards.Add(card.Name, 1);
                }
            } 
            return isValidNum;
        }

        public List<string> ChangeCardColorsToList(string cardColors, bool isCommander){
            List<string> colors = cardColors.Split(',').ToList();
            if(!colors.Contains("C") && isCommander){
                colors.Add("C");
            }
            return colors;
        }
    }
}