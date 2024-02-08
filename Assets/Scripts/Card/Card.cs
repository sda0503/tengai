using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Card : MonoBehaviour 
{
    public Card_Base CardData {  get; private set; }

    public Card(Card_Base cardData) => CardData = cardData;
}
