using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderEffect : AbstractCreatureEffect
{
    public override IEnumerator Activate(GameEvent gameEvent)
    {
        creatureOwner.canAttackDirectly = false;

        yield return null;
    }

    public override EffectTiming GetActivationTimings()
    {
        return EffectTiming.OnSummon;
    }

    public override string GetEffectDescription()
    {
        return "Cannot attack directly";
    }

    public override Effect GetEffectType()
    {
        return Effect.Defender;
    }

    public override bool RequirementsMet(GameEvent gameEvent)
    {
        SummonGameEvent summonGameEvent = gameEvent as SummonGameEvent;

        return creatureOwner == summonGameEvent.summonedCreature;
    }
}