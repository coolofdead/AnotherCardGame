using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameEventManager : MonoBehaviour
{
    public ChoiceManager choiceManager;

    private static Dictionary<EffectTiming, List<CreatureUI>> effectsToActivateByActivationTimeType = new Dictionary<EffectTiming, List<CreatureUI>>();

    public static void TriggerEvent(GameEvent gameEvent)
    {
        List<CreatureUI> creaturesEffectsToTrigger = effectsToActivateByActivationTimeType.GetValueOrDefault(gameEvent.GetEventTiming());
        if (creaturesEffectsToTrigger == null)
            return;

        foreach (CreatureUI creatureUI in creaturesEffectsToTrigger)
        {
            foreach (AbstractCreatureEffect creatureEffect in creatureUI.creatureSO.CreatureEffects)
            {
                gameEvent.creatureUI = creatureUI; // Assign creatureUI to the base game event to know which creature is receiving the event

                creatureEffect.ReceiveEvent(gameEvent);

                if (creatureEffect is IEffectChoicable)
                {
                    
                }
            }
        }
    }

    public static void RegisterEffect(CreatureUI creatureUI)
    {
        foreach (AbstractCreatureEffect creatureEffect in creatureUI.creatureSO.CreatureEffects)
        {
            if (!effectsToActivateByActivationTimeType.ContainsKey(creatureEffect.GetActivationTimings()))
            {
                effectsToActivateByActivationTimeType.Add(creatureEffect.GetActivationTimings(), new List<CreatureUI>());
            }

            effectsToActivateByActivationTimeType.GetValueOrDefault(creatureEffect.GetActivationTimings()).Add(creatureUI);
        }
    }

    public static void UnregisterEffect(CreatureUI creatureUI)
    {
        foreach (AbstractCreatureEffect creatureEffect in creatureUI.creatureSO.CreatureEffects)
        {
            if (effectsToActivateByActivationTimeType.ContainsKey(creatureEffect.GetActivationTimings()))
            {
                effectsToActivateByActivationTimeType[creatureEffect.GetActivationTimings()].Remove(creatureUI);
            }
        }
    }
}