using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChestManager : MonoBehaviour
{
    private GraphicRaycaster _gr;
    private PointerEventData _ped;
    private List<RaycastResult> _rrList;
    private bool isMouseOver = false;

    private Image uiImage;
    private GameObject obj;

    public GameObject mapObj;
    public GameObject rewardObj;
    public Canvas _mainCanvas;

    public Sprite[] closeChest;
    public Sprite[] openChest;

    public Image chestImage;

    public int iNum = 0;

    public void Awake()
    {
        _gr = _mainCanvas.GetComponent<GraphicRaycaster>();
        _ped = new PointerEventData(null);
        _rrList = new List<RaycastResult>();
        chestImage.sprite = closeChest[iNum];
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
                isMouseOver = true;
            }
        }
    }

    public void OnPointerExit()
    {
        if (isMouseOver && uiImage != null)
        {
            if (GetClickedUIObjectComponent<Image>() != null && GetClickedUIObjectComponent<Image>() == uiImage)
                return;

            isMouseOver = false;
        }
    }

    public void OpenChest()
    {
        rewardObj.SetActive(true);
        rewardObj.transform.GetChild(1).GetComponent<RewardController>().MakeReward();
        rewardObj.transform.GetChild(1).GetComponent<RewardController>().iNum = iNum;
        chestImage.sprite = openChest[iNum];
    }
}
