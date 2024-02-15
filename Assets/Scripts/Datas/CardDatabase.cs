using System.Collections.Generic;
using UnityEngine;

public class CardDatabase : MonoBehaviour
{
    [SerializeField] private Card_Base[] cardDatas;
    [SerializeField] private Card_Base[] statusCardDatas;
    [SerializeField] private Card_Base[] defaultCardDatas;
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

        foreach (var defaultCardData in defaultCardDatas)
        {
            _cardDatasDictionary.Add(defaultCardData.name.GetHashCode(), defaultCardData);
        }

        //InitPlayerDeck();
    }

    private void InitPlayerDeck()
    {
        for (int i = 0; i < 5; i++)
        {
            CardManager.instance.AddCardToOriginal(GetCard("Card_Attack_AttackBasic"));
        }
        for (int i = 0; i < 4; i++)
        {
            CardManager.instance.AddCardToOriginal(GetCard("Card_Skill_DefenceBasic"));
        }
        CardManager.instance.AddCardToOriginal(GetCard("Card_Attack_Bash"));
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
