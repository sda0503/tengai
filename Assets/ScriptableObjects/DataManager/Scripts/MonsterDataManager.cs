using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MonsterManager", menuName = "Data/Manager/MonsterManager")]
public class MonsterDataManager : ScriptableObject
{
    [SerializeField] private List<Monsters> defaultDatas;//하나의 방에 어떤 몬스터가 있는지
    [SerializeField] private List<Monsters> EliteDatas;
    [SerializeField] private List<string> BossDatas;

    public StatSystem targetSystem;
    [SerializeField] private GameObject spawnObj;
    private GameObject spawnPivot;

    [NonSerialized] public List<MonsterObject> activeMonster = new();
    [SerializeField] private List<MonsterObject> destoryMonster = new();
    public bool isTurn;

    private ObjectDatas _objectDatas;

    private WaitForSeconds wait = new WaitForSeconds(1.0f);

    public void Init(Transform parent, StatSystem statSystem)
    {
        _objectDatas = ObjectDatas.I;
        spawnPivot = Instantiate(spawnObj, parent);
        targetSystem = statSystem;
    }

    public IEnumerator MonstersAttack()
    {
        isTurn = false;
        foreach (var monster in activeMonster)
        {
            if (monster.HasMonster()) monster.Attack();
            yield return wait;
        }
        isTurn = true;
        foreach (var monster in activeMonster)
        {
            if (monster.HasMonster()) monster.TurnEnd();
            yield return wait;
            if (monster.HasMonster()) monster.UpdateAttackIcon();
        }
        
    }

    public bool CheckMonster()
    {
        bool isMonster = false;
        foreach (var monster in activeMonster)
        {
            if (monster.HasMonster())
            {
                monster.GetComponent<MonsterBase>().UpdateText();
                isMonster = true;
            }
        }
        return isMonster;
    }

    public void DestoryMonsters()
    {
        UpdateMonsters();
        foreach (var monster in destoryMonster)
        {
            Destroy(monster.gameObject);
        }
        destoryMonster.Clear();
    }

    public void UpdateMonsters()
    {
        for(int i = 0; i < activeMonster.Count; i++)
        {
            if (!activeMonster[i].HasMonster())
            {
                destoryMonster.Add(activeMonster[i]);
                activeMonster.RemoveAt(i--);
            }
        }
    }

    public List<StatSystem> GetStats()
    {
        CheckMonster();

        List<StatSystem > stats = new List<StatSystem>();
        foreach(var obj in activeMonster)
        {
            stats.Add(obj.GetStat());
        }
        return stats;
    }

    public List<StatSystem> GetAllMonster()
    {
        UpdateMonsters();
       List<StatSystem> stats = new(activeMonster.Count);
        foreach(var obj in activeMonster)
        {
            stats.Add(obj.GetStat());
        }
        return stats;
    }

    public StatSystem GetRandomMonster()
    {
        UpdateMonsters();
        int r = UnityEngine.Random.Range(0, activeMonster.Count);
        return activeMonster[r].GetStat();
    }

    public void CreateDefalutMonster()
    {
        int r = UnityEngine.Random.Range(0, defaultDatas.Count);
        List<string> monsters = defaultDatas[r].MonsterDatas;
        destoryMonster.Clear();
        for (int i = 0; i < monsters.Count; i++)
        {
            var data = _objectDatas.GetData(monsters[i]);
            var monster = Instantiate(data.prefab, spawnPivot.transform).GetComponent<MonsterObject>();
            monster.UpdateMonster(data, targetSystem);
            activeMonster.Add(monster);
        }
    }

    public void CreateEliteMonster()
    {
        int r = UnityEngine.Random.Range(0, EliteDatas.Count);
        List<string> monsters = EliteDatas[r].MonsterDatas;
        destoryMonster.Clear();
        for (int i = 0; i < monsters.Count; i++)
        {
            var data = _objectDatas.GetData(monsters[i]);
            var monster = Instantiate(data.prefab, spawnPivot.transform).GetComponent<MonsterObject>();
            monster.UpdateMonster(data, targetSystem);
            activeMonster.Add(monster);
        }
    }

    public void CreateBossMonster(int number)
    {
        destoryMonster.Clear();
        var data = _objectDatas.GetData(BossDatas[number]);
        var monster = Instantiate(data.prefab, spawnPivot.transform).GetComponent<MonsterObject>();
        monster.UpdateMonster(data, targetSystem);
        activeMonster.Add(monster);
    }

    [System.Serializable]
    private class Monsters
    {
        public List<string> MonsterDatas;
    }
}