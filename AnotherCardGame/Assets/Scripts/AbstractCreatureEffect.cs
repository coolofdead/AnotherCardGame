using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class AbstractCreatureEffect
{
    public static List<AbstractCreatureEffect> GenerateEffectsForCreature(CreatureSO creatureSO)
    {
        List<AbstractCreatureEffect> creatureEffects = new List<AbstractCreatureEffect>();
        foreach (Effect effectType in creatureSO.effects.GetFlags())
        {
            switch (effectType)
            {
                case Effect.Defender:
                    creatureEffects.Add(new DefenderEffect());
                    break;
                case Effect.FirstAttacker:
                    creatureEffects.Add(new FirstAttackerEffect());
                    break;
                case Effect.PowerPlus:
                    creatureEffects.Add(new PowerPlusEffect());
                    break;
                case Effect.PowerReduce:
                    creatureEffects.Add(new PowerReduceEffect());
                    break;
                case Effect.Redraw:
                    creatureEffects.Add(new RedrawEffect());
                    break;
                case Effect.Return:
                    creatureEffects.Add(new ReturnEffect());
                    break;
                case Effect.Shield:
                    creatureEffects.Add(new ShieldEffect());
                    break;
                default:
                    throw new Exception($"Effect {effectType} has not been implemented yet");
            }
        }

        return creatureEffects;
    }

    public CreatureUI creatureOwner;

    public Sprite IconSprite => Resources.Load<Sprite>(GetEffectType().ToString());

    public IEnumerator ReceiveEvent(GameEvent gameEvent)
    {
        if (RequirementsMet(gameEvent))
        {
            yield return Activate(gameEvent);
        }
    }

    public abstract IEnumerator Activate(GameEvent gameEvent);
    public abstract bool RequirementsMet(GameEvent gameEvent);

    public abstract EffectTiming GetActivationTimings();

    public abstract Effect GetEffectType();

    //public new abstract string ToString();
}
