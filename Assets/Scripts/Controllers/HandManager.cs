using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HandManager : MonoBehaviour
{
    public List<CardDisplay> hands;

    [Header("Position For Bezier Curve")]
    public Transform P0;
    public Transform P1;
    public Transform P2;
    public Transform P3;

    public float angleBetweenCard;

    public float distanceBetweenCard1;
    public float distanceBetweenCard2;

    public GameObject cardPrefab;

    private CardManager _cardManager;

    private GraphicRaycaster _gr;
    private PointerEventData _ped;
    private List<RaycastResult> _rrList;

    private CardDisplay curSelectedCardDisplay;
    private Card curSelectedCard;
    private GameObject curSelectedCardGO;

    public Canvas _mainCanvas;

    private void Awake()
    {
        hands = new List<CardDisplay>();
        Init();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Init()
    {
        _gr = _mainCanvas.GetComponent<GraphicRaycaster>();
        _ped = new PointerEventData(null);
        _rrList = new List<RaycastResult>(10);
    }

    // Update is called once per frame
    void Update()
    {
        _ped.position = Input.mousePosition;
        //Debug.Log(Input.mousePosition);
        OnPointerDown();
        OnPointerUp();
    }

    public void DrawCard(Card card)
    {
        GameObject go = Instantiate(cardPrefab, this.transform);

        CardDisplay cardDisplay = go.GetComponent<CardDisplay>();

        cardDisplay.SetCard(card);

        hands.Add(cardDisplay);

        SortAllCard();
    }

    public void SortAllCard()
    {
        for(int i = 0; i < hands.Count; i++)
        {
            SetCurveRate(i);
            SetAngle(i);
            hands[i].targetPos = GetPositionFromBezierCurve(P0.localPosition, P1.localPosition, P2.localPosition, P3.localPosition, hands[i].curveRateInHand);
            hands[i].transform.localPosition = hands[i].targetPos;
            hands[i].transform.localRotation = Quaternion.Euler(new Vector3(0, 0, hands[i].angle));
        }
    }

    public void SetCurveRate(int index)
    {
        float interval = (hands.Count < 6) ? distanceBetweenCard1 : distanceBetweenCard2;

        if (hands.Count % 2 == 0)
        {
            int i = index - hands.Count / 2;
            hands[index].curveRateInHand = (float)i * interval + 0.55f;
        }
        else
        {
            int i = index - hands.Count / 2;
            hands[index].curveRateInHand = (float)i * interval + 0.5f;
        }
    }

    public void SetAngle(int index)
    {
        int center = hands.Count / 2;

        if (hands.Count % 2 == 1)
            hands[index].angle = (center - index) * angleBetweenCard;
        else
        {
            if (index < center)
                hands[index].angle = (float)(center - index - 1) * angleBetweenCard + angleBetweenCard/ 2f;
            else
                hands[index].angle = (float)(center - index) * angleBetweenCard - angleBetweenCard / 2;
        }
    }

    public Vector3 GetPositionFromBezierCurve(Vector3 P0, Vector3 P1, Vector3 P2, Vector3 P3, float t)
    {
        Vector3 M0 = Vector3.Lerp(P0, P1, t);
        Vector3 M1 = Vector3.Lerp(P1, P2, t);
        Vector3 M2 = Vector3.Lerp(P2, P3, t);

        Vector3 B0 = Vector3.Lerp(M0, M1, t);
        Vector3 B1 = Vector3.Lerp(M1, M2, t);

        return Vector3.Lerp(B0, B1, t);
    }

    private T GetClickedUIObjectComponent<T>() where T : Component
    {
        _rrList.Clear();

        _gr.Raycast(_ped, _rrList);

        if (_rrList.Count == 0)
        {
            return null;
        }

        return _rrList[0].gameObject.GetComponent<T>();
    }

    private void OnPointerDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            curSelectedCardDisplay = GetClickedUIObjectComponent<CardDisplay>();
            curSelectedCard = curSelectedCardDisplay?.GetCard();
            curSelectedCardGO = curSelectedCardDisplay?.gameObject;

            Debug.Log("OnClick");

            if (curSelectedCard != null)
            {
                Debug.Log(curSelectedCard.CardData.name);
            }
        }
    }

    private void OnPointerUp()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (curSelectedCard != null)
            {
                Debug.Log("End Click");
                if (IsMeetUseCondition(curSelectedCard.CardData.condition))
                {
                    UseCard();
                }
            }
        }
    }

    public void UseCard()
    {
        Debug.Log(curSelectedCard.CardData.name + " »ç¿ë");
        hands.Remove(curSelectedCardDisplay);
        Destroy(curSelectedCardGO);
        SortAllCard();
        _cardManager.DropCard(curSelectedCard);
    }
    private bool IsMeetUseCondition(UseCondition useCondition)
    {
        switch (useCondition)
        {
            case UseCondition.Target:
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

                if (hit.collider != null)
                {
                    if (hit.collider.gameObject.CompareTag("Monster"))
                        return true;
                }
                break;
            case UseCondition.NonTarget:
                if (Input.mousePosition.y > 500)
                {
                    return true;
                }
                break;
        }

        return false;
    }

    public void ConnectCardManager(CardManager manager)
    {
        _cardManager = manager;
    }
}
