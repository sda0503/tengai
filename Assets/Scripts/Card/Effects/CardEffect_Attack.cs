using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CardEffect_Attack : ICardEffect
{
    public int attackValue;
    public void OnUse(StatSystem statSystem = null)
    {
        statSystem?.TakeDamage(attackValue);
    }

    public void OnUse(List<StatSystem> statSystemList)
    {
        foreach(StatSystem statSystem in statSystemList)
        {
            statSystem.TakeDamage(attackValue);
        }
    }
}