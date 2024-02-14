using System.Collections.Generic;

public interface ICardEffect
{
    public void OnUse(StatSystem statSystem = null);

    public void OnUse(List<StatSystem> statSystemList);
}
