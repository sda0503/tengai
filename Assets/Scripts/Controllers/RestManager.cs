using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RestManager : MonoBehaviour
{
    private GraphicRaycaster _gr;
    private PointerEventData _ped;
    private List<RaycastResult> _rrList;
    private bool isMouseOver = false;

    private Image uiImage;
    private GameObject obj;
    public GameObject mapObj;
    public Canvas _mainCanvas;
    public Sprite[] sprites;

    // Start is called before the first frame update
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
        if (!isMouseOver)
        {
            obj = GetClickedUIObject();
            if (obj != null && obj.name == "btn")
            {
                isMouseOver = true;
                obj.transform.localScale = new Vector3(1f, 1f, 1);
                obj.transform.GetChild(0).gameObject.SetActive(true);
                obj.transform.GetChild(3).gameObject.SetActive(true);
            }
            else if(obj != null && obj.name == "nextBtn")
            {
                isMouseOver = true;
                obj.GetComponent<Image>().color = new Color(1, 1, 1, 1f);
            }
        }
    }

    public void OnPointerExit()
    {
        if (isMouseOver && obj != null)
        {
            if (GetClickedUIObject() != null && GetClickedUIObject() == obj)
                return;
            if(obj.name == "btn")
            {
                obj.transform.localScale = new Vector3(0.8f, 0.8f, 1);
                obj.transform.GetChild(0).gameObject.SetActive(false);
                obj.transform.GetChild(3).gameObject.SetActive(false);
            }
            else
            {
                obj.GetComponent<Image>().color = new Color (1,1,1,0.5f);
            }
            isMouseOver = false;
        }
    }

    public void Rest()
    {
        gameObject.transform.GetChild(6).gameObject.SetActive(true);
        Invoke("ObjActive",2f);

        InfoSystem.instance.currentHp = InfoSystem.instance.currentHp + (InfoSystem.instance.maxHp / 3) <= InfoSystem.instance.maxHp ? InfoSystem.instance.currentHp + (InfoSystem.instance.maxHp / 3) : InfoSystem.instance.maxHp;
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
