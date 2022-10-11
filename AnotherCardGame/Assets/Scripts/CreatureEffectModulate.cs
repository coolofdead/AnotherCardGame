using MyBox;
using System;

[Serializable]
public class CreatureEffectModulate
{
    public static Action onStatModified;

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
    [ConditionalField("shouldModulateSpeed", false)] public ModulationType shieldModulationType;
    [ConditionalField("shouldModulateSpeed", false)] public int shieldAmount;

    public void Modulate(CreatureFightingUI playerCreature, CreatureFightingUI opponentCreature, CreatureEffectTargetType target)
    {
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
            default:
                break;
        }
    }
}
