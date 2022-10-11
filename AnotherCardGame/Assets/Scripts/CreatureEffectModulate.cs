using MyBox;
using System;
using UnityEngine;

[Serializable]
public class CreatureEffectModulate
{
    public bool shouldModulatePower;
    [ConditionalField("shouldModulatePower", false)] public ModulationType powerModulationType;
    [ConditionalField("shouldModulatePower", false)] public int powerAmount;

    public bool shouldModulateNbHit;
    [ConditionalField("shouldModulateNbHit", false)] public ModulationType nbHitModulationType;
    [ConditionalField("shouldModulateNbHit", false)] public int nbHitAmount;

    public bool shouldModulateSpeed;
    [ConditionalField("shouldModulateSpeed", false)] public ModulationType speedModulationType;
    [ConditionalField("shouldModulateSpeed", false)] public int speedAmount;

    public bool shouldModulateShield;
    [ConditionalField("shouldModulateShield", false)] public ModulationType shieldModulationType;
    [ConditionalField("shouldModulateShield", false)] public int shieldAmount;

    public void Modulate(CreatureFightingUI playerCreature, CreatureFightingUI opponentCreature, CreatureEffectTargetType target)
    {
        EffectActivationTimeType statsModifiedEventType = target == CreatureEffectTargetType.Self || target == CreatureEffectTargetType.Both ?
                                                                    EffectActivationTimeType.OnSelfStatModified : EffectActivationTimeType.OnOpponentStatModified;
        GameEventManager.TriggerEvent(statsModifiedEventType, new EventStruct());

        if (shouldModulatePower)
        {
            if (target == CreatureEffectTargetType.Self || target == CreatureEffectTargetType.Both)
                ModulateStats(playerCreature, new CreatureStats { power = powerAmount });

            if (target == CreatureEffectTargetType.Opponent || target == CreatureEffectTargetType.Both)
                ModulateStats(opponentCreature, new CreatureStats { power = powerAmount });
        }

        if (shouldModulateNbHit)
        {
            if (target == CreatureEffectTargetType.Self || target == CreatureEffectTargetType.Both)
                ModulateStats(playerCreature, new CreatureStats { nbHit = nbHitAmount });

            if (target == CreatureEffectTargetType.Opponent || target == CreatureEffectTargetType.Both)
                ModulateStats(opponentCreature, new CreatureStats { nbHit = nbHitAmount });
        }

        if (shouldModulateSpeed)
        {
            if (target == CreatureEffectTargetType.Self || target == CreatureEffectTargetType.Both)
                ModulateStats(playerCreature, new CreatureStats { speed = speedAmount });

            if (target == CreatureEffectTargetType.Opponent || target == CreatureEffectTargetType.Both)
                ModulateStats(opponentCreature, new CreatureStats { speed = speedAmount });
        }

        if (shouldModulateShield)
        {
            if (target == CreatureEffectTargetType.Self || target == CreatureEffectTargetType.Both)
                ModulateStats(playerCreature, new CreatureStats { shield = shieldAmount });

            if (target == CreatureEffectTargetType.Opponent || target == CreatureEffectTargetType.Both)
                ModulateStats(opponentCreature, new CreatureStats { shield = shieldAmount });
        }
    }

    private void ModulateStats(CreatureFightingUI targetCreature, CreatureStats statsToModulate)
    {
        switch (powerModulationType)
        {
            case ModulationType.Add:
                targetCreature.stats += statsToModulate;
                break;
            case ModulationType.Substract:
                targetCreature.stats-= statsToModulate;
                break;
            case ModulationType.Multiply:
                targetCreature.stats *= statsToModulate;
                break;
            case ModulationType.Divide:
                targetCreature.stats /= statsToModulate;
                break;
            default:
                break;
        }
    }
}
