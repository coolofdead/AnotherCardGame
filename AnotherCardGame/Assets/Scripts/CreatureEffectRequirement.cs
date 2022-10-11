using System;

[Serializable]
public class CreatureEffectRequirement
{
    public CreatureEffectTargetType creatureEffectTargetType = CreatureEffectTargetType.Self;
    public CreatureEffectRequirementType creatureEffectRequirementType;

    public CreatureStats stats;

    public bool IsRequirementMet(CreatureFightingUI selfCreature, CreatureFightingUI opponentCreature)
    {
        bool selfCompare = true;
        bool opponentCompare = true;
        bool selfToOpponent = true;

        if (creatureEffectTargetType == CreatureEffectTargetType.Self || creatureEffectTargetType == CreatureEffectTargetType.Both)
        {
            selfCompare = selfCreature.stats.Compare(stats, creatureEffectRequirementType);
        }

        if (creatureEffectTargetType == CreatureEffectTargetType.Opponent || creatureEffectTargetType == CreatureEffectTargetType.Both)
        {
            opponentCompare = opponentCreature.stats.Compare(stats, creatureEffectRequirementType);
        }

        if (creatureEffectTargetType == CreatureEffectTargetType.SelfToOpponent)
        {
            selfToOpponent = selfCreature.stats.Compare(opponentCreature.stats, creatureEffectRequirementType);
        }

        return selfCompare && opponentCompare && selfToOpponent;
    }
}
