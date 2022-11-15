using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPlusEffect : AbstractCreatureEffect
{
    public override IEnumerator Activate(GameEvent gameEvent)
    {
        BattleDeclarationGameEvent battleDeclarationGameEvent = gameEvent as BattleDeclarationGameEvent;

        CreatureUI faceOffCreatureUI = creatureOwner == battleDeclarationGameEvent.playerCreatureUI ? battleDeclarationGameEvent.opponentCreatureUI : battleDeclarationGameEvent.playerCreatureUI;

        creatureOwner.tempBonusStats.power += faceOffCreatureUI.BaseStats.manaCost * 1000;

        yield return null;
    }

    public override EffectTiming GetActivationTimings()
    {
        return EffectTiming.OnAttackDeclaration;
    }

    public override Effect GetEffectType()
    {
        return Effect.PowerPlus;
    }

    public override bool RequirementsMet(GameEvent gameEvent)
    {
        BattleDeclarationGameEvent battleDeclarationGameEvent = gameEvent as BattleDeclarationGameEvent;

        bool isPlayerCreatureFighting = creatureOwner == battleDeclarationGameEvent.playerCreatureUI;
        bool isOpoonentCreatureFighting = creatureOwner == battleDeclarationGameEvent.opponentCreatureUI;
        return isPlayerCreatureFighting || isOpoonentCreatureFighting;
    }

    public override string ToString()
    {
        return Effect.PowerPlus.ToString();
    }
}
