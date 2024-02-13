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
    public int HP { get { return _hp; } set { _hp = Math.Clamp(_hp + value, 0, _maxHP); } }
    public int MaxHP { get { return _maxHP; } set { _maxHP = Math.Clamp(_maxHP + value, 1, MAX_HP); } }
    public int Cost { get { return _cost; } set 
        {
            if(_cost < value)
            {
                Debug.Log($"Cost Change fail : {_cost - value} / Cost Check Please");
                return;
            }
            _cost = Math.Clamp(_cost + value, 0, MaxCost);
        }
    }
    public int MaxCost { get { return _maxCost; } set { _maxCost = Math.Clamp(_maxCost + value, 1, MAX_COST); } }
    public int ATK { get { return _power; } set { _power = Math.Clamp(_power + value, 0, MAX_ATK); } }
    public int DEF { get { return _defense; } set { _defense = Math.Clamp(_defense + value, 0, MAX_DEF); } }
    #endregion
}