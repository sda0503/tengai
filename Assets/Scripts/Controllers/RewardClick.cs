using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RewardClick : MonoBehaviour
{
    private GraphicRaycaster _gr;
    private PointerEventData _ped;
    private List<RaycastResult> _rrList;
    private bool isMouseOver = false;

    private Image uiImage;
    private GameObject obj;
    private GameObject cardRewardUIObj;
    private GameObject goldObj;
    public GameObject mapObj;
    public GameObject invenObj;
    public Canvas _mainCanvas;

    public RelicsData[] Datas;
    public Transform cardRewardWindow;

    public void Awake()
    {
        _gr = _mainCanvas.GetComponent<GraphicRaycaster>();
        _ped = new PointerEventData(null);
        _rrList = new List<RaycastResult>();
    }

    private void Start()
    {
        this.gameObject.SetActive(false);
    }

    void Update()
    {
        _ped.position = Input.mousePosition;
        OnPointerOver();
        OnPointerExit();
    }

    public T GetClickedUIObjectComponent<T>() where T : Component
    {
        _rrList.Clear();

        _gr.Raycast(_ped, _rrList);

        if (_rrList.Count == 0)
        {
            return null;
        }

        return _rrList[0].gameObject.GetComponent<T>();
    }

    public GameObject GetClickedUIObject()
    {
        _rrList.Clear();

        _gr.Raycast(_ped, _rrList);

        if (_rrList.Count == 0)
        {
            return null;
        }

        return _rrList[0].gameObject;
    }

    public void OnPointerOver()
    {
        if (!isMouseOver)
        {
            uiImage = GetClickedUIObjectComponent<Image>();
            obj = GetClickedUIObject();
            if (uiImage != null)
            {
                //Debug.Log("MouseOver");
                isMouseOver = true;
                if (obj.name == "List(Clone)")
                {
                    uiImage.color = new Color(210f / 255f, 253f / 255f, 255f / 255f, 1f);
                }
                else if (obj.name == "nextBtn")
                {
                    uiImage.color = new Color(1, 1, 1, 1f);
                }
            }
        }
    }
    public void OnPointerExit()
    {
        if (isMouseOver && uiImage != null && obj != null)
        {
            if (GetClickedUIObjectComponent<Image>() != null && GetClickedUIObjectComponent<Image>() == uiImage)
                return;
            if (obj.name == "List(Clone)")
            {
                uiImage.color = new Color(210f / 255f, 253f / 255f, 255f / 255f, 0.31f);
            }
            else if (obj.name == "nextBtn")
            {
                uiImage.color = new Color(1, 1, 1, 168f / 255f);
            }
            isMouseOver = false;
        }
    }

    public void AddGold()
    {
        obj.SetActive(false);
        int gold = gameObject.transform.GetChild(1).GetChild(1).gameObject.GetOrAddComponent<Reward>().power;
        InfoSystem.instance.SetGold(gold);
        InfoSystem.instance.ShowDate();
    }

    public void AddCard()
    {
        obj = GetClickedUIObject();
        cardRewardUIObj = obj;
        cardRewardWindow.gameObject.SetActive(true);
        Transform container = cardRewardWindow.GetChild(0);

        for(int i = 0; i < container.childCount; i++)
        {
            container.GetChild(i).GetComponent<CardDisplay>().SetCard(CardDatabase.instance.GetRandomCard());
        }
    }

    public void OnRewardCardClick(int i)
    {
        Transform container = cardRewardWindow.GetChild(0);
        Debug.Log(container.GetChild(i).GetComponent<CardDisplay>().GetCard().CardData.cardName);
        CardManager.instance.AddCardToOriginal(container.GetChild(i).GetComponent<CardDisplay>().GetCard());
        cardRewardUIObj.SetActive(false);
        OnExitButtonClick();
    }

    public void OnExitButtonClick()
    {
        cardRewardWindow.gameObject.SetActive(false);
    }

    public void Skip()
    {
        gameObject.SetActive(false);
        mapObj.SetActive(true);
    }

    public void AddRelics(int iNum)
    {
        obj = GetClickedUIObject();
        Debug.Log(Datas[iNum]);
        invenObj.GetComponent<Inventory>().AddItem(Datas[iNum]);
        obj.SetActive(false);
    }
}
