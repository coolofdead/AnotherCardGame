using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Hand : IEnumerable
{
    public const int MAX_CARD_IN_HAND = 5;

    [SerializeField] private List<CreatureSO> cards = new List<CreatureSO>(MAX_CARD_IN_HAND);
    public int CurrentCardsInHand => cards.Count;

    public void RemoveCard(CreatureSO card)
    {
        cards.Remove(card);
    }

    public void AddCard(CreatureSO card)
    {
        if (cards.Count < MAX_CARD_IN_HAND)
            cards.Add(card);
    }

    public IEnumerator GetEnumerator()
    {
        foreach (CreatureSO creatureSO in cards)
        {
            yield return creatureSO;
        }
    }
}
