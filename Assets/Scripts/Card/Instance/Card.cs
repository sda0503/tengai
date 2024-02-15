public class Card
{
    public Card_Base CardData {  get; private set; }

    public Card(Card_Base cardData) => CardData = cardData;
}
