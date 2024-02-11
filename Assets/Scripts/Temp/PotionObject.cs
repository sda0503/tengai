using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionObject : MonoBehaviour
{
    public PotionData potion;

    public void AddPotion()
    {
        Inventory.instance.AddPotion(potion);
    }
}
