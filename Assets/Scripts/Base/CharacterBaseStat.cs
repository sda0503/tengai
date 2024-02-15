using System;
using UnityEngine;

[System.Serializable]
public struct CharacterBaseStat
{
    [SerializeField] private int _hp;
    [SerializeField] private int _maxHP;
    [SerializeField] private int _cost;
    [SerializeField] private int _maxCost;
    [SerializeField] private int _power;
    [SerializeField] private int _defense;

    #region Const MAX Stat
    private const int MAX_HP = 300;
    private const int MAX_COST = 10;
    private const int MAX_ATK = 999;
    private const int MAX_DEF = 999;
    #endregion

    #region Stat Getter Setter
    public int HP { get { return _hp; } set { _hp = Math.Clamp(value, 0, _maxHP); } }
    public int MaxHP { get { return _maxHP; } set { _maxHP = Math.Clamp(value, 1, MAX_HP); } }
    public int Cost { get { return _cost; } set 
        {
            _cost = Math.Clamp(value, 0, MaxCost);
        }
    }
    public int MaxCost { get { return _maxCost; } set { _maxCost = Math.Clamp(value, 0, MAX_COST); } }
    public int ATK { get { return _power; } set { _power = Math.Clamp(value, 0, MAX_ATK); } }
    public int DEF { get { return _defense; } set { _defense = Math.Clamp(value, 0, MAX_DEF); } }
    #endregion
}