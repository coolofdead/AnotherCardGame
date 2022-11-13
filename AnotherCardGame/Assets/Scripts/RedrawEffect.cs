using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RedrawEffect : AbstractCreatureEffect, IEffectChoicable
{
    public override EffectTiming GetActivationTimings()
    {
        return EffectTiming.OnSummon;
    }

    public override string ToString()
    {
        return Effect.Redraw.ToString();
    }

    public override void Activate(GameEvent gameEvent)
    {
        SummonGameEvent summonGameEvent = gameEvent as SummonGameEvent;

        Debug.Log("should redraw " + summonGameEvent.totalPlayerSummon);
    }

    public override bool RequirementsMet(GameEvent gameEvent)
    {
        SummonGameEvent summonGameEvent = gameEvent as SummonGameEvent;

        // Effect activate only on creature summon
        return summonGameEvent.creatureUI == summonGameEvent.summonedCreature;
    }

    public ChoiceDetails GetChoicesDetails()
    {
        return new ChoiceDetails()
        {
            choiceType = ChoiceType.DroppableArea,
            nbChoices = 3,
        };
    }

    public void ChoicesSelectionCallback()
    {
        
    }
}
