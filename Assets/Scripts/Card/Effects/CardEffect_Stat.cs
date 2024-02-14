using System.Collections.Generic;

[System.Serializable]
public class CardEffect_Stat : ICardEffect
{
    public Buff buff;
    public void OnUse(StatSystem statSystem = null)
    {
        Buff newBuff = new Buff(buff);
        statSystem?.AddBuff(newBuff);
        statSystem?.UpdateStats();
    }

    public void OnUse(List<StatSystem> statSystemList)
    {
        throw new System.NotImplementedException();
    }
}
