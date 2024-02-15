using System;
using System.Collections.Generic;
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
    public GameObject eventObj;
    public GameObject changeObj;
    public GameObject tuto;
    public GameObject restObj;
    public GameObject chestObj;
    public GameObject shoptObj;
    public Canvas _mainCanvas;

    private BattleManager _battleManager;

    private MapData curMapData;

    bool _checkMap = false;

    private void Awake()
    {
        _gr = _mainCanvas.GetComponent<GraphicRaycaster>();
        _ped = new PointerEventData(null);
        _rrList = new List<RaycastResult>();

        _battleManager = GameObject.Find("BM").GetComponent<BattleManager>();
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
            if (uiImage != null && obj.name != "Viewport")
            {
                if (obj.transform.GetChild(1).gameObject.activeSelf != true)
                {
                    isMouseOver = true;
                    obj.transform.GetChild(0).GetComponent<Image>().color = new Color(0, 0, 0, 1f);
                    uiImage.color = new Color(255f / 255f, 255f / 255f, 255f / 255f, 1f);
                }
            }
        }
    }

    public void OnPointerExit()
    {
        if (isMouseOver && uiImage != null && obj.name != "Viewport")
        {
            if (GetClickedUIObjectComponent<Image>() != null && GetClickedUIObjectComponent<Image>() == uiImage)
                return;
            uiImage.color = new Color(0, 0, 0, 0f);
            obj.transform.GetChild(0).GetComponent<Image>().color = new Color(0, 0, 0, 0.8f);
            isMouseOver = false;
        }
    }

    public void ClickArea()
    {
        obj = GetClickedUIObject();
        Debug.Log(obj.gameObject.GetComponent<MapData>().mapData);
        if (obj.gameObject.GetComponent<MapData>().floor == InfoSystem.instance.currentFloor && 
            (obj.gameObject.GetComponent<MapData>().index == InfoSystem.instance.index -1 
            || obj.gameObject.GetComponent<MapData>().index == InfoSystem.instance.index
            || obj.gameObject.GetComponent<MapData>().index == InfoSystem.instance.index + 1
            ) || InfoSystem.instance.currentFloor==0 || InfoSystem.instance.currentFloor == 14)
        {
            _checkMap = false;
            InfoSystem.instance.currentFloor++;
            InfoSystem.instance.index = obj.gameObject.GetComponent<MapData>().index;
            curMapData = obj.gameObject.GetComponent<MapData>();
            switch (obj.gameObject.GetComponent<MapData>().mapData)
            {
                case 0:
                    obj.transform.GetChild(1).gameObject.SetActive(true);
                    obj.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = obj.transform.GetChild(0).gameObject.GetComponent<MapData>().Complete;
                    obj.GetComponent<Image>().raycastTarget = false;
                    changeObj.SetActive(true);

                    chestObj.GetComponent<ChestManager>().iNum = obj.GetComponent<MapData>().EventNum;
                    chestObj.GetComponent<ChestManager>().chestImage.sprite = chestObj.GetComponent<ChestManager>().closeChest[chestObj.GetComponent<ChestManager>().iNum];
                    Invoke("GoChest", 3f);

                    break;
                case 1:
                    obj.transform.GetChild(1).gameObject.SetActive(true);
                    obj.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = obj.transform.GetChild(0).gameObject.GetComponent<MapData>().Complete;
                    changeObj.SetActive(true);
                    obj.GetComponent<Image>().raycastTarget = false;
                    chestObj.GetComponent<ChestManager>().iNum = obj.GetComponent<MapData>().EventNum;
                    chestObj.GetComponent<ChestManager>().chestImage.sprite = chestObj.GetComponent<ChestManager>().closeChest[chestObj.GetComponent<ChestManager>().iNum];
                    Invoke("GoChest", 3f);

                    break;
                case 2:
                    obj.transform.GetChild(1).gameObject.SetActive(true);
                    obj.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = obj.transform.GetChild(0).gameObject.GetComponent<MapData>().Complete;
                    changeObj.SetActive(true);
                    obj.GetComponent<Image>().raycastTarget = false;
                    Invoke("GoRest", 3f);

                    break;
                case 3:
                    obj.transform.GetChild(1).gameObject.SetActive(true);
                    obj.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = obj.transform.GetChild(0).gameObject.GetComponent<MapData>().Complete;
                    changeObj.SetActive(true);
                    obj.GetComponent<Image>().raycastTarget = false;
                    Invoke("GoBattle", 3f);

                    break;
                case 4:
                    obj.transform.GetChild(1).gameObject.SetActive(true);
                    obj.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = obj.transform.GetChild(0).gameObject.GetComponent<MapData>().Complete;
                    changeObj.SetActive(true);
                    obj.GetComponent<Image>().raycastTarget = false;
                    eventObj.GetComponent<EventManager>().iNum = obj.GetComponent<MapData>().EventNum;
                    Invoke("GoEvent", 3f);

                    break;
                case 5:
                    obj.transform.GetChild(1).gameObject.SetActive(true);
                    obj.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = obj.transform.GetChild(0).gameObject.GetComponent<MapData>().Complete;
                    changeObj.SetActive(true);
                    obj.GetComponent<Image>().raycastTarget = false;
                    Invoke("GoBattle", 3f);

                    break;

                case 8:
                    obj.transform.GetChild(1).gameObject.SetActive(true);
                    obj.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = obj.transform.GetChild(0).gameObject.GetComponent<MapData>().Complete;
                    changeObj.SetActive(true);
                    obj.GetComponent<Image>().raycastTarget = false;
                    Invoke("GoBattle", 3f);

                    break;
                default:
                    break;
            }
        }
        else
        {
            _checkMap = true;
        }

        if (_checkMap)
        {
            if (obj.gameObject.GetComponent<MapData>().floor == InfoSystem.instance.currentFloor &&
            (obj.gameObject.GetComponent<MapData>().index == InfoSystem.instance.index - 2
            || obj.gameObject.GetComponent<MapData>().index == InfoSystem.instance.index
            || obj.gameObject.GetComponent<MapData>().index == InfoSystem.instance.index + 2
            ))
            {
                obj.transform.GetChild(0).GetComponent<Image>().color = new Color(0, 0, 0, 1f);
                _checkMap = false;
                InfoSystem.instance.currentFloor++;
                InfoSystem.instance.index = obj.gameObject.GetComponent<MapData>().index;
                curMapData = obj.gameObject.GetComponent<MapData>();
                switch (obj.gameObject.GetComponent<MapData>().mapData)
                {
                    case 0:
                        obj.transform.GetChild(1).gameObject.SetActive(true);
                        obj.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = obj.transform.GetChild(0).gameObject.GetComponent<MapData>().Complete;
                        obj.GetComponent<Image>().raycastTarget = false;
                        changeObj.SetActive(true);

                        chestObj.GetComponent<ChestManager>().iNum = obj.GetComponent<MapData>().EventNum;
                        chestObj.GetComponent<ChestManager>().chestImage.sprite = chestObj.GetComponent<ChestManager>().closeChest[chestObj.GetComponent<ChestManager>().iNum];
                        Invoke("GoChest", 3f);

                        break;
                    case 1:
                        obj.transform.GetChild(1).gameObject.SetActive(true);
                        obj.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = obj.transform.GetChild(0).gameObject.GetComponent<MapData>().Complete;
                        changeObj.SetActive(true);
                        obj.GetComponent<Image>().raycastTarget = false;
                        chestObj.GetComponent<ChestManager>().iNum = obj.GetComponent<MapData>().EventNum;
                        chestObj.GetComponent<ChestManager>().chestImage.sprite = chestObj.GetComponent<ChestManager>().closeChest[chestObj.GetComponent<ChestManager>().iNum];
                        Invoke("GoChest", 3f);

                        break;
                    case 2:
                        obj.transform.GetChild(1).gameObject.SetActive(true);
                        obj.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = obj.transform.GetChild(0).gameObject.GetComponent<MapData>().Complete;
                        changeObj.SetActive(true);
                        obj.GetComponent<Image>().raycastTarget = false;
                        Invoke("GoRest", 3f);

                        break;
                    case 3:
                        obj.transform.GetChild(1).gameObject.SetActive(true);
                        obj.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = obj.transform.GetChild(0).gameObject.GetComponent<MapData>().Complete;
                        changeObj.SetActive(true);
                        obj.GetComponent<Image>().raycastTarget = false;
                        Invoke("GoBattle", 3f);

                        break;
                    case 4:
                        obj.transform.GetChild(1).gameObject.SetActive(true);
                        obj.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = obj.transform.GetChild(0).gameObject.GetComponent<MapData>().Complete;
                        changeObj.SetActive(true);
                        obj.GetComponent<Image>().raycastTarget = false;
                        eventObj.GetComponent<EventManager>().iNum = obj.GetComponent<MapData>().EventNum;
                        Invoke("GoEvent", 3f);

                        break;
                    case 5:
                        obj.transform.GetChild(1).gameObject.SetActive(true);
                        obj.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = obj.transform.GetChild(0).gameObject.GetComponent<MapData>().Complete;
                        changeObj.SetActive(true);
                        obj.GetComponent<Image>().raycastTarget = false;
                        Invoke("GoBattle", 3f);

                        break;

                    case 8:
                        obj.transform.GetChild(1).gameObject.SetActive(true);
                        obj.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = obj.transform.GetChild(0).gameObject.GetComponent<MapData>().Complete;
                        changeObj.SetActive(true);
                        obj.GetComponent<Image>().raycastTarget = false;
                        Invoke("GoBattle", 3f);

                        break;
                    default:
                        break;
                }
            }
        }
    }

    public void GoBattle()
    {
        if (!InfoSystem.instance._isTuto)
        {
            tuto.SetActive(true);
            InfoSystem.instance._isTuto = true;
        }
        mapObj.SetActive(true);
        _battleManager.Init(curMapData);
        gameObject.SetActive(false);
        changeObj.SetActive(false);
    }

    public void GoEvent()
    {
        gameObject.SetActive(false);
        eventObj.SetActive(true);
        changeObj.SetActive(false);
    }

    public void GoRest()
    {
        gameObject.SetActive(false);
        restObj.SetActive(true);
        changeObj.SetActive(false);
    }

    public void GoChest()
    {
        gameObject.SetActive(false);
        chestObj.SetActive(true);
        
        changeObj.SetActive(false);
    }

    public void GoShop()
    {
        gameObject.SetActive(false);
        chestObj.SetActive(true);
        changeObj.SetActive(false);
    }

}
