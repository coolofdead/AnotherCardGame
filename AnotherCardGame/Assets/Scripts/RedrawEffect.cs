using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RedrawEffect : AbstractCreatureEffect
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
        throw new NotImplementedException();
    }

    public override bool RequirementsMet(GameEvent gameEvent)
    {
        throw new NotImplementedException();
    }
}
