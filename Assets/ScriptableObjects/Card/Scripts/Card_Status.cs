using UnityEngine;

[CreateAssetMenu(fileName = "Card_Status_", menuName = "Card/StatusCard")]
public class Card_Status : Card_Base
{
    public override Card CreateCard()
    {
        return new StatusCard(this);
    }
}
