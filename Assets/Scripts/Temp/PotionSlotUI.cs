using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PotionSlotUI : MonoBehaviour
{
    public Button button;
    public Image icon;
    //public Text quatityText;
    private PotionSlot curSlot;
    //private Outline outline;

    public int index;

    private void Awake()
    {
        //outline = GetComponent<Outline>();
    }

    public void Set(PotionSlot slot)
    {
        curSlot = slot;
        icon.gameObject.SetActive(true);
        icon.sprite = slot.potion.icon;

    }

    public void Clear()
    {
        curSlot = null;
        icon.gameObject.SetActive(false);
    }

    public void OnButtonClick()
    {
        Inventory.instance.SelectPotion(index);
    }
}
