using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MyBox;

[Serializable]
public class CreatureEffect
{
    public CreatureEffectTargetType target = CreatureEffectTargetType.None;
    public EffectActivationTimeType activationTime = EffectActivationTimeType.Default;

    public bool hasRequirement;
    public bool hasModulation;
    public bool hasDamage;

    [ConditionalField("hasRequirement", false)] public CreatureEffectRequirement requirements;
    [ConditionalField("hasModulation", false)] public CreatureEffectModulate modulation;
    [ConditionalField("hasDamage", false)] public CreatureEffectDealDamage damage;

    public void Activate(CreatureFightingUI selfCreature, CreatureFightingUI opponentCreature)
    {
        if (hasModulation)
        {
            modulation.Modulate(selfCreature, opponentCreature, target);
        }

        if (hasDamage)
        {
            if (target == CreatureEffectTargetType.Self || target == CreatureEffectTargetType.Both)
                damage.DealDamage(selfCreature);

            if (target == CreatureEffectTargetType.Opponent || target == CreatureEffectTargetType.Both)
                damage.DealDamage(opponentCreature);
        }
    }

    public void RegisterActivation()
    {
        GameEventManager.RegisterEffect(this, activationTime);
    }

    public void OnGameEvent(EventStruct gameEventData)
    {
        if (hasRequirement == false || requirements.IsRequirementsMet(gameEventData.playerCreature, gameEventData.opponentCreature))
        {
            Activate(gameEventData.playerCreature, gameEventData.opponentCreature);
        }
    }
}
