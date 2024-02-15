using UnityEngine;

[CreateAssetMenu(fileName = "Card_Attack_", menuName = "Card/AttackCard")]
public class Card_Attack : Card_Base
{
    public int attackValue;
    public override Card CreateCard()
    {
        return new AttackCard(this);
    }
}
