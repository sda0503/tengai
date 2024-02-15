public class SlimeS : MonsterBase
{
    public override void Attack()
    {
        base.Attack();
        target.TakeDamage(statSystem.ATK);
    }
}
