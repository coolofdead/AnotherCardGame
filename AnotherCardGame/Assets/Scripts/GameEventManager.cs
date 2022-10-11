using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameEventManager : MonoBehaviour
{
    private static GameEventManager Instance;

    public GameManager gameManager;

    private static Dictionary<EffectActivationTimeType, List<CreatureEffect>> effectsToActivateByActivationTimeType = new Dictionary<EffectActivationTimeType, List<CreatureEffect>>();

    private void Awake()
    {
        Instance = this;
    }
    
    public static void TriggerEvent(EffectActivationTimeType effectActivationTimeType, EventStruct eventData)
    {
        if (effectActivationTimeType == EffectActivationTimeType.OnEndFight)
        {
            effectsToActivateByActivationTimeType.Clear();
            return;
        }

        eventData.playerCreature = Instance.gameManager.playerCreatureFighting;
        eventData.opponentCreature = Instance.gameManager.opponentCreatureFighting;

        List<CreatureEffect> creaturesEffectsToTrigger = effectsToActivateByActivationTimeType.GetValueOrDefault(effectActivationTimeType);
        if (creaturesEffectsToTrigger == null)
            return;

        foreach (CreatureEffect creatureEffect in creaturesEffectsToTrigger)
        {
            creatureEffect.OnGameEvent(eventData);
        }
    }

    public static void RegisterEffect(CreatureEffect creatureEffect, EffectActivationTimeType effectActivationTimeType)
    {
        if (!effectsToActivateByActivationTimeType.ContainsKey(effectActivationTimeType))
        {
            effectsToActivateByActivationTimeType.Add(effectActivationTimeType, new List<CreatureEffect>());
        }

        effectsToActivateByActivationTimeType.GetValueOrDefault(effectActivationTimeType).Add(creatureEffect);
    }
}

public struct EventStruct
{
    public CreatureFightingUI sourceCreature;
    public int damage;
    public CreatureFightingUI playerCreature;
    public CreatureFightingUI opponentCreature;
}
