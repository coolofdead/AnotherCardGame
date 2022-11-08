using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using MyBox;

[Serializable]
public class CreatureEffects
{
    //public bool hasEffects;
    //private bool hasGlobalRequirement;

    //[ConditionalField("hasEffects")] public CollectionWrapper<CreatureEffect> effect;
    //[ConditionalField("hasGlobalRequirement", false)] private CreatureEffectRequirement requirement;

    //public bool HasAnEffectAndRequirementsAreMet(CreatureUI selfCreature, CreatureUI opponentCreature)
    //{
    //    return hasEffects && (hasGlobalRequirement == false || requirement.IsRequirementsMet(selfCreature, opponentCreature));
    //}

    //public void ActivateEffect(CreatureUI selfCreature, CreatureUI opponentCreature)
    //{
    //    foreach (CreatureEffect creatureEffect in effect.Value)
    //    {
    //        if (creatureEffect.activationTime != EffectActivationTimeType.Default)
    //        {
    //            creatureEffect.RegisterActivation();
    //            continue;
    //        }

    //        if (creatureEffect.hasRequirement == false || creatureEffect.requirements.IsRequirementsMet(selfCreature, opponentCreature))
    //        {
    //            creatureEffect.Activate(selfCreature, opponentCreature);
    //        }
    //    }
    //}

    //public CreatureEffectTargetType target = CreatureEffectTargetType.None;
    //public EffectActivationTimeType activationTime = EffectActivationTimeType.Default;

    //public bool hasRequirement;
    //public bool hasModulation;
    //public bool hasDamage;

    //[ConditionalField("hasRequirement", false)] public CreatureEffectRequirement requirements;
    //[ConditionalField("hasModulation", false)] public CreatureEffectModulate modulation;
    //[ConditionalField("hasDamage", false)] public CreatureEffectDealDamage damage;

    //public void Activate(CreatureUI selfCreature, CreatureUI opponentCreature)
    //{
    //    if (hasModulation)
    //    {
    //        modulation.Modulate(selfCreature, opponentCreature, target);
    //    }

    //    if (hasDamage)
    //    {
    //        if (target == CreatureEffectTargetType.Self || target == CreatureEffectTargetType.Both)
    //            damage.DealDamage(selfCreature);

    //        if (target == CreatureEffectTargetType.Opponent || target == CreatureEffectTargetType.Both)
    //            damage.DealDamage(opponentCreature);
    //    }
    //}

    //public void RegisterActivation()
    //{
    //    GameEventManager.RegisterEffect(this, activationTime);
    //}

    //public void OnGameEvent(EventStruct gameEventData)
    //{
    //    if (hasRequirement == false || requirements.IsRequirementsMet(gameEventData.playerCreature, gameEventData.opponentCreature))
    //    {
    //        Activate(gameEventData.playerCreature, gameEventData.opponentCreature);
    //    }
    //}
}
