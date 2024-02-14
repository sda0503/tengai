using UnityEngine;
using UnityEngine.UI;

public class InfoSystem : MonoBehaviour
{
    public static InfoSystem instance = null;
    public int[] cardsList = new int[3]; //카드 정보
    public int gold;
    public int maxHp = 80;
    public int currentHp = 80;
    public int playerPoint = 3;
    public int currentFloor = 0;
    public int index = 0;

    public bool _isTuto = false;

    public Text[] text;

    void Awake()
    {
        if (instance == null) instance = this;
        else 
         if (instance != this) Destroy(this.gameObject);
    }

    void Start()
    {
        cardsList[0] = 10; //전체 카드수
        cardsList[1] = 10; //현재 카드수
        cardsList[2] = 0;  //버린 카드수

        //currentHp = maxHp;
        currentHp = 10;
        gold = 99;
        ShowDate();
    }

    public void SetGold(int addGold)
    {
        gold += addGold;
        Destroy(gameObject);
    }

    public void ShowDate()
    {
        text[0].text = currentHp.ToString();
        text[1].text = maxHp.ToString();
        text[2].text = gold.ToString();
        text[3].text = cardsList[0].ToString();
        text[4].text = cardsList[1].ToString();
        text[5].text = cardsList[2].ToString();
        text[6].text = playerPoint.ToString();
        text[7].text = currentFloor.ToString();
    }
}
