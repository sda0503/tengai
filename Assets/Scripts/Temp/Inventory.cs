using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PotionSlot
{
    public PotionData potion;
    public int slotNumber;
}
public class Inventory : MonoBehaviour
{
    public PotionSlot[] slots;
    public PotionSlotUI[] uiSlots;

    public List<Relics> relics = new List<Relics>();

    [Header("Selected potion")]
    private PotionSlot selectedPotion;
    private int selectedPotionIndex;
    public Text selectedPotionName;
    //public Text selectedPotionDescription;
    public Text selectedPotionAdditionalDescription;
    //public Text selectedPotionStatValues;
    public GameObject useButton;
    public GameObject dropButton;

    public static Inventory instance;


    private void Awake()
    {
        instance = this; 
    }
    private void Start()
    {
        slots = new PotionSlot[uiSlots.Length];

        //for (int i = 0; i < slots.Length; i++)
        //{
        //    slots[i] = new PotionSlot();
        //    uiSlots[i].index = i;
        //    uiSlots[i].Clear();
        //}

        //ClearSeletecItemWindow();
    }
    public void AddPotion(PotionData newPotion)
    {
        //for (int i = 0; i < potions.Length; i++)
        //{
        //    if (potions[i] == null)
        //    {
        //        potions[i] = (Potion)newPotion;
        //        Debug.Log($"{newPotion} + 을 추가했습니다");
        //        return;
        //    }
        //}

        Debug.Log("인벤토리가 가득 찼습니다.");
    }

    public void DropPotion(int potionSlotIndex)
    {
        //if (potions[potionSlotIndex] == null)
        //{
        //    Debug.Log("빈 슬롯입니다.");
        //    return;
        //}

        //potions[potionSlotIndex] = null;
        //Debug.Log("포션을 버렸습니다.");
    }

    public void AddRelics(Relics newRelics)
    {
        relics.Add(newRelics);
        Debug.Log($"{newRelics} 유물을 추가했습니다.");
    }
}
