using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class BattleManager : MonoBehaviour
{
    bool _isPlayerTrun = true;
    bool _isEndBattle = false;   //게임 종료 
    public Text trunBtn;
    public StatSystem _player;
    public HandManager _handManager;
    public MonsterDataManager _monsterDataManager;

    public void Init(MapData mapData)
    {
        //5번이 일반 3번이 엘리트 8번이 보스
        switch(mapData.mapData)
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
        MyTrun();
    }

    private void Update()
    {
        if (_isPlayerTrun)
        {

        }
    }

    public void EndTrun()
    {
        _isPlayerTrun = false;
        _player.UpdateBuffs();//버프들 턴 증가 및 정리
        trunBtn.text = "적 턴";
        StartCoroutine(_monsterDataManager.MonstersAttack());
        InfoSystem.instance.ShowDate();
    }

    public void MyTrun()
    {
        _isPlayerTrun = true;
        _player.RegenCost();//코스트 회복
        trunBtn.text = "턴 종료";
        InfoSystem.instance.ShowDate();
    }
}
