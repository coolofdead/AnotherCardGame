using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonGameEvent : GameEvent
{
    public override EffectTiming GetEventTiming()
    {
        return EffectTiming.OnSummon;
    }
}
