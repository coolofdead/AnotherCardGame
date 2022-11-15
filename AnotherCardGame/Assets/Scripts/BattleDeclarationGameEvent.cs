using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleDeclarationGameEvent : GameEvent
{
    public CreatureUI playerCreatureUI;
    public CreatureUI opponentCreatureUI;
    public int nthBattle;

    public override EffectTiming GetEventTiming()
    {
        return EffectTiming.OnAttackDeclaration;
    }
}
