using System;

[Flags]
public enum EffectActivationTimeType
{
    Default = 0,
    StartRound = 1,
    EndRound = 2,
    OnDamageReceived = 4,
    OnDamageDeal = 8,
//    Not yet implemented
//    OnHitReceived = 16,
//    OnHitLanded = 32,
    OnSelfStatModified = 64,
    OnOpponentStatModified = 128,
    OnEndFight = 256,
}
