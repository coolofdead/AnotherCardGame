using MyBox;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    [Header("UI Params")]
    public Vector2 artworkPositionOffset = Vector2.zero;
    public Vector2 artworkScale = Vector2.one;

    public Vector2 battleArtworkAnchoredPosition = Vector2.zero; // Apply to anchored position
    public Vector2 battleArtworkScale = Vector2.zero;
    [Tooltip("if wrong orientation on battle")] public bool flipArtworkOnBattle;

    [Header("Creature Effect")]
    public Effect effects = Effect.None;
    public AbstractCreatureEffect[] CreatureEffects => creatureEffectsByEffect.Where((keyValuePair) => effects.HasFlag(keyValuePair.Key))
                                                                              .ToDictionary(x => x.Key, x => x.Value)
                                                                              .Values.ToArray();

    public static Dictionary<Effect, AbstractCreatureEffect> creatureEffectsByEffect = new Dictionary<Effect, AbstractCreatureEffect>()
    {
        { Effect.Redraw, new RedrawEffect() },
        { Effect.First_Attacker, new FirstAttackerEffect() },
        //{ Effect.Blocker, new BlockerEffect() },
        //{ Effect.Celerity, new CelerityEffect() },
        //{ Effect.Defender, new DefenderEffect() },
        //{ Effect.PowerModifier, new PowerPlusEffect() },
        //{ Effect.Shield, new ShieldEffect() },
    };

[Header("Creature Attack Animation")]
    public CreatureAttackAnimationHandler attackAnimationPrefab;
}
