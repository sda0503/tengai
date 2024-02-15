using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardCreatePlace
{
    Deck,
    Garbage
}
[System.Serializable]
public class CardEffect_AddCard : ICardEffect
{
    public string cardName;
    public CardCreatePlace cardCreatePlace;

    public void OnUse(StatSystem statSystem = null)
    {
        CardManager.instance.handManager.CreateCard(CardDatabase.instance.GetCard(cardName), cardCreatePlace);
    }

    public void OnUse(List<StatSystem> statSystemList)
    {
        throw new System.NotImplementedException();
    }
}
