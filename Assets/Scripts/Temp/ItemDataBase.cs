using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemDataBase : MonoBehaviour
{
    public static List<Potion> potionList = new List<Potion>();
    public static List<Relics> relicsList = new List<Relics>();

    void Awake()
    {
        AddPotionData();
        AddRelicsData();
    }

    public void AddPotionData()
    {
        potionList.Add(new Potion(0,"","",RarityType.Common, false));
        potionList.Add(new Potion(1, "", "", RarityType.Common, false));
        potionList.Add(new Potion(2, "", "", RarityType.Common, false));
        potionList.Add(new Potion(3, "", "", RarityType.Common, false));
    }

    public void AddRelicsData()
    {
        relicsList.Add(new Relics(0,"","",RarityType.Common));
        relicsList.Add(new Relics(0, "", "", RarityType.Common));
        relicsList.Add(new Relics(0, "", "", RarityType.Common));
        relicsList.Add(new Relics(0, "", "", RarityType.Common));
    }
}


