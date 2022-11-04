using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Hand
{
    public const int MAX_CARD_IN_HAND = 5;

    public List<CreatureSO> cards = new List<CreatureSO>(MAX_CARD_IN_HAND);

    public void AddCardToHand(CreatureSO card)
    {
        cards.Add(card);
    }
}
