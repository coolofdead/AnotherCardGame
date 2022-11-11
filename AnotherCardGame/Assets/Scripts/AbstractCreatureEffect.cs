using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractCreatureEffect
{
    public Sprite IconSprite => Resources.Load<Sprite>(ToString());

    public void ReceiveEvent(GameEvent gameEvent)
    {
        if (RequirementsMet(gameEvent))
        {
            Activate(gameEvent);
        }
    }

    public abstract void Activate(GameEvent gameEvent);
    public abstract bool RequirementsMet(GameEvent gameEvent);

    public abstract EffectTiming GetActivationTimings();

    public new abstract string ToString();
}
