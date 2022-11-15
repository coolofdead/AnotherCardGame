using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnEffect : AbstractCreatureEffect
{
    public override IEnumerator Activate(GameEvent gameEvent)
    {
        DroppableAreaUI emptyHandArea = HandAreaManager.Instance.GetFirstEmptyHandArea();
        
        emptyHandArea.MoveCard(creatureOwner.dragableUI);
        creatureOwner.CanBeSummon(true);

        yield return null;
    }

    public override EffectTiming GetActivationTimings()
    {
        return EffectTiming.OnEndOfTurn;
    }

    public override Effect GetEffectType()
    {
        return Effect.Return;
    }

    public override bool RequirementsMet(GameEvent gameEvent)
    {
        return true;
    }

    public override string ToString()
    {
        return Effect.Return.ToString();
    }
}