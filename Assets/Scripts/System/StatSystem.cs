using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum StatType
{
    HP,
    Cost,
    ATK,
    DEF,
    MaxHP,
    MaxCost,
    Debuff
}

public class StatSystem : MonoBehaviour
{
    [SerializeField] private CharacterBaseStat _stat;
    [SerializeField] private Dictionary<string, Buff> _buffs;
    [SerializeField] private CharacterBaseStat _buffStat = new();
    private Animator _animator;
    private HPBar _bar;

    public int HP { get { return _stat.HP + _buffStat.HP; } }
    public int MaxHP { get { return _stat.MaxHP + _buffStat.MaxHP - 1; } }
    public int COST { get { return _stat.Cost + _buffStat.Cost; } }
    public int MaxCost { get { return _stat.MaxCost + _buffStat.MaxCost; } }
    public int ATK { get { return _stat.ATK + _buffStat.ATK; } }
    public int DEF { get { return _stat.DEF + _buffStat.DEF; } }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void TestDEF(int amount)
    {
        _buffStat.DEF += amount;
        _bar.UpdateHPBar(HP, MaxHP, DEF);
    }

    public void Attack()
    {
        _animator.SetTrigger("Attack");
    }

    public bool HasBuff(string name)
    {
        return _buffs.TryGetValue(name, out _);
    }

    public void TakeDamage(int amount)
    {
        int result = Math.Clamp(amount - DEF, 0, int.MaxValue);
        _buffStat.DEF -= amount;

        _stat.HP -= result;
        if (_stat.HP == 0)
        {
            if (TryGetComponent<MonsterBase>(out _))
            {
                Destroy(gameObject);
            }
            else
                Debug.Log("PlayerDie");
            //_animator.SetTrigger("Die");
        }
        else
        {
            _animator.SetTrigger("TakeDamage");
        }

        _bar.UpdateHPBar(HP, MaxHP, DEF);
    }

    public void TakeCost(int amount)
    {
        _stat.Cost -= amount;
    }

    public void UpdateStats()
    {
        InitStat();
        foreach(var buff in _buffs)
        {
            var value = buff.Value;
            if(value.invokeTurn == 0)
            {
                AddStat(value);
            }
        }
        _bar.UpdateHPBar(HP, MaxHP, DEF);
    }

    public void UpdateBuffs()
    {
        foreach (var buff in _buffs)
        {
            var key = buff.Key;
            var value = buff.Value;
            if (value.invokeTurn > 0)
            {
                --value.invokeTurn;
                continue;
            }
            else if (value.maxTurn == value.turn)
            {
                value.turn = 0;
                _buffs.Remove(key);
                continue;
            }
            value.turn++;
        }
        _bar.UpdateHPBar(HP, MaxHP, DEF);
        _bar.UpdateBuffSlots();
    }

    public void AddBuff(Buff buff)
    {
        if (_buffs.ContainsKey(buff.name))
        {
            _buffs[buff.name].turn = 0;
            return;
        }
        _buffs.Add(buff.name, buff);
        if (buff.type == StatType.ATK || buff.type == StatType.Debuff)
            _bar.CreateBuffSlot(buff);
    }

    public void RemoveBuff(Buff buff)
    {
        if (_buffs.ContainsKey(buff.name))
        {
            _buffs.Remove(buff.name);
            return;
        }
        Debug.Log($"Error Buff Name : {buff.name}");
    }

    public void SettingStat(CharacterBaseStat stat)
    {
        if (_bar == null) _bar = GetComponentInChildren<HPBar>();
        _stat = stat;
        _buffStat.MaxHP = 1;
        _bar.UpdateHPBar(HP, MaxHP, DEF);
    }

    private void InitStat()
    {
        _buffStat.HP = 0;
        _buffStat.MaxHP = 0;
        _buffStat.Cost = 0;
        _buffStat.MaxCost = 0;
        _buffStat.ATK = 0;
        _buffStat.DEF = 0;
    }

    private void AddStat(Buff data)
    {
        switch (data.type)
        {
            case StatType.HP:
                {
                    _buffStat.HP += data.amount;
                }
                break;
            case StatType.ATK:
                {
                    _buffStat.ATK += data.amount;
                }
                break;
            case StatType.DEF:
                {
                    _buffStat.DEF += data.amount;
                }
                break;
            case StatType.Cost:
                {
                    _buffStat.Cost += data.amount;
                }
                break;
            case StatType.MaxHP:
                {
                    _buffStat.MaxHP += data.amount;
                }
                break;
            case StatType.MaxCost:
                {
                    _buffStat.MaxCost += data.amount;
                }
                break;
        }
    }
}

[System.Serializable]
public class Buff
{
    public string name;
    public StatType type;
    public int amount;
    public int turn = 0;
    public int maxTurn;
    public int invokeTurn;
    public Sprite icon;

    public Buff(string name, StatType type, int amount, Sprite icon, int maxTurn = 1, int invokeTurn = 0)
    {
        this.name = name;
        this.type = type;
        this.amount = amount;
        this.maxTurn = maxTurn;
        this.invokeTurn = invokeTurn;
        this.icon = icon;
    }

    public Buff(Buff buff)
    {
        this.name = buff.name;
        this.type = buff.type;
        this.amount = buff.amount;
        this.turn = buff.turn;
        this.maxTurn = buff.maxTurn;
        this.invokeTurn = buff.invokeTurn;
        this.icon = buff.icon;
    }
}