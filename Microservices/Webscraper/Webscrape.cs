using System.Net;

public class search
{
    HttpClient client = new HttpClient();
    public async void getCards()
    {
        if(File.Exists("cards.json"))
        {
            File.Delete("cards.json");
        }
        using (var client = new WebClient())
        {
            client.DownloadFile("https://c2.scryfall.com/file/scryfall-bulk/all-cards/all-cards-20220811091559.json", "cards.json");
        }
        

    }
}