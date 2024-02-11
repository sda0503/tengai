using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

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

    public void AddItem(PotionData potion)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i] == null)
            {
                slots[i].potion = potion;
                UpdateUI();
                return;
            }
        }
    }

    public void SelectPotion(int index)
    {

    }

    public void UpdateUI()
    {
        //for (int i = 0; i < slots.Length; i++)
        //{
        //    if (slots[i].potion  != null)
        //        uiSlots[i].Set(slots[i]);
        //    else
        //        uiSlots[i].Clear();
        //}
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
        //relics.Add(newRelics);
        //Debug.Log($"{newRelics} 유물을 추가했습니다.");
    }

    public void OnDrinkButton()
    {

    }

    public void OnDropButton()
    {

    }

    public void RemoveSelectedPotion()
    {

    }

    public void RemovePotion()
    {

    }

    public bool HasPotions(PotionData potion)
    {
        return false;
    }
}
