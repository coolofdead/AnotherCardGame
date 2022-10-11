using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Deck
{
    public CreatureSO[] creatures = new CreatureSO[9];

    public void Shuffle()
    {
        (new System.Random()).Shuffle(creatures);
    }

    public CreatureSO PickupRandomCreature()
    {
        // TODO
        return null;
    }
}
