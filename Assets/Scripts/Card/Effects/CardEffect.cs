using System.Collections.Generic;

public enum Target
{
    Player,
    TargetEnemy,
    AllEnemy,
    RandomEnemy
}

public interface ICardEffect
{
    public void OnUse(StatSystem statSystem = null);

    public void OnUse(List<StatSystem> statSystemList);
}
