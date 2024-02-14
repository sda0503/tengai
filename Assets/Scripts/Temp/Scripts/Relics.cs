using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Relics
{
    public string relicsName;
    public int curHP;
    public int maxHP;
    public int addATK;
    public int addDEF;

    public Sprite icon;

    public Relics(string _relicsName, int _curHP, int _maxHP, int _addATK, int _addDEF, Sprite icon)
    {
        relicsName = _relicsName;
        curHP = _curHP;
        maxHP = _maxHP;
        addATK = _addATK;
        addDEF = _addDEF;
        this.icon = icon;
    }

}
