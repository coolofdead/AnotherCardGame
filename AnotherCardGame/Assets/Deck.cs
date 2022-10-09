using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Deck
{
    public CreatureSO[] creatures = new CreatureSO[5];
    public CardActionSO[] cardActions = new CardActionSO[15];

    public void Shuffle()
    {
        (new System.Random()).Shuffle(creatures);
        (new System.Random()).Shuffle(cardActions);
    }
}
