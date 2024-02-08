using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Card_Base : ScriptableObject
{
    [Header("Card Info")]
    public int Cost;
    public Sprite cardSprite;
    public string name;
    [Multiline]
    public string description;

    public abstract Card CreateCard();
}
