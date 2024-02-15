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
    MaxCost
}

public class StatSystem : MonoBehaviour
{
    [SerializeField] private CharacterBaseStat _stat;
    [SerializeField] private List<Buff> _buffs;
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
        for (int i = 0; i < _buffs.Count; i++)
        {
            if (_buffs[i].invokeTurn == 0)
            {
                AddStat(_buffs[i]);
            }
        }
        _bar.UpdateHPBar(HP, MaxHP, DEF);
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
        _bar.UpdateHPBar(HP, MaxHP, DEF);
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

    public Buff(Buff buff)
    {
        this.type = buff.type;
        this.amount = buff.amount;
        this.turn = buff.turn;
        this.maxTurn = buff.maxTurn;
        this.invokeTurn = buff.invokeTurn;
    }
}