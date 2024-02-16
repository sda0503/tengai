using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    [SerializeField] private List<Buff> _buffs = new();
    [SerializeField] private Dictionary<string, Buff> _activeBuffs = new Dictionary<string, Buff>();
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

    public void SetHP(int amount)
    {
        _stat.HP += _stat.MaxHP * amount / 100;
    }

    public void RegenCost()
    {
        _stat.Cost = _stat.MaxCost;
        _stat.DEF = 0;
    }

    public void Attack()
    {
        _animator.SetTrigger("Attack");
    }

    public bool HasBuff(string name)
    {
        return _activeBuffs.TryGetValue(name, out _);
    }

    public void TakeDamage(int amount)
    {
        int result = Math.Clamp(amount - _stat.DEF, 0, int.MaxValue);
        _stat.DEF -= amount;

        _stat.HP -= result;
        if (_stat.HP == 0)
        {
            if (TryGetComponent<MonsterBase>(out _))
            {
                transform.GetChild(1).gameObject.SetActive(false);
                transform.GetChild(0).gameObject.SetActive(false);
            }
            else
                SceneManager.LoadScene("Ending");
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
        for(int i = 0; i < _buffs.Count; i++)
        {
            if (_buffs[i].invokeTurn == 0)
            {
                AddStat(_buffs[i]);
                if (_buffs[i].type == StatType.Cost || _buffs[i].type == StatType.DEF)
                {
                    _buffs.RemoveAt(i--);
                }
            }
        }
        _bar.UpdateHPBar(HP, MaxHP, DEF);
    }

    public void UpdateBuffs()
    {
        List<string> remove = new();
        foreach (var buff in _activeBuffs)
        {
            var key = buff.Key;
            var value = buff.Value;
            if (value.invokeTurn > 0)
            {
                --value.invokeTurn;
                continue;
            }

            value.turn++;
            if (value.maxTurn == value.turn)
            {
                remove.Add(key);
                continue;
            }
        }

        for (int i = 0; i < remove.Count; i++)
        {
            _activeBuffs.Remove(remove[i]);
        }

        for(int i = 0; i < _buffs.Count; i++)
        {
            if (_buffs[i].invokeTurn > 0)
            {
                --_buffs[i].invokeTurn;
                continue;
            }
            else if (_buffs[i].maxTurn == _buffs[i].turn)
            {
                _buffs.RemoveAt(i--);
                continue;
            }

            if (_buffs[i].name == "약화")
            {
                _buffs[i].amount = -(ATK * 25 / 100);
                _bar.UpdateBuffSlots();
                UpdateStats();
                continue;
            }
            _buffs[i].turn++;
        }
        UpdateStats();
        _bar.UpdateHPBar(HP, MaxHP, DEF);
        _bar.UpdateBuffSlots(); 
    }

    public void AddBuff(Buff buff)
    {
        if(buff.type == StatType.ATK || buff.type == StatType.Debuff)
        {
            if (_activeBuffs.ContainsKey(buff.name))
            {
                _activeBuffs[buff.name].maxTurn += buff.maxTurn;
                _bar.UpdateBuffSlots();
                return;
            }
            if(buff.name == "약화")
            {
                buff.type = StatType.ATK;
                buff.amount = -(ATK * 25 / 100);
                _buffs.Add(buff);
            }
            _activeBuffs.Add(buff.name, buff);
            _bar.CreateBuffSlot(buff);
        }
        else
        {
            _buffs.Add(buff);
        }
        UpdateStats();
    }

    public void RemoveBuff(Buff buff)
    {
        if (_activeBuffs.ContainsKey(buff.name))
        {
            _activeBuffs.Remove(buff.name);
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
                    _stat.DEF += data.amount;
                }
                break;
            case StatType.Cost:
                {
                    _stat.Cost += data.amount;
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