using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RarityType
{
    Common,
    Advanced,
    Rare
}

[CreateAssetMenu(fileName = "Item", menuName = "New Item")]
public class PotionData : ScriptableObject
{
    [Header("Info")]
    public string displayName;
    public string description;
    public RarityType type;
    public Sprite icon;
    public GameObject dropPrefab;

}
