using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    public int id;
    public string potionName;
    public string potionDescription;
    public RarityType rarityType;

    public bool canUseToEnemy;

    //public Potion(int id, string potionName, string potionDescription, RarityType rarityType, bool canUseToEnemy)
    //{
    //    this.id = id;
    //    this.potionName = potionName;
    //    this.potionDescription = potionDescription;
    //    this.rarityType = rarityType;
    //    this.canUseToEnemy = canUseToEnemy;
    //}

    //public void UsePotion(Potion potion)
    //{
    //    if(canUseToEnemy)
    //    {
    //        switch(potion.id)
    //        {
    //            case 0:
    //            break; 
    //            case 1:
    //            break;

    //        }
    //    }
    //    else
    //    {
    //        switch(potion.id)
    //        {
    //            case 0:
    //            break;
    //            case 1:
    //            break;
    //        }
    //    }
    //}
}
