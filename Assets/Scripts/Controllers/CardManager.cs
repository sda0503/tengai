using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    // 카드 추가, 카드 제거, 드로우 
    public static CardManager instance;

    public List<Card> deck;
    public List<Card> garbages;
    public List<Card> extinguishedCards;

    public HandManager _handManager;

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
        deck = new List<Card>();
        garbages = new List<Card>();
        extinguishedCards = new List<Card>();
        instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < cardDatas.Length; i++)
        {
            for(int j = 0; j < 2; j++)
            {
                AddCard(cardDatas[i].CreateCard());
            }
        }

        _handManager.ConnectCardManager(this);
    }

    private void Update()
    {
        
    }

    public void AddCard(Card card)
    {
        deck.Add(card);
        SetDeckNumText();
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

            if(deck.Count >= 1 && _handManager.hands.Count < 10)
            {
                Card card = deck[Random.Range(0, deck.Count)];

                yield return StartCoroutine(_handManager.DrawCard(card));
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

    private bool IsMeetUseCondition(Card card)
    {
        switch (card.CardData.useCondition)
        {
            case UseCondition.Target:
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                
                if(hit.collider != null)
                {
                    if (hit.collider.gameObject.CompareTag("Monster"))
                        return true;
                }
                break;
            case UseCondition.NonTarget:
                if(Input.mousePosition.y > 500)
                {
                    return true;
                }
                break;
        }

        return false;
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
