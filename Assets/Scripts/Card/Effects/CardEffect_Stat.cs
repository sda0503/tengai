using System.Collections.Generic;

[System.Serializable]
public class CardEffect_Stat : ICardEffect
{
    public Buff buff;
    public Target target;
    public void OnUse(StatSystem statSystem = null)
    {
        Buff newBuff = new Buff(buff);
        statSystem?.AddBuff(newBuff);
    }

    public void OnUse(List<StatSystem> statSystemList)
    {
        foreach (StatSystem statSystem in statSystemList)
        {
            Buff newBuff = new Buff(buff);
            statSystem?.AddBuff(newBuff);
        }
    }
}
