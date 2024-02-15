using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RestManager : MonoBehaviour
{
    private GraphicRaycaster _gr;
    private PointerEventData _ped;
    private List<RaycastResult> _rrList;
    private bool _isMouseOver = false;

    private GameObject _obj;
    public GameObject mapObj;
    public Canvas _mainCanvas;
    public Sprite[] sprites;

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
            _obj = GetClickedUIObject();
            if (_obj != null && _obj.name == "btn")
            {
                _isMouseOver = true;
                _obj.transform.localScale = new Vector3(1f, 1f, 1);
                _obj.transform.GetChild(0).gameObject.SetActive(true);
                _obj.transform.GetChild(3).gameObject.SetActive(true);
            }
            else if(_obj != null && _obj.name == "nextBtn")
            {
                _isMouseOver = true;
                _obj.GetComponent<Image>().color = new Color(1, 1, 1, 1f);
            }
        }
    }

    public void OnPointerExit()
    {
        if (_isMouseOver && _obj != null)
        {
            if (GetClickedUIObject() != null && GetClickedUIObject() == _obj)
                return;
            if(_obj.name == "btn")
            {
                _obj.transform.localScale = new Vector3(0.8f, 0.8f, 1);
                _obj.transform.GetChild(0).gameObject.SetActive(false);
                _obj.transform.GetChild(3).gameObject.SetActive(false);
            }
            else
            {
                _obj.GetComponent<Image>().color = new Color (1,1,1,0.5f);
            }
            _isMouseOver = false;
        }
    }

    public void Rest()
    {
        gameObject.transform.GetChild(6).gameObject.SetActive(true);
        Invoke("ObjActive",2f);
        InfoSystem.instance.player.SetHP(30);
        InfoSystem.instance.ShowDate();
    }

    public void ObjActive()
    {
        gameObject.transform.GetChild(1).GetComponent<Image>().sprite = sprites[1];
        gameObject.transform.GetChild(2).gameObject.SetActive(false);
        gameObject.transform.GetChild(3).gameObject.SetActive(false);
        gameObject.transform.GetChild(4).gameObject.SetActive(false);
        gameObject.transform.GetChild(5).gameObject.SetActive(true);
    }

    public void Skip()
    {
        gameObject.SetActive(false);
        mapObj.SetActive(true);
        gameObject.transform.GetChild(1).GetComponent<Image>().sprite = sprites[0];
        gameObject.transform.GetChild(2).gameObject.SetActive(true);
        gameObject.transform.GetChild(3).gameObject.SetActive(true);
        gameObject.transform.GetChild(4).gameObject.SetActive(true);
        gameObject.transform.GetChild(5).gameObject.SetActive(false);
        gameObject.transform.GetChild(6).gameObject.SetActive(false);
    }
}
