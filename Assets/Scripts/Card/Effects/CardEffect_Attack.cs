using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CardEffect_Attack : ICardEffect
{
    public int attackValue;
    public Target target;
    public AudioClip clip;

    public void OnUse(StatSystem statSystem = null)
    {
        int result = attackValue;

        if (statSystem.HasBuff("취약"))
            result = attackValue * 150 / 100;

        statSystem?.TakeDamage(result);

        if(clip != null)
            SoundManager.PlayClip(clip);
    }

    public void OnUse(List<StatSystem> statSystemList)
    {
        foreach(StatSystem statSystem in statSystemList)
        {
            int result = attackValue;

            if (statSystem.HasBuff("취약"))
                result = attackValue * 150 / 100;

            statSystem?.TakeDamage(result);
        }

        if (clip != null)
            SoundManager.PlayClip(clip);
    }
}
