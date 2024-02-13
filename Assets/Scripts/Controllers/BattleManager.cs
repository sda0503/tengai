using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    bool _isPlayerTrun = true;
    public int playerPoint = 3;
    public int playerHand = 5;

    void Start()
    {
        MyTrun();
    }

    public void EndTrun()
    {
        _isPlayerTrun = false;
    }

    public void MyTrun()
    {
        _isPlayerTrun = true;
        playerPoint = 3;
        playerHand = 5;
    }
}
