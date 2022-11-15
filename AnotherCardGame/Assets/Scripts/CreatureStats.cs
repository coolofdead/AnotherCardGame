using UnityEngine;
using System;

[Serializable]
public class CreatureStats
{
    [Range(0, 4)]
    public int manaCost;
    [RangeEx(0, 14000, 500)]
    public int power;
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
        return new CreatureStats { manaCost = this.manaCost, power = this.power, shield = this.shield };
    }

    public static CreatureStats operator +(CreatureStats a, CreatureStats b)
    {
        CreatureStats newStats = new CreatureStats { manaCost = a.manaCost + b.manaCost, power = a.power + b.power, shield = a.shield + b.shield };

        newStats.manaCost = newStats.manaCost < 0 ? 0 : newStats.manaCost;
        newStats.power = newStats.power < 0 ? 0 : newStats.power;
        newStats.shield = newStats.shield < 0 ? 0 : newStats.shield;

        return newStats;
    }

    public static CreatureStats operator -(CreatureStats a, CreatureStats b)
    {
        CreatureStats newStats = new CreatureStats { manaCost = a.manaCost - b.manaCost, power = a.power - b.power, shield = a.shield - b.shield };
        newStats.manaCost = newStats.manaCost < 0 ? 0 : newStats.manaCost;
        newStats.power = newStats.power < 0 ? 0 : newStats.power;
        newStats.shield = newStats.shield < 0 ? 0 : newStats.shield;

        return newStats;
    }

    public static CreatureStats operator *(CreatureStats a, CreatureStats b)
    {
        b.power = b.power == 0 ? 1 : b.power;
        b.shield = b.shield == 0 ? 1 : b.shield;
        b.manaCost = b.manaCost == 0 ? 1 : b.manaCost;
        return new CreatureStats { manaCost = a.manaCost * b.manaCost, power = a.power * b.power, shield = a.shield * b.shield };
    }

    public static CreatureStats operator /(CreatureStats a, CreatureStats b)
    {
        b.power = b.power == 0 ? 1 : b.power;
        b.shield = b.shield == 0 ? 1 : b.shield;
        b.manaCost = b.manaCost == 0 ? 1 : b.manaCost;
        return new CreatureStats { manaCost = a.manaCost / b.manaCost, power = a.power / b.power, shield = a.shield / b.shield };
    }

    public static bool operator >(CreatureStats a, CreatureStats b)
    {
        return (b.power == 0 || a.power > b.power) && (b.shield == 0 || a.shield > b.shield);
    }

    public static bool operator <(CreatureStats a, CreatureStats b)
    {
        return (b.power == 0 || a.power < b.power) && (b.shield == 0 || a.shield < b.shield);
    }

    public static bool operator ==(CreatureStats a, CreatureStats b)
    {
        return (b.power == 0 || a.power == b.power) && (b.shield == 0 || a.shield == b.shield);
    }

    public static bool operator !=(CreatureStats a, CreatureStats b)
    {
        return (b.power == 0 || a.power != b.power) && (b.shield == 0 || a.shield != b.shield);
    }
}
