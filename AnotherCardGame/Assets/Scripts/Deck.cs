using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Deck
{
    public const int MAX_CARD_DECK = 14;

    public List<CreatureSO> creatures = new List<CreatureSO>(MAX_CARD_DECK);

    public void LoadDeck(string recipe)
    {
        throw new NotImplementedException();
    }

    public void Shuffle()
    {
        System.Random random = new System.Random();
        creatures.Sort((x, y) => random.Next());
    }

    public CreatureSO Draw()
    {
        CreatureSO drawnCreature = null;
        if (creatures.Count != 0)
        {
            drawnCreature = creatures[0];
            creatures.RemoveAt(0);
        }

        return drawnCreature;
    }
}
