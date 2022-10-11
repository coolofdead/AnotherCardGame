using MyBox;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Creature", menuName = "Creature/New Creature", order = 1)]
public class CreatureSO : ScriptableObject
{
    [Header("Creature Type")]
    public CreatureType creatureType;
    // TODO : Refacto below
    public SubCreatureType subCreatureType => creatureType == CreatureType.Fire ? (SubCreatureType)subCreatureTypeFire : 
                                              creatureType == CreatureType.Plant ? (SubCreatureType)subCreatureTypePlant :
                                              creatureType == CreatureType.Water ? (SubCreatureType)subCreatureTypeWater : 
                                              default(SubCreatureType);
    [ConditionalField("creatureType", false, CreatureType.Fire)] public SubCreatureTypeFire subCreatureTypeFire;
    [ConditionalField("creatureType", false, CreatureType.Plant)] public SubCreatureTypePlant subCreatureTypePlant;
    [ConditionalField("creatureType", false, CreatureType.Water)] public SubCreatureTypeWater subCreatureTypeWater;

    [Header("Creature Stats")]
    public CreatureStats stats;

    [Header("Creature Infos")]
    public string creatureName;
    public string effectDescription;
    public Sprite artwork;
    public Vector2 artworkPositionOffset = Vector2.zero;
    public Vector2 artworkScale = Vector2.one;

    [Header("Creature Effect")]
    public bool hasAnEffect;
    public bool hasGlobalRequirement;

    [ConditionalField("hasAnEffect")] public CollectionWrapper<CreatureEffect> effect;
    [ConditionalField("hasGlobalRequirement", false)] public CreatureEffectRequirement requirement;

    public bool HasAnEffectAndRequirementsAreMet(CreatureFightingUI selfCreature, CreatureFightingUI opponentCreature)
    {
        return hasAnEffect && (hasGlobalRequirement == false || requirement.IsRequirementMet(selfCreature, opponentCreature));
    }

    public void ActivateEffect(CreatureFightingUI selfCreature, CreatureFightingUI opponentCreature)
    {
        foreach (CreatureEffect creatureEffect in effect.Value)
        {
            if (creatureEffect.hasRequirement == false || creatureEffect.requirement.IsRequirementMet(selfCreature, opponentCreature))
            {
                creatureEffect.Activate(selfCreature, opponentCreature);
            }
        }
    }
}
