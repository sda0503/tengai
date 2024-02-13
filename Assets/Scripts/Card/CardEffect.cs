using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICardEffect
{
    public void OnUse(StatSystem statSystem = null);
}
