using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MonsterManager", menuName = "Data/Manager/MonsterManager")]
public class MonsterDataManager : ScriptableObject
{
    [SerializeField] private List<Monsters> defaultDatas;//하나의 방에 어떤 몬스터가 있는지
    [SerializeField] private List<Monsters> EliteDatas;
    [SerializeField] private List<MonsterData> BossDatas;

    public StatSystem targetSystem;
    [HideInInspector] public GameObject spowPivot;

    public List<MonsterObject> activeMonster;
    public bool isTurn;
    public void MonstersAttack()
    {
        isTurn = false;
        foreach (var monster in activeMonster)
        {
            monster?.Attack();
        }
        isTurn = true;
    }

    public void CreateDefalutMonster()
    {
        int r = Random.Range(0, defaultDatas.Count);
        List<MonsterData> monsters = defaultDatas[r].MonsterDatas;
        for (int i = 0; i < monsters.Count; i++)
        {
            var monster = Instantiate(monsters[i].prefab, spowPivot.transform).GetComponent<MonsterObject>();
            monster.UpdateMonster(monsters[i], targetSystem);
            activeMonster.Add(monster);
        }
    }

    public void CreateEliteMonster()
    {
        int r = Random.Range(0, EliteDatas.Count);
        List<MonsterData> monsters = EliteDatas[r].MonsterDatas;
        for (int i = 0; i < monsters.Count; i++)
        {
            var monster = Instantiate(monsters[i].prefab, spowPivot.transform).GetComponent<MonsterObject>();
            monster.UpdateMonster(monsters[i], targetSystem);
            activeMonster.Add(monster);
        }
    }

    public void CreateBossMonster(int number)
    {
        var monster = Instantiate(BossDatas[number - 1].prefab, spowPivot.transform).GetComponent<MonsterObject>();
        monster.UpdateMonster(BossDatas[number - 1], targetSystem);
        activeMonster.Add(monster);
    }

    [System.Serializable]
    private class Monsters
    {
        public List<MonsterData> MonsterDatas;
    }
}
[System.Serializable]
public class MonsterData
{
    public string Name;
    public CharacterBaseStat stat;
    public GameObject prefab;
}
