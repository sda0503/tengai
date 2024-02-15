using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class HandManager : MonoBehaviour
{
    public List<CardDisplay> hands;

    [Header("Card Movement Value")]
    [SerializeField] private float drawTime;
    public float dropTime;
    public float extinguishTime;
    public Vector3 drawStartSize;
    public Vector3 drawEndSize;
    public Transform usedCardPlace;
    public Transform dragTargetingCardPlace;
    public float useCardTime1;

    [Header("Position For Bezier Curve")]
    public Transform P0;
    public Transform P1;
    public Transform P2;
    public Transform P3;
    public Transform DeckPos;
    public Transform DeckMid1Pos;
    public Transform DeckMid2Pos;
    public Transform GarbagePos;
    public Transform GarbageMid1Pos;
    public Transform GarbageMid2Pos;

    public float angleBetweenCard;

    public float distanceBetweenCard1;
    public float distanceBetweenCard2;

    public GameObject cardPrefab;
    public GameObject statusCardPrefab;

    private CardManager _cardManager;

    private GraphicRaycaster _gr;
    private PointerEventData _ped;
    private List<RaycastResult> _rrList;

    private CardDisplay curSelectedCardDisplay;
    private Card curSelectedCard;

    private CardDisplay curMouseOverCard;

    private StatSystem targetStatSystem;

    public Canvas _mainCanvas;

    private bool isDrag = false;
    private bool isMouseOver = false;
    private bool isUsing = false;

    [Header("Hightlight Card Value")]
    public float addScaleValueWhenHighlight;
    public float addYPosValue;
    public float expandGapValue;

    [Header("Targeting")]
    [SerializeField] private Sprite reticleBlock;
    [SerializeField] private Sprite reticleArrow;
    [SerializeField] GameObject lineGO;
    [SerializeField] Color onTargetLineColor;
    [SerializeField] Color defaultLineColor;

    private StatSystem playerStatSystem;

    private void Awake()
    {
        hands = new List<CardDisplay>();
        Init();
    }

    // Start is called before the first frame update
    void Start()
    {
        playerStatSystem = GameObject.Find("Player").GetComponent<StatSystem>();
        this.transform.SetAsLastSibling();
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
        UpdateHandsCardPosition();
    }

    private void UpdateHandsCardPosition()
    {
        foreach(var hand in hands)
        {
            hand.transform.localPosition = Vector3.Lerp(hand.transform.localPosition, hand.targetPos, 0.05f);
        }
    }

    public IEnumerator DrawCard(Card card)
    {
        GameObject go;
        if(card is StatusCard)
        {
            go = Instantiate(statusCardPrefab, this.transform);
        }
        else
        {
            go = Instantiate(cardPrefab, this.transform);
        }

        CardDisplay cardDisplay = go.GetComponent<CardDisplay>();

        cardDisplay.SetCard(card);
        cardDisplay.transform.localPosition = DeckPos.transform.localPosition;

        hands.Add(cardDisplay);

        SetCurveRate(hands.Count - 1);
        hands[hands.Count - 1].targetPos = GetPositionFromBezierCurve4(P0.localPosition, P1.localPosition, P2.localPosition, P3.localPosition, hands[hands.Count - 1].curveRateInHand);
        yield return StartCoroutine(ChangeSizeCardC(cardDisplay.transform, drawStartSize, drawEndSize, drawTime));
        //yield return StartCoroutine(MoveObjC(cardDisplay.transform, DeckPos.localPosition, cardDisplay.targetPos, drawTime));
        
        SortAllCard();
        SetAllCardIndex();
    }

    IEnumerator ChangeSizeCardC(Transform card, Vector3 minSize, Vector3 maxSize, float time)
    {
        float startTime = 0;

        card.transform.localScale = minSize;
        while(startTime < time)
        {
            card.transform.localScale = Vector3.Lerp(minSize, maxSize, startTime / time);
            startTime += Time.deltaTime;
            yield return null;
        }

        card.transform.localScale = maxSize;
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
        hands[i].targetPos = GetPositionFromBezierCurve4(P0.localPosition, P1.localPosition, P2.localPosition, P3.localPosition, hands[i].curveRateInHand);
        //hands[i].transform.localPosition = hands[i].targetPos;
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

    public Vector3 GetPositionFromBezierCurve4(Vector3 P0, Vector3 P1, Vector3 P2, Vector3 P3, float t)
    {
        Vector3 M0 = Vector3.Lerp(P0, P1, t);
        Vector3 M1 = Vector3.Lerp(P1, P2, t);
        Vector3 M2 = Vector3.Lerp(P2, P3, t);

        Vector3 B0 = Vector3.Lerp(M0, M1, t);
        Vector3 B1 = Vector3.Lerp(M1, M2, t);

        return Vector3.Lerp(B0, B1, t);
    }

    public Vector3 GetPositionFromBezierCurve3(Vector3 P0, Vector3 P1, Vector3 P2, float t)
    {
        Vector3 M0 = Vector3.Lerp(P0, P1, t);
        Vector3 M1 = Vector3.Lerp(P1, P2, t);

        return Vector3.Lerp(M0, M1, t);
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
        if(isMouseOver && !isDrag && curMouseOverCard != null && !isUsing)
        {
            Debug.Log("OnExit");

            if (GetClickedUIObjectComponent<CardDisplay>() != null && GetClickedUIObjectComponent<CardDisplay>() == curMouseOverCard)
                return;

            //curMouseOverCard.targetPos -= new Vector3(0f, addYPosValue, 0f);
            SortAllCard();
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

            if(curSelectedCardDisplay != null)
                curSelectedCardDisplay.GetComponent<Image>().raycastTarget = false;

            Debug.Log("OnClick");

            if (curSelectedCard != null)
            {
                Debug.Log(curSelectedCard.CardData.cardName);
            }
        }
    }

    private void OnPointerDrag()
    {
        if (isDrag && curSelectedCardDisplay != null && CanUse(curSelectedCard))
        {
            if(curSelectedCardDisplay.GetCard().CardData.useCondition == UseCondition.Target && Input.mousePosition.y > 300)
            {
                if (!lineGO.activeSelf)
                {
                    lineGO.SetActive(true);
                    lineGO.transform.SetAsLastSibling();
                }

                curSelectedCardDisplay.targetPos = dragTargetingCardPlace.transform.localPosition;
                DrawTargetingLine();
            }
            else
            {
                if (lineGO.activeSelf)
                    lineGO.SetActive(false);

                curSelectedCardDisplay.transform.position = Input.mousePosition;
            }
        }
    }

    private void DrawTargetingLine()
    {
        Vector3 endPos = Input.mousePosition;
        Vector3 P1 = new Vector3(dragTargetingCardPlace.position.x, endPos.y + 300);
        for (int i = 0; i < lineGO.transform.childCount - 1; i++)
        {
            lineGO.transform.GetChild(i).position = GetPositionFromBezierCurve3(dragTargetingCardPlace.position, P1, endPos, (float)i / (lineGO.transform.childCount - 1));

            lineGO.transform.GetChild(i).GetComponent<Image>().sprite = reticleBlock;
        }

        float angle;

        for (int i = 0; i < lineGO.transform.childCount - 2; i++)
        {
            angle = Quaternion.FromToRotation(Vector3.up, lineGO.transform.GetChild(i + 1).position - lineGO.transform.GetChild(i).position).eulerAngles.z;

            lineGO.transform.GetChild(i).rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        }

        angle = Quaternion.FromToRotation(Vector3.up, Input.mousePosition - lineGO.transform.GetChild(lineGO.transform.childCount - 2).position).eulerAngles.z;
        lineGO.transform.GetChild(lineGO.transform.childCount - 2).rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));

        Transform arrow = lineGO.transform.GetChild(lineGO.transform.childCount - 1);

        arrow.transform.position = Input.mousePosition;
        arrow.rotation = lineGO.transform.GetChild(lineGO.transform.childCount - 2).rotation;
        arrow.GetComponent<Image>().sprite = reticleArrow;
        arrow.localScale = new Vector3(2f, 2f, 1f);

        if (IsMouseOnTarget<StatSystem>())
        {
            for(int i = 0; i < lineGO.transform.childCount; i++)
            {
                lineGO.transform.GetChild(i).GetComponent<Image>().color = onTargetLineColor;
            }
        }
        else
        {
            for (int i = 0; i < lineGO.transform.childCount; i++)
            {
                lineGO.transform.GetChild(i).GetComponent<Image>().color = defaultLineColor;
            }
        }
    }

    private bool IsMouseOnTarget<T>() where T : Component
    {
        _rrList.Clear();

        _gr.Raycast(_ped, _rrList);

        T target = null;

        if (_rrList.Count > 0)
        {
            target = _rrList[0].gameObject.GetComponentInParent<T>();
        }

        if(target != null)
        {
            if(target.gameObject.CompareTag("Monster"))
                return true;
        }
        return false;
    }

    private void OnPointerUp()
    {
        if (Input.GetMouseButtonUp(0))
        {
            isDrag = false;
            lineGO.SetActive(false);

            if (curSelectedCard != null)
            {
                Debug.Log("End Click");
                if (IsMeetUseCondition(curSelectedCard.CardData.useCondition) && CanUse(curSelectedCard))
                {
                    StartCoroutine(UseCard());
                    isMouseOver = false;
                }
                else
                {
                    SortAllCard();
                    curSelectedCardDisplay.GetComponent<Image>().raycastTarget = true;

                    curSelectedCardDisplay = null;
                    curSelectedCard = null;
                }
            }
        }
    }

    public IEnumerator UseCard()
    {
        Debug.Log(curSelectedCard.CardData.cardName + " 사용");
        playerStatSystem.TakeCost(curSelectedCard.CardData.cost);
        InfoSystem.instance.ShowDate();
        hands.Remove(curSelectedCardDisplay);

        CardDisplay usedCardDisplay = curSelectedCardDisplay;
        Card usedCard = curSelectedCard;

        curSelectedCard = null;
        curSelectedCardDisplay = null;

        isUsing = true;

        yield return StartCoroutine(MoveObjC(usedCardDisplay.transform, usedCardDisplay.transform.localPosition, usedCardPlace.localPosition, 0.2f));

        yield return new WaitForSeconds(0.3f);

        UseCardEffect(usedCard);

        if(usedCard.CardData.extinctionType == ExtinctionType.Extinction)
        {
            yield return StartCoroutine(ExtinguishCardC(usedCardDisplay.transform));

            _cardManager.ExtingushCard(usedCard);
        }
        else
        {
            float angle = Quaternion.FromToRotation(Vector3.up, GarbagePos.localPosition - usedCardDisplay.transform.localPosition).eulerAngles.z;

            StartCoroutine(RotationObjLeftC(usedCardDisplay.transform, angle, dropTime / 2));
            StartCoroutine(ChangeSizeCardC(usedCardDisplay.transform, usedCardDisplay.transform.localScale, new Vector3(0.2f, 0.2f, 1f), dropTime / 2));
            yield return StartCoroutine(MoveObjFollowCurve4C(usedCardDisplay.transform, usedCardPlace.localPosition,
                GarbageMid1Pos.localPosition, GarbageMid2Pos.localPosition, GarbagePos.localPosition, dropTime));

            _cardManager.DropCard(usedCard);

            Destroy(usedCardDisplay.gameObject);
        }

        InfoSystem.instance.ShowDate();

        SortAllCard();
        SetAllCardIndex();

        isUsing = false;
        isMouseOver = false;
        targetStatSystem = null;
    }

    private void UseCardEffect(Card card)
    {
        for (int i = 0; i < card.CardData.attackEffects.Count; i++)
        {
            switch (card.CardData.attackEffects[i].target)
            {
                case Target.Player:
                    card.CardData.attackEffects[i].OnUse(playerStatSystem);
                    break;
                case Target.TargetEnemy:
                    card.CardData.attackEffects[i].OnUse(targetStatSystem);
                    break;
                case Target.AllEnemy:
                case Target.RandomEnemy:
                    card.CardData.attackEffects[i].OnUse();
                    break;
            }
        }

        for(int i = 0; i < card.CardData.drawEffects.Count; i++)
        {
            card.CardData.drawEffects[i].OnUse();
        }

        for(int i = 0; i < card.CardData.statEffects.Count; i++)
        {
            switch (card.CardData.statEffects[i].target)
            {
                case Target.Player:
                    card.CardData.statEffects[i].OnUse(playerStatSystem);
                    break;
                case Target.TargetEnemy:
                    card.CardData.statEffects[i].OnUse(targetStatSystem);
                    break;
                case Target.AllEnemy:
                case Target.RandomEnemy:
                    card.CardData.statEffects[i].OnUse();
                    break;
            }
        }

        for(int i = 0; i < card.CardData.addCards.Count; i++)
        {
            card.CardData.addCards[i].OnUse();
        }
    }

    IEnumerator ExtinguishCardC(Transform obj)
    {
        Image[] images = obj.GetComponentsInChildren<Image>();
        foreach (Image image in images)
        {
            image.DOFade(0f, extinguishTime);
        }
        yield return StartCoroutine(MoveObjC(obj, obj.localPosition, obj.localPosition + new Vector3(0f, 100f, 0f), extinguishTime));

        Destroy(obj.gameObject);
    }

    public void CreateCard(Card card, CardCreatePlace cardCreatePlace)
    {
        StartCoroutine(CreateCardC(card, cardCreatePlace));
    }

    IEnumerator CreateCardC(Card card, CardCreatePlace cardCreatePlace)
    {
        GameObject go;
        if (card is StatusCard)
        {
            go = Instantiate(statusCardPrefab, this.transform);
        }
        else
        {
            go = Instantiate(cardPrefab, this.transform);
        }

        CardDisplay cardDisplay = go.GetComponent<CardDisplay>();

        cardDisplay.SetCard(card);
        go.transform.localPosition = usedCardPlace.localPosition;
        go.transform.localScale  = new Vector3(1f + addScaleValueWhenHighlight, 1f + addScaleValueWhenHighlight, 0);

        yield return new WaitForSeconds(0.3f);

        if(cardCreatePlace == CardCreatePlace.Garbage)
        {
            float angle = Quaternion.FromToRotation(Vector3.up, GarbagePos.localPosition - go.transform.localPosition).eulerAngles.z;

            Debug.Log(angle);

            StartCoroutine(RotationObjLeftC(go.transform, angle, dropTime / 2));
            StartCoroutine(ChangeSizeCardC(go.transform, go.transform.localScale, new Vector3(0.2f, 0.2f, 1f), dropTime / 2));
            yield return StartCoroutine(MoveObjFollowCurve4C(go.transform, usedCardPlace.localPosition,
                GarbageMid1Pos.localPosition, GarbageMid2Pos.localPosition, GarbagePos.localPosition, dropTime));

            _cardManager.DropCard(card);

            Destroy(go.gameObject);
        }
        else
        {
            float angle = Quaternion.FromToRotation(Vector3.up, DeckPos.localPosition - go.transform.localPosition).eulerAngles.z;

            Debug.Log(angle);

            StartCoroutine(RotationObjLeftC(go.transform, angle, dropTime / 2));
            StartCoroutine(ChangeSizeCardC(go.transform, go.transform.localScale, new Vector3(0.2f, 0.2f, 1f), dropTime / 2));
            yield return StartCoroutine(MoveObjFollowCurve4C(go.transform, usedCardPlace.localPosition,
                DeckMid1Pos.localPosition, DeckMid2Pos.localPosition, DeckPos.localPosition, dropTime));

            _cardManager.AddCard(card);

            Destroy(go.gameObject);
        }
    }

    public void EndTurn()
    {
        StartCoroutine(DropAllCardC());
    }

    IEnumerator DropAllCardC()
    {
        for(int i = hands.Count - 1; i >= 0; i--)
        {
            if (hands[i].GetCard().CardData.extinctionType == ExtinctionType.Volatilization)
            {
                StartCoroutine(ExtinguishCardC(hands[i].transform));

                _cardManager.ExtingushCard(hands[i].GetCard());
            }
            else
            {
                float angle = Quaternion.FromToRotation(Vector3.up, GarbagePos.localPosition - hands[i].transform.localPosition).eulerAngles.z - 180;
                StartCoroutine(RotationObjLeftC(hands[i].transform, angle, dropTime / 2));
                StartCoroutine(ChangeSizeCardC(hands[i].transform, hands[i].transform.localScale, new Vector3(0.2f, 0.2f, 1f), dropTime / 2));
                StartCoroutine(DropCardC(hands[i].transform));

                _cardManager.DropCard(hands[i].GetCard());
            }

            hands.Remove(hands[i]);

            yield return null;
        }
    }

    IEnumerator DropCardC(Transform obj)
    {
        yield return StartCoroutine(MoveObjFollowCurve3C(obj, obj.localPosition, obj.transform.localPosition + new Vector3(50f, 50f, 0f), GarbagePos.localPosition, dropTime));

        Destroy(obj.gameObject);
    }

    IEnumerator MoveObjC(Transform obj, Vector3 startPos, Vector3 endPos, float time)
    {
        Vector3 velocity = Vector3.zero;
        obj.localPosition = startPos;

        obj.DOLocalMove(endPos, time).SetEase(Ease.OutQuad);
        yield return new WaitForSeconds(time);

        obj.transform.localPosition = endPos; 
    }

    IEnumerator RotationObjLeftC(Transform obj, float angle, float time)
    {
        float startTime = 0;

        while (startTime < time)
        {
            float x = Mathf.Lerp(0, angle, startTime / time);
            obj.transform.localRotation = Quaternion.Euler(0, 0, x);
            startTime += Time.deltaTime;
            yield return null;
        }

        obj.transform.localRotation = Quaternion.Euler(0, 0, angle);
    }

    IEnumerator MoveObjFollowCurve3C(Transform obj, Vector3 P0, Vector3 P1, Vector3 P2, float time)
    {
        float startTime = 0;

        while (startTime < time)
        {
            obj.localPosition = GetPositionFromBezierCurve3(P0, P1, P2, startTime / time);
            startTime += Time.deltaTime;
            yield return null;
        }

        obj.localPosition = P2;
    }

    IEnumerator MoveObjFollowCurve4C(Transform obj, Vector3 P0, Vector3 P1, Vector3 P2, Vector3 P3, float time)
    {
        float startTime = 0;

        while (startTime < time)
        {
            obj.localPosition = GetPositionFromBezierCurve4(P0, P1, P2, P3, startTime / time);
            startTime += Time.deltaTime;
            yield return null;
        }

        obj.localPosition = P3;
    }

    private bool IsMeetUseCondition(UseCondition useCondition)
    {
        switch (useCondition)
        {
            case UseCondition.Target:
                _rrList.Clear();

                _gr.Raycast(_ped, _rrList);

                if(_rrList.Count > 0)
                {
                    targetStatSystem = _rrList[0].gameObject.GetComponentInParent<StatSystem>();
                }

                if (targetStatSystem != null)
                {
                    if (targetStatSystem.gameObject.CompareTag("Monster"))
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
            curMouseOverCard.transform.localRotation = Quaternion.identity;
            curMouseOverCard.transform.localScale = new Vector3(1f + addScaleValueWhenHighlight, 1f + addScaleValueWhenHighlight, 0);
            curMouseOverCard.targetPos += new Vector3(0f, addYPosValue, 0f);
        }
    }

    private void ExpandGap(int index)
    {
        for(int i = 0; i < hands.Count; i++)
        {
            if(i != index)
            {
                float x = (i < index) ? expandGapValue * -1 : expandGapValue;
                hands[i].targetPos += new Vector3(x, 0, 0);
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

    private bool CanUse(Card card)
    {
        return card.CardData.cost <= playerStatSystem.COST;
    }

    public void ConnectCardManager(CardManager manager)
    {
        _cardManager = manager;
    }
}
