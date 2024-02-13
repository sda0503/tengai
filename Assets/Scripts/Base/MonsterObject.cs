using TMPro;
using UnityEditor.Animations;
using UnityEngine;

public class MonsterObject : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    private Animator animator;
    private MonsterBase monsterBase;

    public void Attack()
    {
        animator.SetTrigger("Attack");
        monsterBase.Attack();
    }

    public void UpdateStat()
    {
        monsterBase.statSystem.UpdateStats();
    }

    public void TurnEnd()
    {
        monsterBase.statSystem.UpdateBuffs();
    }

    private void OnEnable()
    {
        animator = GetComponent<Animator>();
        monsterBase = GetComponent<MonsterBase>();
    }

    public void UpdateMonster(MonsterData data, StatSystem target)
    {
        nameText.text = data.Name;
        monsterBase.statSystem = GetComponent<StatSystem>();
        monsterBase.statSystem.SettingStat(data.stat);
        monsterBase.target = target;
    }
}

