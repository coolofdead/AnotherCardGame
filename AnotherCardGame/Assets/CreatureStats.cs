using UnityEngine;
using System;

[Serializable]
public class CreatureStats
{
    [RangeEx(0, 10000, 500)]
    public int power;
    [Range(0, 9)]
    public int nbHit;
    [Range(1, 9)]
    public int speed = 1;
    [RangeEx(0, 10000, 500)]
    public int shield;

    public bool Compare(CreatureStats statsToCompare, CreatureEffectRequirementType creatureEffectRequirementType)
    {
        switch (creatureEffectRequirementType)
        {
            case CreatureEffectRequirementType.Equal:
                return this == statsToCompare;
            case CreatureEffectRequirementType.Less:
                return this < statsToCompare;
            case CreatureEffectRequirementType.More:
                return this > statsToCompare;
            default:
                return false;
        }
    }

    public CreatureStats Clone()
    {
        return new CreatureStats { power = this.power, nbHit = this.nbHit, speed = this.speed, shield = this.shield };
    }

    public static CreatureStats operator +(CreatureStats a, CreatureStats b)
    {
        return new CreatureStats { power = a.power + b.power, nbHit = a.nbHit + b.nbHit, speed = a.speed + b.speed, shield = a.shield + b.shield };
    }

    public static CreatureStats operator -(CreatureStats a, CreatureStats b)
    {
        return new CreatureStats { power = a.power - b.power, nbHit = a.nbHit - b.nbHit, speed = a.speed - b.speed, shield = a.shield - b.shield };
    }

    public static bool operator >(CreatureStats a, CreatureStats b)
    {
        return (b.power == 0 || a.power > b.power)
               && (b.nbHit == 0 || a.nbHit > b.nbHit)
               && (b.speed == 0 || a.speed > b.speed)
               && (b.shield == 0 || a.shield > b.shield);
    }

    public static bool operator <(CreatureStats a, CreatureStats b)
    {
        return (b.power == 0 || a.power < b.power)
               && (b.nbHit == 0 || a.nbHit < b.nbHit)
               && (b.speed == 0 || a.speed < b.speed)
               && (b.shield == 0 || a.shield < b.shield);
    }

    public static bool operator ==(CreatureStats a, CreatureStats b)
    {
        return (b.power == 0 || a.power == b.power)
               && (b.nbHit == 0 || a.nbHit == b.nbHit)
               && (b.speed == 0 || a.speed == b.speed)
               && (b.shield == 0 || a.shield == b.shield);
    }

    public static bool operator !=(CreatureStats a, CreatureStats b)
    {
        return (b.power == 0 || a.power != b.power)
               && (b.nbHit == 0 || a.nbHit != b.nbHit)
               && (b.speed == 0 || a.speed != b.speed)
               && (b.shield == 0 || a.shield != b.shield);
    }
}
