using UnityEngine;

public abstract class MonsterBase : MonoBehaviour
{
    [SerializeField] private Buff _buff;
    [SerializeField] public StatSystem statSystem;
    [SerializeField] public StatSystem target;
    public abstract void Attack();
}