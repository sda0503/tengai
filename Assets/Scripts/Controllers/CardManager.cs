using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    // 카드 추가, 카드 제거, 드로우 

    public List<Card> deck;
    public List<Card> garbages;

    public HandManager _handManager;

    [SerializeField] private Card_Base[] cardDatas;

    [SerializeField] private GameObject cardPrefab;

    [SerializeField] private Canvas _mainCanvas;

    [SerializeField] private Text deckNumText;
    [SerializeField] private Text garbageNumText;

    private GraphicRaycaster _gr;
    private PointerEventData _ped;
    private List<RaycastResult> _rrList;

    private Card curSelectedCard;
    private GameObject curSelectedCardUI;
    private void Awake()
    {
        deck = new List<Card>();
        garbages = new List<Card>();
        Init();
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

    private void Init()
    {
        _gr = _mainCanvas.GetComponent<GraphicRaycaster>();
        _ped = new PointerEventData(null);
        _rrList = new List<RaycastResult>(10);
    }

    private void Update()
    {
        
    }

    private T GetClickedUIObjectComponent<T>() where T : Component
    {
        _rrList.Clear();

        _gr.Raycast(_ped, _rrList);

        if(_rrList.Count == 0 )
        {
            return null;
        }

        return _rrList[0].gameObject.GetComponent<T>();
    }

    private void OnPointerDown()
    {
        if(Input.GetMouseButtonDown(0))
        {
            curSelectedCard = GetClickedUIObjectComponent<CardDisplay>()?.GetCard();
            curSelectedCardUI = GetClickedUIObjectComponent<CardDisplay>()?.gameObject;

            Debug.Log("OnClick");

            if (curSelectedCard != null )
            {
                Debug.Log(curSelectedCard.CardData.name);
            }
        }
    }

    //private void OnPointerUp()
    //{
    //    if (Input.GetMouseButtonUp(0))
    //    {
    //        if(curSelectedCard != null)
    //        {
    //            Debug.Log("End Click");
    //            if (IsMeetUseCondition(curSelectedCard))
    //            {
    //                UseCard();
    //            }
    //        }
    //    }
    //}

    public void AddCard(Card card)
    {
        deck.Add(card);
        SetDeckNumText();
    }

    public void DrawCard()
    {
        if(deck.Count <= 0)
        {
            ReloadAllCard();
            SetAllNumText();
        }

        if(deck.Count >= 1 && _handManager.hands.Count < 10)
        {
            Card card = deck[Random.Range(0, deck.Count)];

            StartCoroutine(_handManager.DrawCard(card));
            deck.Remove(card);

            SetDeckNumText();
        }
    }

    public void DropCard(Card card)
    {
        garbages.Add(card);
        SetGarbageNumText();
        Destroy(curSelectedCardUI);
    }

    private bool IsMeetUseCondition(Card card)
    {
        switch (card.CardData.condition)
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

    private void SetAllNumText()
    {
        SetDeckNumText();
        SetGarbageNumText();
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
