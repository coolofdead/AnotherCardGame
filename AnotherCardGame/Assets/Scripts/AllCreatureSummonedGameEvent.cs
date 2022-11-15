using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllCreatureSummonedGameEvent : GameEvent
{
    public int totalPlayerSummon;

    public override EffectTiming GetEventTiming()
    {
        return EffectTiming.OnAllCreatureSummoned;
    }
}
