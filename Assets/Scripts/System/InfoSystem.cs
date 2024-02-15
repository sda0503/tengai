using UnityEngine;
using UnityEngine.UI;

public class InfoSystem : MonoBehaviour
{
    public static InfoSystem instance = null;
    public int gold = 0;
    public int currentFloor = 0;
    public int index = 0;

    public bool _isTuto = false;

    public Text[] text;

    public StatSystem player;


    void Awake()
    {
        if (instance == null) instance = this;
        else 
         if (instance != this) Destroy(this.gameObject);
    }

    void Start()
    {
        text[2].text = CardManager.instance.deck.Count.ToString();
        ShowDate();
    }


    public void SetGold(int addGold)
    {
        gold += addGold;
        if(gold < 0) gold = 0;
    }

    public void ShowDate()
    {
        text[0].text = gold.ToString();
        text[1].text = currentFloor.ToString();
        text[3].text = player.HP.ToString();
        text[4].text = player.MaxHP.ToString();
        text[5].text = player.COST.ToString();
        text[6].text = player.MaxCost.ToString();
    }
}
