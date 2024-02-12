using TMPro;
using UnityEditor.Animations;
using UnityEngine;

public class MonsterObject : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    private Animator animator;
    private StatSystem statSystem;
    private MonsterBase monsterBase;

    public void Attack()
    {
        animator.SetTrigger("Attack");
        GetComponent<MonsterBase>().Attack();
    }

    public void UpdateStat()
    {
        statSystem.UpdateStats();
    }

    public void TurnEnd()
    {
        statSystem.UpdateBuffs();
    }

    private void OnEnable()
    {
        animator = GetComponent<Animator>();
        statSystem = GetComponent<StatSystem>();
        monsterBase = GetComponent<MonsterBase>();
    }

    public void UpdateMonster(MonsterData data, StatSystem target)
    {
        statSystem.SettingStat(data.stat);
        nameText.text = data.Name;
        monsterBase._target = target;
    }
}

