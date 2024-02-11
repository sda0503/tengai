using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RarityType
{
    Common,
    Advanced,
    Rare
}

[CreateAssetMenu(fileName = "Potion", menuName = "New Potion")]
public class PotionData : ScriptableObject
{
    [Header("Info")]
    public string potionName;
    public string description;
    public RarityType type;
    public Sprite icon;

    public bool useOnEnemy;

}
