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

    private Image _uiImage;
    private GameObject _obj;
    public GameObject mapObj;
    public GameObject eventObj;
    public GameObject changeObj;
    public GameObject tuto;
    public GameObject restObj;
    public GameObject chestObj;
    public GameObject shoptObj;
    public Canvas _mainCanvas;

    private BattleManager _battleManager;

    private MapData _curMapData;

    private bool _checkMap = false;

    private void Awake()
    {
        _gr = _mainCanvas.GetComponent<GraphicRaycaster>();
        _ped = new PointerEventData(null);
        _rrList = new List<RaycastResult>();

        _battleManager = GameObject.Find("BattleManager").GetComponent<BattleManager>();
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
            _uiImage = GetClickedUIObjectComponent<Image>();
            _obj = GetClickedUIObject();
            if (_uiImage != null && _obj.name != "Viewport")
            {
                if (_obj.transform.GetChild(1).gameObject.activeSelf != true)
                {
                    isMouseOver = true;
                    _obj.transform.GetChild(0).GetComponent<Image>().color = new Color(0, 0, 0, 1f);
                    _uiImage.color = new Color(255f / 255f, 255f / 255f, 255f / 255f, 1f);
                }
            }
        }
    }

    public void OnPointerExit()
    {
        if (isMouseOver && _uiImage != null && _obj.name != "Viewport")
        {
            if (GetClickedUIObjectComponent<Image>() != null && GetClickedUIObjectComponent<Image>() == _uiImage)
                return;
            _uiImage.color = new Color(0, 0, 0, 0f);
            _obj.transform.GetChild(0).GetComponent<Image>().color = new Color(0, 0, 0, 0.8f);
            isMouseOver = false;
        }
    }

    public void ClickArea()
    {
        _obj = GetClickedUIObject();
        Debug.Log(_obj.gameObject.GetComponent<MapData>().mapData);
        Debug.Log(_obj.gameObject.GetComponent<MapData>().EventNum);
        if (_obj.gameObject.GetComponent<MapData>().floor == InfoSystem.instance.currentFloor && 
            (_obj.gameObject.GetComponent<MapData>().index == InfoSystem.instance.index -1 
            || _obj.gameObject.GetComponent<MapData>().index == InfoSystem.instance.index
            || _obj.gameObject.GetComponent<MapData>().index == InfoSystem.instance.index + 1
            ) || InfoSystem.instance.currentFloor==0 || InfoSystem.instance.currentFloor == 14)
        {
            _checkMap = false;
            InfoSystem.instance.currentFloor++;
            InfoSystem.instance.index = _obj.gameObject.GetComponent<MapData>().index;
            _curMapData = _obj.gameObject.GetComponent<MapData>();
            switch (_obj.gameObject.GetComponent<MapData>().mapData)
            {
                case 0:
                    _obj.transform.GetChild(1).gameObject.SetActive(true);
                    _obj.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = _obj.transform.GetChild(0).gameObject.GetComponent<MapData>().Complete;
                    _obj.GetComponent<Image>().raycastTarget = false;
                    changeObj.SetActive(true);

                    chestObj.GetComponent<ChestManager>().iNum = _obj.GetComponent<MapData>().EventNum;
                    chestObj.GetComponent<ChestManager>().chestImage.sprite = chestObj.GetComponent<ChestManager>().closeChest[chestObj.GetComponent<ChestManager>().iNum];
                    Invoke("GoChest", 3f);

                    break;
                case 1:
                    _obj.transform.GetChild(1).gameObject.SetActive(true);
                    _obj.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = _obj.transform.GetChild(0).gameObject.GetComponent<MapData>().Complete;
                    changeObj.SetActive(true);
                    _obj.GetComponent<Image>().raycastTarget = false;
                    chestObj.GetComponent<ChestManager>().iNum = _obj.GetComponent<MapData>().EventNum;
                    chestObj.GetComponent<ChestManager>().chestImage.sprite = chestObj.GetComponent<ChestManager>().closeChest[chestObj.GetComponent<ChestManager>().iNum];
                    Invoke("GoChest", 3f);

                    break;
                case 2:
                    _obj.transform.GetChild(1).gameObject.SetActive(true);
                    _obj.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = _obj.transform.GetChild(0).gameObject.GetComponent<MapData>().Complete;
                    changeObj.SetActive(true);
                    _obj.GetComponent<Image>().raycastTarget = false;
                    Invoke("GoRest", 3f);

                    break;
                case 3:
                    _obj.transform.GetChild(1).gameObject.SetActive(true);
                    _obj.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = _obj.transform.GetChild(0).gameObject.GetComponent<MapData>().Complete;
                    changeObj.SetActive(true);
                    _obj.GetComponent<Image>().raycastTarget = false;
                    Invoke("GoBattle", 3f);

                    break;
                case 4:
                    _obj.transform.GetChild(1).gameObject.SetActive(true);
                    _obj.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = _obj.transform.GetChild(0).gameObject.GetComponent<MapData>().Complete;
                    changeObj.SetActive(true);
                    _obj.GetComponent<Image>().raycastTarget = false;
                    eventObj.GetComponent<EventManager>().EventSet(_obj.GetComponent<MapData>().EventNum);
                    Invoke("GoEvent", 3f);

                    break;
                case 5:
                    _obj.transform.GetChild(1).gameObject.SetActive(true);
                    _obj.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = _obj.transform.GetChild(0).gameObject.GetComponent<MapData>().Complete;
                    changeObj.SetActive(true);
                    _obj.GetComponent<Image>().raycastTarget = false;
                    Invoke("GoBattle", 3f);

                    break;

                case 8:
                    _obj.transform.GetChild(1).gameObject.SetActive(true);
                    _obj.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = _obj.transform.GetChild(0).gameObject.GetComponent<MapData>().Complete;
                    changeObj.SetActive(true);
                    _obj.GetComponent<Image>().raycastTarget = false;
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
            if (_obj.gameObject.GetComponent<MapData>().floor == InfoSystem.instance.currentFloor &&
            (_obj.gameObject.GetComponent<MapData>().index == InfoSystem.instance.index - 2
            || _obj.gameObject.GetComponent<MapData>().index == InfoSystem.instance.index
            || _obj.gameObject.GetComponent<MapData>().index == InfoSystem.instance.index + 2
            ))
            {
                _obj.transform.GetChild(0).GetComponent<Image>().color = new Color(0, 0, 0, 1f);
                _checkMap = false;
                InfoSystem.instance.currentFloor++;
                InfoSystem.instance.index = _obj.gameObject.GetComponent<MapData>().index;
                _curMapData = _obj.gameObject.GetComponent<MapData>();
                switch (_obj.gameObject.GetComponent<MapData>().mapData)
                {
                    case 0:
                        _obj.transform.GetChild(1).gameObject.SetActive(true);
                        _obj.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = _obj.transform.GetChild(0).gameObject.GetComponent<MapData>().Complete;
                        _obj.GetComponent<Image>().raycastTarget = false;
                        changeObj.SetActive(true);

                        chestObj.GetComponent<ChestManager>().iNum = _obj.GetComponent<MapData>().EventNum;
                        chestObj.GetComponent<ChestManager>().chestImage.sprite = chestObj.GetComponent<ChestManager>().closeChest[chestObj.GetComponent<ChestManager>().iNum];
                        Invoke("GoChest", 3f);

                        break;
                    case 1:
                        _obj.transform.GetChild(1).gameObject.SetActive(true);
                        _obj.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = _obj.transform.GetChild(0).gameObject.GetComponent<MapData>().Complete;
                        changeObj.SetActive(true);
                        _obj.GetComponent<Image>().raycastTarget = false;
                        chestObj.GetComponent<ChestManager>().iNum = _obj.GetComponent<MapData>().EventNum;
                        chestObj.GetComponent<ChestManager>().chestImage.sprite = chestObj.GetComponent<ChestManager>().closeChest[chestObj.GetComponent<ChestManager>().iNum];
                        Invoke("GoChest", 3f);

                        break;
                    case 2:
                        _obj.transform.GetChild(1).gameObject.SetActive(true);
                        _obj.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = _obj.transform.GetChild(0).gameObject.GetComponent<MapData>().Complete;
                        changeObj.SetActive(true);
                        _obj.GetComponent<Image>().raycastTarget = false;
                        Invoke("GoRest", 3f);

                        break;
                    case 3:
                        _obj.transform.GetChild(1).gameObject.SetActive(true);
                        _obj.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = _obj.transform.GetChild(0).gameObject.GetComponent<MapData>().Complete;
                        changeObj.SetActive(true);
                        _obj.GetComponent<Image>().raycastTarget = false;
                        Invoke("GoBattle", 3f);

                        break;
                    case 4:
                        _obj.transform.GetChild(1).gameObject.SetActive(true);
                        _obj.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = _obj.transform.GetChild(0).gameObject.GetComponent<MapData>().Complete;
                        changeObj.SetActive(true);
                        _obj.GetComponent<Image>().raycastTarget = false;
                        eventObj.GetComponent<EventManager>().iNum = _obj.GetComponent<MapData>().EventNum;
                        Invoke("GoEvent", 3f);

                        break;
                    case 5:
                        _obj.transform.GetChild(1).gameObject.SetActive(true);
                        _obj.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = _obj.transform.GetChild(0).gameObject.GetComponent<MapData>().Complete;
                        changeObj.SetActive(true);
                        _obj.GetComponent<Image>().raycastTarget = false;
                        Invoke("GoBattle", 3f);

                        break;

                    case 8:
                        _obj.transform.GetChild(1).gameObject.SetActive(true);
                        _obj.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = _obj.transform.GetChild(0).gameObject.GetComponent<MapData>().Complete;
                        changeObj.SetActive(true);
                        _obj.GetComponent<Image>().raycastTarget = false;
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
        _battleManager.Init(_curMapData);
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
