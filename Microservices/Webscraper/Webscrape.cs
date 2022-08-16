using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Microsoft.Extensions.Options;
using Service;
using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("webscrape")]
public class search
{
    private readonly CardService _card;
    public search(CardService cardService)
    {
        _card = cardService;
    }
    //"name":"Dawnhart Wardens",
    string rName = @"(?:(?:""name"":"")([A-Za-z0-9,.!@#?$\s'\\""-:×Þß÷þøÀ-ÿ]*)(?:"",)""lang"":)";
    string rManaCost = @"(?:(?:""mana_cost"":"")([A-Za-z0-9,!@#$\s\{\}\/]*)(?:"",))";
    string rCmc = @"(?:(?:""cmc"":)([A-Za-z0-9,!@#$\s\{\}\.]*)(?:,))";
    string rTypeLine = @"(?:(?:""type_line"":"")([A-Za-z0-9!@#$\s\{\}\.\W]*)(?:"",\W))";
    string rOracleText = @"(?:(?:""oracle_text"":"")([A-Za-z0-9,!@#$\s\{\}\.\-\W]*)(?:"",))";
    string rPower = @"(?:(?:""power"":"")([A-Za-z0-9!@#$\s\{\}\.\-]*)(?:"",))";
    string rToughness = @"(?:(?:""toughness"":"")([A-Za-z0-9!@#$\s\{\}\.\-]*)(?:"",))";
    string rColors = @"(?:(?:""colors"":)\[([A-Za-z0-9!@#$,\s\{\}\.\-\W]*)\](?:,))";
    string rSetName = @"(?:(?:""set_name"":"")([A-Za-z0-9!@#$,\s\{\}\.\-\W]*)(?:"",))";
    string rRarity = @"(?:(?:""rarity"":"")([A-Za-z0-9!@#$\s\{\}\.\-\W]*)(?:"",))";
    string rFlavorText = @"(?:(?:""flavor_text"":"")([A-Za-z0-9!,@#$,\s\{\}\.\-""\W]*)(?:"",))";
    string rPng = @"(?:(?:""png"":)""([A-Za-z0-9!@#$,\s\{\}\.\-\W]*)""(?:,))";
    List<Card> cards = new List<Card>();
    //Mongo Connections
    public async void getCards()
    {
        //Clear MongoDB to store fresh data
        if(!File.Exists("cards.json"))
        {
            using (var client = new WebClient())
            {
                client.DownloadFile("https://c2.scryfall.com/file/scryfall-bulk/default-cards/default-cards-20220812090559.json", "cards.json");
            }
        }
        
        List<string> jsonStrings = File.ReadAllLines("cards.json").ToList();
        foreach(string a in jsonStrings)
        {
            Card ca = new Card();
            //NAME
            Regex r = new Regex(rName);
            MatchCollection mc = r.Matches(a);
            if(mc.Count() != 0){
                ca.Name=mc[0].Value.Remove(0,8);
                if(ca.Name.Contains("lang"))
                {
                    ca.Name = ca.Name.Remove(ca.Name.Length-9);
                }

                //MANA COST
                r = new Regex(rManaCost);
                Match m = r.Match(a);
                ca.ManaCost = m.Groups[1].Value;
                ca.ManaCost = ca.ManaCost.Replace("{","");
                ca.ManaCost = ca.ManaCost.Replace("}","");
                if(ca.ManaCost.Equals(""))
                {
                    ca.ManaCost = "0";
                }
                //CMC
                r = new Regex(rCmc);
                m = r.Match(a);
                ca.ConvertedManaCost = m.Groups[1].Value;
                //TYPE LINE
                r = new Regex(rTypeLine);
                m = r.Match(a);
                ca.Type = m.Groups[1].Value;
                if(ca.Type.Contains(@""""))
                {
                    ca.Type = ca.Type.Remove(ca.Type.IndexOf(@""""));
                }
                //ORACLE TEXT
                r = new Regex(rOracleText);
                m = r.Match(a);
                ca.OracleText = m.Groups[1].Value;
                if(ca.OracleText.Contains(@""""))
                {
                    ca.OracleText = ca.OracleText.Remove(ca.OracleText.IndexOf(@""""));
                }
                //POWER
                r = new Regex(rPower);
                m = r.Match(a);
                ca.Power = m.Groups[1].Value;
                //TOUGHNESS
                r = new Regex(rToughness);
                m = r.Match(a);
                ca.Toughness = m.Groups[1].Value;
                //COLORS
                r = new Regex(rColors);
                m = r.Match(a);
                ca.CardColors = m.Groups[1].Value;
                ca.CardColors = ca.CardColors.Replace(@"""", "");
                if(ca.CardColors.Equals(""))
                {
                    ca.CardColors = "COLORLESS";
                }
                //SET NAME
                r = new Regex(rSetName);
                m = r.Match(a);
                ca.Set = m.Groups[1].Value;
                //RARITY
                r = new Regex(rRarity);
                m = r.Match(a);
                ca.Rarity = m.Groups[1].Value;
                if(ca.Rarity.Contains(@""""))
                {
                    ca.Rarity = ca.Rarity.Remove(ca.Rarity.IndexOf(@""""));
                }
                //FLAVOR TEXT
                r = new Regex(rFlavorText);
                m = r.Match(a);
                ca.FlavorText = m.Groups[1].Value;
                //PNG URL
                r = new Regex(rPng);
                m = r.Match(a);
                ca.Image = m.Groups[1].Value;
                cards.Add(ca);
            }
            
            // System.Console.WriteLine(ca.pngUrl);

        }
        File.Delete("cards.json");
        foreach(Card c in cards)
        {
                // using (var client = new WebClient())
                // {
                //     string imgPath =  $@"img\{c.name}.png";
                //     client.DownloadFile($@"""{c.pngUrl}""", imgPath);
                // }
            await _card.CreateAsync(c);
        }
    }
}

public class Card
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = null!;
    public string Image { get; set;} = null!;
    public string Name { get; set;} = null!;
    public string? ManaCost { get; set;} = null!;
    public string? ConvertedManaCost { get; set;} = null;
    public string CardColors { get; set;} = null!;
    public string Type { get; set;} = null!;
    public string OracleText { get; set;} = null!;
    public string FlavorText { get; set;} = null!;
    public string Set { get; set;} = null!;
    public string Rarity { get; set;} = null!;
    public string? Power { get; set; }
    public string? Toughness { get; set;}
}