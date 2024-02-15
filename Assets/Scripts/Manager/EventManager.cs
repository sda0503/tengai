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

    public GameObject mapObj;
    public Canvas _mainCanvas;

    public Sprite[] _sprite;
    public Text[] _text;
    public Image eventImage;
    public Button[] buttons;

    public int iNum;

    void Start()
    {
        _gr = _mainCanvas.GetComponent<GraphicRaycaster>();
        _ped = new PointerEventData(null);
        _rrList = new List<RaycastResult>();
        EventSet(iNum);
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

    public void AcceptEvent(int iNum)
    {
        gameObject.SetActive(false);
        mapObj.SetActive(true);
        switch (iNum)
        {
            case 0:
                InfoSystem.instance.player.SetHP(100);
                break;
            case 1:
                InfoSystem.instance.SetGold(175);
                break;

            case 2:
                InfoSystem.instance.SetGold(75);
                break;

            case 3:
                _text[2].text = "[열어본다] 유물을 획득합니다.";
                break;
        }
        InfoSystem.instance.ShowDate();
    }

    public void Skip(int iNum)
    {
        gameObject.SetActive(false);
        mapObj.SetActive(true);
        switch (iNum)
        {
            case 0:
                break;
            case 1:
                break;

            case 2:
                InfoSystem.instance.SetGold(-33);
                break;

            case 3:
                break;
        }
    }

    public void EventSet(int iNum)
    {
        switch (iNum)
        {
            case 0:
                eventImage.sprite = _sprite[48];
                _text[0].text = "밝은 빛";
                _text[1].text = "당신은 방 한가운데에서 일렁이는 빛의 덩어리 를 발견했습니다.\r\n\r\n따스한 빛과  황홀한 문양이 당신을 향해 손짓합니다.";
                _text[2].text = "[들어간다] 체력을 모두 회복합니다.";
                _text[3].text = "[돌아간다]";

                break;
            case 1:
                eventImage.sprite = _sprite[32];
                _text[0].text = "배에에에엠";
                _text[1].text = "당신은 땅에 뚤린 큰 구멍을 보고 방으로 들어갔습니다. 구멍앞에 도달하자 갑자기 커다란 뱀이 튀어나왔습니다.\r\n\r\n\"하 하하! 안녕안녕? 무엇이 우리를 여기로 이끌었을까? 안녕 모험가, 간단한 질문을 하나 할게.\n인생의 가장 좋은 점은 바로 뭐든지 살 수 있다는 것 아니겠어?\n너도 그렇게 생각하지?\"";
                _text[2].text = "[승낙] 골드를 175 획득합니다.";
                _text[3].text = "[거절]";
                break;

            case 2:
                eventImage.sprite = _sprite[25];
                _text[0].text = "끈적이 천지";
                _text[1].text = "당신은 물웅덩이에 빠졌습니다.\n 그 웅덩이는 끈적이 슬라임으로 가득 차 있습니다!!\n서서히 끈적이들이 타오르는 것을 느낀 당신은 극도로 흥분하여 수분간 스스로를 긁었습니다.\n귀와 코를 포함한 모든 곳에서 끈적임이 느껴집니다.\r\n\r\n기어 올라와보니 당신은 일부 골드가 사라진것을 알아차렸습니다.\n웅덩이를 되돌아보니 당신은 잃어버린 동전들이 다른 불행한 모험가들의 골드와 함께 웅덩이 속에 섞여있는 것을 알아차렸습니다.";
                _text[2].text = "[골드를 챙긴다] 골드를 75 획득합니다.";
                _text[3].text = "[건드리지 않는다] 골드를 33 잃습니다.";
                break;

            case 3:
                eventImage.sprite = _sprite[37];
                _text[0].text = "영묘";
                _text[1].text = "일련의 무덤 사이를 지나가던 중, 원형방의 중심에 놓인 보석으로 가득한 커다란 석관과 마주쳤습니다.\n관에 적혀있는 글을 알아볼 수는 없지만, 검은 안개가 옆에서 스며나오는 것을 볼 수 있습니다.";
                _text[2].text = "[열어본다] 유물을 획득합니다.";
                _text[3].text = "[떠나기]";
                break;
        }
        buttons[0].onClick.AddListener(() => AcceptEvent(iNum));
        buttons[1].onClick.AddListener(() => Skip(iNum));
    }
}
