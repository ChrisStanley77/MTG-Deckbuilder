using System.Net;
using System.Text;
using System.Text.RegularExpressions;
        /////////NAME//////////
        //(?:(?:\"name\":\")([A-Za-z0-9,!@#$\s]*)(?:\",))
        /////////MANACOST//////////
        //(?:(?:\"mana_cost\":\")([A-Za-z0-9,!@#$\s\{\}]*)(?:\",))
        /////////CMC//////////
        //(?:(?:\"cmc\":)([A-Za-z0-9,!@#$\s\{\}\.]*)(?:,))
        /////////TYPE LINE//////////
        //(?:(?:\"type_line\":\")([A-Za-z0-9,!@#$\s\{\}\.\-\W]*)(?:\",))
        /////////ORACLE TEXT//////////
        //(?:(?:\"oracle_text\":\")([A-Za-z0-9,!@#$\s\{\}\.\-\W]*)(?:\",))
        /////////POWER//////////
        //(?:(?:\"power\":\")([A-Za-z0-9!@#$\s\{\}\.\-]*)(?:\",))
        /////////TOUGHNESS//////////
        //(?:(?:\"toughness\":\")([A-Za-z0-9!@#$\s\{\}\.\-]*)(?:\",))
        /////////COLORS//////////
        //(?:(?:\"colors\":)\[([A-Za-z0-9!@#$,\s\{\}\.\-\W]*)\](?:,))
            //NEED TO REPLACE " with string.Empty
        /////////SET NAME//////////
        //(?:(?:\"set_name\":\")([A-Za-z0-9!@#$,\s\{\}\.\-\W]*)(?:\",))
        /////////RARITY//////////
        //(?:(?:\"rarity\":\")([A-Za-z0-9!@#$,\s\{\}\.\-\W]*)(?:\",))
        /////////FLAVOR TEXT//////////
        //(?:(?:\"flavor_text\":\")([A-Za-z0-9!,@#$,\s\{\}\.\-\"\W]*)(?:\",))
        /////////PNG//////////
        //(?:(?:\"png\":)\"([A-Za-z0-9!@#$,\s\{\}\.\-\W]*)\"(?:,))


public class search
{
    string rName = @"(?:(?:\"name\":\")([A-Za-z0-9,!@#$\s]*)(?:\",))";
    string rManaCost = @"(?:(?:\"mana_cost\":\")([A-Za-z0-9,!@#$\s\{\}]*)(?:\",))";
    string rCmc = @"(?:(?:\"cmc\":)([A-Za-z0-9,!@#$\s\{\}\.]*)(?:,))";
    string rTypeLine = @"(?:(?:\"type_line\":\")([A-Za-z0-9,!@#$\s\{\}\.\-\W]*)(?:\",))";
    string rOracleText = @"(?:(?:\"oracle_text\":\")([A-Za-z0-9,!@#$\s\{\}\.\-\W]*)(?:\",))";
    string rPower = @"(?:(?:\"power\":\")([A-Za-z0-9!@#$\s\{\}\.\-]*)(?:\",))";
    string rToughness = @"(?:(?:\"toughness\":\")([A-Za-z0-9!@#$\s\{\}\.\-]*)(?:\",))";
    string rColors = @"(?:(?:\"colors\":)\[([A-Za-z0-9!@#$,\s\{\}\.\-\W]*)\](?:,))";
    string rSetName = @"(?:(?:\"set_name\":\")([A-Za-z0-9!@#$,\s\{\}\.\-\W]*)(?:\",))";
    string rRarity = @"(?:(?:\"rarity\":\")([A-Za-z0-9!@#$,\s\{\}\.\-\W]*)(?:\",))";
    string rFlavorText = @"(?:(?:\"flavor_text\":\")([A-Za-z0-9!,@#$,\s\{\}\.\-\"\W]*)(?:\",))";
    string rPng = @"(?:(?:\"png\":)\"([A-Za-z0-9!@#$,\s\{\}\.\-\W]*)\"(?:,))";
    HttpClient client = new HttpClient();
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
            Console.WriteLine(a + "\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n");
        }
        
        // File.Delete("cards.json");
    }
}

public class card
{
    public string name;
    public string mana_cost;
    public string cmc;
    public string type_line;
    public string oracle_text;
    public string power;
    public string toughness;
    public string colors;
    public string set_name;
    public string rarity;
    public string flavor_text;
}