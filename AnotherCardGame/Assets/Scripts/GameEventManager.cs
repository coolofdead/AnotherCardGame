using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventManager : AbstractManager<GameEventManager>
{
    private static Dictionary<EffectTiming, List<AbstractCreatureEffect>> effectsToActivateByActivationTimeType = new Dictionary<EffectTiming, List<AbstractCreatureEffect>>();

    public static IEnumerator TriggerEvent(GameEvent gameEvent)
    {
        List<AbstractCreatureEffect> creaturesEffectsToTrigger = effectsToActivateByActivationTimeType.GetValueOrDefault(gameEvent.GetEventTiming());
        if (creaturesEffectsToTrigger == null)
            yield break;

        foreach (AbstractCreatureEffect creatureEffect in creaturesEffectsToTrigger)
        {
            yield return creatureEffect.ReceiveEvent(gameEvent);
        }
    }

    public static void RegisterEffect(CreatureUI creatureUI)
    {
        foreach (AbstractCreatureEffect creatureEffect in creatureUI.CreatureEffects)
        {
            if (!effectsToActivateByActivationTimeType.ContainsKey(creatureEffect.GetActivationTimings()))
            {
                effectsToActivateByActivationTimeType.Add(creatureEffect.GetActivationTimings(), new List<AbstractCreatureEffect>());
            }

            effectsToActivateByActivationTimeType.GetValueOrDefault(creatureEffect.GetActivationTimings()).Add(creatureEffect);
        }
    }

    public static void UnregisterEffect(CreatureUI creatureUI)
    {
        foreach (AbstractCreatureEffect creatureEffect in creatureUI.CreatureEffects)
        {
            if (effectsToActivateByActivationTimeType.ContainsKey(creatureEffect.GetActivationTimings()))
            {
                effectsToActivateByActivationTimeType[creatureEffect.GetActivationTimings()].Remove(creatureEffect);
            }
        }
    }
}