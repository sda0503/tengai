using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelicsDatabase : MonoBehaviour
{
    public List<Relics> relics;
    public List<Sprite> relicsIcons = new List<Sprite>();

    void Start()
    {
        relics = new List<Relics>();
        addRelicsList();
    }

    public void addRelicsList()
    {
        relics.Add(new Relics("불타는 혈액", 6, 0,0, 0, relicsIcons[0]));
        relics.Add(new Relics("금강저", 0, 0, 1, 0, relicsIcons[1]));
        relics.Add(new Relics("매끄러운 돌", 0, 0, 0,1, relicsIcons[2]));
        relics.Add(new Relics("딸기", 0, 7, 0, 0, relicsIcons[3]));
    }
}
