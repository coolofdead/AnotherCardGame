using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FirstAttackerEffect : AbstractCreatureEffect
{
    public override void Activate(GameEvent gameEvent)
    {
        gameEvent.creatureUI.stats.power += gameEvent.creatureUI.creatureSO.stats.manaCost * 1000;
    }

    public override EffectTiming GetActivationTimings()
    {
        return EffectTiming.OnAttackDeclaration | EffectTiming.OnBattleFinished;
    }

    public override bool RequirementsMet(GameEvent gameEvent)
    {
        BattleDeclarationGameEvent battleDeclarationGameEvent = gameEvent as BattleDeclarationGameEvent;

        bool isPlayerCreatureFighting = gameEvent.creatureUI == battleDeclarationGameEvent.playerCreatureUI;
        bool isOpoonentCreatureFighting = gameEvent.creatureUI == battleDeclarationGameEvent.opponentCreatureUI;
        return battleDeclarationGameEvent.nthBattle == 0 && (isPlayerCreatureFighting || isOpoonentCreatureFighting);
    }

    public override string ToString()
    {
        return Effect.First_Attacker.ToString();
    }
}
