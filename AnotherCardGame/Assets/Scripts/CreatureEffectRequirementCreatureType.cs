using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class CreatureEffectRequirementCreatureType
{
    public CreatureEffectTargetType creatureEffectTargetType = CreatureEffectTargetType.Self;

    public CreatureType creatureFightingTypeRequired;

    public bool IsRequirementMet(CreatureFightingUI selfCreature, CreatureFightingUI opponentCreature)
    {
        bool selfCompare = true;
        bool opponentCompare = true;

        if (creatureEffectTargetType == CreatureEffectTargetType.Self || creatureEffectTargetType == CreatureEffectTargetType.Both)
        {
            selfCompare = selfCreature.creatureSO.creatureType == creatureFightingTypeRequired;
        }

        if (creatureEffectTargetType == CreatureEffectTargetType.Opponent || creatureEffectTargetType == CreatureEffectTargetType.Both)
        {
            opponentCompare = opponentCreature.creatureSO.creatureType == creatureFightingTypeRequired;
        }

        return selfCompare && opponentCompare;
    }
}
