using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    // 카드 추가, 카드 제거, 드로우 
    public static CardManager instance;

    public List<Card> originalDeck;
    public List<Card> deck;
    public List<Card> garbages;
    public List<Card> extinguishedCards;

    public HandManager handManager;

    [SerializeField] private Card_Base[] cardDatas;

    [SerializeField] private GameObject cardPrefab;

    [SerializeField] private Canvas _mainCanvas;

    [SerializeField] private Text deckNumText;
    [SerializeField] private Text garbageNumText;
    [SerializeField] private Text extinguishedCardsNumText;

    private GraphicRaycaster _gr;
    private PointerEventData _ped;
    private List<RaycastResult> _rrList;

    private Card curSelectedCard;
    private GameObject curSelectedCardUI;

    private void Awake()
    {
        originalDeck = new List<Card>();
        deck = new List<Card>();
        garbages = new List<Card>();
        extinguishedCards = new List<Card>();
        instance = this;

        for (int i = 0; i < cardDatas.Length; i++)
        {
            for (int j = 0; j < 10; j++)
                AddCardToOriginal(cardDatas[i].CreateCard());
        }
    }

    private void Start()
    {
        handManager.ConnectCardManager(this);
    }

    public void AddCardToOriginal(Card card)
    {
        originalDeck.Add(card);
    }

    public void AddCard(Card card)
    {
        deck.Add(card);
        SetDeckNumText();
    }

    public void CopyFromOriginal()
    {
        deck = originalDeck.ToList();
    }

    public void DrawCard(int drawNum)
    {
        StartCoroutine(DrawCardC(drawNum));
    }

    IEnumerator DrawCardC(int drawNum)
    {
        for(int i = 0; i < drawNum; i++)
        {
            if(deck.Count <= 0)
            {
                ReloadAllCard();
                SetAllNumText();
            }

            if(deck.Count >= 1 && handManager.hands.Count < 10)
            {
                Card card = deck[Random.Range(0, deck.Count)];

                yield return StartCoroutine(handManager.DrawCard(card));
                deck.Remove(card);

                SetDeckNumText();
            }
        }
    }

    public void DropCard(Card card)
    {
        garbages.Add(card);
        SetGarbageNumText();
    }

    public void ExtingushCard(Card card)
    {
        extinguishedCards.Add(card);
        SetExtingushiedCardsNumText();
    }

    public void Clear()
    {
        deck.Clear();
        garbages.Clear();
        extinguishedCards.Clear();

        SetAllNumText();

        handManager.hands.Clear();
        handManager.StopAllCoroutines();

        Transform hand = handManager.transform;

        foreach(Transform child in hand)
        {
            Destroy(child.gameObject);
        }
    }

    private void SetDeckNumText()
    {
        deckNumText.text = deck.Count.ToString();
    }

    private void SetGarbageNumText()
    {
        garbageNumText.text = garbages.Count.ToString();
    }

    private void SetExtingushiedCardsNumText()
    {
        extinguishedCardsNumText.text = extinguishedCards.Count.ToString();
    }

    private void SetAllNumText()
    {
        SetDeckNumText();
        SetGarbageNumText();
        SetExtingushiedCardsNumText();
    }

    private void ReloadAllCard()
    {
        while(garbages.Count > 0)
        {
            deck.Add(garbages[0]);
            garbages.RemoveAt(0);
        }
    }
}
