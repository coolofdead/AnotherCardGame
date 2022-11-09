using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[Serializable]
public class Deck
{
    public const int MAX_CARD_DECK = 14;

    public List<CreatureSO> creatures = new List<CreatureSO>(MAX_CARD_DECK);

    public void LoadDeck(string recipe)
    {
        Shuffle();

        throw new NotImplementedException();
    }

    public void Shuffle()
    {
        creatures = creatures.Randomize().ToList();
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
