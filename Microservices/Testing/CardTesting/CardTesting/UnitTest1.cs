namespace CardTesting;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void CreateCardTest()
    {
        Card card = new Card();
        card.Name = "Alharu, Solemn Ritualist";
        card.Image = "https://c1.scryfall.com/file/scryfall-cards/large/front/2/0/2047bfa4-3f4d-47bd-9484-545686f15b75.jpg?1608908659";
        card.ConvertedManaCost = 5;
        card.CardColors = new List<string> {"White"};
        card.Type = "Legendary Creature - Human Monk";
        card.Set = "Commander Legends (CMR)";
        card.Power = "3";
        card.Toughness = "3";
        card.Text = @"When Alharu, Solemn Ritualist enters the battlefield, put a +1/+1 counter on each of up to two other target creatures.
                    Whenever a nontoken creature you control with a +1/+1 counter on it dies, create a 1/1 white Spirit creature token with flying.
                    Partner (You can have two commanders if both have partner.)";
        
        var result = CardController.MakeCard(card);
        Assert.AreEqual(201, result);
    }
}