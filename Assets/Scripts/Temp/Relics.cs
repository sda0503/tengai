using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Relics : MonoBehaviour
{
    public int id;
    public string relicsName;
    public string relicDescription;
    public RarityType rarityType;

    public Relics(int id, string relicsName, string relicDescription, RarityType rarityType)
    {
        this.id = id;
        this.relicsName = relicsName;
        this.relicDescription = relicDescription;
        this.rarityType = rarityType;
    }
}

