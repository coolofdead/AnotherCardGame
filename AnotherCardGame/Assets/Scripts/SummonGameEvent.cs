using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonGameEvent : GameEvent
{
    public CreatureUI summonedCreature;
    public CreatureUI faceOffCreature;
    public int nthSummon;
    public int nthPlayerSummon;
    public int totalPlayerSummon;
    public bool isPlayerCreature;

    public override EffectTiming GetEventTiming()
    {
        return EffectTiming.OnSummon;
    }
}
