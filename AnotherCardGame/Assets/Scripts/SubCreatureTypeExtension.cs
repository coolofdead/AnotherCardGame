public static class SubCreatureTypeExtension
{
    public static SubCreatureType GetSubCreatureType(CreatureSO creature)
    {
        SubCreatureType subCreatureType = default;
        switch (creature.creatureType)
        {
            case CreatureType.Fire:
                subCreatureType = (SubCreatureType)creature.subCreatureTypeFire;
                break;
            case CreatureType.Plant:
                subCreatureType = (SubCreatureType)creature.subCreatureTypePlant;
                break;
            case CreatureType.Water:
                subCreatureType = (SubCreatureType)creature.subCreatureTypeWater;
                break;
            default:
                break;
        }

        return subCreatureType;
    }
}
