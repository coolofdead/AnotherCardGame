using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Player
{
    public const int MAX_MANA = 4;

    public Action<int> onManaChanged;

    public Deck deck = new Deck();
    public Hand hand = new Hand();
    public int mana = MAX_MANA;

    public bool isReadyToStartRound = false;
    public int damageCounter;

    public void Init()
    {
        // deck.Load();
        deck.Shuffle();
        for (int i = 0; i < Hand.MAX_CARD_IN_HAND; i++)
        {
            hand.AddCardToHand(deck.Draw());
        }
    }

    public void PayManaCost(int cost)
    {
        mana -= cost;

        onManaChanged?.Invoke(mana);
    }

    public void RefundManaCost(int cost)
    {
        PayManaCost(-cost);
    }
}
