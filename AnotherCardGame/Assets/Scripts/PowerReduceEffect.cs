using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerReduceEffect : AbstractCreatureEffect
{
    public override IEnumerator Activate(GameEvent gameEvent)
    {
        SummonGameEvent summonGameEvent = gameEvent as SummonGameEvent;

        if (summonGameEvent.faceOffCreature != null)
            summonGameEvent.faceOffCreature.persistentBonusStats.power -= creatureOwner.BaseStats.manaCost * 1000;

        yield return null;
    }

    public override EffectTiming GetActivationTimings()
    {
        return EffectTiming.OnSummon;
    }

    public override string GetEffectDescription()
    {
        return "Reduce definetly the opp power by 1000 * this creature's mana cost";
    }

    public override Effect GetEffectType()
    {
        return Effect.PowerReduce;
    }

    public override bool RequirementsMet(GameEvent gameEvent)
    {
        SummonGameEvent summonGameEvent = gameEvent as SummonGameEvent;

        return creatureOwner == summonGameEvent.summonedCreature;
    }
}
