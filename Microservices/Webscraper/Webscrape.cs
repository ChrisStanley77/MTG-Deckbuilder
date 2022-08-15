using System.Net;
using System.Text;
using System.Text.RegularExpressions;
public class search
{
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
    List<card> cards = new List<card>();
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
            card ca = new card();
            //NAME
            Regex r = new Regex(rName);
            MatchCollection mc = r.Matches(a);
            if(mc.Count() != 0){
                ca.name=mc[0].Value.Remove(0,8);
                if(ca.name.Contains("lang"))
                {
                    ca.name = ca.name.Remove(ca.name.Length-9);
                }

                //MANA COST
                r = new Regex(rManaCost);
                Match m = r.Match(a);
                ca.mana_cost = m.Groups[1].Value;
                ca.mana_cost = ca.mana_cost.Replace("{","");
                ca.mana_cost = ca.mana_cost.Replace("}","");
                if(ca.mana_cost.Equals(""))
                {
                    ca.mana_cost = "0";
                }
                //CMC
                r = new Regex(rCmc);
                m = r.Match(a);
                ca.cmc = m.Groups[1].Value;
                //TYPE LINE
                r = new Regex(rTypeLine);
                m = r.Match(a);
                ca.type_line = m.Groups[1].Value;
                if(ca.type_line.Contains(@""""))
                {
                    ca.type_line = ca.type_line.Remove(ca.type_line.IndexOf(@""""));
                }
                //ORACLE TEXT
                r = new Regex(rOracleText);
                m = r.Match(a);
                ca.oracle_text = m.Groups[1].Value;
                if(ca.oracle_text.Contains(@""""))
                {
                    ca.oracle_text = ca.oracle_text.Remove(ca.oracle_text.IndexOf(@""""));
                }
                //POWER
                r = new Regex(rPower);
                m = r.Match(a);
                ca.power = m.Groups[1].Value;
                //TOUGHNESS
                r = new Regex(rToughness);
                m = r.Match(a);
                ca.toughness = m.Groups[1].Value;
                //COLORS
                r = new Regex(rColors);
                m = r.Match(a);
                ca.colors = m.Groups[1].Value;
                ca.colors = ca.colors.Replace(@"""", "");
                if(ca.colors.Equals(""))
                {
                    ca.colors = "COLORLESS";
                }
                //SET NAME
                r = new Regex(rSetName);
                m = r.Match(a);
                ca.set_name = m.Groups[1].Value;
                //RARITY
                r = new Regex(rRarity);
                m = r.Match(a);
                ca.rarity = m.Groups[1].Value;
                if(ca.rarity.Contains(@""""))
                {
                    ca.rarity = ca.rarity.Remove(ca.rarity.IndexOf(@""""));
                }
                //FLAVOR TEXT
                r = new Regex(rFlavorText);
                m = r.Match(a);
                ca.flavor_text = m.Groups[1].Value;
                //PNG URL
                r = new Regex(rPng);
                m = r.Match(a);
                ca.pngUrl = m.Groups[1].Value;
                cards.Add(ca);
            }
            
            // System.Console.WriteLine(ca.pngUrl);

        }
        // File.Delete("cards.json");
        // foreach(card c in cards)
        // {
            
        //         using (var client = new WebClient())
        //         {
        //             string imgPath =  $@"img\{c.name}.png";
        //             client.DownloadFile($@"""{c.pngUrl}""", imgPath);
        //         }
        // }
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
    public string pngUrl;
    
}