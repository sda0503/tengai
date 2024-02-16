using TMPro;
using UnityEngine;

public class MonsterObject : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    private MonsterBase monsterBase;

    private GameObject monsterObject;

    public void Attack()
    {
        monsterBase.Attack();
    }

    public void UpdateAttackIcon()
    {
        monsterBase.CheckATKText();
    }

    public bool HasMonster()
    {
        if (monsterObject == null) return false;
        return monsterObject.activeSelf;
    }

    public StatSystem GetStat()
    {
        return monsterBase.statSystem;
    }

    public void TurnEnd()
    {
        monsterBase.statSystem.UpdateBuffs();
        monsterBase.UpdateAttack();
    }

    public void UpdateMonster(ObjectData data, StatSystem target)
    {
        monsterBase = GetComponent<MonsterBase>();
        monsterObject = transform.GetChild(0).gameObject;
        nameText.text = data.Name;
        monsterBase.statSystem = GetComponent<StatSystem>();
        monsterBase.statSystem.SettingStat(data.stat);
        monsterBase.UpdateAttack();
        monsterBase.target = target;
    }
}

