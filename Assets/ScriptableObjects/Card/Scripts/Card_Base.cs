using System.Collections.Generic;
using UnityEngine;

public enum UseCondition
{
    None,
    Target,
    NonTarget,
    Player,
}

public enum ExtinctionType
{
    None,
    Extinction,
    Volatilization
}

public class Card_Base : ScriptableObject
{
    [Header("Card Info")]
    public int cost;
    public Sprite cardSprite;
    public string cardName;
    [Multiline]
    public string description;
    public UseCondition useCondition;
    public ExtinctionType extinctionType;
    public List<CardEffect_Attack> attackEffects;
    public List<CardEffect_Draw> drawEffects;
    public List<CardEffect_Stat> statEffects;
    public List<CardEffect_AddCard> addCards;

    public virtual Card CreateCard()
    {
        return new Card(this);
    }
}
