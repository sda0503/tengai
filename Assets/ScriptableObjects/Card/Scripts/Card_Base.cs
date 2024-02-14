using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UseCondition
{
    Target,
    NonTarget,
    Player,
}

public class Card_Base : ScriptableObject
{
    [Header("Card Info")]
    public int cost;
    public Sprite cardSprite;
    public string name;
    [Multiline]
    public string description;
    public UseCondition condition;
    public List<CardEffect_Attack> attackEffects;
    public List<CardEffect_Draw> drawEffects;
    public List<CardEffect_Stat> statEffects;

    public virtual Card CreateCard()
    {
        return new Card(this);
    }
}
