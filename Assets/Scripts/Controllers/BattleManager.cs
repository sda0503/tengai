using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    bool _isPlayerTrun = true;
    bool _isEndBattle = false;   //게임 종료 
    public Text trunBtn;
    public Player _player;
    public HandManager _handManager;
    public MonsterDataManager _monsterDataManager;
    public bool _isPlayerTrun = true;
    public Text trunBtn;
    
    public bool _isEndBattle = false;   //게임 종료 

    void Start()
    {
        MyTrun();
    }

    public void EndTrun()
    {
        _isPlayerTrun = false;
        trunBtn.text = "적 턴";
    }   

    public void MyTrun()
    {
        _isPlayerTrun = true;
        trunBtn.text = "턴 종료";
        InfoSystem.instance.ShowDate();
    }

}
