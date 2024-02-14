using System;
using System.Collections.Generic;
using UnityEngine;

public enum StatType
{
    HP,
    Cost,
    ATK,
    DEF,
    MaxHP,
    MaxCost
}

public class StatSystem : MonoBehaviour
{
    [SerializeField] private CharacterBaseStat _stat;
    [SerializeField] private List<Buff> _buffs;
    private CharacterBaseStat _buffStat = new();
    private Animator _animator;

    public int HP { get { return _stat.HP + _buffStat.HP; } }
    public int MaxHP { get { return _stat.MaxHP + _buffStat.MaxHP; } }
    public int COST { get { return _stat.Cost + _buffStat.Cost; } }
    public int MaxCost { get { return _stat.MaxCost + _buffStat.MaxCost; } }
    public int ATK { get { return _stat.ATK + _buffStat.ATK; } }
    public int DEF { get { return _stat.DEF + _buffStat.DEF; } }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Attack()
    {
        _animator.SetTrigger("Attack");
    }

    public void TakeDamage(int amount)
    {
        Debug.Log(amount);
        _stat.HP -= amount;
        if (_stat.HP == 0)
        {
            _animator.SetTrigger("Die");
        }
        else
        {
            _animator.SetTrigger("TakeDamage");
        }
    }

    public void TakeCost(int amount)
    {
        _stat.Cost -= amount;
    }

    public void UpdateStats()
    {
        InitStat();
        for (int i = 0; i < _buffs.Count; i++)
        {
            if (_buffs[i].invokeTurn == 0)
            {
                AddStat(_buffs[i]);
            }
        }
    }

    public void UpdateBuffs()
    {
        for (int i = 0; i < _buffs.Count; i++)
        {
            if (_buffs[i].invokeTurn > 0)
            {
                --_buffs[i].invokeTurn;
                continue;
            }
            else if (_buffs[i].maxTurn == _buffs[i].turn)
            {
                _buffs[i].turn = 0;
                _buffs.RemoveAt(i--);
                continue;
            }
            _buffs[i].turn++;
        }
    }

    public void AddBuff(Buff buff)
    {
        if (_buffs.Contains(buff))
        {
            _buffs[_buffs.IndexOf(buff)].turn = 0;
            return;
        }
        _buffs.Add(buff);
    }

    public void RemoveBuff(Buff buff)
    {
        if (_buffs.Contains(buff))
        {
            _buffs.RemoveAt(_buffs.IndexOf(buff));
        }
    }

    public void SettingStat(CharacterBaseStat stat)
    {
        _stat = stat;
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
    public StatType type;
    public int amount;
    public int turn = 0;
    public int maxTurn;
    public int invokeTurn;

    public Buff(StatType type, int amount, int maxTurn = 1, int invokeTurn = 0)
    {
        this.type = type;
        this.amount = amount;
        this.maxTurn = maxTurn;
        this.invokeTurn = invokeTurn;
    }
}