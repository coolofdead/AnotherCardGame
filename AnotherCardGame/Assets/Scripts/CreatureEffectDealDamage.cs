using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class CreatureEffectDealDamage
{
    [RangeEx(0, 10000, 500)] public int damageAmount;

    public void DealDamage(CreatureUI target)
    {
    }
}
