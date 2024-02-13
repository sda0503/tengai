using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private MonsterDataManager _dataManager;
    [SerializeField] private GameObject pivot;
    [SerializeField] private GameObject monsterPivot;

    private void Awake()
    {
        _dataManager.targetSystem = GetComponent<StatSystem>();
        _dataManager.spowPivot = monsterPivot;
    }

    // Start is called before the first frame update
    void Start()
    {
        _dataManager.CreateDefalutMonster();
    }

}