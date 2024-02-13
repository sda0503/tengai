using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MonsterManager", menuName = "Data/Manager/MonsterManager")]
public class MonsterDataManager : ScriptableObject
{
    [SerializeField] private List<Monsters> defaultDatas;//하나의 방에 어떤 몬스터가 있는지
    [SerializeField] private List<Monsters> EliteDatas;
    [SerializeField] private List<string> BossDatas;

    public StatSystem targetSystem;
    [HideInInspector] public GameObject spowPivot;

    [NonSerialized] public List<MonsterObject> activeMonster = new();
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

    public bool CheckMonster()
    {
        foreach (var monster in activeMonster)
        {
            if (monster != null)
            {
                return true;
            }
        }
        return false;
    }

    public void CreateDefalutMonster()
    {
        int r = UnityEngine.Random.Range(0, defaultDatas.Count);
        List<string> monsters = defaultDatas[r].MonsterDatas;
        for (int i = 0; i < monsters.Count; i++)
        {
            var data = ObjectDatas.I.GetData(monsters[i]);
            var monster = Instantiate(data.prefab, spowPivot.transform).GetComponent<MonsterObject>();
            monster.UpdateMonster(data, targetSystem);
            activeMonster.Add(monster);
        }
    }

    public void CreateEliteMonster()
    {
        int r = UnityEngine.Random.Range(0, EliteDatas.Count);
        List<string> monsters = EliteDatas[r].MonsterDatas;
        for (int i = 0; i < monsters.Count; i++)
        {
            var data = ObjectDatas.I.GetData(monsters[i]);
            var monster = Instantiate(data.prefab, spowPivot.transform).GetComponent<MonsterObject>();
            monster.UpdateMonster(data, targetSystem);
            activeMonster.Add(monster);
        }
    }

    public void CreateBossMonster(int number)
    {
        var data = ObjectDatas.I.GetData(BossDatas[number - 1]);
        var monster = Instantiate(data.prefab, spowPivot.transform).GetComponent<MonsterObject>();
        monster.UpdateMonster(data, targetSystem);
        activeMonster.Add(monster);
    }

    [System.Serializable]
    private class Monsters
    {
        public List<string> MonsterDatas;
    }
}