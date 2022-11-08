using System;
using MyBox;

[Serializable]
public class CreatureEffectRequirement
{
    public bool requireStats;
    public bool requireCreatureType;
    public bool requireSubCreatureType;

    [ConditionalField("requireStats", false)] public CreatureEffectRequirementStats requirementStats;
    [ConditionalField("requireCreatureType", false)] public CreatureEffectRequirementCreatureType requirementCreatureType;
    [ConditionalField("requireSubCreatureType", false)] public CreatureEffectRequirementSubCreatureType requirementSubCreatureType;

    public bool IsRequirementsMet(CreatureUI selfCreature, CreatureUI opponentCreature)
    {
        bool requirementStatsMet = requireStats;
        bool requirementCreatureTypeMet = requireCreatureType;
        bool requirementSubCreatureTypeMet = requireSubCreatureType;

        if (requireStats)
            requirementStats.IsRequirementMet(selfCreature, opponentCreature);

        if (requireCreatureType)
            requirementCreatureType.IsRequirementMet(selfCreature, opponentCreature);

        if (requireSubCreatureType)
            requirementSubCreatureType.IsRequirementMet(selfCreature, opponentCreature);

        return requirementStatsMet && requirementCreatureTypeMet && requirementSubCreatureTypeMet;
    }
}
