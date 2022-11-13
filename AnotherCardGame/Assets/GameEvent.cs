using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameEvent
{
    public CreatureUI creatureUI;

    public abstract EffectTiming GetEventTiming();
}
