using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private MonsterDataManager _dataManager;

    private void Awake()
    {
        ObjectDatas.I.Init();
        GetComponent<StatSystem>().SettingStat(ObjectDatas.I.GetData("Ironclad").stat);
        _dataManager.targetSystem = GetComponent<StatSystem>();
    }

    void Start()
    {
        _dataManager.Init(transform.parent);
        _dataManager.CreateDefalutMonster();
    }

    public void TEST_Attack()
    {
        StartCoroutine(_dataManager.MonstersAttack());
    }
}
