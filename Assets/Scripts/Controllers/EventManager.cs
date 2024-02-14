using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EventManager : MonoBehaviour
{
    private GraphicRaycaster _gr;
    private PointerEventData _ped;
    private List<RaycastResult> _rrList;
    private bool isMouseOver = false;

    private Image uiImage;
    private GameObject obj;

    public GameObject mapObj;
    public Canvas _mainCanvas;


    // Start is called before the first frame update
    void Start()
    {
        _gr = _mainCanvas.GetComponent<GraphicRaycaster>();
        _ped = new PointerEventData(null);
        _rrList = new List<RaycastResult>();
    }

    // Update is called once per frame
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
            if (uiImage != null)
            {
                uiImage.color = new Color(255f / 255f, 255f / 255f, 255f / 255f, 1f);
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
            uiImage.color = new Color(255f / 255f, 255f / 255f, 255f / 255f, 0.2f);
            
            isMouseOver = false;
        }
    }

    public void Skip()
    {
        gameObject.SetActive(false);
        mapObj.SetActive(true);
    }
}
