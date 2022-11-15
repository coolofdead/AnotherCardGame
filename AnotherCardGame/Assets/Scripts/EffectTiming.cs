using System;

[Flags]
public enum EffectTiming
{
    OnSummon = 1, 
    OnAttackDeclaration = 2, 
    OnDeath = 4, 
    OnBattleFinished = 8,
    OnEndOfTurn = 16,
    OnAllCreatureSummoned = 32,
}
