using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CardEffect_Stat : ICardEffect
{
    public Buff buff;
    public void OnUse(StatSystem statSystem = null)
    {
        statSystem?.AddBuff(buff);
    }

    public void OnUse(List<StatSystem> statSystemList)
    {
        throw new System.NotImplementedException();
    }
}
