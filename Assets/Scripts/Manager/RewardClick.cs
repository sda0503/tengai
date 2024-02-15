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
    private bool _isMouseOver = false;

    private Image _uiImage;
    private GameObject _obj;
    private GameObject _cardRewardUIObj;

    public GameObject mapObj;
    public GameObject invenObj;
    public GameObject chestObj;
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
        gameObject.SetActive(false);
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
        if (!_isMouseOver)
        {
            _uiImage = GetClickedUIObjectComponent<Image>();
            _obj = GetClickedUIObject();
            if (_uiImage != null)
            {
                //Debug.Log("MouseOver");
                _isMouseOver = true;
                if (_obj.name == "List(Clone)")
                {
                    _uiImage.color = new Color(210f / 255f, 253f / 255f, 255f / 255f, 1f);
                }
                else if (_obj.name == "nextBtn")
                {
                    _uiImage.color = new Color(1, 1, 1, 1f);
                }
            }
        }
    }
    public void OnPointerExit()
    {
        if (_isMouseOver && _uiImage != null && _obj != null)
        {
            if (GetClickedUIObjectComponent<Image>() != null && GetClickedUIObjectComponent<Image>() == _uiImage)
                return;
            if (_obj.name == "List(Clone)")
            {
                _uiImage.color = new Color(210f / 255f, 253f / 255f, 255f / 255f, 0.31f);
            }
            else if (_obj.name == "nextBtn")
            {
                _uiImage.color = new Color(1, 1, 1, 168f / 255f);
            }
            _isMouseOver = false;
        }
    }

    public void AddGold()
    {
        _obj.SetActive(false);
        int gold = gameObject.transform.GetChild(1).GetChild(1).gameObject.GetOrAddComponent<Reward>().power;
        InfoSystem.instance.SetGold(gold);
        InfoSystem.instance.ShowDate();
    }

    public void AddCard()
    {
        _obj = GetClickedUIObject();
        _cardRewardUIObj = _obj;
        cardRewardWindow.gameObject.SetActive(true);
        Transform container = cardRewardWindow.GetChild(0);

        for(int i = 0; i < container.childCount; i++)
        {
            container.GetChild(i).GetComponent<CardDisplay>().SetCard(CardDatabase.instance.GetRandomRewardCard());
        }
    }

    public void OnRewardCardClick(int i)
    {
        Transform container = cardRewardWindow.GetChild(0);
        CardManager.instance.AddCardToOriginal(container.GetChild(i).GetComponent<CardDisplay>().GetCard());
        _cardRewardUIObj.SetActive(false);
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
        chestObj.SetActive(false);
    }

    public void AddRelics(int iNum)
    {
        _obj = GetClickedUIObject();
        invenObj.GetComponent<Inventory>().AddItem(Datas[iNum]);
        _obj.SetActive(false);
    }
}
