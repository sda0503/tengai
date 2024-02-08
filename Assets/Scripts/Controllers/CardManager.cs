using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    // 카드 추가, 카드 제거, 드로우 

    public List<Card> deck;
    public List<Card> hands;
    public List<Card> garbages;

    [SerializeField] private Transform handParnet;

    [SerializeField] private Card_Base[] cardDatas;

    [SerializeField] private GameObject cardPrefab;

    private void Start()
    {
        for (int i = 0; i < cardDatas.Length; i++)
        {
            for(int j = 0; j < 5; j++)
            {
                AddCard(cardDatas[i].CreateCard());
            }
        }
    }

    public void AddCard(Card card)
    {
        deck.Add(card);
    }
    public void DrawCard()
    {
        if(deck.Count >= 1 && hands.Count < 10)
        {
            Card card = deck[Random.Range(0, deck.Count)];

            GameObject go = Instantiate(cardPrefab, handParnet);

            go.GetComponent<CardDisplay>().SetCard(card);

            hands.Add(card);
            deck.Remove(card);
        }
    }
}
