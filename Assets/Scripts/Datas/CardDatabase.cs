using System.Collections.Generic;
using UnityEngine;

public class CardDatabase : MonoBehaviour
{
    [SerializeField] private Card_Base[] cardDatas;
    [SerializeField] private Card_Base[] statusCardDatas;
    private Dictionary<int, Card_Base> _cardDatasDictionary;

    public static CardDatabase instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        _cardDatasDictionary = new Dictionary<int, Card_Base>();
        foreach (var cardData in cardDatas)
        {
            _cardDatasDictionary.Add(cardData.name.GetHashCode(), cardData);
        }

        foreach (var statusCardData in statusCardDatas)
        {
            _cardDatasDictionary.Add(statusCardData.name.GetHashCode(), statusCardData);
        }
    }

    //public Card GetCard(int index)
    //{
    //    return cardDatas[index].CreateCard();
    //}

    public Card GetCard(string name)
    {
        return _cardDatasDictionary[name.GetHashCode()].CreateCard();
    }

    public Card GetRandomRewardCard()
    {
        return cardDatas[Random.Range(0, cardDatas.Length)].CreateCard();
    }
}
