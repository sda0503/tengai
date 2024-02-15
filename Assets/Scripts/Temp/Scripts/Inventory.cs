using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

[System.Serializable]
public class RelcisSlot
{
    public RelicsData relics;
}
public class Inventory : MonoBehaviour
{
    
    public RelcisSlot[] slots;
    public RelicsUI[] relicsUI;

    public static Inventory instance;

    private void Awake()
    {
        instance = this; 
    }
    private void Start()
    {
        slots = new RelcisSlot[relicsUI.Length];

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = new RelcisSlot();
            relicsUI[i].index = i;
            relicsUI[i].Clear();
        }
    }

    public void AddItem(RelicsData relics)
    {
        RelcisSlot emptySlot = GetEmptySlot();

        if (emptySlot != null)
        {
            emptySlot.relics = relics;
            UpdateUI();
            return;
        }
    }

    RelcisSlot GetEmptySlot()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].relics == null)
                return slots[i];
        }

        return null;
    }

    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].relics != null)
                relicsUI[i].Set(slots[i]);
            else
                relicsUI[i].Clear();
        }
    }

}
