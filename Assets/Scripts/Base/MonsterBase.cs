using UnityEngine;

public abstract class MonsterBase : MonoBehaviour
{
    [SerializeField] private Buff _buff;
    [SerializeField] public StatSystem _target;
    public abstract void Attack();
}