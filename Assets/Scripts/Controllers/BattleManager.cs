using JetBrains.Annotations;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEditor.PlayerSettings;

public class BattleManager : MonoBehaviour
{
    bool _isPlayerTrun = true;
    bool _isEndBattle = false;   //게임 종료

    public Text trunBtn;
    public StatSystem _player;
    public CardManager _cardManager;
    public MonsterDataManager _monsterDataManager;
    public RewardController _rewardController;

    [SerializeField] public GameObject canvas;
    [SerializeField] private GameObject rewardCanvas;

    private MapData _mapData;

    private bool isBattle;

    private void Awake()
    {
        ObjectDatas.I.Init();
    }
    void Start()
    {
        _player.SettingStat(ObjectDatas.I.GetData("Ironclad").stat);
        //if (_isElite)
        //{
        //    _monsterDataManager.CreateEliteMonster();
        //}
        //else if (_isBoss)
        //{
        //    _monsterDataManager.CreateBossMonster(0);
        //}
        //else
        //{
        //    _monsterDataManager.CreateDefalutMonster();
        //}
        _cardManager = CardManager.instance;
        _monsterDataManager.Init(canvas.transform, _player);
        canvas.SetActive(false);

        isBattle = false;
    }

    public void Init(MapData mapData)
    {
        switch (mapData.mapData)
        {
            case 5://일반
                {
                    _monsterDataManager.CreateDefalutMonster();
                }
                break;
            case 3://엘리트
                {
                    _monsterDataManager.CreateEliteMonster();
                }
                break;
            case 8://보스
                {
                    _monsterDataManager.CreateBossMonster(mapData.floor);
                }
                break;
        }

        _mapData = mapData;

        _cardManager.CopyFromOriginal();

        isBattle = true;

        MyTrun();
    }

    void Update()
    {
        if (isBattle && !_monsterDataManager.CheckMonster())
        {
            CardManager.instance.Clear();
            rewardCanvas.SetActive(true);
            _rewardController.Init(_mapData.EventNum);
            _player.UpdateBuffs();
            isBattle = false;
            _isPlayerTrun = true;
            return;
        }

        if (!_isPlayerTrun && _monsterDataManager.isTurn)  // 적 턴이 아니면 플레이어의 턴
        {
            MyTrun();
            _monsterDataManager.isTurn = false;
        }
    }
    public void EndTrun()  // 엔드턴을 어디에서 호출해야 할 지 모르겠습니다. -> 버튼에서 호출
    {
        _player.UpdateBuffs();
        _isPlayerTrun = false;
        _monsterDataManager.UpdateMonsters();
        StartCoroutine(_monsterDataManager.MonstersAttack());  // 플레이어의 턴이 끝나면 적이 공격
        trunBtn.text = "적 턴";

        InfoSystem.instance.ShowDate();
    }

    public void MyTrun()
    {
        _isPlayerTrun = true;
        //maxCost = 3;
        //curCost = 3;
        //maxText.text = maxCost.ToString();
        //curText.text = curCost.ToString();
        //_player. 코스트 회복을 어떻게 할지 모르겠습니다.
        _cardManager.DrawCard(5); //드로우 불러오는 방법을 모르겠습니다. -> 카드 매니저에서 불러오기

        trunBtn.text = "턴 종료";
        _player.RegenCost();
        _player.UpdateStats();
        InfoSystem.instance.ShowDate();
        _monsterDataManager.CheckMonster();
    }
}
