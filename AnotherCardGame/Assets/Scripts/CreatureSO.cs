using MyBox;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Creature", menuName = "Creature/New Creature", order = 1)]
public class CreatureSO : ScriptableObject
{
    [Header("Creature Type")]
    public CreatureType creatureType;
    public SubCreatureType subCreatureType => SubCreatureTypeExtension.GetSubCreatureType(this);

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
    public bool hasEffects;
    private bool hasGlobalRequirement;

    [ConditionalField("hasEffects")] public CollectionWrapper<CreatureEffect> effect;
    [ConditionalField("hasGlobalRequirement", false)] private CreatureEffectRequirement requirement;

    public bool HasAnEffectAndRequirementsAreMet(CreatureFightingUI selfCreature, CreatureFightingUI opponentCreature)
    {
        return hasEffects && (hasGlobalRequirement == false || requirement.IsRequirementsMet(selfCreature, opponentCreature));
    }

    public void ActivateEffect(CreatureFightingUI selfCreature, CreatureFightingUI opponentCreature)
    {
        foreach (CreatureEffect creatureEffect in effect.Value)
        {
            if (creatureEffect.activationTime != EffectActivationTimeType.Default)
            {
                creatureEffect.RegisterActivation();
                continue;
            }

            if (creatureEffect.hasRequirement == false || creatureEffect.requirements.IsRequirementsMet(selfCreature, opponentCreature))
            {
                creatureEffect.Activate(selfCreature, opponentCreature);
            }
        }
    }
}
