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

    private CardDisplay curMouseOverCard;

    public Canvas _mainCanvas;

    private bool isDrag = false;
    private bool isMouseOver = false;

    [Header("Hightlight Card Value")]
    public float addScaleValueWhenHighlight;
    public float addYPosValue;
    public float expandGapValue;

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
        OnPointerOver();
        OnPointerExit();
        OnPointerDown();
        OnPointerDrag();
        OnPointerUp();
    }

    public void DrawCard(Card card)
    {
        GameObject go = Instantiate(cardPrefab, this.transform);

        CardDisplay cardDisplay = go.GetComponent<CardDisplay>();

        cardDisplay.SetCard(card);

        hands.Add(cardDisplay);

        SortAllCard();
        SetAllCardIndex();
    }

    private void SetAllCardIndex()
    {
        for(int i = 0; i < hands.Count; i++)
        {
            hands[i].index = i;
        }
    }

    public void SortAllCard()
    {
        for(int i = 0; i < hands.Count; i++)
        {
            SortCard(i);
        }
    }

    private void SortCard(int i)
    {
        SetCurveRate(i);
        SetAngle(i);
        hands[i].targetPos = GetPositionFromBezierCurve(P0.localPosition, P1.localPosition, P2.localPosition, P3.localPosition, hands[i].curveRateInHand);
        hands[i].transform.localPosition = hands[i].targetPos;
        hands[i].transform.localRotation = Quaternion.Euler(new Vector3(0, 0, hands[i].angle));
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
    private void OnPointerOver()
    {
        if(!isMouseOver && !isDrag)
        {
            curMouseOverCard = GetClickedUIObjectComponent<CardDisplay>();

            

            if (curMouseOverCard != null)
            {
                Debug.Log("MouseOver");
                isMouseOver = true;
                HighlightCard();
                curMouseOverCard.transform.SetAsLastSibling();
                ExpandGap(curMouseOverCard.index);
            }
        }
    }

    private void OnPointerExit()
    {
        if(isMouseOver && !isDrag && curMouseOverCard != null)
        {
            if (GetClickedUIObjectComponent<CardDisplay>() != null && GetClickedUIObjectComponent<CardDisplay>() == curMouseOverCard)
                return;

            curMouseOverCard.transform.localPosition = curMouseOverCard.targetPos;
            curMouseOverCard.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, curMouseOverCard.angle));
            curMouseOverCard.transform.localScale = new Vector3(1f, 1f, 0f);
            curMouseOverCard.transform.SetSiblingIndex(curMouseOverCard.index);

            RollBackExpandGap(curMouseOverCard.index);

            isMouseOver = false;
        }
    }

    private void OnPointerDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDrag = true;

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

    private void OnPointerDrag()
    {
        if (isDrag && curSelectedCardDisplay != null)
        {
            curSelectedCardDisplay.transform.position = Input.mousePosition;
        }
    }

    private void OnPointerUp()
    {
        if (Input.GetMouseButtonUp(0))
        {
            isDrag = false;

            if (curSelectedCard != null)
            {
                Debug.Log("End Click");
                if (IsMeetUseCondition(curSelectedCard.CardData.condition))
                {
                    UseCard();
                    isMouseOver = false;
                }
                else
                {
                    curSelectedCardDisplay.transform.localPosition = curSelectedCardDisplay.targetPos;
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
        SetAllCardIndex();

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

    private void HighlightCard()
    {
        if(curMouseOverCard != null)
        {
            Debug.Log("highlight");
            curMouseOverCard.transform.localRotation = Quaternion.identity;
            curMouseOverCard.transform.localScale = new Vector3(1f + addScaleValueWhenHighlight, 1f + addScaleValueWhenHighlight, 0);
            curMouseOverCard.transform.localPosition += new Vector3(0f, addYPosValue, 0f);
        }
    }

    private void ExpandGap(int index)
    {
        for(int i = 0; i < hands.Count; i++)
        {
            if(i != index)
            {
                float x = (i < index) ? expandGapValue * -1 : expandGapValue;
                hands[i].transform.localPosition += new Vector3(x, 0, 0);
            }
        }
    }

    private void RollBackExpandGap(int index)
    {
        for (int i = 0;i < hands.Count; i++)
        {
            if(i != index)
            {
                SortCard(i);
            }
        }
    }

    public void ConnectCardManager(CardManager manager)
    {
        _cardManager = manager;
    }
}
