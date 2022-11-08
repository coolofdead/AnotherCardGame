using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class CreatureEffectRequirementSubCreatureType
{
    public CreatureEffectTargetType creatureEffectTargetType = CreatureEffectTargetType.Self;

    public SubCreatureType creatureFightingSubTypeRequired;

    public bool IsRequirementMet(CreatureUI selfCreature, CreatureUI opponentCreature)
    {
        bool selfCompare = true;
        bool opponentCompare = true;

        if (creatureEffectTargetType == CreatureEffectTargetType.Self || creatureEffectTargetType == CreatureEffectTargetType.Both)
        {
            selfCompare = selfCreature.creatureSO.subCreatureType == creatureFightingSubTypeRequired;
        }

        if (creatureEffectTargetType == CreatureEffectTargetType.Opponent || creatureEffectTargetType == CreatureEffectTargetType.Both)
        {
            opponentCompare = opponentCreature.creatureSO.subCreatureType == creatureFightingSubTypeRequired;
        }

        return selfCompare && opponentCompare;
    }
}
