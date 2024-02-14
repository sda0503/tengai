using System.Collections.Generic;

[System.Serializable]
public class CardEffect_Draw : ICardEffect
{
    public int drawCardNum;

    public void OnUse(StatSystem statSystem = null)
    {
        CardManager.instance.DrawCard(drawCardNum);
    }

    public void OnUse(List<StatSystem> statSystemList)
    {
        throw new System.NotImplementedException();
    }
}
