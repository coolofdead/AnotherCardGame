using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Player
{
    public Deck deck = new Deck();
    public Hand hand = new Hand();

    public void Init()
    {
        deck.Shuffle();
        for (int i = 0; i < Hand.MAX_CARD_IN_HAND; i++)
        {
            hand.AddCardToHand(deck.Draw());
        }
    }
}
