using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FirstAttackerEffect : AbstractCreatureEffect
{
    public override IEnumerator Activate(GameEvent gameEvent)
    {
        creatureOwner.tempBonusStats.power += 1000;

        yield return null;
    }

    public override EffectTiming GetActivationTimings()
    {
        return EffectTiming.OnAttackDeclaration;
    }

    public override Effect GetEffectType()
    {
        return Effect.FirstAttacker;
    }

    public override bool RequirementsMet(GameEvent gameEvent)
    {
        BattleDeclarationGameEvent battleDeclarationGameEvent = gameEvent as BattleDeclarationGameEvent;

        bool isPlayerCreatureFighting = creatureOwner == battleDeclarationGameEvent.playerCreatureUI;
        bool isOpoonentCreatureFighting = creatureOwner == battleDeclarationGameEvent.opponentCreatureUI;
        return battleDeclarationGameEvent.nthBattle == BattleFightManager.FIRST_BATTLE_NTH && (isPlayerCreatureFighting || isOpoonentCreatureFighting);
    }

    public override string ToString()
    {
        return Effect.FirstAttacker.ToString();
    }
}
