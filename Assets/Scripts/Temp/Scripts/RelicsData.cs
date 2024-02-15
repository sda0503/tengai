using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "Relics", menuName = "New Relics")]

[System.Serializable]
public class RelicsData : ScriptableObject
{
    [Header("Info")]
    public string RelcisName;
    public string Relcisdescription;
    public int curHP;
    public int maxHP;
    public int addATK;
    public int addDEF;
    public Sprite icon;

}
