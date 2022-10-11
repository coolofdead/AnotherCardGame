using System;

[Flags]
public enum EffectActivationTimeType
{
    Default = 0,
    StartRound = 1,
    EndRound = 2,
    OnDamageReceived = 4,
    OnAttackLanded = 8,
    OnStatModified = 16,
}
