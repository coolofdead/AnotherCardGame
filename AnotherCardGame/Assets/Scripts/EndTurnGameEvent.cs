using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurnGameEvent : GameEvent
{
    public override EffectTiming GetEventTiming()
    {
        return EffectTiming.OnEndOfTurn;
    }
}
