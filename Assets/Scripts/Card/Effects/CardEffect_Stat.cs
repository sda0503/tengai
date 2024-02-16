using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CardEffect_Stat : ICardEffect
{
    public Buff buff;
    public Target target;
    public AudioClip clip;

    public void OnUse(StatSystem statSystem = null)
    {
        Buff newBuff = new Buff(buff);
        if (statSystem != null) statSystem.AddBuff(newBuff);

        if (clip != null)
            SoundManager.PlayClip(clip);
    }

    public void OnUse(List<StatSystem> statSystemList)
    {
        foreach (StatSystem statSystem in statSystemList)
        {
            Buff newBuff = new Buff(buff);
            if (statSystem != null) statSystem.AddBuff(newBuff);
        }

        if (clip != null)
            SoundManager.PlayClip(clip);
    }
}
