using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    private GraphicRaycaster _gr;
    private PointerEventData _ped;
    private List<RaycastResult> _rrList;
    private bool isMouseOver = false;

    private Image uiImage;
    private GameObject obj;
    public GameObject mapObj;
    public Canvas _mainCanvas;

    private void Awake()
    {
        _gr = _mainCanvas.GetComponent<GraphicRaycaster>();
        _ped = new PointerEventData(null);
        _rrList = new List<RaycastResult>();
    }
    void Update()
    {
        _ped.position = Input.mousePosition;
        OnPointerOver();
        OnPointerExit();
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

    private GameObject GetClickedUIObject()
    {
        _rrList.Clear();

        _gr.Raycast(_ped, _rrList);

        if (_rrList.Count == 0)
        {
            return null;
        }

        return _rrList[0].gameObject;
    }

    private void OnPointerOver()
    {
        if (!isMouseOver)
        {
            uiImage = GetClickedUIObjectComponent<Image>();
            obj = GetClickedUIObject();
            if (uiImage != null)
            {
                //Debug.Log("MouseOver");
                isMouseOver = true;
                if(obj.name != "nextBtn")
                {
                    uiImage.color = new Color(210f/255f,253f/255f,255f/255f,1f);
                }
                else
                {
                    uiImage.color = new Color(1, 1, 1, 1f);
                }
            }
        }
    }

    private void OnPointerExit()
    {
        if (isMouseOver && uiImage != null)
        {
            if (GetClickedUIObjectComponent<Image>() != null && GetClickedUIObjectComponent<Image>() == uiImage)
                return;
            if (obj.name != "nextBtn")
            {
                uiImage.color = new Color(210f / 255f, 253f / 255f, 255f / 255f, 0.31f);
            }
            else
            {
                uiImage.color = new Color(1, 1, 1, 168f/255f);
            }
            
            isMouseOver = false;
        }
    }

    public void AddGold(int gold)
    {

    }

    public void Skip()
    {
        gameObject.SetActive(false);
        mapObj.SetActive(true);

    }
}