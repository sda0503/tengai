using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card_Skill_", menuName = "Card/SkillCard")]
public class Card_Skill : Card_Base
{
    public override Card CreateCard()
    {
        return new SkillCard(this);
    }
}
