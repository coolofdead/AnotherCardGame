using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldEffect : AbstractCreatureEffect
{
    public override IEnumerator Activate(GameEvent gameEvent)
    {
        creatureOwner.persistentBonusStats.shield += creatureOwner.BaseStats.manaCost * 1000;

        yield return null;
    }

    public override EffectTiming GetActivationTimings()
    {
        return EffectTiming.OnSummon;
    }

    public override Effect GetEffectType()
    {
        return Effect.Shield;
    }

    public override bool RequirementsMet(GameEvent gameEvent)
    {
        SummonGameEvent summonGameEvent = gameEvent as SummonGameEvent;

        return creatureOwner == summonGameEvent.summonedCreature;
    }

    public override string ToString()
    {
        return Effect.Shield.ToString();
    }
}