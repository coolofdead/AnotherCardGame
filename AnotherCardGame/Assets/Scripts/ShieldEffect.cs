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

    public override string GetEffectDescription()
    {
        return "On summon get 1000 shield * mana cost";
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
}