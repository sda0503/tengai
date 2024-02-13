using TMPro;
using UnityEditor.Animations;
using UnityEngine;

public class MonsterObject : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    private MonsterBase monsterBase;

    public void Attack()
    {
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
        monsterBase = GetComponent<MonsterBase>();
    }

    public void UpdateMonster(ObjectData data, StatSystem target)
    {
        nameText.text = data.Name;
        monsterBase.statSystem = GetComponent<StatSystem>();
        monsterBase.statSystem.SettingStat(data.stat);
        monsterBase.target = target;
    }
}

