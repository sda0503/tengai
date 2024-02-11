using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UseCondition
{
    Target,
    NonTarget
}

public abstract class Card_Base : ScriptableObject
{
    [Header("Card Info")]
    public int cost;
    public Sprite cardSprite;
    public string name;
    [Multiline]
    public string description;
    public UseCondition condition;

    public abstract Card CreateCard();
}
