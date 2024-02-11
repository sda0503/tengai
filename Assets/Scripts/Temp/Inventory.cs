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
        //        Debug.Log($"{newPotion} + �� �߰��߽��ϴ�");
        //        return;
        //    }
        //}

        Debug.Log("�κ��丮�� ���� á���ϴ�.");
    }

    public void DropPotion(int potionSlotIndex)
    {
        //if (potions[potionSlotIndex] == null)
        //{
        //    Debug.Log("�� �����Դϴ�.");
        //    return;
        //}

        //potions[potionSlotIndex] = null;
        //Debug.Log("������ ���Ƚ��ϴ�.");
    }

    public void AddRelics(Relics newRelics)
    {
        relics.Add(newRelics);
        Debug.Log($"{newRelics} ������ �߰��߽��ϴ�.");
    }
}
