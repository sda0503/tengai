using System;
using System.Collections.Generic;
using UnityEngine;

public enum StatType
{
    HP,
    Cost,
    ATK,
    DEF
}

public class StatSystem : MonoBehaviour
{
    [SerializeField] private CharacterBaseStat _stat;
    private List<BuffData> _buffDatas = new List<BuffData>(MAX_SIZE);
    private List<BuffData> _invokeBuffDatas = new List<BuffData>(MAX_SIZE);

    private const int MAX_SIZE = 20;

    public void TurnStart()
    {
        for(int i = 0; i < _invokeBuffDatas.Count; i++)
        {
            if (_invokeBuffDatas[i].CheckInvoke())
            {
                StatChange(_invokeBuffDatas[i].buff);
                _invokeBuffDatas.RemoveAt(i);
            }
        }

        for(int i = 0; i < _buffDatas.Count; i++)
        {
            if (!_buffDatas[i].CheckBuffEnd())
            {
                StatChange(_buffDatas[i].buff);
                _buffDatas.RemoveAt(i);
            }
        }
    }

    public void SetStat(Buff data)
    {
        if (data.invokeTurn > 0)
        {
            _invokeBuffDatas.Add(new BuffData(data, 0));
            return;
        }

        StatChange(data);
        if (data.maxTurn > 1)
        {
            _buffDatas.Add(new BuffData(data, 1));
        }
    }

    private void StatChange(Buff data)
    {
        switch(data.type)
        {
            case StatType.HP:
                {
                    _stat.HP += data.amount;
                }
                break;
            case StatType.ATK:
                {
                    _stat.ATK += data.amount;
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
        }
    }

    private class BuffData
    {
        public Buff buff;
        public int turn;

        public BuffData(Buff buff, int turn)
        {
            this.buff = buff;
            this.turn = turn;
        }

        public bool CheckBuffEnd()
        {
            return buff.maxTurn == turn;
        }

        public bool CheckInvoke()
        {
            if(buff.invokeTurn-- == 0) return true;

            return false;
        }
    }
}

[System.Serializable]
public class Buff
{
    public StatType type;
    public int amount;
    public int maxTurn = 1;
    public int invokeTurn;

    public Action OnEffect;
}