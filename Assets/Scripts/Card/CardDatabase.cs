using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDatabase : MonoBehaviour
{
    [SerializeField] private Card_Base[] cardDatas;
    Dictionary<int, Card_Base> cardDatasDictionary;

    public static CardDatabase instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        cardDatasDictionary = new Dictionary<int, Card_Base>();
        foreach (var cardData in cardDatas)
        {
            cardDatasDictionary.Add(cardData.name.GetHashCode(), cardData);
        }
    }

    public Card GetCard(int index)
    {
        return cardDatas[index].CreateCard();
    }

    public Card GetCard(string name)
    {
        return cardDatasDictionary[name.GetHashCode()].CreateCard();
    }

    public Card GetRandomCard()
    {
        return cardDatas[Random.Range(0, cardDatas.Length)].CreateCard();
    }
}
