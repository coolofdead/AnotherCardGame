using MyBox;
using System;
using UnityEngine;

[Serializable]
public class CreatureEffectModulate
{
    public bool shouldModulatePower;
    [ConditionalField("shouldModulatePower", false)] public ModulationType powerModulationType;
    [ConditionalField("shouldModulatePower", false)] public int powerAmount;

    public bool shouldModulateShield;
    [ConditionalField("shouldModulateShield", false)] public ModulationType shieldModulationType;
    [ConditionalField("shouldModulateShield", false)] public int shieldAmount;

    public void Modulate(CreatureUI playerCreature, CreatureUI opponentCreature, CreatureEffectTargetType target)
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

        if (shouldModulateShield)
        {
            if (target == CreatureEffectTargetType.Self || target == CreatureEffectTargetType.Both)
                ModulateStats(playerCreature, new CreatureStats { shield = shieldAmount });

            if (target == CreatureEffectTargetType.Opponent || target == CreatureEffectTargetType.Both)
                ModulateStats(opponentCreature, new CreatureStats { shield = shieldAmount });
        }
    }

    private void ModulateStats(CreatureUI targetCreature, CreatureStats statsToModulate)
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
