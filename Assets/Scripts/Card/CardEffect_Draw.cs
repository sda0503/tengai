using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CardEffect_Draw : ICardEffect
{
    public int drawCardNum;

    public void OnUse(StatSystem statSystem = null)
    {
        CardManager.instance.DrawCard(drawCardNum);
    }
}
