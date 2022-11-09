using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Player
{
    public const int MAX_MANA = 4;

    public Action<int> onManaChanged;
    public Action<Player> onDamageReceived;

    public Deck deck = new Deck();
    public Hand hand = new Hand();
    public int mana = MAX_MANA;

    public bool isReadyToStartRound = false;
    public int DamageCounter { get; protected set; }

    public void FillHandAndResetMana()
    {
        deck.Shuffle();

        mana = MAX_MANA;

        FillHand();
    }

    private void FillHand()
    {
        while (hand.CurrentCardsInHand < Hand.MAX_CARD_IN_HAND)
        {
            hand.AddCardToHand(deck.Draw());
        }
    }
    
    public void RefundManaCost(int cost) => PayManaCost(-cost);
    public void PayManaCost(int cost)
    {
        mana -= cost;

        onManaChanged?.Invoke(mana);
    }

    public void ReceiveDamage(int amount)
    {
        if (amount == 0) return;

        DamageCounter += amount;

        onDamageReceived?.Invoke(this);
    }
}
